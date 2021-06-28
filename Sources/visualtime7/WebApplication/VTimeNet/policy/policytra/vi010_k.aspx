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

Dim mblnDisabled As Boolean
Dim mintPreliminar As Object
Dim mintDefinitivo As Object
Dim mstrBranch As Object
Dim mstrProduct As Object
Dim mstrPolicy As Object
Dim mobjRequest As Object
    Dim mintCurrency As Object
    


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI010_K")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "VI010_K"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")

mblnDisabled = Request.QueryString.Item("sCodisplOri") = "CA767"

If mblnDisabled Then
	Session("nPolicy") = Request.QueryString.Item("nPropoNum")
	Session("nPropoNum") = Request.QueryString.Item("nPolicy")
	
	If CDbl(Request.QueryString.Item("nOperat")) = 5 Then
		mintPreliminar = 1
		mintDefinitivo = 0
	Else
		mintPreliminar = 0
		mintDefinitivo = 1
	End If
	mobjRequest = New ePolicy.Request
	
	Call mobjRequest.Find("8", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	Session("nSwitchOrigin") = mobjRequest.nSwitchOrigin
	mobjRequest = Nothing
	
	mobjRequest = New ePolicy.Curren_pol
	
	Call mobjRequest.findCurrency("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPropoNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	mintCurrency = mobjRequest.nCurrency
	mobjRequest = Nothing
	
	
Else
	Session("nPolicy_prop") = ""
	mintPreliminar = 1
	mintDefinitivo = 0
	Session("nPropoNum") = ""
End If

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
	.Write(mobjMenu.MakeMenu("VI010", "VI010_K.aspx", 1, ""))
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
		cbeCurrency.value='';
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
	SetOriginParameters();
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

function SetOriginParameters(){
//-------------------------------------------------------------------------------------------    
	with(document.forms[0])
		{
		if (cbeBranch.value !='0' && valProduct.value !='')
			{
			cbeOrigin.Parameters.Param1.sValue = cbeBranch.value;
			cbeOrigin.Parameters.Param2.sValue = valProduct.value;
			if (tcnPolicy.value == '')
				{ cbeOrigin.Parameters.Param3.sValue = '0'; }
			else
				{ cbeOrigin.Parameters.Param3.sValue = tcnPolicy.value; }
			cbeOrigin.disabled = false;
			btncbeOrigin.disabled = false;
			cbeOrigin.value = '';
			UpdateDiv('cbeOriginDesc','');
			}
		if (tcnPolicy.value == '')
			{
			cbeOrigin.disabled = true;
			btncbeOrigin.disabled = true;
			cbeOrigin.value = '';
			UpdateDiv('cbeOriginDesc','');
			cbeBranch.value = '';
			valProduct.value = '';
			UpdateDiv('valProductDesc','');
			cbeCurrency.value = '';
			}
		}
	}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="POST" ID="FORM" NAME="VI010" ACTION="valPolicyTra.aspx?x=1">
    <TABLE WIDTH="100%" border=0>
        <TR>
            <TD><LABEL ID=13848><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Session("nBranch"), "valProduct",  ,  ,  , "InsChangeField();", mblnDisabled)%></TD>
            
            <TD><LABEL ID=13852><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Session("nBranch"), eFunctions.Values.eValuesType.clngWindowType, mblnDisabled, Session("nProduct"),  ,  ,  , "InsChangeField()")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13851><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD>
            <%
Response.Write(mobjValues.NumericControl("tcnPolicy", 10, Session("nPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insChangePolicy('VI010', 'fraHeader');", mblnDisabled))
Response.Write(mobjValues.HiddenControl("hddnPolicy", vbNullString))
Response.Write(mobjValues.HiddenControl("hddsCodisplOri", Request.QueryString.Item("sCodisplOri")))
%>
			</TD>
            <TD><LABEL ID=13849><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 10, "0",  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  , "insChangeCertif('VI010', 'fraHeader');", True)%></TD>
        </TR>
        <TR>    
            <TD><LABEL ID=13656><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, mintCurrency,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>

            <TD><LABEL ID=13837><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>

            <%
                If mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) = eRemoteDB.Constants.dtmNull Then
                    Session("dEffecdate") = Today.Date
                End If
%>
            <TD><%=mobjValues.DateControl("tcdEffecdate", Session("dEffecdate"),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , mblnDisabled)%></TD>
		</TR>            
		</TR>        
        <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("cbeOriginCaption") %></LABEL></TD>
          <%
If CStr(Session("nBranch")) = "" Then
	mstrBranch = "0"
Else
	mstrBranch = Session("nBranch")
End If
If CStr(Session("nProduct")) = "" Then
	mstrProduct = "0"
Else
	mstrProduct = Session("nProduct")
End If
If CStr(Session("nPolicy")) = "" Then
	mstrPolicy = "0"
Else
	mstrPolicy = Session("nPolicy")
End If
With mobjValues
	.Parameters.Add("nBranch", mstrBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", mstrProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nPolicy", mstrPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
%>
                       
            <TD WIDTH=35%><%=mobjValues.PossiblesValues("cbeOrigin", "TAB_ORIGINPOLVI010", eFunctions.Values.eValuesType.clngWindowType, Session("nSwitchOrigin"), True,  ,  ,  ,  ,  , mblnDisabled,  , GetLocalResourceObject("cbeOriginToolTip"),  , 1)%></TD>
     		<TD colspan=2><%=mobjValues.OptionControl(41185, "optProcessType", GetLocalResourceObject("optProcessType_2Caption"), mintPreliminar, "2",  , CBool(mblnDisabled),  , GetLocalResourceObject("optProcessType_2ToolTip"))%></TD>

		</TR>
		<TR>
			<TD colspan=2>&nbsp;</TD>
			<TD colspan=2><%=mobjValues.OptionControl(41185, "optProcessType", GetLocalResourceObject("optProcessType_1Caption"), mintDefinitivo, "1",  , True,  , GetLocalResourceObject("optProcessType_1ToolTip"))%></TD>
		</TR>        
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%> 

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("VI010_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





