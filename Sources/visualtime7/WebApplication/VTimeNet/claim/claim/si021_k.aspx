<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.12
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si021_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si021_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

Session("SI007_Codispl") = ""
%>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Constantes.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tmenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<SCRIPT>
//- Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 12.31 $|$$Author: Nvaplat60 $"

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
//%insCertificat : Deja certificado con cero y deshabilitado.
//--------------------------------------------------------------------------------    
function insCertificat(Field){
//------------------------------------------------------------
	if(Field.value != "")
		insDefValues('Policy_CA099', 'nBranch='+self.document.forms[0].cbeBranch.value+'&nProduct='+self.document.forms[0].valProduct.value+'&nPolicy='+self.document.forms[0].tcnPolicy.value+'&nCertif='+self.document.forms[0].tcnCertif.value, '/VTimeNet/Policy/PolicyTra')
}

//%insPolicy : Deja certificado con cero y deshabilitado.
//--------------------------------------------------------------------------------    
function insPolicy(Field){
//------------------------------------------------------------
with (self.document.forms[0]){
if(Field.value != "")
	tcnPolicy.disabled = false;		
else{
    tcnPolicy.value='';
	tcnPolicy.disabled = true;}
	}	
}
</SCRIPT>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("SI021", Request.QueryString("sWindowDescript")))
	.Write(mobjMenu.MakeMenu("SI021", "SI021_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
End With
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmOrderCompleted" ACTION="ValClaim.aspx?smode=1">
    <BR>
    <TABLE WIDTH="100%" BORDER=0>
        <TR>        
            <TD>
               <LABEL ID=0>Profesional</LABEL></TD>
            <TD>
            <%With mobjValues
	.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nTypeProv", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valProvider", "tabTab_provider", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.strnull), True,  ,  ,  ,  ,  ,  ,  , "Código del profesional asociado a las órdenes de servicios"))
End With
%></TD>
		</TR>
		<TR>
			<TD WIDTH=15% ><LABEL ID=0>Ramo</LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", "Ramo al que pertenece la póliza o certificado de la orden de servicio",  ,  ,  ,  ,  ,  , False)%> </TD>
			<TD> <LABEL ID=0>Producto</LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", "Producto al que pertenece la póliza o certificado a la que se desea emitir una orden de servicio",  ,  , True,  ,  ,  ,  , "insPolicy(this)")%></TD>
		<TR>
			<TD WIDTH=15% ><LABEL ID=0>Póliza</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 8,  ,  , "Número de la póliza a la que se asocia la orden de servicio.",  , 0,  ,  ,  , "insCertificat(this)", True)%></TD>
		    <TD><LABEL ID=0>Certificado</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCertif", 4,  ,  , "Número del Certificado al que se asocia la orden de servicio",  , 0,  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0>Propuesta</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnProponum", 8,  ,  , "Número de la propuesta a la que se asocia la orden de servicio",  , 0)%></TD>
            <TD WIDTH=15% ><LABEL ID=0>Siniestro </LABEL> </TD>
            <TD> <%=mobjValues.NumericControl("tcnClaim", 10,  ,  , "Número de siniestro al que se asocia la orden de servicio",  , 0)%></TD>
        <TR>
        </TR>
            <TD><LABEL ID=0>Sucursal</LABEL></td>
			<TD><%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , "Sucursal a la que pertenece el profesional, asignado a la orden de servicio")%></TD>

            <TD><LABEL ID=0>Tipo de inspección</LABEL></td>
			<TD><%=mobjValues.PossiblesValues("cbeOrderType", "Table7100", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , "Tipo de inspección asociado a la orden de servicio")%></TD>
        <TR>
            <TD WIDTH=15% ><LABEL ID=0>Estado de las órdenes</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeStatus_ord", "Table215", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , "Estado de las órdenes que se desea tratar, ya sea para consultarlas o para darlas por realizadas")%></TD>
			<TD><LABEL ID=0>Fecha de planificación</LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdFec_prog", CStr(eRemoteDB.Constants.dtmnull),  , "Fecha en la que se planifica o espera realizar la orden de servicio.")%></TD>
        </TR>
    </TABLE>
<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.12
Call mobjNetFrameWork.FinishPage("si021_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




