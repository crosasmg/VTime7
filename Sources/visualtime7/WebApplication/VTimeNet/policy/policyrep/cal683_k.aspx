<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mstrQuote As String


'**% insDefineHeader: This function allows to load the fields of the header
'%   insDefineHeader: Permite cargar los campos del encabezado
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41007><A NAME=""Tipo de listado"">" & GetLocalResourceObject("AnchorTipo de listadoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=41208>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HORLINE""></TD>		" & vbCrLf)
Response.Write("			<TD COLSPAN=""3""></TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_CStr1Caption"), CStr(0), CStr(1),  ,  ,  , GetLocalResourceObject("optType_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=40011>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  ,  ,  ,  ,  ,  , "insEnabledPolicy(this)"))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_CStr2Caption"), CStr(1), CStr(2),  ,  ,  , GetLocalResourceObject("optType_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD> <LABEL ID=40281>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.NumericControl("tcnPolicy", 8, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=41370>" & GetLocalResourceObject("tcdDateRunCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdDateRun", CStr(Today),  , GetLocalResourceObject("tcdDateRunToolTip")))


Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cal683_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cal683_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mstrQuote = """"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<SCRIPT>
//**% insCancel: This function is executed when the page is cancelled.
//%   insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//**% insStateZone: This function allows to control the status of the items page.
//%   insStateZone: Se controla el estado de los campos de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

//**% insChargeProduct: The parameters of the field product are charged
//%   insChargeProduct: Se cargan los parámetros del campo producto.
//------------------------------------------------------------------------------------------
function insChargeProduct(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value!=0)
		with(self.document.forms[0]){
			valProduct.disabled=false;
			btnvalProduct.disabled=false;
			valProduct.value="";
			UpdateDiv("valProductDesc", "")
			valProduct.Parameters.Param1.sValue=lobject.value;
			valProduct.Parameters.Param2.sValue=0;
		}
}

//**% insEnabledFields: The fields of page are enabled and disabled.
//%   insEnabledFields: Permite habilitar e inhabilitar los campos de la página.
//------------------------------------------------------------------------------------------
function insEnabledFields(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value!=1)
		with(self.document.forms[0]){
            valDocument.disabled=false;
            btnvalDocument.disabled=false;
            cbeBranch.disabled=true;
			valProduct.disabled=true;
			btnvalProduct.disabled=true;
			tcnPolicy.disabled=true;
			tcnCertif.disabled=true;
			cbeBranch.value="";
			valProduct.value="";
			tcnPolicy.value="";
			tcnCertif.value="";
			valProductDesc.value="";
			UpdateDiv("valProductDesc", "")
		}
    else
        with(self.document.forms[0]){
            valDocument.disabled=true;
            btnvalDocument.disabled=true;
            cbeBranch.disabled=false;
			valProduct.disabled=false;
			btnvalProduct.disabled=false;
			tcnPolicy.disabled=true;
			tcnCertif.disabled=true;
			tcnPolicy.value="";
			tcnCertif.value="";
			valDocument.value="";
        }			
}

//**% insEnabledPolicy(): Permited enabled and disabled the policy field.
//%   insEnabledPolicy(): Permite habilitar e inhabilitar el campo Póliza.
//------------------------------------------------------------------------------------------
function insEnabledPolicy(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value) 
		self.document.forms[0].tcnPolicy.disabled=false;
    else
        with(self.document.forms[0]){
			tcnPolicy.disabled=true;
			tcnPolicy.value="";
        }			
}

//**% insEnabledCertif(): Permited enabled and disabled the certificate field.
//%   insEnabledCertif(): Permite habilitar e inhabilitar el campo Certificado.
//------------------------------------------------------------------------------------------
function insEnabledCertif(lobject){
//------------------------------------------------------------------------------------------
    var lstrQueryString;
	var lintBranch = 0;
	var lintProduct = 0;
    var llngPolicy = 0

    if (lobject.value){
	    lintBranch  = self.document.forms[0].elements[<%=mstrQuote%>cbeBranch<%=mstrQuote%>].value
	    lintProduct = self.document.forms[0].elements[<%=mstrQuote%>valProduct<%=mstrQuote%>].value
	    llngPolicy  = self.document.forms[0].elements[<%=mstrQuote%>tcnPolicy<%=mstrQuote%>].value
	
	    lstrQueryString = "ShowDefValues.aspx?Field=nPolicy&nBranch=" + lintBranch + " &nProduct=" + lintProduct + " &nPolicy=" + llngPolicy;
        ShowPopUp(lstrQueryString,"Values",1,1,"No","No", 2000, 2000);	
    }        
    else
        with(self.document.forms[0]){
			tcnCertif.disabled=true;
			tcnCertif.value="";
        }			
}

</SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CAL683_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With

mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CAL001" ACTION="valPolicyRep.aspx?Mode=1">
<BR><BR>
<%
Call insDefineHeader()

mobjValues = Nothing
%>

</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("cal683_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




