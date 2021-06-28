<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'**- The object to handling the general function to load values is defined
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'**- The object to handling the generic routines is defined
'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vi7002_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vi7002_k"

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>

//**+ Source Safe control of version
//+ Para Control de Versiones de Source Safe

    document.VssVersion="$$Revision: 3 $|$$Date: 10/06/04 13:44 $"

//**% insStateZone: This function enable/disable the fields of the page according to the action 
//**% to be performed
//% insStateZone: Habilita los campos de la forma según la acción a ejecutar
//-------------------------------------------------------------------------------------------
    function insStateZone(){
//-------------------------------------------------------------------------------------------    
}

//**% insCancel: This function executes the action to cancel of the page.
//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//**% FindCurrPolicy: The search of the policy currency is performed
//% FindCurrPolicy: Se busca la moneda de la póliza
//-----------------------------------------------------------------------------
function FindCurrPolicy(){
//-----------------------------------------------------------------------------
	var frm = self.document.forms[0];
	if (frm.cbeBranch.value != "" && frm.tcnPolicy.value!="" && frm.tcnPolicy.value != "0"){
		insDefValues('Switch_Curr_Pol', 'nBranch=' + frm.cbeBranch.value +
		                                '&nProduct=' + frm.valProduct.value +
		                                '&nPolicy=' + frm.tcnPolicy.value +
		                                '&dEffecdate=' + frm.tcdEffecdate.value + 
		                                '&sCodispl=' + 'VI7002');
	}		                            
}

//**% FindCurrCertif: The search of the certificate currency is performed
//% FindCurr: Se busca la moneda del certificado
//-----------------------------------------------------------------------------
function FindCurrCertif(){
//-----------------------------------------------------------------------------
	var frm = self.document.forms[0];
	if (frm.cbeBranch.value != "" && frm.tcnPolicy.value!="" && frm.tcnPolicy.value != "0" && frm.tcnCertif.value != "" && frm.tcnCertif.value != "0"){
		insDefValues('Switch_Curr_Cer', 'nBranch=' + frm.cbeBranch.value +
		                                '&nProduct=' + frm.valProduct.value +
		                                '&nPolicy=' + frm.tcnPolicy.value +
		                                '&nCertif=' + (frm.tcnCertif.value==''?'0':frm.tcnCertif.value) + 
		                                '&dEffecdate=' + frm.tcdEffecdate.value+ 
		                                '&sCodispl=' + 'VI7002');
	}		                                
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    
 <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("VI7002", "VI7002_k.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="post" ID="FORM" NAME="VI7002" ACTION="valPolicyTra.aspx?x=1">
    <TABLE WIDTH="100%">
		<BR>
		
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", 1, Session("nBranch"),  ,  ,  ,  ,  , "if(typeof(document.forms[0].valProduct)!=""undefined"")document.forms[0].valProduct.Parameters.Param1.sValue=this.value",  ,  , GetLocalResourceObject("cbeBranchToolTip"))%></TD>

            <%
With mobjValues.Parameters
	If IsDbNull(Session("nBranch")) Or IsNothing(Session("nBranch")) Or Trim(Session("nBranch")) = "0" Then
		.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Else
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End If
End With
%>

            <TD><LABEL ID=13664><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valProduct", "tabProdmaster1", 2, Session("nProduct"), True,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("valProductToolTip"))%></TD>
        </TR>

        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 8, Session("nPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "FindCurrPolicy()")%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 8, "0",  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  , "FindCurrCertif()", True)%></TD>
		</TR>
		
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD COLSPAN="2"><%=mobjValues.DateControl("tcdEffecdate", Session("dEffcedate"),  , GetLocalResourceObject("tcdEffecdateToolTip"))%></TD>		
		</TR>
    </TABLE>
    
    <%'=mobjValues.HiddenControl("tcdEffecdate", Date)%>
    <%=mobjValues.HiddenControl("cbeCurrency", CStr(0))%>

<%With Response
	.Write("<SCRIPT>")
	.Write("FindCurrPolicy();")
	.Write("</SCRIPT>")
End With%>    
</FORM>
</BODY>
<%
mobjValues = Nothing%> 
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("vi7002_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




