<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.41
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


Dim lclsLife As ePolicy.Life


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vi002")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.41
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vi002"
lclsLife = New ePolicy.Life

Call lclsLife.Find(CStr(Session("sCertype")), mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCertif")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate))

If Request.QueryString("sSource") = "SI091" Then
	mobjValues.ActionQuery = False
Else
        If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
            mobjValues.ActionQuery = Session("bQuery")
        Else
            mobjValues.ActionQuery = False
            Session("bQuery") = False
        End If
End If

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.41
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.setZone(2, "VI002", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
Response.Write(mobjValues.StyleSheet())
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmVI002" ACTION="VTimeNet\Policy\PolicySeq\valPolicySeq.aspx?sTime=1&sSource=<%=Request.QueryString("sSource")%>">
	<P ALIGN="Center">
		<%If Request.QueryString("sSource") = "SI091" Then%>
			<LABEL ID=100720><a HREF="#Clientes">Clientes</a></LABEL><LABEL ID=0> | </LABEL>
		<%End If%>
		<LABEL ID=40654><a HREF="#Montos">Clave</a></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40656><a HREF="#Fechas">Tipo de negocio</a></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40658><a HREF="#Pagos">Tipo de póliza</a></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40660><a HREF="#Situación actual">Relaciones</a></LABEL>
	</P>	    
    <%=mobjValues.ShowWindowsName("VI002", Request.QueryString("sWindowDescript"))%>
    <P ALIGN="CENTER">
        <TABLE>
			<TR>
				<TD><%=mobjValues.OptionControl(100740, "OptLoanType", "Préstamo", , "1")%></TD>
				<TD WIDTH=50pcx>&nbsp;</TD>
				<TD><%=mobjValues.OptionControl(100741, "OptLoanType", "Arrendamiento", , "2")%></TD>
			</TR>
		</TABLE>
		<TABLE>
			<TR>
				<TD><LABEL ID=100720>Nro. Préstamo/Arrendamiento</LABEL></TD>
				<TD WIDTH=20pcx>&nbsp;</TD>
				<TD><%=mobjValues.TextControl("tctLoan", 20)%></TD>
			</TR>
		</TABLE>
    </P>
    <TABLE WIDTH="100%" COLS=5>
    <%If Request.QueryString("sSource") = "SI091" Then%>
		<TR>
			<TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=100721><A NAME="Clientes">Clientes</A></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="5"><HR></TD>
		</TR>		
		<TR>
			<TD><LABEL ID=100722>Prestatario</LABEL>
			<TD COLSPAN = "4"><%=mobjValues.ClientControl("valBorrower", "",  , "",  ,  , "lblBorrower", False,  ,  ,  , eFunctions.Values.eTypeClient.SearchClient)%></TD>
		</TR> 
		<TR>
			<TD><LABEL ID=100724>CoPrestatario</LABEL>
			<TD COLSPAN = "4"><%=mobjValues.ClientControl("valCoBorrower", "",  , "",  ,  , "lblCoBorrower", False,  ,  ,  , eFunctions.Values.eTypeClient.SearchClient)%></TD>
		</TR>
			<TD><LABEL ID=0>Concesionario</LABEL>
			<TD COLSPAN = "4"><%	mobjValues.ClientRole = CStr(51)
	'mobjValues.nCertif = 0
	Response.Write(mobjValues.ClientControl("valCarDealer", "",  , "Comerciante de vehiculos",  ,  , "lblCliename", False,  ,  ,  , eFunctions.Values.eTypeClient.SearchClientPolicy))
	%>
			</TD>		
		</TR> 
	<%End If%>
        <TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100726><A NAME="Montos">Montos</A></LABEL></TD>
			<TD width=15%>&nbsp;</TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100727><A NAME="Fechas">Fechas</A></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="2"><HR></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><HR></TD>
		</TR>
		<TR>
            <TD><LABEL ID=100728>Total Préstamo/Arrendamiento</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnTotalLoan", 18, ,  , "", True, 6)%></TD> 
            <TD>&nbsp;</TD>
            <TD><LABEL ID=100729>Efecto</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdApplication")%></TD>
            
        </TR>
        <TR>
            <TD><LABEL ID=100730>Gastos</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnInstalment", 18, ,  , "", True, 6)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=100731>Firma</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdSigning")%></TD>
        </TR>
        <TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100732><A NAME="Pagos">Pagos</A></LABEL></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=100733>Pago</LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdDisbursement")%></TD>
        </TR>
        <TR>
    		<TD COLSPAN="2"><HR></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=100734>Expiración</LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdMaturity")%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=100760>Monto</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tctPay_amount", 18, ,  ,  , True, 6)%></TD>            
            <TD>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100736><A NAME="Situación actual">Situación actual al <%=Session("dEffecdate")%></A></LABEL></TD>
        </TR>
        <TR>
            <TD><LABEL ID=100735>Frecuencia</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeAmortize_way", "Table9004", eFunctions.Values.eValuesType.clngComboType)%></TD>
			<TD>&nbsp;</TD>
    		<TD COLSPAN="2"><HR></TD>
		</TR>
		<TR>
            <TD><LABEL ID=100737>Método</LABEL></TD>
            <TD><%=mobjValues.OptionControl(100742, "optPayWay", "EFT", , "1")%></TD>
		    <TD>&nbsp;</TD>
		    <TD><LABEL ID=100738>Balance del préstamo</LABEL></TD>
		    <TD><%=mobjValues.NumericControl("tcnBalance", 18, ,  , "", True, 6)%></TD>
		</TR>
		<TR>
		    <TD>&nbsp;</TD>
		    <TD><%=mobjValues.OptionControl(100743, "optPayWay", "Cheque", , "2")%></TD>
			<TD>&nbsp;</TD>
		    <TD><LABEL ID=100739>Próximo pago</LABEL></TD>
		    <TD><%=mobjValues.DateControl("tcdNextPayment", ,  , "")%></TD>
        </TR>
    </TABLE>
<%=mobjValues.BeginPageButton%>
<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object lclsLife may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
lclsLife = Nothing
%>
</FORM>
</BODY>
</HTML>
            
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.41
Call mobjNetFrameWork.FinishPage("vi002")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




