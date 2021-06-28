<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**- The object to handling the general function to load values is defined
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'**- The object to handling the generic routines is defined
'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = 0

mobjValues = New eFunctions.Values

mobjMenu = New eFunctions.Menues

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>

//**+ Source Safe control of version
//+ Para Control de Versiones de Source Safe

    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:40 $"

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

//**% FindShowCertifShowCertif: This function enabled or disabled the field nCertif. 
//% FindShowCertifShowCertif: Esta función habilita o inhabilita el campo nCertif.
//-----------------------------------------------------------------------------
function FindShowCertif(){
//-----------------------------------------------------------------------------
	ShowPopUp("/VTimeNet/Policy/PolicyRep/ShowDefValues.aspx?Field=Switch_Curr_Pol" + "&nBranch=" + self.document.forms[0].cbeBranch.value + "&nProduct=" + self.document.forms[0].valProduct.value + "&nPolicy=" + self.document.forms[0].tcnPolicy.value,"ShowDefValuesCollectionTra", 1, 1,"no","no",2000,2000)
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    
 <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("VIL7001", "VIL7001_K.aspx", 1, ""))
End With

mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="post" ID="FORM" NAME="VIL7001" ACTION="valPolicyRep.aspx?x=1">
	<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))%>
<BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=13658><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD> <%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  , "if(typeof(document.forms[0].valProduct)!=""undefined"")document.forms[0].valProduct.Parameters.Param1.sValue=this.value",  , 3)%></TD>

            <%
With mobjValues.Parameters
	.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
%>

            <TD><LABEL ID=13664><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD> <%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(0), eFunctions.Values.eValuesType.clngWindowType, False, vbNullString,  ,  ,  ,  , 4)%></TD>            
        </TR>

        <TR>
            <TD><LABEL ID=13663><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 8, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "FindShowCertif()")%></TD>
            
            <TD><LABEL ID=13660><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 8, "0",  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
        <TR>
            <TD><LABEL ID=13644><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>        
            <%
If Session("dEffecdate") = eRemoteDB.Constants.dtmNull Then
	Session("dEffecdate") = Today
End If
%>
            <TD><%=mobjValues.DateControl("tcdEndDate", Session("dEffecdate"),  , GetLocalResourceObject("tcdEndDateToolTip"))%></TD>
		</TR>
    </TABLE>

<%With Response
	.Write("<SCRIPT>")
	.Write("var dEffecdate = '" & CStr(Session("dEffecdate")) & "';")
	.Write("FindShowCertif();")
	.Write("</SCRIPT>")
End With%>    
</FORM>
</BODY>
<%
mobjValues = Nothing%> 
</HTML>





