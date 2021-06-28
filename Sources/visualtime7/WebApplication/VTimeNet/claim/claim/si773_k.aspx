<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.13
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
Call mobjNetFrameWork.BeginPage("si773_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.13
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si773_k"

Response.Write("<SCRIPT>var mlngClaim</SCRIPT>")
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
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT LANGUAGE="JavaScript">
	var nMainAction;	
	
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 12/05/04 17:02 $"
    
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{   
   return true;
}

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return true;
}

//%Activa o desactiva la forma de pago dependiendo del proceso seleccionado
//------------------------------------------------------------------------------------------
function DisablePay(Value)
//------------------------------------------------------------------------------------------
{  
    with (self.document.forms[0]){
      if (Value==1){
        cbePayForm.value = 0;
		cbePayForm.disabled = true; 		 
      }
      else
		cbePayForm.disabled = false; 		 
   }
}
</SCRIPT>

    <%Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.13
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("SI773", "SI773_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
Response.Write("<SCRIPT>nMainAction=0" & Request.QueryString("nMainAction") & "</SCRIPT>")%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmClaimPayment" ACTION="valClaim.aspx?sMode=1">
<BR><BR>
    <TABLE WIDTH="100%">
       <TR>
			<TD><LABEL ID=0>Ramo</LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", "Ramo al que pertenecen los siniestros")%></TD>
            <TD>&nbsp;</TD>
			<TD><LABEL ID=0>Producto</LABEL></TD>
		    <TD><%=mobjValues.ProductControl("valProduct", "Producto al cual pertenecen los siniestros ", CStr(eRemoteDB.Constants.intNull), eFunctions.Values.eValuesType.clngWindowType)%></TD>					    
		</TR>
		<TR>	
			<TD><LABEL ID=0>Siniestro</LABEL></TD>
  		    <TD><%=mobjValues.NumericControl("tcnClaim", 10, "",  , "Número del siniestro al que se desean procesar los pagos de rentas")%></TD>				    
  		    <TD COLSPAN="3">&nbsp;</TD>
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Tipo de proceso">Fecha</A></LABEL></TD>
			<TD>&nbsp;</TD>
  		    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Fecha">Tipo de proceso</A></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="Horline"></TD>
			<TD></TD>
			<TD COLSPAN="2" CLASS="Horline"></TD>
		</TR>
		<TR>
            <TD><LABEL ID=0>Desde</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdStartDate", "",  , "Inicio del período a indemnizar")%></TD>                        
			<TD>&nbsp;</TD>		
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optProcess", "Puntual", "1", "1", "DisablePay(this.value)")%></TD>            
		</TR>
		<TR>
   			<TD><LABEL ID=0>Hasta</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEndDate", "",  , "Fin del período a indemnizar")%></TD>		
			<TD>&nbsp;</TD>            
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optProcess", "Masivo", "0", "2", "DisablePay(this.value)")%></TD>
		</TR>
		<TR>
			<TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><A NAME="Pago">Pago</A></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="5" CLASS="Horline"></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0>Forma de pago</LABEL></TD>
			<TD COLSPAN="4">
			<%With mobjValues
	.TypeList = 1
	.List = "1,4,8,9"
	.BlankPosition = True
	Response.Write(.PossiblesValues("cbePayForm", "table138", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , True,  , "Forma de Pago de la renta"))
End With
%>
			</TD>
		</TR>
  	</TABLE>   
<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.13
Call mobjNetFrameWork.FinishPage("si773_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




