<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'+ Se define la variable para el pase de valores a los campos de encabezado
Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mstrString As String
Dim mobjMantClaim As eClaim.Ord_type
Dim mstrQueryString As String
Dim mintBranch As Object

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'% insValMantProf_ord: Se realizan las validaciones masivas de la forma	
'--------------------------------------------------------------------------------------------
Function insValMantProf_ord() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ MOS661: Tipos de órdenes de servicios profesionales
		Case "MOS661"
			mobjMantClaim = New eClaim.Ord_type
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantProf_ord = mobjMantClaim.insValMOS661_k(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mstrQueryString = "&nCurrency=" & Request.Form.Item("valCurrency") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate")
						
						insValMantProf_ord = mobjMantClaim.insValMOS661(.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeOrd_typeCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble))
					Else
						insValMantProf_ord = vbNullString
					End If
				End If
			End With
			
		Case Else
			insValMantProf_ord = "insValMantProf_ord: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostMantProf_ord: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantProf_ord() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lblnFirst As Object
	Dim lintFirst As Object
	Dim lintIndex As Object
	Dim lintCheck As Object
	
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ MOS661: Tipos de órdenes de servicios profesionales
		Case "MOS661"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nCurrency=" & Request.Form.Item("valCurrency") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate")
					lblnPost = True
				Else
					mstrQueryString = "&nCurrency=" & Request.Form.Item("hddnCurrency") & "&dEffecdate=" & Request.Form.Item("hdddEffecdate")
					
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantClaim.insPostMOS661Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeOrd_typeCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						
					Else
						lblnPost = True
					End If
					
					lblnPost = True
				End If
			End With
	End Select
	insPostMantProf_ord = lblnPost
End Function

</script>
<%Response.Expires = -1
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>




<SCRIPT>
//-Variable para el control de Versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:20 $|$$Author: Nvaplat61 $"
</SCRIPT>
</HEAD>
	<%
If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY>
	<%	
Else
	%><BODY CLASS="Header">
	<%	
End If

mstrCommand = "&sModule=Maintenance&sProject=MantProf_ord&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values

'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValMantProf_ord
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & server.URLEncode(mstrCommand) & "&sQueryString=" & server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantProf_ordError"",660,330);self.document.location.href='/VTimeNet/Common/Blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantProf_ord Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & mstrQueryString & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & """;</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End If
			End If
			'+ Se mueve automaticamente a la siguiente página
		Else
			Select Case Request.QueryString.Item("sCodispl")
				'+ Tipos de órdenes de servicios profesionales
				Case "MOS661"
					Response.Write("<SCRIPT>top.opener.document.location.href='MOS661.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
			End Select
		End If
	End If
End If

mobjValues = Nothing
mobjMantClaim = Nothing
%>
</BODY>
</HTML>




