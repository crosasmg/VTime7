<%@ Page explicit="true" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">


Dim objMem As Object
Dim objTest As ePolicy.ValPolicySeq
Dim numCount As Object
Dim k As Integer
Dim numMemAvail As String
Dim numPreMemAvail As String
Dim blnDoNothing As String


</script>
<html>
<head>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<style>
    BODY        {
                    background-color: gray;
                    font-family:'Verdana';
                    font-size:8pt;
                }
    H2          {
                    color: red;
                    font-size:10pt;
                }
    P           {
                    margin: 10pt
                }
    TH          {
                    border-top: 2 solid navy;
                    border-bottom: 2 solid navy;
                    font-size:8pt;
                }
    TD          {   
                    font-family:'Verdana';
                    font-size:8pt;
                    background-color: lightcyan;
                } 
	TD.sel		{   
	                background-color:ivory;
	            }
	TD.unsel	{   
	                background-color:lightblue
	            }
    .numeric    {
                    text-align: right;
                }       	            
	            
</style>
</head>
<body>
<form name="PerfTest" METHOD="POST" ACTION="MemAvail.aspx">
<label title="Cantidad de veces a ejecutar el ciclo">Repeticiones:&nbsp;</label>
<input type="text" id="text1" name="nCount">
<input type="checkbox" id="checkbox1" name="bNothing" value="1"><label title="Liberar objeto antes de crear uno nuevo">Hacer Set Nothing</label>
<p>
<table BORDER="1" WIDTH="60%">
<tr><th WIDTH="30%">Iteración</th><th Title="Memoria disponible tras realizar asignacion">Mem Avail</th>
<%
numCount = Request.Form.Item("nCount")
blnDoNothing = Request.Form.Item("bNothing")

If numCount <> vbNullString Then
	
'UPGRADE_NOTE: The 'appDebug.Memory' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	objMem = Server.CreateObject("appDebug.Memory")
	For k = 1 To numCount
		objTest = New ePolicy.ValPolicySeq
		
		numMemAvail = objMem.AvailableMemory
		If numMemAvail <> numPreMemAvail Then
			numPreMemAvail = numMemAvail
			Response.Write("<tr><td class=numeric>" & k & "</td><td class=numeric>" & numMemAvail & "</td></tr>")
		End If
		
		If blnDoNothing = "1" Then
			'UPGRADE_NOTE: Object objTest may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
			objTest = Nothing
		End If
	Next 
	'UPGRADE_NOTE: Object objMem may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	objMem = Nothing
End If
%>
</table>
</p>
</body>
</html>





