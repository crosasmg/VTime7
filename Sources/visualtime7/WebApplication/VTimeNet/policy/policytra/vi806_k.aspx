<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
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
Call mobjNetFrameWork.BeginPage("vi806_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vi806_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("VI806", "VI806_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
<SCRIPT> 
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"
</SCRIPT>            
<SCRIPT>

//% insStateZone: se controla el estado de los controles de la página
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
var lintIndex;
var error;
var nActions = new TypeActions();
var nMainAction = top.frames["fraSequence"].plngMainAction;
	with (self.document.forms[0]){
		cbeBranch.disabled=false
		tcnPolicy.disabled=false
		tcnCertif.disabled=false
		tcdEffecdate.disabled=false
		btn_tcdEffecdate.disabled=false
	}	
}
    
function insCancel(){
return true;
}
function insFinish(){
    return true;
}

//% InsChangeField: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function InsChangeField(sField, sValue){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		switch (sField){
			case 'Branch':
			    tcnPolicy.value="";
			    tcnCertif.value="";
				tcdEffecdate.value="";			    
				break;
			case 'Product':
			    tcnPolicy.value="";
			    tcnCertif.value="";
				tcdEffecdate.value="";			    
				break;
		}
	}	
}

//%insChangePolicy : Valida si la póliza es individual para deshabilitar el certificado
//------------------------------------------------------------------------------------------
function insChangePolicy(Form, sCodispl, sFrame){
//------------------------------------------------------------------------------------------
	if (typeof(sCodispl) == 'undefined' ) sCodispl = '';
	if (typeof(sFrame) == 'undefined' ) sFrame = 'fraHeader';
	with (Form){
		if (tcnPolicy.value != '' &&
		    tcnPolicy.value != hddnPolicy.value){
		    insDefValues('insValsPolitype', 'nBranch=' + cbeBranch.value +
		                                    '&nProduct=' + valProduct.value +
		                                    '&nPolicy=' + tcnPolicy.value +
		                                    '&sCodispl=' + sCodispl +
		                                    '&sFrame=' + sFrame);
			hddnPolicy.value = tcnPolicy.value;
		}
	}
}

</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>

<FORM METHOD="POST" ID="FORM" NAME="VI806" ACTION="valPolicyTra.aspx?x=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="15%"><LABEL ID=13848><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  , "valProduct",  ,  ,  , "InsChangeField(""Branch"",this.value)", True)%></TD>            
            <TD WIDTH="3%">&nbsp;</TD>
            <TD WIDTH="22%"><LABEL ID=13852><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD WIDTH="40%"><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  , True,  ,  ,  ,  , "InsChangeField(""Branch"",this.value)")%></TD>
        </TR>
        <TR>
            <TD WIDTH="15%"><LABEL ID=13851><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.NumericControl("tcnPolicy", 10,  ,  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insChangePolicy(self.document.forms[0], 'VI806_K', 'fraHeader');", True)%></TD>
    	                    <%Response.Write(mobjValues.HiddenControl("hddnPolicy", vbNullString))%>
            <TD WIDTH="3%">&nbsp;</TD>
            <TD WIDTH="22%"><LABEL ID=13849><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD WIDTH="40%"><%=mobjValues.NumericControl("tcnCertif", 10,  ,  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  ,  , True)%></TD>

        </TR>
        <TR>    
            <TD WIDTH="15%"><LABEL ID=13837><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.DateControl("tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>        
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%> 

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("vi806_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




