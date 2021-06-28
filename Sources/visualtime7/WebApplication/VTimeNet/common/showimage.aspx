<%@ Page LANGUAGE="VB" explicit="true" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

Dim lclsImage As eGeneralForm.Images


</script>
<%lclsImage = New eGeneralForm.Images

'+ Limpia la informacion en la cabecera HTTP 
With Response
	.Expires = 0
	.Buffer = True
	.Clear()
	
	'+ Cambia la cabecera para reflejar que una imagen a sido pasada
	.ContentType = "image/gif"
End With

With lclsImage
	If .Find(CInt(Request.QueryString.Item("nImagenum")), CInt(Request.QueryString.Item("nConsec")), Nothing, True) Then
		Response.binarywrite(.iImage)
	End If
	Response.End()
End With
lclsImage = Nothing
%>




