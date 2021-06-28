Option Strict Off
Option Explicit On
Public Class AfterProcess
	
	
	
	'**%Objective:
	'**%    sForm        -
	'**%    sQueryString -
	'**%    sSession     -
	'**%    bSkipPosting  -
	'**%    sHTMLValidate -
	'%Objetivo: Manejo de procesos después de las rutina de validación para las páginas que no sean secuencias
	'%Parámetros:
	'%      sForm        - Variable provenientes de objeto "Request.Form"
	'%      sQueryString - Variable provenientes de objeto "Request.QueryString"
	'%      sSession     - Variable provenientes de objeto "Session"
	'%      bSkipPosting  - Indica si se deberá saltar el proceso de "Posting" para la transacción en tratamiento
	'%      sHTMLValidate - Permite la generación de validaciones de forma generica
	Public Function AfterValidate(ByVal sForm As String, ByVal sQueryString As String, ByVal sSession As String, Optional ByRef bSkipPosting As Object = False, Optional ByRef sHTMLValidate As Object = "") As String
		Dim strProcess As String
		
        'If Not IsIDEMode Then
        'End If
		If Len(sForm) > 0 Then
			sForm = "&" & sForm
		End If
		
		strProcess = GetQueryStringVariableValue(sQueryString, "sProcess")
		
		AfterValidate = String.Empty
		
		If strProcess = "1" Then
			AfterValidate = "<SCRIPT>" & "ShowPopUp('/VTimeNetLat/Common/PopUp.aspx?sPageName=/VTimeNetLat/Common/SCA004&" & sQueryString & sForm & "','',350,300,'no','no',350,300,false,false);" & "</SCRIPT>"
			bSkipPosting = True
		End If
		
		Exit Function
	End Function
	
	'**%Objective: Handling of procesess of validation pages not related to sequences
	'%Objetivo: Manejo de procesos después del POST en las páginas de validación que no sean secuencias
	Public Function AfterPost(ByVal sFormV As String, ByVal sQueryString As String, ByVal sSessionV As String) As String
		Dim sCodispl As String
		Dim sModule As String
		Dim sProject As String
		Dim sSubProject As String
		Dim nAction As Short
		Dim sCodispII As String
		Dim sCorrespondence As String
		Dim sPrint As String
		Dim nWindowty As Short
		
        'If Not IsIDEMode Then
        'End If
		sCodispl = GetQueryStringVariableValue(sQueryString, "sCodispl")
		sModule = GetQueryStringVariableValue(sQueryString, "sModule")
		sProject = GetQueryStringVariableValue(sQueryString, "sProject")
		sSubProject = GetQueryStringVariableValue(sQueryString, "sSubProject")
		nAction = CShort("0" & GetQueryStringVariableValue(sQueryString, "nAction"))
		sCodispII = GetQueryStringVariableValue(sQueryString, "sCodispII")
		sCorrespondence = GetQueryStringVariableValue(sQueryString, "sCorrespondence")
		sPrint = GetQueryStringVariableValue(sQueryString, "sPrint")
		nWindowty = CShort("0" & GetQueryStringVariableValue(sQueryString, "nWindowTy"))
		
		AfterPost = String.Empty
		
		If sCorrespondence = "1" And ((nAction = 390 And (nWindowty = 3 Or nWindowty = 5)) Or (nAction = 392 And (nWindowty = 1 Or nWindowty = 2 Or nWindowty = 6))) Then
			AfterPost = "<SCRIPT>" & vbNewLine & "if (top.frames['fraSequence'].pintZone == 1) {" & vbNewLine & "lstrZone = 'fraHeader';" & vbNewLine & "}" & vbNewLine & "else {" & vbNewLine & "lstrZone = 'fraFolder'};" & vbNewLine & "ShowPopUp('/VTimeNetLat/Common/PopUp.aspx?sPageName=/VTimeNetLat/Common/SCA008&sZone='+ lstrZone  +'&sCodispII=SCA805&sModule=Common&sCodisp=SCA008&sCodispl=" & sCodispl & "&sCorrespondence=" & sCorrespondence & "&nAction=" & nAction & "&sScrolling=yes','', 700, 280,'no','no',80,230,false,false);" & vbNewLine & "</SCRIPT>"
			
		End If
		
		Exit Function
	End Function
	
	'**%Objective: Gets a value from a variable in the supplied QueryString
	'%Objetivo: Obtiene el valor de una variable pasada en el QueryString
	Public Function GetQueryStringVariableValue(ByVal sQueryString As String, ByVal sVariableName As String) As String
		Dim lintPos As Short
		Dim lintPos1 As Short
		
        'If Not IsIDEMode Then
        'End If
		GetQueryStringVariableValue = String.Empty
		
		If Len(sQueryString) > 0 Then
			If Right(sQueryString, 1) <> "&" Then
				sQueryString = sQueryString & "&"
			End If
			
			lintPos = InStr(1, UCase(sQueryString), UCase(sVariableName))
			If lintPos > 0 Then
				'Marca la posición donde
				'comienza el valor de la variable
				lintPos1 = InStr(lintPos, sQueryString, "=")
				'Marca la posición donde
				'finaliza el valor de la variable
				lintPos = InStr(lintPos1, sQueryString, "&")
				If lintPos - lintPos1 > 1 Then
					GetQueryStringVariableValue = Mid(sQueryString, lintPos1 + 1, lintPos - lintPos1 - 1)
				End If
			End If
		End If
		
		Exit Function
	End Function
End Class











