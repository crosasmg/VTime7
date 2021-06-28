<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.14
Dim mobjNetFrameWork As eNetFrameWork.Layout
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de la tabla Life
Dim mclsCertificat As ePolicy.Credit


'% insPreCT001: Realiza la lectura de los campos a mostrar en pantalla
'---------------------------------------------------------------------
Private Sub insPreCT001()
	'---------------------------------------------------------------------
	Dim mclsRoleses As Object
	Dim lintAction As Short
	
	lintAction = 1
	If Session("bQuery") Then
		lintAction = 0
	End If
	
	With mobjValues
		Call mclsCertificat.Find_CT001(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), lintAction)
		
	End With
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CT001")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mclsCertificat = New ePolicy.Credit
mobjValues.ActionQuery = Session("bQuery")
Call insPreCT001()
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 11 $|$$Date: 11/12/09 17:47 $|$$Author: Cidler $"

    
//% ShowChangeValues: Se cargan los valores de acuerdo al auto que se seleccione 
//-------------------------------------------------------------------------------------------
function ShowChangeValues(field){
//-------------------------------------------------------------------------------------------
	var nLimit = 0
	var nRate = 0
	var nPercent = 0
	var nvalue = 0
	var lstrString;
	
	with (self.document.forms[0])
	{
//	ldbValue = Request.QueryString("nLimit") * Request.QueryString("nRate") * Request.QueryString("nPercent) / 1000

		lstrString = "nLimitCurrent=" + tcnLimitCurrent.value + "&nRate=" + tcnRate.value + "&nPercent=" + tcnPercentPremium.value;
		insDefValues("PremiumMin", lstrString, '/VTimeNet/Policy/PolicySeq');
		//else
		//{
		//	tcnMinPremium.value = 0 ;
		//}
	}

}    
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
    <%Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCT001" ACTION="valPolicySeq.aspx?nMainAction=301&nHolder=1">
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=13518><%= GetLocalResourceObject("tcnLimitRequestCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnLimitRequest", 18, CStr(mclsCertificat.nLimitRequest),  , GetLocalResourceObject("tcnLimitRequestToolTip"), True, 6)%></TD>
		</TR>
        <TR>
			<%If Session("nCertif") = 0 Then%>
				<TD><LABEL COLSPAN="2" ID=13518><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<%Else%>
				<TD><LABEL COLSPAN="2" ID=13518><%= GetLocalResourceObject("tcnLimitCurrentCaption") %></LABEL></TD>
			<%End If%>
			<TD COLSPAN="2" ><%=mobjValues.NumericControl("tcnLimitCurrent", 18, CStr(mclsCertificat.nLimitCurrent),  , GetLocalResourceObject("tcnLimitCurrentToolTip"), True, 6,  ,  ,  , "ShowChangeValues(this);")%></TD>
		</TR>
        <TR>
			<TD><LABEL COLSPAN="2" ID=13518><%= GetLocalResourceObject("tcnLimitNoPayrollCaption") %></LABEL></TD>
			<TD COLSPAN="2" ><%=mobjValues.NumericControl("tcnLimitNoPayroll", 18, CStr(mclsCertificat.nLimitNoPayroll),  , GetLocalResourceObject("tcnLimitNoPayrollToolTip"), True, 6,  ,  ,  ,  , Session("nCertif") <> 0)%></TD>
		</TR>

        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41077><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        </TR>
		<TR>
			<TD COLSPAN="2" CLASS="Horline"></TD>
		</TR>        
        <TR>
        </TR>
        <TR>
			<TD><LABEL ID=13518><%= GetLocalResourceObject("tcnRateCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnRate", 8, CStr(mclsCertificat.nRate),  , GetLocalResourceObject("tcnRateToolTip"), True, 6,  ,  ,  , "ShowChangeValues(this);", Session("nCertif") <> 0)%></TD>
		</TR>		
        <TR>
			<TD><LABEL ID=13518><%= GetLocalResourceObject("tcnPercentPremiumCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPercentPremium", 18, CStr(mclsCertificat.nPercentPremium),  , GetLocalResourceObject("tcnPercentPremiumToolTip"), True, 6,  ,  ,  , "ShowChangeValues(this);", Session("nCertif") <> 0)%></TD>
		</TR>
        <TR>
			<TD><LABEL ID=13518><%= GetLocalResourceObject("tcnMinPremiumCaption") %> </LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnMinPremium", 18, CStr(mclsCertificat.nMinPremium),  , GetLocalResourceObject("tcnMinPremiumToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=13518><%= GetLocalResourceObject("cbeAjustTypeCaption") %></LABEL></TD>        
			<TD><%=mobjValues.PossiblesValues("cbeAjustType", "TABLE9003", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCertificat.nAjustType), False, False,  ,  ,  ,  , Session("nCertif") <> 0,  , GetLocalResourceObject("cbeAjustTypeToolTip"))%></TD>
        </TR>

        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41077><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
        </TR>
		<TR>
			<TD COLSPAN="2" CLASS="Horline"></TD>
		</TR>   
        <TR>
			<TD><LABEL ID=13518><%= GetLocalResourceObject("cbeMateriaCaption") %></LABEL></TD>        
			<TD><%=mobjValues.PossiblesValues("cbeMateria", "Table9002", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCertificat.nMateria), False, False,  ,  ,  ,  , Session("nCertif") <> 0,  , GetLocalResourceObject("cbeMateriaToolTip"))%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=13518><%= GetLocalResourceObject("cbeClassClientCaption") %></LABEL></TD>       
			<TD><%If Session("nCertif") = 0 And CStr(Session("sPolitype")) = "2" Then
	Response.Write(mobjValues.PossiblesValues("cbeClassClient", "TABLE9004", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCertificat.nClassClient), False, False,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeClassClientToolTip")))
Else
	Response.Write(mobjValues.PossiblesValues("cbeClassClient", "TABLE9004", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCertificat.nClassClient), False, False,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeClassClientToolTip")))
End If%></TD>
        </TR>   
        <TR>
			<TD><LABEL ID=13518><%= GetLocalResourceObject("tcnAgeCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAge", 5, CStr(mclsCertificat.nAge),  , GetLocalResourceObject("tcnAgeToolTip"), False, 0)%></TD>
			<%=mobjValues.HiddenControl("hddPoliType", Session("sPolitype"))%>
			<%=mobjValues.HiddenControl("hddCertif", Session("nCertif"))%>
        </TR>        
    </TABLE>
<%
Response.Write(mobjValues.BeginPageButton)
mobjValues = Nothing
mobjMenu = Nothing
mclsCertificat = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.14
Call mobjNetFrameWork.FinishPage("CT001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




