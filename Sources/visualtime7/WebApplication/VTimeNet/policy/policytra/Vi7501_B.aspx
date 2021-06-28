<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSaapv" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.14
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

Dim mclsSaapv As eSaapv.Saapv


'% insPreVI7501_E: Realiza la lectura de los campos a mostrar en pantalla
'---------------------------------------------------------------------
Private Sub insPreVI7501_B()
	'---------------------------------------------------------------------
	Call mclsSaapv.Find_employ(mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble), CStr(Session("sCertype_saapv")), mobjValues.StringToType(CStr(Session("nBranch_saapv")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nProduct_saapv")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nPolicy_saapv")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCertif_saapv")), eFunctions.Values.eTypeData.etdDouble), "", mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdLong))
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI7501_B")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mclsSaapv = New eSaapv.Saapv
mobjValues.ActionQuery = Session("bQuery")
Call insPreVI7501_B()
%>
<SCRIPT>
// % InsChangeClient: Despliega los datos del cliente
//-------------------------------------------------------------------------------------------
function InsChangeClient(){
//-------------------------------------------------------------------------------------------
   insDefValues('ClientVI7501_B', "sClient=" + self.document.forms[0].tctClient.value, '/VTimeNet/Policy/Policytra');
}
</SCRIPT>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
    <%Response.Write(mobjMenu.setZone(2, Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmVI7501_B" ACTION="valVI7501tra.aspx?nMainAction=301&nHolder=1">
	<%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
    <BR><BR>
    <TABLE WIDTH="100%">
  	    <TR>
				<TD><LABEL ID=0>Empleador</LABEL></TD>
			    <TD COLSPAN="4"><%=mobjValues.ClientControl("tctClient", mclsSaapv.sclient_employer,  , "Rut del empleador", "InsChangeClient(this)",  , "x",  ,  ,  ,  ,  ,  , True)%></TD>
	    </TR>
		<TR>	
				<TD><LABEL ID=0>Razón social</LABEL></TD>
	            <TD WIDTH="25%"> <%=mobjValues.TextControl("tctlegalname", 60, mclsSaapv.slegalname, True, "Nombre legal",  ,  ,  ,  , True)%> </TD>
	            <TD >&nbsp</TD>
	            <TD >&nbsp</TD>
 	    </TR>
		<TR>	
				<TD><LABEL ID=0>Dirección comercial</LABEL></TD>
	            <TD WIDTH="25%"> <%=mobjValues.TextControl("tctdescadd", 60, mclsSaapv.sdescadd, True, "Glosa de dirección comercial",  ,  ,  ,  , True)%> </TD>
                <TD >&nbsp</TD>
                <TD >&nbsp</TD>	            	            
		</TR>
		<TR>	
				<TD><LABEL ID=0>Comuna</LABEL></TD>
				<TD><%=mobjValues.PossiblesValues("cbeMunicipality", "tabmunicipality", 1, CStr(mclsSaapv.nmunicipality),  , False,  ,  ,  ,  , True,  , "Comuna")%></TD>											
				<TD><LABEL ID=0>Provincia</LABEL></TD>
				<TD><%=mobjValues.PossiblesValues("cbeLocal", "tabTab_locat", 1, CStr(mclsSaapv.nlocal),  , False,  ,  ,  ,  , True,  , "Provincia")%></TD>										
 	     </TR>
  			 <TR>	
				<TD><LABEL ID=0>Región</LABEL></TD>
				<TD><%=mobjValues.PossiblesValues("cbeProvince", "Tab_Province", 1, CStr(mclsSaapv.nprovince),  , False,  ,  ,  ,  , True,  , "Región")%></TD>											
				<TD >&nbsp</TD>
				<TD >&nbsp</TD>
				
		</TR>
		<TR>
 		    <TD COLSPAN="5" CLASS="HighLighted"><LABEL><A NAME="Direccion">Datos receptor (Encargado RRHH)</A></LABEL></TD>
			</TR>
			<TR>
			  <TD WIDTH="100%" COLSPAN="5"><HR></TD>
			</TR> 
		
		</TR>
		<TR>
				<TD><LABEL ID=0>Nombre</LABEL></TD>
	            <TD WIDTH="25%"> <%=mobjValues.TextControl("tctname", 60, mclsSaapv.sRrhh_name, True, "Nombre del encargado de recursos humanos")%> </TD>
                <TD >&nbsp</TD>	            
                <TD >&nbsp</TD>
		</TR>
		<TR>	            
				<TD><LABEL ID=0>Mail</LABEL></TD>
	            <TD WIDTH="25%"> <%=mobjValues.TextControl("tctse_mail", 60, mclsSaapv.sRrhh_email, True, "Mail del encargado de recursos humanos")%> </TD> 			
                <TD >&nbsp</TD>	            
                <TD >&nbsp</TD>                
		</TR>
		<TR>
				<TD><LABEL ID=0>Teléfono</LABEL></TD>
	            <TD WIDTH="25%"> <%=mobjValues.TextControl("tctphone", 17, mclsSaapv.sRrhh_phone, True, "Teléfono del encargado de recursos humanos")%> </TD>
                <TD >&nbsp</TD>
                <TD >&nbsp</TD>	                            	            
		</TR>
 			<TR>	            
			    <TD><LABEL ID=0>Fecha de recepción</LABEL></TD>
				<TD> <%=mobjValues.DateControl("tcdRecepdat", mobjValues.TypeToString(mclsSaapv.dRecepdat, eFunctions.Values.eTypeData.etdDate),  , "Fecha de recepción de SAAPV")%> </TD>
                <TD >&nbsp</TD>				
				<TD >&nbsp</TD>                
			</TR> 
    </TABLE>

<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
'UPGRADE_NOTE: Object mclsSaapv may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsSaapv = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.14
Call mobjNetFrameWork.FinishPage("VI7501_B")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




