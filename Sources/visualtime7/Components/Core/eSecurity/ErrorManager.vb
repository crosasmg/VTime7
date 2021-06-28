Option Strict Off
Option Explicit On
Module ErrorManager
	'%Objetivo: .
	
	'-Objetivo: .
	Public ObjectRelease As Object
	
	'-Objetivo: API declarations
	Private Declare Function GetComputerNameAPI Lib "kernel32"  Alias "GetComputerNameA"(ByVal lpBuffer As String, ByRef nSize As Integer) As Integer
	
	'-Objetivo: .
	Public Enum ENUM_ERROR_ACTION
		EA_RERAISE = 1 'Reraise error
		EA_ADVANCED = 2 'Build Error report
		EA_ROLLBACK = &H10 'Call Connection.Rollback
		EA_WEBINFO = &H20 'Add web request information
		EA_CONN_CLOSE = &H40 'Close connection
		EA_DEFAULT = ENUM_ERROR_ACTION.EA_ADVANCED + ENUM_ERROR_ACTION.EA_RERAISE
		EA_NORERAISE = ENUM_ERROR_ACTION.EA_ADVANCED
	End Enum
	
	'-Objetivo: .
	Private Const ERR_PROPAGATION As Decimal = vbObjectError + 4096
	
	'-Objetivo: .
	Private Const SRC_PROCERROR As String = "ErrorManager.ProcError"
	
	'-Objetivo: .
	Private Const TEMPLATE_REPORT As String = "%Descr% %nl%" & "  Date='%Date%' Time='%Time%' Application='%App%' Version='%Ver%' Computer='%Comp%' %nl%" & "  Method: %MethodName% %nl%" & "  Number: %ErrNum% %nl%" & "  Source: %Source% %nl%" & "  Description: %Descr%%nl%"
	
	'-Objetivo: .
	Private mstrErrorReport As String
	
	'%Objetivo: .
	'%Parámetros:
	'%    sMethodHeader - .
	'%    aArrArgs      - .
	'%    nErrorAction  - .
	'%    oObjectCtrl   - .
	Public Sub ProcError(ByVal sMethodHeader As String, Optional ByVal aArrArgs As Object = Nothing, Optional ByVal nErrorAction As ENUM_ERROR_ACTION = ENUM_ERROR_ACTION.EA_DEFAULT, Optional ByRef oObjectCtrl As Object = Nothing, Optional ByVal sMessage As String = "", Optional ByVal sMessageExtra As String = "")
		Dim strMessage As String
        Dim strMethodName As String = String.Empty
        Dim strArgNames As String = ""
        Dim lngErrorLine As Integer
		Dim strErrorDesc As String
		Dim lngErrorNum As Integer
		Dim strErrorSrc As String
		Dim blnFirstErr As Boolean
		
		lngErrorLine = Erl()
		lngErrorNum = Err.Number
		strErrorDesc = Err.Description
		strErrorSrc = Err.Source
		
		ParseMethodHeader(sMethodHeader, strMethodName, strArgNames)
		
		If lngErrorNum <> ERR_PROPAGATION Then
			mstrErrorReport = String.Empty
			strMessage = Replace(TEMPLATE_REPORT, "%Descr%", strErrorDesc)
			strMessage = Replace(strMessage, "%nl%", vbCrLf)
			strMessage = Replace(strMessage, "%Date%", CStr(Today))
			strMessage = Replace(strMessage, "%Time%", CStr(TimeOfDay))
			'UPGRADE_WARNING: App property App.EXEName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			strMessage = Replace(strMessage, "%App%", My.Application.Info.AssemblyName)
			strMessage = Replace(strMessage, "%Ver%", My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision)
			strMessage = Replace(strMessage, "%Comp%", ErrGetComputerName)
			strMessage = Replace(strMessage, "%MethodName%", strMethodName)
			strMessage = Replace(strMessage, "%ErrNum%", CStr(lngErrorNum))
			strMessage = Replace(strMessage, "%Source%", strErrorSrc)
			strErrorDesc = strMessage
			blnFirstErr = True
			
			If oObjectCtrl Is Nothing Then
				oObjectCtrl = ObjectRelease
				'UPGRADE_NOTE: Object ObjectRelease may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				ObjectRelease = Nothing
			End If
			
			If Not oObjectCtrl Is Nothing Then
				'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				Select Case TypeName(oObjectCtrl)
					Case "DOMDocument"
						strErrorDesc = strErrorDesc & DOMDocumentExtract(oObjectCtrl)
					Case "IOraSession", "IOraDatabase"
						strErrorDesc = strErrorDesc & IOraDatabaseExtract(oObjectCtrl, sMessage)
					Case "Connection"
						strErrorDesc = strErrorDesc & ADOConnectionExtract(oObjectCtrl, sMessage)
						'UPGRADE_NOTE: Object oObjectCtrl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						oObjectCtrl = Nothing
				End Select
				'UPGRADE_NOTE: Object oObjectCtrl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				oObjectCtrl = Nothing
			End If
		End If
		strMessage = strErrorDesc & sMessageExtra & AddCallStackInfo(blnFirstErr, strMethodName, strArgNames, aArrArgs, lngErrorLine)
		
		If lngErrorNum <> ERR_PROPAGATION Then
#If ASP_ERR_EXCEPTION Then
			'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression ASP_ERR_EXCEPTION did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
			Call ASPException(strMessage, lngErrorNum)
#End If
		End If
		
		mstrErrorReport = strMessage
		If (nErrorAction And ENUM_ERROR_ACTION.EA_RERAISE) <> 0 Then
			Err.Raise(ERR_PROPAGATION, SRC_PROCERROR, strMessage)
		Else
#If HANDLEERR Then
			'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression HANDLEERR did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
			Call HandleError
#End If
		End If
	End Sub
	
	'%Objetivo: .
	Public ReadOnly Property ErrReport() As String
		Get
			ErrReport = mstrErrorReport
		End Get
	End Property
	
#If HANDLEERR Then
	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression HANDLEERR did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
	
	'%Objetivo: .
	Public Sub HandleError()
	Err.Clear
	
	frmShowError.txtDetail.Text = mstrErrorReport
	On Error Resume Next
	frmShowError.Show vbModal
	If Err.Number <> 0 Then
	On Error GoTo 0
	frmShowError.Show
	Else
	On Error GoTo 0
	End If
	End Sub
	
#End If
	
	'%Objetivo: .
    Private Function ErrGetComputerName() As String
        ErrGetComputerName = System.Net.Dns.GetHostName
    End Function
	
	'%Objetivo: .
	'%Parámetros:
	'%    sMethodHeader - .
	'%    sMethodName - .
	'%    sArgNames - .
	Private Sub ParseMethodHeader(ByVal sMethodHeader As String, ByRef sMethodName As String, ByRef sArgNames As String)
		Dim arrBuffer() As String
		
		On Error GoTo ParseMethodHeader_Err
		arrBuffer = Microsoft.VisualBasic.Split(sMethodHeader, "(")
		If UBound(arrBuffer) >= 0 Then
			sMethodName = arrBuffer(0)
		Else
			sMethodName = String.Empty
		End If
		If UBound(arrBuffer) <= 0 Then
			sArgNames = String.Empty
		Else
			sArgNames = Left(arrBuffer(1), Len(arrBuffer(1)) - 1)
		End If
		Exit Sub
ParseMethodHeader_Err: 
	End Sub
	
	'%Objetivo: .
	'%Parámetros:
	'%    bFirstError - .
	'%    sMethodName - .
	'%    sArgNames   - .
	'%    aArrArgs    - .
	'%    nErrorLine  - .
	Private Function AddCallStackInfo(ByVal bFirstError As Boolean, ByVal sMethodName As String, ByVal sArgNames As String, ByVal aArrArgs As Object, ByVal nErrorLine As Integer) As String
        Dim strLine As String = ""
        Dim strResultado As String = ""

        Try
            If bFirstError Then
                AddCallStackInfo = "  Call Stack:" & vbCrLf
            End If

            If nErrorLine = 0 Then
                strLine = String.Empty
            Else
                strLine = "  at Line " & nErrorLine
            End If
            'UPGRADE_WARNING: App property App.EXEName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            strLine = strLine & "    " & My.Application.Info.AssemblyName & "." & sMethodName & "(" & CreateNameValueList(sArgNames, aArrArgs) & ")" & strLine & vbCrLf
            Return strResultado
        Catch ex As Exception
            Return strResultado
        End Try
    End Function
	
	'%Objetivo: .
	'%Parámetros:
	'%    sNames  - .
	'%    aValues - .
	Private Function CreateNameValueList(ByVal sNames As String, ByVal aValues As Object) As String
		Dim arrNames() As String
		Dim intIndex As Integer
        Dim strBuffer As String = ""

        On Error GoTo CreateNameValueList_Err
		
		arrNames = Microsoft.VisualBasic.Split(sNames, ",")
		For intIndex = 0 To UBound(aValues)
			If strBuffer > String.Empty Then
				strBuffer = strBuffer & ","
			End If
			strBuffer = strBuffer & arrNames(intIndex) & "=" & ErrVarToString(aValues(intIndex))
		Next intIndex
		CreateNameValueList = strBuffer
		Exit Function
CreateNameValueList_Err: 
	End Function
	
	'%Objetivo: .
	'%Parámetros:
	'%    vValue - .
	Private Function ErrVarToString(ByVal vValue As Object) As String
		If IsArray(vValue) Then
			ErrVarToString = "{Array}"
		Else
			'UPGRADE_WARNING: VarType has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			Select Case VarType(vValue)
				Case VariantType.Short, VariantType.Integer, VariantType.Byte, VariantType.Single, VariantType.Double, VariantType.Decimal, VariantType.Boolean, VariantType.Decimal
					ErrVarToString = CStr(vValue)
				Case VariantType.Date
					ErrVarToString = "'" & CStr(vValue) & "'"
				Case VariantType.Error
					ErrVarToString = "" 'Missing arg falls here
				Case VariantType.Empty
					ErrVarToString = "{Empty}"
				Case VariantType.Null
					ErrVarToString = "{Null}"
				Case VariantType.String
					
					vValue = Replace(vValue, vbNewLine, String.Empty)
					vValue = Replace(vValue, vbTab, " ")
					
					If Len(vValue) > 60 Then
						vValue = Left(vValue, 60) & "..."
					End If
					ErrVarToString = "'" & vValue & "'"
				Case VariantType.Object
					'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					ErrVarToString = "{" & TypeName(vValue) & "}" 'Value of Nothing will be shown as "Nothing"
				Case Else
					ErrVarToString = "{?}"
			End Select
		End If
	End Function
	
	'%Objetivo: .
	'%Parámetros:
	'%    oObjectCtrl - .
	Private Function DOMDocumentExtract(ByRef oObjectCtrl As Object) As String
        Dim strPointer As String = ""
        Dim varAux As String = ""
        With oObjectCtrl.parseError
            If .errorCode <> 0 Then
                If .linepos > 0 Then
                    strPointer = Space(.linepos - 1) & "^" & vbNewLine
                End If
                varAux = "  Microsoft XML Parser Info:" & vbNewLine & "      Description: " & Replace(.reason, vbNewLine, String.Empty) & vbNewLine & "      Source Line: " & Replace(.srcText, vbTab, " ") & vbNewLine & "      -----------: " & strPointer & "      Line:" & .Line & " Pos:" & .linepos & vbNewLine

            End If
        End With
        Return varAux
    End Function
	
	'%Objetivo: .
	'%Parámetros:
	'%    oObjectCtrl - .
	'%    sMessage    - .
	Private Function IOraDatabaseExtract(ByRef oObjectCtrl As Object, ByVal sMessage As String) As String
		IOraDatabaseExtract = "  Oracle Object For OLE Info:" & vbNewLine
		
		'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If TypeName(oObjectCtrl) = "IOraSession" Then
			IOraDatabaseExtract = IOraDatabaseExtract & "      IOraDatabase Version: " & oObjectCtrl.OipVersionNumber & vbNewLine
		Else
			IOraDatabaseExtract = IOraDatabaseExtract & "      IOraDatabase Version: " & oObjectCtrl.Session.OipVersionNumber & vbNewLine
		End If
		
		IOraDatabaseExtract = IOraDatabaseExtract & "               Description: " & Replace(oObjectCtrl.LastServerErrText, vbNewLine, "") & vbNewLine
		If sMessage > String.Empty Then
			IOraDatabaseExtract = IOraDatabaseExtract & sMessage & vbNewLine
		End If
		IOraDatabaseExtract = IOraDatabaseExtract & "               Source Line: " & CStr(oObjectCtrl.LastServerErr) & vbNewLine
		
		oObjectCtrl.LastServerErrReset()
		'UPGRADE_NOTE: Object oObjectCtrl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		oObjectCtrl = Nothing
	End Function
	
	'%Objetivo: .
	'%Parámetros:
	'%    oObjectCtrl - .
	'%    sMessage    - .
	Public Function ADOConnectionExtract(ByRef oObjectCtrl As Object, ByVal sMessage As String) As String
		Dim objError As Object
		Dim blnFirst As Boolean
		Dim strErrorMsg As String
		Dim strConn1 As String
		Dim strConn2 As String
		Dim strConn3 As String
		Dim strConn4 As String
		
		strConn1 = Left(oObjectCtrl.ConnectionString, InStr(1, oObjectCtrl.ConnectionString, "Password") + 8)
		strConn2 = Right(oObjectCtrl.ConnectionString, Len(oObjectCtrl.ConnectionString) - Len(strConn1))
		strConn3 = Right(strConn2, Len(strConn2) - InStr(1, strConn2, ";"))
		strConn4 = IIf(strConn2 <> strConn3, strConn1 & "*******;" & strConn3 & "'", strConn1 & "*******;")
		
		'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		strErrorMsg = "  Microsoft ADO Info:" & vbCrLf & "    ADO Version:   " & oObjectCtrl.version & vbCrLf & "    ADO Object :   " & TypeName(oObjectCtrl) & vbCrLf & "    Conn. String: '" & strConn4 & vbCrLf & "    Conn. State:   " & ConnStateAsString(oObjectCtrl.State) & vbCrLf
		If sMessage > String.Empty Then
			strErrorMsg = strErrorMsg & "    " & Replace(sMessage, vbCrLf, "    " & vbCrLf) & vbCrLf
		End If
		If oObjectCtrl.Errors.Count > 0 Then
			blnFirst = True
			For	Each objError In oObjectCtrl.Errors
				With objError
					If blnFirst Then
						strErrorMsg = strErrorMsg & "      Error Number : " & .Number & IIf(.NativeError <> 0, " (NativeError='" & .NativeError & "')", String.Empty) & vbCrLf & "      Source       : " & .Source & vbCrLf
						If .SQLState <> String.Empty Then
							strErrorMsg = strErrorMsg & "      SQL State    : " & .SQLState & vbCrLf
						End If
						strErrorMsg = strErrorMsg & "      Description  : " & .Description & vbCrLf
						blnFirst = False
					Else
						strErrorMsg = strErrorMsg & "                     " & .NativeError & " - " & .Description & vbCrLf
					End If
				End With
			Next objError
		End If
		
		'Name = 48 Provider Friendly Name
		'Code = 27 Provider Name
		'Version = 29 provider Version
		'Data Source = 56 Data Source
		
		
		ADOConnectionExtract = strErrorMsg
	End Function
	
	
	Private Function ConnStateAsString(ByVal AState As Integer) As String
        Dim sState As String = ""
        On Error GoTo errHandler
		If AState = 0 Then
			sState = "adStateClosed"
		Else
			If FlagSet(AState, 1) Then sState = "adStateOpen"
			If FlagSet(AState, 2) Then sState = sState & " + adStateConnecting"
			If FlagSet(AState, 4) Then sState = sState & " + adStateExecuting"
			If FlagSet(AState, 8) Then sState = sState & " + adStateFetching"
		End If
		ConnStateAsString = sState
		Exit Function
errHandler: 
	End Function
	
	Private Function FlagSet(ByVal Value As Integer, ByVal Flag As Integer) As Boolean
		FlagSet = ((Value And Flag) <> 0)
	End Function
	
	
#If ASP_ERR_EXCEPTION Then
	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression ASP_ERR_EXCEPTION did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
	Private Sub ASPException(ByVal sMessage As String, ByVal nErrorNum As Long)
	Dim lngHandler         As Long
	Dim strFileName        As String
	Dim strID              As String
	Dim strAnchor          As String
	Dim clsConfig          As VisualTimeConfig
	Dim clsContext         As ObjectContext
	Dim clsSession         As Session
	Dim clsResponse        As Response
	Dim clsServer          As Server
	
	On Error Resume Next
	
	Set clsContext = Nothing()
	Set clsSession = clsContext("Session")
	
	strID = clsSession.SessionID & Format(Now, "yyyyMMddmmhhmmss")
	strAnchor = "<a NAME=""" & strID & """>" & strID & "</a><BR>" & "Usuario: " & clsSession("nUserCode")
	
	Set clsConfig = New VisualTimeConfig
	strFileName = clsConfig.LoadSetting("Log", "c:\Inetpub\wwwroot\VTimeNet\Log", "Paths") & "\Log" & Format(Date, "yyyyMMdd") & ".htm"
	Set clsConfig = Nothing
	
	lngHandler = FreeFile
	Open strFileName For Append As #lngHandler
	Print #lngHandler, "------------------------------------------------------------------------------------------<BR>"
	Print #lngHandler, strAnchor & vbCrLf & _
	                        "<xmp>" & "Hora: " & Time$ & vbCrLf & _
	                        "Database Error" & vbCrLf & _
	                        sMessage & vbCrLf & _
	                        "</xmp>"
	Close #lngHandler
	
	Set clsResponse = clsContext.Item("Response")
	Set clsServer = clsContext("Server")
	
	If clsSession("sOnErrorClear") <> "1" Then
	clsResponse.Clear
	End If
	
	'+ Se tuvo se sustitiut el redirect por código js para habilitar la opción recargar
	'+ The redirect was changed for JavaScript code to enable the option "retry"
	clsResponse.Write "<SCRIPT>self.document.location.href='/VTimeNet/Common/Exception.aspx?sErrURL=" & clsServer.URLEncode("/VTimeNet/Log/Log" & Format(Date, "yyyyMMdd") & ".htm#" & strID) & "&nErrorNum=" & strID & "&nErrorOracle=" & nErrorNum & "&sPage=' + self.document.location.href ;</SCRIPT>"
	clsResponse.End
	
	Set clsResponse = Nothing
	Set clsSession = Nothing
	Set clsServer = Nothing
	Set clsContext = Nothing
	End Sub
#End If
End Module






