Imports System.Threading
Imports System.Configuration
Imports System.Reflection
Imports System.Text
Imports System.Web.UI.WebControls
Imports System.Security.Principal

''' <summary>
''' Does the impersontation to a specific user on the ASP NET environment
''' </summary>
''' <remarks></remarks>
Public Class Impersonation

    Private Const LOGON32_PROVIDER_DEFAULT As Integer = 0
    Private Const LOGON32_LOGON_INTERACTIVE As Integer = 2
    Private Const LOGON32_LOGON_NETWORK As Integer = 3
    Private Const LOGON32_LOGON_BATCH As Integer = 4
    Private Const LOGON32_LOGON_SERVICE As Integer = 5
    Private Const LOGON32_LOGON_UNLOCK As Integer = 7
    Private Const LOGON32_LOGON_NETWORK_CLEARTEXT As Integer = 8
    Private Const LOGON32_LOGON_NEW_CREDENTIALS As Integer = 9

    Private Shared ImpersonationContext As WindowsImpersonationContext

    Declare Function LogonUserA Lib "advapi32.dll" ( _
                            ByVal lpszUsername As String, _
                            ByVal lpszDomain As String, _
                            ByVal lpszPassword As String, _
                            ByVal dwLogonType As Integer, _
                            ByVal dwLogonProvider As Integer, _
                            ByRef phToken As IntPtr) As Integer

    Declare Auto Function DuplicateToken Lib "advapi32.dll" ( _
                            ByVal ExistingTokenHandle As IntPtr, _
                            ByVal ImpersonationLevel As Integer, _
                            ByRef DuplicateTokenHandle As IntPtr) As Integer
    Declare Auto Function RevertToSelf Lib "advapi32.dll" () As Long
    Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Long

    ''' <summary>
    ''' The identity of the process that impersonates a specific user on a thread must have 
    ''' "Act as part of the operating system" privilege. If the the Aspnet_wp.exe process runs
    ''' under a the ASPNET account, this account does not have the required privileges to 
    ''' impersonate a specific user. This information applies only to the .NET Framework 1.0. 
    ''' This privilege is not required for the .NET Framework 1.1.
    ''' </summary>
    ''' <param name="strUserName">Sets the user domain</param>
    ''' <param name="strDomain">Sets the domain name</param>
    ''' <param name="strPassword">Sets the user password</param>
    ''' <returns>Returns true if the impersonation was succeded, otherwise false</returns>
    ''' <remarks>
    ''' Sample call:
    '''
    '''    If impersonateValidUser("username", "domain", "password") Then
    '''        'Insert your code here.
    '''
    '''        undoImpersonation()
    '''    Else
    '''        'Impersonation failed. Include a fail-safe mechanism here.
    '''    End If
    ''' </remarks>
    Public Shared Function ImpersonateValidUser(ByVal strUserName As String, _
           ByVal strDomain As String, ByVal strPassword As String) As Boolean
        Dim token As IntPtr = IntPtr.Zero
        Dim tokenDuplicate As IntPtr = IntPtr.Zero
        Dim tempWindowsIdentity As WindowsIdentity
        Dim _isSucced As Boolean = False

        If RevertToSelf() <> 0 Then
            If LogonUserA(strUserName, strDomain, _
               strPassword, _
               LOGON32_LOGON_INTERACTIVE, _
               LOGON32_PROVIDER_DEFAULT, token) <> 0 Then
                If DuplicateToken(token, 2, tokenDuplicate) <> 0 Then
                    tempWindowsIdentity = New WindowsIdentity(tokenDuplicate)
                    ImpersonationContext = tempWindowsIdentity.Impersonate()

                    If Not (ImpersonationContext Is Nothing) Then
                        _isSucced = True
                    End If
                End If
            End If
        End If

        If Not tokenDuplicate.Equals(IntPtr.Zero) Then
            CloseHandle(tokenDuplicate)
        End If

        If Not token.Equals(IntPtr.Zero) Then
            CloseHandle(token)
        End If

        Return _isSucced
    End Function

    ''' <summary>
    ''' Validates an user on windows
    ''' </summary>
    ''' <param name="strUserName">Sets the user domain</param>
    ''' <param name="strDomain">Sets the domain name</param>
    ''' <param name="strPassword">Sets the user password</param>
    ''' <returns>Returns true if the user is valid, otherwise false</returns>
    ''' <remarks>
    ''' </remarks>
    Public Shared Function ValidateWindowsUser(ByVal strUserName As String, _
           ByVal strDomain As String, ByVal strPassword As String) As Boolean
        Dim token As IntPtr = IntPtr.Zero
        Dim _isSucced As Boolean = False

        If RevertToSelf() <> 0 Then
            If LogonUserA(strUserName, strDomain, _
               strPassword, _
               LOGON32_LOGON_INTERACTIVE, _
               LOGON32_PROVIDER_DEFAULT, token) <> 0 Then
                _isSucced = True
            End If
        End If

        Return _isSucced
    End Function

    ''' <summary>
    ''' Undo the impersonation 
    ''' </summary>
    Public Shared Sub UndoImpersonation()
        ImpersonationContext.Undo()
    End Sub
End Class
