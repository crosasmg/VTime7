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
	Dim lstrAction As String
	Dim lstrAutomatic As String
	'--------------------------------------------------------------------------------------------
	
	Select Case Request.QueryString.Item("sCodispl")
		
		Case "MGI1401"
			mobjMantInterface = New eInterface.MasterSheet
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					If .QueryString.Item("nMainAction") = "301" Then
						lstrAction = "Add"
					ElseIf .QueryString.Item("nMainAction") = "302" Then 
						lstrAction = "Update"
					End If
					If .Form.Item("chksautomatic") = "1" Then
						lstrAutomatic = "1"
					Else
						lstrAutomatic = "2"
					End If
					insvalmantinterface = mobjMantInterface.insValMGI1401_K("MGI1401", mobjValues.StringToType(.Form.Item("tcnsheet"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tcsdescript"), mobjValues.StringToType(.Form.Item("valnformat"), eFunctions.Values.eTypeData.etdLong), lstrAction, mobjValues.StringToType(.Form.Item("valnperiod"), eFunctions.Values.eTypeData.etdLong), lstrAutomatic, .Form.Item("tcsshortdesc"), .Form.Item("valsestado"))
				End If
				session("nSheet") = Request.Form.Item("tcnSheet")
				session("nPeriod") = Request.Form.Item("valnperiod")
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
					insvalmantinterface = mobjMantInterface.InsValMGI1407("MGI1407", Request.QueryString.Item("Action"), mobjValues.StringToType(session("nSheet"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnfield"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString.Item("nFieldtype"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tcsfielddesc"), .Form.Item("cbestable"), .Form.Item("tcscolumnname"), mobjValues.StringToType(.Form.Item("tcnfieldorder"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbendatatype"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnfieldlarge"), eFunctions.Values.eTypeData.etdLong, True), lstrobligatory, .Form.Item("tcsfieldcommen"), mobjValues.StringToType(.Form.Item("cbenoperator"), eFunctions.Values.eTypeData.etdLong, True))
				Else
					insvalmantinterface = ""
				End If
			End With
			mobjMantInterface = Nothing
			
		Case "MGI1408"
			mobjMantInterface = New eInterface.calend
			With Request
				
				If .QueryString.Item("WindowType") = "PopUp" Then
					insvalmantinterface = mobjMantInterface.InsValMGI1408("MGI1408", mobjValues.StringToType(session("nPeriod"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnday"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("ddateproc"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tcshour"))
				Else
					insvalmantinterface = ""
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
	Dim lstrAction As String
	Dim lstrlastmove As String
	Dim lstrobligatory As String
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lstrAutomatic As String
	Dim lstrgroupby As String
	Dim lstrOnline As String
	
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+MGI1401: Mantenimiento de Interfaces
		Case "MGI1401"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mobjMantInterface = New eInterface.MasterSheet
					
					If .QueryString.Item("nMainAction") = "301" Then
						lstrAction = "Add"
					ElseIf .QueryString.Item("nMainAction") = "302" Then 
						lstrAction = "Update"
					End If
					If .Form.Item("chksautomatic") = "1" Then
						lstrAutomatic = "1"
					Else
						lstrAutomatic = "2"
					End If
					If .Form.Item("chksonline") = "1" Then
						lstrOnline = "1"
					Else
						lstrOnline = "2"
					End If
					If .Form.Item("chksgroupby") = "1" Then
						lstrgroupby = "1"
					Else
						lstrgroupby = "2"
					End If
					lblnPost = mobjMantInterface.insPostMGI1401_k(lstrAction, mobjValues.StringToType(.Form.Item("tcnsheet"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcsdescript"), .Form.Item("tcsshortdesc"), mobjValues.StringToType(.Form.Item("optnintertype"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valnopertype"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tcsprocess"), mobjValues.StringToType(.Form.Item("valnformat"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbnsystem"), eFunctions.Values.eTypeData.etdLong, True), lstrAutomatic, lstrOnline, lstrgroupby, mobjValues.StringToType(session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("valsestado"), mobjValues.StringToType(.Form.Item("valnperiod"), eFunctions.Values.eTypeData.etdLong))
					session("nIntertype") = .Form.Item("optnintertype")
				End If
			End With
			
			'+MGI1406: Tablas de Interfaz
		Case "MGI1406"
			With Request
				
				mobjMantInterface = New eInterface.tablesheet
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantInterface.insPostMGI1406(.QueryString("Action"), mobjValues.StringToType(session("nSheet"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tcstable"), .Form.Item("tcsalias"), mobjValues.StringToType(.Form.Item("tcnorder"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True))
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
					lblnPost = mobjMantInterface.insPostMGI1407(.QueryString("Action"), mobjValues.StringToType(session("nSheet"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnfield"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString.Item("nFieldtype"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("cbestable"), mobjValues.StringToType(session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tcsfielddesc"), .Form.Item("tcscolumnname"), .Form.Item("tcsvalue"), .Form.Item("tcsrutine"), mobjValues.StringToType(.Form.Item("tcnrowdorder"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnfieldorder"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tcsvalueslist"), mobjValues.StringToType(.Form.Item("cbendatatype"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnfieldlarge"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbenobjtype"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbentablehomo"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbenoperator"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbencondit"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tcsfieldcommen"), .Form.Item("tcsfieldrel"), lstrobligatory, lstrlastmove)
				Else
					lblnPost = True
				End If
			End With
			
			'+MGI1408: Calendario de cada Interfaz
		Case "MGI1408"
			With Request
				
				mobjMantInterface = New eInterface.calend
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantInterface.insPostMGI1408(.QueryString("Action"), mobjValues.StringToType(session("nSheet"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnid"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("ddateproc"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnday"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tcshour"), mobjValues.StringToType(session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True))
				Else
					lblnPost = True
				End If
			End With
			
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
	session("sErrorTable") = mstrErrors
	session("sForm") = Request.Form.ToString
Else
	session("sErrorTable") = vbNullString
	session("sForm") = vbNullString
End If

If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & server.URLEncode(mstrCommand) & "&sQueryString=" & server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantinterfaceseqError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
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
						Response.Write("<SCRIPT>top.opener.document.location.href='MGI1407.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
						
					Case "MGI1408"
						Response.Write("<SCRIPT>top.opener.document.location.href='MGI1408.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
						
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




