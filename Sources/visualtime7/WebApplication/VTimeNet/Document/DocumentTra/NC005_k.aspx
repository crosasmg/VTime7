<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mstrCodisplOri As Object
    Dim mstrCertype As Object
Dim mobjRequest As Object
Dim mstrRehab_notrec As Object


</script>
<%'UPGRADE_NOTE: The 'eFunctions.Values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
    
    mobjValues = New eFunctions.Values
    
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "NC005_K"
Response.Expires = -1441
'UPGRADE_NOTE: The 'eNetFrameWork.Layout' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
mobjNetFrameWork = new  eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("NC005_k")

%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 15 $|$$Date: 14/06/05 13:01 $|$$Author: Nvaplat28 $"
</SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tmenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>

    <%
'UPGRADE_NOTE: The 'eFunctions.Menues' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
        mobjMenu = New  eFunctions.Menues
        
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("NC005", "NC005_K.aspx", 1, vbNullString))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>

<SCRIPT>

//- Variable para el control de versiones
	document.VssVersion="$$Revision: 15 $|$$Date: 14/06/05 13:01 $"
	
//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------

	self.document.forms[0].elements[0].disabled = false;
	self.document.forms[0].elements[1].disabled = false;
	
	if (self.document.forms[0].elements[0].checked)
	{
		ShowDiv('DivPuntualProv', 'show');	
	}

}

//%insChangeDisplay: se controla la visualizacion de DIV de la página
//------------------------------------------------------------------------------------------
function insChangeDisplay(value){
//------------------------------------------------------------------------------------------

	if(value == 1)
	{
		ShowDiv('DivMasiveProv', 'hide');
		ShowDiv('DivPuntualProv', 'show');	
		self.document.forms[0].tcdDate_Process.value = '';
	}
	else
	{
		ShowDiv('DivPuntualProv', 'hide');
		ShowDiv('DivMasiveProv', 'show');	
		self.document.forms[0].cbeClient_Provider.value = '';
		UpdateDiv('cbeClient_ProviderDesc','');
	}
	

}

//%ChangeValues: se formatea el campo proveedor
//------------------------------------------------------------------------------------------
function ChangeValues(Field)
//------------------------------------------------------------------------------------------
{
	cliente = InsValuesCero(Field);
	
	self.document.forms[0].cbeClient_Provider.value = cliente;
	
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="NC002" ACTION="valNC005tra.aspx?mode=1">
	<BR></BR>
<%
Response.Write(mobjValues.ShowWindowsName("NC005"))

%>
    <BR><BR>
	
 <TABLE WIDTH=100%>	
	<TR>
	  <TD>
	   	<TABLE WIDTH=100%>
		  <TR>
		    <TD><LABEL ID=9380></LABEL></TD> 
		    <TD><%=mobjValues.OptionControl(0, "optTypProcess", "Puntual", 1, 1, "insChangeDisplay(this.value)", True)%></TD> 
			<TD></TD> 	
		  </TR>
		  <TR>
		    <TD><LABEL ID=9380></LABEL></TD> 
		    <TD><%=mobjValues.OptionControl(0, "optTypProcess", "Masivo", 0, 2, "insChangeDisplay(this.value)", True)%></TD> 
			<TD></TD> 	
		  </TR>
		</TABLE>
      </TD>	
	 <TD>
		<DIV ID="DivPuntualProv" style="display:none">
			<TABLE WIDTH=100%>
			  <TR>
			    <TD><LABEL ID=9380>Proveedor</LABEL></TD> 
			    <TD><%Response.Write(mobjValues.PossiblesValues("cbeClient_Provider", "TABPROVIDER_DOC_PAY", 2,  , False,  ,  ,  ,  , "ChangeValues(this)", False, 15, "Tabla de Proveedores"))%></TD> 
				<TD></TD> 	
			  </TR>
			</TABLE>
		</DIV>
    
		<DIV ID="DivMasiveProv" style="display:none">
			<TABLE WIDTH=90%>
			  <TR>
			    <TD><%=mobjValues.OptionControl(0, "optProcess", "Preliminar", 1, 1,  , False)%></TD> 
			    <TD></TD> 
				<TD><LABEL ID=9380>Fecha Proceso</LABEL></TD> 	
				<TD><%=mobjValues.DateControl("tcdDate_Process", "",  ,  ,  ,  ,  ,  , False)%></TD> 	
			  </TR>
			  <TR>
			    <TD><%=mobjValues.OptionControl(0, "optProcess", "Definitivo", 0, 2,  , False)%></TD> 
			    <TD></TD> 
				<TD></TD> 	
				<TD></TD> 	
			  </TR>
			 </TABLE>
		 </DIV>
	  </TD> 
	</TR>
 </TABLE>   
    
    
    
    
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("NC005_K")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




