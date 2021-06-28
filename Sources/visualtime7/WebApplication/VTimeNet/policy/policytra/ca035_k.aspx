<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca035_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ca035_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>

<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>

<SCRIPT> 
	
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"
    	
//% insStateZone: Coloca los campos de la página Enabled
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
	with (self.document.forms[0])
	{ 
	cbeBranch.disabled = false		
	tcnPolicy.disabled = false	
	tcnCertif.disabled = false
	tcdeffecdate.disabled = false	
	btn_tcdeffecdate.disabled = false	
	}
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}
//% ChangeBranch: Habilita el campo "Producto" y pasa el valor del campo "Ramo" como parámetro
//--------------------------------------------------------------------------------------------
function ChangeBranch(){
//--------------------------------------------------------------------------------------------

	if(typeof(document.forms[0].valProduct)!='undefined'){
		self.document.forms[0].valProduct.value='';
		self.document.forms[0].tcnPolicy.value='';
		self.document.forms[0].tcnCertif.value='';
		self.document.forms[0].tcdeffecdate.value='';
		UpdateDiv('tctCliename','');
	}
}
//% insChangePolicy: 
//--------------------------------------------------------------------------------------------
function insChangePolicy(field){
//--------------------------------------------------------------------------------------------

var lstrField

	lstrField = 'nBranch='+ self.document.forms[0].cbeBranch.value;
    lstrField += '&nProduct='+ self.document.forms[0].valProduct.value;
    lstrField += '&nPolicy='+ field.value;
	
	insDefValues('CA035_K',lstrField);
	
}
</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CA035", "CA035_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmGarantSusp" ACTION="ValPolicyTra.aspx?sTime=1">	
<BR></BR>
    <TABLE WIDTH="100%">         		
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>

            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  , "ChangeBranch()")%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(2), eFunctions.Values.eValuesType.clngWindowType)%></TD>			
		</TR>            
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10,  ,  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insChangePolicy(this);", True, 3)%></TD>			
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCertif", 10,  ,  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  ,  , True, 4)%></TD>			
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdeffecdateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdeffecdate",  ,  , GetLocalResourceObject("tcdeffecdateToolTip"),  ,  ,  ,  , True, 5)%> </TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD><%=mobjValues.DIVControl("tctCliename")%></TD>						
        </TR>
    </TABLE>
</BODY>
</FORM>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("ca035_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




