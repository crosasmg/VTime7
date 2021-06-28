<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues

Dim mclsBal_Histor As eLedge.Bal_histor


Dim mintAccount As Object


'----------------------------------------------------------------------------
Private Sub insLoadCP005_k()
	'----------------------------------------------------------------------------
	Response.Write(mobjValues.ButtonLedCompan("LedCompan", Session("nLedCompan"), GetLocalResourceObject("LedCompanToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=4 WIDTH=""25%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.CheckControl("chkUnmat", GetLocalResourceObject("chkUnmatCaption"),  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.CheckControl("chkFutureMonth", GetLocalResourceObject("chkFutureMonthCaption"),  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    <TD COLS=""2"" >")


Response.Write(mobjValues.CheckControl("chkProcess", GetLocalResourceObject("chkProcessCaption"),  ,  ,  , True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.CheckControl("chkAutomat", GetLocalResourceObject("chkAutomatCaption"),  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.CheckControl("chkRepeat", GetLocalResourceObject("chkRepeatCaption"),  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=11624>" & GetLocalResourceObject("gmdDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.DateControl("gmdDate", CStr(Today),  , GetLocalResourceObject("gmdDateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=2 >" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("	    <TD WIDTH=""40%"" CLASS=""HighLighted""><LABEL><A NAME=""Número de asiento"">" & GetLocalResourceObject("AnchorNúmero de asientoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	    <TD WIDTH=""10%"" >&nbsp</TD>" & vbCrLf)
Response.Write("	    <TD WIDTH=""40%"" CLASS=""HighLighted""><LABEL><A NAME=""Total"">" & GetLocalResourceObject("AnchorTotalCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD><HR></TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp</TD>" & vbCrLf)
Response.Write("	    <TD><HR></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"" COLS=5>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=11628 WIDTH=""25%"" >" & GetLocalResourceObject("tcnNumberCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""30%"" >")


Response.Write(mobjValues.NumericControl("tcnNumber", 15, CStr(0),  , GetLocalResourceObject("tcnNumberToolTip"),  , 0,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""10%"" >&nbsp</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=11626 WIDTH=""20%"" >" & GetLocalResourceObject("lblDebitCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""25%"" >")


Response.Write(mobjValues.TextControl("lblDebit", 30, CStr(0),  , GetLocalResourceObject("lblDebitToolTip"), True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=11629>" & GetLocalResourceObject("tcnNumOffiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnNumOffi", 15, CStr(0),  , GetLocalResourceObject("tcnNumOffiToolTip"),  , 0,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=11623>" & GetLocalResourceObject("lblCreditCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.TextControl("lblCredit", 30, CStr(0),  , GetLocalResourceObject("lblCreditToolTip"), True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("	</TABLE>")

	
	
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsBal_Histor = New eLedge.Bal_histor

mobjValues.sCodisplPage = "CP005_K"
%>

<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}

//% insStateZone: Se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
    self.document.forms[0].chkUnmat.disabled=false;
    self.document.forms[0].chkFutureMonth.disabled=false;
    self.document.forms[0].chkProcess.disabled=false;
    self.document.forms[0].chkAutomat.disabled=false;
    self.document.forms[0].chkRepeat.disabled=false;
    self.document.forms[0].gmdDate.disabled=false;
    self.document.forms[0].tcnNumber.disabled=false;
    self.document.forms[0].tcnNumOffi.disabled=false;
}
</SCRIPT>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>

<%With Response
	.Write(mobjValues.StyleSheet)
	.Write(mobjMenu.MakeMenu("CP005", "CP005_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmHisBalance" ACTION="ValLedGerTra.aspx?sTime=1">
<BR>
<%
Call insLoadCP005_k()
%>	
</BODY>
</FORM>
</HTML>





