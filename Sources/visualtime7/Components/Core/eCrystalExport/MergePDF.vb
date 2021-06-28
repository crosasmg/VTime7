Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports eRemoteDB 
Imports System.IO
Imports SmartSoft.PdfLibrary



Public Class MergePDF
    Public bErr_Module As Boolean
    Public sExportedFileName As String
    Public sExportedFilePath As String
    Public sException As String

    Private Function GetPathClauses() As String
        Dim VTConfig As New VisualTimeConfig
        GetPathClauses = VTConfig.LoadSetting("ClauseLoad", , "Paths")
    End Function

    '------------------------------------------------------------------------------------
    Private Function NamePDF() As String
        '------------------------------------------------------------------------------------
        Randomize(Timer)

        NamePDF = Guid.NewGuid().ToString() & Replace((Rnd() * 10), ",", "")
    End Function

    Public Function MergePDFs2(ByVal nBranch As Long, _
                    ByVal nProduct As Long, _
                    ByVal nPolicy As Double, _
                    ByVal nCertif As Double, _
                    ByVal pathFileCuadro_Polizas As String, _
                    ByVal sPathOfCopy As String, _
                    ByVal nCopies As Integer, _
                    Optional ByVal sCertype As String = "2") As Boolean

        Dim sOldDir As String, inputFolder As String, s As String
        Dim fso As System.IO.File
        Dim f As System.IO.Directory
        Dim i As Integer
        Dim pathClauses As String
        Dim nameRandom As String = ""
        Dim intModulec As Integer
        Dim sCurrentFile As String
        Dim lrecreaTab_Clause_a As eRemoteDB.Execute
        Dim sBaseDoc As String
        Dim sCurrFile As String
        Dim nCounter As Integer
        Dim nCopyCounter As Integer
        On Error GoTo err_h
        lrecreaTab_Clause_a = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.getModulec'
        '+Información leída el 11/04/2001 13:24:24
        nCounter = 20001
        With lrecreaTab_Clause_a
            .StoredProcedure = "READOC_POLICY"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .bErr_Module = bErr_Module
        End With
        nCounter = 20002


        'boMuteMode = True
        nCounter = 20003

        pathClauses = GetPathClauses()
        sBaseDoc = pathFileCuadro_Polizas
        nCounter = 20005

        'creo un directorio de donde se sacaran los pdf para mergear
        sOldDir = pathClauses & nameRandom & "\"

        nCounter = 20006

        With lrecreaTab_Clause_a
            'sCurrentFile =
            If .Run Then
                nCounter = 20007
                Do While Not .EOF
                    'si existen, copio los pdf a una carpeta temporal
                    nCounter = 20008
                    sCurrFile = pathClauses & Replace(Trim(.FieldToClass("sDoc_attach")), ".doc", ".pdf")
                    nCounter = 20009
                    If File.Exists(sCurrFile) Then
                        nCounter = 20010
                        nameRandom = pathFileCuadro_Polizas & NamePDF()
                        nCounter = 20011
                        Call Merge2PDFs(sBaseDoc, sCurrFile, nameRandom)
                        nCounter = 20012
                        sBaseDoc = nameRandom
                        'sExportedFileName = nameRandom
                        sExportedFilePath = nameRandom
                        i = i + 1
                    End If
                    .RNext()
                Loop
                .RCloseRec()
            Else
                nCounter = 20013
                sExportedFileName = ""
                sExportedFilePath = pathFileCuadro_Polizas
            End If
        End With
        nCounter = 20014
        For nCopyCounter = 1 To nCopies
            If File.Exists(sPathOfCopy) Then
                nCounter = 20015
                nameRandom = pathFileCuadro_Polizas & NamePDF()
                nCounter = 20016
                Call Merge2PDFs(sBaseDoc, sPathOfCopy, nameRandom)
                nCounter = 20017
                sBaseDoc = nameRandom
                sExportedFileName = ""
                sExportedFilePath = nameRandom
                i = i + 1
            End If
        Next

        MergePDFs2 = True
        lrecreaTab_Clause_a = Nothing
        Exit Function
err_h:
        sException = "[MergePDFs2] C" & nCounter & " " & Err.Number & "-" & Err.Description
        Err.Clear()
        MergePDFs2 = False
        lrecreaTab_Clause_a = Nothing
    End Function

    Public Function Merge2PDFs(ByVal sDoc1, ByVal sDoc2, ByVal resultName) As Boolean
        Dim filesByte As New List(Of Byte())()
        filesByte.Add(File.ReadAllBytes(sDoc1))
        filesByte.Add(File.ReadAllBytes(sDoc2))

        File.WriteAllBytes(resultName, PdfMerger.MergeFiles(filesByte))

        Return True
    End Function


End Class
