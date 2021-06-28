<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos 
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinanCO As eFinance.financeCO
	lclsFinanCO = New eFinance.financeCO
	
	Call lclsFinanCO.Find(session("nContrat"), Today)
	With lclsFinanCO
		
		'chkPayment_in.Value = IIf(.sPayment_in = eafirmative, eFunctions.Values.vbChecked, eFunctions.Values.vbUnChecked)
		
		
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN = 5 CLASS=""HighLighted""><LABEL><A NAME=""Datos del contrato"">" & GetLocalResourceObject("AnchorDatos del contratoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN = 5><HR></TD>	" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=11092>" & GetLocalResourceObject("cbeOfficeCaption") & "</LABEL></TD>  " & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeOffice", "table9", 1, CStr(.nOffice),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOfficeToolTip")))


Response.Write("</TD>      " & vbCrLf)
Response.Write("			<TD><LABEL ID=11077>" & GetLocalResourceObject("cbeCurr_contCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurr_cont", "table11", 1, CStr(.nCurrency),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurr_contToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH =""22%"" ><LABEL ID=11082>" & GetLocalResourceObject("dtcCodClieCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN = 3>")


Response.Write(mobjValues.ClientControl("dtcCodClie", .sClient,  , GetLocalResourceObject("dtcCodClieToolTip"),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=11090>" & GetLocalResourceObject("lblInteresCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("lblInteres", 15, CStr(.nInterest),  ,  , True, 2,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=11088>" & GetLocalResourceObject("lblInitiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("lblIniti", 19, CStr(.nInitial),  ,  , True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN = 2>")


Response.Write(mobjValues.CheckControl("chkPayment_in", GetLocalResourceObject("chkPayment_inCaption"),  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=11095>" & GetLocalResourceObject("lblQ_drafCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("lblQ_draf", 5, CStr(.nQ_draft),  ,  ,  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD colspan=2 ><LABEL ID=11085>" & GetLocalResourceObject("tcdFirst_drafCaption") & "</LABEL>" & vbCrLf)
Response.Write("            ")


Response.Write(mobjValues.DateControl("tcdFirst_draf", CStr(.dFirst_draf),  , GetLocalResourceObject("tcdFirst_drafToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=11086>" & GetLocalResourceObject("cbeFrequencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeFrequency", "table250", 1, CStr(.nFrequency),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeFrequencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD colspan = 2><LABEL ID=11084>" & GetLocalResourceObject("tcnDscto_amoCaption") & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</LABEL>" & vbCrLf)
Response.Write("            ")


Response.Write(mobjValues.NumericControl("tcnDscto_amo", 19, CStr(.nDscto_amo),  ,  , True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN = 5 CLASS=""HighLighted""><LABEL><A NAME=""Información de la anulación"">" & GetLocalResourceObject("AnchorInformación de la anulaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN = 5><HR></TD>	" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=11079>" & GetLocalResourceObject("cbeCauseCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCause", "table255", 1,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCauseToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>            " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=11093>" & GetLocalResourceObject("cbeOptionCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeOption", "table254", 1,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOptionToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    </TABLE>")

		
		
		Response.Write("<SCRIPT>")
		Response.Write("ChangeValues('" & mobjValues.StringToType(CStr(.sPayment_in), eFunctions.Values.eTypeData.etdDouble) & "');")
		Response.Write("</" & "Script>")
	End With
	lclsFinanCO = Nothing
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues


mobjValues.ActionQuery = session("bQuery")

mobjValues.sCodisplPage = "fi006"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
function ChangeValues(sPayment_in)
{
	with (self.document.forms[0])
	{	if (sPayment_in == 1)
		{ chkPayment_in.checked = true;}
	}
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
        <%
With Response
	.Write(mobjValues.ShowWindowsName("FI006"))
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "FI006", "FI006.aspx"))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAnulContrat" ACTION="valFinancing.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call insDefineHeader()
%>
</FORM>
</BODY>
</HTML>




