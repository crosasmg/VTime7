<script language="VB" runat="Server">
    'AddJsonEntry:
    Private Function AddJsonEntry(skey As String, sValue As String, Optional bIsLastElement As Boolean = False) As String
        Dim rv As String
        rv = """" & skey.Trim() & """:""" & Server.HtmlEncode(sValue.Trim()) & """"
        If Not bIsLastElement Then
            rv &= ","
        End If
        Return rv
    End Function
</script>




