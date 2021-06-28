<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.21
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Variables para almacenar parametros
Dim mstrBranch As Object
Dim mstrProduct As Object
Dim mstrPolicy As Object
Dim mstrCertif As Object
Dim mstrStartdate As Object
Dim mhddPolicy As Object
Dim mstrTransaction As Object


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI7502_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "VI7502_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = Request.QueryString("nMainAction") = 401

'+ Se cargan datos de parametros
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tmenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:37 $"

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}
//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
//%InsChangeLabel: Cambia el label de poliza/certificado a propuesta/certificado
//--------------------------------------------------------------------------------------------
function InsChangeLabel(Value){
//--------------------------------------------------------------------------------------------
	if (Value==2){
		UpdateDiv('lblPolizaPropuesta','Póliza');
	}
	else{
		UpdateDiv('lblPolizaPropuesta','Propuesta');
	}
}
//% ShowPoliza: Se encarga de validar el tipo de Póliza
//--------------------------------------------------------------------------------------------
function ShowPoliza(){
//--------------------------------------------------------------------------------------------
	insDefValues('ValPolitype', "nBranch=" + self.document.forms[0].cbeBranch.value + "&nProduct=" + self.document.forms[0].valProduct.value + "&nPolicy=" + self.document.forms[0].tcnPolicy.value);
 }
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu(Request.QueryString("sCodispl"), Request.QueryString("sCodispl") & ".aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
	'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" NAME="frmPolicyHisQ" ACTION="ValPolicytra.aspx?sMode=2">
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0>Tipo de información</LABEL></TD>
			<TD><%=mobjValues.ComboControl("cbeCertype", "1|Propuesta,2|Póliza",  ,  ,  ,  , "InsChangeLabel(this.value);")%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0>Ramo</LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", "Ramo al que pertenece la póliza a consultar",  ,  ,  ,  ,  , "InsChangeField();")%></TD>

        </TR>
        <TR>
			<TD><LABEL ID=0>Producto</LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", "Producto")%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL><DIV ID="lblPolizaPropuesta">Propuesta/Póliza</DIV></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 10, "",  , "Número de la propuesta o póliza que se desea consultar", False, 0)%></TD>

        </TR>
        <TR>
			<TD><LABEL ID=0>Contratante</LABEL></TD>
            <TD><%=mobjValues.ClientControl("tctClient", "",  , "Contratante",  , False)%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL>Saapv</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCod_saapv", 10, "",  , "Número de de Saapv que se desea consultar", False, 0)%></TD>

        </TR>

    </TABLE>
</FORM> 
</BODY>
</HTML>
<%
'Se agrega validación para identificar si es llamada por navegacion o 
'desde el menú principal.

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.21
Call mobjNetFrameWork.FinishPage("VI7502_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




