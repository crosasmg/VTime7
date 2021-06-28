<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mclsProduct As eProduct.Product
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mclsProduct = New eProduct.Product
	mobjMenu = New eFunctions.Menues
End With

mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "dp043b"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
	.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "DP043B.aspx"))
End With
mobjMenu = Nothing
%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
document.VssVersion="$$Revision: 4 $|$$Date: 15/02/06 16:46 $|$$Author: Clobos $"

//% ShowParcial: Habilita/deshabilita los controles del frame "Rescates parciales"
//--------------------------------------------------------------------------------
function ShowParcial(){
//--------------------------------------------------------------------------------
    with (self.document.forms[0]){
		tcnSurcashv.disabled   = !chkSurrenpi.checked
		cbeFreqSurr.disabled   = !chkSurrenpi.checked
		tcnCharge.disabled     = !chkSurrenpi.checked
		tcnChargeAmo.disabled  = !chkSurrenpi.checked
		tcnQmmsurr.disabled    = !chkSurrenpi.checked
		tcnQmysurr.disabled    = !chkSurrenpi.checked
		tcnAminsurr.disabled   = !chkSurrenpi.checked
		tcnAmaxsurr.disabled   = !chkSurrenpi.checked
		tcnCapminsurr.disabled = !chkSurrenpi.checked
		tcnBalminsurr.disabled = !chkSurrenpi.checked
		tcnPervssurr.disabled  = !chkSurrenpi.checked
		cbeOrigin_Surr.disabled   = !chkSurrenpi.checked
		tcnQmepsurr.disabled   = !chkSurrenti.checked
		tcnQMMPsurr.disabled   = !chkSurrenpi.checked
		 
		if (tctRousurre.value == '' || 
		   !chkSurrenpi.checked) 
		{	tcnSurcashv.value   = 0
			cbeFreqSurr.value   = 0
			tcnCharge.value     = 0
			tcnChargeAmo.value  = 0
			tcnQmmsurr.value    = 0
			tcnQmysurr.value    = 0
			tcnAminsurr.value   = 0
			tcnAmaxsurr.value   = 0
			tcnCapminsurr.value = 0
			tcnBalminsurr.value = 0
			tcnPervssurr.value  = 0
			cbeOrigin_Surr.value   = 0
			tcnQMMPsurr.value   = 0
		}
		if (tctRousurre.value == '' || 
		   !chkSurrenti.checked) 
		{   tcnQmepsurr.value    = 0
		}
		if (tctRousurre.value == '')
		{   tcnQmepsurr.disabled = !chkSurrenti.checked
		    tcnQmepsurr.value    = 0
		    tcnQMMPsurr.disabled = !chkSurrenpi.checked
		    tcnQMMPsurr.value    = 0
        }
    }
}
//% ShowOptSurrType: Habilita/deshabilita las opciones de tipo de rescate: Totales y Parciales
//--------------------------------------------------------------------------------------------
function ShowOptSurrType(){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		chkSurrenti.disabled = !(tctRousurre.value.replace(/ */,'') != '')
		chkSurrenpi.disabled = !(tctRousurre.value.replace(/ */,'') != '')

		if (chkSurrenpi.disabled)
		{	chkSurrenti.checked = false;
			chkSurrenpi.checked = false;
		}
		ShowParcial()
    }
}
//% insDisabled: Habilita/deshabilita el campo Rutina de cálculo de retención
//--------------------------------------------------------------------------------------------
function insDisabled(){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		if (!chkApplyRouSurr.checked)
		    tctRoutineSurr.disabled = true
		else
		    tctRoutineSurr.disabled = false;
	}	    
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP043B" ACTION="valProdLifeSeq.aspx?nMode=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Response.Write("<BR>")
Call mclsProduct.FindProduct_li(Session("nBranch"), Session("nProduct"), Session("dEffecdate"))
%>
	<TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=14802><%= GetLocalResourceObject("tctRousurreCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctRousurre", 12, mclsProduct.sRousurre,  , GetLocalResourceObject("tctRousurreToolTip"),  ,  ,  , "ShowOptSurrType()")%></TD>
            <TD>&nbsp;</TD>
            <TD><%=mobjValues.CheckControl("chkSurrenti", GetLocalResourceObject("chkSurrentiCaption"), mclsProduct.sSurrenti, "1", "ShowParcial()", Not mclsProduct.sSurrenti = "1")%></TD>
            <TD><%=mobjValues.CheckControl("chkSurrenpi", GetLocalResourceObject("chkSurrenpiCaption"), mclsProduct.sSurrenpi, "1", "ShowParcial()", Not mclsProduct.sSurrenpi = "1")%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnQmepsurrCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnQmepsurr", 5, CStr(mclsProduct.nQmepsurr),  , GetLocalResourceObject("tcnQmepsurrToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnQMMPsurrCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnQMMPsurr", 5, CStr(mclsProduct.nQMMPSurr),  , GetLocalResourceObject("tcnQMMPsurrToolTip"))%></TD>
        </TR>
        <TR>
            <TD>&nbsp;</TD>
        </TR>
    </TABLE>
	<TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=100191><A NAME="Rescates"><%= GetLocalResourceObject("AnchorRescatesCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
        </TR>
        <TR>
			<TD><LABEL ID=14911><%= GetLocalResourceObject("tcnSurcashvCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnSurcashv", 18, CStr(mclsProduct.nSurcashv),  , GetLocalResourceObject("tcnSurcashvToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=14894><%= GetLocalResourceObject("cbeFreqSurrCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeFreqSurr", "Table114", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct.nSurrfreq),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeFreqSurrToolTip"))%></TD>
        </TR>        
        <TR>
            <TD><LABEL ID=14892><%= GetLocalResourceObject("tcnChargeCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCharge", 4, CStr(mclsProduct.nCharge),  , GetLocalResourceObject("tcnChargeToolTip"),  , 2,  ,  ,  ,  , True)%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=14893><%= GetLocalResourceObject("tcnChargeAmoCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnChargeAmo", 18, CStr(mclsProduct.nChargeAmo),  , GetLocalResourceObject("tcnChargeAmoToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnQmmsurrCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnQmmsurr", 5, CStr(mclsProduct.nQmmsurr),  , GetLocalResourceObject("tcnQmmsurrToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnQmysurrCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnQmysurr", 5, CStr(mclsProduct.nQmysurr),  , GetLocalResourceObject("tcnQmysurrToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnAminsurrCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAminsurr", 18, CStr(mclsProduct.nAminsurr),  , GetLocalResourceObject("tcnAminsurrToolTip"), True, 6)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnAmaxsurrCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAmaxsurr", 18, CStr(mclsProduct.nAmaxsurr),  , GetLocalResourceObject("tcnAmaxsurrToolTip"), True, 6)%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnCapminsurrCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCapminsurr", 18, CStr(mclsProduct.nCapminsurr),  , GetLocalResourceObject("tcnCapminsurrToolTip"), True, 6)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnBalminsurrCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnBalminsurr", 18, CStr(mclsProduct.nBalminsurr),  , GetLocalResourceObject("tcnBalminsurrToolTip"), True, 6)%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnMaxchargsurrCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnMaxchargsurr", 18, CStr(mclsProduct.nMaxchargsurr),  , GetLocalResourceObject("tcnMaxchargsurrToolTip"), True, 6)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnPervssurrCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPervssurr", 5, CStr(mclsProduct.nPervssurr),  , GetLocalResourceObject("tcnPervssurrToolTip"),  , 2)%></TD>
            
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tctRoutineSurrCaption") %></LABEL></TD>            
            <TD><%=mobjValues.TextControl("tctRoutineSurr", 12, mclsProduct.sRoutineSurr,  , GetLocalResourceObject("tctRoutineSurrToolTip"),  ,  ,  ,  , Not mclsProduct.sApplyRouSurr = "1")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("cbeOrigin_SurrCaption") %></LABEL></TD>
            <TD><%mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("cbeOrigin_Surr", "tab_ord_origin", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct.nOrigin_Surr), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOrigin_SurrToolTip")))%></TD>
        </TR>
        <TR>
            <TD colspan="2"><%=mobjValues.CheckControl("chkApplyRouSurr", GetLocalResourceObject("chkApplyRouSurrCaption"), mclsProduct.sApplyRouSurr, "1", "insDisabled()",  ,  , GetLocalResourceObject("chkApplyRouSurrToolTip"))%></TD>
        </TR>
	</TABLE>	
</FORM>
</BODY>
</HTML>
<%
If Not mobjValues.ActionQuery Then
	If mclsProduct.sSurrenti = "1" Or mclsProduct.sSurrenpi = "1" Then
		Response.Write("<SCRIPT>ShowOptSurrType()</SCRIPT>")
	End If
End If
%>




