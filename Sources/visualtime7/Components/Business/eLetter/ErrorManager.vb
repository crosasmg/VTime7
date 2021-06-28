Option Strict Off
Option Explicit On
Module ErrorManager
	
	'%Objetivo: .
	Private Const SRC_VERSION As String = "$Revision: 17 $$Date: 18/02/04 2:28p $"
	
	'%Objetivo: Win32s on Windows 3.1
	Public Const VER_PLATFORM_WIN32s As Short = 0
	
	'%Objetivo: Windows 95, Windows 98, or Windows Me
	Public Const VER_PLATFORM_WIN32_WINDOWS As Short = 1
	
	'%Objetivo: Windows NT, Windows 2000, Windows XP, or Windows Server 2003 family.
	Public Const VER_PLATFORM_WIN32_NT As Short = 2
	
	'-Objetivo: .
	Private Const ERR_PROPAGATION As Decimal = vbObjectError + 4096
	
	'-Objetivo: .
	Private Const SRC_PROCERROR As String = "ErrorManager.ProcError"
	
	'-Objetivo: .
	Private Const TEMPLATE_REPORT As String = "%Descr% %nl%" & "  Date='%Date%' Time='%Time%' Application='%App%' Version='%Ver%' Computer='%Comp%' SO='%SO%' %nl%" & "  Method: %MethodName% %nl%" & "  Number: %ErrNum% %nl%" & "  Source: %Source% %nl%" & "  Description: %Descr%%nl%"
	'-Objetivo: .
	Public bIntereactive As Boolean
	
	'-Objetivo: .
	Public bNotCancel As Boolean
	
	'-Objetivo: .
	Public bFinishWithError As Boolean
	
	'-Objetivo: .
	Public sLogFileName As String
	
	'-Objetivo: .
	Public ObjectRelease As Object
	
	'-Objetivo: .
	Private Declare Function GetComputerNameAPI Lib "kernel32"  Alias "GetComputerNameA"(ByVal lpBuffer As String, ByRef nSize As Integer) As Integer
	
	'-Objetivo: .
	Private Declare Function GetModuleFileName Lib "kernel32"  Alias "GetModuleFileNameA"(ByVal hModule As Integer, ByVal lpFileName As String, ByVal nSize As Integer) As Integer
	
	'-Objetivo: .
	'UPGRADE_WARNING: Structure OSVERSIONINFO may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
	Private Declare Function GetVersionExA Lib "kernel32" (ByRef lpVersionInformation As OSVERSIONINFO) As Short
	
	'-Objetivo: .
	Public Structure OSVERSIONINFO
		Dim dwOSVersionInfoSize As Integer
		Dim dwMajorVersion As Integer
		Dim dwMinorVersion As Integer
		Dim dwBuildNumber As Integer
		Dim dwPlatformId As Integer
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(128),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=128)> Public szCSDVersion() As Char
	End Structure
	
	'-Objetivo: .
	Public Enum ENUM_ERROR_ACTION
		EA_RERAISE = 1 'Reraise error
		EA_ADVANCED = 2 'Build Error report
		EA_ROLLBACK = &H10s 'Call Connection.Rollback
		EA_WEBINFO = &H20s 'Add web request information
		EA_CONN_CLOSE = &H40s 'Close connection
		EA_DEFAULT = ENUM_ERROR_ACTION.EA_ADVANCED + ENUM_ERROR_ACTION.EA_RERAISE
		EA_NORERAISE = ENUM_ERROR_ACTION.EA_ADVANCED
	End Enum
	
	
	'-Objetivo: .
	Private mstrErrorReport As String
	
	'%Objetivo: .
	'%Parámetros:
	'%    sMethodHeader - .
	'%    aArrArgs      - .
	'%    nErrorAction  - .
	Public Sub ProcError(ByRef sMethodHeader As String, Optional ByRef aArrArgs As Object = Nothing, Optional ByVal nErrorAction As ENUM_ERROR_ACTION = ENUM_ERROR_ACTION.EA_DEFAULT, Optional ByRef objObjectCtrl As Object = Nothing, Optional ByRef sMessage As String = "", Optional ByRef sMessageExtra As String = "", Optional ByRef bNoShowErrMessage As Boolean = False)
        Dim strMessage As String = String.Empty
        Dim strMethodName As String = String.Empty
        Dim strArgNames As String = String.Empty
        Dim lngErrorLine As Integer
        Dim strErrorDesc As String = String.Empty
        Dim lngErrorNum As Integer
        Dim strErrorSrc As String = String.Empty
        Dim blnFirstErr As Boolean = False
        Dim sVersion As String = String.Empty

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
			'UPGRADE_WARNING: App property App.EXEName has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			strMessage = Replace(strMessage, "%App%", My.Application.Info.AssemblyName)
			strMessage = Replace(strMessage, "%Ver%", My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision)
			strMessage = Replace(strMessage, "%Comp%", ErrGetComputerName)
			strMessage = Replace(strMessage, "%SO%", ErrSOVersion)
			strMessage = Replace(strMessage, "%MethodName%", strMethodName)
			strMessage = Replace(strMessage, "%ErrNum%", CStr(lngErrorNum))
			strMessage = Replace(strMessage, "%Source%", strErrorSrc)
			strErrorDesc = strMessage
			blnFirstErr = True
			
			If objObjectCtrl Is Nothing Then
				objObjectCtrl = ObjectRelease
				ObjectRelease = Nothing
			End If
			
			If Not objObjectCtrl Is Nothing Then
				'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				Select Case TypeName(objObjectCtrl)
					Case "DOMDocument"
						strErrorDesc = strErrorDesc & DOMDocumentExtract(objObjectCtrl)
					Case "IOraSession", "IOraDatabase"
						strErrorDesc = strErrorDesc & IOraDatabaseExtract(objObjectCtrl, sMessage)
					Case "Connection"
						strErrorDesc = strErrorDesc & ADOConnectionExtract(objObjectCtrl, sMessage)
				End Select
				objObjectCtrl = Nothing
			End If
		End If
		strMessage = strErrorDesc & sMessageExtra & AddCallStackInfo(blnFirstErr, strMethodName, strArgNames, aArrArgs, lngErrorLine, sVersion)
		
		'If lngErrorNum <> ERR_PROPAGATION Then
		'#If ASP_ERR_EXCEPTION Then
		If Not bNoShowErrMessage Then
			Call ASPException(strMessage, lngErrorNum)
		Else
			'nErrorNum = lngErrorNum
			'sErrorMsg = strMessage
			Err.Clear()
		End If
		'#End If
		'End If
		mstrErrorReport = strMessage
		
		If (nErrorAction And ENUM_ERROR_ACTION.EA_NORERAISE) = ENUM_ERROR_ACTION.EA_NORERAISE Then
#If TRACE Then
			'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression TRACE did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
			WriteLog(mstrErrorReport, "Trace.log")
#Else
			WriteLog(mstrErrorReport)
#End If
		End If
		
		If (nErrorAction And ENUM_ERROR_ACTION.EA_RERAISE) <> 0 And Not bNoShowErrMessage Then
			Err.Raise(ERR_PROPAGATION, SRC_PROCERROR, strMessage)
		End If
	End Sub
	
	'%Objetivo: .
	Public ReadOnly Property ErrNumber() As Integer
		Get
			Dim intIndex As Short
			
			intIndex = InStr(mstrErrorReport, "Number: ") + 8
			ErrNumber = CInt(Mid(mstrErrorReport, intIndex, InStr(intIndex, mstrErrorReport, vbCrLf) - intIndex))
		End Get
	End Property
	
	'%Objetivo: .
	Public ReadOnly Property ErrReport() As String
		Get
			ErrReport = mstrErrorReport
		End Get
	End Property
	
	'%Objetivo: .
	Public Sub HandleError(ByRef oForm As Object)
		Err.Clear()
		
		oForm.txtDetail.Text = mstrErrorReport
		On Error Resume Next
		oForm.Show(1) 'VB6.FormShowConstants.Modal
		If Err.Number <> 0 Then
			oForm.Show()
		Else
		End If
	End Sub
	
	'%Objetivo: .
	Private Function ErrSOVersion() As String
		Dim OSInfo As OSVERSIONINFO
		Dim retvalue As Short
		
        ErrSOVersion = String.Empty

        OSInfo.dwOSVersionInfoSize = 148
		OSInfo.szCSDVersion = Space(128)
		retvalue = GetVersionExA(OSInfo)
		
		With OSInfo
			Select Case .dwPlatformId
				Case VER_PLATFORM_WIN32s ' Win32s on Windows 3.1
					ErrSOVersion = "Windows 3.1"
					
				Case VER_PLATFORM_WIN32_WINDOWS ' Windows 95, Windows 98,
					Select Case .dwMinorVersion ' or Windows Me
						Case 0
							ErrSOVersion = "Windows 95"
						Case 10
							If (OSInfo.dwBuildNumber And &HFFFF) = 2222 Then
								ErrSOVersion = "Windows 98SE"
							Else
								ErrSOVersion = "Windows 98"
							End If
						Case 90
							ErrSOVersion = "Windows Me"
					End Select
					
				Case VER_PLATFORM_WIN32_NT ' Windows NT, Windows 2000, Windows XP,
					Select Case .dwMajorVersion ' or Windows Server 2003 family.
						Case 3
							ErrSOVersion = "Windows NT 3.51"
						Case 4
							ErrSOVersion = "Windows NT 4.0"
						Case 5
							Select Case .dwMinorVersion
								Case 0
									ErrSOVersion = "Windows 2000"
								Case 1
									ErrSOVersion = "Windows XP"
								Case 2
									ErrSOVersion = "Windows Server 2003"
							End Select
					End Select
					
				Case Else
					ErrSOVersion = "Failed"
					
			End Select
			ErrSOVersion = "Microsoft " & ErrSOVersion & " Build " & .dwMajorVersion & "." & .dwMinorVersion & "." & .dwBuildNumber
			If InStr(.szCSDVersion, Chr(0)) > 0 Then
				ErrSOVersion = ErrSOVersion & " " & Left(.szCSDVersion, InStr(.szCSDVersion, Chr(0)) - 1)
			End If
		End With
	End Function
	
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
	Private Function AddCallStackInfo(ByVal bFirstError As Boolean, ByRef sMethodName As String, ByRef sArgNames As String, ByRef aArrArgs As Object, ByVal nErrorLine As Integer, ByRef sVersion As String) As String
		Dim strLine As String
		Dim strRevision As String
		
        On Error Resume Next

        AddCallStackInfo = String.Empty

		strRevision = Trim(Mid(sVersion, 11, InStr(2, sVersion, "$") - 12))
		If Len(strRevision) > 0 Then
			strRevision = " Rev " & strRevision
		End If
		If bFirstError Then
			AddCallStackInfo = "  Call Stack:" & vbCrLf
		End If
		
		If nErrorLine = 0 Then
			strLine = String.Empty
		Else
			strLine = " at Line " & nErrorLine
		End If
		
		'UPGRADE_WARNING: App property App.EXEName has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		AddCallStackInfo = AddCallStackInfo & "    " & My.Application.Info.AssemblyName & "." & sMethodName & "(" & CreateNameValueList(sArgNames, aArrArgs) & ")" & strRevision & strLine & vbCrLf
		Exit Function
AddCallStackInfo_Err: 
	End Function
	
	'%Objetivo: .
	'%Parámetros:
	'%    sNames  - .
	'%    aValues - .
	Private Function CreateNameValueList(ByVal sNames As String, ByVal aValues As Object) As String
		Dim arrNames() As String
		Dim intIndex As Short
        Dim strBuffer As String = String.Empty
		
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
			'UPGRADE_WARNING: VarType has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
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
					'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
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
        Dim strPointer As String = String.Empty

        DOMDocumentExtract = String.Empty

		With oObjectCtrl.parseError
			If .errorCode <> 0 Then
				If .linepos > 0 Then
					strPointer = Space(.linepos - 1) & "^" & vbNewLine
				End If
				DOMDocumentExtract = "  Microsoft XML Parser Info:" & vbNewLine & "      Description: " & Replace(.reason, vbNewLine, String.Empty) & vbNewLine & "      Source Line: " & Replace(.srcText, vbTab, " ") & vbNewLine & "      -----------: " & strPointer & "      Line:" & .Line & " Pos:" & .linepos & vbNewLine
				
			End If
		End With
	End Function
	
	'%Objetivo: .
	'%Parámetros:
	'%    oObjectCtrl - .
	'%    sMessage    - .
	Private Function IOraDatabaseExtract(ByRef oObjectCtrl As Object, ByVal sMessage As String) As String
		IOraDatabaseExtract = "  Oracle Object For OLE Info:" & vbNewLine
		
		'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If TypeName(oObjectCtrl) = "IOraSession" Then
			IOraDatabaseExtract = IOraDatabaseExtract & "      IOraDatabase Version: " & oObjectCtrl.OipVersionNumber & vbNewLine
		Else
			IOraDatabaseExtract = IOraDatabaseExtract & "      IOraDatabase Version: " & oObjectCtrl.Session.OipVersionNumber & vbNewLine
		End If
		
		IOraDatabaseExtract = IOraDatabaseExtract & "               Description: " & Replace(oObjectCtrl.LastServerErrText, vbNewLine, String.Empty) & vbNewLine
		If sMessage > String.Empty Then
			IOraDatabaseExtract = IOraDatabaseExtract & sMessage & vbNewLine
		End If
		IOraDatabaseExtract = IOraDatabaseExtract & "               Source Line: " & CStr(oObjectCtrl.LastServerErr) & vbNewLine
		
		oObjectCtrl.LastServerErrReset()
		oObjectCtrl = Nothing
	End Function
	
	'%Objetivo: .
	'%Parámetros:
	'%    oObjectCtrl - .
	'%    sMessage    - .
	Private Function ADOConnectionExtract(ByRef oObjectCtrl As Object, ByVal sMessage As String) As String
		Dim objError As Object
		Dim blnFirst As Boolean
		Dim strErrorMsg As String
		Dim strConn1 As String
		Dim strConn2 As String
		Dim strConn3 As String
		Dim strConn4 As String
		Dim strNative As String
		
		strConn1 = Left(oObjectCtrl.ConnectionString, InStr(1, oObjectCtrl.ConnectionString, "Password") + 8)
		strConn2 = Right(oObjectCtrl.ConnectionString, Len(oObjectCtrl.ConnectionString) - Len(strConn1))
		strConn3 = Right(strConn2, Len(strConn2) - InStr(1, strConn2, ";"))
		If strConn2 <> strConn3 Then
			strConn4 = strConn1 & "*******;" & strConn3 & "'"
		Else
			strConn4 = strConn1 & "*******;"
		End If
		
		'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		strErrorMsg = "  Microsoft ADO Info:" & vbCrLf & "    ADO Version:   " & oObjectCtrl.Version & vbCrLf & "    ADO Object :   " & TypeName(oObjectCtrl) & vbCrLf & "    Conn. String: '" & strConn4 & vbCrLf & "    Conn. State:   " & ConnStateAsString(oObjectCtrl.State) & vbCrLf
		If sMessage > String.Empty Then
			strErrorMsg = strErrorMsg & "    " & Replace(sMessage, vbCrLf, "    " & vbCrLf) & vbCrLf
		End If
		If oObjectCtrl.Errors.Count > 0 Then
			blnFirst = True
			For	Each objError In oObjectCtrl.Errors
				With objError
					If blnFirst Then
						If .NativeError <> 0 Then
							strNative = " (NativeError='" & .NativeError & "')"
						Else
							strNative = String.Empty
						End If
						
						strErrorMsg = strErrorMsg & "      Error Number : " & .Number & strNative & vbCrLf & "      Source       : " & .Source & vbCrLf
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
	
	
	'%Objetivo: .
	'%Parámetros:
	'%    AState - .
	Private Function ConnStateAsString(ByVal AState As Integer) As String
        Dim sState As String = String.Empty
		If AState = 0 Then
			sState = "adStateClosed"
		Else
			If FlagSet(AState, 1) Then
				sState = "adStateOpen"
			End If
			If FlagSet(AState, 2) Then
				sState = sState & " + adStateConnecting"
			End If
			If FlagSet(AState, 4) Then
				sState = sState & " + adStateExecuting"
			End If
			If FlagSet(AState, 8) Then
				sState = sState & " + adStateFetching"
			End If
		End If
		ConnStateAsString = sState
		Exit Function
errHandler: 
	End Function
	
	'%Objetivo: .
	'%Parámetros:
	'%    Value - .
	'%    Flag  - .
	Private Function FlagSet(ByVal Value As Integer, ByVal Flag As Integer) As Boolean
		FlagSet = ((Value And Flag) <> 0)
	End Function
	
	
	'%Objetivo: .
	'%Parámetros:
	'%    sMessage  - .
	'%    nErrorNum - .
	Private Sub ASPException(ByVal sMessage As String, ByVal nErrorNum As Integer)
		Dim lngHandler As Integer
		Dim strFileName As String
		Dim strID As String
		Dim strAnchor As String
		Dim clsConfig As eRemoteDB.VisualTimeConfig
		Dim clsContext As Object
		Dim clsSession As Object
		Dim clsResponse As Object
		Dim clsServer As Object
		Dim strSessionID As String
		Dim strUsercode As String
		
		On Error Resume Next
		
#If ASP_ERR_EXCEPTION Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression ASP_ERR_EXCEPTION did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Set clsContext = Nothing()
		Set clsSession = clsContext("Session")
		strSessionID = clsSession.SessionID
		strUsercode = clsSession("nUserCode")
#Else
		strSessionID = "6329255"
		strUsercode = "6329"
#End If
		
		strID = strSessionID & Now.ToString("yyyyMMddmmhhmmss")
		strAnchor = "<a NAME=""" & strID & """>" & strID & "</a><BR>" & "Usuario: " & strUsercode
		
		clsConfig = New eRemoteDB.VisualTimeConfig
        strFileName = clsConfig.LoadSetting("Log", "c:\Inetpub\wwwroot\VTimeNet\Log", "Paths") & "\Log" & Today.ToString("yyyyMMdd") & ".htm"
        clsConfig = Nothing

        lngHandler = FreeFile()
        FileOpen(lngHandler, strFileName, OpenMode.Append)
        PrintLine(lngHandler, "------------------------------------------------------------------------------------------<BR>")
        PrintLine(lngHandler, strAnchor & vbCrLf & "<xmp>" & "Hour: " & TimeString & vbCrLf & "Database Error" & vbCrLf & sMessage & vbCrLf & "</xmp>")
        FileClose(lngHandler)

#If ASP_ERR_EXCEPTION Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression ASP_ERR_EXCEPTION did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Set clsResponse = clsContext.Item("Response")
		Set clsServer = clsContext("Server")

		If clsSession("sOnErrorClear") <> "1" Then
		clsResponse.Clear
		End If

		'+ Se tuvo se sustitiut el redirect por código js para habilitar la opción recargar
		'+ The redirect was changed for JavaScript code to enable the option "retry"
		clsResponse.Write "<SCRIPT>self.document.location.href='/VTimeNet/Common/Exception.aspx?sErrURL=" & clsServer.URLEncode("/VTimeNet/Log/Log" & Format$(Date, "yyyyMMdd") & ".htm#" & strID) & "&nErrorNum=" & strID & "&nErrorOracle=" & nErrorNum & "&sPage=' + self.document.location.href ;</SCRIPT>"
		clsResponse.End
#End If
		clsResponse = Nothing
		clsSession = Nothing
		clsServer = Nothing
		clsContext = Nothing
		
	End Sub
	
	
	'%Objetivo: .
	Public Function IsIDEMode() As Boolean
        IsIDEMode = True
        '      Dim strFileName As String
        'Dim lngCount As Integer

        'strFileName = New String(Chr(0), 255) '// Just space could be used, just To reserve memo...
        'lngCount = GetModuleFileName(VB6.GetHInstance.ToInt32, strFileName, 255) '// We have exe file, vb or app
        'strFileName = Left(strFileName, lngCount) '// Remove the empty
        ''//IMPORTANT: Here you just set VB5, or
        ''     VB6, just that... simple.
        'If UCase(Right(strFileName, 7)) <> "VB6.EXE" Then '// We only need To see if it is vb
        '	IsIDEMode = True '// No
        'Else
        '	IsIDEMode = True '// Yes
        'End If
        ''// If you need you can get the VB versi
        ''     on, that is just few lines more...
	End Function
	
#If ShowError Then
	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression ShowError did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
	
	'**%Objetive: .
	'%Objetivo: .
	Public Sub HandleErrorGeneral()
	Err.Clear
	
	frmShowError.txtDetail.Text = ErrReport
	On Error Resume Next
	frmShowError.Show vbModal
	If Err.Number <> 0 Then
	frmShowError.Show
	Else
	End If
	End Sub
	
#End If
	
	'%Objetivo: .
	'%Parámetros:
	'%    sBuffer   -
	'%    sFilename -
	'%    nLevel    -
	Public Sub WriteLog(ByRef sBuffer As String, Optional ByVal sFileName As String = "", Optional ByVal nLevel As Short = 0)
		Dim lngHan As Integer
		
		If Len(sFileName) = 0 Then
			sFileName = sLogFileName
		End If
		If Len(sFileName) > 0 Then
			lngHan = FreeFile
			FileOpen(lngHan, sFileName, OpenMode.Append)
			PrintLine(lngHan, "[" & Right("0" & Hour(TimeOfDay), 2) & ":" & Right("0" & Minute(TimeOfDay), 2) & ":" & Right("0" & Second(TimeOfDay), 2) & "] " & Space(nLevel) & sBuffer)
			FileClose(lngHan)
		End If
		
	End Sub
	
	'%Objetivo: .
	'%Parámetros:
	'%    sMethodHeader -
	'%    aArrArgs      -
	Public Function ShowCall(ByVal sMethodHeader As String, Optional ByVal aArrArgs As Object = Nothing) As String
        Dim strMethodName As String = String.Empty
        Dim strArgNames As String = String.Empty
		
		ParseMethodHeader(sMethodHeader, strMethodName, strArgNames)
		
		'UPGRADE_WARNING: App property App.EXEName has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		ShowCall = My.Application.Info.AssemblyName & "." & strMethodName & "(" & CreateNameValueList(strArgNames, aArrArgs) & ")"
	End Function
	
	'%Objetivo: .
	'%Parámetros:
	'%    sAction       -
	'%    sMethodHeader -
	'%    aArrArgs      -
	Public Sub TraceLog(ByRef sAction As String, ByRef sMethodHeader As String, Optional ByRef aArrArgs As Object = Nothing)
		If sAction = "Push" Or sAction = "Pop" Then
			WriteLog(sAction & " " & ShowCall(sMethodHeader, aArrArgs), "Trace.log")
		Else
			WriteLog(sAction & " " & sMethodHeader, "Trace.log")
		End If
	End Sub
End Module












