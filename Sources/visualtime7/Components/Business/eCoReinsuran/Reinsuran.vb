Option Strict Off
Option Explicit On
Public Class Reinsuran
	'%-------------------------------------------------------%'
	'% $Workfile:: Reinsuran.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.35                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the system table on 05/25/2001
	'+ Propiedades según la tabla en el sistema el 25/05/2001
	
	'   Column_name                    Type              Computed  Length  Prec  Scale  Nullable  TrimTrailingBlanks  FixedLenNullInSource
	Public sCertype As String 'char            no        1                 no               no                   no
	Public nBranch As Integer 'smallint        no        2     5     0     no              (n/a)                (n/a)
	Public nProduct As Integer 'smallint        no        2     5     0     no              (n/a)                (n/a)
	Public nPolicy As Double 'int             no        4     10    0     no              (n/a)                (n/a)
	Public nCertif As Double 'int             no        4     10    0     no              (n/a)                (n/a)
	Public nBranch_rei As Integer 'smallint        no        2     5     0     no              (n/a)                (n/a)
	Public nType As Integer 'smallint        no        2     5     0     no              (n/a)                (n/a)
	Public dEffecdate As Date 'datetime        no        8                 no              (n/a)                (n/a)
	Public nCompany As Integer 'smallint        no        2     5     0     no              (n/a)                (n/a)
	Public dAccedate As Date 'datetime        no        8                 yes             (n/a)                (n/a)
	Public nCapital As Double 'decimal         no        9     12    0     yes             (n/a)                (n/a)
	Public nCommissi As Double 'decimal         no        5     4     2     yes             (n/a)                (n/a)
	Public dCompdate As Date 'datetime        no        8                 yes             (n/a)                (n/a)
	Public nCurrency As Integer 'smallint        no        2     5     0     yes             (n/a)                (n/a)
	Public sHeap_code As String 'char            no        14                yes              no                   yes
	Public nInter_rate As Double 'decimal         no        5     4     2     yes             (n/a)                (n/a)
	Public dNulldate As Date 'datetime        no        8                 yes             (n/a)                (n/a)
	Public nNumber As Integer 'smallint        no        2     5     0     yes             (n/a)                (n/a)
	Public nReser_rate As Double 'decimal         no        5     4     2     yes             (n/a)                (n/a)
	Public nShare As Double 'decimal         no        5     9     6     yes             (n/a)                (n/a)
    Public nUsercode As Integer 'smallint        no        2     5     0     yes             (n/a)                (n/a)
    Public sFileName As String
	
	'**%FindReinsuPolicy: This function verifies if policies have been issued to the reinsurance contract.
	'%FindReinsuPolicy:Esta función permite verificar si ya se han emitido pólizas al contrato de reaseguro.
	Public Function FindReinsuPolicy(ByVal nNumber As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer) As Boolean
		Dim lrecContrmaster As New eRemoteDB.Execute
		
		lrecContrmaster = New eRemoteDB.Execute
		
		On Error GoTo FindReinsuPolicy_Err
		
		'**+ Parameters definition for stored procedure 'insudb.reaReinsuPolicy'
		'**+ Information read on 05/25/2001 04:27:45 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaReinsuPolicy'
		'+ Información leída el 25/05/2001 04:27:45 p.m.
		
		With lrecContrmaster
			
			.StoredProcedure = "reaReinsuPolicy"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindReinsuPolicy = True
			Else
				FindReinsuPolicy = False
			End If
			
		End With
		
FindReinsuPolicy_Err: 
		If Err.Number Then
			FindReinsuPolicy = False
		End If
	End Function
	
	'% InsvalCRL004: Validación de la Transacción de Relación de Cesiones de Primas
    '   ESTA LINEA RECIBE PRAMS POR RRFERECNIA QUE NO SON NECESARIOS
    'Public Function InsValCRL004_K(ByRef sAction As String, ByRef dInitDate As Date, ByRef dEndDate As Date) As String
    '   ESTA LOS RECIBE POR VALOR PARA EVITAR EL ERROR
    Public Function InsValCRL004_K(ByVal sAction As String, ByVal dInitDate As Date, ByVal dEndDate As Date) As String

        Dim lobjErrors As eFunctions.Errors

        On Error GoTo InsValCRL004_K_Err

        lobjErrors = New eFunctions.Errors

        With lobjErrors

            '+ Validación de la fecha de inicio

            If dInitDate = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage("CRL004", 6128)
            End If

            '+ Validación de la fecha final

            If dEndDate = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage("CRL004", 6129)
            End If

            If dInitDate <> eRemoteDB.Constants.dtmNull And dEndDate <> eRemoteDB.Constants.dtmNull Then
                If dInitDate > dEndDate Then
                    .ErrorMessage("CRL004", 6130)
                End If
            End If

            InsValCRL004_K = .Confirm
        End With

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
InsValCRL004_K_Err:
        If Err.Number Then
            InsValCRL004_K = InsValCRL004_K & Err.Description
        End If
        On Error GoTo 0

    End Function

    '%insValCRL005_K: Esta función se encarga de validar los datos introducidos en la zona de
    '%detalle de la forma CRL005.
    Public Function insValCRL005_K(ByVal sCodispl As String, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nCessType As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        lclsErrors = New eFunctions.Errors

        On Error GoTo insValCRL005_K_Err

        '*+Validation of the field Initial Date is performed
        '+Se realiza la validacion del campo Fecha de Inicio
        If dInitDate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 6128)
        End If

        '*+Validation of Final Date is performed
        '+Se valida la fecha final
        If dEndDate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 6129)
        End If

        '*+The Initial Date does not should be bigger than Final Date
        '+Se valida que la fecha inicial no sea mayor que la fecha final
        If Not dInitDate = eRemoteDB.Constants.dtmNull And Not dEndDate = eRemoteDB.Constants.dtmNull Then
            If dInitDate > dEndDate Then
                Call lclsErrors.ErrorMessage(sCodispl, 6130)
            End If
        End If

        '*+Validation of the field Bank Account is performed
        '+Validacion del campo "Cuenta Bancaria"
        If nCessType <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 6058)
        End If

        insValCRL005_K = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

insValCRL005_K_Err:
        If Err.Number Then
            insValCRL005_K = insValCRL005_K & Err.Description
        End If
        On Error GoTo 0
    End Function


    '%insGenFilesCRL001: Crea los archivos del proceso COL502
    Public Function insGenFilesCRL001(ByVal sKey As String) As Boolean
        Dim lrecTime As eRemoteDB.Execute
        Dim lobjGeneral As eGeneral.GeneralFunction
        Dim lobjCompany As eGeneral.Company

        Dim llngRecCounter As Integer
        Dim ljdblAmountTot As Double
        Dim lstrLoadFile As String
        Dim lstrDirFile As String
        Dim lstrCompany As Object

        Dim lstrWritTxt As String
        Dim FileName As String
        Dim FileNameCityDet As String
        Dim FileNum As Integer

        Dim ldblAmountPre As Double
        Dim ldblExchangePre As Double
        Dim ldblAmountmov As Double
        Dim ldblExchangemov As Double

        insGenFilesCRL001 = True

        lrecTime = New eRemoteDB.Execute

        lobjGeneral = New eGeneral.GeneralFunction
        '+ Se busca la ruta en la que se guardará el archivo de texto
        lstrLoadFile = lobjGeneral.GetLoadFile()
        '+ Se busca el directorio virtual del archivo a crear
        Dim lclsValue As eFunctions.Values
        lclsValue = New eFunctions.Values
        lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
        'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValue = Nothing

        With lrecTime
            .StoredProcedure = "REACRL001_FILE"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRej_Exe", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        End With

        If lrecTime.Run() Then
            FileName = lstrLoadFile & "CRL001_Rec" & sKey & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & ".xls"
            FileNum = FreeFile()
            FileOpen(FileNum, FileName, OpenMode.Output)
            PrintLine(FileNum, "")
            lstrWritTxt = " DISTRIBUCION DE CESIONES DE PRIMA " & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Today & Chr(9) & Chr(9) & Chr(9) & Chr(9)
            PrintLine(FileNum, lstrWritTxt)
            PrintLine(FileNum, "")
            PrintLine(FileNum, "")
            lstrWritTxt = "Fecha de Ejecución" & Chr(9) & Today & Chr(9) & "Código del Proceso Generado" & Chr(9) & Chr(9) & sKey & Chr(9)
            PrintLine(FileNum, lstrWritTxt)
            PrintLine(FileNum, "")
            PrintLine(FileNum, "")
            lstrWritTxt = ""
            lstrWritTxt = lstrWritTxt & "Ramo" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Producto" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Poliza" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Certificado" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Cliente" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Nombre" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Recibo" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Fecha_desde" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Fecha_hasta" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Facturacion" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Ramo_reaseguro" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Descripcion" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Tipo" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Tipo_Reaseguro" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Compania" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Capital" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Capital_cedido" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Prima" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Prima_cedida" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Comision" & Chr(9)
            lstrWritTxt = lstrWritTxt & "Fecha_Vig" & Chr(9)
            PrintLine(FileNum, lstrWritTxt)

            Do While Not lrecTime.EOF
                lstrWritTxt = ""
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Ramo") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Producto") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Poliza") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Certificado") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Cliente") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Nombre") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Recibo") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Fecha_desde") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Fecha_hasta") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Facturacion") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Ramo_reaseguro") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Descripcion") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Tipo") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Tipo_Reaseguro") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Compania") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Capital") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Tipo_Reaseguro") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Prima") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Capital_cedido") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Comision") & Chr(9)
                lstrWritTxt = lstrWritTxt & lrecTime.FieldToClass("Fecha_Vig") & Chr(9)
                PrintLine(FileNum, lstrWritTxt)

                lrecTime.RNext()
            Loop
            FileClose(FileNum)
            '+Se retorna el nombre de archivo generado
            If FileName <> String.Empty Then
                Me.sFileName = FileName
            Else
                sFileName = String.Empty
                insGenFilesCRL001 = False
            End If
        Else
            insGenFilesCRL001 = False
        End If

insGenFilesCOL502_Err:
        If Err.Number Then
            insGenFilesCRL001 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecTime = Nothing
        'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjGeneral = Nothing
        'UPGRADE_NOTE: Object lobjCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjCompany = Nothing
    End Function
End Class






