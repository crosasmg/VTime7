Option Strict Off
Option Explicit On

Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.HSSF.Extractor
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel

Public Class Colsheet
    '%-------------------------------------------------------%'
    '% $Workfile:: Colsheet.cls                             $%'
    '% $Author:: Nvaplat28                                  $%'
    '% $Date:: 22/10/03 17.19                               $%'
    '% $Revision:: 53                                       $%'
    '%-------------------------------------------------------%'

    'Column_name                      Type        Length      Prec  Scale Nullable
    '-------------------------------- ----------- ----------- ----- ----- ---------
    Public nId As Integer 'int        4           10    0     no
    Public nIdRec As Integer 'int        4           10    0     no
    Public sSel As String 'char       1                       yes
    Public sColumnName As String 'char       30                      no
    Public nOrder As Integer 'smallint   2           5     0     yes
    Public sRequire As String 'char       1                       yes
    Public sSelected As String 'char       1                       yes
    Public nUsercode As Integer
    Public sDefaultValue As String 'Varchar       50   
    '-Se definen las variables auxiliares
    Public sSheet As String
    Public sField As String
    Public sGroupRequire As String
    Public sValuesList As String
    Public sPossibleValues As String
    Public sComment As String
    Public sExists As String
    Public sTableName As String
    Public nBranch As Integer
    Public nProduct As Integer
    Public npolicy As Double
    Public nSheet As Integer
    Public sData_Type As String
    Public nData_Length As Integer
    Public nData_Precision As Integer
    Public nData_Scale As Integer

    '-Mensaje de proceso
    Public sMessage As String

    '%Add: Esta funcion se encarga de incluir un registro en la tabla ColSheet
    Public Function Add() As Boolean
        Dim lrecCreColSheet As eRemoteDB.Execute

        On Error GoTo Add_Err
        lrecCreColSheet = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.CreColSheet'
        '+Información leída el 05/02/2001 10:58:38 a.m.

        With lrecCreColSheet
            .StoredProcedure = "CreColSheet"
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIdRec", nIdRec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColumnName", sColumnName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSelected", sSelected, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sValue", sDefaultValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

Add_Err:
        If Err.Number Then
            Add = False
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecCreColSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecCreColSheet = Nothing
    End Function

    '%Update Esta funcion se encarga de actualizar el registro en la tabla ColSheet
    Public Function Update() As Boolean
        Dim lrecUpdColSheet As eRemoteDB.Execute

        On Error GoTo Update_Err

        lrecUpdColSheet = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.UpdColSheet'
        '+Información leída el 05/02/2001 10:58:38 a.m.

        With lrecUpdColSheet
            .StoredProcedure = "UpdColSheet"
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIdRec", nIdRec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColumnName", sColumnName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSelected", sSelected, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sValue", sDefaultValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

Update_Err:
        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecUpdColSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecUpdColSheet = Nothing
    End Function

    '%Delete. Esta funcion se encarga de eliminar el registro indicado de la tabla ColSheet
    Public Function Delete() As Boolean
        Dim lrecDelColSheet As eRemoteDB.Execute

        On Error GoTo Delete_Err

        lrecDelColSheet = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.UpdColSheet'
        '+Información leída el 05/02/2001 10:58:38 a.m.
        With lrecDelColSheet
            .StoredProcedure = "DelColSheet"
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIdRec", nIdRec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

Delete_Err:
        If Err.Number Then
            Delete = False
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecDelColSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecDelColSheet = Nothing
    End Function

    '%insQueryExportExcel: Exporta planilla excel
    Public Sub insQueryExportExcel(ByVal nId As Integer, ByVal sFile As String)
        Dim lclsExcelApp As Microsoft.Office.Interop.Excel.Application
        Dim lclsWorksheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim lclsWorksheet_1 As Microsoft.Office.Interop.Excel.Worksheet
        Dim lintSheet As Integer
        Dim lintCounterSheet As Integer
        Dim lintColumn As Integer
        Dim lstrField As String
        Dim lclsColsheets As Colsheets
        Dim lclsColsheet As Colsheet
        Dim lreclocal As eFunctions.Tables

        Dim lstrPosition As String
        Dim lclsvalue As eFunctions.Values
        Dim lstrFileName As String
        Dim lstrFile As String
        Dim lintExist As Integer
        Dim lintlength As Integer

        Dim lintValRow As Integer
        Dim lintValCol As Integer
        Dim lintReferCol As Integer

        '-Variables para almacenar mensajes
        Dim lstrMsg1252 As String
        Dim lstrMsg1254 As String
        Dim lstrMsg1255 As String
        Dim lstrMsg1256 As String

        'NS:On Error GoTo insQueryExportExcel_Err

        lintExist = InStr(1, UCase(sFile), ".XLS")
        If lintExist > 0 Then
            lstrFile = Mid(sFile, 1, lintExist - 1)
        Else
            lstrFile = sFile
        End If

        lclsvalue = New eFunctions.Values

        'NS:On Error Resume Next
        lstrFileName = Trim(UCase(lclsvalue.insGetSetting("MASSIVELOAD", String.Empty, "PATHS")))
        If lstrFileName = String.Empty Then
            lstrFileName = Trim(UCase(lclsvalue.insGetSetting("MASSIVELOAD", String.Empty, "Config")))
        End If

        sMessage &= "1_ archivo " & lstrFileName
        'NS:On Error GoTo insQueryExportExcel_Err

        lintlength = Len(lstrFileName)
        If Mid(lstrFileName, lintlength, 1) <> "\" Then
            lstrFileName = lstrFileName & "\"
        End If

        lstrFileName = lstrFileName & Trim(lstrFile) & ".XLS"
        sMessage &= "2_ archivo " & lstrFileName

        lintValCol = 1
        lintValRow = 1
        lintReferCol = 1
        lstrField = String.Empty
        lintSheet = 0
        lintColumn = 0

        sMessage &= "21_ "

        lclsExcelApp = New Microsoft.Office.Interop.Excel.Application
        sMessage &= "22_ "

        lclsExcelApp.Visible = False
        sMessage &= "231_ "

        lclsExcelApp.Workbooks.Add()
        sMessage &= "232_ "

        lclsExcelApp.DisplayAlerts = False

        '+Se guardan los valores de ramo producto y poliza en la primera hoja del libro y luego se oculta
        sMessage &= "233_ "

        With lclsExcelApp.Workbooks(1)
            .Sheets(1).Visible = False
            .Sheets(3).Visible = False
            .Sheets(1).Name = "Datos de la hoja"
            .Sheets(2).Name = "Plantilla de carga"
            .Sheets(3).Name = "Lista"
        End With
        sMessage &= "234_ "

        lintCounterSheet = 2

        '+Se realiza la busqueda de los nombres de las columnas y de las hojas a crear
        sMessage &= "2_4 id " & nId

        lclsColsheets = New Colsheets
        If lclsColsheets.FindSheet(nId) Then

            lclsColsheet = New Colsheet
            sMessage &= "2_5 id " & nId

            With lclsExcelApp
                .Workbooks(1).Sheets(lintCounterSheet).Activate()
                lclsWorksheet = .Workbooks(1).Sheets(lintCounterSheet)
                lclsWorksheet_1 = .Workbooks(1).Sheets(3)
            End With

            '+Se ubica la columna a partir de la cual se crearan las referencias
            '+a las listas de valores posibles
            lintReferCol = lclsColsheets.Count + 10

            lstrMsg1252 = eFunctions.Values.GetMessage(1252)
            lstrMsg1255 = eFunctions.Values.GetMessage(1255)
            lstrMsg1254 = eFunctions.Values.GetMessage(1254)
            lstrMsg1256 = eFunctions.Values.GetMessage(1256)

            For Each lclsColsheet In lclsColsheets
                sMessage &= "3_ "

                lintColumn = lintColumn + 1

                '+Se obtiene la direccion de la columna en tratamiento
                'lclsExcelApp.Range("AZ6").Select
                'lclsExcelApp.ActiveCell.FormulaR1C1 = "=ADDRESS(1," & lintColumn & ")"
                'lstrPosition = lclsExcelApp.Range("AZ6")
                'lclsExcelApp.ActiveCell.FormulaR1C1 = String.Empty
                'lstrPosition = insFindSheetAddress(lstrPosition)

                lstrPosition = lclsWorksheet.Cells._Default(1, lintColumn).Address

                '+Se asigna nombre a cabecera de columna
                lclsWorksheet.Cells._Default(1, lintColumn) = lclsColsheet.sColumnName
                '+Se ajusta ancho de columna según nombre
                lclsWorksheet.Columns._Default(lintColumn).EntireColumn.AutoFit()

                '+Se indica que el contenido de la celda debe corresponder a un valor tipo fecha
                If InStr(1, lclsColsheet.sField, "d", CompareMethod.Text) = 1 Then
                    lclsWorksheet.Columns._Default(lintColumn).Select()
                    lclsExcelApp.Selection.NumberFormat = "yyyy/MM/dd"
                    With lclsExcelApp.Selection.Validation
                        .Delete()
                        .Add(Type:=Microsoft.Office.Interop.Excel.XlDVType.xlValidateDate, AlertStyle:=Microsoft.Office.Interop.Excel.XlDVAlertStyle.xlValidAlertStop, Operator:=Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlGreater, Formula1:="01/01/1900")
                        .IgnoreBlank = True
                        .InCellDropdown = True
                        .InputTitle = ""
                        .ErrorTitle = ""
                        .InputMessage = ""
                        .ErrorMessage = lstrMsg1252
                        .ShowInput = True
                        .ShowError = True
                    End With
                End If

                '+Se incluye la validacion que impedira que se realicen cambios al nombre de la columna
                lclsExcelApp.Range(lstrPosition).Select()
                With lclsExcelApp.Selection.Validation
                    .Delete()
                    .Add(Type:=Microsoft.Office.Interop.Excel.XlDVType.xlValidateCustom, AlertStyle:=Microsoft.Office.Interop.Excel.XlDVAlertStyle.xlValidAlertStop, Operator:=Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlBetween, Formula1:=lclsColsheet.sColumnName)
                    .IgnoreBlank = True
                    .InCellDropdown = True
                    .InputTitle = ""
                    .ErrorTitle = ""
                    .InputMessage = ""
                    .ErrorMessage = lstrMsg1254
                    .ShowInput = True
                    .ShowError = True
                End With

                '+Si se encuentra, se agrega el comentario correspondiente a la columna
                If lclsColsheet.sComment <> String.Empty Then
                    With lclsExcelApp.Range(lstrPosition)
                        .AddComment()
                        .Comment.Visible = False
                        .Comment.Text(Text:=lclsColsheet.sComment)
                        .Select()
                        .Comment.Shape.ScaleHeight(0.47, 0, 0)
                    End With
                End If

                '+Se le da formato de encabezado a la celda seleccionada (bordes, alineacion, etc.)
                lclsExcelApp.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = Microsoft.Office.Interop.Excel.Constants.xlNone
                lclsExcelApp.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalUp).LineStyle = Microsoft.Office.Interop.Excel.Constants.xlNone
                With lclsExcelApp.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick
                    .ColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
                End With
                With lclsExcelApp.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick
                    .ColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
                End With
                With lclsExcelApp.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick
                    .ColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
                End With
                With lclsExcelApp.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick
                    .ColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
                End With
                With lclsExcelApp.Selection
                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    .VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
                    .WrapText = False
                    .Orientation = 0
                    .AddIndent = False
                    .ShrinkToFit = False
                    .MergeCells = False
                End With

                '+Si existen listas:

                If lclsColsheet.sValuesList = "1" And Len(Trim(lclsColsheet.sPossibleValues)) > 0 Then
                    '+Se realiza la lectura de los valores de la lista y se agregan en una columna a ocultar
                    lreclocal = New eFunctions.Tables
                    If lreclocal.reaTable(lclsColsheet.sPossibleValues) Then
                        lintValRow = 1
                        Do While Not lreclocal.EOF
                            lclsWorksheet_1.Cells._Default(lintValRow, lintValCol) = Trim(lreclocal.Fields(lreclocal.KeyField) & " - " & lreclocal.Fields(lreclocal.DescriptField))
                            lreclocal.NextRecord()
                            lintValRow = lintValRow + 1
                        Loop
                        lreclocal.closeTable()

                        '+Se ubica en ultima fila condatos
                        lintValRow = lintValRow - 1

                        '+Se da nombre a datos de lista
                        lstrPosition = lclsWorksheet_1.Range(lclsWorksheet_1.Cells._Default(1, lintValCol), lclsWorksheet_1.Cells._Default(lintValRow, lintValCol)).Address
                        'ExcelGlobal_definst.ActiveWorkbook.Names.Add(Name:=lclsColsheet.sPossibleValues, RefersTo:="=Lista!" & lstrPosition)
                        lclsWorksheet_1.Application.ActiveWorkbook.Names.Add(Name:=lclsColsheet.sPossibleValues, RefersTo:="=Lista!" & lstrPosition)
                        'DirectCast(lclsWorksheet_1.Application, Microsoft.Office.Interop.Excel.ApplicationClass).ActiveWorkbook


                        '+Se ubica la columna que sera tipo combo
                        'lclsExcelApp.Range("AZ6").Select
                        'lclsExcelApp.ActiveCell.FormulaR1C1 = "=ADDRESS(1," & lintValCol & ")"
                        'lstrPosition = lclsExcelApp.Range("AZ6")
                        'lclsExcelApp.ActiveCell.FormulaR1C1 = String.Empty
                        'lstrPosition = Mid(lstrPosition, 1, (InStr(2, lstrPosition, "$", vbTextCompare) - 1))


                        '+En la hoja de datos se crea referencia a lista de valores posibles
                        lclsWorksheet.Cells._Default(1, lintReferCol).Formula = "=" & lclsColsheet.sPossibleValues
                        lclsWorksheet.Columns._Default(lintReferCol).Hidden = True

                        '+Se selecciona la columna con valores posibles y se le asgigna validacion
                        lclsWorksheet.Columns._Default(lintColumn).Select()
                        With lclsExcelApp.Selection.Validation
                            .Delete()
                            .Add(Type:=Microsoft.Office.Interop.Excel.XlDVType.xlValidateList, AlertStyle:=Microsoft.Office.Interop.Excel.XlDVAlertStyle.xlValidAlertStop, Operator:=Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlBetween, Formula1:="=" & lclsColsheet.sPossibleValues)
                            .IgnoreBlank = True
                            .InCellDropdown = True
                            .InputTitle = ""
                            .ErrorTitle = ""
                            .InputMessage = lclsColsheet.sComment
                            .ErrorMessage = lstrMsg1255
                            .ShowInput = True
                            .ShowError = True
                        End With

                        '+Se selecciona la celda correspondiente a encabezado
                        '                    lclsExcelApp.Range("AZ6").Select
                        '                    lclsExcelApp.ActiveCell.FormulaR1C1 = "=ADDRESS(1," & lintColumn & ")"
                        '                    lstrPosition = lclsExcelApp.Range("AZ6")
                        '                    lclsExcelApp.ActiveCell.FormulaR1C1 = String.Empty
                        '                    lstrPosition = insFindSheetAddress(lstrPosition)

                        '+y se quita la lista de valores para la misma indicando que no puede ser modificada
                        '                    lclsExcelApp.Range(lstrPosition).Select
                        With lclsExcelApp.Selection.Validation
                            '                        .Delete
                            '                        .Add Type:=xlValidateCustom, AlertStyle:=xlValidAlertStop, Operator:= _
                            ''                        xlBetween, Formula1:=lclsColsheet.sColumnName
                            '                        .IgnoreBlank = True
                            '                        .InCellDropdown = True
                            '                        .InputTitle = ""
                            '                        .ErrorTitle = ""
                            '                        .InputMessage = ""
                            '                        .ErrorMessage = lstrMsg1256
                            '                        .ShowInput = True
                            '                        .ShowError = True
                        End With
                    End If
                    lreclocal = Nothing
                    lintValCol = lintValCol + 1
                    lintReferCol = lintReferCol + 1
                End If
            Next lclsColsheet
        End If

        lclsExcelApp.DisplayAlerts = False
        sMessage &= "5 antes de salvar_ "

        lclsExcelApp.ActiveWorkbook.SaveAs(lstrFileName, 56)
        sMessage &= "6 despues de  "

        lclsvalue = Nothing

        lclsExcelApp.ActiveWorkbook.Close()
        lclsExcelApp.Quit()

        lclsColsheets = Nothing
        lclsWorksheet = Nothing
        lclsWorksheet_1 = Nothing
        lclsExcelApp = Nothing

    End Sub

    '%insFindSheetAddress. Esta funcion se encarga de extraer de la cadena enviada el
    '%simbolo "$"
    Private Function insFindSheetAddress(ByVal lstrPosition As String) As String
        Do While InStr(1, lstrPosition, "$", CompareMethod.Text) <> 0
            lstrPosition = Mid(lstrPosition, 1, InStr(1, lstrPosition, "$", CompareMethod.Text) - 1) & Mid(lstrPosition, (InStr(1, lstrPosition, "$", CompareMethod.Text)) + 1, Len(Trim(lstrPosition)))
        Loop
        insFindSheetAddress = lstrPosition
    End Function

    '%FindTable5571. Esta funcion se encarga de Buscar el Criterio de busqueda en Table5571
    Public Function FindTable5571(ByVal sDescript As String) As Boolean
        Dim lrecFindTable5571 As eRemoteDB.Execute

        On Error GoTo FindTable5571_Err

        lrecFindTable5571 = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.UpdColSheet'
        '+Información leída el 05/02/2001 10:58:38 a.m.
        With lrecFindTable5571
            .StoredProcedure = "reaTable5571"
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            FindTable5571 = .Run
        End With

        'UPGRADE_NOTE: Object lrecFindTable5571 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecFindTable5571 = Nothing

FindTable5571_Err:
        If Err.Number Then
            FindTable5571 = False
        End If
        On Error GoTo 0
    End Function

    '%insQueryinportExcel(). Esta funcion se encarga de cargar el archivo en la base de datos
    Public Function insQueryInportExcel(ByVal nId As Integer, ByVal sFile As String, ByVal sKey As String, Optional ByRef sError As String = "", Optional ByVal sSeparate As String = "", Optional ByVal nRepinsured As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nPreview As Integer = 1) As Boolean

        '-Tamaño en que se aumenta bloque de carga
        Const ARRBLOCK As Short = 500

        Dim lclsvalue As eFunctions.Values
        Dim lclsColsheet As Colsheet
        Dim lcolColsheets As Colsheets
        Dim lclsMasiveCharge As MasiveCharge

        Dim lintRow As Integer
        Dim lintColumn As Integer
        Dim lintRow_end As Integer
        Dim lintColumn_end As Integer
        Dim lstrMassiveDir As String

        Dim lblnContinue As Boolean
        Dim lintExist As Integer
        Dim lintlength As Integer
        Dim lintFileNum As Integer
        '-Nombre de archivo con directorio
        Dim lstrFile As String
        '-Nombre de archivo con extension
        Dim lstrFileName As String
        '- Nombre archivo sin extensión
        Dim lstrFileNameWOExt As String
        Dim lstrFiledelxls As String
        Dim lstrFiledeltxt As String

        Dim lstrRow As String
        Dim lstrquery As String
        Dim lintPos As Integer
        '-Cantidad de columnas y valor de ultima columna
        Dim lintColCount As Integer
        Dim lintColMax As Integer
        '-Valores obtenidos de plantilla
        Dim lstrArray_txt() As String
        Dim lstrArray(,) As Object
        Dim lstrValue As String
        Dim lvntValue As Object
        Dim lintCount As Integer
        Dim sMassive As String = ""
        Dim lrecReaSheet As eRemoteDB.Execute
        Dim lintxlsx As Integer

        '-Tipo de datos de la columna
        Dim lstrType As String

        '-Valores obtenidos de colsheet
        Dim lstrField As String

        On Error GoTo insQueryinportExcel_Err
        sMessage &= "1_"
        insQueryInportExcel = False

        lintExist = 1
        Do While lintExist <> 0
            lintExist = InStr(1, UCase(sFile), "\")
            sFile = Mid(sFile, lintExist + 1)
            If InStr(1, UCase(sFile), "\") = 0 Then
                lintExist = 0
            End If
        Loop
        sMessage &= "2_"

        lintxlsx = InStr(1, UCase(sFile), ".XLSX")
        lintExist = InStr(1, UCase(sFile), ".XLS")
        If lintExist > 0 Then
            lstrFileNameWOExt = Mid(sFile, 1, lintExist - 1)
            sMassive = "2"
        Else
            lstrFileNameWOExt = sFile
        End If
        sMessage &= "3_"
        If sMassive = "2" Then
            lcolColsheets = New Colsheets
            lclsMasiveCharge = New MasiveCharge

            lclsvalue = New eFunctions.Values

            On Error Resume Next
            lstrMassiveDir = UCase(lclsvalue.insGetSetting("MASSIVELOAD", String.Empty, "PATHS"))
            If lstrMassiveDir = String.Empty Then
                lstrMassiveDir = UCase(lclsvalue.insGetSetting("MASSIVELOAD", String.Empty, "Config"))
            End If

            On Error GoTo insQueryinportExcel_Err

            'UPGRADE_NOTE: Object lclsvalue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsvalue = Nothing

            '+Si el directorio no incorpora linea se le agrega
            lintlength = Len(lstrMassiveDir)
            If Mid(lstrMassiveDir, lintlength, 1) <> "\" Then
                lstrMassiveDir = lstrMassiveDir & "\"
            End If

            sMessage &= "4_"
            If lintxlsx > 0 Then
                lstrFileName = lstrFileNameWOExt & ".XLSX"
            Else
                lstrFileName = lstrFileNameWOExt & ".XLS"
            End If
            lstrFile = lstrMassiveDir & lstrFileName

            lstrFiledelxls = lstrFile

            sMessage &= "5_"

            If lcolColsheets.Find(nId) Then
                sMessage &= "6_"

                '+Duplica el archivo con formato texto separado por tabuladores
                '+El archivo tiene el mismo nombre con la extensión TXT
                If Not insTransformationExcel(lstrFileName) Then
                    sMessage &= "14_"

                    GoTo insQueryinportExcel_Err
                End If
                sMessage &= "15_"

                lstrFile = lstrMassiveDir & lstrFileNameWOExt & ".TXT"
                lstrFiledeltxt = lstrFile

                lintColCount = lcolColsheets.Count()
                lintColMax = lintColCount - 1

                Dim lclsImages As eRemoteDB.Images
                lclsImages = New eRemoteDB.Images
                If lclsImages.AddTextClob(lstrFile, lstrFileNameWOExt & ".TXT", sKey, nId, nUsercode, "1") And nPreview = 1 Then
                    insQueryInportExcel = UpdateT_massive(sKey)
                    Call Add_tmp_cal013a(sKey, lstrFileName, 4)
                Else
                    '+Si se creo el archivo de texto
                    'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
                    If Len(Dir(lstrFiledeltxt, FileAttribute.Archive)) > 0 And lstrFiledeltxt <> String.Empty Then
                        sMessage &= "16_"

                        '+Se abre archivo de texto a procesar
                        On Error Resume Next
                        lintFileNum = FreeFile()
                        FileOpen(lintFileNum, lstrFile, OpenMode.Input)
                        If Err.Number Then
                            FileClose(lintFileNum)
                            FileOpen(lintFileNum, lstrFile, OpenMode.Input)
                        End If
                        On Error GoTo insQueryinportExcel_Err

                        '+Se lee la columna de titulos. No se cargan
                        lstrRow = LineInput(lintFileNum)
                        lblnContinue = True

                        '+Se redefine matriz al bloque máximo
                        'UPGRADE_ISSUE: As Variant was removed from ReDim lstrArray(lintColMax, ARRBLOCK) statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="19AFCB41-AA8E-4E6B-A441-A3E802E5FD64"'
                        ReDim lstrArray(lintColMax, ARRBLOCK)

                        lintRow = 0
                        lintColumn = 0

                        '+Se carga archivo texto a matriz
                        Do While Not EOF(lintFileNum) And lblnContinue
                            lstrRow = Replace(LineInput(lintFileNum), """", "")
                            '+Por si viene linea vacía
                            If Len(lstrRow) = 0 Or Len(lstrRow) = lintColMax Then
                                lblnContinue = False
                            Else
                                lstrArray_txt = Microsoft.VisualBasic.Split(lstrRow, vbTab)
                                lintColumn = 0
                                On Error Resume Next
                                For lintColumn = 0 To lintColMax
                                    lstrArray(lintColumn, lintRow) = lstrArray_txt(lintColumn)
                                Next
                                On Error GoTo insQueryinportExcel_Err
                                lintRow = lintRow + 1
                            End If
                            '+Si las filas llegaron al máximo disponible se agrega un bloque nuevo
                            If (lintRow Mod ARRBLOCK) = 0 Then
                                'UPGRADE_ISSUE: As Variant was removed from ReDim lstrArray(lintColMax, lintRow + ARRBLOCK) statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="19AFCB41-AA8E-4E6B-A441-A3E802E5FD64"'
                                ReDim Preserve lstrArray(lintColMax, lintRow + ARRBLOCK)
                            End If
                        Loop

                        FileClose(lintFileNum)

                        '+Se ajusta matriz a las filas leídas
                        If lintRow > 0 Then
                            'UPGRADE_ISSUE: As Variant was removed from ReDim lstrArray(lintColMax, lintRow - 1) statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="19AFCB41-AA8E-4E6B-A441-A3E802E5FD64"'
                            ReDim Preserve lstrArray(lintColMax, lintRow - 1)
                        End If

                        '+Se obtienen los valores límite de la matriz
                        lintRow_end = UBound(lstrArray, 2)
                        lintColumn_end = UBound(lstrArray)

                        lintRow = 0
                        lintColumn = 0

                        '+Se almacena llave a usar en el proceso
                        lclsMasiveCharge.sKey = sKey

                        '+Se procesa columna a columna
                        For Each lclsColsheet In lcolColsheets

                            '+Se almacenan campos que varian en cada columna
                            With lclsColsheet
                                lclsMasiveCharge.sField = .sField
                                lclsMasiveCharge.nSearch = Val(.sSelected)
                                If lclsMasiveCharge.nSearch = 0 Then
                                    lclsMasiveCharge.nSearch = 2
                                End If
                                lclsMasiveCharge.nColumns = lintColumn + 1
                                lclsMasiveCharge.sTable = .sTableName
                                lclsMasiveCharge.sValuesList = .sValuesList

                                '+Se obtiene tipo de dato de columna según primer caracter
                                lstrType = UCase(Mid(.sField, 1, 1))
                            End With

                            '+Si no es tipo fecha
                            If lstrType <> "D" Then
                                lintCount = 0
                                lclsMasiveCharge.sValue = ""
                                For lintRow = 0 To lintRow_end
                                    With lclsMasiveCharge
                                        lstrValue = lstrArray(lintColumn, lintRow)
                                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                        If IsDBNull(lstrValue) Then
                                            .sValue = .sValue & "|"
                                        Else
                                            .sValue = .sValue & "|" & lstrValue
                                        End If
                                    End With
                                    lintCount = lintCount + 1
                                    If lintCount = 250 Then
                                        lintCount = 0
                                        Call lclsMasiveCharge.Add()
                                        lclsMasiveCharge.sValue = ""
                                    End If
                                Next
                                If lclsMasiveCharge.sValue <> "" Then
                                    Call lclsMasiveCharge.Add()
                                End If
                                '+Si es tipo fecha
                            Else
                                lintCount = 0
                                lclsMasiveCharge.sValue = ""
                                For lintRow = 0 To lintRow_end

                                    lvntValue = lstrArray(lintColumn, lintRow)
                                    If Not IsDate(lvntValue) Then
                                        If Len(lvntValue) = 8 Then
                                            lstrValue = Mid(lvntValue, 7, 2) & "/" & Mid(lvntValue, 5, 2) & "/" & Mid(lvntValue, 1, 4)
                                            If IsDate(lstrValue) Then
                                                lstrValue = DateTime.Parse(lstrValue).ToString("dd/MM/yyyy")
                                            Else
                                                lstrValue = String.Empty
                                            End If
                                        Else
                                            lstrValue = String.Empty
                                        End If
                                    Else
                                        lstrValue = DateTime.Parse(lvntValue).ToString("dd/MM/yyyy")
                                    End If
                                    With lclsMasiveCharge
                                        .sValue = .sValue & "|" & lstrValue
                                    End With
                                    lintCount = lintCount + 1
                                    If lintCount = 250 Then
                                        lintCount = 0
                                        Call lclsMasiveCharge.Add()
                                        lclsMasiveCharge.sValue = ""
                                    End If
                                Next
                                If lclsMasiveCharge.sValue <> "" Then
                                    Call lclsMasiveCharge.Add()
                                End If
                            End If
                            lintRow = 0
                            lintColumn = lintColumn + 1
                        Next lclsColsheet
                    End If

                    insQueryInportExcel = UpdateT_massive(sKey)
                    Call Add_tmp_cal013a(sKey, lstrFileName, 4)
                End If

            End If
            sMessage &= "19_"
        Else
            lrecReaSheet = New eRemoteDB.Execute

            'Definición de parámetros para stored procedure 'insudb.CreColSheet'
            'Información leída el 05/02/2001 10:58:38 a.m.

            With lrecReaSheet
                .StoredProcedure = "GEN_TABLE_DINAMIC"
                .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 22, 0, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sFile", sFile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nSheet", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sSeparate", sSeparate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 22, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nRepinsured", nRepinsured, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sMassive", sMassive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 22, 0, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                insQueryInportExcel = .Run(False)
            End With
        End If

insQueryinportExcel_Err:
        If Err.Number Then
            sMessage &= "21_"

            insQueryInportExcel = False
            sMessage = sMessage & "[insQueryinportExcel]" & Err.Description
            sError = sMessage
        End If
        FileClose(lintFileNum)

        'UPGRADE_NOTE: Object lclsColsheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsColsheet = Nothing
        'UPGRADE_NOTE: Object lcolColsheets may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolColsheets = Nothing
        'UPGRADE_NOTE: Object lclsMasiveCharge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsMasiveCharge = Nothing
        'UPGRADE_NOTE: Object lclsvalue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsvalue = Nothing
        lrecReaSheet = Nothing


        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        'If Len(Dir(lstrFiledelxls, FileAttribute.Archive)) > 0 And lstrFiledelxls <> String.Empty Then
        '	On Error Resume Next
        '	Kill(lstrFiledelxls)
        '	On Error GoTo 0
        'End If
        ''UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        'If Len(Dir(lstrFiledeltxt, FileAttribute.Archive)) > 0 And lstrFiledeltxt <> String.Empty Then
        '	On Error Resume Next
        '	Kill(lstrFiledeltxt)
        '	On Error GoTo 0
        'End If
        On Error GoTo 0
    End Function
    '%Count(). Esta funcion devuelve la cantidad de Campos para una Plantilla en especifico
    Public Function Count(ByVal nId As Integer) As Integer

        Dim lrecReaCountColSheet As eRemoteDB.Execute

        lrecReaCountColSheet = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.CreColSheet'
        'Información leída el 05/02/2001 10:58:38 a.m.

        With lrecReaCountColSheet
            .StoredProcedure = "REACOUNTCOLSHEET"
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Count = .FieldToClass("nCount", 0)
                .RCloseRec()
            Else
                Count = 0
            End If
        End With

        'UPGRADE_NOTE: Object lrecReaCountColSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaCountColSheet = Nothing

    End Function

    '%insTransformationExcel(). Transforma el archivo excel para utilizar solo los valores 
    Public Function insTransformationExcel(ByVal sFile As String) As Boolean
        'Dim mvarSalidaExcel As Microsoft.Office.Interop.Excel.Application
        Dim lclsvalue As eFunctions.Values
        Dim lstrFileName As String
        Dim lintExist As Integer
        Dim lstrFile As String
        Dim lintlength As Integer
        Dim lintExistxlsx As Integer
        Dim lngFile As Long = 0

        On Error GoTo insTransformationExcel_Err

        lintExist = InStr(1, UCase(sFile), ".XLS")
        lintExistxlsx = InStr(1, UCase(sFile), ".XLSX")
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

        sMessage &= "7_"
        If lintExistxlsx <= 0 Then
            Dim ALines() As String
            Dim sLine As String = String.Empty
            Dim sSheet As String = String.Empty

            Using file As New FileStream(lstrFileName & Trim(lstrFile) & ".XLS", FileMode.Open, FileAccess.Read)
                Dim hssWorkbook As HSSFWorkbook
                hssWorkbook = New HSSFWorkbook(file)
                Dim hssExtractor As New ExcelExtractor(hssWorkbook)
                hssExtractor.IncludeBlankCells = True
                'hssExtractor.FormulasNotResults = True
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
            Using file As New FileStream(lstrFileName & lstrFile.Trim() & ".XLSX", FileMode.Open, FileAccess.Read)
                Dim wb As XSSFWorkbook
                lngFile = FreeFile()
                FileOpen(lngFile, lstrFileName & lstrFile.Trim() & ".TXT", OpenMode.Append)

                wb = New XSSFWorkbook(file)

                Dim sheet As ISheet = wb.GetSheetAt(0)
                Dim headerRow As IRow = sheet.GetRow(0)
                Dim sheetRows As IEnumerator = sheet.GetRowEnumerator()

                Dim colCount As Integer = headerRow.LastCellNum
                Dim rowCount As Integer = sheet.LastRowNum

                'Dim skipReadingHeaderRow As Boolean = sheetRows.MoveNext()
                Do While sheetRows.MoveNext()
                    Dim curRow As IRow = sheetRows.Current '(XSSFRow)
                    Dim line As String = String.Empty
                    For i As Integer = 0 To colCount - 1
                        Dim cell As ICell = curRow.GetCell(i)
                        If Not cell Is Nothing Then
                            Select Case cell.CellType
                                Case CellType.Numeric, CellType.Formula
                                    Dim cellDate As Date
                                    Dim style As ICellStyle
                                    Dim format As String
                                    If DateUtil.IsCellDateFormatted(cell) Then
                                        style = cell.CellStyle
                                        format = style.GetDataFormatString().Replace("m", "M")
                                        line = line & cell.DateCellValue.ToString(format) & Chr(9)
                                    Else
                                        line = line & cell.NumericCellValue & Chr(9)
                                        cellDate.ToString()
                                    End If
                                Case CellType.String
                                    line = line & cell.StringCellValue & Chr(9)
                                Case Else
                                    line = line & cell.ToString() & Chr(9)
                            End Select
                        Else
                            line = line & Chr(9)
                        End If
                    Next
                    line = line.Substring(0, line.Length - 1)
                    PrintLine(lngFile, line)
                Loop
                FileClose(lngFile)
            End Using
        End If
        insTransformationExcel = True

insTransformationExcel_Err:
        If Err.Number Then
            insTransformationExcel = False
            sMessage = sMessage & "[insTransformationExcel]" & Err.Description & vbCrLf
        End If
        On Error Resume Next
        'mvarSalidaExcel.ActiveWorkbook.Close()
        'mvarSalidaExcel.Quit()
        sMessage &= "10_"

        'UPGRADE_NOTE: Object mvarSalidaExcel may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        'mvarSalidaExcel = Nothing
        On Error GoTo 0
    End Function

    '%UpdateT_massive(). Esta funcion repara la tabla temporal T_MasiveCharge
    Public Function UpdateT_massive(ByVal sKey As String) As Boolean

        Dim lrecUpdateT_massive As eRemoteDB.Execute

        On Error GoTo UpdateT_massive_Err

        lrecUpdateT_massive = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.CreColSheet'
        'Información leída el 05/02/2001 10:58:38 a.m.

        With lrecUpdateT_massive
            .StoredProcedure = "UPDT_MASIVECHARGE_1"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UpdateT_massive = .Run(False)
        End With

        'UPGRADE_NOTE: Object lrecUpdateT_massive may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecUpdateT_massive = Nothing

UpdateT_massive_Err:
        If Err.Number Then
            UpdateT_massive = False
        End If
        On Error GoTo 0

    End Function

    '%Add_tmp_cal013a: Esta funcion se encarga de incluir un registro en la tabla ColSheet
    Public Function Add_tmp_cal013a(ByVal sKey As String, ByVal sFile As String, ByVal nAction As Short) As Boolean
        Dim lrecCreColSheet As eRemoteDB.Execute

        On Error GoTo Add_tmp_cal013a_Err
        lrecCreColSheet = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.CreColSheet'
        '+Información leída el 05/02/2001 10:58:38 a.m.

        With lrecCreColSheet
            .StoredProcedure = "cretmp_cal013a"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sFile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 120, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add_tmp_cal013a = .Run(False)
        End With

Add_tmp_cal013a_Err:
        If Err.Number Then
            Add_tmp_cal013a = False
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecCreColSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecCreColSheet = Nothing
    End Function

    Public ReadOnly Property CN_DATE() As Object
        Get

            CN_DATE = "DATE"

        End Get
    End Property

    Public ReadOnly Property CN_NUMBER() As Object
        Get

            CN_NUMBER = "NUMBER"

        End Get
    End Property

    Public ReadOnly Property CN_CHAR() As Object
        Get

            CN_CHAR = "CHAR"

        End Get
    End Property
    Public ReadOnly Property CN_VARCHAR2() As Object
        Get

            CN_VARCHAR2 = "VARCHAR2"

        End Get
    End Property
End Class






