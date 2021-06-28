<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%mobjValues = New eFunctions.Values
Response.Expires = -1
%>
<HTML>
<HEAD>
<SCRIPT>		
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $"

//%insChangePolicy : Obtiene los datos de la póliza
//------------------------------------------------------------------------------------------
function insChangePolicy(sCodispl, sFrame){
//------------------------------------------------------------------------------------------
	if (typeof(sCodispl) == 'undefined' ) sCodispl = '';
	if (typeof(sFrame) == 'undefined' ) sFrame = 'fraHeader';
	with (self.document.forms[0]){
		if (tcnPolicy.value != '' ||
		    tcnPolicy.value != hddnPolicy.value){
		    insDefValues('Switch_Curr_Pol', 'nBranch=' + cbeBranch.value +
		                                    '&nProduct=' + valProduct.value +
		                                    '&nPolicy=' + tcnPolicy.value +
		                                    '&sCodispl=' + sCodispl);
			hddnPolicy.value = tcnPolicy.value;
		}
	}
}

//% InsChangeField: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function InsChangeField(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0])
		{
		if (tcnPolicy.value == '')
			{
			tcnPolicy.value='';
			valProduct.value='';
			UpdateDiv('valProductDesc','','');
		}
	}
}

//%insCancel:
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%insStateZone:
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}
</SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>


    <%
mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjMenu.MakeMenu("CA642", "CA642_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmReahPolicy_K" ACTION="ValPolicyTra.aspx?x=1">
<BR></BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=13901><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%
Response.Write(mobjValues.HiddenControl("tctCertype", "2"))
Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip")))
%>
			</TD>
            <TD><LABEL ID=13909><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%
Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), "", eFunctions.Values.eValuesType.clngWindowType))
%>
			</TD>
        </TR>
        <TR>
            <TD><LABEL ID=13908><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
			<TD><%
Response.Write(mobjValues.NumericControl("tcnPolicy", 8, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insChangePolicy('CA642', 'fraHeader');InsChangeField();"))
Response.Write(mobjValues.HiddenControl("hddnPolicy", vbNullString))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>





