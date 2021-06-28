<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">
'Dim LoadPageInSequence() As Object
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.02
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjWorksheet As Object
Dim mobjBatch As eBatch.ValBatch
Dim mdEffecdate As Object


'% LoadHeader: se cargan los datos para la secuencia
'--------------------------------------------------------------------------------------------
Private Sub LoadHeader()
	'--------------------------------------------------------------------------------------------
	Dim lblnDisabled As Boolean
	
	'+ En caso que la ventana sea llamada desde otra transacción
	If Request.QueryString.Item("sLinkSpecial") = "CA658" Then
		Session("sLinkSpecial") = Request.QueryString.Item("sLinkSpecial")
		mdEffecdate = Session("dEffecdate")
		Session("sTypeage") = Request.QueryString.Item("sTypeage")
	Else
		Session("sLinkSpecial") = ""
		mdEffecdate = vbNullString
	End If
	
	
	If Request.QueryString.Item("sLinkSpecial") = vbNullString Then
		lblnDisabled = False
	Else
		lblnDisabled = True
	End If
	
	Response.Write(mobjValues.HiddenControl("hdtFileName", ""))
	
Response.Write("" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Ruta del archivo"">Ruta del archivo</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD CLASS=""HorLine"" COLSPAN=""5""></TD>" & vbCrLf)
Response.Write("		</TR>        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1""><LABEL ID=0>Archivo</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">")


Response.Write(mobjValues.FileControl("tctFile", 45))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("")

	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("BCL804_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.02
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "BCL804_k"
mobjBatch = New eBatch.ValBatch

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Constantes.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTimeNet/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>


<SCRIPT LANGUAGE= "JavaScript">

var nContent
var message
var sRoot

message =<%Response.Write("""Se procesara " & mobjValues.getMessage(Session("nAction"), "Table5578") & """;")%>

//- Variable para el control de versiones
    document.VssVersion="$$Revision:   1.1  $|$$Date:   Mar 11 2005 10:24:34  $|$$Author:   crsepulv  $"

//% insStateZone: se manejan los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return(true);
}

//% insRefresh: Refresca los valores de la Pagina
//--------------------------------------------------------------------------------------------
function insRefresh(){
//--------------------------------------------------------------------------------------------
	return(true);
}

//%insShowNextWindow. Se encarga de mostrar la siguiente ventana a ser mostrada
//--------------------------------------------------------------------------------------------
function insShowNextWindow(){
//--------------------------------------------------------------------------------------------
	var lblnDoIt=true;
	
	if (typeof(top.frames['fraSequence'])!='undefined')
	    if (typeof(top.frames['fraSequence'].NextWindows)!='undefined'){
			top.frames['fraSequence'].NextWindows('');
			lblnDoIt = false;
	    }
	    
	if (lblnDoIt) setTimeout('insShowNextWindow()',50)
}


//% ChangeAction: Se controla las controla las restricciones
//--------------------------------------------------------------------------------------------
function ChangeAction(Field){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		chkRest.value="";
		chkRest.checked=false;
		if (cbeAction.value=="4")
			chkRest.disabled=true;
        else
			chkRest.disabled=false;	
	}
}
//%insChangeField : Cambia valores de campos dependientes
//--------------------------------------------------------------------------------------------
function insChangeField(objField){
//--------------------------------------------------------------------------------------------
    var frm = self.document.forms[0]
    var lintOpt
    
    switch(objField.name){
    case 'tcnPolicy':
        if (frm.optType[0].checked) lintOpt = '3'
        else if (frm.optType[1].checked) lintOpt = '1'
             else if (frm.optType[2].checked) lintOpt = '2';
             
        insDefValues('PolicyDate', 'sCertype=' + lintOpt + 
                                   '&nBranch='+ frm.cbeBranch.value +
                                   '&nProduct='+ frm.valProduct.value +
                                   '&nPolicy='+ frm.tcnPolicy.value,
                     '/VTimeNet/policy/policyrep/');
        break;
    }            

}

//% ChangeChecked: Se controla el valor de las restricciones
//--------------------------------------------------------------------------------------------
function ChangeChecked()
//--------------------------------------------------------------------------------------------
{
	if(self.document.forms[0].elements["chkChange1"].checked)
	{
		self.document.forms[0].elements["chkChange1"].value = 1;
		self.document.forms[0].elements["hddChkPolicy"].value = 1;
	}		
	else
	{
		self.document.forms[0].elements["chkChange1"].value = 0;
		self.document.forms[0].elements["hddChkPolicy"].value = 0;
	}

	if(self.document.forms[0].elements["chkChange2"].checked)
	{
		self.document.forms[0].elements["chkChange2"].value = 1
		self.document.forms[0].elements["hddChkReceipt"].value = 1
	}
	else
	{
		self.document.forms[0].elements["chkChange2"].value = 0;
		self.document.forms[0].elements["hddChkReceipt"].value = 0;
	}
		
    if(self.document.forms[0].elements["chkChange2"].checked==false &&
       (self.document.forms[0].elements["chkChange1"].checked==false))
    {
        alert('Al menos unas de las dos opciones de "Cambio de vía de cobro de la póliza" debe estar seleccionada');
        self.document.forms[0].elements["chkChange1"].checked=true;
    }
}

//%insFinish. Esta función es utilizada para realizar cambios al momento de finalizar la transacción
//--------------------------------------------------------------------------------    
function insFinish(){
//--------------------------------------------------------------------------------    
    return(true);
}

function ChangeValue(valor)
{
	self.document.forms[0].elements["hddReport"].value=valor;
}
</SCRIPT>    
    <%mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("BCL804", "BCL804_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjValues.WindowsTitle("BCL804"))
End With
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
'
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="BCL804" ACTION="valBCL804.aspx?sMode=1" ENCTYPE="multipart/form-data">
<P>&nbsp;</P>
<%If Request.QueryString.Item("sConfig") = "InSequence" Then
	'Call LoadPageInSequence()
Else
	Call LoadHeader()
End If
%>
</FORM>
</BODY>
</HTML>
<%

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjBatch may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjBatch = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.02
Call mobjNetFrameWork.FinishPage("BCL804_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




