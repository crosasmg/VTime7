<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjValues As eFunctions.Values


'%InsShowWindow: Muestra la ventana para ver el archivo log
'--------------------------------------------------------------------------------
Sub InsShowWindow()
	'--------------------------------------------------------------------------------
	If Request.QueryString.Item("sDisplay") = "1" Then
		mobjValues = New eFunctions.Values
		Response.Write(mobjValues.StyleSheet())
	Else
		Response.Write("<SCRIPT>ShowPopUp(self.document.location.href + ""&sDisplay=1"" ,""LetterErrors"",600,270);</" & "Script>")
		Response.End()
	End If
	
Response.Write("" & vbCrLf)
Response.Write("<BODY>" & vbCrLf)
Response.Write("<H1><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & " " & GetLocalResourceObject("Anchor2Caption") & "</LABEL></H1>" )
Response.Write("<HR>" )
Response.Write("<LABEL ID=0>" & GetLocalResourceObject("tctPageCaption") & ":</LABEL>")
Response.Write("<LABEL>&#160;" & Request.QueryString.Item("nErrorNum") & "</LABEL><BR><BR>")
Response.Write("<LABEL>Fecha : " & DateTime.Now & "</LABEL><BR><BR>")
'Response.Write("<LABEL ID=0>Página: " & Request.QueryString.Item("sPage") & "</LABEL><BR><BR>")
Response.Write("<TABLE WIDTH=""100%"" border=""0"">")
        Response.Write("<TR><TD><LABEL ID=0>") '<a href=""JAVASCRIPT:ShowPopUp('")
        'Response.Write(Request.QueryString.Item("sErrURL"))
        'Response.Write("','ErrorsLog', '700', '350', 'yes', 'no', '10','10');insCancel('1');"">Ver detalles</a></LABEL></TD></TR>" & vbCrLf)
        

        'Response.Write("<TR><TD><LABEL ID=0><a href=""JAVASCRIPT:insGoBack();insCancel('2')"">" & GetLocalResourceObject("Anchor3Caption") & "</a></LABEL></TD></TR>" & vbCrLf)
        Response.Write("<!--<TR><TD><LABEL ID=0><a href=""JAVASCRIPT:insCancel('2');document.location.href='")

        Response.Write(Application("ContactUs"))
        
        Response.Write("'"">Enviar a soporte</a></LABEL></TD></TR>-->" & vbCrLf)
        
        'Response.Write("<TR><TD><LABEL ID=0><a href=""JAVASCRIPT:ShowPopUp('/VTimeNet/errors/ermenu.aspx', 'Errors', 300, 180,'yes');insCancel()"">" & GetLocalResourceObject("Anchor4Caption") & "</a></LABEL></TD></TR>" & vbCrLf)
        Response.Write("<TR><TD><LABEL ID=0><a href=""JAVASCRIPT:insGoTo('ER001');insCancel()"">" & GetLocalResourceObject("Anchor4Caption") & "</a></LABEL></TD></TR>" & vbCrLf)
        Response.Write("<TR ALIGN = RIGHT><TD>")

	Response.Write(mobjValues.ButtonAcceptCancel( , "insCancel()",  ,  , 2))
        Response.Write("</TD></TR>" & vbCrLf)
        Response.Write("</TABLE>" & vbCrLf)
        
        Response.Write("</BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR></BR>" & vbCrLf)
        Response.Write("<TR><TD><LABEL ID=0><a href=""JAVASCRIPT:ShowPopUp('")
        Response.Write(Request.QueryString.Item("sErrURL"))
        Response.Write("','ErrorsLog', '700', '350', 'yes', 'no', '10','10');insCancel('1');"">Ver Detalles Ocultos :( </a></LABEL></TD></TR>" & vbCrLf)
        
Response.Write("</BODY>")

	
End Sub

'%InsShowMessage: Muestra un mensaje alert
'--------------------------------------------------------------------------------
Sub InsShowMessage(ByRef nError As Integer)
	'--------------------------------------------------------------------------------
	Dim lclsError As eFunctions.Errors
	lclsError = New eFunctions.Errors
	With Response
		.Write(lclsError.ErrorMessage("GE1000", nError,  ,  ,  , True))
		
		.Write("<SCRIPT>")
		.Write("var lstrURL1;")
		.Write("var lstrURL2;")
		.Write("var lstrURL;")
		.Write("var lstrReload;")
		.Write("var lstrPut;")
		.Write("var lstrLen;")
		.Write("var lstrFrame;")
		.Write("var lstrWindow;")
		
		If InStr(1, Request.QueryString.Item("sPage"), "Type=PopUp", CompareMethod.Binary) > 0 Then
			.Write("lstrWindow = top.opener.top;")
		Else
			.Write("lstrWindow = top;")
		End If
		
		.Write("if(lstrWindow.fraSequence.pintZone=='1')")
		.Write("    lstrFrame = 'fraHeader';")
		.Write("if(lstrWindow.fraSequence.pintZone=='2')")
		.Write("    lstrFrame = 'fraFolder';")
		
		.Write("lstrURL = lstrWindow.frames[lstrFrame].location.href;")
		.Write("lstrPut = lstrWindow.frames[lstrFrame].location.href.indexOf('Reload=');")
		
		.Write("if(lstrPut!='-1'){")
		.Write("    lstrLen = lstrWindow.frames[lstrFrame].location.href.length;")
		.Write("    lstrURL1 = lstrWindow.frames[lstrFrame].location.href.substr(0,lstrPut);")
		.Write("    lstrURL2 = lstrWindow.frames[lstrFrame].location.href.substr(lstrPut+7,lstrLen);")
		.Write("    lstrReload = lstrWindow.frames[lstrFrame].location.href.substr(lstrPut,7);")
		.Write("    lstrURL = lstrURL1 + 'Reload=' + lstrURL2;")
		.Write("}")
		
		.Write("    lstrWindow.frames[lstrFrame].location.href = lstrURL;")
		.Write("</" & "Script>")
		
	End With
	lclsError = Nothing
End Sub

</script>
<%
Response.Expires = -1

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Author: Iusr_llanquihue $|$$Revision: 1 $|$$Date: 2/09/03 19:02 $"


//%insGoBack: Reintentar la ejecución de la última página
//--------------------------------------------------------------------------------
function insGoBack(){
//--------------------------------------------------------------------------------
    opener.document.location.href = "<%=Request.QueryString.Item("sPage")%>";
    window.close();
}

//%insCancel: Se ejecuta cuando se cancela la pàgina
//--------------------------------------------------------------------------------
function insCancel(sCloseW){
//--------------------------------------------------------------------------------
    if (typeof(sCloseW)=='undefined')
		sCloseW="1"
    if (typeof(opener.top.fraHeader) != 'undefined')  
    if (opener.top.fraHeader.document.location.href.indexOf("InSequence")>=0){
        opener.top.fraHeader.insHandImage("A390", true);
        opener.top.fraHeader.insHandImage("A391", true);
        opener.top.fraHeader.insHandImage("A392", true);
        opener.top.fraHeader.insHandImage("A393", true);
    }
    if (sCloseW=="1"){
//        opener.top.close();
        window.close()
    }
}

function insGoTo(RefUrl){
    open('/VTimeNet/Common/GoTo.aspx?sCodispl=' + RefUrl, 'ErrorsWindow', 'toolbar=no,resizable=no,location=no,directories=no, status=yes,menubar=no,copyhistory=no,width=780,height=450,left=20,top=20');
    top.close();

}

</SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
Select Case Request.QueryString.Item("nErrorOracle")
	Case "2292"
		InsShowMessage((100011))
	Case Else
		InsShowWindow()
End Select
%>  
</HEAD>
</HTML>
<%
    
mobjValues = Nothing
%>





