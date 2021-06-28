<%@ Page Language="VB" explicit="true" %>
<script language="VB" runat="Server">


'---------------------------------------------------------------------------------------------
Private Function Dummy() As Object
	'---------------------------------------------------------------------------------------------
	Dim shora As String
	Dim lrecreaTest As Object
	
	Session("nusercode") = "9933"
	'Session("sInitials") = "insudb"
	'Session("sAccessWO") = "QŒ€úÍk(" 'Password encriptada de Insudb en QCTIME
	Session("sInitials") = "enicholls"
	Session("sAccessWO") = "FMÛã¬ö•„" 'Password encriptada de Insudb en QCTIME
	
'UPGRADE_NOTE: The 'eTimeOracle.StoredProcedure' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lrecreaTest = server.CreateObject("eTimeOracle.StoredProcedure")
	
	With lrecreaTest
		.StoredProcedure = "reaNada"
		.Parameters.Add("instr", "HOla", 1, 200, 14, 0, 0, 64)
		If .Run Then
			shora = CStr(.Fields("stime"))
			.RCloseRec()
		End If
	End With
	Response.Write(shora)
	'UPGRADE_NOTE: Object lrecreaTest may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lrecreaTest = Nothing
	
	
	
End Function

</script>
<%Response.Expires = -1
Response.Buffer = True

Call Dummy()

%>





