Option Strict Off
Option Explicit On
Public Class Report 
    '%-------------------------------------------------------%'
    '% $Workfile:: Report.cls                               $%'
    '% $Author:: Rabreu                                     $%'
    '% $Date:: 15/05/06 18:33                               $%'
    '% $Revision:: 2                                        $%'
    '%-------------------------------------------------------%'`    qa\ 

    Public ReportFilename As String
    Public Tittle As String
    Public sCodispl As String
    Public bTimeOut As Boolean
    Public nTimeOut As Double
    Public bErrModule As Boolean
    Public nLeft As Integer
    Public nTop As Integer
    Public nWidth As Integer
    Public nHeight As Integer
    Public Merge As Boolean
    Public MergeBranch As Integer
    Public MergeProduct As Integer
    Public MergePolicy As Double
    Public MergeCertif As Double
    Public MergeCertype As String
    Public MergeCartol As String
    Public nCopies As Short
    Public nFormat As Integer
    Public nReport As Integer
    Public nGenPolicy As Integer = 0
    Public nMovement As Integer = 0
    Public nForzaRep As Integer = 0
    Public nTratypep As Integer = 0
    Public sReport As String
    Public nCopyPolicy As Integer = 0
    Public nBranch As Long
    Public nProduct As Long
    Public sCertype As String
    Public nPolicy As Long
    Public nCertif As Long
    Public sNameReport As String

    Public sPolitype As String
    Public sCartol As String
    Public nCartol As String



    Public Enum sTypeServer
        sSQLServer65 = 1
        sSQLServer7 = 2
        sOracle = 3
        sInformix = 4
        sDB2 = 5
    End Enum

    Private mcolSPParameters As Collection
    Private mcolParameters As Collection

    Private mstrDataBase As String
    Private mintServer As sTypeServer
    Private mstrServerName As String
    '-Correlativo de los reportes
    '-Permite mostrar dos veces un reporte con el mismo nombre
    Private mintSeqReport As Integer
    '**%RepReset: Property to initialize the Crystal Report control
    '% RepReset: propiedad de inicialización del control de Crystal
    'UPGRADE_NOTE: Reset was upgraded to Reset_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Sub Reset()
        ReportFilename = String.Empty
        Tittle = String.Empty
        bTimeOut = False
        nTimeOut = 1000
        nLeft = 70
        nTop = 150
        nWidth = 660
        nHeight = 330
        'UPGRADE_NOTE: Object mcolParameters may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mcolParameters = Nothing
        'UPGRADE_NOTE: Object mcolSPParameters may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mcolSPParameters = Nothing
        mcolParameters = New Collection
        mcolSPParameters = New Collection

    End Sub

    '**% StorProcParams: Assings the parameters to be sent to the SP
    '% StorProcParams: asigna los parámetros a enviar al SP
    Public Sub setStorProcParam(ByVal Index As Integer, ByVal Value As Object)
        mcolSPParameters.Add(Value, "p" & Index)
    End Sub

    '**% ParamFields: Assings the parameters to send to the report
    '% ParamFields: Asigna los parámetros a enviar al reporte
    Public Sub setParamField(ByVal Index As Integer, ByVal Name As String, ByVal Value As Object)
        mcolParameters.Add(Value, Name)
    End Sub

    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        Dim lclsRegistry As eFunctions.Values
        Dim mvarSPParameters(15) As Object
        Dim mvarParameters(15, 2) As Object

        '**+ Gets the Server in use
        '+ Se obtiene el Servidor con el cual se está trabajando.
        lclsRegistry = New eFunctions.Values
        If lclsRegistry.insGetSetting("Server", String.Empty) > String.Empty Then
            mintServer = SelServer(lclsRegistry.insGetSetting("Server", String.Empty))
            If mintServer = sTypeServer.sSQLServer65 Or mintServer = sTypeServer.sSQLServer7 Then
                mstrServerName = lclsRegistry.insGetSetting("ServerName", String.Empty)
            Else
                mstrServerName = lclsRegistry.insGetSetting("Provider", String.Empty)
            End If

            mstrDataBase = lclsRegistry.insGetSetting("Database", String.Empty)
        End If
        'UPGRADE_NOTE: Object lclsRegistry may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRegistry = Nothing
        mcolParameters = New Collection
        mcolSPParameters = New Collection

        '+ Se inicializan variables publicas
        bTimeOut = False
        nTimeOut = 1000
        bErrModule = False
        nLeft = 70
        nTop = 150
        nWidth = 660
        nHeight = 330
        mintSeqReport = 1
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**% setdate: Changes the format of the date
    '% setdate: Transforma la fecha de formato
    Public Function setdate(ByVal sdate As String) As String
        If IsDate(sdate) Then
            'setdate = FormatDateTime(CDate(sdate), DateFormat.ShortDate).ToString()
            setdate = CDate(sdate).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        End If

    End Function

    '**% settime: Returns the system hour
    '% settime: Devuelve la hora del sistema
    Public Function settime(ByVal sdate As String) As String
        settime = IIf(sdate = String.Empty, String.Empty, CStr(Hour(CDate(sdate))) & ":" & CStr(Minute(CDate(sdate))))
    End Function

    '**% insMakeURL: Builds the URL (Address of the report)
    '% insMakeURL: Construye el URL (Dirección del reporte)
    Private Function insMakeURL() As String
        Dim lobjQuery As Object

        insMakeURL = String.Empty
        insMakeURL = "/VTimeNet/reports/" & ReportFilename

        'UPGRADE_NOTE: Object lobjQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjQuery = Nothing
    End Function


    '**% Command: Executes the report
    '% Command: Ejecuta el reporte
    'UPGRADE_NOTE: Command was upgraded to Command_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public ReadOnly Property Command() As String
        Get
            Dim lclsRegistry As eFunctions.Values
            Dim lstrURL As String
            Dim lintIndex As Integer
            Dim lstrReportName As String
            Dim larrDummy() As String
            Dim sPatharchivoPdf As String

            '+Se obtiene nombre de base de datos al cual conectarse
            lclsRegistry = New eFunctions.Values
            If lclsRegistry.insGetSetting("Server", String.Empty) > String.Empty Then
                mintServer = SelServer(lclsRegistry.insGetSetting("Server", String.Empty))
                If mintServer = sTypeServer.sSQLServer65 Or mintServer = sTypeServer.sSQLServer7 Then
                    mstrServerName = lclsRegistry.insGetSetting("ServerName", String.Empty)
                Else
                    mstrServerName = lclsRegistry.insGetSetting("Provider", String.Empty)
                End If

                mstrDataBase = lclsRegistry.insGetSetting("Database", String.Empty)
            End If

            sPatharchivoPdf = lclsRegistry.insGetSetting("ExportDirectoryPolicy", "/Reports/", "Paths")

            'UPGRADE_NOTE: Object lclsRegistry may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsRegistry = Nothing

            lstrURL = "/VTimeNet/Common/Reports/Report.aspx?URL=" & insMakeURL() '& "&ServerName=" & mstrServerName & "&DataBase=" & mstrDataBase & "&Server=" & mintServer & "&nPriority=" & GetPriority(ReportFilename) & "&nCopies=" & nCopies

            '+Se anexan los parametros del reporte y del procedimiento almacenado
            For lintIndex = 1 To mcolParameters.Count()
                lstrURL = lstrURL & "&p" & "=" & mcolParameters.Item(lintIndex)
            Next lintIndex
            For lintIndex = 1 To mcolSPParameters.Count()
                lstrURL = lstrURL & "&sp" & "=" & mcolSPParameters.Item(lintIndex)
            Next lintIndex

            lstrURL = lstrURL & "&Merge" & "=" & Merge
            lstrURL = lstrURL & "&MergeBranch" & "=" & MergeBranch
            lstrURL = lstrURL & "&MergeProduct" & "=" & MergeProduct
            lstrURL = lstrURL & "&MergePolicy" & "=" & MergePolicy
            lstrURL = lstrURL & "&MergeCertif" & "=" & MergeCertif
            lstrURL = lstrURL & "&MergeCertype" & "=" & MergeCertype
            lstrURL = lstrURL & "&nFormat" & "=" & nFormat
            lstrURL = lstrURL & "&nreport" & "=" & nReport
            lstrURL = lstrURL & "&nGenPolicy" & "=" & nGenPolicy
            lstrURL = lstrURL & "&nMovement" & "=" & nMovement
            lstrURL = lstrURL & "&nForzaRep" & "=" & nForzaRep
            lstrURL = lstrURL & "&nTratypep" & "=" & nTratypep
            lstrURL = lstrURL & "&nCopyPolicy" & "=" & nCopyPolicy
            lstrURL = lstrURL & "&sPolitype" & "=" & sPolitype
            lstrURL = lstrURL & "&sCartol" & "=" & sCartol
            lstrURL = lstrURL & "&nCartol" & "=" & nCartol
            'lstrURL = lstrURL & "&sReport" & "=" & sReport


            larrDummy = Microsoft.VisualBasic.Split(ReportFilename, ".")
            lstrReportName = larrDummy(0)

            '+Se crea comando con ventana popup que despliega reporte
            '+Para crear nombre de ventana popup, se concatena nombre de reporte + correlativo
            '+Esto permite mostrar el mismo reporte en dos ventanas distintas, por
            '+si se llama al mismo con distintos parametros
            '+  Ej: COL502_1 y COL502_2
            If sNameReport <> vbNullString And InStr(1, UCase(sNameReport), ".PDF") > 0 Then
                sPatharchivoPdf = sPatharchivoPdf & sNameReport
                sPatharchivoPdf = Replace(sPatharchivoPdf, "\", "/")
                Command = "<SCRIPT>ShowPopUp('" & sPatharchivoPdf & "','R" & (New Random).Next() & "'," & nWidth & "," & nHeight & ", '', 'yes'," & nLeft & "," & nTop & ");</SCRIPT>"
            Else
                If bTimeOut Then
                    Command = "<SCRIPT>setTimeout(""ShowPopUp('" & lstrURL & "','" & lstrReportName & "_" & mintSeqReport & "'," & nWidth & "," & nHeight & ", '', 'yes'," & nLeft & "," & nTop & ")""," & nTimeOut & ");</SCRIPT>"
                Else
                    Command = "<SCRIPT>ShowPopUp('" & lstrURL & "','R" & (New Random).Next() & "_" & mintSeqReport & "'," & nWidth & "," & nHeight & ", '', 'yes'," & nLeft & "," & nTop & ");</SCRIPT>"
                End If
            End If
            '+Se aumenta cantidad de reportes mostrados con el mismo objeto
            mintSeqReport = mintSeqReport + 1

        End Get
    End Property   

    '**%SelServer: This method selects the server to read and write data
    '%SelServer: Este metodo selecciona el servidor de base de datos
    Private Function SelServer(ByRef strServer As String) As sTypeServer
        strServer = UCase(Trim(strServer))
        Select Case strServer
            Case "1" 'SQLSERVER65
                SelServer = sTypeServer.sSQLServer65
            Case "2" 'ORACLE
                SelServer = sTypeServer.sOracle
            Case "3" 'SQLSERVER7
                SelServer = sTypeServer.sSQLServer7
            Case "4" 'DB2
                SelServer = sTypeServer.sDB2
            Case Else
                SelServer = sTypeServer.sSQLServer65
        End Select
    End Function



    '**%GetPriority: Retorna la prioridad del reporte en milisegundos
    Private Function GetPriority(ByRef sReportName As String) As Short
        Dim lrecPriority As eRemoteDB.Execute

        On Error GoTo err_h
        lrecPriority = New eRemoteDB.Execute
        With lrecPriority
            .StoredProcedure = "reaReportPriority"
            .Parameters.Add("sReportName", sReportName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                GetPriority = .FieldToClass("nPriority")
                .RCloseRec()
            Else
                GetPriority = 10000
            End If
        End With

        Exit Function
err_h:
        'UPGRADE_NOTE: Object lrecPriority may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPriority = Nothing
        Err.Raise(Err.Number, Err.Description)
    End Function
End Class






