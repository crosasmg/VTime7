Public Class ServiceEnviroment

    Public Shared Property isServiceConsumer As Boolean = False

    Public Shared Property CompanyNumber As Integer = 0

    Public Shared Property Usercode As Integer

    Public Shared Property Schema As String

    Public Shared Property SecurityLevel As Integer

    Public Shared Property LastErrorValidate() As ArrayList


    Public Shared Function GetValue(ByVal name As String) As Object
        Dim result As Object = Nothing

        Select Case name.ToLower
            Case "nusercode"
                result = Usercode
            Case "ssche_code"
                result = Schema
            Case "nshemelevel"
                result = SecurityLevel
            Case "vt_theme" 'Excluir...
            Case Else
                Throw New Exception(String.Format("Service Enviroment value not found for '{0}'", name))
        End Select
        Return result
    End Function

End Class
