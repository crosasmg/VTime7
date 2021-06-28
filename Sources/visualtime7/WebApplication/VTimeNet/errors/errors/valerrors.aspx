<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" ValidateRequest="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eErrors" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">

'- Variable que guarda las validaciones
Dim mstrErrors As String

'- Objeto para el manejo de las funciones generales
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de los métodos VAL y POST de las transacciones
Dim mobjError As Object

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

'-Variable para indicar si se muestra ventana de componentes modificados
Dim mblnShowCompon As Boolean


'% insvalError: Se realizan las validaciones de las formas
'--------------------------------------------------------------------------------------------
Function insvalError() As String
'Declaracion de variables
        'Dim eIniVal As Object
        'Dim eEndVal As Integer
	'--------------------------------------------------------------------------------------------
	'^^Begin Trace Block 08/09/2005 05:40:40 p.m.
        'Call insCommonFunction("valerrors", Request.QueryString.Item("sCodispl"), eIniVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
	Dim lstrPerType As Object
	Dim lstrInforType As Object
	
	Select Case Request.QueryString.Item("sCodispl")
		'+ Actualización de Errores
		Case "ER001"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
				'   Public Function insValER001_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nErroNum As Integer) As String
					insvalError = mobjError.insValER001_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnErrorNum"), eFunctions.Values.eTypeData.etdLong))
				Else
					If (Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) And Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionCut)) Then
						insvalError = mobjError.insValER001(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdLong), UCase(.Form.Item("tctCodisp")), mobjValues.StringToType(.Form.Item("cbePriority"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeErrorType"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctShortDesc"), Request.Form.Item("txtDescript"), Request.Form.Item("cbeSeverity"), mobjValues.StringToType(.Form.Item("hddModuleError"), eFunctions.Values.eTypeData.etdLong))
					End If
				End If
			End With
			
			'+ Consulta de Errores            
		Case "ER002"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalError = mobjError.insValER002_K(.QueryString.Item("sCodispl"), UCase(.Form.Item("tctCodisp")), mobjValues.StringToType(.Form.Item("cbeStaterr"), eFunctions.Values.eTypeData.etdLong))
					
					Session("sCodispl") = UCase(.Form.Item("tctCodisp"))
					Session("sStaterr") = .Form.Item("cbeStaterr")
					Session("nSrcerr") = .Form.Item("cbeSrcerr")
				Else
					insvalError = vbNullString
				End If
			End With
			
			'+ Actualizacion de Estado de los Errores.
		Case "ER003"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nErrorNum") = .Form.Item("tcnErrorNum")
					insvalError = mobjError.insValnErrorNum(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcnErrorNum"), eFunctions.Values.eTypeData.etdLong))
				Else
					If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						insvalError = mobjError.insValER003(.QueryString("sCodispl"), Session("nErrorNum"), .Form.Item("tctUserDetect"), mobjValues.StringToType(.Form.Item("cbeErrorType"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeStaterr"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctHourDetect"), mobjValues.StringToType(.Form.Item("tcnDays"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctHours"), .Form.Item("txtDescript"))
					End If
				End If
			End With
			
			'+ Historia de un Error
		Case "ER004"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalError = mobjError.insValnErrorNum(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcnErrorNum"), eFunctions.Values.eTypeData.etdLong))
				Else
					insvalError = vbNullString
				End If
			End With
			
			'+Componentes según error
		Case "ER005"
			mobjError = New eErrors.Err_Comp
			With Request
				insvalError = mobjError.InsValER005(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnError"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnId"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbeCompType"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tctName"), .Form.Item("tctPath"), mobjValues.StringToType(.Form.Item("tcnVersion"), eFunctions.Values.eTypeData.etdLong, True))
			End With
			
			'+ Reporte de Errores
		Case "ERL001"
			insvalError = vbNullString
			
			'+ Asignación de errores por transacción
		Case "ER006"
			insvalError = mobjError.InsValER006(Request.QueryString.Item("sCodispl"), Request.Form.Item("valCodisp"), Request.Form.Item("tctUserAssign"))
			
			'+ aActualización masiva de errores
		Case "ER007"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalError = mobjError.insValER007_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdLong), UCase(.Form.Item("tctCodisp")), .Form.Item("cbeStaterr"), mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbePriority"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeSeverity"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("hddModuleError"), eFunctions.Values.eTypeData.etdLong))
				Else
					If Request.QueryString.Item("WindowType") = "PopUp" Then
						insvalError = mobjError.insValER007(.QueryString("sCodispl"), .Form.Item("cbeStaterr_new"), .Form.Item("tctUser"))
					End If
				End If
			End With
			
		Case Else
                insvalError = "insvalError: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
	'^^Begin Trace Block 08/09/2005 05:40:40 p.m.
        'Call insCommonFunction("valerrors", Request.QueryString.Item("sCodispl"), eEndVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
End Function



'% insPostError: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostError() As Boolean
        'Dim eIniPost As Object
        'Dim eEndPost As Integer
'--------------------------------------------------------------------------------------------
	'^^Begin Trace Block 08/09/2005 05:40:40 p.m.
        'Call insCommonFunction("valerrors", Request.QueryString.Item("sCodispl"), eIniPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
	Dim lblnPost As Boolean
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		'+ Actualización de Errores
		Case "ER001"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
					mobjError.bErr_Module = True
					Session("nErrorNum") = mobjError.Generate(.Form.Item("tcnErrorNum"), Request.QueryString.Item("nMainAction"), Session("nUsercode"))
				Else
					If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						lblnPost = True
					Else
						
						'lblnpost = false                    
						
						'response.Write "<NOTSCRIPT>alert('" & .QueryString ("sCodispl") & "');</" & "Script>" 
						'response.Write "<NOTSCRIPT>alert('" & .QueryString ("nMainAction") & "');</" & "Script>" 
						'response.Write "<NOTSCRIPT>alert('" & mobjValues.StringToType(Session("nErrorNum"),eFunctions.Values.eTypeData.etdLong) & "');</" & "Script>" 
						'response.Write "<NOTSCRIPT>alert('" & Ucase(.Form("tctCodisp")) & "');</" & "Script>" 
						'response.Write "<NOTSCRIPT>alert('" & mobjValues.StringToType(.Form("cbePriority"),eFunctions.Values.eTypeData.etdLong) & "');</" & "Script>" 
						'response.Write "<NOTSCRIPT>alert('" & mobjValues.StringToType(.Form("cbeErrorType"),eFunctions.Values.eTypeData.etdLong) & "');</" & "Script>" 
						'response.Write "<NOTSCRIPT>alert('" & mobjValues.StringToType(.Form("cbeSource"),eFunctions.Values.eTypeData.etdLong) & "');</" & "Script>"                                                                                               
						'response.Write "<NOTSCRIPT>alert('" & .Form("tctShortDesc") & "');</" & "Script>" 
						'response.Write "<NOTSCRIPT>alert('" & mobjValues.StringToType(.Form("cbeStaterr"),eFunctions.Values.eTypeData.etdLong) & "');</" & "Script>" 
						'response.Write "<NOTSCRIPT>alert('" & .Form("tUserDetect") & "');</" & "Script>" 
						'response.Write "<NOTSCRIPT>alert('" & .Form("tctVersion") & "');</" & "Script>" 
						'response.Write "<NOTSCRIPT>alert('" & .Form("tdateDetect") & "');</" & "Script>" 
						'response.Write "<NOTSCRIPT>alert('" & .Form("tHourDetect") & "');</" & "Script>" 
						'response.Write "<NOTSCRIPT>alert('" & .Form("cbeSeverity") & "');</" & "Script>" 
						'response.Write "<NOTSCRIPT>alert('" & mobjValues.StringToType(.Form("hddModuleError"),eFunctions.Values.eTypeData.etdLong) & "');</" & "Script>" 
						
						lblnPost = mobjError.insPostER001(.QueryString("sCodispl"), .QueryString("nMainAction"), mobjValues.StringToType(Session("nErrorNum"), eFunctions.Values.eTypeData.etdLong), UCase(.Form.Item("tctCodisp")), mobjValues.StringToType(.Form.Item("cbePriority"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeErrorType"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctShortDesc"), .Form.Item("txtDescript"), mobjValues.StringToType(.Form.Item("cbeStaterr"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tUserDetect"), .Form.Item("tctVersion"), .Form.Item("tdateDetect"), .Form.Item("tHourDetect"), .Form.Item("cbeSeverity"), mobjValues.StringToType(.Form.Item("hddModuleError"), eFunctions.Values.eTypeData.etdLong))
                            If CDbl(.QueryString.Item("nMainAction")) = 301 And lblnPost Then
                                
                                If mobjError.Find(mobjValues.StringToType(Session("nErrorNum"), eFunctions.Values.eTypeData.etdLong)) Then
                                    Response.Write("<SCRIPT>alert('" & "El error " & Session("nErrorNum") & " se grabó correctamente" & "')</" & "Script>")
                                Else
                                    lblnPost = mobjError.insPostER001(.QueryString("sCodispl"), .QueryString("nMainAction"), mobjValues.StringToType(Session("nErrorNum"), eFunctions.Values.eTypeData.etdLong), UCase(.Form.Item("tctCodisp")), mobjValues.StringToType(.Form.Item("cbePriority"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeErrorType"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctShortDesc"), .Form.Item("txtDescript"), mobjValues.StringToType(.Form.Item("cbeStaterr"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tUserDetect"), .Form.Item("tctVersion"), .Form.Item("tdateDetect"), .Form.Item("tHourDetect"), .Form.Item("cbeSeverity"), mobjValues.StringToType(.Form.Item("hddModuleError"), eFunctions.Values.eTypeData.etdLong))
                                    If mobjError.Find(mobjValues.StringToType(Session("nErrorNum"), eFunctions.Values.eTypeData.etdLong)) Then
                                        Response.Write("<SCRIPT>alert('" & "El error " & Session("nErrorNum") & " se grabó correctamente" & "')</" & "Script>")
                                    Else
                                        Response.Write("<SCRIPT>alert('" & "El error:" & Session("nErrorNum") & " tiene problemas para grabar " & "')</" & "Script>")
                                    End If
                                End If
                            End If
                        End If
				End If
			End With
			
			'+ Consulta de Errores            
		Case "ER002"
			lblnPost = True
			
			'+ Actuaqlizaciión de Estado de los Errores
		Case "ER003"
			lblnPost = True
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nErrorNum") = .Form.Item("tcnErrorNum")
				Else
					lblnPost = mobjError.insPostER003(.QueryString("sCodispl"), Session("nErrorNum"), .Form.Item("tctUserDetect"), mobjValues.StringToType(.Form.Item("cbeErrorType"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeStaterr"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctHourDetect"), mobjValues.StringToType(.Form.Item("tcnDays"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctHours"), .Form.Item("txtDescript"))
					Session("nErrorNum") = vbNullString
				End If
			End With
			
			'+ Historia de un Error
		Case "ER004"
			lblnPost = True
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nErrorNum") = .Form.Item("tcnErrorNum")
				End If
			End With
			
			'+Componentes por error
		Case "ER005"
			With Request
				lblnPost = mobjError.InsPostER005(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnError"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnId"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbeCompType"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tctName"), .Form.Item("tctPath"), mobjValues.StringToType(.Form.Item("tcnVersion"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcdToQC"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdToQA"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUSercode"), eFunctions.Values.eTypeData.etdLong, True))
				
			End With
			
			'+ Reporte de Errores
		Case "ERL001"
			lblnPost = True
			insPrintDocuments("ERL001")
			
			'+ Asignación de errores por transacción
		Case "ER006"
			lblnPost = mobjError.InsPostER006(Request.Form.Item("valCodisp"), Request.Form.Item("tctUserAssign"))
			
			'+ Actualización masiva estado de errores
		Case "ER007"
			With Request
				If Request.QueryString.Item("WindowType") <> "PopUp" Then
					If mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdLong) = 390 Then
						lblnPost = mobjError.insPostER007_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdLong), UCase(.Form.Item("tctCodisp")), .Form.Item("cbeStaterr"), mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbePriority"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeSeverity"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("hddModuleError"), eFunctions.Values.eTypeData.etdLong), Session("SessionId"), Session("nUsercode"))
					Else
						If mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdLong) = 392 Then
							lblnPost = mobjError.insPostER007_Upd(Session("SessionId"), Session("nUsercode"))
						End If
					End If
				Else
					lblnPost = mobjError.insPostER007(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcnErrorNum"), eFunctions.Values.eTypeData.etdLong), .Form.Item("cbeStaterr_new"), .Form.Item("tctUser"), Session("SessionId"), Session("nUsercode"))
				End If
			End With
			
	End Select
	insPostError = lblnPost
	'^^Begin Trace Block 08/09/2005 05:40:40 p.m.
        'Call insCommonFunction("valerrors", Request.QueryString.Item("sCodispl"), eEndPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
End Function

'----------------------------------------------------------------------------------------------------------------------------------------
Private Sub insPrintDocuments(ByRef sCodispl As Object)
	'----------------------------------------------------------------------------------------------------------------------------------------
	Dim lobjDocuments As eReports.Report
	Dim lintPercentPos As Object
	Dim lstrCodisp As String
	Dim lintSize As Integer
	Dim lstrMessage As String
	
	lobjDocuments = New eReports.Report

	Select Case sCodispl
		Case "ERL001"
			With lobjDocuments
				.sCodispl = "ERL001"
				.ReportFilename = "ErrorsReport.rpt"
				
				'.setParamField(1, "Title", "Reportes de errores")
				'.setParamField(2, "Transaction", "Transaction")
				'.setParamField(3, "Time", .settime(CStr(Now)))
				
                    'If mobjValues.StringToType(Request.Form.Item("cbeStaterr"), eFunctions.Values.eTypeData.etdLong) > 0 Then
                    '	lstrMessage = mobjValues.getMessage(mobjValues.StringToType(Request.Form.Item("cbeStaterr"), eFunctions.Values.eTypeData.etdLong), "Table999")
                    '	.setParamField(4, "State", lstrMessage)
                    'Else
                    '	.setParamField(4, "State", "Todos")
                    'End If
				
                    'If mobjValues.StringToType(Request.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdLong) > 0 Then
                    '	lstrMessage = mobjValues.getMessage(mobjValues.StringToType(Request.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdLong), "Table531")
                    '	.setParamField(5, "Source", lstrMessage)
                    'Else
                    '	.setParamField(5, "Source", "Todos")
                    'End If
				
                    'If mobjValues.StringToType(Request.Form.Item("cbeErrorType"), eFunctions.Values.eTypeData.etdLong) > 0 Then
                    '	lstrMessage = mobjValues.getMessage(mobjValues.StringToType(Request.Form.Item("cbeErrorType"), eFunctions.Values.eTypeData.etdLong), "Tab_typerr")
                    '	.setParamField(6, "Type", lstrMessage)
                    'Else
                    '	.setParamField(6, "Type", "Todos")
                    'End If
				
                    'If mobjValues.StringToType(Request.Form.Item("cbePriority"), eFunctions.Values.eTypeData.etdLong) > 0 Then
                    '	lstrMessage = mobjValues.getMessage(mobjValues.StringToType(Request.Form.Item("cbePriority"), eFunctions.Values.eTypeData.etdLong), "Table1006")
                    '	.setParamField(7, "Priority", lstrMessage)
                    'Else
                    '	.setParamField(7, "Priority", "Todas")
                    'End If
				
				'+ Si el caracter '*' aparece en la cadena "Codisp" se elimina
				'+ para colocar en su lugar el caracter '%'
				lintSize = Len(CStr(Request.Form.Item("tctCodisp")))
				If InStr(1, Request.Form.Item("tctCodisp"), "*") > 0 Then
					lstrCodisp = Mid(Request.Form.Item("tctCodisp"), 1, lintSize - 1)
					lstrCodisp = lstrCodisp & "%"
				Else
					lstrCodisp = Request.Form.Item("tctCodisp")
				End If
				
				'Envio al SP del .rpt de las variables de entrada para mostrar
				'los resultados requeridos
				.setStorProcParam(1, lstrCodisp)
                    If mobjValues.StringToType(Request.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdLong) <> eRemoteDB.Constants.intNull Then
                        .setStorProcParam(2, Request.Form.Item("cbeSource"))
                    Else
                        .setStorProcParam(2, 0)
                    End If
                    If mobjValues.StringToType(Request.Form.Item("cbePriority"), eFunctions.Values.eTypeData.etdLong) <> eRemoteDB.Constants.intNull Then
                        .setStorProcParam(3, Request.Form.Item("cbePriority"))
                    Else
                        .setStorProcParam(3, 0)
                    End If
                if mobjValues.StringToType(Request.Form.Item("tcnErrorNum"), eFunctions.Values.eTypeData.etdLong) <> eRemoteDB.Constants.intNull then
				    .setStorProcParam(4, Request.Form.Item("tcnErrorNum"))
			    Else
				    .setStorProcParam(4, 0)
				End if
				.setStorProcParam(5, Request.Form.Item("cbeStaterr"))
				.setStorProcParam(6, Request.Form.Item("tctUserRegister"))
				.setStorProcParam(7, Request.Form.Item("tctUserPending"))
			    .setStorProcParam(8, Request.Form.Item("tctUserClear"))
				.setStorProcParam(9, Request.Form.Item("tctUserNew"))
				.setStorProcParam(10, Request.Form.Item("tctUserDetect"))
				.setStorProcParam(11, Request.Form.Item("tctUserAssig"))
				.setStorProcParam(12, Request.Form.Item("tctUserCorrec"))
				.setStorProcParam(13, Request.Form.Item("tctUserConfir"))
				.setStorProcParam(14, Request.Form.Item("tctUserNoAcept"))
				.setStorProcParam(15, Request.Form.Item("tctUserAcept"))
				.setStorProcParam(16, .setdate(Request.Form.Item("tcddateRegister")))
				.setStorProcParam(17, .setdate(Request.Form.Item("tcddatePending")))		
				.setStorProcParam(18, .setdate(Request.Form.Item("tcddateClear")))
				.setStorProcParam(19, .setdate(Request.Form.Item("tcddateNew")))
				.setStorProcParam(20, .setdate(Request.Form.Item("tcddateDetect")))
				.setStorProcParam(21, .setdate(Request.Form.Item("tcddateAssig")))
				.setStorProcParam(22, .setdate(Request.Form.Item("tcddateAcept")))
				.setStorProcParam(23, .setdate(Request.Form.Item("tcddateCorrec")))
				.setStorProcParam(24, .setdate(Request.Form.Item("tcddateConfir")))
				.setStorProcParam(25, .setdate(Request.Form.Item("tcddateNoAcept")))
				.setStorProcParam(26, Request.Form.Item("cbeErrorType"))
				.setStorProcParam(27, Request.Form.Item("chkTransfer"))
			End With
			lobjDocuments.bErrModule = True
			Session("bErrorModule") = "1"
			Response.Write(lobjDocuments.Command)
	End Select
	lobjDocuments = Nothing
End Sub

</script>
<%
Response.Expires = -1441
mblnShowCompon = False

%>
<HTML>
<HEAD>
    <LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
    <META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">

</HEAD>
<BODY>
<SCRIPT src="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
    //+ Variable para el control de versiones
    document.VssVersion = "$$Revision: 6 $|$$Date: 22/09/04 11:07a $|$$Author: Fbonilla $"
</SCRIPT>
<%
mstrCommand = "&sModule=Errors&sProject=Errors&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values
mobjError = New eErrors.ErrorTyp

Session("sCodispl") = Request.QueryString.Item("sCodispl")

'+ Si no se han validado los campos de la página
'+ o si ya fueron validados previamente
If Request.Form.Item("sCodisplReload") = vbNullString And Request.QueryString.Item("IsValid") <> "1" Then
	mstrErrors = insvalError
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                                                              .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & """, ""ErrError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write("</SCRIPT>")
	End With
Else
	
	'+Se realiza llamada a página de componentes cuando el estado asignado es 3-Corregido
	'+y no se ha validado previamente
	mblnShowCompon = mobjValues.StringToType(Request.Form.Item("cbeStaterr"), eFunctions.Values.eTypeData.etdLong) = 3 And Request.QueryString.Item("sCodispl") = "ER003" And Request.QueryString.Item("IsValid") <> "1"
        mblnShowCompon = False
	If mblnShowCompon Then
		Response.Write("<SCRIPT>" & vbCrLf)
		'+Cuando se abre la ventana de error por una advertencia, se cierra 
		'+Luego, para que la ventana de componentes ER005 quede asociada al fraGeneric, 
		'+se debe especificar la ruta antes de abrirla
		'+Esto ultimo se hace porque ER005 al cancelar el proceso, actualiza el estado de los 
		'+botones de fraHeader usando opener.top.fraHeader...etc. 
		'+Sino se asignara la ruta antes de abrirla, la ventana ER005 queda asociada a la de errores
		'+por lo que opener.top.fraHeader no existe y da error
		If Request.Form.Item("sCodisplReload") <> vbNullString Then
			Response.Write("  top.close();" & vbCrLf)
			Response.Write("  top.opener.ShowPopUp('ER005_K.aspx?nError=" & Session("nErrorNum") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "', 'Componentes','720','400','no','','60','80');" & vbCrLf)
		Else
			Response.Write("  ShowPopUp('ER005_K.aspx?nError=" & Session("nErrorNum") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "', 'Componentes','720','400','no','','60','80');" & vbCrLf)
		End If
		Response.Write("</SCRIPT>" & vbCrLf)
	ElseIf insPostError() Then 
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.QueryString.Item("sCodispl") = "ER002" Then
					Session("sCallForm") = vbNullString
					Session("nErrorNum") = vbNullString
					Session("Query") = False
				End If
				
				If CStr(Session("sCallForm")) <> vbNullString Then
					If Request.QueryString.Item("sCodispl") <> "ER001" And Request.QueryString.Item("sCodispl") <> "ER003" Then
						Session("nErrorNum") = vbNullString
						Session("Query") = False
					Else
						Response.Write("<SCRIPT>top.close();</SCRIPT>")
					End If
				End If
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.fraHeader.insReloadTop(true,false);</SCRIPT>")
				End If
			Else
				If Request.QueryString.Item("sCodispl") = "ER001" Then
					Response.Write("<SCRIPT>;self.history.go(-1);top.document.frames['fraFolder'].location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				Else
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						
						If Request.QueryString.Item("sCodispl") = "ERL001" Then
							Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
						Else
							Response.Write("<SCRIPT>;self.history.go(-1);top.document.frames['fraFolder'].location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodisp=" & Request.Form.Item("tctCodisp") & "&nStaterr=" & Request.Form.Item("cbeStaterr") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
						End If
					Else
						Response.Write("<SCRIPT>window.close();opener.top.document.frames['fraFolder'].location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodisp=" & Request.Form.Item("tctCodisp") & "&nStaterr=" & Request.Form.Item("cbeStaterr") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
					End If
				End If
			End If
			
		Else
			Select Case Request.QueryString.Item("sCodispl")
				Case "ER005"
					Response.Write("<SCRIPT>top.opener.document.location.href='ER005_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nError=" & Request.Form.Item("tcnError") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'" & vbCrLf)
					Response.Write("top.close();</SCRIPT>")
				Case Else
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
			End Select
		End If
	End If
End If


mobjValues = Nothing
mobjError = Nothing
%>
</BODY>
</HTML>




