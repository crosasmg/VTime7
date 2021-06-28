<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.05
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
Call mobjNetFrameWork.BeginPage("vil7701_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vil7701_k"

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    
 <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("VIL7701", "vil7701_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With

mobjMenu = Nothing%>
    
<SCRIPT>
//**+ Source Safe control of version
//+ Para Control de Versiones de Source Safe
    document.VssVersion="$$Revision: 1 $|$$Date: 8/10/03 19:15 $"

//**% insStateZone: This function enable/disable the fields of the page according to the action 
//**% to be performed
//% insStateZone: Habilita los campos de la forma según la acción a ejecutar
//-------------------------------------------------------------------------------------------
    function insStateZone(){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
	    optTypePD[0].disabled=false;
	    optTypePD[1].disabled=false;
		cbeBranch.disabled=false;
		valProduct.disabled=false;
		btnvalProduct.disabled=false;
	}
	
	EnableFields(self.document.forms[0].elements['optTypePD'][0]);
}

//**% insCancel: This function executes the action to cancel of the page.
//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% EnableFields: Habilita / Deshabilita los campos de la ventana
//-------------------------------------------------------------------------------------------------
function EnableFields(Field){
//-------------------------------------------------------------------------------------------------
    var lblnDisabled=false
    
    with (self.document.forms[0])
    {
		switch (Field.name)
		{
		    case 'optTypeIM':
		    {
		        lblnDisabled=optTypeIM[1].checked;
		        if(optTypeIM[1].checked)
		        {
					cbeBranch.disabled=!lblnDisabled;
					valProduct.disabled=!lblnDisabled;
					btnvalProduct.disabled=!lblnDisabled;
					tcnProponum.disabled=true;
		        }
		        
		        if(optTypeIM[0].checked)
		        {
					cbeBranch.disabled=lblnDisabled;
					valProduct.disabled=lblnDisabled;
					tcnProponum.disabled=lblnDisabled;
					btnvalProduct.disabled=lblnDisabled;
		        }
		        break;
		    }
		    
		    case "optTypePD":
		    {
				if(optTypePD[0].checked)
				{
					optTypeIM[0].disabled=false;
					optTypeIM[1].disabled=false;
				}
				break;
		    }
		    default:
		    {
		        optTypeIM[0].disabled=optTypePD[0].checked;
		        optTypeIM[1].disabled=optTypePD[0].checked;
		        lblnDisabled=true;
		    }
		}
		if (lblnDisabled)
		{
		    cbeBranch.value='';
		    valProduct.value='';
		    tcnProponum.value='';
		    UpdateDiv('valProductDesc','','Normal');
		}
    }
}
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="POST" ID="FORM" NAME="VIL7701" ACTION="valPolicyRep.aspx?x=1">
	<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))%>
<BR>
    <TABLE WIDTH="100%">
	    <TR>
		    <TD WIDTH="5%"></TD>
			<TD COLSPAN=2 CLASS="HighLighted"><LABEL ID=0><A NAME="Proceso"><%= GetLocalResourceObject("AnchorProcesoCaption") %></A></LABEL></TD>
			<TD></TD>
			<TD></TD>
			<TD></TD>
	    </TR>
		<TR>
		    <TD></TD>
            <TD COLSPAN=2 CLASS="HORLINE"></TD>
			<TD></TD>
			<TD></TD>
			<TD></TD>
        </TR>
		<TR>
			<TD></TD>
		    <TD><%=mobjValues.OptionControl(0, "optTypePD", GetLocalResourceObject("optTypePD_CStr1Caption"), CStr(1), CStr(1), "EnableFields(this);", True,  , GetLocalResourceObject("optTypePD_CStr1ToolTip"))%></TD>
			<TD></TD>
			<TD><LABEL ID=41208><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  ,  , True)%></TD>
        </TR>
		<TR>
			<TD></TD>	
			<TD><%=mobjValues.OptionControl(0, "optTypePD", GetLocalResourceObject("optTypePD_CStr2Caption"), CStr(2), CStr(2), "EnableFields(this);", True,  , GetLocalResourceObject("optTypePD_CStr2ToolTip"))%></TD>
            <TD></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), vbNullString, eFunctions.Values.eValuesType.clngWindowType, True, vbNullString)%></TD>
		</TR>
		<TR>
			<TD></TD>
            <TD><%=mobjValues.OptionControl(0, "optTypeIM", GetLocalResourceObject("optTypeIM_CStr1Caption"), CStr(2), CStr(1), "EnableFields(this);", True,  , GetLocalResourceObject("optTypeIM_CStr1ToolTip"))%></TD>		    
            <TD></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnProponumCaption") %></LABEL> </TD>
            <TD><%=mobjValues.NumericControl("tcnProponum", 8, vbNullString,  , GetLocalResourceObject("tcnProponumToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
		</TR>
		
		<TR>
			<TD></TD>
		    <TD><%=mobjValues.OptionControl(0, "optTypeIM", GetLocalResourceObject("optTypeIM_CStr2Caption"), CStr(1), CStr(2), "EnableFields(this);", True,  , GetLocalResourceObject("optTypeIM_CStr2ToolTip"))%></TD>
			<TD></TD>
			<TD></TD>
        </TR>
        
    </TABLE>
</FORM>
</BODY>
<%
mobjValues = Nothing%> 
</HTML>
<%
Response.Write("<SCRIPT>EnableFields(self.document.forms[0].elements['optTypePD'][0].checked=true);</SCRIPT>")
'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.05
Call mobjNetFrameWork.FinishPage("vil7701_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>




