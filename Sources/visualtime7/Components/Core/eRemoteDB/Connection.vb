Option Strict Off
Option Explicit On

Imports System.Data.Common
Imports System.Configuration

''' <summary>
''' Class that supports the table Connection
''' </summary>
''' <remarks>$$Revision: $</remarks>
Public Class Connection

#Region "Internal varibles"

    ''' <summary>
    ''' 
    ''' </summary>
    Private mblnDebugActive As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    Private mblnLogActive As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    Private mTypeServer As sTypeServer

    
    ''' <summary>
    ''' 
    ''' </summary>
    Private mclsConnection As DbConnection

    ''' <summary>
    ''' The whole VisualStudioNet supported DbProviderFactory Name
    ''' </summary>
    Public Property ProviderName As String

    ''' <summary>
    ''' 
    ''' </summary>
    Private sqlProvider As DbProviderFactory

    Public bErr_Module As Object

    Public Database As Object
    
    Public Login As Object
    
    Public Password As Object    

#End Region

#Region "Public Properties"

    ''' <summary>
    ''' 
    ''' </summary>
    Public Enum sTypeServer
        sSQLServer65 = 1
        sSQLServer7 = 2
        sOracle = 3
        sInformix = 4
        sDB2 = 5
    End Enum

    ''' <summary>
    ''' 
    ''' </summary>
    Public RegPathConfig As String

    ''' <summary>
    ''' 
    ''' </summary>
    Public bMultiCompany As Boolean

    ''' <summary>
    ''' Obtiene la conección activa
    ''' </summary>
    Public ReadOnly Property Connection_Renamed() As DbConnection
        Get
            Return mclsConnection
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    Public ReadOnly Property Server() As sTypeServer
        Get
            Return mTypeServer
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    Public ReadOnly Property Provider() As DbProviderFactory
        Get
            Return sqlProvider
        End Get
    End Property

#End Region

#Region "Constructor..."

    ''' <summary>
    ''' Controls the creation of an instance of the class
    ''' </summary>
    Public Sub New()
        MyBase.New()

        Dim lclsRegistry As eRemoteDB.VisualTimeConfig
        Dim lstrServer As String

        RegPathConfig = "Database"

        lclsRegistry = New eRemoteDB.VisualTimeConfig
        bMultiCompany = (lclsRegistry.LoadSetting("MultiCompany", "No", "Database") = "Yes")

        mblnDebugActive = (lclsRegistry.LoadSetting("Active", "No", "Debug") = "Yes")
        mblnLogActive = (lclsRegistry.LoadSetting("Log", "No", "Debug") = "Yes")

        lstrServer = UCase(lclsRegistry.LoadSetting("Server", "Oracle", "Database"))
        Select Case lstrServer
            Case "ORACLE"
                mTypeServer = sTypeServer.sOracle
                ProviderName = "Oracle"
            Case "SQL SERVER", "SQLSERVER7"
                mTypeServer = sTypeServer.sSQLServer7
                ProviderName = "Sql"
            Case "DB2"
                mTypeServer = sTypeServer.sDB2
            Case "INFORMIX"
                mTypeServer = sTypeServer.sInformix
        End Select

        ProviderName = "System.Data." & ProviderName & "Client"
        Dim customProvider As String = lclsRegistry.LoadSetting("ConnectionStringProvider", String.Empty, "Database").ToLower
        If Not String.IsNullOrEmpty(customProvider) Then
            ProviderName = customProvider.ToLower
        End If

        lclsRegistry = Nothing
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Protected Overrides Sub Finalize()

        If Not mclsConnection Is Nothing Then

            If mclsConnection.State <> ConnectionState.Closed Then
                Try
                    mclsConnection.Close()
                Catch ex As Exception
                    NetHelper.WriteToEventLog("VisualTIME.Net: Error Exception(cnn): " + ex.Message)
                End Try

            End If
            mclsConnection = Nothing
        End If

        MyBase.Finalize()
    End Sub

#End Region

#Region "public Methods"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sLogin"></param>
    ''' <param name="sPassword"></param>
    ''' <param name="sDSN"></param>
    ''' <param name="sSessionID"></param>
    Public Function OpenConnection(Optional ByVal sLogin As String = "", Optional ByVal sPassword As String = "", _
                                   Optional ByVal sDSN As String = "", Optional ByVal sSessionID As String = "") As Boolean
        Dim clsRegistrySupport As eRemoteDB.VisualTimeConfig
        Dim strConString As String
        Dim CompanyId As Integer = 0

        If mblnDebugActive Then
            DebugLog("Push", "Connect.OpenConnection(Login,PassWord,DSN,sSessionID)", New Object() {sLogin, sPassword, sDSN, sSessionID})
        End If

        If mblnLogActive Then
            Call FileSupport.AddBufferToFile(sSessionID & "|Begin|Procedure|Connection|OpenConnection", sSessionID)
        End If

        clsRegistrySupport = New eRemoteDB.VisualTimeConfig

        bMultiCompany = (clsRegistrySupport.LoadSetting("MultiCompany", String.Empty, RegPathConfig).ToUpper = "YES")
        If bMultiCompany Then
            If ServiceEnviroment.CompanyNumber > 0 Then
                CompanyId = ServiceEnviroment.CompanyNumber
            Else
                With New eRemoteDB.ASPSupport
                    sLogin = .GetASPSessionValue("sInitialsCon")
                    sPassword = .GetASPSessionValue("sAccesswoCon")
                    Try
                        CompanyId = .GetASPSessionValue("CompanyId")
                    Catch ex As Exception
                        CompanyId = 0
                    End Try
                End With
            End If

            If CompanyId <= 0 Then
                CompanyId = 1
            End If

            If Not String.IsNullOrEmpty(sLogin) Then
                sLogin = CryptSupport.DecryptString(sLogin)
                sPassword = CryptSupport.DecryptString(sPassword)
            ElseIf CompanyId > 0 Then
                clsRegistrySupport.GetCompanySettings(CompanyId, "", sLogin, sPassword)
                sLogin = CryptSupport.DecryptString(sLogin)
                sPassword = CryptSupport.DecryptString(sPassword)
            Else
                Err.Raise(666, "eRemoteDB.Connection", "CompanyId is empty")
            End If
        Else
            If sLogin = String.Empty Then
                sLogin = CryptSupport.DecryptString(clsRegistrySupport.LoadSetting("User", String.Empty, RegPathConfig))
            End If
            If sPassword = String.Empty Then
                sPassword = CryptSupport.DecryptString(clsRegistrySupport.LoadSetting("Password", String.Empty, RegPathConfig))
            End If
        End If
        If sDSN = String.Empty Then
            sDSN = clsRegistrySupport.LoadSetting("ConnectionString", "Provider=MSDAORA.1;Data Source=ORACNSQA.GITUSA.COM;OLE DB Services=-1", RegPathConfig)
        End If
        sqlProvider = DbProviderFactories.GetFactory(ProviderName)

        mclsConnection = sqlProvider.CreateConnection

        clsRegistrySupport = Nothing

        strConString = sDSN & ";User ID=" & sLogin & ";Password=" & sPassword

        mclsConnection = sqlProvider.CreateConnection()

        With mclsConnection
            .ConnectionString = strConString
            .Open()
        End With
        OpenConnection = True

        If mblnDebugActive Then
            DebugLog("Pop", "Connect.OpenConnection(Return)", New Object() {sLogin, sPassword, sDSN, sSessionID})
        End If
        If mblnLogActive Then
            Call eRemoteDB.FileSupport.AddBufferToFile(sSessionID & "|Finish|Procedure|Connection|OpenConnection", sSessionID)
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub CloseConnection()
        If Not mclsConnection Is Nothing Then
            If mclsConnection.State <> ConnectionState.Broken And _
                mclsConnection.State <> ConnectionState.Closed Then
                mclsConnection.Close()
            End If
            mclsConnection.Dispose()
            mclsConnection = Nothing
        End If
    End Sub

#End Region

End Class

