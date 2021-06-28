<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

    Dim mintPreliminar As Object
    Dim mintDefinitivo As Object

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI017_K")
    '~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "VI017_K"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
    

    Session("nPolicy_prop") = ""
	mintPreliminar = 1
	mintDefinitivo = 0
	Session("nPropoNum") = ""

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
        .Write(mobjMenu.MakeMenu(Request.QueryString("sCodispl"), "VI017_K.aspx", 1, ""))
    End With
    mobjMenu = Nothing
%>
<SCRIPT> 
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 29-05-14 15:22 $|$$Author: Mgonzalez $"
</SCRIPT>            
<SCRIPT>

//% insStateZone: se controla el estado de los controles de la página
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
	with (self.document.forms[0]){
        cbeBranch.disabled = false;
        tcnPolicy.disabled = false;
        tcdEffecdate.disabled = false;
        btn_tcdEffecdate.disabled = false;
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
function InsChangeField(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tcnPolicy.value='';
		tcnCertif.value='';
	}
	//SetOriginParameters();
}

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
		                                    '&dEffecdate=' + tcdEffecdate.value +
		                                    '&sCodispl=' + sCodispl);
			hddnPolicy.value = tcnPolicy.value;
		}
	}
}

//%insChangeCertif : Obtiene los datos del certificado
//------------------------------------------------------------------------------------------
function insChangeCertif(sCodispl, sFrame){
//------------------------------------------------------------------------------------------
	if (typeof(sCodispl) == 'undefined' ) sCodispl = '';
	if (typeof(sFrame) == 'undefined' ) sFrame = 'fraHeader';
	with (self.document.forms[0]){
		insDefValues('Switch_Curr_Cer', 'nBranch=' + cbeBranch.value +
		                                '&nProduct=' + valProduct.value +
		                                '&nPolicy=' + tcnPolicy.value +
		                                '&nCertif=' + tcnCertif.value +
		                                '&dEffecdate=' + tcdEffecdate.value +
		                                '&sCodispl=' + sCodispl);
	}
	
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="POST" ID="FORM" NAME="VI017" ACTION="valpolicytra.aspx?x=1">
    <TABLE WIDTH="100%" border=0>
        <TR>
            <TD><LABEL ID=LABEL1><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%= mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), "", "valProduct", , , , "InsChangeField();", True)%></TD>
            
            <TD><LABEL ID=LABEL1><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%= mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), "", eFunctions.Values.eValuesType.clngWindowType, True, , , , , "InsChangeField()")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=LABEL2><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD>
            <%
                Response.Write(mobjValues.NumericControl("tcnPolicy", 10, "", , GetLocalResourceObject("tcnPolicyToolTip"), , 0, , , , "insChangePolicy('VI017', 'fraHeader');", True))
                Response.Write(mobjValues.HiddenControl("hddnPolicy", vbNullString))
                Response.Write(mobjValues.HiddenControl("hddsCodisplOri", ""))
			%>
			</TD>
            <TD><LABEL ID=LABEL3><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%= mobjValues.NumericControl("tcnCertif", 10, "0", , GetLocalResourceObject("tcnCertifToolTip"), , 0, , , , "insChangeCertif('VI010', 'fraHeader');", True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=LABEL4><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%= mobjValues.DateControl("tcdEffecdate", Today, , GetLocalResourceObject("tcdEffecdateToolTip"), , , , , True)%></TD>
			<TD WIDTH="25%"><%= mobjValues.CheckControl("chkByAccount", GetLocalResourceObject("chkByAccountCaption"), 1, "1")%><BR></TD>
		</TR>
		<%If Request.QueryString("sCodispl") = "VI017-2" Then%>
        <TR>
			<TD COLSPAN=4><%= mobjValues.CheckControl("chkTransferAll", GetLocalResourceObject("chkTransferAllCaption"), 0, "1")%></TD>
        </TR>
		<%End If%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%  mobjValues = Nothing%> 

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
	Call mobjNetFrameWork.FinishPage("VI017_K")
	mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>


