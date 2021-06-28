Option Strict Off
Option Explicit On
Module XMLSupport
	'**+Objective: Class that supports the table XMLSupport
	'**+           it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla XMLSupport
	'+          cuyo contenido es:
	'+Version: $$Revision: 2 $
	
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
	Public Function BuildXMLElement(ByVal sName As String, ByVal vValue As Object, ByVal nLevel As Integer) As String
		Dim vntNewValue As Object
		Dim strSpace As String
		Dim strCrLf As String
		
		On Error GoTo ErrorHandler
		If bXMLIsCompress Then
			strSpace = String.Empty
			strCrLf = String.Empty
		Else
			strSpace = Space(nLevel)
			strCrLf = vbCrLf
		End If
		vntNewValue = Nothing
		
		'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		Select Case TypeName(vValue)
			Case "object"
			Case "Byte"
			Case "Integer", "Long", "Single", "Short"
				If vValue <> 0 Then
					vntNewValue = vValue
				End If
			Case "Double", "Decimal", "Currency"
				If vValue <> 0 Then
					vntNewValue = Replace(vValue, ",", ".")
				End If
			Case "Date"
                If IsDate(vValue) Then
                    'vntNewValue = Format(vValue, "YYYY-MM-DDTHH:MM:SS")
                    vntNewValue = Format(vValue, "yyyy-MM-dd HH:mm:ss")
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
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			If Not IsNothing(vntNewValue) Then
				BuildXMLElement = sName & "='" & CStr(vntNewValue) & "' "
			End If
		Else
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			If Not IsNothing(vntNewValue) Then
				BuildXMLElement = strSpace & "<" & sName & ">" & CStr(vntNewValue) & "</" & sName & ">" & strCrLf
			End If
		End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("XMLSupport.BuildXMLElement(sName,vValue,nLevel)", New Object(){sName, vValue, nLevel})
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
	Public Function BuildXMLEntity(ByVal sName As String, ByVal sContent As String, ByVal nLevel As Integer, Optional ByVal bCollect As Boolean = False, Optional ByVal sAttribute As String = "") As String
		Dim strSpace As String
		Dim strCrLf As String
		Dim strRootAttribute As String
        Dim blnShowHeader As Boolean
        Dim strResultado As String = ""

        Try
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
                    strRootAttribute = strRootAttribute & " _HandledAsAttribute='Yes'"
                End If
                If bXMLIsCompress Then
                    strRootAttribute = strRootAttribute & " _IsCompress='Yes'"
                End If
                sAttribute = Trim(strRootAttribute & " " & sAttribute)
            End If

            If sAttribute > String.Empty Then
                sAttribute = " " & Trim(sAttribute)
            End If
            If bXMLHandledAsAttribute Then
                If Trim(sContent) > String.Empty Then
                    If bCollect Then
                        strResultado = strSpace & "<" & sName & sAttribute & ">" & strCrLf & sContent & strSpace & "</" & sName & ">" & strCrLf

                    Else

                        strResultado = strSpace & "<" & sName & sAttribute & ">" & strCrLf & sContent & strSpace & "</" & sName & ">" & strCrLf
                    End If
                ElseIf sAttribute > String.Empty Then
                    strResultado = strSpace & "<" & sName & sAttribute & "/>" & strCrLf
                End If
            Else
                If Trim(sContent) > String.Empty Then
                    strResultado = strSpace & "<" & sName & sAttribute & ">" & strCrLf & sContent & strSpace & "</" & sName & ">" & strCrLf
                End If
            End If
            If blnShowHeader Then
                strResultado = GetXMLHeader() & strResultado
            End If

            Return strResultado
        Catch ex As Exception
        End Try
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        ProcError("XMLSupport.BuildXMLEntity(sName,sContent,nLevel,bCollect,sAttribute)", New Object() {sName, sContent, nLevel, bCollect, sAttribute})
        Return strResultado
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
		On Error GoTo ErrorHandler
		Select Case nLanguage
			Case eXMLLanguage.exlEnglish
				If Right(sName, 2) = "sh" Then
					GetEntityPlural = sName & "es"
				ElseIf Right(sName, 2) = "ch" Then 
					GetEntityPlural = sName & "es"
				ElseIf Right(sName, 1) = "y" Then 
					GetEntityPlural = Left(sName, Len(sName) - 1) & "ies"
				ElseIf Right(sName, 1) = "s" Then 
					GetEntityPlural = sName & "es"
				ElseIf Right(sName, 1) = "x" Then 
					GetEntityPlural = sName & "es"
				ElseIf Right(sName, 1) = "o" Then 
					GetEntityPlural = sName & "es"
				Else
					GetEntityPlural = sName & "s"
				End If
			Case eXMLLanguage.exlSpanish
				GetEntityPlural = sName & "s"
		End Select
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("XMLSupport.GetEntityPlural(sName,nLanguage)", New Object(){sName, nLanguage})
	End Function
	
	'**%Objective:
	'%Objetivo: .
	Public Function GetXMLHeader() As String
		Dim strCrLf As String
		
		On Error GoTo ErrorHandler
		If bXMLIsCompress Then
			strCrLf = String.Empty
		Else
			strCrLf = vbCrLf
		End If
		GetXMLHeader = "<?xml version='1.0' encoding='ISO-8859-1'?>" & strCrLf
		
		Exit Function
ErrorHandler: 
		ProcError("XMLSupport.GetXMLHeader()")
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
        Dim strValue As String = ""

        On Error GoTo ErrorHandler
        If bXMLHandledAsAttribute Or bIsAttribute Then
            If Not oNode.Attributes.GetNamedItem(sNodeName) Is Nothing Then
                strValue = oNode.Attributes.GetNamedItem(sNodeName).InnerText
            End If
        Else
            If Not oNode.SelectSingleNode(sNodeName) Is Nothing Then
                strValue = oNode.SelectSingleNode(sNodeName).InnerText
            End If
        End If

        Select Case nType
            Case eXMLGetValueType.exvString
                If strValue = String.Empty Then
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
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
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
                    If IsNothing(vDefault) Then
                        XMLGetValue = 0 'dtmNull
                    Else
                        XMLGetValue = vDefault
                    End If
                Else
                    XMLGetValue = CDate(Mid(strValue, 9, 2) & "/" & Mid(strValue, 6, 2) & "/" & Mid(strValue, 1, 4))
                End If
            Case eXMLGetValueType.exvDouble, eXMLGetValueType.exvDecimal
                If strValue = String.Empty Then
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
                    If IsNothing(vDefault) Then
                        XMLGetValue = 0
                    Else
                        XMLGetValue = vDefault
                    End If
                Else
                    XMLGetValue = CDbl(Replace(strValue, ".", ","))
                End If
            Case eXMLGetValueType.exvLong
                If strValue = String.Empty Then
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
                    If IsNothing(vDefault) Then
                        XMLGetValue = 0
                    Else
                        XMLGetValue = vDefault
                    End If
                Else
                    XMLGetValue = CInt(strValue)
                End If
            Case eXMLGetValueType.exvInteger
                If strValue = String.Empty Then
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
                    If IsNothing(vDefault) Then
                        XMLGetValue = 0
                    Else
                        XMLGetValue = vDefault
                    End If

                Else
                    XMLGetValue = CShort(strValue)
                End If
            Case eXMLGetValueType.exvByte
                If strValue = String.Empty Then
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
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
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
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
ErrorHandler:
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        ProcError("XMLSupport.XMLGetValue(oNode,sNodeName,nType,bIsAttribute,vDefault)", New Object() {oNode, sNodeName, nType, bIsAttribute, vDefault})
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
		On Error GoTo ErrorHandler
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
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("XMLSupport.Encode(bEncode,sValue)", New Object(){bEncode, sValue})
	End Function
End Module






