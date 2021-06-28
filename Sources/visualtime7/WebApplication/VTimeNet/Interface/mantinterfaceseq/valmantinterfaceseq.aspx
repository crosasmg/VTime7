<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<script language="VB" runat="Server">

Dim mobjMantInterface As Object
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
	Dim lstrobligatory As String
	'--------------------------------------------------------------------------------------------
	
	Select Case Request.QueryString.Item("sCodispl")
		
		Case "MGI1401"
			mobjMantInterface = New eInterface.MasterSheet
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalmantinterface = mobjMantInterface.insValMGI1401_K("MGI1401", mobjValues.StringToType(.Form.Item("tcnsheet"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tcsdescript"), mobjValues.StringToType(.Form.Item("cbeFormat"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbePeriod"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("chksautomatic"), .Form.Item("tcsshortdesc"))
				End If
			End With
			mobjMantInterface = Nothing
			
		Case "MGI1406"
			mobjMantInterface = New eInterface.tablesheet
			With Request
				
				If .QueryString.Item("WindowType") = "PopUp" Then
					insvalmantinterface = mobjMantInterface.InsValMGI1406("MGI1406", .Form.Item("tcstable"), .Form.Item("tcsalias"), mobjValues.StringToType(.Form.Item("tcnorder"), eFunctions.Values.eTypeData.etdLong, True))
				Else
					insvalmantinterface = ""
				End If
			End With
			mobjMantInterface = Nothing
			
		Case "MGI1407"
			mobjMantInterface = New eInterface.FieldSheet
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					If .Form.Item("chksobligatory") <> "1" Then
						lstrobligatory = "2"
					Else
						lstrobligatory = "1"
					End If
					insvalmantinterface = mobjMantInterface.InsValMGI1407("MGI1407", Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnfield"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString.Item("nFieldtype"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tcsfielddesc"), .Form.Item("cbestable"), .Form.Item("tcscolumnname"), mobjValues.StringToType(.Form.Item("tcnfieldorder"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbendatatype"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnfieldlarge"), eFunctions.Values.eTypeData.etdLong, True), lstrobligatory, .Form.Item("tcsfieldcommen"), mobjValues.StringToType(.Form.Item("cbenoperator"), eFunctions.Values.eTypeData.etdLong, True))
				Else
					insvalmantinterface = ""
				End If
			End With
			mobjMantInterface = Nothing
			
		Case "MGI1408"
			mobjMantInterface = New eInterface.calend
			With Request
				
				If .QueryString.Item("WindowType") = "PopUp" Then
					insvalmantinterface = mobjMantInterface.InsValMGI1408("MGI1408", mobjValues.StringToType(Session("nPeriod"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnday"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("ddateproc"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tcshour"))
				Else
					insvalmantinterface = ""
				End If
			End With
			mobjMantInterface = Nothing
			
		Case "MGI1405"
			mobjMantInterface = New eInterface.MasterSheet
			With Request
                    insvalmantinterface = mobjMantInterface.insValMGI1405(.QueryString("sCodispl"),
                                                                          mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdLong, True),
                                                                          .Form.Item("chkSheet_father"), .Form.Item("cbeStatusSheet"),
                                                                          .Form.Item("tctPrefix_fname"))
			End With
			mobjMantInterface = Nothing
			
		Case "MGI1410"
			mobjMantInterface = New eInterface.Depend_Sheet
			With Request
				
				If .QueryString.Item("WindowType") = "PopUp" Then
					insvalmantinterface = mobjMantInterface.InsValMGI1410Upd(.QueryString("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("valSheet_Child"), eFunctions.Values.eTypeData.etdLong, True))
				Else
					insvalmantinterface = mobjMantInterface.InsValMGI1410(.QueryString("sCodispl"), mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdLong, True))
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
	Dim lstrlastmove As String
	Dim lstrobligatory As String
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+MGI1401: Mantenimiento de Interfaces
		Case "MGI1401"
			With Request
				
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mobjMantInterface = New eInterface.MasterSheet
					
					lblnPost = mobjMantInterface.insPostMGI1401_k(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnsheet"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcsdescript"), .Form.Item("tcsshortdesc"), mobjValues.StringToType(.Form.Item("optnintertype"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOpertype"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tcsprocess"), mobjValues.StringToType(.Form.Item("cbeFormat"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbnsystem"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("chksautomatic"), .Form.Item("chksonline"), .Form.Item("chksgroupby"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbePeriod"), eFunctions.Values.eTypeData.etdLong, True))
					Session("nSheet") = .Form.Item("tcnSheet")
					If .Form.Item("cbePeriod") = "0" Then
						Session("nPeriod") = vbNullString
					Else
						Session("nPeriod") = .Form.Item("cbePeriod")
					End If
					Session("nIntertype") = .Form.Item("optnintertype")
				End If
			End With
			
			'+MGI1406: Tablas de Interfaz
		Case "MGI1406"
			With Request
				
				mobjMantInterface = New eInterface.tablesheet
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantInterface.insPostMGI1406(.QueryString("Action"), mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tcstable"), .Form.Item("tcsalias"), mobjValues.StringToType(.Form.Item("tcnorder"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True))
				Else
					lblnPost = True
				End If
			End With
			
			'+MGI1407: Campos de Interfaz
		Case "MGI1407"
			With Request
				
				mobjMantInterface = New eInterface.FieldSheet
				If .QueryString.Item("WindowType") = "PopUp" Then
					mstrQueryString = "&nFieldType=" & mobjValues.StringToType(.QueryString.Item("nFieldtype"), eFunctions.Values.eTypeData.etdLong, True)
					
					If .Form.Item("chksobligatory") <> "1" Then
						lstrobligatory = "2"
					Else
						lstrobligatory = "1"
					End If
					If .Form.Item("chkslastmove") <> "1" Then
						lstrlastmove = "2"
					Else
						lstrlastmove = "1"
					End If
					lblnPost = mobjMantInterface.insPostMGI1407(.QueryString("Action"), mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnfield"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString.Item("nFieldtype"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("cbestable"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tcsfielddesc"), .Form.Item("tcscolumnname"), .Form.Item("tcsvalue"), .Form.Item("tcsrutine"), mobjValues.StringToType(.Form.Item("tcnrowdorder"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnfieldorder"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tcsvalueslist"), mobjValues.StringToType(.Form.Item("cbendatatype"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnfieldlarge"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbenobjtype"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbentablehomo"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbenoperator"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbencondit"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tcsfieldcommen"), .Form.Item("tcsfieldrel"), lstrobligatory, lstrlastmove, mobjValues.StringToType(.Form.Item("tcnDecimal"), eFunctions.Values.eTypeData.etdLong, True))
				Else
					lblnPost = True
				End If
			End With
			
			'+MGI1408: Calendario de cada Interfaz
		Case "MGI1408"
			With Request
				
				mobjMantInterface = New eInterface.calend
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantInterface.insPostMGI1408(.QueryString("Action"), mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnid"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("ddateproc"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnday"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tcshour"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True))
				Else
					lblnPost = True
				End If
			End With
			
			'+MGI1405: Datos generales
		Case "MGI1405"
			With Request
				mobjMantInterface = New eInterface.MasterSheet
                    lblnPost = mobjMantInterface.InsPostMGI1405(mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdDouble),
                                                                .Form.Item("cbeStatusSheet"),
                                                                .Form.Item("tctPrefix_fname"),
                                                                .Form.Item("tctSeparator"),
                                                                .Form.Item("tctSpace"),
                                                                mobjValues.StringToType(.Form.Item("optnAling"), eFunctions.Values.eTypeData.etdLong),
                                                                .Form.Item("chkHeader"),
                                                                .Form.Item("chkTotal"),
                                                                mobjValues.StringToType(.Form.Item("tcnPosition"), eFunctions.Values.eTypeData.etdLong),
                                                                .Form.Item("chkMassive"),
                                                                mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong),
                                                                .Form.Item("chkNogrid"),
                                                                .Form.Item("chkView_interface"),
                                                                .Form.Item("chkView_Report"),
                                                                .Form.Item("tctReport"),
                                                                .Form.Item("chkSheet_father"),
                                                                .Form.Item("chkFile_unique"),
                                                                .Form.Item("tctQuery"),
                                                                .Form.Item("chkXsl"),
                                                                .Form.Item("tctQuery_xsl"),
                                                                .Form.Item("tctName_routine"),
                                                                .Form.Item("tctOut_routine"),
                                                                .Form.Item("tctsworkflowname"),
                                                                .Form.Item("tctsfolder"),
                                                                .Form.Item("tctQueProcess"),
                                                                .Form.Item("tctQueQuery"))
			End With
			
		Case "MGI1410"
			mobjMantInterface = New eInterface.Depend_Sheet
			With Request
				
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantInterface.InsPostMGI1410Upd(.QueryString("Action"), mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("valSheet_Child"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True))
				Else
					lblnPost = True
				End If
			End With
			mobjMantInterface = Nothing
	End Select
	
	insPostMantInterface = lblnPost
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

mstrCommand = "&sModule=Interface&sProject=Mantinterfaceseq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 3/11/03 17:08 $|$$Author: Nvaplat28 $"
	
</SCRIPT>
</HEAD>
<BODY>
<FORM id=form1 name=form1>
<%
'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalmantinterface
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
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantinterfaceseqError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostMantInterface Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Interface/Mantinterfaceseq/Sequence.aspx?nAction=" & Request.QueryString.Item("nAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrCommand & "';</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Interface/Mantinterfaceseq/Sequence.aspx?nMainAction=" & Request.QueryString.Item("nAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & mstrCommand & "';</SCRIPT>")
				End If
			Else
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "MGI1406"
						Response.Write("<SCRIPT>top.opener.document.location.href='MGI1406.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
						
					Case "MGI1407"
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.opener.document.location.href='MGI1407.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();opener.top.opener.document.location.href='MGI1407.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
						End If
						
					Case "MGI1408"
						Response.Write("<SCRIPT>top.opener.document.location.href='MGI1408.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
						
					Case "MGI1410"
						Response.Write("<SCRIPT>top.opener.document.location.href='MGI1410.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
						
				End Select
			End If
		End If
	End If
Else
	If insFinish() Then
		With Response
			.Write("<SCRIPT>")
			.Write("insReloadTop(false)")
			.Write("</SCRIPT>")
		End With
	End If
End If
mobjMantInterface = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




