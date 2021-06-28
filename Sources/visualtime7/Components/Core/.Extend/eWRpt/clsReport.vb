Option Strict Off
Option Explicit On
Public Class clsReport
	'%-------------------------------------------------------%'
	'% $Workfile:: clsReport.cls                            $%'
	'% $Author:: Nvaplat17                                  $%'
	'% $Date:: 26/03/03 18:17                               $%'
	'% $Revision:: 56                                       $%'
	'%-------------------------------------------------------%'
	
	'%GetOracleRecordset: Esta función ejecuta un procedimiento almacenado de la base de
	'%datos ORACLE y a partir del cursor que devuelve genera un recordset desconectado
	'%de tipo ADODB.Recordset
	Public Function GetOracleRecordset(ByVal sDatabase As String, ByVal sUser As String, ByVal sPassword As String, ByVal sStoreProcedure As String, ByVal sCursorName As String, ByVal sParameters As String, ByVal sParameterSeparator As String) As ADODB.Recordset
		
		''    Dim lrecReport As ADODB.Recordset
		''    Dim lobjSession As OracleInProcServer.OraSessionClass
		''    Dim lobjDB As OracleInProcServer.OraDatabase
		''    Dim lobjDyn As OracleInProcServer.OraDynaset
		''    Dim lobjField As OracleInProcServer.OraField
		''    Dim lstrSepPos As Long
		''    Dim lintPrmNumber As long
		''    Dim lintPrmCount As long
		''    Dim lstrPrmValue As String
		''    Dim lstrPrmName As String
		''    Dim lstrPLSQL As String
		''    Dim lstrErrSource As String
		''
		''    On Error GoTo ConnectionError
		''
		''    lstrErrSource = "WebReport.CReport.GetOracleRecordset"
		''    Set lobjSession = New OracleInProcServer.OraSessionClass
		''    '-se conecta a la base de datos ORACLE
		''    Set lobjDB = lobjSession.OpenDatabase(sDatabase, sUser & "/" & sPassword, &H0&)  '&H0& - ORADB_DEFAULT
		''
		''    On Error GoTo ParametersError
		''
		''    lstrPLSQL = ""
		''    lintPrmNumber = 0
		''    While Trim(sParameters) <> ""
		''        lintPrmNumber = lintPrmNumber + 1
		''        lstrSepPos = InStr(1, sParameters, sParameterSeparator)
		''        lstrPrmValue = Mid(sParameters, 1, lstrSepPos - 1)
		''        sParameters = Mid(sParameters, (lstrSepPos + Len(sParameterSeparator)), (Len(sParameters) - lstrSepPos - 1))
		''        lstrPrmName = "prm" & CStr(lintPrmNumber)
		''        If UCase(lstrPrmValue) = "<NULL>" Then
		''            lobjDB.Parameters.Add lstrPrmName, Null, 1
		''        Else
		''            lobjDB.Parameters.Add lstrPrmName, lstrPrmValue, 1
		''        End If
		''        lobjDB.Parameters(lstrPrmName).serverType = 1
		''    Wend
		''    lstrPLSQL = "Begin " & sStoreProcedure & " (:" & sCursorName
		''    lintPrmCount = lintPrmNumber
		''    lintPrmNumber = 0
		''    While lintPrmNumber < lintPrmCount
		''        lintPrmNumber = lintPrmNumber + 1
		''        lstrPLSQL = lstrPLSQL & ",:prm" & lintPrmNumber
		''    Wend
		''    lstrPLSQL = lstrPLSQL & "); end;"
		''
		''    On Error GoTo ExecuteStoreProcedureError
		''
		''    '-se ejecuta el procedimiento almacenado.
		''    Set lobjDyn = lobjDB.CreatePlsqlDynaset(lstrPLSQL, sCursorName, 8 + 4)
		''
		''    '-se construye el recordset desconectado
		''    Set lrecReport = New ADODB.Recordset
		''    lrecReport.CursorLocation = 3
		''    lrecReport.CursorType = 3
		''    lrecReport.LockType = 4
		''
		''    On Error GoTo BuilADODBRecordsetError
		''
		''    For Each lobjField In lobjDyn.FieldToClass
		''        Select Case lobjField.OraIDataType
		''            '1 - ORATYPE_VARCHAR2
		''            Case 1
		''                lrecReport.FieldToClass.Append lobjField.Name, 200, lobjField.Size, adFldMayBeNull        '200 - adVarChar
		''            '2 - ORATYPE_NUMBER
		''            Case 2
		''                lrecReport.FieldToClass.Append lobjField.Name, 14, lobjField.OraPrecision, adFldMayBeNull '14 - adDecimal
		''            '7 - ORATYPE_DECIMAL
		''            Case 7
		''                lrecReport.FieldToClass.Append lobjField.Name, 14, lobjField.OraPrecision, adFldMayBeNull '14 - adDecimal
		''            '9 - ORATYPE_VARCHAR
		''            Case 9
		''                lrecReport.FieldToClass.Append lobjField.Name, 200, lobjField.Size, adFldMayBeNull        '200 - adVarChar
		''            '12 - ORATYPE_DATE
		''            Case 12
		''                lrecReport.FieldToClass.Append lobjField.Name, 7, , adFldMayBeNull                        '7 - adDate
		''            '22 - ORATYPE_DOUBLE
		''            Case 22
		''                lrecReport.FieldToClass.Append lobjField.Name, 5, , adFldMayBeNull                        '5 - adDouble
		''            '96 - ORATYPE_CHAR
		''            Case 96
		''                lrecReport.FieldToClass.Append lobjField.Name, 129, lobjField.Size, adFldMayBeNull        '129 - adChar
		''        End Select
		''    Next lobjField
		''
		''    lrecReport.Open
		''    If Not lobjDyn.EOF Then
		''        lobjDyn.MoveFirst
		''        While Not lobjDyn.EOF
		''            lrecReport.AddNew
		''            For Each lobjField In lobjDyn.FieldToClass
		''                lrecReport.FieldToClass(lobjField.Name).Value = lobjField.Value
		''            Next lobjField
		''            lobjDyn.MoveNext
		''        Wend
		''        lrecReport.MoveFirst
		''    End If
		''    lobjDyn.Close
		''    Set lobjDyn = Nothing
		''
		''    Set GetOracleRecordset = lrecReport
		''    '-se cierra la conexión con la base de datos.
		''    lobjDB.Close
		''    Set lobjDB = Nothing
		''    Set lobjSession = Nothing
		''    Exit Function
		''
		''ConnectionError:
		''    Set lobjDB = Nothing
		''    Set lobjSession = Nothing
		''    Err.Raise 9000, lstrErrSource, "Error de conexión con el Servidor de Datos." & vbCrLf & Err.Description
		''ParametersError:
		''    lobjDB.Close
		''    Set lobjDB = Nothing
		''    Set lobjSession = Nothing
		''    Err.Raise 9001, lstrErrSource, "Error en los valores de los parámetros. Verifique el separador." & vbCrLf & Err.Description
		''ExecuteStoreProcedureError:
		''    Set lobjDyn = Nothing
		''    lobjDB.Close
		''    Set lobjDB = Nothing
		''    Set lobjSession = Nothing
		''    Err.Raise 9002, lstrErrSource, "Error ejecutando el procedimiento almacenado." & vbCrLf & Err.Description
		''BuilADODBRecordsetError:
		''    lobjDyn.Close
		''    Set lobjDyn = Nothing
		''    lobjDB.Close
		''    Set lobjDB = Nothing
		''    Set lobjSession = Nothing
		''    Set lrecReport = Nothing
		''    Err.Raise 9003, lstrErrSource, "Error creando el recordset desconectado." & vbCrLf & Err.Description
	End Function
	
	'%GetSpecifications: Esta función busca en el documento XML las especificaciones
	'%para un reporte determinado por sReportID y genera con estas especificaciones
	'un recordset desconectado de tipo ADODB.Recordset
	Public Function GetSpecifications(ByVal sSpecificationFile As String, ByVal sReportID As String) As ADODB.Recordset
		Dim lstrErrSource As String
		Dim lrecSpecifications As ADODB.Recordset
		
		On Error GoTo OpenFileError
		
		lstrErrSource = "Reportes.CReport.GetSpecifications"
		lrecSpecifications = New ADODB.Recordset
		lrecSpecifications.CursorLocation = 3
		lrecSpecifications.CursorType = 3
		lrecSpecifications.LockType = 4
		lrecSpecifications.Open(sSpecificationFile)
		
		On Error GoTo FilterFileError
		
		lrecSpecifications.Filter = "IdEspecificacion = '" & sReportID & "'"
		If lrecSpecifications.EOF Then
			On Error GoTo 0
			'UPGRADE_NOTE: Object lrecSpecifications may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecSpecifications = Nothing
			Err.Raise(9004, lstrErrSource, "No existen especificaciones para el reporte con Id = " & sReportID)
		End If
		GetSpecifications = lrecSpecifications
		'UPGRADE_NOTE: Object lrecSpecifications may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecSpecifications = Nothing
		
		Exit Function
OpenFileError: 
		'UPGRADE_NOTE: Object lrecSpecifications may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecSpecifications = Nothing
		Err.Raise(9003, lstrErrSource, "Error obteniendo las especificaciones." & vbCrLf & Err.Description)
FilterFileError: 
		'UPGRADE_NOTE: Object lrecSpecifications may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecSpecifications = Nothing
		Err.Raise(9004, lstrErrSource, "No existen especificaciones para el reporte con Id = " & sReportID)
	End Function
	
	'%TranslateWebPath: Esta función traduce una ruta en formato URL a
	'%ruta física en disco.
	Public Function TranslateWebPath(ByVal sWebPath As String) As String
		Dim lintLen As Integer
		Dim lstrPath As String
		
		lstrPath = sWebPath
		While (Right(lstrPath, 1) <> "\" And Len(lstrPath) <> 0)
			lintLen = Len(lstrPath) - 1
			lstrPath = Left(lstrPath, lintLen)
		End While
		TranslateWebPath = lstrPath
	End Function
	
	'%ValidationsFunctions: Esta función genera un código en lenguaje Java Script para
	'%validar formatos de fechas y numéricos.
	Public Function ValidationsFunctions() As String
		Dim lstrCodigoJS As String
		
		lstrCodigoJS = "   function validdate(datetocheck)" & vbCrLf & "   {" & vbCrLf & "       var err=0;" & vbCrLf & "       var psj=0;" & vbCrLf & "" & vbCrLf & "       if (datetocheck.length == 10)" & vbCrLf & "       {" & vbCrLf & "           b = datetocheck.substring(3, 5)// month" & vbCrLf & "           c = datetocheck.substring(2, 3)// '/'" & vbCrLf & "           d = datetocheck.substring(0, 2)// day" & vbCrLf & "           e = datetocheck.substring(5, 6)// '/'" & vbCrLf & "           f = datetocheck.substring(6, 10)// year" & vbCrLf & "" & vbCrLf & "           if (!IsNumeric(b) || b<1 || b>12) err = 1" & vbCrLf & "           if ((c != '/') && (c != '-')) err = 1" & vbCrLf & "           if (!IsNumeric(d) || d<1 || d>31) err = 1" & vbCrLf & "           if ((e != '/') && (e != '-')) err = 1" & vbCrLf & "           if (!IsNumeric(f) || f<0 || f>9999) err = 1" & vbCrLf
		lstrCodigoJS = lstrCodigoJS & "" & vbCrLf & "           if (b==4 || b==6 || b==9 || b==11)" & vbCrLf & "           {" & vbCrLf & "               if (d==31) err=1" & vbCrLf & "           }" & vbCrLf & "" & vbCrLf & "           if (b==2)" & vbCrLf & "           {" & vbCrLf & "               // feb" & vbCrLf & "               var g=parseInt(f/4)" & vbCrLf & "               if (isNaN(g))" & vbCrLf & "               {" & vbCrLf & "                   err=1" & vbCrLf & "               }" & vbCrLf & "" & vbCrLf
		lstrCodigoJS = lstrCodigoJS & "               if (d>29) err=1" & vbCrLf & "               if (d==29 && ((f/4)!=parseInt(f/4))) err=1" & vbCrLf & "           }" & vbCrLf & "       }" & vbCrLf & "       else" & vbCrLf & "           err=1;" & vbCrLf & "" & vbCrLf & "       if (err==0)" & vbCrLf & "           return (true)" & vbCrLf & "       else" & vbCrLf & "           return (false);" & vbCrLf & "   }" & vbCrLf
		lstrCodigoJS = lstrCodigoJS & "" & vbCrLf & "   function IsNumeric(sText)" & vbCrLf & "   {" & vbCrLf & "      var ValidChars = '0123456789.';" & vbCrLf & "      var IsNumber=true;" & vbCrLf & "      var Char;" & vbCrLf & "" & vbCrLf & "" & vbCrLf
		lstrCodigoJS = lstrCodigoJS & "      for (i = 0; i < sText.length && IsNumber == true; i++)" & vbCrLf & "         {" & vbCrLf & "         Char = sText.charAt(i);" & vbCrLf & "         if (ValidChars.indexOf(Char) == -1)" & vbCrLf & "            {" & vbCrLf & "            IsNumber = false;" & vbCrLf & "            }" & vbCrLf & "         }" & vbCrLf & "      return IsNumber;" & vbCrLf & "   }"
		
		'lstrCodigoJS =         lstrCodigoJS & "alert"
		ValidationsFunctions = lstrCodigoJS
		
		
		
	End Function
	
	'%GetHtmlForm: Esta función genera a partir del documento XML de especificaciones
	'%el código HTML con el formulario y las validaciones necesarias para tomar los parámetros
	'%y ejecutar el reporte.
	'%+Los parámetros de base de datos son opcionales ya que estos son utilizados para ejecutar
	'%+un procedimiento almacenado y a partir del resultado llenar la lista de un control HTML
	'%+de tipo <SELECT>, en caso de que en el formulario nof exista ningún control de este tipo}
	'%+estos pueden ser omitidos.
	Public Function GetHtmlForm(ByVal sSpecificationFile As String, ByVal sValidationFile As String, ByVal sReportID As String, ByVal ReportServer As String, Optional ByVal sDatabase As String = "", Optional ByVal sUser As String = "", Optional ByVal sPassword As String = "") As String
		Dim lstrHTML As String
		Dim lstrErrSource As String
		Dim lrecSpecifications As ADODB.Recordset
		Dim lrecSelect As ADODB.Recordset
		Dim lstrCodigoJS As String
		Dim lstrButtonClass As String
		Dim lstrValidFormatIF As String
		Dim lstrValidFormatELSE As String
		Dim lstrTipoSalida As String
		Dim lclsValues As eFunctions.Values
		Dim lstr1dmsysdate As Date
		Dim lstrlastsysdate As Date
		Dim lstrPrName As String
		Dim lstrPrViewType As String
		Dim lstrPrDefault As String
		Dim lstrPrOptional As String
		Dim lstrprTooltips As String
		Dim lstrprASPVal As String
		Dim lstrprJSOnChange As String
		Dim lstrprJSFile As String
		Dim lstrprTypeList As Short
		Dim lstrprList As String
		
		Dim lblnEOF As Boolean
		Dim lblnDoValidation As Boolean
		
		Dim sBranchName As String
		Dim sBranchAlias As String
		Dim sBranchValue As String
		Dim sBranchPresentation As String
		Dim sBranchJSOnchange As String
		Dim sTypeList As String
		Dim sList As String
		
		On Error GoTo SpecificationFileError
		
		lstrErrSource = "Reportes.CReport.GetHtmlForm"
		
		lrecSpecifications = New ADODB.Recordset
		
		lrecSpecifications.Open(sSpecificationFile)
		lrecSpecifications.Filter = "IdEspecificacion = '" & sReportID & "'"
		
		lstrValidFormatIF = ""
		lstrValidFormatELSE = ""
		lstrHTML = "<FORM name=" & Chr(34) & "frmReport" & Chr(34) & " method=" & Chr(34) & "post" & Chr(34) & " action=" & Chr(34) & "http:\\" & ReportServer & Chr(34) & "> " & vbCrLf & "<TABLE>" & vbCrLf
		
		On Error GoTo SpecificationFieldError
		
		lclsValues = New eFunctions.Values
		
		lclsValues.sCodisplPage = sReportID
		
		Dim lDate As Integer
		While Not lrecSpecifications.EOF
			
			lstrTipoSalida = lrecSpecifications.FieldToClass("esTipoSalida").Value
			
			If lrecSpecifications.FieldToClass("prVisible").Value = 1 Then
				lstrHTML = lstrHTML & "<TR> " & vbCrLf
				With lrecSpecifications
					lstrPrName = .FieldToClass("prNombre").Value
					lstrPrViewType = .FieldToClass("prTipoPresentacion").Value
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If IsDbNull(.FieldToClass("prDefecto").Value) Then
						lstrPrDefault = ""
					Else
						lstrPrDefault = .FieldToClass("prDefecto").Value
					End If
					
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If IsDbNull(.FieldToClass("prOpcional").Value) Then
						lstrPrOptional = "1"
					Else
						lstrPrOptional = .FieldToClass("prOpcional").Value
					End If
					
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If IsDbNull(.FieldToClass("prTooltips").Value) Then
						lstrprTooltips = .FieldToClass("prPresentacion").Value
					Else
						lstrprTooltips = .FieldToClass("prTooltips").Value
					End If
					
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If IsDbNull(.FieldToClass("prASPVal").Value) Then
						lstrprASPVal = ""
					Else
						lstrprASPVal = .FieldToClass("prASPVal").Value
					End If
					
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If IsDbNull(.FieldToClass("prJSOnChange").Value) Then
						lstrprJSOnChange = ""
					Else
						lstrprJSOnChange = .FieldToClass("prJSOnChange").Value
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If IsDbNull(.FieldToClass("prJSFile").Value) Then
						lstrprJSFile = ""
					Else
						lstrprJSFile = lrecSpecifications.FieldToClass("prJSFile").Value
					End If
					
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If IsDbNull(.FieldToClass("prTypeList").Value) Then
						lstrprTypeList = 0
					Else
						lstrprTypeList = lrecSpecifications.FieldToClass("prTypeList").Value
					End If
					
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If IsDbNull(.FieldToClass("prList").Value) Then
						lstrprList = ""
					Else
						lstrprList = lrecSpecifications.FieldToClass("prList").Value
					End If
					
				End With
				
				'+Se genera validacion javascript para todos los campos,
				'+excepto de los de listas de valores fijas los cuales siempre cargan valores
				lblnDoValidation = True
				If lstrPrViewType <> "branch" And lstrPrViewType <> "product" And lstrPrViewType <> "option" And lstrPrViewType <> "check" Then
					If lrecSpecifications.FieldToClass("prClass").Value > String.Empty Then
						lstrHTML = lstrHTML & "   <TD COLSPAN=2 CLASS=" & "HighLighted" & " ><LABEL ALIGN=" & "LEFT" & ">" & lrecSpecifications.FieldToClass("prPresentacion").Value & "</LABEL></TD> " & vbCrLf
						lstrHTML = lstrHTML & "   </TR><TR><TD COLSPAN=2 CLASS=HORLINE></TD>"
					Else
						lstrHTML = lstrHTML & "   <TD><LABEL>" & lrecSpecifications.FieldToClass("prPresentacion").Value & "</LABEL></TD> " & vbCrLf
					End If
				End If
				
				Select Case lstrPrViewType
					
					'+Control de clientes
					Case "client"
						lstrHTML = lstrHTML & "<TD>" & lclsValues.ClientControl(lstrPrName, Format(lstrPrDefault, New String("0", 14)),  , lstrprTooltips, lstrprJSOnChange) & "</TD>"
						
						'+Control de Campo Oculto
					Case "Hidden"
						lstrHTML = lstrHTML & "<TD>" & lclsValues.HiddenControl(lstrPrName, lstrPrDefault) & "</TD>"
						
						'+Control de ingreso simple
					Case "input"
						
						'+Se agrega para el manejo de meses y años NO QUITAR
						If lstrPrDefault = "sysdate" Then
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							If Not IsDbNull(lrecSpecifications.FieldToClass("prFormato").Value) Then
								lstrPrDefault = CStr(CInt(Format(Today, lrecSpecifications.FieldToClass("prFormato").Value)))
							End If
						Else
							lstrPrDefault = lstrPrDefault
						End If
						
						If lrecSpecifications.FieldToClass("prNumeric").Value = "1" Then
							
							'+ Campo de ingreso numerico
							lstrHTML = lstrHTML & "<TD>" & lclsValues.NumericControl(lstrPrName, lrecSpecifications.FieldToClass("prLargo").Value, lstrPrDefault, lstrPrOptional = "0", lstrprTooltips,  ,  ,  ,  ,  , lstrprJSOnChange) & "</TD>"
						Else
							'+ Campo de ingreso de texto
							lstrHTML = lstrHTML & "<TD>" & lclsValues.TextControl(lstrPrName, lrecSpecifications.FieldToClass("prLargo").Value, lstrPrDefault, lstrPrOptional = "0", lstrprTooltips,  ,  ,  , lstrprJSOnChange) & "</TD>"
						End If
						
						'+Lista a través de procedimiento
					Case "select"
						lrecSelect = GetOracleRecordset(sDatabase, sUser, sPassword, lrecSpecifications.FieldToClass("prProcAlmacenado").Value, lrecSpecifications.FieldToClass("prNombreCursor").Value, "", "")
						'+Si encuentra datos en lista se crea seleccion
						If Not lrecSelect Is Nothing Then
							lstrHTML = lstrHTML & "<TD> <SELECT name=" & Chr(34) & lstrPrName & Chr(34) & ">" & vbCrLf
							While Not lrecSelect.EOF
								lstrHTML = lstrHTML & "<OPTION value=" & Chr(34) & lrecSelect.FieldToClass(0).Value & Chr(34) & ">" & lrecSelect.FieldToClass(1).Value & "</OPTION>" & vbCrLf
								lrecSelect.MoveNext()
							End While
							lstrHTML = lstrHTML & "</SELECT> </TD>"
							
							'+No requiere validacion javascript
							lblnDoValidation = False
							
						Else
							'+Si no encuentra, se crea un campo de ingreso
							'+Se deja el ingreso de tipo input para que entre a validar
							'+si es requerido en el select case sgte
							lstrPrViewType = "input"
							lstrHTML = lstrHTML & "<TD>" & lclsValues.TextControl(lstrPrName, lrecSpecifications.FieldToClass("prLargo").Value, lstrPrDefault, lstrPrOptional = "0",  ,  ,  ,  , lstrprJSOnChange) & "</TD>"
						End If
						'UPGRADE_NOTE: Object lrecSelect may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lrecSelect = Nothing
						
						'+Control de valores posibles de tipo combo
					Case "list"
						lclsValues.List = lstrprList
						lclsValues.TypeList = lstrprTypeList
						lstrHTML = lstrHTML & "<TD>" & lclsValues.PossiblesValues(lstrPrName, lrecSpecifications.FieldToClass("prProcAlmacenado").Value, eFunctions.Values.eValuesType.clngComboType, lstrPrDefault, False,  ,  ,  ,  , lstrprJSOnChange,  ,  , lstrprTooltips) & "</TD>"
						lblnDoValidation = True
						
						'+Control de valores posibles de tipo ventana
					Case "window"
						If lrecSpecifications.FieldToClass("prNombre").Value = "P_COD_OFICINA" Then
							lclsValues.Parameters.Add("P_COD_OFICINA", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							lclsValues.Parameters.Add("P_COD_AGENCIA", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							lclsValues.Parameters.ReturnValue("nBran_off",  ,  , True)
							lstrHTML = lstrHTML & "<TD>" & lclsValues.PossiblesValues(lstrPrName, lrecSpecifications.FieldToClass("prProcAlmacenado").Value, eFunctions.Values.eValuesType.clngWindowType, lstrPrDefault, True,  ,  ,  ,  , lstrprJSOnChange,  ,  , lstrprTooltips) & "</TD>"
						Else
							If lrecSpecifications.FieldToClass("prNombre").Value = "P_COD_AGENCIA" Then
								lclsValues.Parameters.Add("P_COD_OFICINA", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
								lclsValues.Parameters.Add("P_COD_AGENCIA", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
								lclsValues.Parameters.ReturnValue("nBran_off",  ,  , True)
								lclsValues.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
								lclsValues.Parameters.ReturnValue("sDesAgen",  ,  , True)
								lstrHTML = lstrHTML & "<TD>" & lclsValues.PossiblesValues(lstrPrName, lrecSpecifications.FieldToClass("prProcAlmacenado").Value, eFunctions.Values.eValuesType.clngWindowType, lstrPrDefault, True,  ,  ,  ,  , lstrprJSOnChange,  ,  , lstrprTooltips) & "</TD>"
							Else
								If lrecSpecifications.FieldToClass("prNombre").Value = "P_NUM_INTERMED" Then
									lclsValues.Parameters.Add("P_COD_SUCURSAL", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
									lclsValues.Parameters.Add("P_COD_OFICINA", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
									lclsValues.Parameters.Add("P_COD_AGENCIA", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
									lclsValues.Parameters.ReturnValue("nOffice",  ,  , True)
									lclsValues.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
									lclsValues.Parameters.ReturnValue("nAgency",  ,  , True)
									lclsValues.Parameters.ReturnValue("sCliename",  ,  , True)
									lstrHTML = lstrHTML & "<TD>" & lclsValues.PossiblesValues(lstrPrName, lrecSpecifications.FieldToClass("prProcAlmacenado").Value, eFunctions.Values.eValuesType.clngWindowType, lstrPrDefault, True,  ,  ,  ,  , lstrprJSOnChange,  ,  , lstrprTooltips) & "</TD>"
								Else
									
									lstrHTML = lstrHTML & "<TD>" & lclsValues.PossiblesValues(lstrPrName, lrecSpecifications.FieldToClass("prProcAlmacenado").Value, eFunctions.Values.eValuesType.clngWindowType, lstrPrDefault, False,  ,  ,  ,  , lstrprJSOnChange,  ,  , lstrprTooltips) & "</TD>"
								End If
							End If
						End If
						lblnDoValidation = True
						
						'+Control de fechas
					Case "calendar"
						If lstrPrDefault = "sysdate" Then
							lstrHTML = lstrHTML & "<TD>" & lclsValues.DateControl(lstrPrName, CStr(Today), lstrPrOptional = "0", lstrprTooltips,  ,  ,  , lstrprJSOnChange) & "</TD>"
						Else
							If lstrPrDefault = "1dmsysdate" Then
								lstr1dmsysdate = CDate(Format("1/" & Month(Today) & "/" & Year(Today), lrecSpecifications.FieldToClass("prFormato").Value))
								lstrHTML = lstrHTML & "<TD>" & lclsValues.DateControl(lstrPrName, CStr(lstr1dmsysdate), lstrPrOptional = "0", lstrprTooltips,  ,  ,  , lstrprJSOnChange) & "</TD>"
							Else
								If lstrPrDefault = "lastsysdate" Then
									lstrlastsysdate = CDate(Format(System.Date.FromOADate(DateSerial(Year(Today), Month(Today) + 1, 1).ToOADate - 1), lrecSpecifications.FieldToClass("prFormato").Value))
									
									lstrHTML = lstrHTML & "<TD>" & lclsValues.DateControl(lstrPrName, CStr(lstrlastsysdate), lstrPrOptional = "0", lstrprTooltips,  ,  ,  , lstrprJSOnChange) & "</TD>"
								Else
									lstrHTML = lstrHTML & "<TD>" & lclsValues.DateControl(lstrPrName, lstrPrDefault, lstrPrOptional = "0", lstrprTooltips,  ,  ,  , lstrprJSOnChange) & "</TD>"
								End If
							End If
						End If
						
						'+Control de ramo. Solo se cargan valores para crear control cuando se crea producto
					Case "branch"
						sBranchName = lstrPrName
						sBranchAlias = lstrprTooltips
						sBranchValue = lstrPrDefault
						sBranchPresentation = lrecSpecifications.FieldToClass("prPresentacion").Value
						sBranchJSOnchange = lstrprJSOnChange
						sTypeList = CStr(lstrprTypeList)
						sList = lstrprList
						lblnDoValidation = False
						
					Case "product"
						If Len(sBranchName) Then
							lstrHTML = lstrHTML & "   <TD><LABEL>" & sBranchPresentation & "</LABEL></TD> " & vbCrLf
							lclsValues.List = sList
							lclsValues.TypeList = CShort(sTypeList)
							lstrHTML = lstrHTML & "<TD>" & lclsValues.BranchControl(sBranchName, sBranchAlias, sBranchValue, lstrPrName,  ,  ,  , sBranchJSOnchange) & "</TD>"
							sBranchName = ""
						End If
						lstrHTML = lstrHTML & "   <TD><LABEL>" & lrecSpecifications.FieldToClass("prPresentacion").Value & "</LABEL></TD> " & vbCrLf
						lclsValues.List = lstrprList
						lclsValues.TypeList = lstrprTypeList
						lstrHTML = lstrHTML & "<TD>" & lclsValues.ProductControl(lstrPrName, lstrprTooltips, sBranchValue,  ,  , lstrPrDefault,  ,  ,  , lstrprJSOnChange) & "</TD>"
						lblnDoValidation = True
						
					Case "option"
						If Len(lstrPrName) > 0 Then
							lstrHTML = lstrHTML & "<TD>" & lclsValues.OptionControl(0, lstrPrName, lrecSpecifications.FieldToClass("prPresentacion").Value, lstrPrDefault, lstrPrDefault, lstrprJSOnChange) & "</TD>"
						End If
					Case "check"
						lstrHTML = lstrHTML & "<TD>" & lclsValues.CheckControl(lstrPrName, lrecSpecifications.FieldToClass("prPresentacion").Value, lstrPrDefault, lstrPrDefault) & "</TD>"
				End Select
				
				'+Se crea campo oculto con tipo de validacion ASP
				If lstrprASPVal <> "" Then
					lstrHTML = lstrHTML & vbCrLf & lclsValues.HiddenControl("hddASPVal", lstrprASPVal & "|" & lstrPrName) & vbCrLf
				End If
				
				'+Se procesan script de validacion de tipos de datos y campos requeridos
				If lblnDoValidation Then
					'+Si es obligatorio
					If lstrPrOptional = "0" Then
						If lstrPrViewType = "list" Then
							lstrValidFormatIF = lstrValidFormatIF & "if (frmReport." & lstrPrName & ".value != '0' && frmReport." & lstrPrName & ".value != '')" & vbCrLf
						Else
							lstrValidFormatIF = lstrValidFormatIF & "if (frmReport." & lstrPrName & ".value != '')" & vbCrLf
						End If
						lstrValidFormatELSE = "else alert('El parámetro " & lrecSpecifications.FieldToClass("prPresentacion").Value & " es obligatorio.')" & vbCrLf & lstrValidFormatELSE
						If lrecSpecifications.FieldToClass("prTipoDato").Value = "date" Then
							lstrValidFormatIF = lstrValidFormatIF & "if (validdate(frmReport." & lstrPrName & ".value))" & vbCrLf
							lstrValidFormatELSE = "else alert('El valor del parámetro " & lrecSpecifications.FieldToClass("prPresentacion").Value & " no es una fecha válida.')" & vbCrLf & lstrValidFormatELSE
						ElseIf lrecSpecifications.FieldToClass("prTipoDato").Value = "numeric" Then 
							lstrValidFormatIF = lstrValidFormatIF & "if (IsNumeric(frmReport." & lstrPrName & ".value))" & vbCrLf
							lstrValidFormatELSE = "else alert('El valor del parámetro " & lrecSpecifications.FieldToClass("prPresentacion").Value & " no es número.')" & vbCrLf & lstrValidFormatELSE
						End If
						'+Si es opcional se valida el tipo de dato
					Else
						If lrecSpecifications.FieldToClass("prTipoDato").Value = "date" Then
							lstrValidFormatIF = lstrValidFormatIF & "if ((frmReport." & lstrPrName & ".value == '') || (validdate(frmReport." & lstrPrName & ".value)))" & vbCrLf
							lstrValidFormatELSE = "else alert('El valor del parámetro " & lrecSpecifications.FieldToClass("prPresentacion").Value & " no es una fecha válida.')" & vbCrLf & lstrValidFormatELSE
						ElseIf lrecSpecifications.FieldToClass("prTipoDato").Value = "numeric" Then 
							lstrValidFormatIF = lstrValidFormatIF & "if ((frmReport." & lstrPrName & ".value == '') || (IsNumeric(frmReport." & lstrPrName & ".value)))" & vbCrLf
							lstrValidFormatELSE = "else alert('El valor del parámetro " & lrecSpecifications.FieldToClass("prPresentacion").Value & " no es número.')" & vbCrLf & lstrValidFormatELSE
						End If
					End If
				End If
				
				lstrHTML = lstrHTML & "</TR> " & vbCrLf
				
				'+Si viene archivo que se requiere incluir, se carga
				If Len(lstrprJSFile) Then
					lstrHTML = lstrHTML & "<SCRIPT LANGUAGE='JavaScript' SRC='" & lstrprJSFile & "'></SCRIPT>" & vbCrLf
				End If
			End If
			
			lrecSpecifications.MoveNext()
		End While
		lrecSpecifications.Close()
		
		lstrValidFormatELSE = lstrValidFormatELSE & ";"
		lstrHTML = lstrHTML & "   </TABLE> " & vbCrLf
		lstrHTML = lstrHTML & "   <p> </p> " & vbCrLf & "   <TABLE> " & vbCrLf & "       <TR> " & vbCrLf & "          <TD><INPUT type=" & Chr(34) & "hidden" & Chr(34) & " name=" & Chr(34) & "txtOutputType" & Chr(34) & " value =" & Chr(34) & lstrTipoSalida & Chr(34) & ">" & vbCrLf & "          </TD> " & vbCrLf & "       </TR> " & vbCrLf & "   </TABLE> " & vbCrLf & "</FORM> " & vbCrLf
		lstrHTML = lstrHTML & "<SCRIPT LANGUAGE=javascript> " & vbCrLf & "<!-- " & vbCrLf & "function ValidFormat(){" & vbCrLf & lstrValidFormatIF & "       return true" & vbCrLf & lstrValidFormatELSE & "   return false;" & vbCrLf & "}" & vbCrLf & vbCrLf
		
		lstrHTML = lstrHTML & "function Validation(){" & vbCrLf & "if (ValidFormat())" & vbCrLf
		
		'+Se genera el código de validación Java Script a partir del documento XML de validaciones.
		On Error GoTo ValidationFileError
		
		lrecSpecifications.Open(sValidationFile)
		lrecSpecifications.Filter = "IdEspecificacion = '" & sReportID & "'"
		If Not lrecSpecifications.EOF Then
			lrecSpecifications.MoveFirst()
			lstrCodigoJS = ""
			While Not lrecSpecifications.EOF
				lstrCodigoJS = lstrCodigoJS & "if " & Replace(lrecSpecifications.FieldToClass("vdCodigoJS").Value, "<prm>", "frmReport." & lstrPrName) & vbCrLf & vbTab
				lrecSpecifications.MoveNext()
			End While
			lstrCodigoJS = lstrCodigoJS & "return true" & vbCrLf
			lrecSpecifications.MoveLast()
			While Not lrecSpecifications.BOF
				lstrCodigoJS = lstrCodigoJS & "else alert('" & Replace(lrecSpecifications.FieldToClass("vdMsgError").Value, "<label>", lrecSpecifications.FieldToClass("prPresentacion").Value) & "')" & vbCrLf
				lrecSpecifications.MovePrevious()
			End While
			lrecSpecifications.Close()
			lstrCodigoJS = Mid(lstrCodigoJS, 1, Len(lstrCodigoJS) - 2) & ";"
			lstrCodigoJS = lstrCodigoJS & "return false;" & vbCrLf
		Else
			lstrCodigoJS = "return true;" & vbCrLf
		End If
		lstrHTML = lstrHTML & lstrCodigoJS & vbCrLf & "}" & vbCrLf & ValidationsFunctions() & vbCrLf & "//--> " & vbCrLf & "</SCRIPT> "
		'UPGRADE_NOTE: Object lrecSpecifications may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecSpecifications = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		
		GetHtmlForm = lstrHTML
		Exit Function
SpecificationFileError: 
		'UPGRADE_NOTE: Object lrecSpecifications may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecSpecifications = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		Err.Raise(9003, lstrErrSource, "Error abriendo el archivo de especificaciones." & vbCrLf & Err.Description)
SpecificationFieldError: 
		'UPGRADE_NOTE: Object lrecSpecifications may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecSpecifications = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		Err.Raise(9003, lstrErrSource, "Error creando campo " & lstrPrName & vbCrLf & Err.Description)
ValidationFileError: 
		'UPGRADE_NOTE: Object lrecSpecifications may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecSpecifications = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		Err.Raise(9003, lstrErrSource, "Error abriendo el archivo de validaciones." & vbCrLf & Err.Description)
	End Function
	'%Version: Esta función devuelve la versión del componente ActiveX DLL.
	Public Function Version() As String
		Version = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision
	End Function
End Class






