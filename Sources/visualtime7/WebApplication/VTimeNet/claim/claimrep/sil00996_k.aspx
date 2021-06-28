<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjNetFrameWork As eNetFrameWork.Layout
Dim sCodispl As String
Dim sCodisplPage As String


</script>
<%
sCodispl = Trim(Request.QueryString("sCodispl"))
sCodisplPage = LCase(sCodispl) & "_k"

'Response.write sCodispl & "  " & sCodisplPage : response.end

Response.Expires = -1441

'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(sCodisplPage)
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = sCodisplPage
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<HTML>
<HEAD>
<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<!--SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT-->

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{		    
	return true;
}

//%insDefValue:Permite asignarle "0,00" al control en caso de no haber indicado
//%valor numerico al campo
//------------------------------------------------------------------------------------------
function insDefValue(Field){
//------------------------------------------------------------------------------------------
//    if(Field.value=='')
//        self.document.forms[0].tcnExcess.value='0'
}

function f_exportar(FormatType)
{	
	if (Validaciones())
	{
		document.frmClaimExcessAmount.FormatType.value = FormatType;
		document.frmClaimExcessAmount.target="nueva";
		document.frmClaimExcessAmount.action="exportar.aspx?sCodispl=<%=sCodispl%>";
		document.frmClaimExcessAmount.submit();
	}
}


function Validaciones()
{
	obj = document.frmClaimExcessAmount.tcdIniDate

	if (obj.value == '')
	{
		alert('El parámetro Fecha Hasta es obligatorio.');
		return false;
	}	

	fecha = new String(obj.value)
	if(obj.value!='' && !EsFecha(fecha,'dma'))
	{
		alert('Debe ingresar una fecha correcta.');
		obj.select();		
		obj.focus();		
		return false;
	}

	obj = document.frmClaimExcessAmount.tcdFinDate

	if (obj.value == '')
	{
		alert('El parámetro Fecha Hasta es obligatorio.');
		return false;
	}	
	
	fecha = new String(obj.value)
	if(obj.value!='' && !EsFecha(fecha,'dma'))
	{
		alert('Debe ingresar una fecha correcta.');
		obj.select();		
		obj.focus();		
		return false;
	}
	return true;
}


</SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%
Response.Write(mobjValues.StyleSheet())

'Response.Write mobjMenu.MakeMenu(sCodispl,sCodisplPage & ".aspx",1, Request.QueryString("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code"))

'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmClaimExcessAmount" ACTION="VALCLAIMREP.ASPX?mode=1">
	<input type="hidden" name="FormatType" value=""/>
    <BR></BR>
    <TABLE>
		<TR>
			<td height="28" align="right">
				<table width="100%" border="0" cellspacing="0" cellpadding="0">
					<tr>
						<td width="60%" align="left">
							<img onClick="javascript:f_exportar('PDF')" style="cursor: hand;" src="/VTimeNet/images/ic_pdf.gif" alt="Exportar a PDF" border="0"/>
							<img onClick="javascript:f_exportar('XLS')" style="cursor: hand;" src="/VTimeNet/images/ic_xls.gif" alt="Exportar a Excel" border="0"/>
							<img onClick="javascript:f_exportar('DOC')" style="cursor: hand;" src="/VTimeNet/images/ic_doc.gif" alt="Exportar a Word" border="0"/>
							<img src="/VTimeNet/images/block.gif" width="50" height="1"/>							
						</td>
					</tr>
				</table>
			</td>
            <TD COLSPAN="2" CLASS="HighLighted">Periódo a consultar</TD>
		</TR>
		<TR>
			<TD COLSPAN="4" CLASS="HorLine"></TD>
			<TD COLSPAN="4"></TD>	
		</TR>
		<TR>
            <TD><LABEL ID="8704">Fecha Desde: </LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdIniDate",  ,  , "Fecha desde la cual se desea listar los siniestros")%></TD>
			<TD COLSPAN="4">&nbsp</TD>	
        </TR>
        <TR>
            <TD><LABEL ID="8703">Fecha Hasta:</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdFinDate",  ,  , "Fecha hasta la cual se desea listar los siniestros")%></TD>
			<TD COLSPAN="4">&nbsp</TD>	        
		</TR>
		<TR>
			<TD COLSPAN="6">&nbsp</TD>	        
		</TR>			
	</TABLE>
</FORM>
</BODY>
</HTML>
<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.14
'Call mobjNetFrameWork.FinishPage(sCodisplPage)
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




