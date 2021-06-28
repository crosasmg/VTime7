Option Strict Off
Option Explicit On
Module XMLSupport
	'**+Objective: Class that supports the table XMLSupport
	'**+           it's content is:
	'**+Version: $$Revision: $
	'+Objetivo: Clase que le da soporte a la tabla XMLSupport
	'+          cuyo contenido es:
	'+Version: $$Revision: $
	
	'**-Objective:
	'-Objetivo:
	Public Enum eXMLGetValueType
		exvString
		exvDate
		exvByte
		exvInteger
		exvLong
		exvDecimal
		exvDouble
		exvCurrency
		exvBoolean
	End Enum
	
	'**-Objective:
	'-Objetivo:
	Public Enum eXMLLanguage
		exlEnglish
		exlSpanish
	End Enum
	
	'**-Objective:
	'-Objetivo:
	Public bXMLHandledAsAttribute As Boolean
	
	'**-Objective:
	'-Objetivo:
	Public bXMLIsCompress As Boolean
	
	'**%Objective:
	'**%Parameters:
	'**%  sName  -
	'**%  vValue -
	'**%  nLevel -
	'%Objetivo: .
	'%Parámetros:
	'%    sName  -
	'%    vValue -
	'%    nLevel -
	Public Function BuildXMLElement(ByVal sName As String, ByVal vValue As Object, ByVal nLevel As Short) As String
		Dim vntNewValue As Object
		Dim strSpace As String
		Dim strCrLf As String

        BuildXMLElement = String.Empty

#If PERFORMANCE Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression PERFORMANCE did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		sName = UCase$(sName)
		bXMLIsCompress = True
		bXMLHandledAsAttribute = True
#End If
		If bXMLIsCompress Then
			strSpace = String.Empty
			strCrLf = String.Empty
		Else
			strSpace = Space(nLevel)
			strCrLf = vbCrLf
		End If
		vntNewValue = Nothing
		
		'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		Select Case TypeName(vValue)
			Case "object"
			Case "Byte"
			Case "Integer", "Long", "Single", "Short"
				If vValue <> intNull And vValue <> dblNull Then
					vntNewValue = vValue
				End If
			Case "Double", "Decimal", "Currency"
				If vValue <> intNull And vValue <> dblNull Then
					vntNewValue = Replace(vValue, ",", ".")
				End If
			Case "Date"
				If vValue <> dtmNull Then
					vntNewValue = Format(vValue, "YYYY-MM-DDTHH:MM:SS")
				End If
			Case "String"
				If vValue > String.Empty Then
					vntNewValue = Encode(True, vValue)
				End If
			Case "Boolean"
				If vValue Then
					vntNewValue = "True"
				Else
					vntNewValue = "False"
				End If
			Case "Error"
			Case "Empty"
			Case "Null"
			Case "Object"
			Case "Unknown"
			Case "Nothing"
		End Select
		If bXMLHandledAsAttribute Then
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			If Not IsNothing(vntNewValue) Then
				BuildXMLElement = sName & "='" & CStr(vntNewValue) & "' "
			End If
		Else
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			If Not IsNothing(vntNewValue) Then
				BuildXMLElement = strSpace & "<" & sName & ">" & CStr(vntNewValue) & "</" & sName & ">" & strCrLf
			End If
		End If
		
		Exit Function
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  sName      -
	'**%  sContent   -
	'**%  nLevel     -
	'**%  bCollect   -
	'**%  sAttribute -
	'%Objetivo: .
	'%Parámetros:
	'%    sName      -
	'%    sContent   -
	'%    nLevel     -
	'%    bCollect   -
	'%    sAttribute -
	Public Function BuildXMLEntity(ByVal sName As String, ByVal sContent As String, ByVal nLevel As Short, Optional ByVal bCollect As Boolean = False, Optional ByVal sAttribute As String = "") As String
		Dim strSpace As String
		Dim strCrLf As String
		Dim strRootAttribute As String
		Dim blnShowHeader As Boolean

        BuildXMLEntity = String.Empty

#If PERFORMANCE Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression PERFORMANCE did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		sName = UCase$(sName)
		bXMLIsCompress = True
		bXMLHandledAsAttribute = True
#End If
		strRootAttribute = String.Empty
		
		If bXMLIsCompress Then
			strSpace = String.Empty
			strCrLf = String.Empty
		Else
			strSpace = Space(nLevel)
			strCrLf = vbCrLf
		End If
		
		If bCollect Then
			sName = GetEntityPlural(sName)
		End If
		
		If nLevel = 0 Then
			blnShowHeader = True
			If bXMLHandledAsAttribute Then
#If PERFORMANCE Then
				'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression PERFORMANCE did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
				strRootAttribute = strRootAttribute & " _HANDLEDASATTRIBUTE='Yes'"
#Else
				strRootAttribute = strRootAttribute & " _HandledAsAttribute='Yes'"
#End If
			End If
			If bXMLIsCompress Then
#If PERFORMANCE Then
				'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression PERFORMANCE did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
				strRootAttribute = strRootAttribute & " _ISCOMPRESS='Yes'"
#Else
				strRootAttribute = strRootAttribute & " _IsCompress='Yes'"
#End If
				
			End If
			sAttribute = Trim(strRootAttribute & " " & sAttribute)
		End If
		
		If sAttribute > String.Empty Then
			sAttribute = " " & Trim(sAttribute)
		End If
		If bXMLHandledAsAttribute Then
			If Trim(sContent) > String.Empty Then
				If bCollect Then
					BuildXMLEntity = strSpace & "<" & sName & sAttribute & ">" & strCrLf & sContent & strSpace & "</" & sName & ">" & strCrLf
					
				Else
					
					BuildXMLEntity = strSpace & "<" & sName & sAttribute & ">" & strCrLf & sContent & strSpace & "</" & sName & ">" & strCrLf
				End If
			ElseIf sAttribute > String.Empty Then 
				BuildXMLEntity = strSpace & "<" & sName & sAttribute & "/>" & strCrLf
			End If
		Else
			If Trim(sContent) > String.Empty Then
				BuildXMLEntity = strSpace & "<" & sName & sAttribute & ">" & strCrLf & sContent & strSpace & "</" & sName & ">" & strCrLf
			End If
		End If
		If blnShowHeader Then
			BuildXMLEntity = GetXMLHeader & BuildXMLEntity
		End If
		
		Exit Function
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  sName     -
	'**%  nLanguage -
	'%Objetivo: Se encarga de generar el plural en ingles para una palabra.
	'%Parámetros:
	'%    sName     -
	'%    nLanguage -
	Public Function GetEntityPlural(ByVal sName As String, Optional ByVal nLanguage As eXMLLanguage = eXMLLanguage.exlEnglish) As String

        GetEntityPlural = String.Empty

		Select Case nLanguage
			Case eXMLLanguage.exlEnglish
				If UCase(Right(sName, 2)) = "SH" Then
					GetEntityPlural = sName & "es"
				ElseIf UCase(Right(sName, 2)) = "CH" Then 
					GetEntityPlural = sName & "es"
				ElseIf UCase(Right(sName, 1)) = "Y" Then 
					GetEntityPlural = Left(sName, Len(sName) - 1) & "ies"
				ElseIf UCase(Right(sName, 1)) = "S" Then 
					GetEntityPlural = sName & "es"
				ElseIf UCase(Right(sName, 1)) = "X" Then 
					GetEntityPlural = sName & "es"
				ElseIf UCase(Right(sName, 1)) = "O" Then 
					GetEntityPlural = sName & "es"
				Else
					GetEntityPlural = sName & "s"
				End If
			Case eXMLLanguage.exlSpanish
				GetEntityPlural = sName & "s"
		End Select
#If PERFORMANCE Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression PERFORMANCE did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		GetEntityPlural = UCase$(GetEntityPlural)
#End If
		Exit Function
	End Function
	
	'**%Objective:
	'%Objetivo: .
	Public Function GetXMLHeader() As String
		Dim strCrLf As String
		
#If PERFORMANCE Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression PERFORMANCE did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		bXMLIsCompress = True
		bXMLHandledAsAttribute = True
#End If
		
		If bXMLIsCompress Then
			strCrLf = String.Empty
		Else
			strCrLf = vbCrLf
		End If
		GetXMLHeader = "<?xml version='1.0' encoding='ISO-8859-1'?>" & strCrLf
		
		Exit Function
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  oNode        -
	'**%  sNodeName    -
	'**%  nType        -
	'**%  bIsAttribute -
	'**%  vDefault     -
	'%Objetivo: Devuelve el valor de un elemento a partir de su nodo XML.
	'%Parámetros:
	'%    oNode        -
	'%    sNodeName    -
	'%    nType        -
	'%    bIsAttribute -
	'%    vDefault     -
    Public Function XMLGetValue(ByRef oNode As Xml.XmlNode, ByVal sNodeName As String, ByVal nType As eXMLGetValueType, Optional ByRef bIsAttribute As Boolean = False, Optional ByRef vDefault As Object = Nothing) As Object
        Dim strValue As String = String.Empty

        XMLGetValue = Nothing

#If PERFORMANCE Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression PERFORMANCE did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		sNodeName = UCase$(sNodeName)
		bXMLIsCompress = True
		bXMLHandledAsAttribute = True
#End If
        If bXMLHandledAsAttribute Or bIsAttribute Then
            If Not oNode.attributes.getNamedItem(sNodeName) Is Nothing Then
                strValue = oNode.Attributes.GetNamedItem(sNodeName).InnerText
            End If
        Else
            If Not oNode.selectSingleNode(sNodeName) Is Nothing Then
                strValue = oNode.SelectSingleNode(sNodeName).InnerText
            End If
        End If

        Select Case nType
            Case eXMLGetValueType.exvString
                If strValue = String.Empty Then
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
                    If IsNothing(vDefault) Then
                        XMLGetValue = String.Empty
                    Else
                        XMLGetValue = Trim(Encode(False, vDefault))
                    End If
                Else
                    XMLGetValue = Trim(Encode(False, strValue))
                End If
            Case eXMLGetValueType.exvDate
                If strValue = String.Empty Then
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
                    If IsNothing(vDefault) Then
                        XMLGetValue = dtmNull
                    Else
                        XMLGetValue = vDefault
                    End If
                Else
                    XMLGetValue = DateSerial(CShort(Mid(strValue, 1, 4)), CShort(Mid(strValue, 6, 2)), CShort(Mid(strValue, 9, 2)))
                End If
            Case eXMLGetValueType.exvDouble, eXMLGetValueType.exvDecimal
                If strValue = String.Empty Then
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
                    If IsNothing(vDefault) Then
                        XMLGetValue = dblNull
                    Else
                        XMLGetValue = vDefault
                    End If
                Else
                    XMLGetValue = CDbl(Replace(strValue, ".", ","))
                End If
            Case eXMLGetValueType.exvLong
                If strValue = String.Empty Then
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
                    If IsNothing(vDefault) Then
                        XMLGetValue = intNull
                    Else
                        XMLGetValue = vDefault
                    End If
                Else
                    XMLGetValue = CInt(strValue)
                End If
            Case eXMLGetValueType.exvInteger
                If strValue = String.Empty Then
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
                    If IsNothing(vDefault) Then
                        XMLGetValue = intNull
                    Else
                        XMLGetValue = vDefault
                    End If

                Else
                    XMLGetValue = CShort(strValue)
                End If
            Case eXMLGetValueType.exvByte
                If strValue = String.Empty Then
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
                    If IsNothing(vDefault) Then
                        XMLGetValue = 0
                    Else
                        XMLGetValue = vDefault
                    End If
                Else
                    XMLGetValue = CByte(strValue)
                End If
            Case eXMLGetValueType.exvBoolean
                If strValue = String.Empty Then
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
                    If IsNothing(vDefault) Then
                        XMLGetValue = False
                    Else
                        XMLGetValue = vDefault
                    End If
                Else
                    XMLGetValue = (InStr("Y|T", UCase(Mid(strValue, 1, 1))) > 0)
                End If
        End Select

        Exit Function
    End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  bEncode -
	'**%  sValue  -
	'%Objetivo:
	'%Parámetros:
	'%    bEncode -
	'%    sValue  -
	Private Function Encode(ByVal bEncode As Boolean, ByVal sValue As String) As String
		If bEncode Then
			sValue = Replace(sValue, "&", "&amp;")
			sValue = Replace(sValue, "<", "&lt;")
			sValue = Replace(sValue, ">", "&gt;")
			sValue = Replace(sValue, "'", "&apos;")
			sValue = Replace(sValue, """", "&quot;")
		Else
			sValue = Replace(sValue, "&amp;", "&")
			sValue = Replace(sValue, "&lt;", "<")
			sValue = Replace(sValue, "&gt;", ">")
			sValue = Replace(sValue, "&apos;", "'")
			sValue = Replace(sValue, "&quot;", """")
		End If
		Encode = sValue
		
		Exit Function
	End Function
	
	
#If PERFORMANCE Then
	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression PERFORMANCE did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
	
	'**%Objective:
	'**%Parameters:
	'**%    sFilename     -
	'**%    sNodeName     -
	'**%    sXMLStream    -
	'**%    bUseCachePath -
	'**%    sGroupPath    -
	'**%    sRootName     -
	'%Objetivo:
	'%Parámetros:
	'%    sFilename     -
	'%    sNodeName     -
	'%    sXMLStream    -
	'%    bUseCachePath -
	'%    sGroupPath    -
	'%    sRootName     -
	Public Function SynchXMLFile(ByVal sFileName As String, _
	                             ByVal sNodeName As String, _
	                             ByVal sXMLStream As String, _
	                    Optional ByVal bUseCachePath As Boolean = True, _
	                    Optional ByVal sGroupPath As String = "Claims", _
	                    Optional ByVal sRootName As String = "Classes", _
	                    Optional ByVal sAttributeKey As String = "") As Boolean
	Dim lobjSession          As ASPSupport
	Dim lclsVisualTimeConfig As eRemoteDB.VisualTimeConfig
	Dim objXML               As MSXML2.FreeThreadedDOMDocument
	Dim objXMLChild          As MSXML2.FreeThreadedDOMDocument
	Dim objXMLNode           As MSXML2.IXMLDOMNode
	Dim strXMLBuffer         As String
	
	sGroupPath = UCase$(sGroupPath)
	sRootName = UCase$(sRootName)
	sNodeName = UCase$(sNodeName)
	
	Set lobjSession = New ASPSupport
	With lobjSession
	sFileName = .GetASPSessionValue("sXMLClaimFileName")
	End With
	Set lobjSession = Nothing
	
	If sFileName = String.Empty Then
	Exit Function
	End If
	
	If bUseCachePath Then
	Set lclsVisualTimeConfig = New eRemoteDB.VisualTimeConfig
	sFileName = lclsVisualTimeConfig.LoadSetting("Cache", String.Empty, "Paths") & "\" & sGroupPath & "\" & sFileName & ".xml"
	Set lclsVisualTimeConfig = Nothing
	End If
	
	strXMLBuffer = LoadFileToText(sFileName)
	If strXMLBuffer = String.Empty Then
	strXMLBuffer = "<?xml version=""1.0"" encoding=""ISO-8859-1""?><CLASSES _HANDLEDASATTRIBUTE=""Yes"" _ISCOMPRESS=""Yes""></CLASSES>"
	End If
	
	Set objXML = New MSXML2.FreeThreadedDOMDocument
	
	With objXML
	.async = False
	.validateOnParse = False
	.loadXML strXMLBuffer
	If .parseError = 0 Then
	Set objXMLChild = New MSXML2.FreeThreadedDOMDocument
	objXMLChild.async = False
	objXMLChild.validateOnParse = False
	objXMLChild.loadXML sXMLStream
	
	If sAttributeKey <> String.Empty Then
	Set objXMLNode = .documentElement.selectSingleNode("/" & sRootName & "/" & sNodeName & "/" & sAttributeKey)
	If Not objXMLNode Is Nothing Then
	With .documentElement.selectSingleNode("/" & sRootName & "/" & sNodeName)
	.removeChild objXMLNode
	End With
	End If
	
	If .documentElement.selectSingleNode("/" & sRootName & "/" & sNodeName) Is Nothing Then
	.documentElement.appendChild objXMLChild.documentElement
	Else
	With .documentElement.selectSingleNode("/" & sRootName & "/" & sNodeName)
	Set objXMLNode = objXMLChild.documentElement.selectSingleNode("/" & sNodeName & "/" & sAttributeKey)
	.appendChild objXMLNode
	End With
	End If
	
	Else
	Set objXMLNode = .documentElement.selectSingleNode("/" & sRootName & "/" & sNodeName)
	If Not objXMLNode Is Nothing Then
	.documentElement.removeChild objXMLNode
	End If
	Set objXMLNode = objXMLChild.documentElement
	.documentElement.appendChild objXMLNode
	End If
	.save sFileName
	
	Set objXMLChild = Nothing
	SynchXMLFile = True
	Else
	Err.Raise .parseError.errorCode, , .parseError.reason
	End If
	End With
	Set objXML = Nothing
	
	Exit Function
	ProcError "XMLSupport.SynchXMLFile(sFilename,sNodeName,sXMLStream,bUseCachePath,sGroupPath,sRootName)", Array(sFileName, sNodeName, sXMLStream, bUseCachePath, sGroupPath, sRootName)
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%    sFilename     -
	'**%    sNodeName     -
	'**%    bUseCachePath -
	'**%    sGroupPath    -
	'**%    sRootName     -
	'%Objetivo:
	'%Parámetros:
	'%    sFilename     -
	'%    sNodeName     -
	'%    bUseCachePath -
	'%    sGroupPath    -
	'%    sRootName     -
	Public Function GetNodeSynchXMLFile(ByVal sFileName As String, _
	                                    ByVal sNodeName As String, _
	                           Optional ByVal bUseCachePath As Boolean = True, _
	                           Optional ByVal sGroupPath As String = "Claims", _
	                           Optional ByVal sRootName As String = "Classes") As String
	Dim lobjSession          As ASPSupport
	Dim objXML               As MSXML2.FreeThreadedDOMDocument
	Dim lclsVisualTimeConfig As eRemoteDB.VisualTimeConfig
	Dim lstrXMLSchema        As String
	Dim intTransactio        As Integer
	Dim lngPolicy            As Long
	Dim lngDivision          As Long
	Dim strCertif            As String
	Dim lngClaim             As Long
	
	sGroupPath = UCase$(sGroupPath)
	sRootName = UCase$(sRootName)
	sNodeName = UCase$(sNodeName)
	
	Set lobjSession = New ASPSupport
	With lobjSession
	sFileName = .GetASPSessionValue("sXMLClaimFileName")
	End With
	Set lobjSession = Nothing
	
	If sFileName = String.Empty Then
	Exit Function
	End If
	
	If bUseCachePath Then
	Set lclsVisualTimeConfig = New eRemoteDB.VisualTimeConfig
	sFileName = lclsVisualTimeConfig.LoadSetting("Cache", String.Empty, "Paths") & "\" & sGroupPath & "\" & sFileName & ".xml"
	Set lclsVisualTimeConfig = Nothing
	End If
	
	lstrXMLSchema = LoadFileToText(sFileName)
	
	If lstrXMLSchema <> String.Empty Then
	
	Set objXML = New MSXML2.FreeThreadedDOMDocument
	
	With objXML
	.async = False
	.validateOnParse = False
	.loadXML lstrXMLSchema
	If .parseError = 0 Then
	If Not .documentElement.selectSingleNode("/" & sRootName & "/" & sNodeName) Is Nothing Then
	GetNodeSynchXMLFile = lstrXMLSchema
	End If
	Else
	Err.Raise .parseError.errorCode, , .parseError.reason
	End If
	End With
	Set objXML = Nothing
	End If
	
	Exit Function
	ProcError "XMLSupport.GetNodeSynchXMLFile(sFilename,sNodeName,bUseCachePath,sGroupPath,sRootName)", Array(sFileName, sNodeName, bUseCachePath, sGroupPath, sRootName)
	End Function
	
#End If
End Module











