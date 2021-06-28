Option Strict Off
Option Explicit On

Imports System.Web
Imports System.Web.SessionState

Module ErrorManager
    '**+Objective: Error Manager
    '**+Version: $$Revision: 3 $
    '+Objetivo: Manejo de errores
    '+Version: $$Revision: 3 $

    '**-Objective:
    '-Objetivo:
    Public ObjectRelease As Object

    '**-Objective:
    '-Objetivo:
    Private Declare Function GetComputerNameAPI Lib "kernel32" Alias "GetComputerNameA" (ByVal lpBuffer As String, ByRef nSize As Integer) As Integer

    '**-Objective:
    '-Objetivo:
    Private Declare Function GetModuleFileName Lib "kernel32" Alias "GetModuleFileNameA" (ByVal hModule As Integer, ByVal lpFileName As String, ByVal nSize As Integer) As Integer

    '**-Objective:
    '-Objetivo:
    Public Enum ENUM_ERROR_ACTION
        EA_RERAISE = 1 'Reraise error
        EA_ADVANCED = 2 'Build Error report
        EA_ROLLBACK = &H10S 'Call Connection.Rollback
        EA_WEBINFO = &H20S 'Add web request information
        EA_CONN_CLOSE = &H40S 'Close connection
        EA_DEFAULT = ENUM_ERROR_ACTION.EA_ADVANCED + ENUM_ERROR_ACTION.EA_RERAISE
        EA_NORERAISE = ENUM_ERROR_ACTION.EA_ADVANCED
    End Enum

    '**-Objective:
    '-Objetivo:
    Private Const ERR_PROPAGATION As Decimal = vbObjectError + 4096

    '**-Objective:
    '-Objetivo:
    Private Const SRC_PROCERROR As String = "ErrorManager.ProcError"

    '**-Objective:
    '-Objetivo:
    Private Const TEMPLATE_REPORT As String = "%Descr% %nl%" & "  Date='%Date%' Time='%Time%' Application='%App%' Version='%Ver%' Computer='%Comp%' %nl%" & "  Method: %MethodName% %nl%" & "  Number: %ErrNum% %nl%" & "  Source: %Source% %nl%" & "  Description: %Descr%%nl%"

    '**-Objective:
    '-Objetivo:
    Private mstrErrorReport As String

    '**%Objetive: .
    '**%Parameters:
    '**%    sMethodHeader  - .
    '**%    aArrArgs       - .
    '**%    nErrorAction   - .
    '**%    oObjectCtrl    - .
    '**%    bNotDriveError - .
    '**%    nErrorNum      - .
    '**%    sErrorMsg      - .
    '%Objetivo: .
    '%Parámetros:
    '%    sMethodHeader  - .
    '%    aArrArgs       - .
    '%    nErrorAction   - .
    '%    oObjectCtrl    - .
    '%    bNotDriveError - .
    '%    nErrorNum      - .
    '%    sErrorMsg      - .
    Public Sub ProcError(ByVal sMethodHeader As String, Optional ByVal aArrArgs As Object = Nothing, Optional ByVal nErrorAction As ENUM_ERROR_ACTION = ENUM_ERROR_ACTION.EA_DEFAULT, Optional ByRef oObjectCtrl As Object = Nothing, Optional ByVal sMessage As String = "", Optional ByVal bNotDriveError As Object = False, Optional ByRef nErrorNum As Integer = 0, Optional ByRef sErrorMsg As String = "")
        Dim strMessage As String
        Dim strMethodName As String = String.Empty
        Dim strArgNames = String.Empty
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
                ObjectRelease = Nothing
            End If

            If Not oObjectCtrl Is Nothing Then
                Select Case TypeName(oObjectCtrl)
                    Case "DOMDocument"
                        strErrorDesc = strErrorDesc & DOMDocumentExtract(oObjectCtrl)
                    Case "IOraSession", "IOraDatabase"
                        strErrorDesc = strErrorDesc & IOraDatabaseExtract(oObjectCtrl, sMessage)
                    Case "Connection"
                        strErrorDesc = strErrorDesc & ADOConnectionExtract(oObjectCtrl, sMessage, lngErrorNum)
                        oObjectCtrl = Nothing
                End Select
                oObjectCtrl = Nothing
            Else
                If sMessage > String.Empty Then
                    strMessage = strMessage & "    " & Replace(sMessage, vbCrLf, "    " & vbCrLf) & vbCrLf
                End If
                strErrorDesc = strMessage
            End If
        End If
        strMessage = strErrorDesc & AddCallStackInfo(blnFirstErr, strMethodName, strArgNames, aArrArgs, lngErrorLine)

        If bNotDriveError Then
            nErrorNum = lngErrorNum
            sErrorMsg = strMessage
            Err.Clear()
        Else
            mstrErrorReport = strMessage
            If lngErrorNum <> ERR_PROPAGATION Then
                '           #If LOG Then
                '               App.StartLogging strErrorSrc, vbLogAuto
                '               App.LogEvent strMessage, vbLogEventTypeError
                '           #End If

                Call ASPException(strMessage, lngErrorNum)
            End If
            If (nErrorAction And ENUM_ERROR_ACTION.EA_RERAISE) <> 0 Then
                Err.Raise(ERR_PROPAGATION, SRC_PROCERROR, strMessage)
            End If
        End If
    End Sub

    '%Objetivo: .
    Public ReadOnly Property ErrReport() As String
        Get
            ErrReport = mstrErrorReport
        End Get
    End Property

    '**%Objetive: .
    '%Objetivo: .
    Private Function ErrGetComputerName() As String
        ErrGetComputerName = System.Net.Dns.GetHostName
    End Function

    '**%Objetive: .
    '**%Parameters:
    '**%    sMethodHeader - .
    '**%    sMethodName   - .
    '**%    sArgNames     - .
    '%Objetivo: .
    '%Parámetros:
    '%    sMethodHeader - .
    '%    sMethodName   - .
    '%    sArgNames     - .
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

    '**%Objetive: .
    '**%Parameters:
    '**%    bFirstError - .
    '**%    sMethodName - .
    '**%    sArgNames   - .
    '**%    aArrArgs    - .
    '**%    nErrorLine  - .
    '%Objetivo: .
    '%Parámetros:
    '%    bFirstError - .
    '%    sMethodName - .
    '%    sArgNames   - .
    '%    aArrArgs    - .
    '%    nErrorLine  - .
    Private Function AddCallStackInfo(ByVal bFirstError As Boolean, ByVal sMethodName As String, ByVal sArgNames As String, ByVal aArrArgs As Object, ByVal nErrorLine As Integer) As String
        Dim clsRegistrySupport As eRemoteDB.VisualTimeConfig
        Dim strLine As String
        Dim strFilename As String
        Dim lngHandler As Integer

        On Error GoTo AddCallStackInfo_Err
        AddCallStackInfo = String.Empty
        If bFirstError Then
            AddCallStackInfo = "  Call Stack:" & vbCrLf
        End If

        If nErrorLine = 0 Then
            strLine = String.Empty
        Else
            strLine = "  at Line " & nErrorLine
        End If
        AddCallStackInfo = AddCallStackInfo & "    " & My.Application.Info.AssemblyName & "." & sMethodName & "(" & CreateNameValueList(sArgNames, aArrArgs) & ")" & strLine & vbCrLf

        If Not bFirstError Then
            clsRegistrySupport = New eRemoteDB.VisualTimeConfig
            strFilename = clsRegistrySupport.LoadSetting("ErrorLog", "c:\Inetpub\wwwroot\VTimeNetLat\Log", "Paths") & "\Log" & Today.ToString("yyyyMMdd") & ".htm"
            clsRegistrySupport = Nothing
            lngHandler = FreeFile()
            FileOpen(lngHandler, strFilename, OpenMode.Append)
            PrintLine(lngHandler, "<xmp>" & AddCallStackInfo & "</xmp>")
            FileClose(lngHandler)
        End If

        Exit Function
AddCallStackInfo_Err:
    End Function

    '**%Objetive: .
    '**%Parameters:
    '**%    sNames  - .
    '**%    aValues - .
    '%Objetivo: .
    '%Parámetros:
    '%    sNames  - .
    '%    aValues - .
    Public Function CreateNameValueList(ByVal sNames As String, ByVal aValues As Object) As String
        Dim arrNames() As String = Nothing
        Dim intIndex As Short
        Dim strBuffer As String = String.Empty

        On Error GoTo CreateNameValueList_Err

        arrNames = Microsoft.VisualBasic.Split(sNames, ",")
        For intIndex = 0 To UBound(aValues)
            If strBuffer > String.Empty Then
                strBuffer = strBuffer & ","
            End If
            strBuffer = strBuffer & arrNames(intIndex) & ":=" & ErrVarToString(aValues(intIndex))
        Next intIndex
        CreateNameValueList = strBuffer
        Exit Function
CreateNameValueList_Err:
    End Function

    '**%Objetive: .
    '**%Parameters:
    '**%    vValue - .
    '%Objetivo: .
    '%Parámetros:
    '%    vValue - .
    Private Function ErrVarToString(ByVal vValue As Object) As String
        If IsArray(vValue) Then
            ErrVarToString = "{Array}"
        Else
            Select Case VarType(vValue)
                Case VariantType.Short, VariantType.Integer, VariantType.Byte, VariantType.Single, VariantType.Double, VariantType.Decimal, VariantType.Decimal
                    ErrVarToString = CStr(vValue)
                Case VariantType.Boolean
                    If vValue Then
                        ErrVarToString = "True"
                    Else
                        ErrVarToString = "False"
                    End If
                Case VariantType.Date
                    ErrVarToString = """" & CStr(vValue) & """"
                Case VariantType.Error
                    ErrVarToString = ""
                Case VariantType.Empty
                    ErrVarToString = "{Empty}"
                Case VariantType.Null
                    ErrVarToString = "{Null}"
                Case VariantType.String

                    vValue = Replace(vValue, vbNewLine, String.Empty)
                    vValue = Replace(vValue, vbTab, " ")

                    If Len(vValue) > 100 Then
                        vValue = Left(vValue, 100) & "..."
                    End If
                    ErrVarToString = """" & vValue & """"
                Case VariantType.Object
                    ErrVarToString = "{" & TypeName(vValue) & "}" 'Value of Nothing will be shown as "Nothing"
                Case Else
                    ErrVarToString = "{?}"
            End Select
        End If
    End Function

    '**%Objetive: .
    '**%Parameters:
    '**%    oObjectCtrl - .
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

    '**%Objetive: .
    '**%Parameters:
    '**%    oObjectCtrl - .
    '**%    sMessage    - .
    '%Objetivo: .
    '%Parámetros:
    '%    oObjectCtrl - .
    '%    sMessage    - .
    Private Function IOraDatabaseExtract(ByRef oObjectCtrl As Object, ByVal sMessage As String) As String
        IOraDatabaseExtract = "  Oracle Object For OLE Info:" & vbNewLine

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
        oObjectCtrl = Nothing
    End Function

    '**%Objetive: .
    '**%Parameters:
    '**%    oObjectCtrl - .
    '**%    sMessage    - .
    '%Objetivo: .
    '%Parámetros:
    '%    oObjectCtrl - .
    '%    sMessage    - .
    Public Function ADOConnectionExtract(ByRef oObjectCtrl As Object, ByVal sMessage As String, ByRef nNativeErrorNum As Integer) As String
        Dim objError As Object
        Dim blnFirst As Boolean
        Dim strErrorMsg As String

        strErrorMsg = "  Microsoft ADO Info:" & vbCrLf & "    ADO Version:   " & oObjectCtrl.Version & vbCrLf & "    ADO Object :   " & TypeName(oObjectCtrl) & vbCrLf & "    Conn. State:   " & ConnStateAsString(oObjectCtrl.State) & vbCrLf

        '"    Conn. String: '" & oObjectCtrl.ConnectionString & "'" & vbCrLf

        If sMessage > String.Empty Then
            strErrorMsg = strErrorMsg & "    " & Replace(sMessage, vbCrLf, "    " & vbCrLf) & vbCrLf
        End If
        If oObjectCtrl.Errors.Count > 0 Then
            blnFirst = True
            For Each objError In oObjectCtrl.Errors
                With objError
                    If blnFirst Then
                        strErrorMsg = strErrorMsg & "      Error Number : " & .Number & IIf(.NativeError <> 0, " (NativeError='" & .NativeError & "')", String.Empty) & vbCrLf & "      Source       : " & .Source & vbCrLf
                        If .SQLState <> String.Empty Then
                            strErrorMsg = strErrorMsg & "      SQL State    : " & .SQLState & vbCrLf
                        End If
                        strErrorMsg = strErrorMsg & "      Description  : " & .Description & vbCrLf
                        nNativeErrorNum = .NativeError
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


    '**%Objetive: .
    '**%Parameters:
    '**%    AState - .
    '%Objetivo: .
    '%Parámetros:
    '%    AState - .
    Private Function ConnStateAsString(ByVal AState As Integer) As String
        Dim sState As String = String.Empty

        On Error GoTo errHandler

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

    '**%Objetive: .
    '**%Parameters:
    '**%    Value - .
    '**%    Flag - .
    '%Objetivo: .
    '%Parámetros:
    '%    Value - .
    '%    Flag - .
    Private Function FlagSet(ByVal Value As Integer, ByVal Flag As Integer) As Boolean
        FlagSet = ((Value And Flag) <> 0)
    End Function

    '**%Objetive: .
    '**%Parameters:
    '**%    sMessage - .
    '**%    nErrorNum - .
    '%Objetivo: .
    '%Parámetros:
    '%    sMessage - .
    '%    nErrorNum - .
    Private Sub ASPException(ByVal sMessage As String, ByVal nErrorNum As Integer, Optional ByRef sTitle As String = "Database Error")
        Dim lngHandler As Integer
        Dim strFilename As String
        Dim strID As String
        Dim strAnchor As String
        Dim context As HttpContext = System.Web.HttpContext.Current

        If Not IsNothing(context) Then
            Dim session As HttpSessionState = context.Session()
            Dim strSessionID As String = session.SessionID
            Dim strUsercode As String = session("nUserCode")
            Dim response As HttpResponse = context.Response
            Dim server As HttpServerUtility = context.Server
            strID = strSessionID & Now.ToString("yyyyMMddmmhhmmss")
            strAnchor = "<a NAME=""" & strID & """>" & strID & "</a><BR>" & "Usuario: " & strUsercode

            strFilename = server.MapPath("")
            If strFilename.IndexOf("\VTimeNet") > -1 Then
                strFilename = strFilename.Substring(0, strFilename.IndexOf("\VTimeNet") + 9)
            End If
            strFilename &= "\Log\" & Today.ToString("yyyyMMdd") & ".htm"
            If IO.Directory.Exists(IO.Path.GetDirectoryName(strFilename)) Then


                lngHandler = FreeFile()
                FileOpen(lngHandler, strFilename, OpenMode.Append)
                PrintLine(lngHandler, "------------------------------------------------------------------------------------------<BR>")
                PrintLine(lngHandler, strAnchor & vbCrLf & "<xmp>" & "Hora: " & TimeString & vbCrLf & sTitle & vbCrLf & sMessage & vbCrLf & "</xmp>")
                FileClose(lngHandler)

                If session("sOnErrorClear") <> "1" Then
                    response.Clear()
                End If

                '+ Se tuvo se sustitiut el redirect por código js para habilitar la opción recargar
                '+ The redirect was changed for JavaScript code to enable the option "retry"
                response.Write("<SCRIPT>self.document.location.href='/VTimeNet/Common/Exception.aspx?sErrURL=" & server.UrlEncode("/VTimeNet/Log/" & Today.ToString("yyyyMMdd") & ".htm#" & strID) & "&nErrorNum=" & strID & "&nErrorOracle=" & nErrorNum & "&sPage=' + self.document.location.href ;</SCRIPT>")
                response.End()
            End If

        End If
    End Sub

    Public Function ShowCall(ByVal sMethodHeader As String, Optional ByVal aArrArgs As Object = Nothing) As String
        Dim strMethodName As String = String.Empty
        Dim strArgNames = String.Empty

        ParseMethodHeader(sMethodHeader, strMethodName, strArgNames)

        ShowCall = My.Application.Info.AssemblyName & "." & strMethodName & "(" & CreateNameValueList(strArgNames, aArrArgs) & ")"
    End Function

    '%Objetivo: .
    '%Parámetros:
    '%    sAction       -
    '%    sMethodHeader -
    '%    aArrArgs      -
    Public Sub DebugLog(ByRef sAction As String, ByRef sMethodHeader As String, Optional ByRef aArrArgs As Object = Nothing)
        Dim clsConfig As eRemoteDB.VisualTimeConfig
        Dim strFilename As String
        Dim sngTimer As Single
        Dim intValue As Short
        Dim strTime As String
        Dim lngHan As Integer

        ''On Error GoTo ErrorHandler


        clsConfig = New eRemoteDB.VisualTimeConfig
        strFilename = clsConfig.LoadSetting("VisualTIMERLog", "D:\VisualTIMENet\VisualTIMERLog", "Paths")
        clsConfig = Nothing

        With New ASPSupport
            strFilename += String.Format("\{0}.log", .SessionID)
        End With

        '+Se calcula la hora exacta
        sngTimer = Microsoft.VisualBasic.Timer()

        If sngTimer >= 3600 Then
            intValue = Int(sngTimer / 3600.0!)
            sngTimer = sngTimer - (3600.0! * intValue)
        Else
            intValue = 0
        End If
        strTime = intValue.ToString("00") & ":"

        If sngTimer >= 60 Then
            intValue = Int(sngTimer / 60)
            sngTimer = sngTimer - (60 * intValue)
        Else
            intValue = 0
        End If
        strTime = strTime & intValue.ToString("00") & ":" & sngTimer.ToString("00.00000")
        If InStr(strTime, ",") > 0 Then
            Mid(strTime, InStr(strTime, ","), 1) = "."
        End If

        lngHan = FreeFile()
        FileOpen(lngHan, strFilename, OpenMode.Append)
        If sAction = "Push" Then
            PrintLine(lngHan, strTime & "|Push|" & ShowCall(sMethodHeader, aArrArgs))
        ElseIf sAction = "Pop" Then
            PrintLine(lngHan, strTime & "|Pop|" & ShowCall(sMethodHeader, aArrArgs))
        Else
            PrintLine(lngHan, strTime & "|" & sAction & "|" & sMethodHeader)
        End If
        FileClose(lngHan)

        Exit Sub
ErrorHandler:
        ProcError("ErrorManager.DebugLog(sAction,sMethodHeader,aArrArgs)", New Object() {sAction, sMethodHeader, aArrArgs})
    End Sub

    '%Objetivo: .
    Public Function IsIDEMode() As Boolean

        IsIDEMode = True

        'Dim strFilename As String
        'Dim lngCount As Integer

        'strFilename = New String(Chr(0), 255) '// Just space could be used, just To reserve memo...
        '      lngCount = GetModuleFileName(VB6.GetHInstance.ToInt32, strFilename, 255) '// We have exe file, vb or app
        'strFilename = Left(strFilename, lngCount) '// Remove the empty
        ''//IMPORTANT: Here you just set VB5, or
        ''     VB6, just that... simple.
        'If UCase(Right(strFilename, 7)) <> "VB6.EXE" Then '// We only need To see if it is vb
        '	IsIDEMode = True '// No
        'Else
        '	IsIDEMode = True '// Yes
        'End If
        ''// If you need you can get the VB versi
        ''     on, that is just few lines more...
    End Function
End Module
