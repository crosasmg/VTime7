Imports System.Linq
Imports System.Reflection
Imports InMotionGIT.Core.Configuration
Imports System.Net

Partial Class Support_email
    Inherits System.Web.UI.Page

#Region "Filds"
    Dim config As VisualTIME = TryCast(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)
#End Region

#Region "Helpers"

    ''' <summary>
    ''' Verify the security of the page
    ''' </summary>
    ''' <returns>True if all conditions are valid, call from localhost and user admin</returns>
    Private Shared Function IsUserValid() As Boolean
               Return True
    End Function

    ''' <summary>
    ''' Verify if the host is localhost
    ''' </summary>
    ''' <param name="host"></param>
    ''' <returns>True if it's localhost</returns>
    Private Shared Function IsLocalIpAddress(host As String) As Boolean
        Try
            Dim hostIPs As IPAddress() = Dns.GetHostAddresses(host)
            ' get local IP addresses
            Dim localIPs As IPAddress() = Dns.GetHostAddresses(Dns.GetHostName())

            If Not HttpContext.Current.Request.IsLocal Then
                Return False
            End If

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

#End Region

#Region "Process"
    ''' <summary>
    '''
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getTypeEmail() As List(Of String)
        Dim result As New List(Of String)
        result.Add("NetMail")
        result.Add("ExchangeService")
        Return result
    End Function
#End Region

#Region "Events"

    ''' <summary>
    '''  Load method
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub txtSend_Click(sender As Object, e As System.EventArgs) Handles txtSend.Click
        Try
            Dim clientmail = Nothing
            Dim ModeMail As String = chkTypeEmail.SelectedItem.ToString
            Dim temporal As InMotionGIT.Core.Configuration.Enumerations.EnumMailMode
            System.Enum.TryParse(ModeMail, temporal)
            ModeMail = CInt(temporal)
            Dim host As String = txtHost.Text
            Dim credentialUserName As String = txtUserName.Text
            Dim credentialPassword As String = txtPassword.Text
            Dim port As Integer = txtPort.Text
            Dim enableSSL As Boolean = rbEnableSSL.Checked
            Dim [to] As String = txtTo.Text
            Dim bodyExample As String
            Dim c As Char = """"c
            bodyExample = "<Mail Mode=" + c + "{0}" + c +
                          " host = " + c + "{1}" + c +
                          " port = " + c + "{2}" + c +
                          " SupportMail =" + c + "{3}" + c +
                          " credentialUserName =" + c + "{4}" + c +
                          " credentialPassword =" + c + "{5}" + c +
                          " enableSSL =" + c + "{6}" + c +
                          " TemplatesPath=" + c + "{7}  />"




            Dim parametes As New InMotionGIT.FrontOffice.Contracts.Parameter
            With parametes
                .ParameterInternal = New Dictionary(Of String, String)
                With .ParameterInternal
                    .Add("Host", host)
                    .Add("Port", port)
                    .Add("CredentialPassword", credentialPassword)
                    .Add("CredentialUserName", credentialUserName)
                    .Add("EnableSSL", enableSSL)
                    .Add("ModeMail", ModeMail)
                End With
                .To = [to]
                .Body = "Test Body"
                .Subject = "Test Local"
                .Attachment = Server.MapPath("~/images/Logos/logo.png")
            End With
            If InMotionGIT.FrontOffice.Proxy.Helpers.Email.SendMailTest(parametes) Then
                lblMessage.Text = "I was sent the email correctly!!! "
                txtExample.Visible = True
                bodyExample = String.Format(bodyExample, ModeMail,
                                            host, port,
                                            credentialUserName, credentialUserName,
                                            credentialPassword, IIf(enableSSL, "True", "False"),
                                            config.Mail.TemplatesPath)
                txtExample.Text = bodyExample
            Else
                lblMessage.Text = "Se ejecuto el proceso!!!"
                lblMessage.Visible = True
            End If


        Catch ex As Exception
            lblMessage.Text = ex.Message
            txtExample.Visible = False
        Finally
            lblMessage.Visible = True
        End Try
    End Sub

#End Region

End Class