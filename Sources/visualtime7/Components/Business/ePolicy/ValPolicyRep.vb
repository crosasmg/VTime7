Option Strict Off
Option Explicit On

Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.HSSF.Extractor

Public Class ValPolicyRep
	
	'%-------------------------------------------------------%'
	'% $Workfile:: ValPolicyRep.cls                         $%'
	'% $Author:: Nmoreno                                    $%'
	'% $Date:: 9-10-09 15:39                                $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	Public sKey As String
	Public P_SKEY As String
	Public sPolitype As String
	Public sFile_name As String
	
	
	'+[APV2]: HAD 1018. Conversión Automática de Propuesta a Póliza
	Private nBranch As Integer
	Private nProduct As Integer
	Private nProponum As Double
	Private bFirts_Premium As Boolean
	Private bCred_Prop_Cli As Boolean
	Public nNumCart As Double
    Public mstrFile As String
    Public sMessage As String
	
    Enum TypeStatus_Pol
        cstrValid = 1 'Valido
        cstrInvalid = 2 'Invalido
        cstrIncomplete = 3 'En captura incompleta
        cstrPrintPendent = 4 'Pendiente por impresión
        cstrPrinted = 5 'Impreso
        cstrAnnuled = 6 'Anulada
        cstrSaldProrr = 7 'Saldado prorrogado
        cstrRansom = 8 'Rescatada
    End Enum
    '%insQueryExportExcel: Exporta planilla excel
    Public Sub insQueryExportExcel(ByVal sKey As String, ByVal sFile As String)
        Dim lclsExcelApp As Microsoft.Office.Interop.Excel.Application
        Dim lclsWorksheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim lclsWorksheet_1 As Microsoft.Office.Interop.Excel.Worksheet
        Dim lintSheet As Integer
        Dim lintCounterSheet As Integer
        Dim lintColumn As Integer
        Dim lstrField As String

        Dim lrecRenewal As eRemoteDB.Execute
        lrecRenewal = New eRemoteDB.Execute
        Dim lclsvalue As eFunctions.Values
        Dim lstrFileName As String
        Dim lstrFile As String
        Dim lintExist As Integer
        Dim lintlength As Integer

        Dim lintValRow As Integer
        Dim lintValCol As Integer
        Dim lintReferCol As Integer

        '-Variables para almacenar mensajes        
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
            .Sheets(1).Visible = True
            .Sheets(1).Name = "Reporte de Renovacion"
        End With
        sMessage &= "234_ "

        lintCounterSheet = 2

        '+Se realiza la busqueda de los nombres de las columnas y de las hojas a crear        
        With lclsExcelApp
            .Workbooks(1).Sheets(1).Activate()
            lclsWorksheet = .Workbooks(1).Sheets(1)
        End With

        sMessage &= "3_ "

        lclsWorksheet.Cells(1, 1).Value = "Codigo de la Agencia"
        lclsWorksheet.Cells(1, 2).Value = "Agencia"
        lclsWorksheet.Cells(1, 3).Value = "Codigo del Ramo"
        lclsWorksheet.Cells(1, 4).Value = "Ramo"
        lclsWorksheet.Cells(1, 5).Value = "Codigo del Producto"
        lclsWorksheet.Cells(1, 6).Value = "Producto"
        lclsWorksheet.Cells(1, 7).Value = "Poliza"
        lclsWorksheet.Cells(1, 8).Value = "Certificado"
        lclsWorksheet.Cells(1, 9).Value = "Codigo del Contratante"
        'lclsWorksheet.Cells(1, 10).Select()
        lclsExcelApp.Columns._Default(9).Select()
        lclsExcelApp.Selection.NumberFormat = "@"
        lclsWorksheet.Cells(1, 10).Value = "Contratante"
        lclsWorksheet.Cells(1, 11).Value = "Codigo del Asegurado"
        lclsExcelApp.Columns._Default(11).Select()
        lclsExcelApp.Selection.NumberFormat = "@"
        lclsWorksheet.Cells(1, 12).Value = "Asegurado"
        lclsWorksheet.Cells(1, 13).Value = "Fecha de Inicio de Vigencia"
        lclsExcelApp.Columns._Default(13).Select()
        lclsExcelApp.Selection.NumberFormat = "yyyy/MM/dd"
        With lclsExcelApp.Selection.Validation
            .Delete()
            .Add(Type:=Microsoft.Office.Interop.Excel.XlDVType.xlValidateDate, AlertStyle:=Microsoft.Office.Interop.Excel.XlDVAlertStyle.xlValidAlertStop, Operator:=Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlGreater, Formula1:="01/01/1900")
            .IgnoreBlank = True
            .InCellDropdown = True
            .InputTitle = ""
            .ErrorTitle = ""
            .InputMessage = ""
            .ShowInput = True
            .ShowError = True
        End With
        lclsWorksheet.Cells(1, 14).Value = "Fecha de Fin de Vigencia"
        lclsExcelApp.Columns._Default(14).Select()
        lclsExcelApp.Selection.NumberFormat = "dd/MM/yyyy"
        With lclsExcelApp.Selection.Validation
            .Delete()
            .Add(Type:=Microsoft.Office.Interop.Excel.XlDVType.xlValidateDate, AlertStyle:=Microsoft.Office.Interop.Excel.XlDVAlertStyle.xlValidAlertStop, Operator:=Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlGreater, Formula1:="01/01/1900")
            .IgnoreBlank = True
            .InCellDropdown = True
            .InputTitle = ""
            .ErrorTitle = ""
            .InputMessage = ""
            .ShowInput = True
            .ShowError = True
        End With
        lclsWorksheet.Cells(1, 15).Value = "Codigo del APS"
        lclsWorksheet.Cells(1, 16).Value = "APS"
        lclsWorksheet.Cells(1, 17).Value = "Cobertura básica"
        lclsWorksheet.Cells(1, 18).Value = "Tasa de la cobertura básica"
        lclsWorksheet.Cells(1, 19).Value = "Suma asegurada"
        lclsWorksheet.Cells(1, 20).Value = "Prima neta"
        lclsWorksheet.Cells(1, 21).Value = "Prima pendiente de pago"
        lclsWorksheet.Cells(1, 22).Value = "Siniestralidad ultima vigencia"
        lclsWorksheet.Cells(1, 23).Value = "Numero de siniestro última vigencia"
        lclsWorksheet.Cells(1, 24).Value = "Siniestralidad histórica"
        lclsWorksheet.Cells(1, 25).Value = "Numero de siniestro histórica"
        lclsWorksheet.Cells(1, 26).Value = "Indicador de reaseguro Facultativo"
        lclsWorksheet.Cells(1, 27).Value = "Comentario"
        With lrecRenewal
            .StoredProcedure = "REATRENEWEXCEL"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                lintColumn = 2
                Do While Not .EOF
                    lclsWorksheet.Cells(lintColumn, 1).Value = .FieldToClass("nAgency")
                    lclsWorksheet.Cells(lintColumn, 2).Value = .FieldToClass("sAgency")
                    lclsWorksheet.Cells(lintColumn, 3).Value = .FieldToClass("nBranch")
                    lclsWorksheet.Cells(lintColumn, 4).Value = .FieldToClass("sBranch")
                    lclsWorksheet.Cells(lintColumn, 5).Value = .FieldToClass("nProduct")
                    lclsWorksheet.Cells(lintColumn, 6).Value = .FieldToClass("sProduct")
                    lclsWorksheet.Cells(lintColumn, 7).Value = .FieldToClass("nPolicy")
                    lclsWorksheet.Cells(lintColumn, 8).Value = .FieldToClass("nCertif")
                    lclsWorksheet.Cells(lintColumn, 9).Value = .FieldToClass("sClicont")
                    lclsWorksheet.Cells(lintColumn, 10).Value = .FieldToClass("sCont")
                    lclsWorksheet.Cells(lintColumn, 11).Value = .FieldToClass("sCliaseg")
                    lclsWorksheet.Cells(lintColumn, 12).Value = .FieldToClass("sAseg")
                    lclsWorksheet.Cells(lintColumn, 13).Value = .FieldToClass("dStartdate")
                    lclsWorksheet.Cells(lintColumn, 14).Value = .FieldToClass("dExpirdat")
                    lclsWorksheet.Cells(lintColumn, 15).Value = .FieldToClass("nIntermed")
                    lclsWorksheet.Cells(lintColumn, 16).Value = .FieldToClass("sIntermed")
                    lclsWorksheet.Cells(lintColumn, 17).Value = .FieldToClass("sCover")
                    lclsWorksheet.Cells(lintColumn, 18).Value = .FieldToClass("nRatecov")
                    lclsWorksheet.Cells(lintColumn, 19).Value = .FieldToClass("nCapital")
                    lclsWorksheet.Cells(lintColumn, 20).Value = .FieldToClass("nPremium")
                    lclsWorksheet.Cells(lintColumn, 21).Value = .FieldToClass("nPendingPremium")
                    lclsWorksheet.Cells(lintColumn, 27).Value = .FieldToClass("sComment")
                    lintColumn = lintColumn + 1
                    .RNext()
                Loop
                .RCloseRec()
            End If
        End With
        lclsExcelApp.DisplayAlerts = False
        sMessage &= "5 antes de salvar_ "

        lclsExcelApp.ActiveWorkbook.SaveAs(lstrFileName, 56)
        sMessage &= "6 despues de  "

        lclsvalue = Nothing

        lclsExcelApp.ActiveWorkbook.Close()
        lclsExcelApp.Quit()

        lclsWorksheet = Nothing
        lclsWorksheet_1 = Nothing
        lclsExcelApp = Nothing

    End Sub
    '% insValVAL708_k: Se valida cálculo de interes por prestamo
    Public Function insValVAL708_k(ByVal nYear As Integer, ByVal nMonth As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValVAL708_k_Err
		
		'+ Se valida el campo año
		If nYear <= 0 Then
			Call lobjErrors.ErrorMessage("VAL708", 9060)
		End If
		
		'+ Se validar el campo mes
		If nMonth <= 0 Then
			Call lobjErrors.ErrorMessage("VAL708", 60267)
		Else
			If nMonth > 12 Then
				Call lobjErrors.ErrorMessage("VAL708", 60290)
			End If
		End If
		
		insValVAL708_k = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValVAL708_k_Err: 
		If Err.Number Then
			insValVAL708_k = insValVAL708_k & Err.Description
		End If
		On Error GoTo 0
	End Function

	'% insValCAL782_k: Se valida las pólizas pendientes de impresión/cuponera
	Public Function insValCAL782_k(ByVal dStart As Date, ByVal dEnd As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValCAL782_k_Err
		
		'+ Se valida el campo fecha de inicio
		If dStart = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage("CAL782", 5072)
		End If
		
		'+ Se validar el campo fecha final
		If dEnd = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage("CAL782", 9072)
		End If
		
		insValCAL782_k = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValCAL782_k_Err: 
		If Err.Number Then
			insValCAL782_k = insValCAL782_k & Err.Description
		End If
		On Error GoTo 0
		
	End Function
    '% insValCAL826_k: Validaciones proceso de calculo de la prima ganada incobrable
    Public Function insValCAL826_K(ByVal dStart As Date, ByVal dEnd As Date) As String
        Dim lobjErrors As eFunctions.Errors

        lobjErrors = New eFunctions.Errors

        On Error GoTo insValCAL826_K_Err

        '+ Se valida el campo fecha de inicio
        If dStart = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("CAL826", 3237)
        End If

        '+ Se validar el campo fecha final
        If dEnd = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("CAL826", 3239)
        End If

        If dStart <> eRemoteDB.Constants.dtmNull And dEnd <> eRemoteDB.Constants.dtmNull And dStart > dEnd Then
            Call lobjErrors.ErrorMessage("CAL826", 1132)
        End If

        insValCAL826_K = lobjErrors.Confirm

insValCAL826_K_Err:
        If Err.Number Then
            insValCAL826_K = insValCAL826_K & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '% insValcal600_k: Validaciones correspondientes al reporte CAL600: Reporte de compras y ventas de unidades                     
    Public Function insValcal600_k(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String

        Dim lobjErrors As eFunctions.Errors

        Dim lclsCertificat As Certificat

        lobjErrors = New eFunctions.Errors

        lclsCertificat = New Certificat

        On Error GoTo insValcal600_k_Err

        '+ Se valida el campo fecha de efecto
        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("cal600", 4003)
        End If


        insValcal600_k = lobjErrors.Confirm

insValcal600_k_Err:
        If Err.Number Then
            insValcal600_k = insValcal600_k & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

    End Function

	'% insValCAL970_k: SE VALIDA ALGO
	Public Function insValCAL970_k(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As String
		
		Dim lobjErrors As eFunctions.Errors
		
		Dim lclsCertificat As Certificat
		
		lobjErrors = New eFunctions.Errors
		
		lclsCertificat = New Certificat
		
		On Error GoTo insValCAL970_k_Err
		
		'+ Se valida el campo fecha de efecto
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage("CAL970", 4003)
		End If
		
		'+ Se valida el campo ramo comercial
		If nBranch <= 0 Then
			Call lobjErrors.ErrorMessage("CAL970", 1022)
		End If
		
		nCertif = IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif)
		
		If nBranch > 0 And nProduct > 0 And nPolicy > 0 And nCertif >= 0 Then
			If lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif, True) Then
				If lclsCertificat.sStatusva = "3" Or lclsCertificat.sStatusva = "2" Then
					Call lobjErrors.ErrorMessage("CAL970", 3724)
				End If
			Else
				Call lobjErrors.ErrorMessage("CAL970", 1978)
			End If
		End If
		
		insValCAL970_k = lobjErrors.Confirm
		
insValCAL970_k_Err: 
		If Err.Number Then
			insValCAL970_k = insValCAL970_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
	End Function
	'% insValVIL900_k: Se valida la Fecha Efecto para el calculo del DEF
	Public Function insValVIL900_k(ByVal dEffecdate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValVIL900_k_Err
		
		'+ Se valida el campo fecha de Efecto
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage("VIL900", 7114)
		End If
		
		insValVIL900_k = lobjErrors.Confirm
		
		
		
insValVIL900_k_Err: 
		If Err.Number Then
			insValVIL900_k = insValVIL900_k & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
		
	End Function
    '% insValCAL970_k: SE VALIDA ALGO
    Public Function insValCAL980_k(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal dEffecdate As Date, ByVal dExpirdat As Date) As String

        Dim lobjErrors As eFunctions.Errors

        Dim lclsCertificat As Certificat

        lobjErrors = New eFunctions.Errors

        lclsCertificat = New Certificat

        On Error GoTo insValCAL980_k_Err

        '+ Se valida el campo fecha desde sea menor o igual al campo fecha hasta
        If dEffecdate <> eRemoteDB.Constants.dtmNull And dExpirdat <> eRemoteDB.Constants.dtmNull Then
            If dEffecdate > dExpirdat Then
                Call lobjErrors.ErrorMessage("CAL980", 6130)
            End If
        End If

        If nBranch > 0 And nProduct > 0 And nPolicy > 0 Then
            If lclsCertificat.Find("2", nBranch, nProduct, nPolicy, 0, True) Then
                If lclsCertificat.sStatusva = "3" Or lclsCertificat.sStatusva = "2" Then
                    Call lobjErrors.ErrorMessage("CAL980", 3724)
                End If
            Else
                Call lobjErrors.ErrorMessage("CAL980", 1978)
            End If
        End If

        insValCAL980_k = lobjErrors.Confirm

insValCAL980_k_Err:
        If Err.Number Then
            insValCAL980_k = insValCAL980_k & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

    End Function
    '% insValVIL7920_K - Certificado 7
    Public Function insValVIL1890_K(ByVal sCodispl As String, ByVal sProcessType As String, ByVal nYear As Integer, ByVal nDecType As Integer, ByVal nRectif As Integer, ByVal sClient As String, ByVal dPrintDate As Date) As String
        Dim lobjErrors As eFunctions.Errors

        lobjErrors = New eFunctions.Errors

        On Error GoTo insValVIL1890_K_Err

        '+ Se valida fecha de impresión
        If dPrintDate = eRemoteDB.Constants.dtmNull Then
            lobjErrors.ErrorMessage(sCodispl, 55546)
        End If

        '+ Se valida el año
        If nYear = eRemoteDB.Constants.intNull Then
            lobjErrors.ErrorMessage(sCodispl, 70131)
        End If

        '+ Se valida rectificatoria
        If nDecType = 2 And nRectif <= 0 Then
            lobjErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Rectificatoria:")
        End If

        '+ Se valida el cliente
        If sProcessType = "2" And sClient = String.Empty Then
            lobjErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Cliente:")
        End If

        insValVIL1890_K = lobjErrors.Confirm

insValVIL1890_K_Err:
        If Err.Number Then
            insValVIL1890_K = insValVIL1890_K & Err.Description
        End If
        lobjErrors = Nothing
        On Error GoTo 0

    End Function

    '% insPostCAL826_K: Se realiza proceso de calculo de la prima ganada incobrable
    Public Function insPostCAL826_K(ByVal dStart As Date, ByVal dEnd As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nUsercode As Integer, ByVal sProcessType As String) As Boolean
		Dim lrecinsPostCAL826_K As eRemoteDB.Execute
		
		On Error GoTo insPostCAL826_K_Err
		
		lrecinsPostCAL826_K = New eRemoteDB.Execute
		
		With lrecinsPostCAL826_K
			.StoredProcedure = "insPostCAL826_K"
			.Parameters.Add("dStart", dStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd", dEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProcessType", sProcessType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCAL826_K = .Run(False)
			If insPostCAL826_K Then
				sKey = .Parameters("sKey").Value
			End If
		End With
		
insPostCAL826_K_Err: 
		If Err.Number Then
			insPostCAL826_K = False
		End If
		'UPGRADE_NOTE: Object lrecinsPostCAL826_K may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL826_K = Nothing
		On Error GoTo 0
	End Function
	
	
	
	'% insPostCAL872:
	Public Function insPostCAL872(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal lintUsercode As Integer, ByVal lintCompany As Integer) As Boolean
		
		Dim lrecinsPostCAL872 As eRemoteDB.Execute
		
		lrecinsPostCAL872 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCAL872
			.StoredProcedure = "rea_cal872"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercomp", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCAL872 = True
			Else
				insPostCAL872 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAL872 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL872 = Nothing
		
	End Function
	
	
	'% insValCAL872: Valida si la poliza ingresada es colectiva y tiene facturación por certificado
	Public Function insValCAL872(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		
		Dim lrecinsValCAL872 As eRemoteDB.Execute
		
		lrecinsValCAL872 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsValCAL872
			.StoredProcedure = "reapolicy_colec"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insValCAL872 = True
			Else
				insValCAL872 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsValCAL872 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValCAL872 = Nothing
		
	End Function
	'% insValCAL908:
	Public Function insValCAL908(ByVal sCodispl As String, ByVal sClient As String, ByVal nPolicy As Double) As String
		
		Dim lclsClient As eClient.Client
		Dim lclsPolicy As ePolicy.Policy
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		lclsClient = New eClient.Client
		lclsPolicy = New ePolicy.Policy
		
		insValCAL908 = String.Empty
		On Error GoTo insValCAL908_Err
		
		If nPolicy = eRemoteDB.Constants.intNull And sClient = "" Then
			Call lobjErrors.ErrorMessage("CAL908", 60105)
		End If
		
		If sClient <> "" Then
			If Not lclsClient.Find(sClient) Then
				Call lobjErrors.ErrorMessage(sCodispl, 2044)
			End If
		End If
		
		If nPolicy <> eRemoteDB.Constants.intNull Then
			If Not lclsPolicy.FindPolPropbyPolicy(nPolicy) Then
				Call lobjErrors.ErrorMessage(sCodispl, 55683)
			End If
		End If
		
		insValCAL908 = lobjErrors.Confirm
		
insValCAL908_Err: 
		If Err.Number Then
			insValCAL908 = "insValCAL908 : " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
	End Function
	'% VT00015 GAP 10 Historial del Asegurado
	Public Function insValCAL00975(ByVal sCodispl As String, ByVal sClient As String, ByVal dEndDate As Date) As String
		
		
		Dim lobjErrors As eFunctions.Errors
		lobjErrors = New eFunctions.Errors
		
		insValCAL00975 = String.Empty
		
		On Error GoTo insValPolicy_Err
		
		If sClient = String.Empty Then
			Call lobjErrors.ErrorMessage(sCodispl, 2001)
		End If
		'+ Si la fecha final es diferente de vacio continua las validaciones
		If dEndDate = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 4245)
		End If
		
		'+ Se verifica que la fecha final no sea mayor a la fecha del día
		If dEndDate > Today Then
			Call lobjErrors.ErrorMessage(sCodispl, 4341)
		End If
		
		insValCAL00975 = lobjErrors.Confirm
		
insValPolicy_Err: 
		If Err.Number Then
			insValCAL00975 = "insValCAL00975 : " & Err.Description
		End If
	End Function
	'% VT00015 GAP 10 Historial del Asegurado
	Public Function insValCAL00976(ByVal sCodispl As String, ByVal dIniDate As Date, ByVal dEndDate As Date) As String
		
		
		Dim lobjErrors As eFunctions.Errors
		lobjErrors = New eFunctions.Errors
		
		insValCAL00976 = String.Empty
		
		On Error GoTo insValPolicy_Err
		
		'+ Si la fecha Inicial es diferente de vacio continua las validaciones
		If dIniDate = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 9071)
		End If
		'+ Si la fecha final es diferente de vacio continua las validaciones
		If dEndDate = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 9072)
		End If
		'+ Se verifica que que la fecha final sea mayor a la fecha inicial
		If dEndDate < dIniDate Then
			Call lobjErrors.ErrorMessage(sCodispl, 4159)
		End If
		'+ Se verifica que la fecha final no sea mayor a la fecha del día
		If dEndDate > Today Then
			Call lobjErrors.ErrorMessage(sCodispl, 4341)
		End If
		
		insValCAL00976 = lobjErrors.Confirm
		
insValPolicy_Err: 
		If Err.Number Then
			insValCAL00976 = "insValCAL00976 : " & Err.Description
		End If
	End Function
	
	
	'% insPostCOL870:
	Public Function insPostCOL870(ByVal dDateFrom As Date, ByVal ddateto As Date, ByVal sindren As String, ByVal lintUsercode As Integer, ByVal lintCompany As Integer) As Boolean
		
		Dim lrecinsPostCOL870 As eRemoteDB.Execute
		
		lrecinsPostCOL870 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCOL870
			.StoredProcedure = "rea_col870"
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", ddateto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sindren", sindren, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercomp", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCOL870 = True
			Else
				insPostCOL870 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCOL870 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCOL870 = Nothing
		
	End Function
	'% insPostCOL906:
	Public Function insPostCOL906(ByVal dDateInit As Date, ByVal dDateEnd As Date) As Boolean
		
		Dim lrecinsPostCOL906 As eRemoteDB.Execute
		
		lrecinsPostCOL906 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.INSUPDLCTACTICTACONT'
		With lrecinsPostCOL906
			.StoredProcedure = "INSUPDLCTACTICTACONT"
			.Parameters.Add("ddateinit", dDateInit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ddateend", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCOL906 = True
			Else
				insPostCOL906 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCOL906 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCOL906 = Nothing
		
	End Function
	'% insPostCOL987:
	Public Function insPostCOL987(ByVal dDateLimit As Date, ByVal lintUsercode As Integer, ByVal lintCompany As Integer) As Boolean
		
		Dim lrecinsPostCOL987 As eRemoteDB.Execute
		
		lrecinsPostCOL987 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCOL987
			.StoredProcedure = "rea_col987"
			.Parameters.Add("dlimitdate", dDateLimit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercomp", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCOL987 = True
			Else
				insPostCOL987 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCOL987 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCOL987 = Nothing
		
	End Function
	'% insPostCAC940:
	Public Function insPostCAC940(ByVal sCertype As String, ByVal nPolicy As Double) As Boolean
		
		Dim lrecinsPostCAC940 As eRemoteDB.Execute
		
		lrecinsPostCAC940 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.INSUPDLCTACTICTACONT'
		With lrecinsPostCAC940
			.StoredProcedure = "rea_cac940"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("SKEY").Value
				insPostCAC940 = True
			Else
				insPostCAC940 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAC940 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAC940 = Nothing
		
	End Function
	
	'% insPostCAC988:
	Public Function insPostCAC988(ByVal nPolicy As Double) As Boolean
		
		Dim lrecinsPostCAC988 As eRemoteDB.Execute
		
		lrecinsPostCAC988 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.INSUPDLCTACTICTACONT'
		With lrecinsPostCAC988
			.StoredProcedure = "INSUPDCONGRAL_CONPOLRAMFC"
			.Parameters.Add("npolicy_par", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCAC988 = True
			Else
				insPostCAC988 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAC988 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAC988 = Nothing
		
	End Function
	
	'% insPostCAC1014:
	Public Function insPostCAC1014(ByVal nPolicy As Double) As Boolean
		
		Dim lrecinsPostCAC1014 As eRemoteDB.Execute
		
		lrecinsPostCAC1014 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCAC1014
			.StoredProcedure = "rea_cac1014"
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCAC1014 = True
			Else
				insPostCAC1014 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAC1014 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAC1014 = Nothing
		
	End Function
	
	
	'% insPostVIL1078:
	Public Function insPostVIL1078(ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal dDateFrom As Date, ByVal ddateto As Date) As Boolean
		
		Dim lrecinsPostVIL1078 As eRemoteDB.Execute
		
		lrecinsPostVIL1078 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostVIL1078
			.StoredProcedure = "rea_vil1078"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ddatefrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ddateto", ddateto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostVIL1078 = True
			Else
				insPostVIL1078 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostVIL1078 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostVIL1078 = Nothing
		
	End Function
	
	
	'% Find_Proponum: verifica si la propuesta se encuentra registrada para otra póliza
	Public Function InsValVIL1078(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecPolicy As eRemoteDB.Execute
		
		On Error GoTo ValPolBranch_Err
		
		lrecPolicy = New eRemoteDB.Execute
		
		With lrecPolicy
			.StoredProcedure = "reapolicybranch"
			.Parameters.Add("scertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				InsValVIL1078 = True
			End If
		End With
		
ValPolBranch_Err: 
		If Err.Number Then
			InsValVIL1078 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPolicy = Nothing
	End Function
	
	'% Validación de los campos de la ventana del reporte VIL7700
	Public Function insValVIL7700(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dInitialDate As Date, ByVal dFinalDate As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPolicy As Policy
        Dim lclsProduct As eProduct.Product
		
		On Error GoTo insValVIL770_err
		
		lclsErrors = New eFunctions.Errors
		lclsPolicy = New Policy
        lclsProduct = New eProduct.Product
		
        If dInitialDate = System.DateTime.FromOADate(eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 70182)
		End If
		
        If dFinalDate = System.DateTime.FromOADate(eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 70183)
		End If
		
        '* Validación de transacción *'
        If nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull And dFinalDate <> System.DateTime.FromOADate(eRemoteDB.Constants.intNull) And nBranch <> 0 And nBranch <> eRemoteDB.Constants.intNull Then
            With lclsProduct
                If .FindProduct_li(nBranch, nProduct, dFinalDate) Then
                    '*Validación de producto distintos de APV*'
                    If .sApv = "1" Then
                        Call lclsErrors.ErrorMessage(sCodispl, 10000005)
                    End If
                End If
            End With
        End If

		insValVIL7700 = lclsErrors.Confirm
		
insValVIL770_err: 
		If Err.Number Then
			insValVIL7700 = "insValVIL7700 " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
	End Function

    '% insPostVIL7700: Ejecuta el SP que llena las temporales para ejecutar el reporte
    Public Function insPostVIL7700(ByVal nCartPol As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dInitial_date As Date, ByVal dFinal_date As Date, ByVal nUsercode As Integer) As Boolean

        Dim lrecreavil7700 As eRemoteDB.Execute
        Dim lstrKey As String

        On Error GoTo reavil7700_Err

        lrecreavil7700 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'reavil7700'
        '+ Información leída el: 16/09/2003

        With lrecreavil7700
            .StoredProcedure = "Cartola_vul_pkg.Generate_Cartol"
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nNumCart", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDate", dInitial_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEndDate", dFinal_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                nNumCart = .Parameters("nNumCart").Value
                sKey = .Parameters("sKey").Value
                insPostVIL7700 = True
            End If
        End With

reavil7700_Err:
        If Err.Number Then
            insPostVIL7700 = False
        End If

        'UPGRADE_NOTE: Object lrecreavil7700 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreavil7700 = Nothing
        On Error GoTo 0
    End Function

    '% Validación de los campos de la ventana del reporte VIL1486
    Public Function insValVIL1486(ByVal sCodispl As String, ByVal sCompanyType As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dInitDate As Date, ByVal dEndDate As Date) As String

        Dim lclsProduct As eProduct.Product
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lobjErrors As Object
        Dim lobjValues As Object
        Dim lblnError As Boolean
        Dim ldtmDate As Date

        On Error GoTo ErrorHandler

        lclsProduct = New eProduct.Product
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        lobjValues = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Values")

        lblnError = False

        '**+ Validate the field Product.
        '+ Se valida el campo Producto.

        If nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull Then
            If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
                Call lobjErrors.ErrorMessage(sCodispl, 70137)
            Else
                lobjValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                '*Validación de productos registrados*'
                If Not lobjValues.IsValid("tabProdmaster1", CStr(nProduct), True) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 9066)

                    lblnError = True
                Else

                    '**+ Validate that the product corresponds to life or combined
                    '+ Se valida que el producto corresponda a vida o combinado
                    With lclsProduct
                        Call .insValProdMaster(nBranch, nProduct)

                        If .blnError Then
                            '*Validación de productos de vida*'
                            If CStr(.sBrancht) <> "1" And CStr(.sBrancht) <> "2" And CStr(.sBrancht) <> "5" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3403)

                                lblnError = True
                            Else
                                If dEndDate <> eRemoteDB.Constants.dtmNull Then
                                    If .FindProduct_li(nBranch, nProduct, dEndDate) Then
                                        '*Validación de vida universal o unit linked*'
                                        If .nProdClas <> 4 Then
                                            Call lobjErrors.ErrorMessage(sCodispl, 70123)
                                        End If

                                        '*Validación de producto de APV*'
                                        If .sApv <> "1" Then
                                            Call lobjErrors.ErrorMessage(sCodispl, 767126)
                                        End If
                                    Else
                                        '*Validación de vida universal o unit linked
                                        Call lobjErrors.ErrorMessage(sCodispl, 70123)
                                    End If
                                Else
                                    Call lobjErrors.ErrorMessage(sCodispl, 3239)
                                End If
                            End If
                        End If
                    End With
                End If
            End If
        Else
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
        End If

        '**+ Validate the field Policy
        '+ Se valida el campo Póliza.

        If Not lblnError Then
            If nPolicy <> 0 And nPolicy <> eRemoteDB.Constants.intNull Then
                If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
                    Call lobjErrors.ErrorMessage(sCodispl, 70138)
                    '**+ Validate that it is valid policy.
                    '+ Se valida que sea una póliza válida.
                Else
                    With lclsPolicy
                        If Not .FindPolicyOfficeName("2", nBranch, nProduct, nPolicy, sCompanyType) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3001)

                            lblnError = True
                        Else
                            If .sStatus_pol = CStr(TypeStatus_Pol.cstrIncomplete) Or .sStatus_pol = CStr(TypeStatus_Pol.cstrInvalid) Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3720)

                                lblnError = True
                            Else
                                '**+ Verify that the policy is not anulled
                                '+ Verificar que la póliza no esté anulada

                                If .dNulldate <> eRemoteDB.Constants.dtmNull Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3098)

                                    lblnError = True
                                End If
                            End If
                        End If
                    End With
                End If
            Else
                Call lobjErrors.ErrorMessage(sCodispl, 21033)
            End If
        End If

        '**+ Validate the field Certificate.
        '+Se valida el campo Certificado.

        If Not lblnError Then
            If nCertif <> 0 And nCertif <> eRemoteDB.Constants.intNull Then
                If (nPolicy = 0 Or nPolicy = eRemoteDB.Constants.intNull) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 70139)
                Else
                    With lclsCertificat
                        If Not .Find("2", nBranch, nProduct, nPolicy, nCertif) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3010)

                            lblnError = True
                        Else
                            '**+ Validate that the certificate is valid
                            '+ Se válida que el certificado sea válido
                            If .sStatusva = "3" Or .sStatusva = "2" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 750044)

                                lblnError = True
                            Else
                                If .dNulldate <> eRemoteDB.Constants.dtmNull Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3099)

                                    lblnError = True
                                End If
                            End If
                        End If
                    End With
                End If
            Else
                If lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
                    If lclsPolicy.sPolitype <> "1" Then
                        If nCertif = eRemoteDB.Constants.intNull Or nCertif = 0 Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3200)
                        End If
                    End If
                End If
            End If
        End If

        '**+ The field date from must be full
        '+ El campo fecha desde debe estar lleno.
        If dInitDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3237)
        End If

        '**+ The field date to must be full.
        '+ El campo fecha hasta debe estar lleno.
        If dEndDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3239)
        Else
            If dEndDate < dInitDate Then
                Call lobjErrors.ErrorMessage(sCodispl, 11425)
            End If
        End If

        insValVIL1486 = lobjErrors.Confirm

        lclsProduct = Nothing
        lclsPolicy = Nothing
        lclsCertificat = Nothing
        lobjErrors = Nothing
        lobjValues = Nothing

        Exit Function
ErrorHandler:
        lclsProduct = Nothing
        lclsPolicy = Nothing
        lclsCertificat = Nothing
        lobjErrors = Nothing
        lobjValues = Nothing
    End Function


    '% insPostVIL1486: Ejecuta el SP que llena las temporales para ejecutar el reporte
    Public Function insPostVIL1486(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dInitial_date As Date, ByVal dFinal_date As Date, ByVal nUsercode As Integer) As Boolean
        Dim lrecreavil1486 As eRemoteDB.Execute
        Dim lstrKey As String

        On Error GoTo reavil1486_Err

        lrecreavil1486 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'Cartola_vul_pkg.Generate_Cartol'
        '+ Información leída el: 06/11/2013

        With lrecreavil1486
            .StoredProcedure = "REAPOLICYCARTAPVPKG.GENERATE_CARTOL"
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nNumCart", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDate", dInitial_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEndDate", dFinal_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", "VIL1486", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDirectory", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, , , , eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                nNumCart = .Parameters("nNumCart").Value
                sKey = .Parameters("sKey").Value
                insPostVIL1486 = True
            End If
        End With

reavil1486_Err:
        If Err.Number Then
            insPostVIL1486 = False
        End If

        'UPGRADE_NOTE: Object lrecreavil7700 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreavil1486 = Nothing

        On Error GoTo 0
    End Function



    '% Validación de los campos de la ventana
    '+[APV2]: HAD 1018. Conversión Automática de Propuesta a Póliza
    Public Function insValVIL7701(ByVal sCodispl As String, ByVal sTypeIM As String, ByVal nParamBranch As Integer, ByVal nParamProduct As Integer, ByVal nParamProponum As Integer) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValVIL7701_err
		
		lclsErrors = New eFunctions.Errors
		
		'+Si la forma de corrida es individual se validan los siguientes campos
		If sTypeIM = "1" Then
			
			nBranch = nParamBranch
			nProduct = nParamProduct
			nProponum = nParamProponum
			
			If nBranch = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 1022)
			End If
			
			If nProduct = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 1014)
			End If
			
			If nProponum = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 3789)
			End If
			
			Call insValidation()
			
			If Not bFirts_Premium Then
				Call lclsErrors.ErrorMessage(sCodispl, 70179)
			End If
			
			If Not bCred_Prop_Cli Then
				Call lclsErrors.ErrorMessage(sCodispl, 70180)
			End If
			
		End If
		
		insValVIL7701 = lclsErrors.Confirm
		
insValVIL7701_err: 
		If Err.Number Then
			insValVIL7701 = "insValVIL7701 " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% ValFirst_Premium: Validar que la poliza a convertir se encuentre en estado
	'% captura incompleta por falta de primera prima y que exsta un credito en la cuenta
	'%corriente para el Pagador de la poliza
	'+[APV2]: HAD 1018. Conversión Automática de Propuesta a Póliza
	Private Sub insValidation()
		
		Dim lrecinsValidation As eRemoteDB.Execute
		
		On Error GoTo insValidation_Err
		
		lrecinsValidation = New eRemoteDB.Execute
		
		With lrecinsValidation
			.StoredProcedure = "INSVIL7701PKG.INSVALVIL7701"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFirts_Premium", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCred_Prop_Cli", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				bFirts_Premium = IIf(.Parameters("nFirts_Premium").Value = 0, False, True)
				bCred_Prop_Cli = IIf(.Parameters("nCred_Prop_Cli").Value = 0, False, True)
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsValidation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValidation = Nothing
		
insValidation_Err: 
		'UPGRADE_NOTE: Object lrecinsValidation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValidation = Nothing
		On Error GoTo 0
	End Sub
	
	Public Function insPostVil7701(ByVal sInd_mass As String, ByVal sPre_def As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nUsercode As Integer, Optional ByVal nPropoNumber As Integer = eRemoteDB.Constants.intNull) As Boolean
		
		Dim lrecRea_VIL7701 As eRemoteDB.Execute
		
		On Error GoTo insPostVil7701_err
		
		lrecRea_VIL7701 = New eRemoteDB.Execute
		
		With lrecRea_VIL7701
			.StoredProcedure = "INSVIL7701PKG.INSVIL7701"
			.Parameters.Add("STYPEPD", sPre_def, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("STYPEIM", sInd_mass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NBRANCH", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NPRODUCT", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NPROPONUM", nPropoNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NUSERCODE", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SKEY_AUX", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				sKey = .Parameters("sKey_aux").Value
				If sKey > String.Empty Then
					insPostVil7701 = True
				Else
					insPostVil7701 = False
				End If
			End If
		End With
		
insPostVil7701_err: 
		If Err.Number Then
			insPostVil7701 = False
		End If
		'UPGRADE_NOTE: Object lrecRea_VIL7701 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRea_VIL7701 = Nothing
	End Function
	
	'insValAML884
	Public Function insValAML884(ByVal sCertype As String, ByVal nPolicy As Double, ByVal sClient As String) As Boolean
		
		Dim flag As String
		
		Dim lrecinsValAML884 As eRemoteDB.Execute
		
		On Error GoTo lrecinsValAML884_err
		
		lrecinsValAML884 = New eRemoteDB.Execute
		
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsValAML884
			.StoredProcedure = "REAPOLICY_NUMCOTIZ"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("flag", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				flag = .Parameters("flag").Value
				If flag = "0" Then
					insValAML884 = True
				Else
					insValAML884 = False
				End If
			End If
			
		End With
		
lrecinsValAML884_err: 
		If Err.Number Then
			insValAML884 = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsValAML884 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValAML884 = Nothing
		
	End Function
	
	Public Function insPostCAL1110(ByVal dDateFrom As Date, ByVal ddateto As Date) As Boolean
		
		Dim lrecinsPostCAL1110 As eRemoteDB.Execute
		
		lrecinsPostCAL1110 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCAL1110
			.StoredProcedure = "rea_cal1110"
			.Parameters.Add("ddatefrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ddateto", ddateto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCAL1110 = True
			Else
				insPostCAL1110 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAL1110 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL1110 = Nothing
		
	End Function
	
	
	Public Function insPostCAL1151(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		
		Dim lrecinsPostCAL1151 As eRemoteDB.Execute
		
		lrecinsPostCAL1151 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCAL1151
			.StoredProcedure = "rea_cal1151"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostCAL1151 = True
				P_SKEY = .Parameters("P_SKEY").Value
			Else
				insPostCAL1151 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAL1151 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL1151 = Nothing
		
	End Function
	
	Public Function insPostCAL1156(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		
		Dim lrecinsPostCAL1156 As eRemoteDB.Execute
		
		lrecinsPostCAL1156 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCAL1156
			.StoredProcedure = "rea_cal1156"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostCAL1156 = True
				P_SKEY = .Parameters("P_SKEY").Value
			Else
				insPostCAL1156 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAL1156 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL1156 = Nothing
		
	End Function
	
	
	'% insValPolColec: Valida si la poliza ingresada es colectiva
	Public Function insValPolColec(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		
		Dim lrecinsValPolColec As eRemoteDB.Execute
		
		lrecinsValPolColec = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'reapolcolec'
		With lrecinsValPolColec
			.StoredProcedure = "reapolcolec"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				insValPolColec = True
				sPolitype = .FieldToClass("sPolitype")
			Else
				sPolitype = "1"
				insValPolColec = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsValPolColec may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValPolColec = Nothing
		
	End Function
	
	'% insValVIL7021: Validación de los campos de la ventana
	Public Function insValVIL7021(ByVal sCodispl As String, ByVal sYear As String, ByVal sSelection As String, ByVal sClient As String) As String
		
		Dim lclsErrors As eFunctions.Errors
		On Error GoTo insValVIL7021_err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If sYear = String.Empty Then
				Call .ErrorMessage(sCodispl, 70202)
			ElseIf CShort(sYear) > Year(Today) Then 
				Call .ErrorMessage(sCodispl, 70200)
			End If
			If sSelection = "1" And sClient = String.Empty Then
				Call .ErrorMessage(sCodispl, 70201)
			End If
			insValVIL7021 = .Confirm
		End With
		
insValVIL7021_err: 
		If Err.Number Then
			insValVIL7021 = "insValVIL7021 " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	
	'% insPostVIL891:
	Public Function insPostVIL891(ByVal dDateFrom As Date, ByVal ddateto As Date) As Boolean
		
		Dim lrecinsPostVIL891 As eRemoteDB.Execute
		
		lrecinsPostVIL891 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostVIL891
			.StoredProcedure = "REA_VIL891"
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", ddateto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostVIL891 = True
			Else
				insPostVIL891 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostVIL891 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostVIL891 = Nothing
		
	End Function
	
	
	'% insPostVIL7021: Llamado del procedure que ejecuta el reporte
	Public Function insPostVIL7021(ByVal sProcess As String, ByVal sSelection As String, ByVal sYear As String, ByVal sClient As String, ByVal nAnnualcertifnr As Double, ByVal nUsercode As Integer) As Boolean
		
		Dim lrecInsVIL7021 As eRemoteDB.Execute
		
		On Error GoTo insPostVIL7021_err
		
		lrecInsVIL7021 = New eRemoteDB.Execute
		
		With lrecInsVIL7021
			.StoredProcedure = "INSVIL7021PKG.INSVIL7021"
			.Parameters.Add("sProcess", sProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSelection", sSelection, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sYear", sYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnnualcertifnr", nAnnualcertifnr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFlag", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostVIL7021 = IIf(.Parameters("nFlag").Value = 1, True, False)
			Else
				insPostVIL7021 = False
			End If
		End With
		
insPostVIL7021_err: 
		If Err.Number Then
			insPostVIL7021 = False
		End If
		'UPGRADE_NOTE: Object lrecInsVIL7021 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsVIL7021 = Nothing
	End Function
	
	
	Public Function insPostCAL1080(ByVal nOrigin As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		
		Dim lrecinsPostCAL1080 As eRemoteDB.Execute
		
		lrecinsPostCAL1080 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCAL1080
			.StoredProcedure = "rea_cal1080"
			.Parameters.Add("norigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostCAL1080 = True
				P_SKEY = .Parameters("P_SKEY").Value
			Else
				insPostCAL1080 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAL1080 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL1080 = Nothing
		
	End Function
	
	
	
	Public Function insPostCAL1081(ByVal nOrigin As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		
		Dim lrecinsPostCAL1081 As eRemoteDB.Execute
		
		lrecinsPostCAL1081 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCAL1081
			.StoredProcedure = "rea_CAL1081"
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostCAL1081 = True
				P_SKEY = .Parameters("P_SKEY").Value
			Else
				insPostCAL1081 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAL1081 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL1081 = Nothing
		
	End Function
	
	Public Function insPostCAL1082(ByVal nOrigin As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		
		Dim lrecinsPostCAL1082 As eRemoteDB.Execute
		
		lrecinsPostCAL1082 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCAL1082
			.StoredProcedure = "rea_cal1082"
			.Parameters.Add("norigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostCAL1082 = True
				P_SKEY = .Parameters("P_SKEY").Value
			Else
				insPostCAL1082 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAL1082 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL1082 = Nothing
		
	End Function
	
	Public Function insPostCAL1079(ByVal nOrigin As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		
		Dim lrecinsPostCAL1079 As eRemoteDB.Execute
		
		lrecinsPostCAL1079 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCAL1079
			.StoredProcedure = "rea_cal1079"
			.Parameters.Add("norigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostCAL1079 = True
				P_SKEY = .Parameters("P_SKEY").Value
			Else
				insPostCAL1079 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAL1079 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL1079 = Nothing
		
	End Function
	
	
	Public Function insPostCAL1078(ByVal nOrigin As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		
		Dim lrecinsPostCAL1078 As eRemoteDB.Execute
		
		lrecinsPostCAL1078 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_cal1078'
		With lrecinsPostCAL1078
			.StoredProcedure = "rea_cal1078"
			.Parameters.Add("norigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostCAL1078 = True
				P_SKEY = .Parameters("P_SKEY").Value
			Else
				insPostCAL1078 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAL1078 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL1078 = Nothing
		
	End Function
	
	
	Public Function insPostCAL1108(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dDateFrom As Date, ByVal ddateto As Date) As Boolean
		
		Dim lrecinsPostCAL1108 As eRemoteDB.Execute
		
		lrecinsPostCAL1108 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCAL1108
			.StoredProcedure = "rea_cal1108"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ddatefrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ddateto", ddateto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCAL1108 = True
			Else
				insPostCAL1108 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAL1108 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL1108 = Nothing
		
	End Function
	
	Public Function insPostCAL908(ByVal sClient As String, ByVal nProponum As Double, ByVal nUsercode As Integer) As Boolean
		
		Dim lrecinsPostCAL908 As eRemoteDB.Execute
		
		lrecinsPostCAL908 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_cal908'
		With lrecinsPostCAL908
			.StoredProcedure = "rea_cal908"
			.Parameters.Add("Skey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				sKey = .Parameters("Skey").Value
				insPostCAL908 = True
			Else
				insPostCAL908 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAL908 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL908 = Nothing
		
	End Function
	
	'% insValCAL848_k: Se valida cálculo de interes por prestamo
	Public Function insValCAL848_k(ByVal dDate_ini As Date, ByVal dDate_end As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValCAL848_k_Err
		
		'+ Se valida el campo fecha de inicio
		If dDate_ini = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage("CAL848", 60217)
		Else
			'+ Se valida el campo fecha de termino
			If dDate_end = eRemoteDB.Constants.dtmNull Then
				Call lobjErrors.ErrorMessage("CAL848", 60218)
			Else
				'+ Se valida el campo fecha de inicio se menor a Fecha de termino
				If dDate_ini >= dDate_end Then
					Call lobjErrors.ErrorMessage("CAL848", 60205)
				End If
			End If
		End If
		
		insValCAL848_k = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValCAL848_k_Err: 
		If Err.Number Then
			insValCAL848_k = insValCAL848_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	Public Function insPostCAL848_k(ByVal dDate_ini As Date, ByVal dDate_end As Date, Optional ByVal nBranch As Integer = eRemoteDB.Constants.intNull, Optional ByVal nProduct As Integer = eRemoteDB.Constants.intNull, Optional ByVal nOffice As Integer = eRemoteDB.Constants.intNull, Optional ByVal nOfficeAgen As Integer = eRemoteDB.Constants.intNull, Optional ByVal nAgency As Integer = eRemoteDB.Constants.intNull, Optional ByVal nIntermed As Integer = eRemoteDB.Constants.intNull, Optional ByVal nStatquota As Integer = eRemoteDB.Constants.intNull, Optional ByVal nOrigin As Integer = eRemoteDB.Constants.intNull) As Boolean
		Dim lrecinsPostCAL848_k As eRemoteDB.Execute
		
		lrecinsPostCAL848_k = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_cal1078'
		With lrecinsPostCAL848_k
			.StoredProcedure = "insPostcal848"
			.Parameters.Add("dDate_ini", dDate_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_end", dDate_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeagen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatquota", nStatquota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostCAL848_k = True
				P_SKEY = .Parameters("sKey").Value
				sFile_name = insGenFilesCAL848(P_SKEY)
			Else
				insPostCAL848_k = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAL848_k may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL848_k = Nothing
		
	End Function
	
	
	'%FormatData: Esta función se encarga de dar formato a los datos a enviar a archivos de texto.
	Private Function FormatData(ByVal sValue As Object, ByVal sChar As String, ByVal nposition As Integer, Optional ByVal sTrunc As String = "Right", Optional ByVal sAlign As String = "Right") As String
		
		Dim nLength As Integer
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sValue) Then
			sValue = Trim(sValue)
			nLength = Len(sValue)
			If nLength > nposition Then
				If sTrunc = "Right" Then
					FormatData = Right(sValue, nposition)
				Else
					FormatData = Left(sValue, nposition)
				End If
			Else
				If sAlign = "Right" Then
					FormatData = New String(sChar, nposition - nLength) & sValue
				Else
					FormatData = sValue & New String(sChar, nposition - nLength)
				End If
			End If
		Else
			FormatData = New String(sChar, nposition)
		End If
	End Function
	
	'%insGenFilesCAL848: Crea los archivos del proceso CAL848
	Public Function insGenFilesCAL848(ByVal sKey As String) As String
		Dim lrecTime As eRemoteDB.Execute
		Dim lrecinsReatmp_cal848 As eRemoteDB.Execute
		Dim lrecinsReatmp_cal848res As eRemoteDB.Execute
		
		Dim lobjGeneral As eGeneral.GeneralFunction
        'Dim lobjClient As eClient.Client
        'Dim lobjCompany As eGeneral.Company
		
        'Dim llngRecCounter As Integer
        'Dim ljdblAmountTot As Double
		Dim lstrLoadFile As String
		Dim lstrDirFile As String
        'Dim lstrCompany As Object
		
		Dim lstrWritTitle As String
		Dim lstrWritTxt As String
		Dim FileName As String
        'Dim FileNameCityDet As String
		Dim FileNum As Integer
		Dim lProduct As Integer
		Dim lBranch As Integer
		Dim lControl As Short
		Dim lTotal As Integer
        'Dim ncount As Integer
		
		insGenFilesCAL848 = CStr(True)
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "insReatmp_cal848m"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		Dim lclsValue As eFunctions.Values
		If lrecTime.Run() Then
			
			lobjGeneral = New eGeneral.GeneralFunction
			'+ Se busca la ruta en la que se guardará el archivo de texto
			lstrLoadFile = lobjGeneral.GetLoadFile()
			'+ Se busca el directorio virtual del archivo a crear
			lclsValue = New eFunctions.Values
			lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
			'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsValue = Nothing
			'+ ------------------------------------------
			If Not lrecTime.EOF Then
				insGenFilesCAL848 = "CAL848_" & sKey & ".xls"
				FileName = lstrLoadFile & "CAL848_" & sKey & ".xls"
                FileNum = FreeFile()
				FileOpen(FileNum, FileName, OpenMode.Output)
				PrintLine(FileNum, "CONSORCIO")
				PrintLine(FileNum, "SEGUROS GENERALES" & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Today.ToString("yyyy/MM/dd"))
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				PrintLine(FileNum, Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & "DETALLE SELECTIVO DE PROPUESTAS AL " & CStr(IIf(IsDbNull(lrecTime.FieldToClass("dDate_end")), "yyyy/MM/dd", lrecTime.FieldToClass("dDate_end").ToString("yyyy/MM/dd"))))
				PrintLine(FileNum, "")
				PrintLine(FileNum, "SELECCION")
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				PrintLine(FileNum, Chr(9) & "FECHA DIGITACION :" & Chr(9) & CStr(IIf(IsDbNull(lrecTime.FieldToClass("dDate_ini")), "00/00/0000", lrecTime.FieldToClass("dDate_ini").ToString("yyyy/MM/dd"))) & " AL " & CStr(IIf(IsDbNull(lrecTime.FieldToClass("dDate_end")), "00/00/0000", lrecTime.FieldToClass("dDate_end").ToString("yyyy/MM/dd"))))
				PrintLine(FileNum, Chr(9) & "RAMO :" & Chr(9) & FormatData(lrecTime.FieldToClass("sBranch"), " ", 19, "Left", "Left"))
				PrintLine(FileNum, Chr(9) & "PRODUCTO :" & Chr(9) & FormatData(lrecTime.FieldToClass("sProduct"), " ", 19, "Left", "Left"))
				PrintLine(FileNum, Chr(9) & "SUCURSAL :" & Chr(9) & IIf(lrecTime.FieldToClass("nOffice") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nOffice")) & " " & FormatData(lrecTime.FieldToClass("sOffice"), " ", 19, "Left", "Left"))
				PrintLine(FileNum, Chr(9) & "OFICINA :" & Chr(9) & IIf(lrecTime.FieldToClass("nOfficeagen") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nOfficeagen")) & " " & FormatData(lrecTime.FieldToClass("sOfficeagen"), " ", 19, "Left", "Left"))
				PrintLine(FileNum, Chr(9) & "AGENCIA :" & Chr(9) & IIf(lrecTime.FieldToClass("nAgency") = eRemoteDB.Constants.intNull, "", lrecTime.FieldToClass("nAgency")) & " " & FormatData(lrecTime.FieldToClass("sAgency"), " ", 19, "Left", "Left"))
				PrintLine(FileNum, Chr(9) & "AGENTE :" & Chr(9) & FormatData(lrecTime.FieldToClass("sIntermed"), " ", 19, "Left", "Left"))
				PrintLine(FileNum, Chr(9) & "TIPO MOV :" & Chr(9) & IIf(lrecTime.FieldToClass("nOrigin") = eRemoteDB.Constants.intNull, "TODOS", lrecTime.FieldToClass("nOrigin")) & " " & FormatData(lrecTime.FieldToClass("sOrigin"), " ", 19, "Left", "Left"))
				PrintLine(FileNum, Chr(9) & "DICTAMEN :" & Chr(9) & FormatData(lrecTime.FieldToClass("sStatquota"), " ", 19, "Left", "Left") & vbCrLf)
				lstrWritTxt = ""
				lstrWritTxt = lstrWritTxt & "Producto" & Chr(9)
				lstrWritTxt = lstrWritTxt & "T. Vigencia" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Propuesta" & Chr(9)
				lstrWritTxt = lstrWritTxt & "F.Ingreso" & Chr(9)
				lstrWritTxt = lstrWritTxt & "T.Mov.(Glosa)" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Estado de la propuesta" & Chr(9)
                lstrWritTxt = lstrWritTxt & "Rut Inspector" & Chr(9)
				lstrWritTxt = lstrWritTxt & "N° Orden de Servicio" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Estado de la Orden de Servicio" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Poliza" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Vig.Poliza Desde" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Vig.Poliza Hasta" & Chr(9)
				lstrWritTxt = lstrWritTxt & "F.Dictamen" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Usuario. Dictamino" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Obser. Dictamen" & Chr(9)
                lstrWritTxt = lstrWritTxt & "rut asegurado" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Nombre asegurado" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Direccion Aseg." & Chr(9)
				lstrWritTxt = lstrWritTxt & "Comuna" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Ciudad" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Tel. Aseg 1" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Contratante" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Nombre" & Chr(9)
				lstrWritTxt = lstrWritTxt & "F.Suscrip." & Chr(9)
				lstrWritTxt = lstrWritTxt & "Sucursal" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Agencia" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Oficina" & Chr(9)
                lstrWritTxt = lstrWritTxt & "Rut Agente" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Fecha Pago/Caja" & Chr(9)
				lstrWritTxt = lstrWritTxt & "monto 1era/Prima Peso" & Chr(9)
				lstrWritTxt = lstrWritTxt & "1era/Prima UF" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Prima Afecta Final" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Prima Exenta" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Iva" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Total Prima" & Chr(9)
                lstrWritTxt = lstrWritTxt & "Patente" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Dir. Riesgo" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Comuna (DIRECCION DEL RIESGO)" & Chr(9)
				'lstrWritTxt = lstrWritTxt & "Ciudad (DIR. DEL RIESGO)" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Fono Particular" & Chr(9)
				lstrWritTxt = lstrWritTxt & "Fono Comercial" & Chr(9)
				PrintLine(FileNum, lstrWritTxt)
				
				lrecinsReatmp_cal848 = New eRemoteDB.Execute
				
				With lrecinsReatmp_cal848
					.StoredProcedure = "insReatmp_cal848"
					.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If .Run() Then
						lstrWritTxt = ""
						Do While Not .EOF
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nProduct") = eRemoteDB.Constants.intNull, "", .FieldToClass("nProduct")) & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sVigency"), " ", 40, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nProponum") = eRemoteDB.Constants.intNull, "", .FieldToClass("nProponum")) & Chr(9)
							lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dPropodat") = eRemoteDB.Constants.dtmNull, "", Format(.FieldToClass("dPropodat"), "yyyy/MM/dd"))) & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nOrigin") = eRemoteDB.Constants.intNull, "", .FieldToClass("nOrigin")) & " " & FormatData(.FieldToClass("sOrigin"), " ", 19, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nStatquota") = eRemoteDB.Constants.intNull, "", .FieldToClass("nStatquota")) & " " & FormatData(.FieldToClass("sStatquota"), " ", 19, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sImp_client"), " ", 40, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nServ_order") = eRemoteDB.Constants.intNull, "", .FieldToClass("nServ_order")) & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nStatus_ord") = eRemoteDB.Constants.intNull, "", .FieldToClass("nStatus_ord")) & " " & FormatData(.FieldToClass("sStatus_ord"), " ", 19, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nPol_quot") = eRemoteDB.Constants.intNull, "", .FieldToClass("nPol_quot")) & Chr(9)
							lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dstartdate_pol") = eRemoteDB.Constants.dtmNull, "", Format(.FieldToClass("dstartdate_pol"), "yyyy/MM/dd"))) & Chr(9)
							lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dExpirdat_pol") = eRemoteDB.Constants.dtmNull, "", Format(.FieldToClass("dExpirdat_pol"), "yyyy/MM/dd"))) & Chr(9)
							lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dDate_dic") = eRemoteDB.Constants.dtmNull, "", Format(.FieldToClass("dDate_dic"), "yyyy/MM/dd"))) & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nUsercode") = eRemoteDB.Constants.intNull, "", .FieldToClass("nUsercode")) & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nNotenum") = eRemoteDB.Constants.intNull, "", .FieldToClass("nNotenum")) & IIf(.FieldToClass("sNotenum") = eRemoteDB.Constants.intNull, "", .FieldToClass("sNotenum")) & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sClient"), " ", 40, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sCliename"), " ", 40, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sDescadd"), " ", 40, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sMunicipality"), " ", 40, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sLocal"), " ", 40, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sPhones"), " ", 40, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sProp_client"), " ", 40, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sProp_name"), " ", 40, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dStartdate") = eRemoteDB.Constants.dtmNull, "", Format(.FieldToClass("dStartDate"), "yyyy/MM/dd"))) & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nOffice") = eRemoteDB.Constants.intNull, "", .FieldToClass("nOffice")) & " " & FormatData(.FieldToClass("sOffice"), " ", 19, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nOfficeagen") = eRemoteDB.Constants.intNull, "", .FieldToClass("nOfficeagen")) & " " & FormatData(.FieldToClass("sOfficeagen"), " ", 19, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nAgency") = eRemoteDB.Constants.intNull, "", .FieldToClass("nAgency")) & " " & FormatData(.FieldToClass("sAgency"), " ", 19, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sClient_inter"), " ", 40, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dPaydate") = eRemoteDB.Constants.dtmNull, "", Format(.FieldToClass("dPaydate"), "yyyy/MM/dd"))) & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nAmount_local") = eRemoteDB.Constants.intNull, "", FormatNumber(.FieldToClass("nAmount_local"), 6,  ,  , TriState.True)) & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nAmount") = eRemoteDB.Constants.intNull, "", FormatNumber(.FieldToClass("nAmount"), 6,  ,  , TriState.True)) & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nPremiuma") = eRemoteDB.Constants.intNull, "", FormatNumber(.FieldToClass("nPremiuma"), 6,  ,  , TriState.True)) & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nPremiume") = eRemoteDB.Constants.intNull, "", FormatNumber(.FieldToClass("nPremiume"), 6,  ,  , TriState.True)) & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nIva") = eRemoteDB.Constants.intNull, "", FormatNumber(.FieldToClass("nIva"), 6,  ,  , TriState.True)) & Chr(9)
							lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nPremium") = eRemoteDB.Constants.intNull, "", FormatNumber(.FieldToClass("nPremium"), 6,  ,  , TriState.True)) & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sRegist"), " ", 40, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sDescadd_par"), " ", 40, "Left", "Left") & Chr(9)
							'lstrWritTxt = lstrWritTxt & .FieldToClass("sMunicipality_par") & Chr(9)
							lstrWritTxt = lstrWritTxt & .FieldToClass("sLocal_par") & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sPhones_par"), " ", 40, "Left", "Left") & Chr(9)
							lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sPhones_com"), " ", 40, "Left", "Left") & Chr(9) '& vbCrLf
							
							PrintLine(FileNum, lstrWritTxt)
							lstrWritTxt = ""
							.RNext()
						Loop 
						
						If (lstrWritTxt <> "") Then
							PrintLine(FileNum, lstrWritTxt)
						End If
						
						'UPGRADE_NOTE: Object lrecinsReatmp_cal848 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lrecinsReatmp_cal848 = Nothing
					End If
				End With
				
				lrecinsReatmp_cal848res = New eRemoteDB.Execute
				'+
				'+ Definición de store procedure insReatmp_cal848res al 03-15-2004 20:23:04
				'+
				With lrecinsReatmp_cal848res
					.StoredProcedure = "insReatmp_cal848res"
					.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					lControl = 0
					If .Run(True) Then
						lProduct = .FieldToClass("nProduct")
						lBranch = .FieldToClass("nBranch")
						PrintLine(FileNum, "")
						PrintLine(FileNum, "")
						PrintLine(FileNum, Chr(9) & Chr(9) & Chr(9) & Chr(9) & "TOTALES POR TIPO DE MOVIMIENTO")
						lstrWritTxt = .FieldToClass("sProduct") & Chr(9)
						lstrWritTitle = ""
						lTotal = 0
						Do While Not .EOF
							If (lProduct = .FieldToClass("nProduct")) And (lBranch = .FieldToClass("nBranch")) Then
								lstrWritTitle = lstrWritTitle & .FieldToClass("sOrigin") & Chr(9)
								lstrWritTxt = lstrWritTxt & .FieldToClass("nTotal") & Chr(9)
								lTotal = lTotal + .FieldToClass("nTotal")
							End If
							If (lProduct <> .FieldToClass("nProduct")) Or (lBranch <> .FieldToClass("nBranch")) Then
								If lControl = 0 Then
									PrintLine(FileNum, Chr(9) & lstrWritTitle & "Total")
								End If
								lstrWritTxt = lstrWritTxt & lTotal
								PrintLine(FileNum, lstrWritTxt)
								lProduct = .FieldToClass("nProduct")
								lBranch = .FieldToClass("nBranch")
								lstrWritTxt = .FieldToClass("sProduct") & Chr(9) & .FieldToClass("nTotal") & Chr(9)
								lTotal = .FieldToClass("nTotal")
								lControl = 1
							End If
							.RNext()
						Loop 
						If lControl = 0 Then
							PrintLine(FileNum, Chr(9) & lstrWritTitle & "Total")
						End If
						PrintLine(FileNum, lstrWritTxt & lTotal)
						'UPGRADE_NOTE: Object lrecinsReatmp_cal848res may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lrecinsReatmp_cal848res = Nothing
					End If
				End With
				FileClose(FileNum)
			End If
		End If
		
	End Function
	
	Public Function insPostCAC947(ByVal nPolicy As Double, ByVal sClient As String) As Boolean
		
		Dim lrecinsPostCAC947 As eRemoteDB.Execute
		
		lrecinsPostCAC947 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_cac947'
		With lrecinsPostCAC947
			.StoredProcedure = "rea_cac947"
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCAC947 = True
			Else
				insPostCAC947 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAC947 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAC947 = Nothing
		
	End Function
	
	Public Function insPostCAL969(ByVal dDateIni As Date, ByVal dDateEnd As Date) As Boolean
		Dim lrecinsPostCAL969 As eRemoteDB.Execute
		
		lrecinsPostCAL969 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.INSUPDLCTACTICTACONT'
		With lrecinsPostCAL969
			.StoredProcedure = "Rea_Cal969"
			.Parameters.Add("dDateIni", dDateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCAL969 = True
			Else
				insPostCAL969 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAL969 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL969 = Nothing
		
	End Function
	
	Public Function insPostCAC929(ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nStatquota As Integer, ByVal nOrigin As Integer, ByVal dDateIni As Date, ByVal dDateEnd As Date) As Boolean
		Dim lrecinsPostCAC929 As eRemoteDB.Execute
		
		lrecinsPostCAC929 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.INSUPDLCTACTICTACONT'
		With lrecinsPostCAC929
			.StoredProcedure = "Rea_Cac929"
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeagen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatquota", nStatquota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateIni", dDateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCAC929 = True
			Else
				insPostCAC929 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAC929 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAC929 = Nothing
		
	End Function
	
	
	
	Public Function insPostCAC1005(ByVal sClient As String) As Boolean
		
		Dim lrecinsPostCAC1005 As eRemoteDB.Execute
		
		lrecinsPostCAC1005 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_cac1005'
		With lrecinsPostCAC1005
			.StoredProcedure = "rea_cac1005"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("SKEY").Value
				insPostCAC1005 = True
			Else
				insPostCAC1005 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCAC1005 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAC1005 = Nothing
		
	End Function
	
	'% insPostCAL826_K: Se realiza proceso de calculo de la prima ganada incobrable
	Public Function insPostVAL630_K(ByVal dEffecini As Date, ByVal dEffecend As Date, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal optType As Integer) As Boolean
		Dim lrecinsPostVAL630_K As eRemoteDB.Execute
		
		lrecinsPostVAL630_K = New eRemoteDB.Execute
		
		With lrecinsPostVAL630_K
			.StoredProcedure = "insval630_K"
			.Parameters.Add("deffecini", dEffecini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("deffecend", dEffecend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("opttype", optType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				sKey = .Parameters("sKey").Value
				insPostVAL630_K = True
			Else
				insPostVAL630_K = False
			End If
		End With
		
	End Function
	
	
	'% insValVIL1413_K: Se valida parámetros del reporte VIL1413 movimientos de la cuenta cte de poliza - VUL
	Public Function insValVIL1413_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dDate_ini As Date, ByVal dDate_end As Date, ByVal sSel As String) As String
		Dim lobjErrors As eFunctions.Errors
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValVIL1413_K_Err
		
		If nBranch = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 1022)
		End If
		
		If nProduct = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 1014)
		End If
		
		If sSel = "" Then
			Call lobjErrors.ErrorMessage(sCodispl, 750135)
		End If
		
		'+ Se valida el campo fecha de inicio
		If dDate_ini = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 60217)
		Else
			'+ Se valida el campo fecha de termino
			If dDate_end = eRemoteDB.Constants.dtmNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 60218)
			Else
				'+ Se valida el campo fecha de inicio se menor a Fecha de termino
				If dDate_ini >= dDate_end Then
					Call lobjErrors.ErrorMessage(sCodispl, 60205)
				End If
			End If
		End If
		
		insValVIL1413_K = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValVIL1413_K_Err: 
		If Err.Number Then
			insValVIL1413_K = insValVIL1413_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	
	'%inspostVIL900_k: Crea los archivos del proceso VIL900
	Public Function insPostVIL900_k(ByVal dDeffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		
		insPostVIL900_k = True
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "InsCalDef"
			.Parameters.Add("dDeffecdate", dDeffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = Trim(.Parameters("sKey").Value)
				insPostVIL900_k = True
			Else
				insPostVIL900_k = False
			End If
		End With
		
		insGenVIL900_k(P_SKEY)
		On Error GoTo 0
	End Function
	
	'%insGenVIL900_k: Genera el *.txt de la transaccion VIL900
	Public Function insGenVIL900_k(ByVal sKey As String) As String
		Dim lrecTime As eRemoteDB.Execute
        Dim lobjGeneral As eGeneral.GeneralFunction
        Dim varAux As String = ""
        Dim lstrLoadFile As String
        Dim lstrDirFile As String
        Dim lstrWritTxt As String
		Dim FileName As String
		Dim FileNum As Integer

        Try
            lrecTime = New eRemoteDB.Execute

            With lrecTime
                .StoredProcedure = "ReaTmp_VIL900"
                .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With

            Dim lclsValue As eFunctions.Values
            If lrecTime.Run() Then

                lobjGeneral = New eGeneral.GeneralFunction
                '+ Se busca la ruta en la que se guardará el archivo de texto
                lstrLoadFile = lobjGeneral.GetLoadFile()
                '+ Se busca el directorio virtual del archivo a crear
                lclsValue = New eFunctions.Values
                lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
                'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsValue = Nothing
                '+ ------------------------------------------
                '+          Se envia archivo al directorio
                '+          Campos faltantes: /

                If Not lrecTime.EOF Then
                    'insGenVIL900_k = "DEF-" & Format(Date, "yyyyMMdd") & Format(Time, "hhmmss") & ".txt"
                    sFile_name = "DEF-" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".txt"
                    FileName = lstrLoadFile & sFile_name
                    FileNum = FreeFile()
                    FileOpen(FileNum, FileName, OpenMode.Output)
                    lstrWritTxt = ""
                    lstrWritTxt = lstrWritTxt & "Inic. Vigencia" & Chr(9)
                    lstrWritTxt = lstrWritTxt & "Poliza" & Chr(9)
                    lstrWritTxt = lstrWritTxt & "Prima Neta" & Chr(9)
                    lstrWritTxt = lstrWritTxt & "Sin. Pagados" & Chr(9)
                    lstrWritTxt = lstrWritTxt & "Sin. Prov." & Chr(9)
                    PrintLine(FileNum, lstrWritTxt)
                    Do While Not lrecTime.EOF
                        lstrWritTxt = ""
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        lstrWritTxt = lstrWritTxt & CStr(IIf(IsDBNull(lrecTime.FieldToClass("dStartdate")), "00/00/0000", Format(lrecTime.FieldToClass("dStartdate"), "yyyy/MM/dd"))) & Chr(9)
                        lstrWritTxt = lstrWritTxt & FormatData(lrecTime.FieldToClass("nPolicy"), " ", 10) & Chr(9)
                        lstrWritTxt = lstrWritTxt & FormatData(System.Math.Round(lrecTime.FieldToClass("nTotPremium_Pay")), "0", 13) & Chr(9)
                        lstrWritTxt = lstrWritTxt & FormatData(System.Math.Round(lrecTime.FieldToClass("nTotClaim_Pay")), "0", 13) & Chr(9)
                        lstrWritTxt = lstrWritTxt & FormatData(System.Math.Round(lrecTime.FieldToClass("nTotClaim_Prov")), "0", 13) & Chr(9)
                        PrintLine(FileNum, lstrWritTxt)
                        lrecTime.RNext()
                    Loop
                    FileClose(FileNum)
                End If
            End If
            Return lrecTime
        Catch ex As Exception
            Return varAux = varAux & Err.Description
        End Try
    End Function
	
	'% InsCreTMP_CAL503: Crea los registros de producción en la tabla TMP_CAL503, para luego mostrar el LT de producción.
	Public Function InsCreTMP_CAL503(ByVal p_cod_dia As Integer, ByVal p_area_seguro As Integer, ByVal p_fecha_desde As Date, ByVal p_fecha_hasta As Date) As Boolean
		
		'  ByVal nUsercode As Date   se comenta
		Dim lclsTmp_CAL503 As eRemoteDB.Execute
        Dim sKey As String = ""


        On Error GoTo Add_err
		lclsTmp_CAL503 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..
		
		'+ Se comenta el parametro de entrada nUsercode
		With lclsTmp_CAL503
			.StoredProcedure = "CRETMP_CAL503"
			.Parameters.Add("p_cod_dia", p_cod_dia, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_area_seguro", p_area_seguro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_fecha_desde", p_fecha_desde, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_fecha_hasta", p_fecha_hasta, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'.Parameters.Add "nUsercode", nUsercode, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			
			If .Run(False) Then
				P_SKEY = .Parameters.Item("sKey").Value
				InsCreTMP_CAL503 = True
			Else
				InsCreTMP_CAL503 = False
			End If
			
		End With
		
Add_err: 
		If Err.Number Then
			InsCreTMP_CAL503 = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsTmp_CAL503 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTmp_CAL503 = Nothing
	End Function
	
	
	'%insValCAL00008: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCAL00008(ByVal sCodispl As String, ByVal dDateInd As Date, ByVal dDateEnd As Date, ByVal dblPolicyType As Double) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insVal_Err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			
			'           '+ Si la fecha inicial es diferente de vacio continua las validaciones
			If dDateInd = eRemoteDB.Constants.dtmNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 9071)
			End If
			'+ Si la fecha final es diferente de vacio continua las validaciones
			If dDateEnd = eRemoteDB.Constants.dtmNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 9072)
			End If
			'+ Se verifica que que la fecha final sea mayor a la fecha inicial
			If dDateEnd < dDateInd Then
				Call lobjErrors.ErrorMessage(sCodispl, 4159)
			End If
			'+ Se verifica que la fecha final no sea mayor a la fecha del día
			If dDateEnd > Today Then
				Call lobjErrors.ErrorMessage(sCodispl, 4341)
			End If
			'+ Se verifica que se ingrese el tipo de poliza
			If dblPolicyType <= 0 Then
				Call lobjErrors.ErrorMessage(sCodispl, 5565)
			End If
			
			
			
			
			insValCAL00008 = .Confirm
		End With
		
insVal_Err: 
		If Err.Number Then
			insValCAL00008 = "insValCAL00008: " & insValCAL00008 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValVIL01600: Realiza la validación de los campos de la ventana VIL01600 - Reporte de Inversiones.
	Public Function insValVIL01600(ByVal sCodispl As String, ByVal sCompanyType As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dDateInd As Date, ByVal dDateEnd As Date) As String
		Dim lclsProduct As eProduct.Product
		Dim lclsPolicy As ePolicy.Policy
		Dim lobjErrors As Object
		Dim lobjValues As Object
		Dim lblnError As Boolean
		Dim ldtmDate As Date
		
		On Error GoTo ErrorHandler
		
		lclsProduct = New eProduct.Product
		lclsPolicy = New ePolicy.Policy
		lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		lobjValues = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Values")
		
		lblnError = False
		
		
		
		'+ Si la fecha inicial es diferente de vacio continua las validaciones
		If dDateInd = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 9071)
		End If
		'+ Si la fecha final es diferente de vacio continua las validaciones
		If dDateEnd = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 9072)
		End If
		'+ Se verifica que que la fecha final sea mayor a la fecha inicial
		If dDateEnd < dDateInd Then
			Call lobjErrors.ErrorMessage(sCodispl, 4159)
		End If
		
		
		
		'**+ Validate the field Product.
		'+ Se valida el campo Producto.
		
		If nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull Then
			If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
				Call lobjErrors.ErrorMessage(sCodispl, 70137)
			Else
				lobjValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
                If Not lobjValues.IsValid("tabProdmaster1", CStr(nProduct), True) Then
					Call lobjErrors.ErrorMessage(sCodispl, 9066)
					
					lblnError = True
				Else
					
					'**+ Validate that the product corresponds to life or combined
					'+ Se valida que el producto corresponda a vida o combinado
					
					With lclsProduct
						Call .insValProdMaster(nBranch, nProduct)
						
						If .blnError Then
							If CStr(.sBrancht) <> "1" And CStr(.sBrancht) <> "2" And CStr(.sBrancht) <> "5" Then
								Call lobjErrors.ErrorMessage(sCodispl, 3403)
								
								lblnError = True
							Else
								If dDateInd <> eRemoteDB.Constants.dtmNull Then
									If .FindProduct_li(nBranch, nProduct, dDateInd) Then
										If .nProdClas <> 4 Then
											Call lobjErrors.ErrorMessage(sCodispl, 70123)
										End If
									Else
										Call lobjErrors.ErrorMessage(sCodispl, 70123)
									End If
								End If
							End If
						End If
					End With
				End If
			End If
		End If
		
		'**+ Validate the field Policy
		'+ Se valida el campo Póliza.
		
		If Not lblnError Then
			If nPolicy <> 0 And nPolicy <> eRemoteDB.Constants.intNull Then
				If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
					Call lobjErrors.ErrorMessage(sCodispl, 70138)
					
					'**+ Validate that it is valid policy.
					'+ Se valida que sea una póliza válida.
					
				Else
					With lclsPolicy
						If Not .FindPolicyOfficeName("2", nBranch, nProduct, nPolicy, sCompanyType) Then
							Call lobjErrors.ErrorMessage(sCodispl, 3001)
							
							lblnError = True
						Else
							If .sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrIncomplete) Or .sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrInvalid) Then
								Call lobjErrors.ErrorMessage(sCodispl, 3720)
								
								lblnError = True
							Else
								
								'**+ Verify that the policy is not anulled
								'+ Verificar que la póliza no esté anulada
								
								If .dNulldate <> eRemoteDB.Constants.dtmNull Then
									Call lobjErrors.ErrorMessage(sCodispl, 3098)
									
									lblnError = True
								End If
							End If
						End If
					End With
				End If
			End If
		End If
		
		'**+ The field date to must be full.
		'+ El campo fecha hasta debe estar lleno si el indicador Invertido es seleccionado
		
		'    If sProcess = "1" Then
		'        If dFecha = dtmNull Then
		'            Call lobjErrors.ErrorMessage(sCodispl, 7114)
		'        End If
		'    End If
		
		insValVIL01600 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
	End Function
	'% VT00059 HAD002 Informe de Detalle de Primeras Primas
	Public Function insValCAL01500(ByVal sCodispl As String, ByVal dIniDate As Date, ByVal dEndDate As Date) As String
		
		Dim lobjErrors As eFunctions.Errors
		lobjErrors = New eFunctions.Errors
		
		insValCAL01500 = String.Empty
		
		On Error GoTo insValPolicy_Err
		
		'+ Si la fecha inicial es diferente de vacio continua las validaciones
		If dIniDate = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 9071)
		End If
		'+ Si la fecha final es diferente de vacio continua las validaciones
		If dEndDate = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 9072)
		End If
		'+ Se verifica que que la fecha final sea mayor a la fecha inicial
		If dEndDate < dIniDate Then
			Call lobjErrors.ErrorMessage(sCodispl, 4159)
		End If
		'+ Se verifica que la fecha final no sea mayor a la fecha del día
		If dEndDate > Today Then
			Call lobjErrors.ErrorMessage(sCodispl, 4341)
		End If
		
		insValCAL01500 = lobjErrors.Confirm
		
insValPolicy_Err: 
		If Err.Number Then
			insValCAL01500 = "insValCAL01500: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
		On Error GoTo 0
	End Function
	'%insValCAL00832: Esta función se encarga de realizar las respectivas validaciones de la transacción.
	Public Function insValCAL00832(ByVal sCodispl As String, ByVal dIniDate As Date, ByVal dEndDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insVal_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'           '+ Si la fecha inicial es diferente de vacio continua las validaciones
			If dIniDate = eRemoteDB.Constants.dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 9071)
			End If
			'+ Si la fecha final es diferente de vacio continua las validaciones
			If dEndDate = eRemoteDB.Constants.dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 9072)
			End If
			'+ Se verifica que que la fecha final sea mayor a la fecha inicial
			If dEndDate < dIniDate Then
				Call lclsErrors.ErrorMessage(sCodispl, 4159)
			End If
			'+ Se verifica que la fecha final no sea mayor a la fecha del día
			If dEndDate > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 4341)
			End If
			
			
			
			insValCAL00832 = .Confirm
		End With
		
insVal_Err: 
		If Err.Number Then
			insValCAL00832 = "insValCAL00832: " & insValCAL00832 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% InsValCAL01501: Valida filtros para reporte Informe de gestión de operaciones
	Public Function insValCAL01501(ByVal sCodispl As String, ByVal sCertype As String, ByVal nInsurArea As Integer, ByVal dIniDate As Date, ByVal dEndDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValPolicy_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Se debe ingresar el codigo de area
			If nInsurArea = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60215)
			End If
			
			'+ Se debe ingresar el tipo de informacion
			If sCertype = String.Empty Then
				.ErrorMessage(sCodispl, 60216)
			End If
			
			'+ Se debe ingresar la fecha de inicio
			If dIniDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 60217)
			End If
			
			'+ Se debe ingresar la fecha de fin
			If dEndDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 60218)
			End If
			
			'+ Fecha de inicio no debe superar a fecha fin
			If dIniDate <> eRemoteDB.Constants.dtmNull And dEndDate <> eRemoteDB.Constants.dtmNull Then
				If dIniDate > dEndDate Then
					.ErrorMessage(sCodispl, 60207)
				End If
			End If
			
			insValCAL01501 = .Confirm
		End With
		
insValPolicy_Err: 
		If Err.Number Then
			insValCAL01501 = "insValCAL01501: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	'%insValCAL01502: Esta función realiza las validaciones de la transacciòn CAL01502 - "Informe de Polizas Emitidas".
	Public Function insValCAL01502(ByVal sCodispl As String, ByVal nType As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal nOption As Integer = 0, Optional ByVal nOffice As Integer = 0, Optional ByVal nAgency As Integer = 0, Optional ByVal dteEndoso As Date = #12:00:00 AM#) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsCertificat As ePolicy.Certificat
		Dim lstrsCertype As String
		Dim oPolicy As Policy
		
		On Error GoTo insValPolicy_Err
		
		lobjErrors = New eFunctions.Errors
		insValCAL01502 = String.Empty
		oPolicy = New Policy
		
		
		'+ Si el tipo de ejecucion es "Puntual" se realizan las validaciones de los campos
		'+ Póliza y certificado.
		
		If nType = 1 Then
			'+ la poliza debe estar llena
			If (nPolicy = eRemoteDB.Constants.intNull Or nPolicy = 0) Then
				Call lobjErrors.ErrorMessage(sCodispl, 3003)
			End If
			'+ si el producto y la poliza tienen valor el ramo debe estar lleno
			If (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) And (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
				Call lobjErrors.ErrorMessage(sCodispl, 11135)
			End If
			'+ si la poliza tienen valor el producto debe estar lleno
			If (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
				Call lobjErrors.ErrorMessage(sCodispl, 1014)
			End If
			'+ si la poliza tienen valor debe existir en el sistema
			If (nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
				If Not oPolicy.ValExistPolicyRec(nBranch, nProduct, nPolicy, "1") Then
					Call lobjErrors.ErrorMessage(sCodispl, 3001)
				End If
			End If
			'+ si el certificado tienen valor debe existir en el sistema
			If (nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) And (nCertif <> eRemoteDB.Constants.intNull) Then
				lclsCertificat = New ePolicy.Certificat
				If Not lclsCertificat.Find("2", CInt(nBranch), CInt(nProduct), nPolicy, nCertif) Then
					Call lobjErrors.ErrorMessage(sCodispl, 8215)
					
				End If
			End If
			
			'+ si la opcion es una emision de POLIZA (2) ó es Propuesta de Modificacion (6)
			'+ se deberia ingresar la poliza
			'If (nOption = 2 Or nOption = 6) And _
			''   nPolicy <= 0 Then
			'    Call lobjErrors.ErrorMessage(sCodispl, 3003)
			'End If
			
			'+ si noption es igual a modificacion,
			'se debe ingresar la fecha de endoso
			If nOption = 6 And dteEndoso = eRemoteDB.Constants.dtmNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 55534)
			End If
			
			'+si tipo de cotizacion o propuesta no se ha ingresado doy el error
			If nOption <= 0 Then
				Call lobjErrors.ErrorMessage(sCodispl, 60213)
			End If
			
			
			
		Else
			
			'+ Si el tipo de ejecucion es "Masivo" se realizan las validaciones de los campos
			'+ Sucursal y Agencia.
			
			'+ si es masivo y ninguno de los campos tiene informacion
			If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) And (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) And (nOffice = eRemoteDB.Constants.intNull Or nOffice = 0) And (nAgency = eRemoteDB.Constants.intNull Or nAgency = 0) Then
				Call lobjErrors.ErrorMessage(sCodispl, 55550)
			End If
			
			'+ si la agencia tiene informacion debe estar llena la sucursal
			If (nOffice = eRemoteDB.Constants.intNull Or nOffice = 0) And (nAgency <> eRemoteDB.Constants.intNull And nAgency <> 0) Then
				Call lobjErrors.ErrorMessage(sCodispl, 55520)
			End If
			
			'+ si el producto tiene informacion debe estar lleno el ramo
			If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) Then
				Call lobjErrors.ErrorMessage(sCodispl, 11135)
			End If
		End If
		
		
		insValCAL01502 = lobjErrors.Confirm
		
insValPolicy_Err: 
		If Err.Number Then
			insValCAL01502 = "insValCAL01502: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
	End Function
	'%insValCAL01503: Esta función se encarga de realizar las respectivas validaciones de la transacción."Reporte de Carta de pólizas"
	Public Function insValCAL01503(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nOffice As Integer, ByVal nAgency As Integer, ByVal dIniDate As Date, ByVal dEndDate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValPolicy_Err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			
			
			'+ si el producto tiene informacion debe estar lleno el ramo
			If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) Then
				Call .ErrorMessage(sCodispl, 1005)
			End If
			
			'+ Si la fecha inicial es diferente de vacio continua las validaciones
			If dIniDate = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 60217)
			End If
			
			'+ Si la fecha final es diferente de vacio continua las validaciones
			If dEndDate = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 60218)
			End If
			
			'+ Se verifica que que la fecha final sea mayor a la fecha inicial
			If dEndDate < dIniDate Then
				Call .ErrorMessage(sCodispl, 55006)
			End If
			
			'+ Se verifica que la fecha final no sea mayor a la fecha del día
			If dEndDate > Today Then
				Call .ErrorMessage(sCodispl, 55852)
			End If
			
			'+ Se verifica que la fecha inicial no sea mayor a la fecha del día
			If dIniDate > Today Then
				Call .ErrorMessage(sCodispl, 55852)
			End If
			
			
			insValCAL01503 = .Confirm
		End With
		
insValPolicy_Err: 
		If Err.Number Then
			insValCAL01503 = "insValCAL01503: " & insValCAL01503 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	
	'% InsValCAL01504: Valida filtros para reporte de cotización más salud
	Public Function InsValCAL01504(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nIntermed As Integer, ByVal nUsercode As Integer) As String
		
		
		Dim lobjErrors As eFunctions.Errors
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsUser As eSecurity.User
		
		On Error GoTo insValPolicy_Err
		lclsPolicy = New ePolicy.Policy
		lobjErrors = New eFunctions.Errors
		lclsUser = New eSecurity.User
		InsValCAL01504 = String.Empty
		
		
		'+ El ramo debe estar lleno
		If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 11135)
		End If
		
		'+ El producto debe estar lleno
		If (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 1014)
		End If
		
		
		'+Busco el usuario logueado
		lclsUser.Find(nUsercode)
		
		
		'+La cotizacion debe existir
		If Not lclsPolicy.Find("3", nBranch, nProduct, nPolicy) Then
			Call lobjErrors.ErrorMessage(sCodispl, 55651)
		Else
			'+ El Intermediario debe estar lleno si y solo si el usuario es intermediario
			If lclsUser.sType = "3" Then
				If (nIntermed = eRemoteDB.Constants.intNull Or nIntermed = 0) Then
					Call lobjErrors.ErrorMessage(sCodispl, 21038)
				Else
					'+ Se busca el intermediario de la cotización
					If lclsPolicy.nIntermed <> eRemoteDB.Constants.intNull Then
						'+El Intermediario que este solicitando el reporte, debe ser el mismo que creó la cotización.
						If lclsPolicy.nIntermed <> nIntermed Then
							Call lobjErrors.ErrorMessage(sCodispl, 80112)
						End If
					End If
				End If
			End If
		End If
		
		
		InsValCAL01504 = lobjErrors.Confirm
		
		
insValPolicy_Err: 
		If Err.Number Then
			InsValCAL01504 = "insValCAL01504: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValVIL8003: Valida filtros para reporte de cotización Previsor Plus
	Public Function InsValVIL8003(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nIntermed As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsPolicy As ePolicy.Policy
		
		On Error GoTo insValPolicy_Err
		lclsPolicy = New ePolicy.Policy
		lobjErrors = New eFunctions.Errors
		InsValVIL8003 = String.Empty
		
		'+ El ramo debe estar lleno
		If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 11135)
		End If
		
		'+ El producto debe estar lleno
		If (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 1014)
		End If
		
		'+ El Intermediario debe estar lleno
		If (nIntermed = eRemoteDB.Constants.intNull Or nIntermed = 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 21038)
		End If
		
		'+La cotizacion debe existir
		If Not lclsPolicy.Find("3", nBranch, nProduct, nPolicy) Then
			Call lobjErrors.ErrorMessage(sCodispl, 55651)
		Else
			'+ Se busca el intermediario de la cotización
			If lclsPolicy.nIntermed <> eRemoteDB.Constants.intNull Then
				'+El Intermediario que este solicitando el reporte, debe ser el mismo que creó la cotización.
				If lclsPolicy.nIntermed <> nIntermed Then
					Call lobjErrors.ErrorMessage(sCodispl, 80112)
				End If
			Else
				'+ Si el intermediario es nulo se manda el error
				Call lobjErrors.ErrorMessage(sCodispl, 80112)
			End If
		End If
		
		InsValVIL8003 = lobjErrors.Confirm
insValPolicy_Err: 
		If Err.Number Then
			InsValVIL8003 = "insValVIL8003: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValVIL8002: Valida filtros para reporte de cotización Previsor Plus
	Public Function InsValVIL8002(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nIntermed As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsPolicy As ePolicy.Policy
		
		On Error GoTo insValPolicy_Err
		lclsPolicy = New ePolicy.Policy
		lobjErrors = New eFunctions.Errors
		InsValVIL8002 = String.Empty
		
		'+ El ramo debe estar lleno
		If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 11135)
		End If
		
		'+ El producto debe estar lleno
		If (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 1014)
		End If
		
		'+ El Intermediario debe estar lleno
		If (nIntermed = eRemoteDB.Constants.intNull Or nIntermed = 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 21038)
		End If
		
		'+La cotizacion debe existir
		If Not lclsPolicy.Find("3", nBranch, nProduct, nPolicy) Then
			Call lobjErrors.ErrorMessage(sCodispl, 55651)
		Else
			'+ Se busca el intermediario de la cotización
			If lclsPolicy.nIntermed <> eRemoteDB.Constants.intNull Then
				'+El Intermediario que este solicitando el reporte, debe ser el mismo que creó la cotización.
				If lclsPolicy.nIntermed <> nIntermed Then
					Call lobjErrors.ErrorMessage(sCodispl, 80112)
				End If
			Else
				'+ Si el intermediario es nulo se manda el error
				Call lobjErrors.ErrorMessage(sCodispl, 80112)
			End If
		End If
		
		InsValVIL8002 = lobjErrors.Confirm
insValPolicy_Err: 
		If Err.Number Then
			InsValVIL8002 = "insValVIL8002: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValVIL8004: Valida filtros para reporte de cotización Planificador
	Public Function InsValVIL8004(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nIntermed As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsPolicy As ePolicy.Policy
		
		On Error GoTo insValPolicy_Err
		lclsPolicy = New ePolicy.Policy
		lobjErrors = New eFunctions.Errors
		InsValVIL8004 = String.Empty
		
		'+ El ramo debe estar lleno
		If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 11135)
		End If
		
		'+ El producto debe estar lleno
		If (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 1014)
		End If
		
		'+ El Intermediario debe estar lleno
		If (nIntermed = eRemoteDB.Constants.intNull Or nIntermed = 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 21038)
		End If
		
		'+La cotizacion debe existir
		If Not lclsPolicy.Find("3", nBranch, nProduct, nPolicy) Then
			Call lobjErrors.ErrorMessage(sCodispl, 55651)
		Else
			'+ Se busca el intermediario de la cotización
			If lclsPolicy.nIntermed <> eRemoteDB.Constants.intNull Then
				'+El Intermediario que este solicitando el reporte, debe ser el mismo que creó la cotización.
				If lclsPolicy.nIntermed <> nIntermed Then
					Call lobjErrors.ErrorMessage(sCodispl, 80112)
				End If
			Else
				'+ Si el intermediario es nulo se manda el error
				Call lobjErrors.ErrorMessage(sCodispl, 80112)
			End If
		End If
		
		InsValVIL8004 = lobjErrors.Confirm
insValPolicy_Err: 
		If Err.Number Then
			InsValVIL8004 = "insValVIL8004: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		On Error GoTo 0
	End Function
	
	
	
	
	'%insValCALXXXXX: Esta función realiza las validaciones de la transacciòn CAL01506 - "Informe de Polizas Emitidas".
	Public Function insValCALXXXXX(ByVal sCodispl As String, ByVal nType As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal nOption As Integer = 0, Optional ByVal nOffice As Integer = 0, Optional ByVal nAgency As Integer = 0, Optional ByVal dDateCopy As Date = #12:00:00 AM#) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsCertificat As ePolicy.Certificat
		Dim lstrsCertype As String
		Dim oPolicy As Policy
		
		On Error GoTo insValPolicy_Err
		
		lobjErrors = New eFunctions.Errors
		insValCALXXXXX = String.Empty
		oPolicy = New Policy
		
		
		'+ Si el tipo de ejecucion es "Puntual" se realizan las validaciones de los campos
		'+ Póliza
		
		If nType = 1 Then
			'+ la poliza debe estar llena
			If (nPolicy = eRemoteDB.Constants.intNull Or nPolicy = 0) Then
				Call lobjErrors.ErrorMessage(sCodispl, 3003)
			End If
			'+ si el producto y la poliza tienen valor el ramo debe estar lleno
			If (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) And (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
				Call lobjErrors.ErrorMessage(sCodispl, 11135)
			End If
			'+ si la poliza tienen valor el producto debe estar lleno
			If (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
				Call lobjErrors.ErrorMessage(sCodispl, 1014)
			End If
			'+ si la poliza tienen valor debe existir en el sistema
			If (nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) And (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then
				If Not oPolicy.ValExistPolicyRec(nBranch, nProduct, nPolicy, "1") Then
					Call lobjErrors.ErrorMessage(sCodispl, 3001)
				Else
					'+ si la poliza exite verifica que estado sea distinto a caputra incompleta
					If oPolicy.sStatus_pol = "3" Then
						Call lobjErrors.ErrorMessage(sCodispl, 70042)
					End If
				End If
			End If
			
			'+ si el certificado tienen valor debe existir en el sistema
			'If (nBranch <> NumNull And _
			''   nBranch <> 0) And _
			''  (nProduct <> NumNull And _
			''   nProduct <> 0) And _
			''  (nPolicy <> NumNull And _
			''   nPolicy <> 0) And _
			''  (nCertif <> NumNull) Then
			'   Set lclsCertificat = New ePolicy.Certificat
			'   If Not lclsCertificat.Find("2", CLng(nBranch), CLng(nProduct), nPolicy, nCertif) Then
			'       Call lobjErrors.ErrorMessage(sCodispl, 8215)
			'
			'   End If
			'End If
			
			'+ si la opcion es una emision de POLIZA (2) ó es Propuesta de Modificacion (6)
			'+ se deberia ingresar la poliza
			If (nOption = 2 Or nOption = 6) And nPolicy <= 0 Then
				Call lobjErrors.ErrorMessage(sCodispl, 3003)
			End If
			
			'+ si noption es igual a modificacion,
			'se debe ingresar la fecha de endoso
			'If nOption = 6 And _
			''   dteEndoso = dtmNull Then
			'    Call lobjErrors.ErrorMessage(sCodispl, 55534)
			'End If
			
			'+si tipo de cotizacion o propuesta no se ha ingresado doy el error
			If nOption <= 0 Then
				Call lobjErrors.ErrorMessage(sCodispl, 60213)
			End If
			
			
			
		Else
			
			'+ Si el tipo de ejecucion es "Masivo" se realizan las validaciones de los campos
			'+ Sucursal y Agencia.
			
			'+ si es masivo y ninguno de los campos tiene informacion
			If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) And (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) And (nOffice = eRemoteDB.Constants.intNull Or nOffice = 0) And (nAgency = eRemoteDB.Constants.intNull Or nAgency = 0) Then
				Call lobjErrors.ErrorMessage(sCodispl, 55550)
			End If
			
			'+ si la agencia tiene informacion debe estar llena la sucursal
			If (nOffice = eRemoteDB.Constants.intNull Or nOffice = 0) And (nAgency <> eRemoteDB.Constants.intNull And nAgency <> 0) Then
				Call lobjErrors.ErrorMessage(sCodispl, 55520)
			End If
			
			'+ si el producto tiene informacion debe estar lleno el ramo
			If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) And (nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0) Then
				Call lobjErrors.ErrorMessage(sCodispl, 11135)
			End If
		End If
		
		If sCodispl = "CAL01512" Then
			If dDateCopy = eRemoteDB.Constants.dtmNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 978008)
			End If
		End If
		
		insValCALXXXXX = lobjErrors.Confirm
		
insValPolicy_Err: 
		If Err.Number Then
			insValCALXXXXX = "insValCALXXXXX: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
	End Function
	
	'% insValVIL08001_k: VT00114 HAD17 Reporte de esquema de ahorro garantizado
	Public Function insValVIL08001_k(ByVal sCodispl As String, ByVal nYear As Integer, ByVal nMonth As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValVIL08001_k_Err
		
		'+ Se valida el campo año
		If nYear <= 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 9060)
		End If
		
		'+ Se valida el campo mes
		If nMonth <= 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 60267)
		Else
			If nMonth > 12 Then
				Call lobjErrors.ErrorMessage(sCodispl, 60290)
			End If
		End If
		
		insValVIL08001_k = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValVIL08001_k_Err: 
		If Err.Number Then
			insValVIL08001_k = insValVIL08001_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% VT00112  Gap 15 - Reporte de Ahorros Garantizados
	Public Function insValVIL08000(ByVal sCodispl As String, ByVal dIniDate As Date, ByVal dEndDate As Date) As String
		
		Dim lobjErrors As eFunctions.Errors
		lobjErrors = New eFunctions.Errors
		
		insValVIL08000 = String.Empty
		
		On Error GoTo insValPolicy_Err
		
		'+ Se verifica que la Fecha desde y la Fecha Hasta sean ingresadas
		If (dIniDate = eRemoteDB.Constants.dtmNull Or dEndDate = eRemoteDB.Constants.dtmNull) Then
			Call lobjErrors.ErrorMessage(sCodispl, 4157)
		End If
		
		'+ Se verifica que que la fecha desde sea menor a la fecha hasta
		If dEndDate < dIniDate Then
			Call lobjErrors.ErrorMessage(sCodispl, 60205)
		End If
		
		insValVIL08000 = lobjErrors.Confirm
		
insValPolicy_Err: 
		If Err.Number Then
			insValVIL08000 = "insValVIL08000: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	
	'% insValVIL08006_k: REPORTE DE SALDOS FINALES POR FONDO
	Public Function insValVIL08006_k(ByVal sCodispl As String, ByVal nYear As Integer, ByVal nMonth As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValVIL08001_k_Err
		
		
		
		'+ Se valida que el periodo sea menor o igual al actual
		'+ Se valida que el periodo sea menor o igual al actual
		If (nYear > Year(Now)) Then
			Call lobjErrors.ErrorMessage(sCodispl, 38006)
		ElseIf (nYear = Year(Now)) And (nMonth > Month(Now)) Then 
			Call lobjErrors.ErrorMessage(sCodispl, 38006)
		End If
		
		
		
		insValVIL08006_k = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValVIL08001_k_Err: 
		If Err.Number Then
			insValVIL08006_k = insValVIL08006_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% InsValVIL8007:
	Public Function insValVIL8007(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonth As Short, ByVal nYear As Short) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsPolicy As ePolicy.Policy
		
		On Error GoTo insValPolicy_Err
		lclsPolicy = New ePolicy.Policy
		lobjErrors = New eFunctions.Errors
		
		insValVIL8007 = String.Empty
		
		'+ El ramo debe estar lleno
		If nBranch = 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 11135)
		End If
		
		'+ El producto debe estar lleno
		If nProduct = 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 1014)
		End If
		
		'+ El año y el mes debe estar llenos
		
		If nMonth = 0 Or nYear = 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 36227)
		Else
		End If
		
		insValVIL8007 = lobjErrors.Confirm
insValPolicy_Err: 
		If Err.Number Then
			insValVIL8007 = "insValVIL8007: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		On Error GoTo 0
	End Function
	
	'% insPostVIL8007:
	Public Function insPostVIL8007(ByVal nMonth As Short, ByVal nYear As Short, ByVal nBranch As Short, ByVal nProduct As Short, ByVal sFile As String) As Boolean
		Dim lrecinsPostVIL8007 As eRemoteDB.Execute
		
		Dim lclsExcelApp As Microsoft.Office.Interop.Excel.Application
		Dim lclsWorksheet As Microsoft.Office.Interop.Excel.Worksheet
		Dim lclsValue As eFunctions.Values
		Dim lintRow As Short
		Dim lintExist As Short
		Dim lstrFile As String
		Dim lstrFileName As String
		Dim lintlength As Short
		
		lclsExcelApp = New Microsoft.Office.Interop.Excel.Application
		
		lintExist = InStr(1, UCase(sFile), ".XLS")
		If lintExist > 0 Then
			lstrFile = Mid(sFile, 1, lintExist - 1)
		Else
			lstrFile = sFile
		End If
		
		lclsValue = New eFunctions.Values
		
		lstrFileName = Trim(UCase(lclsValue.insGetSetting("MASSIVELOAD", String.Empty, "PATHS")))
		If lstrFileName = String.Empty Then
			lstrFileName = Trim(UCase(lclsValue.insGetSetting("MASSIVELOAD", String.Empty, "Config")))
		End If
		
		lintlength = Len(lstrFileName)
		If Mid(lstrFileName, lintlength, 1) <> "\" Then
			lstrFileName = lstrFileName & "\"
		End If
		
		lstrFileName = lstrFileName & Trim(lstrFile) & ".XLS"
		
		With lclsExcelApp
			.DisplayAlerts = False
			.Workbooks.Add()
			.Workbooks(1).Sheets(1).Name = "Cartola mensual"
			.Workbooks(1).Sheets(2).Delete()
			.Workbooks(1).Sheets(2).Delete()
			.Workbooks(1).Sheets(1).Activate()
			lclsWorksheet = .Workbooks(1).Sheets(1)
		End With
		
		lrecinsPostVIL8007 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostVIL8007
			.StoredProcedure = "REACARTOLA"
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				insPostVIL8007 = True
				lintRow = 1
				
				Do While Not .EOF
					'+Si es el primer registro del cursor
					If lintRow = 1 Then
						With lclsWorksheet.Range(lclsWorksheet.Cells._Default(2, 1), lclsWorksheet.Cells._Default(3, 1))
							.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
							.Font.Bold = True
						End With
						lclsWorksheet.Cells._Default(2, 1) = "Período: " & .FieldToClass("sMonth") & " " & CStr(.FieldToClass("sYear"))
						lclsWorksheet.Cells._Default(3, 1) = "Producto: " & .FieldToClass("sProduct")
						lintRow = 4
					End If
					If lintRow = 4 Then
						With lclsWorksheet.Range(lclsWorksheet.Cells._Default(4, 1), lclsWorksheet.Cells._Default(4, 160))
							.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
							.Font.Bold = True
						End With
					End If
					lclsWorksheet.Cells._Default(lintRow, 1) = .FieldToClass("SCLIENAME")
					lclsWorksheet.Cells._Default(lintRow, 2) = .FieldToClass("SCLIENT")
					lclsWorksheet.Cells._Default(lintRow, 3) = .FieldToClass("SDIGIT") '"Dig"
					lclsWorksheet.Cells._Default(lintRow, 4) = .FieldToClass("NPOLICY") '"N° póliza"
					lclsWorksheet.Cells._Default(lintRow, 5) = .FieldToClass("DSTARTDATE")
					'+Coberturas
					lclsWorksheet.Cells._Default(lintRow, 6) = .FieldToClass("F1")
					lclsWorksheet.Cells._Default(lintRow, 7) = .FieldToClass("F2")
					lclsWorksheet.Cells._Default(lintRow, 8) = .FieldToClass("F3")
					lclsWorksheet.Cells._Default(lintRow, 9) = .FieldToClass("F4")
					lclsWorksheet.Cells._Default(lintRow, 10) = .FieldToClass("F5")
					lclsWorksheet.Cells._Default(lintRow, 11) = .FieldToClass("F6")
					lclsWorksheet.Cells._Default(lintRow, 12) = .FieldToClass("F7")
					lclsWorksheet.Cells._Default(lintRow, 13) = .FieldToClass("F8")
					lclsWorksheet.Cells._Default(lintRow, 14) = .FieldToClass("F9")
					lclsWorksheet.Cells._Default(lintRow, 15) = .FieldToClass("F10")
					
					lclsWorksheet.Cells._Default(lintRow, 16) = .FieldToClass("NPERIODPREMIUM")
					lclsWorksheet.Cells._Default(lintRow, 17) = .FieldToClass("SPAYFREQ")
					lclsWorksheet.Cells._Default(lintRow, 18) = .FieldToClass("NRATE")
					lclsWorksheet.Cells._Default(lintRow, 19) = .FieldToClass("NRATE_APV")
					lclsWorksheet.Cells._Default(lintRow, 20) = .FieldToClass("NVP_OLD")
					lclsWorksheet.Cells._Default(lintRow, 21) = .FieldToClass("NPAYPREMIUM")
					lclsWorksheet.Cells._Default(lintRow, 22) = .FieldToClass("NCOVER_COST")
					lclsWorksheet.Cells._Default(lintRow, 23) = .FieldToClass("NCOST_TOT")
					lclsWorksheet.Cells._Default(lintRow, 24) = .FieldToClass("NSURRAMOUNT")
					lclsWorksheet.Cells._Default(lintRow, 25) = .FieldToClass("NVARIATION")
					lclsWorksheet.Cells._Default(lintRow, 26) = .FieldToClass("NPERMAMOUNT")
					lclsWorksheet.Cells._Default(lintRow, 27) = .FieldToClass("NVP_ACT")
					lclsWorksheet.Cells._Default(lintRow, 28) = .FieldToClass("SINTERMED", String.Empty) '"Nombre ejecutivo"
					lclsWorksheet.Cells._Default(lintRow, 29) = .FieldToClass("SE_MAIL", String.Empty) '"E-Mail Intermediario"
					lclsWorksheet.Cells._Default(lintRow, 30) = .FieldToClass("SPHONE", String.Empty) '"Teléfono Intermediario"
					lclsWorksheet.Cells._Default(lintRow, 31) = .FieldToClass("SSUPERINTERMED", String.Empty) '"Nombre supervisor"
					lclsWorksheet.Cells._Default(lintRow, 32) = .FieldToClass("SSUPERE_MAIL", String.Empty) '"E-Mail supervisor"
					lclsWorksheet.Cells._Default(lintRow, 33) = .FieldToClass("SSUPERPHONE", String.Empty) '"Teléfono supervisor"
					'+ Distribución de primas
					lclsWorksheet.Cells._Default(lintRow, 34) = .FieldToClass("F11")
					lclsWorksheet.Cells._Default(lintRow, 35) = .FieldToClass("F12")
					lclsWorksheet.Cells._Default(lintRow, 36) = .FieldToClass("F13")
					lclsWorksheet.Cells._Default(lintRow, 37) = .FieldToClass("F14")
					lclsWorksheet.Cells._Default(lintRow, 38) = .FieldToClass("F15")
					lclsWorksheet.Cells._Default(lintRow, 39) = .FieldToClass("F16")
					lclsWorksheet.Cells._Default(lintRow, 40) = .FieldToClass("F17")
					lclsWorksheet.Cells._Default(lintRow, 41) = .FieldToClass("F18")
					lclsWorksheet.Cells._Default(lintRow, 42) = .FieldToClass("F19")
					lclsWorksheet.Cells._Default(lintRow, 43) = .FieldToClass("F20")
					lclsWorksheet.Cells._Default(lintRow, 44) = .FieldToClass("F21")
					lclsWorksheet.Cells._Default(lintRow, 45) = .FieldToClass("F22")
					lclsWorksheet.Cells._Default(lintRow, 46) = .FieldToClass("F23")
					lclsWorksheet.Cells._Default(lintRow, 47) = .FieldToClass("F24")
					lclsWorksheet.Cells._Default(lintRow, 48) = .FieldToClass("F25")
					lclsWorksheet.Cells._Default(lintRow, 49) = .FieldToClass("F26")
					lclsWorksheet.Cells._Default(lintRow, 50) = .FieldToClass("F27")
					lclsWorksheet.Cells._Default(lintRow, 51) = .FieldToClass("F28")
					lclsWorksheet.Cells._Default(lintRow, 52) = .FieldToClass("F29")
					lclsWorksheet.Cells._Default(lintRow, 53) = .FieldToClass("F30")
					lclsWorksheet.Cells._Default(lintRow, 54) = .FieldToClass("F31")
					lclsWorksheet.Cells._Default(lintRow, 55) = .FieldToClass("F32")
					lclsWorksheet.Cells._Default(lintRow, 56) = .FieldToClass("F33")
					lclsWorksheet.Cells._Default(lintRow, 57) = .FieldToClass("F34")
					lclsWorksheet.Cells._Default(lintRow, 58) = .FieldToClass("F35")
					lclsWorksheet.Cells._Default(lintRow, 59) = .FieldToClass("F36")
					lclsWorksheet.Cells._Default(lintRow, 60) = .FieldToClass("F37")
					lclsWorksheet.Cells._Default(lintRow, 61) = .FieldToClass("F38")
					lclsWorksheet.Cells._Default(lintRow, 62) = .FieldToClass("F39")
					lclsWorksheet.Cells._Default(lintRow, 63) = .FieldToClass("F40")
					lclsWorksheet.Cells._Default(lintRow, 64) = .FieldToClass("F41")
					lclsWorksheet.Cells._Default(lintRow, 65) = .FieldToClass("F42")
					lclsWorksheet.Cells._Default(lintRow, 66) = .FieldToClass("F43")
					lclsWorksheet.Cells._Default(lintRow, 67) = .FieldToClass("F44")
					lclsWorksheet.Cells._Default(lintRow, 68) = .FieldToClass("F45")
					lclsWorksheet.Cells._Default(lintRow, 69) = .FieldToClass("F46")
					lclsWorksheet.Cells._Default(lintRow, 70) = .FieldToClass("F47")
					lclsWorksheet.Cells._Default(lintRow, 71) = .FieldToClass("F48")
					lclsWorksheet.Cells._Default(lintRow, 72) = .FieldToClass("F49")
					lclsWorksheet.Cells._Default(lintRow, 73) = .FieldToClass("F50")
					lclsWorksheet.Cells._Default(lintRow, 74) = .FieldToClass("F51")
					lclsWorksheet.Cells._Default(lintRow, 75) = .FieldToClass("F52")
					lclsWorksheet.Cells._Default(lintRow, 76) = .FieldToClass("F53")
					lclsWorksheet.Cells._Default(lintRow, 77) = .FieldToClass("F54")
					lclsWorksheet.Cells._Default(lintRow, 78) = .FieldToClass("F55")
					lclsWorksheet.Cells._Default(lintRow, 79) = .FieldToClass("F56")
					lclsWorksheet.Cells._Default(lintRow, 80) = .FieldToClass("F57")
					lclsWorksheet.Cells._Default(lintRow, 81) = .FieldToClass("F58")
					lclsWorksheet.Cells._Default(lintRow, 82) = .FieldToClass("F59")
					lclsWorksheet.Cells._Default(lintRow, 83) = .FieldToClass("F60")
					lclsWorksheet.Cells._Default(lintRow, 84) = .FieldToClass("F61")
					lclsWorksheet.Cells._Default(lintRow, 85) = .FieldToClass("F62")
					lclsWorksheet.Cells._Default(lintRow, 86) = .FieldToClass("F63")
					lclsWorksheet.Cells._Default(lintRow, 87) = .FieldToClass("F64")
					lclsWorksheet.Cells._Default(lintRow, 88) = .FieldToClass("F65")
					lclsWorksheet.Cells._Default(lintRow, 89) = .FieldToClass("F66")
					lclsWorksheet.Cells._Default(lintRow, 90) = .FieldToClass("F67")
					lclsWorksheet.Cells._Default(lintRow, 91) = .FieldToClass("F68")
					lclsWorksheet.Cells._Default(lintRow, 92) = .FieldToClass("F69")
					lclsWorksheet.Cells._Default(lintRow, 93) = .FieldToClass("F70")
					lclsWorksheet.Cells._Default(lintRow, 94) = .FieldToClass("F71")
					lclsWorksheet.Cells._Default(lintRow, 95) = .FieldToClass("F72")
					lclsWorksheet.Cells._Default(lintRow, 96) = .FieldToClass("F73")
					lclsWorksheet.Cells._Default(lintRow, 97) = .FieldToClass("F74")
					lclsWorksheet.Cells._Default(lintRow, 98) = .FieldToClass("F75")
					lclsWorksheet.Cells._Default(lintRow, 99) = .FieldToClass("F76")
					lclsWorksheet.Cells._Default(lintRow, 100) = .FieldToClass("F77")
					lclsWorksheet.Cells._Default(lintRow, 101) = .FieldToClass("F78")
					lclsWorksheet.Cells._Default(lintRow, 102) = .FieldToClass("F79")
					lclsWorksheet.Cells._Default(lintRow, 103) = .FieldToClass("F80")
					lclsWorksheet.Cells._Default(lintRow, 104) = .FieldToClass("F81")
					lclsWorksheet.Cells._Default(lintRow, 105) = .FieldToClass("F82")
					lclsWorksheet.Cells._Default(lintRow, 106) = .FieldToClass("F83")
					lclsWorksheet.Cells._Default(lintRow, 107) = .FieldToClass("F84")
					lclsWorksheet.Cells._Default(lintRow, 108) = .FieldToClass("F85")
					lclsWorksheet.Cells._Default(lintRow, 109) = .FieldToClass("F86")
					lclsWorksheet.Cells._Default(lintRow, 110) = .FieldToClass("F87")
					lclsWorksheet.Cells._Default(lintRow, 111) = .FieldToClass("F88")
					lclsWorksheet.Cells._Default(lintRow, 112) = .FieldToClass("F89")
					lclsWorksheet.Cells._Default(lintRow, 113) = .FieldToClass("F90")
					lclsWorksheet.Cells._Default(lintRow, 114) = .FieldToClass("F91")
					lclsWorksheet.Cells._Default(lintRow, 115) = .FieldToClass("F92")
					lclsWorksheet.Cells._Default(lintRow, 116) = .FieldToClass("F93")
					lclsWorksheet.Cells._Default(lintRow, 117) = .FieldToClass("F94")
					lclsWorksheet.Cells._Default(lintRow, 118) = .FieldToClass("F95")
					lclsWorksheet.Cells._Default(lintRow, 119) = .FieldToClass("F96")
					lclsWorksheet.Cells._Default(lintRow, 120) = .FieldToClass("F97")
					lclsWorksheet.Cells._Default(lintRow, 121) = .FieldToClass("F98")
					lclsWorksheet.Cells._Default(lintRow, 122) = .FieldToClass("F99")
					lclsWorksheet.Cells._Default(lintRow, 123) = .FieldToClass("F100")
					lclsWorksheet.Cells._Default(lintRow, 124) = .FieldToClass("F101")
					lclsWorksheet.Cells._Default(lintRow, 125) = .FieldToClass("F102")
					lclsWorksheet.Cells._Default(lintRow, 126) = .FieldToClass("F103")
					lclsWorksheet.Cells._Default(lintRow, 127) = .FieldToClass("F104")
					lclsWorksheet.Cells._Default(lintRow, 128) = .FieldToClass("F105")
					lclsWorksheet.Cells._Default(lintRow, 129) = .FieldToClass("F106")
					lclsWorksheet.Cells._Default(lintRow, 130) = .FieldToClass("F107")
					lclsWorksheet.Cells._Default(lintRow, 131) = .FieldToClass("F108")
					lclsWorksheet.Cells._Default(lintRow, 132) = .FieldToClass("F109")
					lclsWorksheet.Cells._Default(lintRow, 133) = .FieldToClass("F110")
					lclsWorksheet.Cells._Default(lintRow, 134) = .FieldToClass("F111")
					lclsWorksheet.Cells._Default(lintRow, 135) = .FieldToClass("F112")
					lclsWorksheet.Cells._Default(lintRow, 136) = .FieldToClass("F113")
					lclsWorksheet.Cells._Default(lintRow, 137) = .FieldToClass("F114")
					lclsWorksheet.Cells._Default(lintRow, 138) = .FieldToClass("F115")
					lclsWorksheet.Cells._Default(lintRow, 139) = .FieldToClass("F116")
					lclsWorksheet.Cells._Default(lintRow, 140) = .FieldToClass("F117")
					lclsWorksheet.Cells._Default(lintRow, 141) = .FieldToClass("F118")
					lclsWorksheet.Cells._Default(lintRow, 142) = .FieldToClass("F119")
					lclsWorksheet.Cells._Default(lintRow, 143) = .FieldToClass("F120")
					lclsWorksheet.Cells._Default(lintRow, 144) = .FieldToClass("F121")
					lclsWorksheet.Cells._Default(lintRow, 145) = .FieldToClass("F122")
					lclsWorksheet.Cells._Default(lintRow, 146) = .FieldToClass("F123")
					lclsWorksheet.Cells._Default(lintRow, 147) = .FieldToClass("F124")
					lclsWorksheet.Cells._Default(lintRow, 148) = .FieldToClass("F125")
					lclsWorksheet.Cells._Default(lintRow, 149) = .FieldToClass("F126")
					lclsWorksheet.Cells._Default(lintRow, 150) = .FieldToClass("F127")
					lclsWorksheet.Cells._Default(lintRow, 151) = .FieldToClass("F128")
					lclsWorksheet.Cells._Default(lintRow, 152) = .FieldToClass("F129")
					lclsWorksheet.Cells._Default(lintRow, 153) = .FieldToClass("F130")
					lclsWorksheet.Cells._Default(lintRow, 154) = .FieldToClass("F131")
					lclsWorksheet.Cells._Default(lintRow, 155) = .FieldToClass("F132")
					lclsWorksheet.Cells._Default(lintRow, 156) = .FieldToClass("F133")
					lclsWorksheet.Cells._Default(lintRow, 157) = .FieldToClass("F134")
					lclsWorksheet.Cells._Default(lintRow, 158) = .FieldToClass("F135")
					lclsWorksheet.Cells._Default(lintRow, 159) = .FieldToClass("F136")
					lclsWorksheet.Cells._Default(lintRow, 160) = .FieldToClass("F137")
					
					lintRow = lintRow + 1
					.RNext()
				Loop 
				.RCloseRec()
			Else
				insPostVIL8007 = False
			End If
		End With
		
		With lclsExcelApp
			.ActiveWorkbook.SaveAs(lstrFileName)
			
			.ActiveWorkbook.Close()
			.Quit()
		End With
		
		'UPGRADE_NOTE: Object lclsExcelApp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExcelApp = Nothing
		
		'UPGRADE_NOTE: Object lrecinsPostVIL8007 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostVIL8007 = Nothing
	End Function
	
	'% insValVIL8005_k: Reporte de Esquema APV
	Public Function insValVIL8005_k(ByVal sCodispl As String, ByVal nYear As Integer, ByVal nMonth As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValVIL8005_k_Err
		
		'+ Se valida que el periodo sea menor o igual al actual
		If (nYear > Year(Now) Or nMonth > Month(Now)) Then
			Call lobjErrors.ErrorMessage(sCodispl, 38006)
		End If
		
		insValVIL8005_k = lobjErrors.Confirm
		
insValVIL8005_k_Err: 
		If Err.Number Then
			insValVIL8005_k = "insValVIL8005_k: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insReportExcel_VIL8005: Exporta planilla excel
	Public Function insReportExcel_VIL8005(ByVal sFile As String, ByVal sPath As String, ByVal sMonth As String, ByVal sYear As String) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		Dim lstrWritTxt As String
		Dim FileName As String
		Dim FileNum As String
		Dim lintlength As Integer
		
		Dim ncount As Integer
		On Error GoTo insReportExcel_VIL8005_Err
		
		insReportExcel_VIL8005 = True
		'+Si el directorio no incorpora linea se le agrega
		lintlength = Len(sPath)
		If Mid(sPath, lintlength, 1) <> "\" Then
			sPath = sPath & "\"
		End If
		
		FileName = sPath & sFile & ".xls"
		
        FileNum = CStr(FreeFile())
		FileOpen(CInt(FileNum), FileName, OpenMode.Output)
		
		' Generar encabezado
        lstrWritTxt = "Póliza" & Chr(9) & "Fecha de emisión" & Chr(9) & "Fecha de inicio de vigencia" & Chr(9) & "Fecha de fin de vigencia" & Chr(9) & "Plan" & Chr(9) & "Frecuencia de pago" & Chr(9) & "Fecha de nacimiento" & Chr(9) & "Sexo" & Chr(9) & "Código Tabla" & Chr(9) & "Fecha de cobertura pagada" & Chr(9) & "Prima pagada" & Chr(9) & "Prima mínima" & Chr(9) & "Prima de fallecimiento" & Chr(9) & "Prima fondo" & Chr(9) & "Rescates" & Chr(9) & "Valor Póliza Anterior" & Chr(9) & "Valor Póliza Actual" & Chr(9) & "Reservas" & Chr(9) & "Número de cuotas anterior" & Chr(9) & "Número de cuotas actual" & Chr(9) & "Estado fumador" & Chr(9) & "Retención de impuesto sobre rescates" & Chr(9) & "Valor póliza anterior" & Chr(9) & "Reserva del bono de permanencia actual" & Chr(9) & "Reserva del bono de permanencia anterior"
		PrintLine(CInt(FileNum), "")
		
		PrintLine(CInt(FileNum), lstrWritTxt)
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "INSVIL8005PKG.REAVIL8005"
			.Parameters.Add("sKey", sFile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMonth", sMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sYear", sYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Do While Not .EOF
					lstrWritTxt = .FieldToClass("npolicy") & Chr(9) & .FieldToClass("dissuedat") & Chr(9) & .FieldToClass("dstartdate") & Chr(9) & .FieldToClass("dexpirdat") & Chr(9) & .FieldToClass("nproduct") & Chr(9) & .FieldToClass("sdescript") & Chr(9) & .FieldToClass("dbirthdat") & Chr(9) & .FieldToClass("ssexclien") & Chr(9) & .FieldToClass("smortalco") & Chr(9) & .FieldToClass("dstatdate") & Chr(9) & .FieldToClass("npayed_premium") & Chr(9) & .FieldToClass("nmin_premium") & Chr(9) & .FieldToClass("ndeath_premium") & Chr(9) & .FieldToClass("nfund_premium") & Chr(9) & .FieldToClass("namount_res") & Chr(9) & .FieldToClass("nvp_bef") & Chr(9) & .FieldToClass("nvp_act_1_n") & Chr(9) & .FieldToClass("nreserve_1_n") & Chr(9) & .FieldToClass("namount_quot_bef") & Chr(9) & .FieldToClass("namount_quot_act") & Chr(9) & .FieldToClass("ssmoking") & Chr(9) & .FieldToClass("nres_tax") & Chr(9) & .FieldToClass("nvp_bef_1_n") & Chr(9) & .FieldToClass("nres_act") & Chr(9) & .FieldToClass("nres_bef")
					PrintLine(CInt(FileNum), lstrWritTxt)
					.RNext()
				Loop 
				'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lrecTime = Nothing
			End If
		End With
		FileClose(CInt(FileNum))
		
insReportExcel_VIL8005_Err: 
		If Err.Number Then
			insReportExcel_VIL8005 = False
		End If
		On Error Resume Next
		FileClose(CInt(FileNum))
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
		On Error GoTo 0
	End Function
	
	'% insValVIL8009_k: Reservas por Producto de Ahorros Garantizados
	Public Function insValVIL8009_k(ByVal sCodispl As String, ByVal nYear As Integer, ByVal nMonth As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsctrol_date As eGeneral.Ctrol_date
		Dim dEffecdate As Date
		
		lobjErrors = New eFunctions.Errors
		lclsctrol_date = New eGeneral.Ctrol_date
		
		On Error GoTo insValVIL8009_k_Err
		
		'+ Se valida que el periodo sea menor o igual al actual
		With lobjErrors
			If nYear <= 0 Or nMonth <= 0 Then
				.ErrorMessage(sCodispl, 36227)
			End If
			
			'+ Se valida que el periodo sea menor o igual al actual
			If (nYear > Year(Now) Or nMonth > Month(Now)) Then
				.ErrorMessage(sCodispl, 38006)
			End If
			
			If nYear > 0 And nMonth > 0 Then
				dEffecdate = DateSerial(nYear, nMonth + 1, 0)
				If lclsctrol_date.Find(102) Then
					If dEffecdate > lclsctrol_date.dEffecdate Then
						.ErrorMessage(sCodispl, 36037)
					End If
				End If
			End If
			insValVIL8009_k = .Confirm
		End With
		
insValVIL8009_k_Err: 
		If Err.Number Then
			insValVIL8009_k = "insValVIL8009_k: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsctrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsctrol_date = Nothing
	End Function
	
	'%insReportExcel_VIL8009: Exporta planilla excel
	Public Function insReportExcel_VIL8009(ByVal sFile As String, ByVal sPath As String, ByVal sMonth As String, ByVal sYear As String) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		Dim lstrWritTxt As String
		Dim FileName As String
		Dim FileNum As String
		Dim lintlength As Integer
		
		Dim ncount As Integer
		On Error GoTo insReportExcel_VIL8009_Err
		
		insReportExcel_VIL8009 = True
		'+Si el directorio no incorpora linea se le agrega
		lintlength = Len(sPath)
		If Mid(sPath, lintlength, 1) <> "\" Then
			sPath = sPath & "\"
		End If
		
		FileName = sPath & sFile & ".xls"
		
        FileNum = CStr(FreeFile())
		FileOpen(CInt(FileNum), FileName, OpenMode.Output)
		
		' Generar encabezado
		lstrWritTxt = "Ramo" & Chr(9) & "Póliza" & Chr(9) & "Ahorro Garantizado" & Chr(9) & "Plazo " & Chr(9) & "Valor vencimiento" & Chr(9) & "Moneda" & Chr(9) & "Producto (Código)" & Chr(9) & "Producto" & Chr(9) & "Prima única" & Chr(9) & "Valor cuota garantizada" & Chr(9) & "Reserva" & Chr(9) & "Fecha vencimiento" & Chr(9) & "Periodo"
		
		PrintLine(CInt(FileNum), "")
		
		PrintLine(CInt(FileNum), lstrWritTxt)
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "INSVIL8009PKG.REAVIL8009"
			.Parameters.Add("sMonth", sMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sYear", sYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Do While Not .EOF
					lstrWritTxt = .FieldToClass("nbranch") & Chr(9) & .FieldToClass("npolicy") & Chr(9) & .FieldToClass("nguarsavid") & Chr(9) & .FieldToClass("nguarsav_year") & Chr(9) & .FieldToClass("nguarsav_value") & Chr(9) & .FieldToClass("ncurrency") & Chr(9) & .FieldToClass("nproduct") & Chr(9) & .FieldToClass("sproduct") & Chr(9) & .FieldToClass("nguarsav_prem") & Chr(9) & .FieldToClass("nguarsav_cost") & Chr(9) & .FieldToClass("nguarreserve") & Chr(9) & .FieldToClass("dend_guarsav") & Chr(9) & .FieldToClass("denddate")
					PrintLine(CInt(FileNum), lstrWritTxt)
					.RNext()
				Loop 
				'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lrecTime = Nothing
			End If
		End With
		FileClose(CInt(FileNum))
		
insReportExcel_VIL8009_Err: 
		If Err.Number Then
			insReportExcel_VIL8009 = False
		End If
		On Error Resume Next
		FileClose(CInt(FileNum))
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
		On Error GoTo 0
	End Function
	
	'% insValVIL8012_k: Reporte de FECU Corredores
	Public Function insValVIL8012_k(ByVal sCodispl As String, ByVal dIniDate As Date, ByVal dEndDate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValVIL8012_k_Err
		
		'+ Se valida el campo "Fecha desde"
		If dIniDate = eRemoteDB.Constants.dtmNull Then
			lobjErrors.ErrorMessage(sCodispl, 60217)
		End If
		
		'+ Se valida el campo "Fecha hasta"
		If dEndDate = eRemoteDB.Constants.dtmNull Then
			lobjErrors.ErrorMessage(sCodispl, 60218)
		Else
			If dIniDate <> eRemoteDB.Constants.dtmNull Then
				If dEndDate < dIniDate Then
					lobjErrors.ErrorMessage(sCodispl, 6130)
				End If
			End If
		End If
		
		
		insValVIL8012_k = lobjErrors.Confirm
		
insValVIL8012_k_Err: 
		If Err.Number Then
			insValVIL8012_k = "insValVIL8012_k: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% InsValVIL8030: Reporte de resumen de libro de producción foliado
	Public Function insValVIL8030(ByVal sCodispl As String, ByVal dIniDate As Date, ByVal dEndDate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValPolicy_Err
		lobjErrors = New eFunctions.Errors
		
		insValVIL8030 = String.Empty
		
		With lobjErrors
			'+ La fecha desde debe estar llena
			If dIniDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 12081)
			End If
			
			'+ La fecha hasta debe estar llena
			If dEndDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 60218)
			End If
			
			'+ La fecha desde no debe ser posterior a la fecha hasta
			If dIniDate <> eRemoteDB.Constants.dtmNull And dEndDate <> eRemoteDB.Constants.dtmNull And dIniDate > dEndDate Then
				.ErrorMessage(sCodispl, 7165)
			End If
		End With
		
		insValVIL8030 = lobjErrors.Confirm
		
insValPolicy_Err: 
		If Err.Number Then
			insValVIL8030 = "insValVIL8030: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% insValVIL8010_k: Reporte de FECU Mensual Interno
	Public Function insValVIL8010_k(ByVal sCodispl As String, ByVal dEndDate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValVIL8010_k_Err
		
		'+ Se valida el campo "Fecha hasta"
		With lobjErrors
			If dEndDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 36227)
			Else
				If dEndDate > Today Then
					.ErrorMessage(sCodispl, 38006)
				End If
			End If
		End With
		
		insValVIL8010_k = lobjErrors.Confirm
		
insValVIL8010_k_Err: 
		If Err.Number Then
			insValVIL8010_k = "insValVIL8010_k: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% InsValVIL8031: Reporte de resumen de libro de producción
	Public Function insValVIL8031(ByVal sCodispl As String, ByVal dIniDate As Date, ByVal dEndDate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValPolicy_Err
		lobjErrors = New eFunctions.Errors
		
		insValVIL8031 = String.Empty
		
		With lobjErrors
			'+ La fecha desde debe estar llena
			If dIniDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 12081)
			End If
			
			'+ La fecha hasta debe estar llena
			If dEndDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 60218)
			End If
			
			'+ La fecha desde no debe ser posterior a la fecha hasta
			If dIniDate <> eRemoteDB.Constants.dtmNull And dEndDate <> eRemoteDB.Constants.dtmNull And dIniDate > dEndDate Then
				.ErrorMessage(sCodispl, 7165)
			End If
		End With
		
		insValVIL8031 = lobjErrors.Confirm
		
insValPolicy_Err: 
		If Err.Number Then
			insValVIL8031 = "insValVIL8031: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
	
	
	
	'% HAD033 VIL8032 REPORTE DE RESUMEN PRODUCCION POR OFICINA
	Public Function insValVIL8032(ByVal sCodispl As String, ByVal dIniDate As Date, ByVal dEndDate As Date) As String
		
		Dim lobjErrors As eFunctions.Errors
		lobjErrors = New eFunctions.Errors
		
		insValVIL8032 = String.Empty
		
		On Error GoTo insValPolicy_Err
		
		'+ Si la fecha inicial es diferente de vacio continua las validaciones
		If dIniDate = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 9071)
		End If
		'+ Si la fecha final es diferente de vacio continua las validaciones
		If dEndDate = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 9072)
		End If
		'+ Se verifica que que la fecha final sea mayor a la fecha inicial
		If dEndDate < dIniDate Then
			Call lobjErrors.ErrorMessage(sCodispl, 4159)
		End If
		'+ Se verifica que la fecha final no sea mayor a la fecha del día
		If dEndDate > Today Then
			Call lobjErrors.ErrorMessage(sCodispl, 4341)
		End If
		
		insValVIL8032 = lobjErrors.Confirm
		
insValPolicy_Err: 
		If Err.Number Then
			insValVIL8032 = "insValVIL8032: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	
	
	'% insValVIL8033: HAD034 - Reporte de Resumen de Produccion por cobertura
	Public Function insValVIL8033(ByVal sCodispl As String, ByVal nYear As Integer, ByVal nMonth As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		lobjErrors = New eFunctions.Errors
		On Error GoTo insValPolicy_Err
		
		'+ Se valida el campo año
		If nYear <= 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 9060)
		End If
		
		'+ Se valida el campo mes
		If nMonth <= 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 60267)
		Else
			If nMonth > 12 Then
				Call lobjErrors.ErrorMessage(sCodispl, 60290)
			End If
		End If
		
		'+ Se valida que el periodo sea menor o igual al actual
		If (nYear > Year(Now)) Then
			Call lobjErrors.ErrorMessage(sCodispl, 38006)
		ElseIf (nYear = Year(Now)) And (nMonth > Month(Now)) Then 
			Call lobjErrors.ErrorMessage(sCodispl, 38006)
		End If
		
		insValVIL8033 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValPolicy_Err: 
		If Err.Number Then
			insValVIL8033 = insValVIL8033 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	
	'% insValCAL803: HAD 27 - Reporte de Cobranza Indiapv por Póliza
	Public Function insValCAL803(ByVal sCodispl As String, ByVal nYear As Integer, ByVal nMonth As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		lobjErrors = New eFunctions.Errors
		On Error GoTo insValPolicy_Err
		
		'+ Se valida el campo año
		If nYear <= 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 9060)
		End If
		
		'+ Se valida el campo mes
		If nMonth <= 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 60267)
		Else
			If nMonth > 12 Then
				Call lobjErrors.ErrorMessage(sCodispl, 60290)
			End If
		End If
		
		'+ Se valida que el periodo sea menor o igual al actual
		If (nYear > Year(Now)) Then
			Call lobjErrors.ErrorMessage(sCodispl, 38006)
		ElseIf (nYear = Year(Now)) And (nMonth > Month(Now)) Then 
			Call lobjErrors.ErrorMessage(sCodispl, 38006)
		End If
		
		insValCAL803 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValPolicy_Err: 
		If Err.Number Then
			insValCAL803 = insValCAL803 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	
	'% insPostCAL803: Reporte de Cobranza Indiapv por Póliza
	Public Function insPostCAL803(ByVal nMonth As Short, ByVal nYear As Short, ByVal sFile As String) As Boolean
		Dim lrecinsPostCAL803 As eRemoteDB.Execute
		
		Dim lclsExcelApp As Microsoft.Office.Interop.Excel.Application
		Dim lclsWorksheet As Microsoft.Office.Interop.Excel.Worksheet
		Dim lclsValue As eFunctions.Values
		Dim lintRow As Short
		Dim lintExist As Short
		Dim lstrFile As String
		Dim lstrFileName As String
		Dim lintlength As Short
		
		lclsExcelApp = New Microsoft.Office.Interop.Excel.Application
		
		lintExist = InStr(1, UCase(sFile), ".XLS")
		If lintExist > 0 Then
			lstrFile = Mid(sFile, 1, lintExist - 1)
		Else
			lstrFile = sFile
		End If
		
		lclsValue = New eFunctions.Values
		
		lstrFileName = Trim(UCase(lclsValue.insGetSetting("MASSIVELOAD", String.Empty, "PATHS")))
		If lstrFileName = String.Empty Then
			lstrFileName = Trim(UCase(lclsValue.insGetSetting("MASSIVELOAD", String.Empty, "Config")))
		End If
		
		lintlength = Len(lstrFileName)
		If Mid(lstrFileName, lintlength, 1) <> "\" Then
			lstrFileName = lstrFileName & "\"
		End If
		
		lstrFileName = lstrFileName & Trim(lstrFile) & ".XLS"
		
		With lclsExcelApp
			.DisplayAlerts = False
			.Workbooks.Add()
			.Workbooks(1).Sheets(1).Name = "Extracción de datos"
			.Workbooks(1).Sheets(2).Name = "Resumen por vía de pago"
			.Workbooks(1).Sheets(1).Activate()
			lclsWorksheet = .Workbooks(1).Sheets(1)
		End With
		
		lrecinsPostCAL803 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCAL803
			.StoredProcedure = "REA_CAL803"
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				
				insPostCAL803 = True
				
				
				lclsWorksheet.Cells._Default(5, 1) = "Código de sucursal"
                lclsWorksheet.Cells._Default(5, 2) = "RUT del contratante"
				lclsWorksheet.Cells._Default(5, 3) = "Dígito verificador del contratante"
				lclsWorksheet.Cells._Default(5, 4) = "Número de cuenta para pago de PAC/PAT"
				lclsWorksheet.Cells._Default(5, 5) = "Código de convenio de vía de pago"
				lclsWorksheet.Cells._Default(5, 6) = "Nombre de entidad pago PAC/PAT"
				lclsWorksheet.Cells._Default(5, 7) = "Cuentas cerradas"
				lclsWorksheet.Cells._Default(5, 8) = "Valor en UF para prima en pesos"
				lclsWorksheet.Cells._Default(5, 9) = "Valor afecto en UF de prima periodizada"
				lclsWorksheet.Cells._Default(5, 10) = "Valor exento en UF de prima periodizada"
				lclsWorksheet.Cells._Default(5, 11) = "Valor IVA en UF de prima periodizada"
				lclsWorksheet.Cells._Default(5, 12) = "Fecha de valorización"
				lclsWorksheet.Cells._Default(5, 13) = "Prima bruta en UF"
				lclsWorksheet.Cells._Default(5, 14) = "Saldo insoluto en UF"
				lclsWorksheet.Cells._Default(5, 15) = "Prima bruta en pesos"
				lclsWorksheet.Cells._Default(5, 16) = "Valor afecto en pesos de prima periodizada"
				lclsWorksheet.Cells._Default(5, 17) = "Valor exento en pesos de prima periodizada"
				lclsWorksheet.Cells._Default(5, 18) = "Valor IVA en pesos de prima periodizada"
				lclsWorksheet.Cells._Default(5, 19) = "Saldo insoluto en pesos"
				lclsWorksheet.Cells._Default(5, 20) = "Número de póliza"
				lclsWorksheet.Cells._Default(5, 21) = "Código de producto de la póliza"
				lclsWorksheet.Cells._Default(5, 22) = "Código de vía de pago"
				lclsWorksheet.Cells._Default(5, 23) = "Descripción de vía de pago"
                lclsWorksheet.Cells._Default(5, 24) = "RUT de la entidad de convenio"
				lclsWorksheet.Cells._Default(5, 25) = "Dígito verificador de la entidad de convenio"
				lclsWorksheet.Cells._Default(5, 26) = "Fecha de inicio de vigencia de la cobertura"
				lclsWorksheet.Cells._Default(5, 27) = "Fecha de fin de vigencia de la cobertura"
				
				
				lintRow = 6
				
				Do While Not .EOF
					
					lclsWorksheet.Cells._Default(lintRow, 1) = .FieldToClass("NOFFICE") '"cod de sucursal"
					lclsWorksheet.Cells._Default(lintRow, 2) = .FieldToClass("SRUT") '"RUT"
					lclsWorksheet.Cells._Default(lintRow, 3) = .FieldToClass("SDIGIT") '"Dig"
					lclsWorksheet.Cells._Default(lintRow, 3).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
					lclsWorksheet.Cells._Default(lintRow, 4) = .FieldToClass("CUENTA_PACPAT") '"CUENTA PAC-PAT"
					lclsWorksheet.Cells._Default(lintRow, 5) = .FieldToClass("CODIGO_CONVENIOPACPAT", String.Empty) '"Cod de convenio PAC-PAT"
					lclsWorksheet.Cells._Default(lintRow, 6) = .FieldToClass("NOMBRE_CONVENIOPACPAT", String.Empty) '"Nombre de convenio PAC-PAT"
					lclsWorksheet.Cells._Default(lintRow, 7) = .FieldToClass("CUENTASCERRADAS", String.Empty) '"Cuentas cerradas"
					lclsWorksheet.Cells._Default(lintRow, 8) = .FieldToClass("VALOR_UF", String.Empty) '"Valor UF"
					lclsWorksheet.Cells._Default(lintRow, 9) = .FieldToClass("PRIMA_AFECTA_PERIODIZADA_UF", String.Empty)
					lclsWorksheet.Cells._Default(lintRow, 10) = .FieldToClass("PRIMA_EXENTA_PERIODIZADA_UF", String.Empty)
					lclsWorksheet.Cells._Default(lintRow, 11) = .FieldToClass("IVA_PRIMA_PERIODIZADA_UF", String.Empty)
					lclsWorksheet.Cells._Default(lintRow, 12) = .FieldToClass("FECHAVALORIZACION")
					lclsWorksheet.Cells._Default(lintRow, 13) = .FieldToClass("NPREMIUM")
					lclsWorksheet.Cells._Default(lintRow, 14) = .FieldToClass("SALDO_INSOLUTO_UF")
					lclsWorksheet.Cells._Default(lintRow, 15) = .FieldToClass("PRIMABRUTAPESOS")
					lclsWorksheet.Cells._Default(lintRow, 16) = .FieldToClass("PRIMA_AFECTA_PERIODIZADA_PESOS")
					lclsWorksheet.Cells._Default(lintRow, 17) = .FieldToClass("PRIMA_EXENTA_PERIODIZADA_PESOS")
					lclsWorksheet.Cells._Default(lintRow, 18) = .FieldToClass("IVA_PRIMA_PERIODIZADA_PESOS")
					lclsWorksheet.Cells._Default(lintRow, 19) = .FieldToClass("SALDO_INSOLUTO_PESOS")
					lclsWorksheet.Cells._Default(lintRow, 20) = .FieldToClass("NPOLICY")
					lclsWorksheet.Cells._Default(lintRow, 21) = .FieldToClass("NPRODUCT")
					lclsWorksheet.Cells._Default(lintRow, 22) = .FieldToClass("NWAY_PAY")
					lclsWorksheet.Cells._Default(lintRow, 23) = .FieldToClass("DESCWAYPAY")
					lclsWorksheet.Cells._Default(lintRow, 24) = .FieldToClass("RUT_ENTIDAD_CONVENIO")
					lclsWorksheet.Cells._Default(lintRow, 25) = .FieldToClass("DV_ENTIDAD")
					lclsWorksheet.Cells._Default(lintRow, 26) = .FieldToClass("DEFFECDATE")
					lclsWorksheet.Cells._Default(lintRow, 27) = .FieldToClass("DEXPIRDAT")
					
					
					lintRow = lintRow + 1
					.RNext()
				Loop 
				.RCloseRec()
			Else
				insPostCAL803 = False
			End If
		End With
		
		With lclsWorksheet
			For lintRow = 1 To 25
				.Cells._Default(5, lintRow).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
				.Cells._Default(5, lintRow).Font.Bold = True
				.Columns._Default(lintRow).EntireColumn.AutoFit()
			Next 
		End With
		
		'--------------------------------------HOJA 2----------------------------------------------------------------------------------------------------
		lclsWorksheet = lclsExcelApp.Workbooks(1).Sheets(2)
		
		With lrecinsPostCAL803
			.StoredProcedure = "REA_CAL803B"
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				
				insPostCAL803 = True
				
				lclsWorksheet.Cells._Default(5, 1) = "Vía de pago"
				lclsWorksheet.Cells._Default(5, 2) = "Código de convenio"
				lclsWorksheet.Cells._Default(5, 3) = "Nombre del convenio"
				lclsWorksheet.Cells._Default(5, 4) = "Cantidad de pólizas"
				lclsWorksheet.Cells._Default(5, 5) = "Monto bruto en UF"
				lclsWorksheet.Cells._Default(5, 6) = "Monto bruto en pesos"
				
				lintRow = 6
				
				Do While Not .EOF
					
					lclsWorksheet.Cells._Default(lintRow, 1) = .FieldToClass("NWAY_PAY") & " " & .FieldToClass("DESCWAYPAY")
					lclsWorksheet.Cells._Default(lintRow, 2) = .FieldToClass("CODIGO_CONVENIOPACPAT", String.Empty)
					lclsWorksheet.Cells._Default(lintRow, 3) = .FieldToClass("NOMBRE_CONVENIOPACPAT", String.Empty)
					lclsWorksheet.Cells._Default(lintRow, 4) = .FieldToClass("NROPOLIZAS")
					lclsWorksheet.Cells._Default(lintRow, 5) = .FieldToClass("MONTO_UF")
					lclsWorksheet.Cells._Default(lintRow, 6) = .FieldToClass("MONTO_PESOS")
					
					lintRow = lintRow + 1
					.RNext()
				Loop 
				.RCloseRec()
			Else
				insPostCAL803 = False
			End If
		End With
		
		With lclsWorksheet
			For lintRow = 1 To 6
				.Cells._Default(5, lintRow).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
				.Cells._Default(5, lintRow).Font.Bold = True
				.Columns._Default(lintRow).EntireColumn.AutoFit()
			Next 
		End With
		
		
		With lclsExcelApp
			.ActiveWorkbook.SaveAs(lstrFileName)
			
			.ActiveWorkbook.Close()
			.Quit()
		End With
		
		'UPGRADE_NOTE: Object lclsExcelApp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExcelApp = Nothing
		
		'UPGRADE_NOTE: Object lrecinsPostCAL803 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL803 = Nothing
	End Function
	
	
	'%InsValCA037DB: Llamado del procedure de la validación de los campos a actualizar en la
	'                ventana CA037
	Public Function InsValCAl01510(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dBegindate As Date, ByVal dEndDate As Date, ByVal nUsercode As Integer) As String
		Dim lrecInsValCAL01510 As eRemoteDB.Execute
		Dim lclsErrors As eFunctions.Errors
        Dim sError As String = String.Empty
        On Error GoTo ValCAL01510_err
        lrecInsValCAL01510 = New eRemoteDB.Execute

        With lrecInsValCAL01510
            .StoredProcedure = "InsValCAL01510"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBegindate", dBegindate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEnddate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                sError = .Parameters("Arrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors
        With lclsErrors
            If sError.Length > 0 Then
                .ErrorMessage(sCodispl, , , , , , sError)
            End If
            InsValCAl01510 = .Confirm
        End With

ValCAL01510_err:
        If Err.Number Then
            InsValCAl01510 = Err.Description
        End If
        lrecInsValCAL01510 = Nothing
        lclsErrors = Nothing
    End Function


    Public Function InsExistsCal0150X(ByVal sCodispl As String, ByVal nProduct As Integer) As Boolean
        Dim lrecCal0150X As eRemoteDB.Execute

        On Error GoTo InsExistsCal0150X_Err

        InsExistsCal0150X = False

        lrecCal0150X = New eRemoteDB.Execute

        With lrecCal0150X
            .StoredProcedure = "INSEXISTSCAL0150X"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then

                If .Parameters.Item("nExists").Value = 1 Then
                    InsExistsCal0150X = True
                End If
            End If
        End With

        lrecCal0150X = Nothing

InsExistsCal0150X_Err:
        If Err.Number Then
            InsExistsCal0150X = False
        End If
        On Error GoTo 0
    End Function


    '**% insValCAL08001: This function makes the validations of the CAL08001 - "" transaction.
    '%insValCAL08001: Esta función realiza las validaciones de la transacciòn CAL08001 - "Detalle de póliza de vida".
    Public Function insValCAL08001(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nUsercode As Integer) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy

        On Error GoTo insValCAL08001_Err

        lobjErrors = New eFunctions.Errors
        insValCAL08001 = String.Empty

        '+ Si el tipo de ejecucion es "Puntual" se realizan las validaciones de los campos
        '+ Póliza y certificado.

        '+ El ramo debe estar lleno
        If (nBranch = eRemoteDB.Constants.intNull Or nBranch = 0) Then
            Call lobjErrors.ErrorMessage(sCodispl, 11135)
        Else

            '+ El producto debe estar lleno
            If (nProduct = eRemoteDB.Constants.intNull Or nProduct = 0) Then
                Call lobjErrors.ErrorMessage(sCodispl, 1014)
            Else

                '+ si la poliza tiene valor debe existir en el sistema
                If (nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0) Then

                    lclsPolicy = New ePolicy.Policy

                    If Not lclsPolicy.ValExistPolicyRec(nBranch, nProduct, nPolicy, "1") Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3001)
                    End If

                    If Not insValpolicyintermedia("2", nBranch, nProduct, nPolicy, nUsercode) Then
                        lobjErrors.ErrorMessage(sCodispl, 1102, , eFunctions.Errors.TextAlign.RigthAling, "Esta cotización,propuesta,poliza pertenece a otro intermediario")
                    End If

                    'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsPolicy = Nothing
                End If

            End If

            If (nPolicy = eRemoteDB.Constants.intNull Or nPolicy = 0) Then
                Call lobjErrors.ErrorMessage(sCodispl, 21033)
            End If

        End If

        insValCAL08001 = lobjErrors.Confirm

insValCAL08001_Err:
        If Err.Number Then
            insValCAL08001 = "insValCAL08001: " & Err.Description
        End If
        On Error GoTo 0
        lobjErrors = Nothing
        lclsPolicy = Nothing
    End Function

    '%insValCAL979: Esta función realiza las validaciones de la transacciòn CAL979 - "Actualización automática de capitales crecientes/decrecientes".
    Public Function insValCAL979(ByVal sCodispl As String, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nModulec As Integer) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsctrol_date As eGeneral.Ctrol_date

        On Error GoTo insValCAL979_Err

        lobjErrors = New eFunctions.Errors
        lclsctrol_date = New eGeneral.Ctrol_date
        insValCAL979 = String.Empty

        '+ Se valida el campo fecha de efecto
        With lobjErrors
            If dEffecdate = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 4003)
            Else
                If lclsctrol_date.Find(104) Then
                    If dEffecdate <= lclsctrol_date.dEffecdate Then
                        .ErrorMessage(sCodispl, 9122)
                    End If
                End If
            End If
        End With

        insValCAL979 = lobjErrors.Confirm

insValCAL979_Err:
        If Err.Number Then
            insValCAL979 = "insValCAL979: " & Err.Description
        End If
        On Error GoTo 0
        lobjErrors = Nothing
    End Function

    '% insPostCAL979: Ejecuta el SP de actualización automática de capitales crecientes decrecientes
    Public Function insPostCAL979(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nModulec As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Object) As Boolean

        Dim lrec_insPostCAL979 As eRemoteDB.Execute

        On Error GoTo insPostCAL979_Err

        lrec_insPostCAL979 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'INSUPDCAPITAL_CRE_DEC'

        With lrec_insPostCAL979
            .StoredProcedure = "INSUPDCAPITAL_CRE_DEC"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostCAL979 = True
            End If
        End With

insPostCAL979_Err:
        If Err.Number Then
            insPostCAL979 = False
        End If
        lrec_insPostCAL979 = Nothing
        On Error GoTo 0

    End Function


    Public Function InsValCAl01511(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dBegindate As Date, ByVal dEndDate As Date, ByVal nUsercode As Integer) As String
        Dim lrecInsValCAl01511 As eRemoteDB.Execute
        Dim lclsErrors As eFunctions.Errors
        Dim sError As String = String.Empty
        On Error GoTo ValCAl01511_err
        lrecInsValCAl01511 = New eRemoteDB.Execute

        With lrecInsValCAl01511
            .StoredProcedure = "INSCAL01511PKG.InsValCAl01511"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBegindate", dBegindate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEnddate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                sError = .Parameters("Arrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors
        With lclsErrors
            If sError.Length > 0 Then
                .ErrorMessage(sCodispl, , , , , , sError)
            End If
            InsValCAl01511 = .Confirm
        End With

ValCAl01511_err:
        If Err.Number Then
            InsValCAl01511 = Err.Description
        End If
        lrecInsValCAl01511 = Nothing
        lclsErrors = Nothing
    End Function

    '%Funcion insValpolicyintermedia. Esta funcion se encarga de verificar si una poliza esta relacionado con un
    'intermediario
    Public Function insValpolicyintermedia(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nUsercode As Integer) As Boolean
        Dim lrecquedatpolint As eRemoteDB.Execute
        lrecquedatpolint = New eRemoteDB.Execute

        With lrecquedatpolint
            .StoredProcedure = "QUEDATPOLINT"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insValpolicyintermedia = (.Parameters("nExist").Value = 1)
            End If
        End With
        lrecquedatpolint = Nothing
    End Function

    Public Function InsValCAl01517(ByVal sCodispl As String, _
                              ByVal sCertype As String, _
                              ByVal nBranch As Long, _
                              ByVal nProduct As Long, _
                              ByVal nUsercode As Long) As String
        '--------------------------------------------------------------------------------
        Dim lrecInsValCAl01517 As eRemoteDB.Execute
        Dim lclsErrors As eFunctions.Errors
        Dim sError As String = String.Empty
        On Error GoTo ValCAl01517_err
        lrecInsValCAl01517 = New eRemoteDB.Execute

        With lrecInsValCAl01517
            .StoredProcedure = "INSCAL01517PKG.InsValCAl01517"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                sError = .Parameters("Arrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors
        With lclsErrors
            If sError.Length > 0 Then
                .ErrorMessage(sCodispl, , , , , , sError)
            End If
            InsValCAl01517 = .Confirm
        End With

ValCAl01517_err:
        If Err.Number Then
            InsValCAl01517 = Err.Description
        End If
        lrecInsValCAl01517 = Nothing
        lclsErrors = Nothing
    End Function

    '% insValCAL665: Carga de Fidens
    Public Function insValCAL665(ByVal sCodispl As String,
                                 ByVal sFileNameProp As String,
                                 ByVal sFileNameRoles As String,
                                 ByVal sFileNameBenef As String) As String
        Dim lobjErrors As eFunctions.Errors
        lobjErrors = New eFunctions.Errors
        On Error GoTo insValPolicy_Err

        '+ Se valida el archivo de propuestas
        If sFileNameProp = vbNullString Then
            Call lobjErrors.ErrorMessage(sCodispl, 99045)
        Else
            If Not sFileNameProp.ToUpper.Contains(".TXT") And Not sFileNameProp.ToUpper.Contains(".DAT") Then
                Call lobjErrors.ErrorMessage(sCodispl, 90000024, , eFunctions.Errors.TextAlign.LeftAling, "Archivo de propuestas: ")
            End If
        End If

        '+ Se valida el archivo de roles
        If sFileNameRoles = vbNullString Then
            Call lobjErrors.ErrorMessage(sCodispl, 99046)
        Else
            If Not sFileNameRoles.ToUpper.Contains(".TXT") And Not sFileNameRoles.ToUpper.Contains(".DAT") Then
                Call lobjErrors.ErrorMessage(sCodispl, 90000024, , eFunctions.Errors.TextAlign.LeftAling, "Archivo de roles: ")
            End If
        End If

        '+ Se valida el archivo de beneficiarios
        If sFileNameBenef = vbNullString Then
            Call lobjErrors.ErrorMessage(sCodispl, 90000025)
        Else
            If Not sFileNameBenef.ToUpper.Contains(".TXT") And Not sFileNameBenef.ToUpper.Contains(".DAT") Then
                Call lobjErrors.ErrorMessage(sCodispl, 90000024, , eFunctions.Errors.TextAlign.LeftAling, "Archivo de beneficiarios: ")
            End If
        End If


        insValCAL665 = lobjErrors.Confirm

        lobjErrors = Nothing

insValPolicy_Err:
        If Err.Number Then
            insValCAL665 = insValCAL665 & Err.Description
        End If
        On Error GoTo 0
    End Function

    '% insPostCAL665: Ejecuta el SP de actualización automática de capitales crecientes decrecientes
    Public Function insPostCAL665(ByVal sCodispl As String,
                                  ByVal nUsercode As Integer,
                                  ByVal sKey As String,
                                  ByVal sFileNameProp As String,
                                  ByVal sFileNameRoles As String,
                                  ByVal sFileNameBenef As String) As Boolean

        Dim lrec_insPostCAL665 As eRemoteDB.Execute

        On Error GoTo insPostCAL665_Err

        lrec_insPostCAL665 = New eRemoteDB.Execute

        ' Create an instance of StreamReader to read from a file.
        Dim srProp As StreamReader = New StreamReader(sFileNameProp)
        Dim lineProp As String
        ' Read and display the lines from the file until the end of the file is reached.
        Do
            lineProp = srProp.ReadLine()
            If Not String.IsNullOrEmpty(lineProp) then
                With lrec_insPostCAL665
                    .StoredProcedure = "CRET_FIDENS"
                    .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sSource", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sLine", lineProp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1024, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    If .Run(False) Then
                        insPostCAL665 = True
                    End If
                End With
            End If
        Loop Until lineProp Is Nothing


        ' Create an instance of StreamReader to read from a file.
        Dim srRoles As StreamReader = New StreamReader(sFileNameRoles)
        Dim lineRoles As String
        ' Read and display the lines from the file until the end of the file is reached.
        Do
            lineRoles = srRoles.ReadLine()
            
            If Not String.IsNullOrEmpty(lineRoles) Then
                With lrec_insPostCAL665
                    .StoredProcedure = "CRET_FIDENS"
                    .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sSource", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sLine", lineRoles, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1024, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    If .Run(False) Then
                        insPostCAL665 = True
                    End If
                End With
            End If

        Loop Until lineRoles Is Nothing

        ' Create an instance of StreamReader to read from a file.
        Dim srBenef As StreamReader = New StreamReader(sFileNameBenef)
        Dim lineBenef As String
        ' Read and display the lines from the file until the end of the file is reached.
        Do
            lineBenef = srBenef.ReadLine()

            If Not String.IsNullOrEmpty(lineBenef) then
                With lrec_insPostCAL665
                    .StoredProcedure = "CRET_FIDENS"
                    .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sSource", "3", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sLine", lineBenef, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1024, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    If .Run(False) Then
                        insPostCAL665 = True
                    End If
                End With
            End If

        Loop Until lineBenef Is Nothing

insPostCAL665_Err:
        If Err.Number Then
            insPostCAL665 = False
        End If
        lrec_insPostCAL665 = Nothing
        On Error GoTo 0

    End Function


    '% insValCAL0110_k: Se validan los campos que se ingresan para ver sus reportes
    Public Function insValCAL0110_k(ByVal sCodispl As String, ByVal nType As Integer, ByVal nTypeReport As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dIssuedatIn As Date, ByVal dIssuedatEnd As Date, ByVal nPolicy As Integer, ByVal nCertif As Integer) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy

        On Error GoTo insValCAL0110_k_Err

        lobjErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy

        With lobjErrors
            'Generación de validaciones dependiendo de reporte que se ejecuta
            If nTypeReport = 1 Then '+ Cuadro de pólizas
                If nType = 1 Then '+Puntuales
                    If nBranch = eRemoteDB.Constants.intNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 11135)
                    ElseIf nProduct = eRemoteDB.Constants.intNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 11009)
                    ElseIf nPolicy = eRemoteDB.Constants.intNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3003)
                    End If

                    'Validacion para saber si la poliza esta en algun estado valido.
                    With lclsPolicy
                        '+ Si la póliza no existe
                        If Not .Find(2, CInt(nBranch), CInt(nProduct), nPolicy) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3917)
                        Else
                            '+ Si está anulada
                            If .nNullcode <> 0 And .nNullcode <> eRemoteDB.Constants.intNull And .sStatus_pol = "6" And .dNulldate <> eRemoteDB.Constants.dtmNull Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3098)
                            End If
                            '+ Si no es válida
                            If .sStatus_pol <> "1" And .sStatus_pol <> "3" And .sStatus_pol <> "4" And .sStatus_pol <> "5" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3882)
                            End If
                            sPolitype = .sPolitype
                        End If
                    End With
                Else '+ Masivos
                    ''Para Cliente Corpvida, Se pide que el Ramo sea Opcional
                    'If nBranch = eRemoteDB.Constants.intNull Then
                    '    Call lobjErrors.ErrorMessage(sCodispl, 11135)
                    'End If
                    ''Para Cliente Corpvida, Se pide que el Producto sea Opcional
                    'If nProduct = eRemoteDB.Constants.intNull Then
                    '    Call lobjErrors.ErrorMessage(sCodispl, 11009)
                    'End If
                    If dIssuedatIn = eRemoteDB.Constants.dtmNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 60217)
                    End If
                    If dIssuedatEnd = eRemoteDB.Constants.dtmNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 60218)
                    End If
                    If dIssuedatEnd < dIssuedatIn Then
                        Call lobjErrors.ErrorMessage(sCodispl, 55006)
                    End If
                End If
            ElseIf nTypeReport = 3 Then '+Certificado de Cobertura
                If nType = 1 Then '+Puntuales
                    If nBranch = eRemoteDB.Constants.intNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 11135)
                    End If
                    If nProduct = eRemoteDB.Constants.intNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 11009)
                    End If
                    If nPolicy = eRemoteDB.Constants.intNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3003)
                    End If
                    If nCertif = eRemoteDB.Constants.intNull Or nCertif = 0 Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3006)
                    End If
                Else '+Masivos
                    ''Para Cliente Corpvida, Se pide que el Ramo sea Opcional
                    'If nBranch = eRemoteDB.Constants.intNull Then
                    '    Call lobjErrors.ErrorMessage(sCodispl, 11135)
                    'End If
                    ''Para Cliente Corpvida, Se pide que el Producto sea Opcional
                    'If nProduct = eRemoteDB.Constants.intNull Then
                    '    Call lobjErrors.ErrorMessage(sCodispl, 11009)
                    'End If
                    If dIssuedatIn = eRemoteDB.Constants.dtmNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 60217)
                    End If
                    If dIssuedatEnd = eRemoteDB.Constants.dtmNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 60218)
                    End If
                    If dIssuedatEnd < dIssuedatIn Then
                        Call lobjErrors.ErrorMessage(sCodispl, 55006)
                    End If
                End If
            ElseIf nTypeReport = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 100102)
            End If
        End With

        insValCAL0110_k = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

insValCAL0110_k_Err:
        If Err.Number Then
            insValCAL0110_k = insValCAL0110_k & Err.Description
        End If
        On Error GoTo 0

    End Function


    '% InsCreTMP_CAL504: Crea los registros de producción en la tabla TMP_CAL504, para luego mostrar el LT de producción SOAP-AS400.
    Public Function InsCreTMP_CAL504(ByVal p_cod_cia As Integer, ByVal p_area_seguro As Integer, ByVal p_fecha_desde As Date, ByVal p_fecha_hasta As Date, ByVal nUsercode As Integer, ByVal nOption As Integer) As Boolean

        '  ByVal nUsercode As Date   se comenta
        Dim lclsTmp_CAL504 As eRemoteDB.Execute
        Dim sKey As String


        On Error GoTo Add_err
        lclsTmp_CAL504 = New eRemoteDB.Execute

        '**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
        '+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..

        '+ Se comenta el parametro de entrada nUsercode
        With lclsTmp_CAL504
            .StoredProcedure = "CRETMP_CAL504_SOAP"
            .Parameters.Add("p_cod_cia", p_cod_cia, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("p_area_seguro", p_area_seguro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("p_fecha_desde", p_fecha_desde, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("p_fecha_hasta", p_fecha_hasta, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExecute", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("p_sKey", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                P_SKEY = .Parameters.Item("p_sKey").Value
                InsCreTMP_CAL504 = True
            Else
                InsCreTMP_CAL504 = False
            End If

        End With

Add_err:
        If Err.Number Then
            InsCreTMP_CAL504 = False
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lclsTmp_CAL504 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTmp_CAL504 = Nothing
    End Function

  '% Validación de los campos de la ventana del reporte VIL1486C
    '---------------------------------------------------------------------------------------
    Public Function insValVIL1488(ByVal sCodisp As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dInitialDate As Date, ByVal dFinalDate As Date, ByVal sCodispl As String) As String
        '---------------------------------------------------------------------------------------

        Dim lrecinsValVIL1488 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String

        On Error GoTo insValVIL1488_Err
        lrecinsValVIL1488 = New eRemoteDB.Execute
        With lrecinsValVIL1488
            .StoredProcedure = "REAPOLICY_CUIPKG.insValVIL1488"
            .Parameters.Add("sCodisp", sCodisp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dInitialDate", dInitialDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dFinalDate", dFinalDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)

            lstrError = .Parameters("Arrayerrors").Value

            If lstrError <> vbNullString Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage(sCodispl, , , , , , lstrError)
                    insValVIL1488 = lobjErrors.Confirm
                End With
                'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjErrors = Nothing
            End If

        End With
insValVIL1488_Err:
        If Err.Number Then
            insValVIL1488 = "insValVIL1488: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecinsValVIL1488 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValVIL1488 = Nothing
        On Error GoTo 0
    End Function
  Public Function insPostVIL1488(ByVal NCARTPOL As Double, ByVal sKey As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dInitial_date As Date, ByVal dFinal_date As Date, ByVal nUsercode As Integer, ByVal sIndPdf As String, ByVal sCodispl As String) As Boolean
        '----------------------------------------------------------------------------------------
        Dim sDirectory As String
        Dim lrecreavil1488 As eRemoteDB.Execute
        Dim lstrKey As String
        ' Dim lclsGetsettings As Object
        Dim lclsGetsettings As New eRemoteDB.VisualTimeConfig
        'lclsGetsettings = CreateObject("eCrystalrexport.VisualTimeConfig")


        On Error GoTo reavil1488_Err

        lrecreavil1488 = New eRemoteDB.Execute

        'UPGRADE_WARNING: Couldn't resolve default property of object lclsGetsettings.LoadSetting. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        sDirectory = lclsGetsettings.LoadSetting("ExportDirectoryReport", "/Reports/", "Paths")

        '+ Definición de parámetros para stored procedure 'reavil1486'
        '+ Información leída el: 16/09/2003

        With lrecreavil1488
            .StoredProcedure = "REAPOLICY_CUIPKG.Generate_Cartol"

            .Parameters.Add("nNumCart", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("sKey", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDate", dInitial_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEndDate", dFinal_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ''SPROJECTVUL no existe o falta parametro
            .Parameters.Add("SPROJECTVUL", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDirectory", sDirectory, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                'UPGRADE_WARNING: Couldn't resolve default property of object lrecreavil1488.Parameters().Value. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                nNumCart = .Parameters("nNumCart").Value
                'UPGRADE_WARNING: Couldn't resolve default property of object lrecreavil1488.Parameters().Value. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Me.sKey = .Parameters("sKey").Value
                insPostVIL1488 = True
            End If
        End With

reavil1488_Err:
        If Err.Number Then
            insPostVIL1488 = False
        End If

        'UPGRADE_NOTE: Object lrecreavil1488 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreavil1488 = Nothing

        On Error GoTo 0

    End Function


    '% InsPostCAL7933_File: Mueve el archivo desde SII al Servidor BD
    Public Function InsPostCAL7933_File(ByVal sFileName As String, ByVal sFileContent As String, ByVal nIntertype As Integer, ByVal nFormat As Integer) As Boolean

        Dim lrecInsPostCAL7933_File As eRemoteDB.Execute
        Dim i As Integer
        Dim lintline As Integer
        Dim sLine As String
        Dim sLineTot As String
        Dim sNewFile As String
        Dim ALines() As String


        '+ Solo admite archivos para interfaces de Entrada
        If nIntertype = 1 Then
            lrecInsPostCAL7933_File = New eRemoteDB.Execute

            ALines = sFileContent.Split(Chr(10))

            lintline = 0
            i = 1
            sNewFile = "S"
            sLineTot = ""

            For lintline = 0 To ALines.Length - 1

                If i > 1 Then
                    sNewFile = "N"
                End If
                sLine = ALines(lintline)
                If String.IsNullOrEmpty(sLine) And lintline = ALines.Length - 1 Then
                    Exit For
                End If

                If (Len(sLine & sLineTot) + 1) <= 1000 Then
                    sLineTot = sLineTot & sLine & Chr(10)
                Else
                    'llamo a la rutina que inserta en el servidor de BD

                    With lrecInsPostCAL7933_File
                        .StoredProcedure = "CREFILE2"
                        .Parameters.Add("sName_File", sFileName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sLine", sLineTot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sNewFile", sNewFile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        InsPostCAL7933_File = .Run(False)
                    End With
                    i = i + 1
                    sLineTot = sLine & Chr(10)
                    sLine = ""
                End If
            Next
            If Len(sLineTot) <> 0 Then

                With lrecInsPostCAL7933_File
                    .StoredProcedure = "CREFILE2"
                    .Parameters.Add("sName_File", sFIleName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sLine", sLineTot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sNewFile", sNewFile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    InsPostCAL7933_File = .Run(False)

                End With
            End If
            InsPostCAL7933_File = True
        Else
            InsPostCAL7933_File = True
        End If
    End Function


    '%insTransformationExcel(). Transforma el archivo excel para utilizar solo los valores y deshabilita
    ' las formulas
    Public Function insTransformationExcel(ByVal sFile As String) As Boolean
        Dim lclsValue As eFunctions.Values
        Dim mvarSalidaExcel As Microsoft.Office.Interop.Excel.Application
        Dim lstrFileName As String
        Dim lintExist As Integer
        Dim lstrFile As String
        Dim lintlength As Integer

        On Error GoTo insTransformationExcel_Err

        lintExist = InStr(1, UCase(sFile), ".XLS")

        If lintExist > 0 Then
            lstrFile = Mid(sFile, 1, lintExist - 1)
        Else
            lstrFile = sFile
        End If

        lclsValue = New eFunctions.Values
        On Error Resume Next


        lstrFileName = UCase(lclsValue.insGetSetting("LoadFile", String.Empty, "PATHS"))

        If lstrFileName = String.Empty Then
            lstrFileName = UCase(lclsValue.insGetSetting("LoadFile", String.Empty, "Config"))
        End If

        lclsValue = Nothing

        On Error GoTo insTransformationExcel_Err

        lintlength = Len(lstrFileName)

        If Mid(lstrFileName, lintlength, 1) <> "\" Then
            lstrFileName = lstrFileName & "\"
        End If

        mvarSalidaExcel = New Microsoft.Office.Interop.Excel.Application
        mvarSalidaExcel.DisplayAlerts = True

        mvarSalidaExcel.Workbooks.Open(lstrFileName & Trim(lstrFile) & ".XLS", 0, True)

        '+Se guarda el archivo como texto separador por tabuladores
        mvarSalidaExcel.ActiveWorkbook.SaveAs(lstrFileName & Trim(lstrFile) & ".TXT", Microsoft.Office.Interop.Excel.XlPivotFieldDataType.xlText, False)

        mstrFile = Trim(lstrFile) & ".TXT"

        insTransformationExcel = True

insTransformationExcel_Err:
        If Err.Number Then
            insTransformationExcel = False

        End If
        mvarSalidaExcel = Nothing
        On Error GoTo 0
    End Function



    '% InsPostCAL7933: Convierte excel a text sin depender de la libería de Office 
    Private Function insNewTransformationExcel(sExcelName As String) As String
        Dim hssfwb As HSSFWorkbook
        Dim sheet As ISheet


        Try
            Using file As New FileStream(sExcelName, FileMode.Open, FileAccess.Read)

                hssfwb = New HSSFWorkbook(file)
                sheet = hssfwb.GetSheet(hssfwb.GetSheetName(0))
                Dim extractor As New ExcelExtractor(hssfwb)

                extractor.FormulasNotResults = True
                extractor.IncludeSheetNames = False
                Return (extractor.Text)

            End Using
        Catch ex As IOException
            Return String.Empty
        Catch ex As NPOI.POIFS.FileSystem.OfficeXmlFileException

            Return String.Empty
        End Try
    End Function
    '% InsPostCAL7933: Ejecuta el post de la transacción
    Public Function InsPostCAL7933(ByVal nIntertype As Integer, ByVal sFileName As String, ByVal nFormat As Integer) As Boolean
        Dim sFileContent As String

        sFileContent = insNewTransformationExcel(sFileName)

        If Not String.IsNullOrEmpty(sFileContent) Then
            Me.mstrFile = Path.GetFileNameWithoutExtension(sFileName) & ".txt"

            If InsPostCAL7933_File(mstrFile, sFileContent, nIntertype, nFormat) Then

                InsPostCAL7933 = True

            Else
                InsPostCAL7933 = False

            End If
        End If

    End Function



    '% InsValCAL1200:
    Public Function insValCAL1200(ByVal sCodispl As String, ByVal nBranch As Integer) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy

        On Error GoTo insValPolicy_Err
        lclsPolicy = New ePolicy.Policy
        lobjErrors = New eFunctions.Errors

        insValCAL1200 = String.Empty

        '+ El ramo debe estar lleno
        If nBranch = 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 11135)
        End If

        insValCAL1200 = lobjErrors.Confirm
insValPolicy_Err:
        If Err.Number Then
            insValCAL1200 = "insValCAL1200: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        On Error GoTo 0
    End Function


End Class