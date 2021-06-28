Imports System.Net
Imports System.Web.Configuration
Imports System.Web.Services
Imports InMotionGIT.Common.Proxy

Partial Class VTimeNet_visualtime_Security
    Inherits System.Web.UI.Page

    Shared provider As String = "RSAProtectedConfigurationProvider"
    Shared sectionConfig As String = "connectionStrings"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '(!Request.IsSecureConnection)
        If Not IsUserValid() Then
            'Response.Write("<script>window.location.href = '" & ConfigurationManager.AppSettings("Url.WebApplication") & "';</script>")
            Response.Redirect(FormsAuthentication.DefaultUrl)
        End If
        'Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-1);
    End Sub


    ''' <summary>
    ''' Sets the credentials of the connection string in the web.config for
    ''' oracle and SQLServer Connections.
    ''' If not, encrypts the section.
    ''' </summary>
    ''' <param name="connName"></param>
    <WebMethod()>
    Public Shared Function TestConnection(connName As String) As Object
        Dim message As String = String.Empty
        Dim IsValid As Boolean = False
        Try
            If IsUserValid() Then
                Dim configuration As System.Configuration.Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath)
                Dim section As ConnectionStringsSection = DirectCast(configuration.GetSection(sectionConfig), ConnectionStringsSection)

                Dim conn = section.ConnectionStrings(connName)

                With New DataManagerFactory()
                    IsValid = .OpenConnection(connName)
                End With
            Else
                Throw New Exception("Not Valid")
            End If
        Catch ex As Exception
            message = ex.Message
        End Try
        Return New With {.Message = message, .IsValid = IsValid}
    End Function

    ''' <summary>
    ''' Sets the credentials of the connection string in the web.config for
    ''' oracle and SQLServer Connections.
    ''' If not, encrypts the section.
    ''' </summary>
    ''' <param name="connName"></param>
    ''' <param name="initialCatalog"></param>
    ''' <param name="dataSource"></param>
    ''' <param name="userId"></param>
    ''' <param name="pass1"></param>
    ''' <param name="pass2"></param>
    <WebMethod()>
    Public Shared Sub EncryptConfig(connName As String, initialCatalog As String, dataSource As String, userId As String, pass1 As String, pass2 As String)
        Try
            If IsUserValid() Then
                Dim configuration As System.Configuration.Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath)
                Dim section As ConnectionStringsSection = DirectCast(configuration.GetSection(sectionConfig), ConnectionStringsSection)

                Dim conn = section.ConnectionStrings(connName)

                If conn.ProviderName = "Oracle.DataAccess.Client" Then
                    conn.ConnectionString = "Data Source=" & dataSource & ";User ID=" & userId & ";Password=" & pass1 & pass2 & ";Min Pool Size=5;Max Pool Size=50;Connection Lifetime=120;Incr Pool Size=3;Decr Pool Size=1;Connection Timeout=5;"
                ElseIf conn.ProviderName = "System.Data.SqlClient" Then
                    conn.ConnectionString = "Data Source=" & dataSource & ";Initial Catalog=" & initialCatalog & ";User ID=" & userId & ";Password=" & pass1 & pass2 & ";Asynchronous Processing=true;"
                ElseIf conn.ProviderName = "System.Data.EntityClient" Then
                    conn.ConnectionString = "metadata=res://*/Model.Repository.csdl|res://*/Model.Repository.ssdl|res://*/Model.Repository.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=" & dataSource & ";initial catalog=" & initialCatalog & ";persist security info=True;user id=" & userId & ";password=" & pass1 & pass2 & ";multipleactiveresultsets=True;App=EntityFramework&quot;"
                ElseIf conn.Name = "STEF" Then
                    conn.ConnectionString = "Provider=ASEOLEDB; Server Name=" & dataSource & ";Catalog=" & initialCatalog & ";User ID=" & userId & ";Password=" & pass1 & pass2 & ";"
                End If

                If (Not IsNothing(section)) Then
                    section.SectionInformation.ProtectSection(provider)
                End If

                ' Esta sección se encarga de encriptar el user y password del archivo VisualTIMEConfig.xml
                ' para la conexión a base de datos, actualmente se sigue usando el ejecutable
                ' se deja para posible uso

                'eRemoteDB.VisualTimeConfig clsRegistrySupport = new eRemoteDB.VisualTimeConfig();
                'XmlDocument doc = new XmlDocument();
                'string path = clsRegistrySupport.XMLPath();
                'doc.Load(path);
                'XmlNodeList nodes = doc.SelectSingleNode("Config/MultiCompanies").ChildNodes;

                'foreach (XmlNode node in nodes)
                '{
                '    node.Attributes["user"].Value = eRemoteDB.CryptSupport.EncryptString(userId);
                '    node.Attributes["password"].Value = eRemoteDB.CryptSupport.EncryptString(pass1 + pass2);
                '}

                'doc.Save(path);
                'string oldText = File.ReadAllText(path);
                'string newText = oldText.Replace("\"", "'");
                'File.WriteAllText(path, newText, Encoding.UTF8);
                configuration.Save()
            Else
                Throw New Exception("Not Valid")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    ''' <summary>
    '''  Función que se encarga de desencriptar el contenido del web.config
    '''  y regresar a los valores originales.
    ''' </summary>
    <WebMethod()>
    Public Shared Sub DecryptConfig()
        Try
            If IsUserValid() Then
                Dim configuration As System.Configuration.Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath)
                Dim section As ConnectionStringsSection = DirectCast(configuration.GetSection(sectionConfig), ConnectionStringsSection)
                Dim connStringArray As String()

                ' Array objects at position 0 and 1 are
                ' "LocalSqlServer" and "OraAspNetConString"
                For i As Integer = 0 To section.ConnectionStrings.Count - 1
                    If ((Not section.ConnectionStrings(i).Name.Equals("LocalSqlServer")) And (Not section.ConnectionStrings(i).Name.Equals("OraAspNetConString"))) Then
                        'If section.ConnectionStrings(i).ProviderName = "Oracle.DataAccess.Client" Then
                        connStringArray = section.ConnectionStrings(i).ConnectionString.ToString().Split(";")
                        Dim connStringSB As StringBuilder = New StringBuilder

                        For Each connString In connStringArray
                            If connString.ToString().Contains("User ID=") Then
                                connString = "User ID=vtapps"
                            ElseIf connString.ToString().Contains("user id=") Then
                                connString = "user id=vtapps"
                            ElseIf connString.ToString().Contains("User Id=") Then
                                connString = "User Id=vtapps"
                            ElseIf connString.ToString().ToLower().Contains("user id=") Then
                                connString = "user id=vtapps"
                            ElseIf connString.ToString().Contains("Password=") Then
                                connString = "Password=vtapps"
                            ElseIf connString.ToString().Contains("password=") Then
                                connString = "password=vtapps"

                            End If
                            connStringSB.Append(connString)
                            connStringSB.Append(";")
                        Next

                        section.ConnectionStrings(i).ConnectionString = connStringSB.Remove((connStringSB.Length - 1), 1).ToString()
                        '<add name="FrontOfficeConnectionString" connectionString="Data Source=192.168.0.76\SQLFRONTOFFICE;Initial Catalog=FrontOffice;User ID=vtapps;Password=vtapps; Asynchronous Processing=true" providerName="System.Data.SqlClient" />
                        '<add name="STEF" connectionString="driver=ASEOLEDB;DataSource=sybase_con:4020;User Id=stef_vt;Password=VT_stef1”/>
                        '<add name="BackOfficeConnectionString" connectionString="Data Source=TIME;Min Pool Size=5;Max Pool Size=50;Connection Lifetime=120;Incr Pool Size=3;Decr Pool Size=1;Connection Timeout=5" providerName="Oracle.DataAccess.Client" />
                        '<add name="Repository" connectionString="metadata=res://*/Model.Repository.csdl|res://*/Model.Repository.ssdl|res://*/Model.Repository.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=181.193.37.28;initial catalog=frontoffice;persist security info=True;user id=vtapps;password=vtapps;multipleactiveresultsets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
                    End If
                Next

                If section.SectionInformation.IsProtected Then
                    section.SectionInformation.UnprotectSection()
                End If

                configuration.Save()
            Else
                Throw New Exception("Not Valid")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Gets all the connString that are inside the web.config
    ''' </summary>
    ''' <returns>A list of connection strings from the web.config file</returns>
    <WebMethod()>
    Public Shared Function GetConnStrings() As List(Of ConnString)
        Try
            If True Then
                Dim configuration As System.Configuration.Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath)
                Dim section As ConnectionStringsSection = DirectCast(configuration.GetSection(sectionConfig), ConnectionStringsSection)

                Dim connStrings As New List(Of ConnString)()

                ' Array objects at position 0 and 1 are
                ' "LocalSqlServer" and "OraAspNetConString"
                For i As Integer = 0 To section.ConnectionStrings.Count - 1
                    If ((Not section.ConnectionStrings(i).Name.Equals("LocalSqlServer")) And (Not section.ConnectionStrings(i).Name.Equals("OraAspNetConString"))) Then
                        If (section.ConnectionStrings(i).ProviderName.Equals("System.Data.SqlClient") Or
                            section.ConnectionStrings(i).ProviderName.Equals("Oracle.DataAccess.Client") Or
                            section.ConnectionStrings(i).ProviderName.Equals("System.Data.EntityClient") Or
                            section.ConnectionStrings(i).Name.Equals("STEF")) Then
                            connStrings.Add(New ConnString(section.ConnectionStrings(i).ProviderName, section.ConnectionStrings(i).Name))
                        End If
                    End If
                Next

                Return connStrings
            Else
                Throw New Exception("Not Valid")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Verify the security of the page
    ''' </summary>
    ''' <returns>True if all conditions are valid, call from localhost and user admin</returns>
    Private Shared Function IsUserValid() As Boolean
        Try
            If Not IsLocalIpAddress(HttpContext.Current.Request.Url.Host) Then
                Return False
                'if (!HttpContext.Current.User.Identity.IsAuthenticated) return false;
                'if (!(Membership.GetUser().UserName == "admin")) return false;
                'if (!Roles.IsUserInRole("Administrador")) return false;
            End If
        Catch
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Verify if the host is localhost
    ''' </summary>
    ''' <param name="host"></param>
    ''' <returns>True if it's localhost</returns>
    Private Shared Function IsLocalIpAddress(host As String) As Boolean
        Try
            ' get host IP addresses
            Dim hostIPs As IPAddress() = Dns.GetHostAddresses(host)
            ' get local IP addresses
            Dim localIPs As IPAddress() = Dns.GetHostAddresses(Dns.GetHostName())

            If Not HttpContext.Current.Request.IsLocal Then
                Return False
            End If

            ' test if any host IP equals to any local IP or to localhost
            For Each hostIP As IPAddress In hostIPs
                ' is localhost
                If IPAddress.IsLoopback(hostIP) Then
                    Return True
                End If

                ' is local address
                For Each localIP As IPAddress In localIPs
                    If hostIP.Equals(localIP) Then
                        Return True
                    End If
                Next
            Next
        Catch
        End Try
        Return False
    End Function

End Class

''' <summary>
''' Object that contains all the info related to the
''' type and name of the conn string contain in the web.config
''' </summary>
Public Class ConnString

    ''' <summary>
    ''' Type of connection, Oralce or SQLServer
    ''' </summary>
    Public Property ConnType() As String
        Get
            Return m_ConnType
        End Get
        Set(value As String)
            m_ConnType = value
        End Set
    End Property

    Private m_ConnType As String

    ''' <summary>
    ''' Name of the connection in the connString of the web.config file.
    ''' </summary>
    Public Property ConnName() As String
        Get
            Return m_ConnName
        End Get
        Set(value As String)
            m_ConnName = value
        End Set
    End Property

    Private m_ConnName As String

    ''' <summary>
    ''' Primary constructor of the class
    ''' </summary>
    ''' <param name="connType"></param>
    ''' <param name="connName"></param>
    Public Sub New(connType As String, connName As String)
        Me.ConnType = connType
        Me.ConnName = connName
    End Sub

End Class