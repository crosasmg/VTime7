<%@ Page explicit="true" %>
<script language="VB" runat="Server">

Dim objMem As Object
Dim dtmStart As Single
Dim nDuration As Object


</script>
<html>
<head>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<style>
    BODY        {
                    background-color: lightcyan;
                    font-family:'Verdana';
                    font-size:8pt;
                }
    H1          {
                    color: red;
                    font-size:10pt;
                    text-align: center;
                }
    H2          {
                    color: blue;
                    font-size:8pt;
                }

    P           {
                    margin: 20pt;
                }
    P.Desc      {
                    border: solid blue 1px;
                    background-color: lightblue;
                }                
	LABEL       {
	                color: red;
	                margin: 10pt;
	            }            
</style>
</head>
<body>
<form name="PerfTest" METHOD="POST" ACTION="EndLessDB.aspx">

<H1>Ejecución de procedimiento en base de datos</H1>
<HR>
<P CLASS=Desc>Se mantiene ejecutando procedimiento DBG_ENDLESS la cantidad de centésimas de segundo indicado</P>

<p>
<%

If Request.Form.Item("btnSend") = "Ejecutar" Then
	
	dtmStart = Microsoft.VisualBasic.Timer()
	
	
	
'UPGRADE_NOTE: The 'appDebug.Process' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	objMem = Server.CreateObject("appDebug.Process")
	'Set objMem = Server.CreateObject("eErrors.Error")
	'If objMem.Add("fmendoza", "fmendoza") then
	'    Response.Write "<B>oK</B>"
	'eLSE
	'    Response.Write "<B>Error</B>"
	'end if
	If IsNumeric(Request.Form.Item("tcnDuration")) Then
		nDuration = Request.Form.Item("tcnDuration")
	Else
		nDuration = 100
	End If
	If objMem.EndLessDBProcess(nDuration) Then
		Response.Write("<B>Tiempo de proceso en DB:</B>" & Microsoft.VisualBasic.Timer() - dtmStart)
	Else
		Response.Write("<B>Error en proceso:" & nDuration & "</B>")
	End If
	
	'UPGRADE_NOTE: Object objMem may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	objMem = Nothing
	Response.Write("<BR><INPUT TYPE='submit' VALUE='Volver' NAME='btnSend'>")
	
Else
	Response.Write("<LABEL class='Label' name=tcnDuration>Sec/100</LABEL>&nbsp;")
	Response.Write("<INPUT type='text' TITLE='Cantidad de centesimas de segundo a ejecutar proceso' name=tcnDuration><BR>")
	Response.Write("<INPUT TYPE='submit' VALUE='Ejecutar' NAME='btnSend'>")
End If
%>
</p>
</body>
</html>





