Option Strict Off
Option Explicit On

Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.HSSF.Extractor
Imports NPOI.SS.UserModel
Imports OfficeOpenXml
Public Class ValInterfaceSeq

	'% Estructura de tabla T_PARAM_INTERFACE
	Public sKey As Integer 'NUMBER(5)                     NOT NULL,
	Public nField As String 'VARCHAR2(40)                  NOT NULL,
	Public sValue As String 'VARCHAR2(5)                   NOT NULL,
	Public nUsercode As Integer 'NUMERIC(22)                   NOT NULL
	Public sDescript As String
	Public bReport As Boolean
    Public mblncheck As Boolean
	Public mblnfile As Boolean

    Public sVirtualdirview As String
    Public sIPremote As String
    Public sDirwork As String
    Public sDirview As String
    Public sDirout As String
    Public sDirentry As String
    Public sDeletetable As String

    '-Mensaje de proceso
    Public sMessage As String
    Public mstrFile As String
    Public mstrFileTxt As String

	'% RETORNO DE POSTGI1405, MANEJO DE MENSAJE "PROCESO OK" O "NO OK"
    Public nExistError As Short


	
	'% insValGI1402_K: Valida los datos introducidos en el Folder
	'-------------------------------------------------------------
    Public Function insValGI1402_K(ByVal sCodispl As String, ByVal nSystem As Integer, ByVal nSheet As Integer, ByVal sFile As String, ByVal nIntertype As Integer, Optional ByVal nFormat As Integer = numNull, Optional ByVal sTable As String = strNull, Optional ByVal nMainAction As Integer = numNull, Optional ByVal sOnLine As String = "2", Optional ByVal sSheet_father As String = "2") As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValGI1402_K_Err

        lclsErrors = New eFunctions.Errors

        '+ Validaci?n del campo "Sistema Externo"
        If nSystem = numNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 700001, , eFunctions.Errors.TextAlign.RigthAling, "Sistema externo")
        End If

        '+ Validaci?n del campo "C?digo de interfaz"
        If nSheet = numNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 700001, , eFunctions.Errors.TextAlign.RigthAling, "C?digo de interfaz")
        End If

        If nMainAction <> 401 Then
            '+ Validaci?n del campo "Nombre del archivo"
            If nIntertype = 1 Then
                If nFormat <> 3 Then
                    If sFile = strNull Then
                        Call lclsErrors.ErrorMessage(sCodispl, 700001, , eFunctions.Errors.TextAlign.RigthAling, "Archivo")
                    End If
                End If
            Else
                If nFormat = 3 Then
                    If sTable = strNull Then
                        Call lclsErrors.ErrorMessage(sCodispl, 700001, , eFunctions.Errors.TextAlign.RigthAling, "Tabla")
                    End If
                End If
            End If
        Else
            If sOnLine = "2" Or sSheet_father = "1" Then
                Call lclsErrors.ErrorMessage(sCodispl, 700047, , eFunctions.Errors.TextAlign.RigthAling, "Tabla")
            End If
        End If

        insValGI1402_K = lclsErrors.Confirm

insValGI1402_K_Err:
        If Err.Number Then
            insValGI1402_K = lclsErrors.Confirm & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function
	
	'% GetParam: Trae parametros por cada campo definido para su construccion
    Public Function GetParam(ByRef nSheet As Object) As String
        Dim lclsField As Object
        Dim lcolField As Object
        Dim lclsCtrol_date As eGeneral.Ctrol_date

        '% Columnas de definicion de campos "Parametro"
        Dim sFieldDesc As String        'Nombre Campo
        Dim nObjtype As Integer         'Objeto de despliegue
        Dim sValue As String            'Valor por defecto
        Dim nFieldLarge As Integer      'Tama?o del campo
        Dim sFieldCommen As String      'Comentario del campo
        Dim sValueslist As String       'Lista de Posibles Valores
        Dim nDataType As Integer        'Tipo de Datos del Campo
        Dim sObligatory As String       'Indica si el campo es requerido: 1-Afirmativo, 2-Negativo
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim nType As Short
        Dim sColumnName As String

        Dim sFieldProducto As String    'Nombre del Campo

        lcolField = New FieldSheets
        lclsField = New FieldSheet
        GetParam = "<TABLE WIDTH=""80%""><TR>"

        If lcolField.Find(nSheet, 3) Then
            For Each lclsField In lcolField
                If (lclsField.nObjtype = 6) Then
                    sFieldProducto = lclsField.sColumnName
                End If
            Next lclsField
        End If

        mblncheck = False
        mblnfile = False

        For Each lclsField In lcolField
            i = i + 1
            j = j + 1
            sFieldDesc = lclsField.sFieldDesc
            sColumnName = lclsField.sColumnName
            nObjtype = lclsField.nObjtype
            If (nObjtype = 10) And (j = 1) Then  'File
                GetParam = GetParam.Substring(0, GetParam.Length - 4)
            End If
            sObligatory = lclsField.sObligatory
            If (lclsField.sValue = "") And (lclsField.sValueRutine <> "") Then
                sValue = lclsField.sValueRutine
            Else
                sValue = lclsField.sValue
            End If

            '+ Para las interfaces contables se muestra la ?ltima fecha contable m?s un d?a
            If (nSheet = 951 Or nSheet = 952 Or nSheet = 953 Or nSheet = 1102 Or nSheet = 1110 Or nSheet = 1139) And lclsField.sColumnName = "DPOSTED" Then
                lclsCtrol_date = New eGeneral.Ctrol_date

                Select Case nSheet
                    Case 951
                        nType = 4
                    Case 952
                        nType = 3
                    Case 953
                        nType = 1
                    Case 1102
                        nType = 2
                    Case 1110
                        nType = 3
                    Case 1139
                        nType = 6
                End Select

                If lclsCtrol_date.Find(nType) Then
                    sValue = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, lclsCtrol_date.dEffecdate))
                End If
                'UPGRADE_NOTE: Object lclsCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsCtrol_date = Nothing
            End If

            nFieldLarge = lclsField.nFieldLarge
            sFieldCommen = lclsField.sFieldCommen
            sValueslist = lclsField.sValueslist
            nDataType = lclsField.nDataType
            GetParam = GetParam & GenParam(sColumnName, sFieldDesc, nObjtype, sValue, nFieldLarge, sFieldCommen, sValueslist, nDataType, sFieldProducto, sObligatory, lclsField.nDecimal, nSheet)
            If nObjtype <> 10 Then  'File
                If i = 2 Then
                    i = 0
                    GetParam = GetParam & "</TR>"
                End If
            End If
        Next lclsField

        If Not mblnfile Then  'File
            If i = 1 Then
                GetParam = GetParam & "</TR>"
            End If
        End If
        GetParam = GetParam & "</TABLE>"

        If mblncheck Then
            GetParam = GetParam & "<SCRIPT> function Onchecked(Field){ "
            GetParam = GetParam & " if(Field.checked) "
            GetParam = GetParam & " self.document.forms[0].elements[Field.name + 'hdd'].value=1; "
            GetParam = GetParam & " else "
            GetParam = GetParam & " self.document.forms[0].elements[Field.name + 'hdd'].value=2; "
            GetParam = GetParam & " } </SCRIPT>"
        End If

        If mblnfile Then
            GetParam = GetParam & "<SCRIPT> function insSelectFile(Field){ "
            GetParam = GetParam & " with (self.document.forms[0]) { "
            GetParam = GetParam & " var fullPath = Field.value; "
            GetParam = GetParam & " var filename; "
            GetParam = GetParam & " if (fullPath) { "
            GetParam = GetParam & " var startIndex = (fullPath.indexOf('\\') >= 0 ? fullPath.lastIndexOf('\\') : fullPath.lastIndexOf('/')); "
            GetParam = GetParam & " var filename = fullPath.substring(startIndex); "
            GetParam = GetParam & " if (filename.indexOf('\\') === 0 || filename.indexOf('/') === 0) { "
            GetParam = GetParam & " filename = filename.substring(1); } "
            GetParam = GetParam & " self.document.forms[0].elements[Field.name + 'tctFile'].value= filename; "
            GetParam = GetParam & " } "
            GetParam = GetParam & " } "
            GetParam = GetParam & " } </SCRIPT>"
        End If

        'UPGRADE_NOTE: Object lcolField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolField = Nothing
        'UPGRADE_NOTE: Object lclsField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsField = Nothing
    End Function
	
    '% GenParam: Construye cada campo con los parametros necesarios
    Public Function GenParam(ByRef sColumnName As String, ByRef sFieldDesc As String, ByRef nObjtype As Integer, ByRef sValue As String, ByRef nFieldLarge As Integer, ByRef sFieldCommen As String, ByRef sValueslist As String, ByRef nDataType As Integer, Optional ByRef sFieldComple As String = "", Optional ByRef sObligatory As String = "", Optional ByVal nDecimal As Long = 0, Optional ByVal nSheet As Long = 0, Optional ByVal nTransaction As Integer = 0) As String
        Dim sFielDescAux As String
        Dim lintDecimal As Long

        Dim lObj As eFunctions.Values
        lObj = New eFunctions.Values

        If nObjtype = 10 Then  'File
            GenParam = "<TR>"
            GenParam = GenParam & "<TD><LABEL>"
        Else
            GenParam = "<TD><LABEL>"
        End If
        GenParam = GenParam & sFieldDesc
        GenParam = GenParam & "</LABEL></TD>"

        sFielDescAux = ""
        sFielDescAux = Replace(sFieldDesc, " ", "")
        'inicio construccion de string del campo
        GenParam = GenParam & "<TD>"
        lintDecimal = nDecimal
        If lintDecimal = numNull Then
            lintDecimal = 0
        End If

        lObj.sCodisplPage = "INT" & Str(nSheet)
        If nObjtype = 1 Then 'Edicion
            If (nDataType = 4) Then 'tipodato=4,num se crea como numericcontrol con la cantidad de decimales definidos
                GenParam = GenParam & lObj.NumericControl(sColumnName, nFieldLarge, sValue, , sFieldCommen, , lintDecimal, , , , "if(typeof ChangeINT" & Trim(Str(nSheet)) & " == ""function"")" & "{ ChangeINT" & Trim(Str(nSheet)) & "(this);}", IIf(nTransaction = 8, True, False))
            ElseIf (nDataType = 5 Or nDataType = 6) Then  '5,6 int,smalint, se crea como numericcontrol sin decimales
                GenParam = GenParam & lObj.NumericControl(sColumnName, nFieldLarge, sValue, , sFieldCommen, , , , , , "if(typeof ChangeINT" & Trim(Str(nSheet)) & " == ""function"")" & "{ ChangeINT" & Trim(Str(nSheet)) & "(this);}", IIf(nTransaction = 8, True, False))
            ElseIf (nDataType = 1 Or nDataType = 2) Then  'si tipodato=1,2 bit,char, se crea como texto simple
                GenParam = GenParam & lObj.TextControl(sColumnName, nFieldLarge, sValue, , sFieldCommen, , , , "if(typeof ChangeINT" & Trim(Str(nSheet)) & " == ""function"")" & "{ ChangeINT" & Trim(Str(nSheet)) & "(this);}", IIf(nTransaction = 8, True, False))
            Else 'si tipodato = 3 datetime
                GenParam = GenParam & lObj.DateControl(sColumnName, sValue, , , , , , "if(typeof ChangeINT" & Trim(Str(nSheet)) & " == ""function"")" & "{ ChangeINT" & Trim(Str(nSheet)) & "(this);}", IIf(nTransaction = 8, True, False))
            End If
        ElseIf nObjtype = 2 Then  'ComboBox
            GenParam = GenParam & lObj.PossiblesValues(sColumnName, sValueslist, eFunctions.Values.eValuesType.clngComboType, sValue, , , , , , "if(typeof ChangeINT" & Trim(Str(nSheet)) & " == ""function"")" & "{ ChangeINT" & Trim(Str(nSheet)) & "(this);}", IIf(nTransaction = 8, True, False), , sFieldCommen, , 14)
        ElseIf nObjtype = 3 Then  'Possibles Values
            GenParam = GenParam & lObj.PossiblesValues(sColumnName, sValueslist, eFunctions.Values.eValuesType.clngWindowType, sValue, , , , , , "if(typeof ChangeINT" & Trim(Str(nSheet)) & " == ""function"")" & "{ ChangeINT" & Trim(Str(nSheet)) & "(this);}", IIf(nTransaction = 8, True, False), , sFieldCommen, , 14)
        ElseIf nObjtype = 4 Then  'Fecha
            GenParam = GenParam & lObj.DateControl(sColumnName, sValue, , , , , , "if(typeof ChangeINT" & Trim(Str(nSheet)) & " == ""function"")" & "{ ChangeINT" & Trim(Str(nSheet)) & "(this);}", IIf(nTransaction = 8, True, False))
        ElseIf nObjtype = 5 Then  'Ramo
            GenParam = GenParam & lObj.BranchControl(sColumnName, "Ramos de Seguros", sValue, sFieldComple, , , , "if(typeof ChangeINT" & Trim(Str(nSheet)) & " == ""function"")" & "{ ChangeINT" & Trim(Str(nSheet)) & "(this);}", IIf(nTransaction = 8, True, False))
        ElseIf nObjtype = 6 Then  'Producto
            GenParam = GenParam & lObj.ProductControl(sColumnName, "Productos del ramo", , , , sValue, , , , "if(typeof ChangeINT" & Trim(Str(nSheet)) & " == ""function"")" & "{ ChangeINT" & Trim(Str(nSheet)) & "(this);}", )
        ElseIf nObjtype = 7 Then  'Cliente
            GenParam = GenParam & lObj.ClientControl(sColumnName, sValue, , , , IIf(nTransaction = 8, True, False))
        ElseIf nObjtype = 8 Then  'Check
            GenParam = GenParam & lObj.CheckControl(sColumnName, "", sValue, sValue, "Onchecked(this)", IIf(nTransaction = 8, True, False)) + "</TD>"
            GenParam = GenParam & "<TD>" & lObj.HiddenControl(sColumnName & "hdd", sValue)
            mblncheck = True
        ElseIf nObjtype = 9 Then  'Combo manual            
            GenParam = GenParam & lObj.ComboControl(sColumnName, sValueslist, sValue, IIf(sObligatory = "1", False, True), , , "if(typeof ChangeINT" & Trim(Str(nSheet)) & " == ""function"")" & "{ ChangeINT" & Trim(Str(nSheet)) & "(this);}", IIf(nTransaction = 8, True, False))
        ElseIf nObjtype = 10 Then  'File
            GenParam = GenParam & lObj.FileControl(sColumnName, nFieldLarge, , False, , "insSelectFile(this);") + "</TD>"
            GenParam = GenParam & "<TD>" & lObj.HiddenControl(sColumnName & "tctFile", sValue) + "</TD>"
            GenParam = GenParam & "</TR>"
            mblnfile = True
        End If
        If nObjtype <> 10 Then  'File
            GenParam = GenParam & "</TD>"
        End If

        'UPGRADE_NOTE: Object lObj may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lObj = Nothing
    End Function

	'% CreT_Param_Interface: Guarda los valores de los parametros dinamicos.
	Public Function CreT_Param_Interface(ByVal sKey As String, ByVal nField As Integer, ByVal sValue As String, ByVal nUsercode As Integer) As Boolean
		
		Dim lrecCreT_Param_Interface As eRemoteDB.Execute
		
		On Error GoTo CreT_Param_Interface_Err
		lrecCreT_Param_Interface = New eRemoteDB.Execute
		
		With lrecCreT_Param_Interface
			.StoredProcedure = "CreT_Param_Interface"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nField", nField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValue", sValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 100, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			CreT_Param_Interface = .Run(False)
		End With
		
CreT_Param_Interface_Err: 
		If Err.Number Then
			CreT_Param_Interface = False
		End If
		'UPGRADE_NOTE: Object lrecCreT_Param_Interface may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreT_Param_Interface = Nothing
		On Error GoTo 0
    End Function

    '%insTransformationExcel(). Transforma el archivo excel para utilizar solo los valores y deshabilita las formulas
    Public Function insTransformationExcel(ByVal sFile As String, Optional ByVal sSeparator As String = strNull) As Boolean
        'Dim mvarSalidaExcel As Microsoft.Office.Interop.Excel.Application
        Dim lclsvalue As eFunctions.Values
        Dim lstrFileName As String
        Dim lintExist As Integer
        Dim lstrFile As String
        Dim lintlength As Integer
        Dim lstrFiledeltxt As String
        Dim lintFileNum As Integer
        Dim lstrRow As String
        Dim lblnContinue As Boolean
        Dim sExtension As String
        Dim lintExist2 As Integer
        Dim lngFile As Long = 0

        On Error GoTo insTransformationExcel_Err

        lintExist = InStr(1, UCase(sFile), ".XLS")
        If lintExist > 0 Then
            lstrFile = Mid(sFile, 1, lintExist - 1)
        Else
            lstrFile = sFile
        End If

        lclsvalue = New eFunctions.Values
        On Error Resume Next

        lstrFileName = UCase(lclsvalue.insGetSetting("MASSIVELOAD", String.Empty, "PATHS"))
        If lstrFileName = String.Empty Then
            lstrFileName = UCase(lclsvalue.insGetSetting("MASSIVELOAD", String.Empty, "Config"))
        End If

        'UPGRADE_NOTE: Object lclsvalue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsvalue = Nothing

        On Error GoTo insTransformationExcel_Err

        lintlength = Len(lstrFileName)
        If Mid(lstrFileName, lintlength, 1) <> "\" Then
            lstrFileName = lstrFileName & "\"
        End If
        sExtension = ".XLS"

        lintExist2 = InStr(1, UCase(sFile), ".XLSX")
        If lintExist2 > 0 Then
            sExtension = ".XLSX"
        End If

        mstrFileTxt = Trim(lstrFile) & ".TXT"
        sMessage &= "7_"
        If lintExist2 <= 0 Then
            Dim ALines() As String
            Dim sLine As String = String.Empty
            Dim sSheet As String = String.Empty

            Using file As New FileStream(lstrFileName & Trim(lstrFile) & sExtension, FileMode.Open, FileAccess.Read)
                Dim hssWorkbook As HSSFWorkbook
                hssWorkbook = New HSSFWorkbook(file)
                Dim hssExtractor As New ExcelExtractor(hssWorkbook)
                hssExtractor.IncludeBlankCells = True
                hssExtractor.FormulasNotResults = True
                hssExtractor.IncludeSheetNames = False
                sSheet = hssExtractor.Text
            End Using

            sMessage &= "8_"
            lngFile = FreeFile()
            ALines = sSheet.Split(Chr(10))
            FileOpen(lngFile, lstrFileName & Trim(lstrFile) & ".TXT", OpenMode.Append)
            For lintline As Integer = 0 To ALines.Length - 1
                sLine = ALines(lintline)
                If String.IsNullOrEmpty(sLine) And lintline = ALines.Length - 1 Then
                    Exit For
                End If
                PrintLine(lngFile, sLine)
            Next
            FileClose(lngFile)
            sMessage &= "9_"
        Else
            Dim package = New ExcelPackage
            package = New ExcelPackage(New FileInfo(lstrFileName & Trim(lstrFile) & sExtension))
            Dim workSheet As ExcelWorksheet = package.Workbook.Worksheets(1)

            sMessage &= "8_"
            lngFile = FreeFile()
            FileOpen(lngFile, lstrFileName & Trim(lstrFile) & ".TXT", OpenMode.Append)
            For rowNum As Integer = 1 To workSheet.Cells("A:A").Count()
                Dim line As String = String.Empty
                For colNum As Integer = 1 To workSheet.Dimension.End.Column
                    line = line & workSheet.Cells(rowNum, colNum).Text & Chr(9)
                Next

                line = Mid(line, 1, (Len(line) - 1))
                PrintLine(lngFile, line)
            Next
            FileClose(lngFile)
            sMessage &= "9_"
        End If

        'mvarSalidaExcel = New Microsoft.Office.Interop.Excel.Application
        'mvarSalidaExcel.DisplayAlerts = False
        'sMessage &= "7_"
        'mvarSalidaExcel.Workbooks.Open(lstrFileName & Trim(lstrFile) & sExtension, 0, True, , "insudb")
        'sMessage &= "8_"
        ''+Se guarda el archivo como texto separador por tabuladores
        'If sSeparator = ";" Then
        '    mvarSalidaExcel.ActiveWorkbook.SaveAs(lstrFileName & Trim(lstrFile) & ".csv", 6, False)
        '    mvarSalidaExcel.ActiveWorkbook.Close()
        '    mvarSalidaExcel.Quit()
        '    System.IO.File.Move(lstrFileName & Trim(lstrFile) & ".csv", lstrFileName & Trim(lstrFile) & ".txt")
        'Else
        '    mvarSalidaExcel.ActiveWorkbook.SaveAs(lstrFileName & Trim(lstrFile) & ".TXT", Microsoft.Office.Interop.Excel.XlPivotFieldDataType.xlText, False)
        '    mstrFileTxt = Trim(lstrFile) & ".TXT"
        '    mvarSalidaExcel.ActiveWorkbook.Close()
        '    mvarSalidaExcel.Quit()
        'End If
        'sMessage &= "9_"

        insTransformationExcel = True

insTransformationExcel_Err:
        If Err.Number Then
            insTransformationExcel = False
            sMessage = sMessage & "[insTransformationExcel]" & Err.Description & vbCrLf
        Else
            sMessage = sMessage & "[insTransformationExcel]" & Err.Description & vbCrLf
            sMessage &= "10_"
        End If
        On Error Resume Next

        'UPGRADE_NOTE: Object mvarSalidaExcel may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        'mvarSalidaExcel = Nothing
        On Error GoTo 0
    End Function

    '% insTransformationPDF: Transforma el archivo a PDF
    Public Function insTransformationPDF(ByVal sKey As String, ByVal sDescript As String) As Boolean
        Dim lclsvalue As eFunctions.Values
        Dim mvarSalidaExcel As Microsoft.Office.Interop.Excel.Application
        Dim lstrFileName As String
        Dim lintExist As Integer
        Dim lstrFile As String
        Dim lintlength As Integer
        Dim sFile As String
        Dim sAlfabeto As String
        Dim sCell As String
        Dim sColumn As String
        Dim lintCount As Integer
        Dim lclsBatch_job As eSchedule.Batch_job
        Dim lobjSalidaExcel As Microsoft.Office.Interop.Excel.Worksheet
        Dim sRange As Microsoft.Office.Interop.Excel.Range

        Dim lintcolumn As Integer
        Dim sLogo As String
        On Error GoTo insTransformationExcel_Err

        lclsvalue = New eFunctions.Values

        sMessage = "Asignaci?n logo:i " & vbCrLf

        'NS:On Error Resume Next
        sLogo = Trim(UCase(lclsvalue.insGetSetting("LogoFilename", String.Empty, "PATHS")))

        sMessage &= "Asignaci?n logo:e " & vbCrLf

        lclsBatch_job = New eSchedule.Batch_job
        sMessage = "Find Batch job:i " & vbCrLf

        If lclsBatch_job.Find_Interface_Batch_Job(sKey) Then
            sMessage &= "Find Batch job:true " & vbCrLf

            lintCount = 1
            sAlfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

            While lclsBatch_job.ItemBatchJob(lintCount)
                If lclsBatch_job.sOutputFile <> vbNullString Then
                    sMessage &= "output: " & lclsBatch_job.sOutputFile & vbCrLf
                    lstrFileName = lclsBatch_job.sDirOut & "\" & lclsBatch_job.sOutputFile
                    lintExist = InStr(1, UCase(lclsBatch_job.sOutputFile), ".XLS")
                    If lintExist > 0 Then
                        lstrFile = Mid(lclsBatch_job.sOutputFile, 1, lintExist - 1)
                        lstrFile = lstrFile & ".pdf"
                        mstrFile = lstrFile
                        sMessage &= "open app: i" & vbCrLf

                        mvarSalidaExcel = New Microsoft.Office.Interop.Excel.Application
                        sMessage &= "open app: e" & vbCrLf

                        mvarSalidaExcel.DisplayAlerts = False

                        sMessage &= "open workbook: i" & vbCrLf

                        mvarSalidaExcel.Workbooks.Open(lstrFileName, 0, True, , "insudb")
                        sMessage &= "open workbook: e" & vbCrLf

                        mvarSalidaExcel.DisplayAlerts = False

                        sMessage &= "activate sheet: i" & vbCrLf

                        lobjSalidaExcel = mvarSalidaExcel.ActiveSheet
                        sMessage &= "activate sheet: e" & vbCrLf

                        sMessage &= "counting cols: i" & vbCrLf

                        lintcolumn = 1
                        While lintcolumn < 26
                            sColumn = Mid(sAlfabeto, lintcolumn, 1)
                            sCell = sColumn & "1:" & sColumn & "1"
                            sRange = lobjSalidaExcel.Range(sCell)
                            If sRange.FormulaR1C1 = vbNullString Then
                                lintcolumn = lintcolumn - 1
                                Exit While
                            End If
                            lintcolumn = lintcolumn + 1
                        End While
                        sMessage &= "counting cols: e" & vbCrLf

                        sColumn = Mid(sAlfabeto, lintcolumn, 1)
                        sMessage &= "ranging: i" & vbCrLf

                        sRange = lobjSalidaExcel.Range("1:1")
                        With (sRange.Interior)
                            .Pattern = 1
                            .PatternColorIndex = -4105
                            .ThemeColor = 1
                            .TintAndShade = 0
                            .PatternTintAndShade = 0
                        End With
                        sRange.RowHeight = 30
                        sRange.Font.FontStyle = "Bold"
                        sRange.Font.Size = 12
                        sCell = "A1:" & sColumn & "1"
                        sMessage &= "ranging: e" & vbCrLf

                        With (sRange.Interior)
                            .Pattern = 1
                            .PatternColorIndex = -4105
                            .ThemeColor = 7
                            .TintAndShade = 0
                            .Color = 5296274
                            .PatternTintAndShade = 0
                        End With
                        lobjSalidaExcel.Rows("1:1").Select()
                        sRange = lobjSalidaExcel.Range("1:1")
                        sMessage &= "addrows: i" & vbCrLf

                        sRange.Insert(Shift:=-4121, CopyOrigin:=0)
                        sRange.Insert(Shift:=-4121, CopyOrigin:=0)
                        sRange.Insert(Shift:=-4121, CopyOrigin:=0)
                        sMessage &= "addrows: e"

                        sColumn = Mid(sAlfabeto, lintcolumn, 1)
                        If sColumn = vbNullString Then
                            sColumn = "Z"
                        End If

                        sCell = sColumn & "1"
                        sRange = lobjSalidaExcel.Range(sCell)
                        sRange.FormulaR1C1 = DateTime.Now.ToString()
                        sRange.EntireColumn.AutoFit()
                        sCell = "A2:" & sColumn & "2"

                        sRange = lobjSalidaExcel.Range(sCell)

                        With sRange
                            .HorizontalAlignment = -4108
                            .VerticalAlignment = -4107
                            .WrapText = False
                            .Orientation = 0
                            .AddIndent = False
                            .IndentLevel = 0
                            .ShrinkToFit = False
                            .ReadingOrder = -5002
                            .MergeCells = True
                            .FormulaR1C1 = sDescript
                        End With

                        sCell = "A1:" & sColumn & "4"
                        sMessage &= "setting page: i" & vbCrLf

                        With (lobjSalidaExcel.PageSetup)

                            .Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape
                            .PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperLetter
                            .Zoom = 100
                            .PrintTitleRows = sCell
                            .PrintTitleColumns = ""
                            .Zoom = False
                            .FitToPagesWide = 1
                            .FitToPagesTall = 500
                        End With

                        sMessage &= "setting page: e" & vbCrLf
                        sMessage &= "exporting: i" & vbCrLf

                        lobjSalidaExcel.ExportAsFixedFormat(Type:=Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, Filename:= _
                            lclsBatch_job.sDirOut & "\" & lstrFile, Quality:=Microsoft.Office.Interop.Excel.XlFixedFormatQuality.xlQualityStandard.xlQualityStandard, _
                            IncludeDocProperties:=False, IgnorePrintAreas:=False, OpenAfterPublish:= _
                            False)

                        sMessage &= "exporting: e" & vbCrLf

                        insTransformationPDF = True
                    End If
                End If
                lintCount = lintCount + 1
                sMessage &= "looping"
            End While
        End If

        sMessage &= "Find Batch job:e " & vbCrLf

insTransformationExcel_Err:
        If Err.Number Then
            insTransformationPDF = False
            sMessage &= "ERRRRRRR" & vbCrLf
            sMessage = sMessage & "[insTransformationPDF]" & Err.Description & vbCrLf
            Err.Raise(Err.Number, Err.Description)
        End If
        sMessage = sMessage & "[insTransformationPDF]" & Err.Description & vbCrLf
        mvarSalidaExcel.ActiveWorkbook.Close()
        mvarSalidaExcel.Quit()
        sMessage &= "10_"
        'End If
        On Error Resume Next

        'UPGRADE_NOTE: Object mvarSalidaExcel may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mvarSalidaExcel = Nothing
        lclsBatch_job = Nothing
        lobjSalidaExcel = Nothing
        lclsvalue = Nothing
        sRange = Nothing
        On Error GoTo 0
    End Function

    '% InsPostGI1402: Inserta un registro en la tabla T_ERR_INTERFACE de la base de datos.
    Public Function Ins_Cret_Err_Interface(ByVal sKey As String, ByVal nUsercode As Integer)
        Dim lrecInsPostGI1402 As eRemoteDB.Execute

        If Me.sMessage <> "" Then
            lrecInsPostGI1402 = New eRemoteDB.Execute
            With lrecInsPostGI1402
                .StoredProcedure = "CRET_ERR_INTERFACE"
                .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nSeq", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nRow", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nError", -1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sDescript", Me.sMessage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sCertype", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", numNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", numNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", numNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", numNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sClient", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nClaim", numNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                Ins_Cret_Err_Interface = .Run(False)
            End With
        End If

InsPostGI1402_Err:
        If Err.Number Then
            Ins_Cret_Err_Interface = False
        End If
        'UPGRADE_NOTE: Object lrecInsPostGI1402 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPostGI1402 = Nothing
        On Error GoTo 0
    End Function

    '% InsPostGI1402: Ejecuta el post de la transacci?n
    Public Function InsPostGI1402(ByVal nIntertype As Integer, ByVal nSheet As Integer, ByVal nUsercode As Integer, ByVal sKey As String, ByVal sFileName As String, Optional ByVal nFormat As Integer = numNull, Optional ByRef sMessage As String = "", Optional ByVal sActionQuery As String = "2") As Boolean

        Dim lrecInsPostGI1402 As eRemoteDB.Execute
        Dim bejeInsPostGI1402 As Boolean

        bejeInsPostGI1402 = True
        Me.sMessage = ""

        If sActionQuery = "2" Then
            If (nFormat = 2 Or nFormat = 11) And nIntertype = 1 Then
                Dim sExtension = Path.GetExtension(sFileName).ToString.ToLower
                If sExtension = ".xls" Or sExtension = ".xlsx" Then
                    If insTransformationExcel(sFileName) Then
                        sFileName = mstrFileTxt
                        InsPostGI1402_File(sFileName, nIntertype, nFormat, sKey, nSheet, nUsercode, False)
                        bejeInsPostGI1402 = True
                    Else
                        If Me.sMessage <> "" Then
                            Ins_Cret_Err_Interface(sKey, nUsercode)
                        End If
                        bejeInsPostGI1402 = False
                    End If
                Else
                    If InsPostGI1402_File(sFileName, nIntertype, nFormat, sKey, nSheet, nUsercode, False) Then
                        bejeInsPostGI1402 = True
                    Else
                        bejeInsPostGI1402 = False
                    End If
                End If
            Else
                If InsPostGI1402_File(sFileName, nIntertype, nFormat, sKey, nSheet, nUsercode, False) Then
                    bejeInsPostGI1402 = True
                Else
                    bejeInsPostGI1402 = False
                End If
            End If
        End If
        mstrFile = sFileName

        If bejeInsPostGI1402 Then
            On Error GoTo InsPostGI1402_Err
            lrecInsPostGI1402 = New eRemoteDB.Execute

            'Inserta un registro en la base de datos en la tabla BATCH_JOB.
            With lrecInsPostGI1402
                .StoredProcedure = "INSPOSTGI1402"
                .Parameters.Add("nIntertype", nIntertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 100, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sFileName", mstrFile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'Si nError = 1, indica que encontro error de formato de archivo por lo que no debe hacerse nada mas en la pagina
                .Parameters.Add("nError", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sKey_father", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sActionQuery", sActionQuery, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                InsPostGI1402 = .Run(False)

                If .Parameters("nError").Value = 1 Then
                    InsPostGI1402 = False
                End If
            End With
        Else
            On Error GoTo InsPostGI1402_Err
            lrecInsPostGI1402 = New eRemoteDB.Execute

            'Inserta un registro en la base de datos en la tabla BATCH_JOB.
            With lrecInsPostGI1402
                .StoredProcedure = "INSBEGININTERFACE"
                .Parameters.Add("nIntertype", nIntertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 100, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                InsPostGI1402 = .Run(False)

                If .Parameters("nError").Value = 1 Then
                    InsPostGI1402 = False
                End If
            End With
        End If

InsPostGI1402_Err:
        If Err.Number Then
            InsPostGI1402 = False
        End If
        'UPGRADE_NOTE: Object lrecInsPostGI1402 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPostGI1402 = Nothing
        On Error GoTo 0
    End Function

    '%InsPostGI1405: Ejecuta el post de la transacci?n SALIDA DEL ARCHIVO XML
	Public Function InsPostGI1405(ByVal nIntertype As Integer, ByVal nSheet As Integer, ByVal nUsercode As Integer, ByVal sKey As String, Optional ByVal sTableName As String = strNull) As Boolean
		
		Dim lrecInsPostGI1405 As eRemoteDB.Execute
        Dim lclsMastersheet As MasterSheet
		'+ nExistError = 0 Ok, > 0 No Ok
		On Error GoTo InsPostGI1405_Err
		lrecInsPostGI1405 = New eRemoteDB.Execute
        lclsMastersheet = New MasterSheet

		With lrecInsPostGI1405
			.StoredProcedure = "INSPOSTGI1405"
			.Parameters.Add("nInterType", nIntertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTableName", sTableName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsPostGI1405 = .Run(False)

            lclsMastersheet.Find(nSheet)
            ' Para las interfaces de salida en PDF se transforma el codigo en excel
            If InsPostGI1405 Then
                If (lclsMastersheet.nFormat = 9 And nIntertype = 2) Or _
                   (UCase(lclsMastersheet.sOut_routine) = "GENPDF" And nIntertype = 1) Then
                    If insTransformationPDF(sKey, lclsMastersheet.sDescript) Then
                        lrecInsPostGI1405 = Nothing
                        lrecInsPostGI1405 = New eRemoteDB.Execute
                        With lrecInsPostGI1405
                            .StoredProcedure = "UPDBATCH_JOB_FILE"
                            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("sFileName", mstrFile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            InsPostGI1405 = .Run(False)
                        End With
                    End If
                End If
            End If
            nExistError = .Parameters("nExists").Value

        End With
		
InsPostGI1405_Err: 
		If Err.Number Then
			InsPostGI1405 = False
		End If
		'UPGRADE_NOTE: Object lrecInsPostGI1405 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPostGI1405 = Nothing
        lclsMastersheet = Nothing
        On Error GoTo 0
	End Function
	
	'% InsUpdBatch_Job: Actualiza el estado de una interfaz cancelada
	Public Function InsUpdBatch_Job(ByVal sKey As String) As Boolean
		
		Dim lrecInsUpdBatch_Job As eRemoteDB.Execute
		
		On Error GoTo InsUpdBatch_Job_Err
		lrecInsUpdBatch_Job = New eRemoteDB.Execute
		
		With lrecInsUpdBatch_Job
			.StoredProcedure = "INSUPDBATCH_JOB"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdBatch_Job = .Run(False)
		End With
		
InsUpdBatch_Job_Err: 
		If Err.Number Then
			InsUpdBatch_Job = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdBatch_Job may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsUpdBatch_Job = Nothing

		On Error GoTo 0
	End Function
	
    '% InsPostGI1402_File: Mueve el archivo desde SII al Servidor BD
    Public Function InsPostGI1402_File(ByVal sFileName As String, ByVal nIntertype As Integer, ByVal nFormat As Integer, Optional ByVal sKey As String = "", Optional ByVal nId As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal bPermitted As Boolean = False) As Boolean

        Dim lrecInsPostGI1402_File As eRemoteDB.Execute
        Dim lclsvalue As eFunctions.Values
        Dim i As Integer
        Dim n As Integer
        Dim sLine As String
        Dim sLineTot As String
        Dim PathFile As String
        Dim sNewFile As String
        Dim FileNum As Integer
        Dim sPathFile As String
        Dim lintlength As Integer
        Dim bChargeFile As Boolean

        On Error GoTo InsPostGI1402_File_Err

        bChargeFile = False
        If (nIntertype = 1) Or (bPermitted) Then
            Dim lclsImages As eRemoteDB.Images
            lclsImages = New eRemoteDB.Images
            lclsvalue = New eFunctions.Values

            sPathFile = UCase(lclsvalue.insGetSetting("MASSIVELOAD", String.Empty, "PATHS"))
            If sPathFile = String.Empty Then
                sPathFile = UCase(lclsvalue.insGetSetting("MASSIVELOAD", String.Empty, "Config"))
            End If

            If Mid(sPathFile, Len(sPathFile), 1) <> "\" Then
                sPathFile = sPathFile & "\"
            End If
            PathFile = sPathFile & Trim(sFileName)
            If lclsImages.AddTextClob(PathFile, sFileName, sKey, nId, nUsercode, "2") Then
                InsPostGI1402_File = True
            Else
                bChargeFile = True
            End If
        Else
            InsPostGI1402_File = True
        End If

        If bChargeFile Then
            lrecInsPostGI1402_File = New eRemoteDB.Execute

            '+ Solo muevo archivos para interfaces de Entrada
            If nIntertype = 1 Then
                lclsvalue = New eFunctions.Values

                sPathFile = UCase(lclsvalue.insGetSetting("MASSIVELOAD", String.Empty, "PATHS"))
                If sPathFile = String.Empty Then
                    sPathFile = UCase(lclsvalue.insGetSetting("MASSIVELOAD", String.Empty, "Config"))
                End If

                lintlength = Len(sPathFile)
                If Mid(sPathFile, lintlength, 1) <> "\" Then
                    sPathFile = sPathFile & "\"
                End If

                PathFile = sPathFile & sFileName
                i = 1
                n = 100
                sNewFile = "S"
                FileNum = FreeFile()
                FileOpen(FileNum, PathFile, OpenMode.Input)
                sLineTot = ""
                Do While Not EOF(FileNum)
                    If i > 1 Then
                        sNewFile = "N"
                    End If
                    sLine = LineInput(FileNum)
                    If (Len(sLine & sLineTot) + 1) <= 30000 Then
                        sLineTot = sLineTot & sLine & Chr(10)
                    Else
                        'llamo a la rutina que inserta en el servidor de BD
                        With lrecInsPostGI1402_File
                            .StoredProcedure = "CREFILE"
                            .Parameters.Add("sName_File", sFileName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("sLine", sLineTot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("sNewFile", sNewFile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            InsPostGI1402_File = .Run(False)
                        End With
                        sNewFile = "N"
                        i = i + 1
                        sLineTot = sLine & Chr(10)
                        sLine = ""
                    End If
                Loop
                If Len(sLineTot) <> 0 Then
                    With lrecInsPostGI1402_File
                        .StoredProcedure = "CREFILE"
                        .Parameters.Add("sName_File", sFileName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sLine", sLineTot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sNewFile", sNewFile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        InsPostGI1402_File = .Run(False)
                    End With
                End If
                FileClose(FileNum)
            Else
                InsPostGI1402_File = True
            End If
        End If

InsPostGI1402_File_Err:
        If Err.Number Then
            InsPostGI1402_File = False
        End If
        'UPGRADE_NOTE: Object lrecInsPostGI1402_File may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPostGI1402_File = Nothing
        On Error GoTo 0
    End Function

    '% insValGI1402: Se realizan las validaciones de parametros dinamicos
    Public Function insValGI1402(ByVal sKey As String, ByRef nSheet As Integer) As String
        Dim lrecinsValGI1402 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty
        Dim sCodispl As String

        On Error GoTo insValGI1402_Err
        lrecinsValGI1402 = New eRemoteDB.Execute
        sCodispl = "GI1402"
        With lrecinsValGI1402
            .StoredProcedure = "INSVALGI1402"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("Arrerrors").Value

            If lstrError <> String.Empty Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage(sCodispl, , , , , , lstrError)
                    insValGI1402 = lobjErrors.Confirm
                End With
                'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjErrors = Nothing
            End If

        End With
insValGI1402_Err:
        If Err.Number Then
            insValGI1402 = "insValGI1402: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecinsValGI1402 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValGI1402 = Nothing
        On Error GoTo 0
    End Function

    '% insReport: Proceso que verifica si se genera reporte de interface
    Public Function insReport(ByVal sKey As String, ByRef nSheet As Integer) As Boolean
        Dim lrecinsValGI1402 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty
        Dim sCodispl As String

        On Error GoTo insReport_Err
        lrecinsValGI1402 = New eRemoteDB.Execute
        sCodispl = "GI1402"
        insReport = False
        With lrecinsValGI1402
            .StoredProcedure = "REAREPORT_EXISTS"
            .Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run()
            If .FieldToClass("ncount") > 0 Then
                Me.bReport = True
                insReport = True
            End If
            Me.sDescript = .FieldToClass("sDescript")
        End With
insReport_Err:
        If Err.Number Then
            insReport = False
        End If
        'UPGRADE_NOTE: Object lrecinsValGI1402 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValGI1402 = Nothing
        On Error GoTo 0
    End Function

    '% insReport: Proceso que verifica si se genera reporte de error
    Public Function insReportError(ByVal sKey As String, ByRef nSheet As Integer) As Boolean
        Dim lrecinsValGI1402 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty
        Dim sCodispl As String

        On Error GoTo insReport_Err
        lrecinsValGI1402 = New eRemoteDB.Execute
        sCodispl = "GI1402"
        insReportError = False
        With lrecinsValGI1402
            .StoredProcedure = "REAREPORTERR_EXISTS"
            .Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run()
            If .FieldToClass("ncount") > 0 Then
                Me.bReport = True
                insReportError = True
            End If
            Me.sDescript = .FieldToClass("sDescript")
        End With
insReport_Err:
        If Err.Number Then
            insReportError = False
        End If
        'UPGRADE_NOTE: Object lrecinsValGI1402 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValGI1402 = Nothing
        On Error GoTo 0
    End Function

    Public Function Find_Opt_Interfase() As Boolean
        Dim lrecreaOpt_Interfase As eRemoteDB.Execute

        lrecreaOpt_Interfase = New eRemoteDB.Execute

        '**+ Parameter definitions for stored procedure 'insud.reaClient'
        '+ Definici?n de par?metros para stored procedure 'insudb.reaClient'
        '**+ Data of July 1st,1999  03:20:55 p.m.
        '+ Informaci?n le?da el 01/07/1999 03:20:55 PM

        With lrecreaOpt_Interfase
            .StoredProcedure = "reaOpt_Interface"
            If .Run Then
                sVirtualdirview = .FieldToClass("sVirtualdirview")
                sIPremote = .FieldToClass("sIPremote")
                sDirwork = .FieldToClass("sDirwork")
                sDirview = .FieldToClass("sDirview")
                sDirout = .FieldToClass("sDirout")
                sDirentry = .FieldToClass("sDirentry")
                sDeletetable = .FieldToClass("sDeletetable")
                .RCloseRec()
                Find_Opt_Interfase = True
            Else
                Find_Opt_Interfase = False
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaOpt_Interfase = Nothing

    End Function

    '% GetParam: Trae parametros por cada campo definido para su construccion
    Public Function GetParamGI1408(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nSheet As Object, ByVal dEffecdate As Date, Optional ByVal nTransaction As Integer = 0) As String
        Dim lclsField As Object
        Dim lcolField As Object

        '% Columnas de definicion de campos "Parametro"
        Dim sFieldDesc As String 'Nombre Campo
        Dim nObjtype As Integer 'Objeto de despliegue
        Dim sValue As String 'Valor por defecto
        Dim nFieldLarge As Integer 'Tama?o del campo
        Dim sFieldCommen As String 'Comentario del campo
        Dim sValueslist As String 'Lista de Posibles Valores
        Dim nDataType As Integer 'Tipo de Datos del Campo
        Dim sObligatory As String 'Indica si el campo es requerido: 1-Afirmativo, 2-Negativo
        Dim i As Integer
        Dim sColumnName As String

        Dim sFieldProducto As String = "" 'Nombre Campo

        lcolField = New FieldSheets
        lclsField = New FieldSheet
        i = 0
        GetParamGI1408 = "<TABLE WIDTH=""80%""><TR>"

        If lcolField.Find_Dinamic_Table(sCertype, nBranch, nProduct, nPolicy, nCertif, nSheet, 3, dEffecdate) Then
            For Each lclsField In lcolField
                If (lclsField.nObjtype = 6) Then
                    sFieldProducto = lclsField.sColumnName
                End If
            Next lclsField
        End If

        mblncheck = False
        For Each lclsField In lcolField
            i = i + 1
            sFieldDesc = lclsField.sFieldDesc
            sColumnName = lclsField.sColumnName
            nObjtype = lclsField.nObjtype
            sObligatory = lclsField.sObligatory
            If (lclsField.sValue = "") And (lclsField.sValueRutine <> "") Then
                sValue = lclsField.sValueRutine
            Else
                sValue = lclsField.sValue
            End If

            nFieldLarge = lclsField.nFieldLarge
            sFieldCommen = lclsField.sFieldCommen
            sValueslist = lclsField.sValueslist
            nDataType = lclsField.nDataType
            GetParamGI1408 = GetParamGI1408 & GenParam(sColumnName, sFieldDesc, nObjtype, sValue, nFieldLarge, sFieldCommen, sValueslist, nDataType, sFieldProducto, sObligatory, lclsField.nDecimal, 0, nTransaction)
            If i = 2 Then
                i = 0
                GetParamGI1408 = GetParamGI1408 & "</TR>"
            End If
        Next lclsField
        If i = 1 Then
            GetParamGI1408 = GetParamGI1408 & "</TR>"
        End If
        GetParamGI1408 = GetParamGI1408 & "</TABLE>"

        If mblncheck Then
            GetParamGI1408 = GetParamGI1408 & "<SCRIPT> function Onchecked(Field){ "
            GetParamGI1408 = GetParamGI1408 & " if(Field.checked) "
            GetParamGI1408 = GetParamGI1408 & " self.document.forms[0].elements[Field.name + 'hdd'].value=1; "
            GetParamGI1408 = GetParamGI1408 & " else "
            GetParamGI1408 = GetParamGI1408 & " self.document.forms[0].elements[Field.name + 'hdd'].value=2; "
            GetParamGI1408 = GetParamGI1408 & " } </SCRIPT>"
        End If

        'UPGRADE_NOTE: Object lcolField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolField = Nothing
        'UPGRADE_NOTE: Object lclsField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsField = Nothing
    End Function

    '%InsPostGI1408: Ejecuta el post de la transacci?n GI1408
    Public Function InsPostGI1408(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nSheet As Object, ByVal dEffecdate As Date, ByVal nField As Long, ByVal nDataType As Integer, ByVal sValue As String, ByVal nUsercode As Long, ByVal sCodispl As String) As Boolean

        Dim lrecInsPostGI1408 As eRemoteDB.Execute
        Dim lclsValues As eFunctions.Values
        Dim sValue_aux As String
        Dim dValue As Date
        Dim nValue As Double

        On Error GoTo InsPostGI1408_Err
        lrecInsPostGI1408 = New eRemoteDB.Execute

        sValue_aux = eRemoteDB.Constants.strNull
        dValue = eRemoteDB.Constants.dtmNull
        nValue = eRemoteDB.Constants.dblNull

        lclsValues = New eFunctions.Values

        If nDataType = 1 Or _
           nDataType = 2 Then
            sValue_aux = sValue
        ElseIf nDataType = 3 Then
            dValue = lclsValues.StringToDate(sValue)
        ElseIf nDataType = 4 Or _
                nDataType = 5 Or _
                nDataType = 6 Then
            nValue = lclsValues.StringToType(sValue, eFunctions.Values.eTypeData.etdDouble)
        End If

        With lrecInsPostGI1408
            .StoredProcedure = "INSPOSTGI1408"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nField", nField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sValue", sValue_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dValue", dValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValue", nValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCosidpl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            InsPostGI1408 = .Run(False)
        End With

InsPostGI1408_Err:
        If Err.Number Then
            InsPostGI1408 = False
        End If
        lrecInsPostGI1408 = Nothing
        On Error GoTo 0
    End Function
End Class






