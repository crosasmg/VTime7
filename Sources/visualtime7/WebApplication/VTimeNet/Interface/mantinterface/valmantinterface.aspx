<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<script language="VB" runat="Server">

Dim mobjMantInterface As eInterface.Homolog_table
Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mstrString As Object

'+  Variable para usar el querystring
Dim mstrQueryString As String

'- Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'% insvalmantinterface: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalmantinterface() As String
	'--------------------------------------------------------------------------------------------
	
	Select Case Request.QueryString.Item("sCodispl")
		
		Case "MGI1400"
			mobjMantInterface = New eInterface.Homolog_table
			
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalmantinterface = mobjMantInterface.insValMGI1400_K("MGI1400", mobjValues.StringToType(.Form.Item("cbeSystem"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("valTable"), eFunctions.Values.eTypeData.etdLong))
				Else
					If Request.QueryString.Item("nMainAction") <> "401" Then
						insvalmantinterface = mobjMantInterface.insValMGI1400("MGI1400", mobjValues.StringToType(.Form.Item("tcnId"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCampovt"), .Form.Item("tctCodvt"), .Form.Item("tctValorvt"), .Form.Item("tctTablase"), .Form.Item("tctCampose"), .Form.Item("tctValorse"), .Form.Item("chkPredom"))
					End If
				End If
			End With
			mobjMantInterface = Nothing
			
		Case Else
			insvalmantinterface = "insvalmantinterface: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
			
	End Select
End Function

'% insPostMantInterface: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantInterface() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+MGI1400: Homologacion de Codigos
		Case "MGI1400"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
					mstrQueryString = "&nSystem=" & mobjValues.StringToType(.Form.Item("cbeSystem"), eFunctions.Values.eTypeData.etdLong, True) & "&nTable=" & mobjValues.StringToType(.Form.Item("valTable"), eFunctions.Values.eTypeData.etdLong)
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mobjMantInterface = New eInterface.Homolog_table
						lblnPost = mobjMantInterface.insPostMGI1400(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nSystem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnId"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCampovt"), .Form.Item("tctCodvt"), .Form.Item("tctValorvt"), .Form.Item("tctTablase"), .Form.Item("tctCampose"), .Form.Item("tctValorse"), .Form.Item("chkPredom"), session("nUsercode"))
					Else
						lblnPost = True
					End If
				End If
			End With
			
	End Select
	
	insPostMantInterface = lblnPost
End Function

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mstrCommand = "sModule=Interface&sProject=MantInterface&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




	<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("GE002"))
End With
%> 
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 3/11/03 17:08 $|$$Author: Nvaplat28 $"
	
//% NewLocation: se recalcula la ruta de la página
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
<FORM id=form1 name=form1>
<%
'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalmantinterface
	session("sErrorTable") = mstrErrors
	session("sForm") = Request.Form.ToString
Else
	session("sErrorTable") = vbNullString
	session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & server.URLEncode(mstrCommand) & "&sQueryString=" & server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantGeneralError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantInterface Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.insReloadTop(true,false);</SCRIPT>")
				End If
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
					End If
				Else
					
					'+ Se realiza un manejo especial pues de tratarse de algún frame de la secuencia, tiene que recargar el fraSequence.
					Select Case Request.QueryString.Item("sCodispl")
						Case "MS010"
							Response.Write("<SCRIPT>self.history.go(-1); top.frames['fraSequence'].document.location='/VTimeNet/Maintenance/MAntGeneral/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sOriginalForm=" & Request.Form.Item("tctOriginalForm") & "';</SCRIPT>")
						Case Else
							Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
					End Select
				End If
			End If
		Else
			
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "MGI1400"
					Response.Write("<SCRIPT>top.opener.document.location.href='MGI1400.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nSystem=" & Request.QueryString.Item("nSystem") & "&nTable=" & Request.QueryString.Item("nTable") & "&nMainAction=302'</SCRIPT>")
					
			End Select
		End If
	End If
End If

mobjMantInterface = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




