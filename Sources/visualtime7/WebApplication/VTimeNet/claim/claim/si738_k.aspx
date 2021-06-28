<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.13
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si738_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.13
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si738_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.13
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Request.QueryString("nMainAction") = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>

//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

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

function insChangeValue(Field){
//-------------------------------------------------------------------------------------------
    var lstrQstring='';
    
    switch(Field.name)
	{
	    case "tcnPolicy":	
            with(self.document.forms[0])
		    {		
	            if(Field.value!='' && Field.value>0)
	            {
	                lstrQString = 'dEffecdate=' + tcdPayDate.value +
	                              '&nPolicy='   + Field.value +
	                              '&nCertif=0' +
	                              '&nBranch=' + cbeBranch.value +
	                              '&nProduct=' + valProduct.value
	                insDefValues('ClaimData', lstrQString ,'/VTimeNet/Claim/Claim');
	            }
            }
            break;
            
        case "cbeBranch":	
            with(self.document.forms[0])
		    {		
//		     tcnPolicy.value =''; 
//		     
//		     tcnCertif.value=''; 
//		     tctClientCollect.value='';
//		     tctClientCollect_Digit.value=''
//		     UpdateDiv('lblCliename',' ');
//		     UpdateDiv('valProductDesc', ' ');

		     if (Field.value != '' && Field.value > 0) {
		         
		         valProduct.Parameters.Param1.sValue = Field.value;
		         btnvalProduct.disabled = false;
		         valProduct.disabled = false;
		         valProduct.value = '';
		         tctClientCollect.value = '';
		         tctClientCollect_Digit.value = '';
		         UpdateDiv('valProductDesc', ' ', 'Normal');
		         UpdateDiv('lblCliename', ' ', 'Normal');
		     }
		     else {
		         valProduct.disabled = true;
		         btnvalProduct.disabled = true;
		         valProduct.value = '';
		         tctClientCollect.value = '';
		         tctClientCollect_Digit.value = '';
		         UpdateDiv('valProductDesc', ' ', 'Normal');
		         UpdateDiv('lblCliename', ' ', 'Normal');
		     }

            }
            break;
        
        
        
                
                
        

   	}
}

</SCRIPT>
    <%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("SI738", "SI738_K.aspx", 1, "SI738"))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="ClaimPaymentLot" ACTION="valClaim.aspx?sMode=2">
<BR><BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0>Fecha de pago</LABEL></TD>
            <TD><%'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'%>
<%=mobjValues.DateControl("tcdPayDate", CStr(Today),  , "Fecha de pago de los siniestros incluídos en una relación.")%> </TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
		</TR>
		<TR>
            <TD><LABEL ID=0>Ramo</LABEL></TD>  
            <TD><!--%= mobjValues.BranchControl("cbeBranch","Ramo al que pertenece la póliza o certificado",Request.QueryString("nBranch"),"valProduct",,,,"insChangeValue(this)",False)%></TD-->
                <%=mobjValues.BranchControl("cbeBranch", "Ramo al que pertenecen la(s) póliza(s) siniestrada(s)",  , CStr(True),  ,  ,  , "insChangeValue(this)")%></TD>
		    <TD>&nbsp;</TD>
			<TD><LABEL ID=0>Producto</LABEL></TD>
			<%--<TD><%=mobjValues.ProductControl("valProduct", "Producto al que pertenece la póliza o certificado", Request.QueryString("nBranch"), eFunctions.Values.eValuesType.clngWindowType, False, Request.QueryString("nProduct"), , , , "")%></TD>--%>
            <TD><%=mobjValues.ProductControl("valProduct", "Producto al que pertenecen la(s) póliza(s) siniestrada(s)",  , eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  , "insChangeValue(this);")%></TD>
        </TR>
		<TR>
			<TD><LABEL ID=0>Póliza</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, "",  , "Número de Poliza de siniestros a procesar.",  , 0,  ,  ,  , "insChangeValue(this)", False)%></TD>
            <TD>&nbsp;</TD>    
			<TD><LABEL ID=0>Certificado</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 10, "",  , "Número de Certificado de siniestros a procesar.")%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0>Liquidador</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCod_Agree", 5, "",  , "Número del Liquidador Externo con siniestros a procesar.")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0>Usuario</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valUsercod", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  ,  , False, 5, "Código que identifica al usuario en el sistema",  ,  ,  , False)%></TD>
 		</TR>
 		<TR>
 		    <TD><LABEL ID=0>Contratante RUT </LABEL></TD>
            <TD><%=mobjValues.ClientControl("tctClientCollect", "",  , "Código del cliente contratante",  , False, "lblCliename", False,  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
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
Call mobjNetFrameWork.FinishPage("si738_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




