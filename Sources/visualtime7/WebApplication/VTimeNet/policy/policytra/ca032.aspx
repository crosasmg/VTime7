<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mstrQueryString As String

'- Objeto para el manejo de las rutinas genéricas
Dim mclsValPolicyTra As ePolicy.ValPolicyTra


</script>
<%Response.Expires = 0

'- Variables que contendrán la información que está en las variables de Sesión
With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mclsValPolicyTra = New ePolicy.ValPolicyTra
End With

With Request
	mstrQueryString = "nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif")
End With
%>

<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet)
	.Write(mobjMenu.setZone(2, "CA032", "CA032.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmNullPolicy" ACTION="ValPolicyTra.aspx?<%=mstrQueryString%>">
<%
Response.Write(mobjValues.ShowWindowsName("CA032"))
With Request
	mclsValPolicyTra.insPreCA032("2", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble))
End With
%>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="3">&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=101245><A NAME="Transaccion"><%= GetLocalResourceObject("AnchorTransaccionCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="3"></TD>
            <TD COLSPAN="2" CLASS="HORLINE"></TD> 
        </TR>
        <TR>
            <TD COLSPAN="3"><%=mobjValues.CheckControl("chkNullReceipt", GetLocalResourceObject("chkNullReceiptCaption"), CStr(1), CStr(1),  , mclsValPolicyTra.bNullReceipt)%></TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(101247, "optTransac", GetLocalResourceObject("optTransac_1Caption"), "1", "1",  , True)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="3"><%=mobjValues.CheckControl("chkNullPropQuot", GetLocalResourceObject("chkNullPropQuotCaption"), "1", CStr(1),  , True)%></TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(101248, "optTransac", GetLocalResourceObject("optTransac_2Caption"),  , "2",  , True)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="3">&nbsp;</TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(101249, "optTransac", GetLocalResourceObject("optTransac_3Caption"),  , "3",  , True)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="3">&nbsp;</TD>
            <TD COLSPAN="2"><%= mobjValues.OptionControl(101249, "optTransac", GetLocalResourceObject("optTransac_4Caption"), , "4", , True)%></TD>
        </TR>
	</TABLE>
	<BR>
	<TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=101246><%= GetLocalResourceObject("tcdTransDateCaption") %></LABEL></TD>
            <TD COLSPAN="2"><%=mobjValues.DateControl("tcdTransDate", CStr(mclsValPolicyTra.dTransDate),  , GetLocalResourceObject("tcdTransDateToolTip"), True,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD WIDTH=20%><LABEL ID=13794><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD WIDTH=20%><LABEL ID=41125><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
			<TD WIDTH=60%><LABEL ID=41126><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
        </TR>
        <TR>
			<TD><LABEL ID=13797><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
			<TD><LABEL ID=41127><%= GetLocalResourceObject("Anchor5Caption") %></LABEL></TD>
			<TD><LABEL ID=41128><%= GetLocalResourceObject("Anchor6Caption") %></LABEL></TD>
        </TR>
    </TABLE>
	
	<%=mobjValues.HiddenControl("tcnNullOutMov", CStr(mclsValPolicyTra.nNullOutMov))%>
	<%=mobjValues.HiddenControl("tctReverCertif", mclsValPolicyTra.sReverCertif)%>
    <%="<SCRIPT> self.document.forms[0].optTransac[" & mclsValPolicyTra.nTransactio & "].checked = true </SCRIPT>"%>
</FORM>
</BODY>
</HTML>
<%
mclsValPolicyTra = Nothing
mobjValues = Nothing
%>





