<%@ Page explicit="true" %>
<%@ Import namespace="eErrors" %>
<script language="VB" runat="Server">

Dim objMem As eErrors.Err_Comp


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
<form name="PerfTest" METHOD="Get" ACTION="InsertDB.aspx">
<label title="Cantidad de veces a ejecutar el ciclo">Creacion de registros en Err_Comp</label>
<input type="checkbox" name="chkSend" value="1">
<br>
<input type="submit" value="Submit" id="submit1" name="submit1">
<p>
<%'  If Request.Form("chkSend") = "1" Then
objMem = New eErrors.Err_Comp

'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
Call objMem.InsPostER005("Add", 1, 0, 1, "err perf", "", 1, Today, Today, 666)

'UPGRADE_NOTE: Object objMem may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
objMem = Nothing

'  End If
%>
</p>
</body>
</html>





