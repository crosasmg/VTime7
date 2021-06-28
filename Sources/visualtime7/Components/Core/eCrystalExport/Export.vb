Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports eRemoteDB
Imports System.IO 

Public Class Export
    Inherits System.Collections.ArrayList
    Private sServer As String
    Private sDatabase As String


    Public sCodispl As String
    Public bPuntual As Boolean
    Public bLog As Boolean
    Public sNameReport As String
    Public ReportFilename As String
    Public sExportedFileName As String
    Public sExportedFilePath As String
    Private lmdlAplication As Object
    Private lmdlReport As Object
    Private mstrLogin As String
    Private mstrPassword As String
    Public nClaim As Long
    Public noptTrans As Long
    Public bMerge As Boolean



    Private sLogin As String
    Private sPassword As String
    Public _sExportedFilePath As String
    Private crystalDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
    Private _dbParameters As System.Collections.ArrayList
    Private _reportParameters As System.Collections.ArrayList
    Private mstrLogoFilename As String
    Public nBranch As Long
    Public nProduct As Long

    Public dCompdate As Date
    Public nId As Integer = 0
    Public sClient As String
    Public sMassive As String
    Public nYear As Integer = 0
    Public nRectif As Integer = 0

    Public nPolicy As Long
    Public nCertif As Long
    Public bMerger As Boolean
    Public sError As String
    Public sException As String
    Public nCopies As Integer
    Public sCopyName As String
    Public nMovement As Integer 'int      no       4      10    0     no       (n/a)              (n/a)
    Public nUsercode As Integer 'smallint no       2      5     0     yes      (n/a)              (n/a)
    Public sReport As String
    Public nForzaRep As Integer = 0
    Public nTratypep As Integer = 0
    Public nGenPolicy As Integer = 0
    Public nCopyPolicy As Integer = 0
    Public nGenReportseven As Integer = 0

    Public sPolitype As String = ""
    Public sReportName As String = ""
    Public sErrorReport As String = ""

    Public sCartol As String = "0"
    Public nCartol As String
    Public sCertype As Long

    Public Parameters As Collection

    Structure AddFolder
        Dim sNewFolder As String
    End Structure
    Enum FolderExport
        Policy = 1
        Claim = 2
        Collection = 3
        Ledger = 4
    End Enum


    Public ReadOnly Property DBParameters() As System.Collections.ArrayList
        Get
            Return _dbParameters
        End Get
    End Property

    Public ReadOnly Property ReportParameters() As System.Collections.ArrayList
        Get
            Return _reportParameters
        End Get
    End Property

    'Public ReadOnly Property sExportedFilePath() As String
    '    Get
    '        Return _sExportedFilePath
    '    End Get
    'End Property.

    '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Public Sub AssignSPParams(ByRef nStartingFrom As Integer)
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim lintIndex As Integer
        Dim lstrValue As String

        For lintIndex = 0 To _dbParameters.Count - 1
            lstrValue = _dbParameters.Item(lintIndex)
            Dim discreteValue As New ParameterDiscreteValue
            Select Case crystalDoc.ParameterFields(nStartingFrom + lintIndex).ParameterValueType
                Case ParameterValueKind.StringParameter
                    crystalDoc.SetParameterValue(nStartingFrom + lintIndex, CStr(_dbParameters(lintIndex)))
                    'discreteValue.Value = CStr(_parameters(lintIndex))
                Case ParameterValueKind.NumberParameter
                    If IsNumeric(_dbParameters(lintIndex)) Then
                        crystalDoc.SetParameterValue(nStartingFrom + lintIndex, Convert.ToInt32(_dbParameters(lintIndex)))
                    Else
                        crystalDoc.SetParameterValue(nStartingFrom + lintIndex, System.DBNull.Value)
                    End If
                    'discreteValue.Value = Convert.ToInt32(_parameters(lintIndex))
                Case ParameterValueKind.CurrencyParameter
                    If IsNumeric(_dbParameters(lintIndex)) Then
                        crystalDoc.SetParameterValue(nStartingFrom + lintIndex, CDbl(_dbParameters(lintIndex)))
                    Else
                        crystalDoc.SetParameterValue(nStartingFrom + lintIndex, System.DBNull.Value)
                    End If

                    ''discreteValue.Value = CDbl(_parameters(lintIndex))
                Case ParameterValueKind.TimeParameter, ParameterValueKind.DateParameter, ParameterValueKind.DateTimeParameter
                    crystalDoc.SetParameterValue(nStartingFrom + lintIndex, New Date(Mid(_dbParameters(lintIndex), 1, 4), Mid(_dbParameters(lintIndex), 5, 2), Mid(_dbParameters(lintIndex), 7, 2)))
                    'discreteValue.Value = CDate(_parameters(lintIndex))
            End Select
        Next lintIndex
        nStartingFrom = nStartingFrom + lintIndex
    End Sub

    Private Function GetValueFromExistingParam(ByVal parameterName As String, ByVal nRoof As Integer) As String
        Dim lintIndex As Integer
        Dim lstrValue As String

        For lintIndex = 0 To nRoof
            If crystalDoc.ParameterFields(lintIndex).Name.ToUpper() = parameterName.ToUpper() Then

                '                lstrValue = crystalDoc.ParameterFields(lintIndex).CurrentValues.Item(0).ToString
                lstrValue = DirectCast(crystalDoc.ParameterFields(lintIndex).CurrentValues.Item(0), CrystalDecisions.Shared.ParameterDiscreteValue).Value.ToString()
                Exit For
            End If
        Next lintIndex
        Return lstrValue
    End Function
    '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Public Sub TryAssigningSubReportParameters(ByRef nStartingFrom As Integer)
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim lintIndex As Integer
        Dim lstrValue As String

        For lintIndex = nStartingFrom To crystalDoc.ParameterFields.Count - 1
            lstrValue = GetValueFromExistingParam(crystalDoc.ParameterFields(lintIndex).Name, nStartingFrom - 1)
            Dim discreteValue As New ParameterDiscreteValue
            Select Case crystalDoc.ParameterFields(lintIndex).ParameterValueType
                Case ParameterValueKind.StringParameter
                    crystalDoc.SetParameterValue(lintIndex, lstrValue)
                    'discreteValue.Value = CStr(_parameters(lintIndex))
                Case ParameterValueKind.NumberParameter
                    crystalDoc.SetParameterValue(lintIndex, Convert.ToInt32(lstrValue))
                    'discreteValue.Value = Convert.ToInt32(_parameters(lintIndex))
                Case ParameterValueKind.CurrencyParameter
                    crystalDoc.SetParameterValue(lintIndex, CDbl(lstrValue))
                    ''discreteValue.Value = CDbl(_parameters(lintIndex))
                Case ParameterValueKind.TimeParameter, ParameterValueKind.DateParameter
                    crystalDoc.SetParameterValue(lintIndex, CDate(lstrValue))
                    'discreteValue.Value = CDate(_parameters(lintIndex))
            End Select
        Next lintIndex
        nStartingFrom = nStartingFrom + lintIndex

        Dim FieldDefinition As ParameterFieldDefinition = Nothing
        Try
            FieldDefinition = crystalDoc.DataDefinition.ParameterFields.Item("LogoFilename")
            If Not IsNothing(FieldDefinition) Then
                crystalDoc.SetParameterValue("LogoFilename", mstrLogoFilename)
            End If
        Catch ex As Exception
            FieldDefinition = Nothing
        End Try


    End Sub


    '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Public Sub AssignRPTParams(ByRef nStartingFrom As Integer)
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim lintIndex As Integer
        Dim lstrValue As String

        For lintIndex = 0 To _reportParameters.Count - 1
            lstrValue = _reportParameters.Item(lintIndex)
            Dim discreteValue As New ParameterDiscreteValue
            Select Case crystalDoc.ParameterFields(nStartingFrom + lintIndex).ParameterValueType
                Case ParameterValueKind.StringParameter
                    crystalDoc.SetParameterValue(nStartingFrom + lintIndex, CStr(_reportParameters(lintIndex)))
                    'discreteValue.Value = CStr(_parameters(lintIndex))
                Case ParameterValueKind.NumberParameter
                    crystalDoc.SetParameterValue(nStartingFrom + lintIndex, Convert.ToInt32(_reportParameters(lintIndex)))
                    'discreteValue.Value = Convert.ToInt32(_parameters(lintIndex))
                Case ParameterValueKind.CurrencyParameter
                    crystalDoc.SetParameterValue(nStartingFrom + lintIndex, CDbl(_reportParameters(lintIndex)))
                    ''discreteValue.Value = CDbl(_parameters(lintIndex))
                Case ParameterValueKind.TimeParameter, ParameterValueKind.DateParameter
                    crystalDoc.SetParameterValue(nStartingFrom + lintIndex, CDate(_reportParameters(lintIndex)))
                    'discreteValue.Value = CDate(_parameters(lintIndex))
            End Select
        Next lintIndex
        nStartingFrom = nStartingFrom + lintIndex

        Dim FieldDefinition As ParameterFieldDefinition = Nothing
        Try
            FieldDefinition = crystalDoc.DataDefinition.ParameterFields.Item("LogoFilename")
            If Not IsNothing(FieldDefinition) Then
                crystalDoc.SetParameterValue("LogoFilename", mstrLogoFilename)
            End If
        Catch ex As Exception
            FieldDefinition = Nothing
        End Try


    End Sub

    '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Public Sub PrepareObject(ByVal strPathReport As String)
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim lintIndex As Integer = 0
        Dim connInfo As New CrystalDecisions.Shared.TableLogOnInfo
        crystalDoc = New CrystalDecisions.CrystalReports.Engine.ReportDocument


        crystalDoc.Load(strPathReport)
        connInfo.ConnectionInfo.UserID = sLogin
        connInfo.ConnectionInfo.Password = sPassword
        connInfo.ConnectionInfo.ServerName = sDatabase
        connInfo.ConnectionInfo.DatabaseName = sDatabase

        'crystalDoc.Database.Tables(0).ApplyLogOnInfo(connInfo)

        crystalDoc =  CrystalReportFixes.OraclePackageFix(crystalDoc, connInfo)

        If crystalDoc.ParameterFields.Count > 0 AndAlso crystalDoc.ParameterFields(0).ReportParameterType = CrystalDecisions.Shared.ParameterType.StoreProcedureParameter Then
            AssignSPParams(lintIndex)
            AssignRPTParams(lintIndex)
        Else
            AssignRPTParams(lintIndex)
            AssignSPParams(lintIndex)
        End If
        'If lintIndex < crystalDoc.ParameterFields.Count - 1 And crystalDoc.Subreports.Count > 0 And False Then
        '    TryAssigningSubReportParameters(lintIndex)
        'End If
    End Sub


    ''DoRealExport: Se realiza la exportacion
    ''----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    Public Function RealExport(ByVal sFullPathReport As String, _
                           ByVal sNameExport As String, _
                           ByVal sFormatExport As String, _
                           Optional ByVal sName As String = "", _
                           Optional ByVal sReptype As String = "", _
                           Optional ByVal strLogin As String = "", _
                           Optional ByVal strPassword As String = "") As Boolean
        Dim lIndex As Integer
        Dim sDigital As String
        Dim lclsGetsettings As New VisualTimeConfig
        Dim sPatharchivoPdf As String
        'Dim sService As New Service
        'sService = New Service

        sServer = lclsGetsettings.LoadSetting("ServerName", "", "Database")
        sDatabase = lclsGetsettings.LoadSetting("DatabaseName", "", "Database")
        mstrLogoFilename = lclsGetsettings.LoadSetting("LogoFilename", , "Paths")

        sLogin = eRemoteDB.CryptSupport.DecryptString(strLogin)
        sPassword = eRemoteDB.CryptSupport.DecryptString(strPassword)

        lIndex = InStr(1, sNameExport, ".rpt", vbTextCompare)


        sDigital = lclsGetsettings.LoadSetting("Digital", "NO", "DigitalSystem")
        'sExportedFileName = CreateGUID()
        sExportedFileName = "" & Guid.NewGuid().ToString() & "-" & sFormatExport & ResolveExtension(sFormatExport)
        sExportedFilePath = lclsGetsettings.LoadSetting("ExportDirectoryReport", "/Reports/", "Paths")
        _sExportedFilePath = lclsGetsettings.LoadSetting("ExportDirectoryReport", "\\Reports\\", "Paths") & "\\" & sExportedFileName
        '_sExportedFilePath = lclsGetsettings.LoadSetting("ExportDirectoryReport", "\\Reports\\", "Paths") & sExportedFileName
        sExportedFilePath = _sExportedFilePath
        ''sNameReport = lclsGetsettings.LoadSetting("ExportDirectoryReportService", "/Reports/", "Paths") & Me.sExportedFileName
        'sNameReport = sNameExport
        sNameReport = _sExportedFilePath
        PrepareObject(sFullPathReport)
        Try
            crystalDoc.ExportToDisk(ResolveFormat(sFormatExport), _sExportedFilePath)

            'crystalDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "c:\josemiguel.pdf")

            'crystalDoc.ExportToDisk(ExportFormatType.HTML32, sExportedFilePath)
        Catch ex As Exception

            Console.WriteLine(ex.Message)
        End Try


        'Código para la limpieza de la memoria
        crystalDoc.Close()
        crystalDoc.Dispose()

        If nGenPolicy = 1 Then
            'copi el reporte en la ruta 
            sPatharchivoPdf = lclsGetsettings.LoadSetting("ExportDirectoryPolicy", "/Reports/", "Paths")
            If nCopyPolicy = 1 Then
                sReport = NameReport(nBranch, nProduct, nPolicy, nCertif, sCertype, nMovement)
                Call File.Copy(_sExportedFilePath, sPatharchivoPdf & sReport, True)
            Else
                Call File.Copy(_sExportedFilePath, sPatharchivoPdf & sReport & ".pdf", True)
            End If
            sExportedFilePath = _sExportedFilePath
            Me.UpdatePolicy_his_sReport()
        End If

        If nGenReportseven = 1 Then
            'copi el reporte en la ruta 
            sPatharchivoPdf = lclsGetsettings.LoadSetting("ExportDirectoryPolicy", "/Reports/", "Paths")
            'If nCopyCertifSeven = 1 Then
            '    sReport = NameReport(nBranch, nProduct, nPolicy, nCertif, sCertype, nMovement)
            '    Call File.Copy(_sExportedFilePath, sPatharchivoPdf & sReport, True)
            'Else
            Call File.Copy(_sExportedFilePath, sPatharchivoPdf & sReport & ".pdf", True)
            'End If
            sExportedFilePath = _sExportedFilePath
            'Me.UpdatePolicy_his_sReport()
        End If

        Return True
    End Function


    '   ResolveExtension
    '---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Private Function ResolveExtension(sFormatExport As String) As String
        '----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Select Case Trim(sFormatExport).ToUpper
            Case "31", "0"
                Return ".pdf"
            Case "PDF"
                Return ".pdf"
            Case "14"
                Return ".doc"
            Case Else
                Return ".xls"
        End Select
    End Function

    ' ResolveFormat
    '----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Private Function ResolveFormat(sFormatExport As String) As Integer
        '----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Select Case Trim(sFormatExport).ToUpper
            Case "31", "0"
                Return ExportFormatType.PortableDocFormat
            Case "PDF"
                Return ExportFormatType.PortableDocFormat
            Case "14"
                Return ExportFormatType.WordForWindows
            Case Else
                Return ExportFormatType.Excel
        End Select
    End Function

    '------------------------------------------------------------------------------------
    Private Function insGetLoginPsw() As String
        '------------------------------------------------------------------------------------
        'Dim objContext As ObjectContext
        Dim objContext As Object
        'Dim objSession As ASPTypeLibrary.Session
        Dim objSession As Object
        On Error Resume Next
        'objContext = GetObjectContext()
        objSession = objContext("Session")


        insGetLoginPsw = "0" & objSession("SessionID")

        If insGetLoginPsw = "0" Or insGetLoginPsw = vbNullString Then
            insGetLoginPsw = "9999"
        End If

        objContext = Nothing
        objSession = Nothing

        On Error GoTo 0
    End Function

    Private Function insMakeURL() As String
        Dim lobjQuery As Object

        insMakeURL = String.Empty
        insMakeURL = "/VTimeNet/reports/" & ReportFilename

        'UPGRADE_NOTE: Object lobjQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjQuery = Nothing
    End Function


    Public Sub LogThis(ByVal msg As String)
        If bLog Then
            'Call CExport.LogThis(msg, Me.bLog)
        End If
    End Sub

    Public Function InvokeRealExport2(ByVal nFormatType As String, _
                                      ByVal nReportType As String, _
                                      ByVal strLogin As String, _
                                      ByVal strPassword As String, _
                                      Optional ByVal bErrModule As Boolean = False,
                                      Optional ByVal sVTRoot As String = "") As Integer
        Dim nCount As Integer
        Dim report1 As String
        Dim report2 As String
        Dim nameRandom As String
        Dim sNameReport As String
        Dim MergePDF As New MergePDF
        Dim exp As New Export
        Dim lreRemote As New eRemoteDB.Execute
        Dim visual As New VisualTimeConfig
        Dim sPath As String
        Dim i As Integer

        Dim sRoutine As String
        Dim bPrint As Boolean
        Dim oMergePDF As MergePDF

        On Error GoTo InvokeRealExport2_Err

        sPath = visual.LoadSetting("ExportDirectoryReport", "/Reports/", "Paths")
        nameRandom = sPath & Guid.NewGuid().ToString() & ".pdf"
        nCount = 0
        InvokeRealExport2 = False
        exp = New Export
        ' Trae la los reporte a generar
        With lreRemote
            .StoredProcedure = "READOC_PRODUCTBBVA"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompon", IIf(nCertif = 0, "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .bErr_Module = bErrModule
            If .Run Then
                For i = 0 To Me.DBParameters.Count - 1
                    exp.DBParameters.Add(DBParameters(i))
                Next
                For i = 0 To Me.ReportParameters.Count - 1
                    exp.ReportParameters.Add(ReportParameters(i))
                Next

                Do While Not .EOF
                    bPrint = True
                    sNameReport = sVTRoot & "\reports\" & .FieldToClass("SREPORT")
                    sRoutine = .FieldToClass("SROUTINE")
                    If sRoutine <> vbNullString Then
                        bPrint = InsValReport(sCertype, nBranch, nProduct, nPolicy, nCertif, sRoutine, .FieldToClass("NSEQUENCE"), .FieldToClass("SREPORT"))
                    End If
                    If bPrint Then
                        If exp.RealExport(sNameReport, "xxx.rpt", nFormatType, "", "", strLogin, strPassword) Then
                            InvokeRealExport2 = True
                            report2 = exp.sExportedFilePath '& exp.sExportedFileName
                            If nCount > 0 Then
                                Call MergePDF.Merge2PDFs(report1, report2, nameRandom)
                                Call File.Delete(report1)
                                Call File.Delete(report2)
                                Call File.Copy(nameRandom, report1)
                                Call File.Delete(nameRandom)
                            Else
                                report1 = report2
                                _sExportedFilePath = exp.sExportedFilePath
                                'sExportedFileName = exp.sExportedFileName
                            End If
                        End If
                    End If
                    nCount = nCount + 1
                    .RNext()
                Loop
                .RCloseRec()
            End If
        End With
InvokeRealExport2_Err:
        If Err.Number > 0 Then
            InvokeRealExport2 = False
            sError = "[InvokeRealExport2]" & Err.Number & "-" & Err.Description
            sErrorReport = Err.Description
        End If

    End Function


    Public Function InvokeRealExport3(ByVal nFormatType As String, _
                                  ByVal nReportType As String, _
                                  ByVal strLogin As String, _
                                  ByVal strPassword As String, _
                                  Optional ByVal bErrModule As Boolean = False) As Integer
        Dim nCount As Integer
        Dim report1 As String
        Dim report2 As String
        Dim nameRandom As String
        Dim sNameReport As String
        Dim MergePDF As New MergePDF
        Dim subRPT As New Export
        Dim lreRemote As New eRemoteDB.Execute
        Dim visual As New VisualTimeConfig
        Dim sPath As String
        Dim i As Integer

        Dim sRoutine As String
        Dim bPrint As Boolean
        Dim oMergePDF As MergePDF

        On Error GoTo InvokeRealExport3_Err
        sPath = visual.LoadSetting("ExportDirectoryReport", "/Reports/", "Paths")
        nameRandom = sPath & Guid.NewGuid().ToString()
        nCount = 0
        InvokeRealExport3 = False
        subRPT = New Export
        Me.sError = ""


        ' Trae la los reporte a generar
        With lreRemote
            .StoredProcedure = "READOC_PRODUCT"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .bErr_Module = bErrModule
            If .Run Then
                ' segun el tipo de reporte es como se hace el paso de parametro
                If .FieldToClass("NREPTYPE") = 1 Then
                    For i = 0 To Me.DBParameters.Count - 1
                        subRPT.DBParameters.Add(DBParameters(i))
                    Next
                    For i = 0 To Me.ReportParameters.Count - 1
                        subRPT.ReportParameters.Add(ReportParameters(i))
                    Next

                Else
                    subRPT.DBParameters.Add(Me.DBParameters.Item(5))
                    subRPT.DBParameters.Add(Me.DBParameters.Item(1))
                    subRPT.DBParameters.Add(Me.DBParameters.Item(2))
                    subRPT.DBParameters.Add(Me.DBParameters.Item(3))
                    subRPT.DBParameters.Add(Me.DBParameters.Item(4))
                    subRPT.DBParameters.Add(Me.DBParameters.Item(7))

                End If

                Do While Not .EOF
                    bPrint = True
                    sNameReport = "reports\" & .FieldToClass("SREPORT")
                    sRoutine = .FieldToClass("SROUTINE")
                    If sRoutine <> vbNullString Then
                        bPrint = InsValReport(sCertype, nBranch, nProduct, nPolicy, nCertif, sRoutine, .FieldToClass("NSEQUENCE"), .FieldToClass("SREPORT"))
                    End If
                    If bPrint Then
                        If subRPT.RealExport(sNameReport, "xxx.rpt", nFormatType, "", "", strLogin, strPassword) Then
                            InvokeRealExport3 = True
                            report2 = subRPT.sExportedFilePath '& subRPT.sExportedFileName
                            If nCount > 0 Then
                                Call MergePDF.Merge2PDFs(report1, report2, nameRandom)
                                Call File.Delete(report1)
                                Call File.Delete(report2)
                                Call File.Copy(nameRandom, report1)
                                Call File.Delete(nameRandom)
                            Else
                                report1 = report2
                                _sExportedFilePath = subRPT.sExportedFilePath
                                'sExportedFileName = Export.sExportedFileName
                            End If
                        Else
                            Me.sError = subRPT.sException
                        End If
                    End If
                    nCount = nCount + 1
                    .RNext()
                Loop
                .RCloseRec()
            End If
        End With

        ' concatenar lso condicionado por poliza
        If bMerger And Me.sError = "" Then
            nameRandom = nameRandom & "-1-CC"
            oMergePDF = New MergePDF
            oMergePDF.bErr_Module = bErrModule
            Call oMergePDF.MergePDFs2(DBParameters.Item(1), _
                                      DBParameters.Item(2), _
                                      DBParameters.Item(3), _
                                      DBParameters.Item(4), _
                                      sExportedFilePath, _
                                      nameRandom, _
                                      1)
            If oMergePDF.sExportedFilePath <> "" Then
                _sExportedFilePath = oMergePDF.sExportedFilePath
            End If
        End If
InvokeRealExport3_Err:
        If Err.Number > 0 Then
            InvokeRealExport3 = False
            sError = "[InvokeRealExport3]" & Err.Number & "-" & Err.Description
            sErrorReport = Err.Description
        End If

    End Function
    '--------------------------------------------------------------------------------------------------------------------------------
    Private Function InsValReport(ByVal sCertype As String, _
                                  ByVal nBranch As Long, _
                                  ByVal nProduct As Long, _
                                  ByVal nPolicy As Double, _
                                  ByVal nCertif As Double, _
                                  ByVal sRoutine As String, _
                                  ByVal nSequence As Long, _
                                  ByVal sReport As String) As Boolean
        '--------------------------------------------------------------------------------------------------------------------------------
        Dim lreRemoteval As New eRemoteDB.Execute
        Dim nExists As Integer

        On Error GoTo InsValReport_Err

        InsValReport = False
        With lreRemoteval
            .StoredProcedure = "InsRoutinereports"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReport", sReport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nExists = .FieldToClass("nExists")
            End If
            If nExists > 0 Then
                InsValReport = True
            End If
        End With

InsValReport_Err:
        If Err.Number > 0 Then
            InsValReport = False
        End If
        On Error GoTo 0
        lreRemoteval = Nothing
    End Function


    Public Function GenPoliza(ByVal nFormatType As String, _
                                      ByVal nReportType As String, _
                                      ByVal strLogin As String, _
                                      ByVal strPassword As String, _
                                      Optional ByVal bErrModule As Boolean = False,
                                      Optional ByVal sVTRoot As String = "") As Integer

        Dim nCount As Integer
        Dim report1 As String = ""
        Dim report2 As String = ""
        Dim nameRandom As String
        Dim sNameReport As String
        Dim MergePDF As New MergePDF
        Dim exp As New Export
        Dim lreRemote As New eRemoteDB.Execute
        Dim visual As New VisualTimeConfig
        Dim sPath As String
        Dim i As Integer
        Dim sCertype_aux As Long

        Dim sRoutine, sPathArchivo, sPatharchivoPdf, sExtencion As String
        Dim bPrint As Boolean
        Dim oMergePDF As MergePDF

        On Error GoTo InvokeRealExport2_Err

        sPath = visual.LoadSetting("ExportDirectoryReport", "/Reports/", "Paths")
        sPathArchivo = visual.LoadSetting("ExportDirectoryPolicy", "/Reports/", "Paths")
        nameRandom = sPath & Guid.NewGuid().ToString() & ".pdf"

        FindPolicy_sfile_report(sCertype, nBranch, nProduct, nPolicy, nCertif, nMovement)
        sPatharchivoPdf = Me.sReport

        ' verifica que el reporte no ete generadp 
        sPatharchivoPdf = sPathArchivo & Me.sReport
        If nCopyPolicy = 0 Then
            If Me.sReport <> "" And File.Exists(sPatharchivoPdf) Then
                _sExportedFilePath = sPatharchivoPdf
                GenPoliza = True
            Else
                Me.sReport = NameReport(nBranch, nProduct, nPolicy, nCertif, sCertype, nMovement)
                sPatharchivoPdf = sPathArchivo & Me.sReport
                nCount = 0
                GenPoliza = False
                exp = New Export
                ' Trae la los reporte a generar

                If nTratypep = 2 Then
                    sCertype_aux = 6
                Else
                    sCertype_aux = sCertype
                End If

                If nTratypep <> -1 Then '* Esta validación se realiza para los cuadros de pólizas cuando sus reportes son distintos de los cuadros de pólizas
                    With lreRemote
                        .StoredProcedure = "INSCAL0110PKG.READOC_PRODUCT"
                        .Parameters.Add("sCertype", sCertype_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sCompon", IIf(nCertif = 0, "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nReptype", nReportType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                        .bErr_Module = bErrModule
                        If .Run Then
                            For i = 0 To Me.DBParameters.Count - 1
                                exp.DBParameters.Add(DBParameters(i))
                            Next
                            For i = 0 To Me.ReportParameters.Count - 1
                                exp.ReportParameters.Add(ReportParameters(i))
                            Next

                            Do While Not .EOF
                                bPrint = True
                                sNameReport = sVTRoot & "\reports\" & .FieldToClass("SREPORT")
                                sRoutine = .FieldToClass("SROUTINE")
                                sReportName = .FieldToClass("SREPORT")
                                If sRoutine <> vbNullString Then
                                    bPrint = InsValReport(sCertype, nBranch, nProduct, nPolicy, nCertif, sRoutine, .FieldToClass("NSEQUENCE"), .FieldToClass("SREPORT"))
                                End If
                                sExtencion = Extraer(.FieldToClass("SREPORT").ToString.ToUpper, ".")
                                If bPrint Then
                                    Select Case sExtencion
                                        Case "RPT"
                                            If exp.RealExport(sNameReport, "xxx.rpt", nFormatType, "", "", strLogin, strPassword) Then
                                                GenPoliza = True
                                                report2 = exp.sExportedFilePath '& exp.sExportedFileName
                                                If nCount > 0 Then
                                                    Call MergePDF.Merge2PDFs(report1, report2, nameRandom)
                                                    Call File.Delete(report1)
                                                    Call File.Delete(report2)
                                                    Call File.Copy(nameRandom, report1)
                                                    Call File.Delete(nameRandom)
                                                Else
                                                    report1 = report2
                                                    _sExportedFilePath = exp.sExportedFilePath
                                                    'sExportedFileName = exp.sExportedFileName
                                                End If
                                            End If
                                        Case "PDF"
                                            Call File.Copy(sNameReport, report2)
                                            Call MergePDF.Merge2PDFs(report1, report2, nameRandom)
                                            Call File.Delete(report1)
                                            Call File.Delete(report2)
                                            Call File.Copy(nameRandom, report1)
                                            Call File.Delete(nameRandom)
                                    End Select
                                End If
                                nCount = nCount + 1
                                .RNext()
                            Loop
                            .RCloseRec()
                        End If
                    End With
                End If

                'hacer que el reporte se guarde en la ruta
                If GenPoliza Then
                    'copia el reporte en la ruta 
                    Call File.Copy(_sExportedFilePath, sPatharchivoPdf, True)
                    Me.UpdatePolicy_his_sReport()
                End If
            End If
        Else
            Me.sReport = NameReport(nBranch, nProduct, nPolicy, nCertif, sCertype, nMovement)
            sPatharchivoPdf = sPathArchivo & Me.sReport
            GenPoliza = True

            Call File.Copy(_sExportedFilePath, sPatharchivoPdf, True)
            Me.UpdatePolicy_his_sReport()
        End If
InvokeRealExport2_Err:
        If (Err.Number > 0) Or (Err.Number < 0) Then
            GenPoliza = False
            sError = "[GenPoliza]" & Err.Number & "-" & Err.Description
            sErrorReport = Err.Description
        End If

    End Function

    '+ Generación del Reporte: Crea el nombre del reporte que se almacena en el servidor
    Private Function NameReport(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sCertype As String, ByVal nMovement As Integer) As String
        sCartol = "0"
        'Nombre: Ramo_Producto_Poliza_Certificado_Movimiento_Dia-Mes-Año_Hora-Minutos-Segundos_Milisegundos.pdf 
        If sCertype = "2" Then
            If sCartol <> "0" Then

                NameReport = "CART_" & nBranch & "_" & nProduct & "_" & nPolicy & "_" & nCertif & "_" & nMovement & "_" & DateTime.Now.ToString("dd-MM-yyyy") & ".pdf"
            Else

                NameReport = "POL_" & nBranch & "_" & nProduct & "_" & nPolicy & "_" & nCertif & "_" & nMovement & "_" & DateTime.Now.ToString("dd-MM-yyyy") & "_" & Guid.NewGuid().ToString().Substring(0, 10) & ".pdf"
            End If
        Else
            If sCertype = "6" Then
                NameReport = "END_" & nBranch & "_" & nProduct & "_" & nPolicy & "_" & nCertif & "_" & nMovement & ".pdf"
            Else
                NameReport = "END_" & nBranch & "_" & nProduct & "_" & nPolicy & "_" & nCertif & "_" & nMovement & "_" & DateTime.Now.ToString("dd-MM-yyyy") & "_" & Guid.NewGuid().ToString().Substring(0, 10) & ".pdf"
            End If
        End If
    End Function

    Public Sub New()
        _dbParameters = New ArrayList
        _reportParameters = New ArrayList
    End Sub


    Public Function FindPolicy_sfile_report(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nMovement As Integer) As Boolean
        Dim lrecreaFindPolicy_His_nNovement As eRemoteDB.Execute

        On Error GoTo FindPolicy_His_nNovement_Err
        lrecreaFindPolicy_His_nNovement = New eRemoteDB.Execute

        '+ Definición de store procedure reaPolicy_his_type_last al 12-06-2001 10:50:41
        With lrecreaFindPolicy_His_nNovement
            .StoredProcedure = "INSCAL0110PKG.REA_SFILE_REPORT"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                If Not .EOF Then
                    Me.sReport = .FieldToClass("sfile_report")
                    FindPolicy_sfile_report = True
                End If
            End If

        End With

FindPolicy_His_nNovement_Err:
        If Err.Number Then
            FindPolicy_sfile_report = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaFindPropType_Hist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaFindPolicy_His_nNovement = Nothing
    End Function



    Public Function UpdatePolicy_his_sReport() As Boolean

        Dim lrecupdPolicy_his_sReport As eRemoteDB.Execute

        On Error GoTo Update_Policy_his_sReport_Err

        lrecupdPolicy_his_sReport = New eRemoteDB.Execute


        With lrecupdPolicy_his_sReport
            .StoredProcedure = "INSCAL0110PKG.UPDPOLICY_HIS_SREPORT"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReport", sReport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdatePolicy_his_sReport = .Run(False)

        End With
        'UPGRADE_NOTE: Object lrecupdPolicy_his_ClaimOccurdat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        UpdatePolicy_his_sReport = Nothing

Update_Policy_his_sReport_Err:
        If Err.Number Then
            UpdatePolicy_his_sReport = False
        End If
    End Function


    Function Extraer(ByVal Path As String, ByVal Caracter As String) As String
        Dim ret As String
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))
        Extraer = ret
    End Function



    '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Private Function InstancingObjects(strPathReport As String) As Boolean
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim lintIndex As Integer
        Dim lstrValue As String
        Dim lstrLocation As String
        Dim lintPos As Integer
        On Error GoTo ErrorHandler

        LogThis("Step " & 1)

        InstancingObjects = True

        LogThis("Step " & 2)

        lmdlAplication = CreateObject("CrystalRuntime.Application")
        LogThis("Step " & 3)

        lmdlReport = lmdlAplication.OpenReport(strPathReport)
        LogThis("Step " & 4)

        lmdlReport.DiscardSavedData()
        lmdlReport.DiscardSavedData()
        lmdlReport.DiscardSavedData()

        LogThis("Step " & 5)
        lmdlReport.Database.Tables(1).SetLogOnInfo(sServer, sDatabase, mstrLogin, mstrPassword)
        'lstrLocation = "insudb.Reapolicydatpkg." & lmdlReport.Database.Tables(1).Location
        LogThis("Step " & 6)

        lstrLocation = lmdlReport.Database.Tables(1).Location
        lintPos = InStr(1, lstrLocation, "Insudb", vbTextCompare)
        If lintPos > 0 Then
            lstrLocation = Mid$(lstrLocation, lintPos, Len(lstrLocation))
            lmdlReport.Database.Tables(1).Location = lstrLocation
        End If
        LogThis("Step " & 7)

        For lintIndex = 1 To DBParameters.Count()
            LogThis("Step " & 8)

            Dim lstrNameParameter As String

            lstrValue = DBParameters.Item(lintIndex)

            lstrNameParameter = lmdlReport.ParameterFields(lintIndex).name
            Select Case lmdlReport.ParameterFields(lintIndex).ValueType
                Case 12 'crSPTVarChar
                    lmdlReport.ParameterFields(lintIndex).AddCurrentValue(CStr(lstrValue))
                Case 2 'crSPTNumeric
                    lmdlReport.ParameterFields(lintIndex).AddCurrentValue(CInt(IIf(lstrValue = vbNullString, 0, lstrValue)))
                Case 7 'crSPTReal
                    If lstrValue <> vbNullString Then
                        If lstrValue <> 0 Then
                            If InStr(1, lstrNameParameter, "nPolicy", vbTextCompare) > 0 Then
                                nPolicy = CLng(IIf(lstrValue = vbNullString, 0, lstrValue))
                            End If
                            If InStr(1, lstrNameParameter, "nBranch", vbTextCompare) > 0 Then
                                nBranch = CLng(IIf(lstrValue = vbNullString, 0, lstrValue))
                            End If
                            If InStr(1, lstrNameParameter, "nProduct", vbTextCompare) > 0 Then
                                nProduct = CLng(IIf(lstrValue = vbNullString, 0, lstrValue))
                            End If

                            If InStr(1, lstrNameParameter, "nCertif", vbTextCompare) > 0 Then
                                nCertif = CLng(IIf(lstrValue = vbNullString, 0, lstrValue))
                            End If

                            If InStr(1, lstrNameParameter, "nClaim", vbTextCompare) > 0 Then
                                nClaim = CLng(IIf(lstrValue = vbNullString, 0, lstrValue))
                            End If
                            lmdlReport.ParameterFields(lintIndex).AddCurrentValue(CDbl(IIf(lstrValue = vbNullString, 0, lstrValue)))
                        Else
                            lmdlReport.ParameterFields(lintIndex).AddCurrentValue(CInt(IIf(lstrValue = vbNullString, 0, lstrValue)))
                        End If
                    Else
                        lmdlReport.ParameterFields(lintIndex).AddCurrentValue(CInt(IIf(lstrValue = vbNullString, 0, lstrValue)))
                    End If
                Case 10 'crSPTTime
                    lmdlReport.ParameterFields(lintIndex).AddCurrentValue(CDate(lstrValue))
                Case 16
                    lmdlReport.ParameterFields(lintIndex).AddCurrentValue(CDate(lstrValue))
            End Select

        Next lintIndex
        LogThis("Step " & 9)

        If lintIndex <= lmdlReport.ParameterFields.count Then
            InstancingObjects = False
            LogThis("Step " & 10)

        End If
        LogThis("Step " & 11)

        Exit Function
ErrorHandler:
        LogThis("Err.number " & Err.Number)
        LogThis("Err.description " & Err.Description)
        Me.sException = Me.sException & " " & Err.Description
        InstancingObjects = False
        'Me.sPosition = "se cae en la instancia "
        'ProcError "CrystalExport.InstancingObjects(strPathReport )", Array(strPathReport)
        LogThis("CrystalExport.InstancingObjects(" & strPathReport & " )")
    End Function
    '+ Generación del Certificado 7 DJ1890: Crea el nombre del reporte que se almacena en el servidor
    Private Function NameCertifSeven(ByVal dCompdate As Date, ByVal sClient As String, ByVal nId As Integer) As String
        'Nombre: Fecha de ejecución-RUTERO-DJ-Certificado.pdf - Campos necesarios Fecha de ejecucíón y el id del certificado
        NameCertifSeven = sClient & "-DJ-" & nId & ".pdf"

    End Function
    Public Function GenCertifSeven(ByVal nFormatType As String,
                                          ByVal nReportType As String,
                                              ByVal strLogin As String,
                                              ByVal strPassword As String,
                                              Optional ByVal bErrModule As Boolean = False,
                                              Optional ByVal sVTRoot As String = "") As Integer

        Dim nCount As Integer
        Dim report1 As String = ""
        Dim report2 As String = ""
        Dim nameRandom As String
        Dim sNameReport As String
        Dim MergePDF As New MergePDF
        Dim exp As New Export
        Dim lreRemote As New eRemoteDB.Execute
        Dim visual As New VisualTimeConfig
        Dim sPath As String
        Dim i As Integer
        Dim sCertype_aux As Long

        Dim sRoutine, sPathArchivo, sPatharchivoPdf, sExtencion As String
        Dim bPrint As Boolean
        Dim oMergePDF As MergePDF

        On Error GoTo InvokeRealExport2_Err

        sPath = visual.LoadSetting("ExportDirectoryReport", "/Reports/", "Paths")
        sPathArchivo = visual.LoadSetting("ExportDirectoryPolicy", "/Reports/", "Paths")
        nameRandom = sPath & Guid.NewGuid().ToString() & ".pdf"

        sPatharchivoPdf = Me.sReport

        If Me.sReport <> "" And File.Exists(sPatharchivoPdf) Then
            _sExportedFilePath = sPatharchivoPdf
            GenCertifSeven = True
        Else
            Me.sReport = NameCertifSeven(Me.dCompdate, Me.sClient, Me.nId)
            sPatharchivoPdf = sPath & Me.sReport 'sPathArchivo + Me.sReport
            nCount = 0
            GenCertifSeven = False
            exp = New Export

            With lreRemote
                For i = 0 To Me.ReportParameters.Count - 1
                    exp.ReportParameters.Add(ReportParameters(i))
                Next

                bPrint = True
                sNameReport = sVTRoot & "\reports\" & Me.sReportName 'tiene que ser ruta + nombre
                report2 = sPatharchivoPdf.Replace("/", "") 'Me.sReport
                nGenReportseven = 1
                sExtencion = Extraer(sNameReport.ToString.ToUpper, ".")
                If bPrint Then
                    Select Case sExtencion
                        Case "RPT"
                            If exp.RealExport(sNameReport, "xxx.rpt", nFormatType, "", "", strLogin, strPassword) Then
                                GenCertifSeven = True
                                report1 = exp.sExportedFilePath
                                Call File.Copy(report1, report2, True)
                                Call File.Delete(report1)
                            End If
                        Case "PDF"
                            Call File.Copy(report1, report2)
                            Call File.Delete(report1)
                    End Select
                End If
                .RCloseRec()
            End With

            'hacer que el reporte se guarde en la ruta
            'If GenCertifSeven Then
            '    'copia el reporte en la ruta 
            '    Call File.Copy(_sExportedFilePath, sPatharchivoPdf, True)
            '    Me.UpdatePolicy_his_sReport()
            'End If..
        End If


InvokeRealExport2_Err:
        If (Err.Number > 0) Or (Err.Number < 0) Then
            GenCertifSeven = False
            sError = "[GenCertifseven]" & Err.Number & "-" & Err.Description
            sErrorReport = Err.Description
            nCount = 0
            GenCertifSeven = False
            exp = New Export
        End If
    End Function
End Class
