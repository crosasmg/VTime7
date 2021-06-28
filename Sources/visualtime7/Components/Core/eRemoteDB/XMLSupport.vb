Option Strict Off
Option Explicit On

Module XMLSupport
	'+Objetivo: Módulo de uso general que provee el soporte para la manipulación de valores XML.
	
	'-Objetivo: Permite identificar la vesión de este fuente en la base de datos de Visual Source Safe.
	Private Const SRC_VERSION As String = "$Revision: 2 $$Date: 6/01/06 4:24p $"
	
	'-Objetivo:
	Public Enum eXMLGetValueType
		exvString = 1
		exvDate = 2
		exvByte = 3
		exvInteger = 4
		exvLong = 5
		exvDecimal = 6
		exvDouble = 7
		exvCurrency = 8
		exvBoolean = 9
	End Enum
	
	'-Objetivo:
	Public Enum eXMLLanguage
		exlEnglish = 1
		exlSpanish = 2
	End Enum
	
	'-Objetivo:
	Public bXMLHandledAsAttribute As Boolean
	
	'-Objetivo:
	Public bXMLIsCompress As Boolean
	
	'%Objetivo:
	'%Parámetros:
	'%    sName  -
	'%    vValue -
	'%    nType  - Tipo de datos del elemento a procesar.
	'%    nLevel -
	Public Function BuildXMLElement(ByVal sName As String, ByVal vValue As Object, ByVal nType As eXMLGetValueType, Optional ByVal nLevel As Short = 1, Optional ByVal bNotFormat As Boolean = False, Optional ByVal bNotIncludeEmpty As Boolean = True) As String
		Dim vntNewValue As Object
		Dim strSpace As String
		Dim strCrLf As String
		
		If Not IsIDEMode Then
			''On Error GoTo ErrorHandler
		End If

        BuildXMLElement = String.Empty
		If bXMLIsCompress Then
			strSpace = String.Empty
			strCrLf = String.Empty
		Else
			strSpace = Space(nLevel)
			strCrLf = vbCrLf
		End If
		vntNewValue = Nothing
		
		If Not IsDbNull(vValue) Then
			Select Case nType
				Case eXMLGetValueType.exvByte, eXMLGetValueType.exvInteger, eXMLGetValueType.exvLong
					If Not bNotIncludeEmpty Or vValue <> 0 Then
						vntNewValue = vValue
					End If
				Case eXMLGetValueType.exvDouble, eXMLGetValueType.exvDecimal
					If Not bNotIncludeEmpty Or vValue <> 0 Then
						vntNewValue = Replace(vValue, ",", ".")
					End If
				Case eXMLGetValueType.exvDate
                    If Not bNotIncludeEmpty Or vValue <> dtmNull Then
                        vntNewValue = Convert.ToDateTime(vValue).ToString("yyyy-MM-ddTHH:mm:ss")
                    End If
				Case eXMLGetValueType.exvString
					If Not bNotIncludeEmpty Or vValue > String.Empty Then
						vntNewValue = XMLEncode(True, Trim(vValue))
					End If
				Case eXMLGetValueType.exvBoolean
					If vValue Then
						vntNewValue = "Yes"
					ElseIf Not bNotIncludeEmpty Then 
						vntNewValue = "No"
					End If
			End Select
		End If
		If bNotFormat Then
			BuildXMLElement = CStr(vntNewValue)
		Else
			If bXMLHandledAsAttribute Then
				
				If Not IsNothing(vntNewValue) Then
					BuildXMLElement = sName & "='" & CStr(vntNewValue) & "' "
				End If
			Else
				
				If Not IsNothing(vntNewValue) Then
					BuildXMLElement = strSpace & "<" & sName & ">" & CStr(vntNewValue) & "</" & sName & ">" & strCrLf
				End If
			End If
		End If
		
		Exit Function
ErrorHandler: 
		ProcError("XMLSupport.BuildXMLElement(sName,vValue,nLevel,bNotFormat,bNotIncludeEmpty)", New Object(){sName, vValue, nLevel, bNotFormat, bNotIncludeEmpty}, CShort(SRC_VERSION))
	End Function
	
	
	'%Objetivo:
	'%Parámetros:
	'%    sName         -
	'%    sItems        -
	'%    sChildContent -
	'%    nLevel        -
	'%    bCollect      -
	'%    sAttribute    -
	Public Function BuildXMLEntity(ByVal sName As String, Optional ByVal sItems As String = "", Optional ByVal sChildContent As String = "", Optional ByVal nLevel As Short = 0, Optional ByVal bCollect As Boolean = False, Optional ByVal sAttribute As String = "") As String

		Dim strSpace As String
		Dim strCrLf As String
		Dim strRootAttribute As String
		Dim blnShowHeader As Boolean
		
		If Not IsIDEMode Then
			''On Error GoTo ErrorHandler
		End If
		
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
				strRootAttribute = strRootAttribute & "_HandledAsAttribute='Yes'"
			End If
			If bXMLIsCompress Then
				If strRootAttribute <> String.Empty Then
					strRootAttribute = strRootAttribute & Space(1)
				End If
				strRootAttribute = strRootAttribute & "_IsCompress='Yes'"
			End If
			If sAttribute <> String.Empty Then
				sAttribute = Trim(strRootAttribute & " " & sAttribute)
			Else
				sAttribute = strRootAttribute
			End If
			
		End If
		
		If sAttribute <> String.Empty Then
			sAttribute = Space(1) & sAttribute
		End If
		If bXMLHandledAsAttribute Then
			If sItems <> String.Empty Then
				sItems = Space(1) & Trim(sItems)
			End If
			If sChildContent <> String.Empty Then
				BuildXMLEntity = strSpace & "<" & sName & sAttribute & sItems & ">" & strCrLf & sChildContent & strSpace & "</" & sName & ">" & strCrLf
			Else
				BuildXMLEntity = strSpace & "<" & sName & sAttribute & sItems & "/>" & strCrLf
			End If
		Else
			BuildXMLEntity = strSpace & "<" & sName & sAttribute & ">" & strCrLf & sItems & sChildContent & strSpace & "</" & sName & ">" & strCrLf
			
		End If
		If blnShowHeader Then
			BuildXMLEntity = GetXMLHeader & BuildXMLEntity
		End If
		
		Exit Function
ErrorHandler: 
		ProcError("XMLSupport.BuildXMLEntity(sName,sContent,nLevel,bCollect,sAttribute)", New Object(){sName, sItems, sChildContent, nLevel, bCollect, sAttribute}, CShort(SRC_VERSION))
	End Function
	
	'%Objetivo: Se encarga de generar el plural en ingles para una palabra.
	'%Parámetros:
	'%    sName     -
	'%    nLanguage -
	Public Function GetEntityPlural(ByVal sName As String, Optional ByVal nLanguage As eXMLLanguage = eXMLLanguage.exlEnglish) As String
		Dim strUpName As String
		
		If Not IsIDEMode Then
			''On Error GoTo ErrorHandler
        End If
        GetEntityPlural = String.Empty
		
		strUpName = UCase(sName)
		Select Case nLanguage
			Case eXMLLanguage.exlEnglish
				If Right(sName, 2) = "SH" Then
					GetEntityPlural = sName & "es"
				ElseIf Right(sName, 2) = "CH" Then 
					GetEntityPlural = sName & "es"
				ElseIf Right(sName, 1) = "Y" Then 
					GetEntityPlural = Left(sName, Len(sName) - 1) & "ies"
				ElseIf Right(sName, 1) = "S" Then 
					GetEntityPlural = sName & "es"
				ElseIf Right(sName, 1) = "X" Then 
					GetEntityPlural = sName & "es"
				ElseIf Right(sName, 1) = "O" Then 
					GetEntityPlural = sName & "es"
				Else
					GetEntityPlural = sName & "s"
				End If
			Case eXMLLanguage.exlSpanish
				GetEntityPlural = sName & "s"
		End Select
		Exit Function
ErrorHandler: 
		ProcError("XMLSupport.GetEntityPlural(sName,nLanguage)", New Object(){sName, nLanguage}, CShort(SRC_VERSION))
	End Function
	
	'%Objetivo:
	Public Function GetXMLHeader() As String
		Dim strCrLf As String
		
		If Not IsIDEMode Then
			''On Error GoTo ErrorHandler
		End If
		
		If bXMLIsCompress Then
			strCrLf = String.Empty
		Else
			strCrLf = vbCrLf
		End If
		GetXMLHeader = "<?xml version='1.0' encoding='ISO-8859-1'?>" & strCrLf
		
		Exit Function
ErrorHandler: 
		ProcError("XMLSupport.GetXMLHeader()",  , CShort(SRC_VERSION))
	End Function
	
	'%Objetivo: Devuelve el valor de un elemento a partir de su nodo XML.
	'%Parámetros:
	'%    oNode        - Nodo del XMLDOM donde se debe obtener un elemento determiando.
	'%    sNodeName    - Nombre del elemento a procesar.
	'%    nType        - Tipo de datos del elemento a procesar.
	'%    bIsAttribute - Indica si el elemento es tratado como un atriibuto.
	'%    vDefault     - Valor por defecto
    Public Function XMLGetValue(ByRef oNode As Xml.XmlNode, ByVal sNodeName As String, ByVal nType As eXMLGetValueType, Optional ByVal bIsAttribute As Boolean = False, Optional ByVal vDefault As Object = Nothing) As Object
        Dim strValue As String = String.Empty

        If Not IsIDEMode() Then
            ''On Error GoTo ErrorHandler
        End If
        XMLGetValue = Nothing

        If Not oNode Is Nothing Then
            If bXMLHandledAsAttribute Or bIsAttribute Then
                If Not oNode.Attributes.getNamedItem(sNodeName) Is Nothing Then
                    strValue = oNode.Attributes.GetNamedItem(sNodeName).InnerText
                End If
            Else
                If Not oNode.selectSingleNode(sNodeName) Is Nothing Then
                    strValue = oNode.SelectSingleNode(sNodeName).InnerText
                End If
            End If
        End If

        Select Case nType
            Case eXMLGetValueType.exvString
                If strValue = String.Empty Then
                    
                    If IsNothing(vDefault) Then
                        XMLGetValue = String.Empty
                    Else
                        XMLGetValue = Trim(XMLEncode(False, vDefault))
                    End If
                Else
                    XMLGetValue = Trim(XMLEncode(False, strValue))
                End If
            Case eXMLGetValueType.exvDate
                If strValue = String.Empty Then
                    
                    If IsNothing(vDefault) Then
                        XMLGetValue = 0
                    Else
                        XMLGetValue = vDefault
                    End If
                Else
                    XMLGetValue = DateSerial(CShort(Mid(strValue, 1, 4)), CShort(Mid(strValue, 6, 2)), CShort(Mid(strValue, 9, 2)))
                End If
            Case eXMLGetValueType.exvDouble, eXMLGetValueType.exvDecimal
                If strValue = String.Empty Then
                    
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
        ProcError("XMLSupport.XMLGetValue(oNode,sNodeName,nType,bIsAttribute,vDefault)", New Object() {oNode, sNodeName, nType, bIsAttribute, vDefault}, CShort(SRC_VERSION))
    End Function
	
	'%Objetivo:
	'%Parámetros:
	'%    bEncode -
	'%    sValue  -
	Public Function XMLEncode(ByVal bEncode As Boolean, ByVal sValue As String) As String
		If Not IsIDEMode Then
			''On Error GoTo ErrorHandler
		End If
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
		XMLEncode = sValue
		
		Exit Function
ErrorHandler: 
		ProcError("XMLSupport.XMLEncode(bEncode,sValue)", New Object(){bEncode, sValue}, CShort(SRC_VERSION))
	End Function
	
	'%Objetivo: .
	'%Parámetros:
	'%    sFilename - .
	'%    sRootNode - .
	'%    sStream   - .
    Public Function GetInstance(ByRef sFileName As String, ByRef sRootNode As String, Optional ByRef sStream As String = "") As Xml.XmlDocument
        Dim clsXMLDocument As Xml.XmlDocument

        If Not IsIDEMode() Then
            ''On Error GoTo ErrorHandler
        End If

        clsXMLDocument = New Xml.XmlDocument
        With clsXMLDocument
            If Len(sFileName) > 0 Then
                .Load(sFileName)
            Else
                .LoadXml(sStream)
            End If
            If Len(sRootNode) = 0 Then
                sRootNode = .DocumentElement.Name
            End If
            bXMLHandledAsAttribute = XMLGetValue(.DocumentElement.SelectSingleNode("/" & sRootNode), "_HandledAsAttribute", eXMLGetValueType.exvBoolean, True)
            bXMLIsCompress = XMLGetValue(.DocumentElement.SelectSingleNode("/" & sRootNode), "_IsCompress", eXMLGetValueType.exvBoolean, True)
        End With

        GetInstance = clsXMLDocument

        clsXMLDocument = Nothing
        Exit Function
ErrorHandler:
        ObjectRelease = clsXMLDocument
        ProcError("XMLSupport.GetInstance(sFilename,sRootNode)", New Object() {sFileName, sRootNode}, CShort(SRC_VERSION))
    End Function
End Module






