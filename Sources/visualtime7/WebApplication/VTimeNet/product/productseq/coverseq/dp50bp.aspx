<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'% insPreDP50BP: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP50BP()
	'--------------------------------------------------------------------------------------------
	Dim lclsLife_cover As eProduct.Life_cover
	lclsLife_cover = New eProduct.Life_cover
	Call lclsLife_cover.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	With lclsLife_cover
		
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=100139><A NAME=""Tabla de Mortalidad"">" & GetLocalResourceObject("AnchorTabla de MortalidadCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=14752>" & GetLocalResourceObject("tctMortacomCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.TextControl("tctMortacom", 6, .sMortacom,  , GetLocalResourceObject("tctMortacomToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=14751>" & GetLocalResourceObject("tctMortacofCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.TextControl("tctMortacof", 6, .sMortacof,  , GetLocalResourceObject("tctMortacofToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("tcnInterestCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnInterest", 4, CStr(.nInterest),  , GetLocalResourceObject("tcnInterestToolTip"),  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("tcnPer_tabmorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnPer_tabmor", 5, CStr(.nPer_tabmor),  , GetLocalResourceObject("tcnPer_tabmorToolTip"),  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Gastos"">" & GetLocalResourceObject("AnchorGastosCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		<TD></TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Gastos"">" & GetLocalResourceObject("AnchorGastos2Caption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		<TD></TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=14754>" & GetLocalResourceObject("tcnPrintexpCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnPrintexp", 6, CStr(.nPrintexp),  , GetLocalResourceObject("tcnPrintexpToolTip"),  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=19395>" & GetLocalResourceObject("tcnCaintexpCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnCaintexp", 6, CStr(.nCaintexp),  , GetLocalResourceObject("tcnCaintexpToolTip"),  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=14753>" & GetLocalResourceObject("tcnPrextexpCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnPrextexp", 6, CStr(.nPrextexp),  , GetLocalResourceObject("tcnPrextexpToolTip"),  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=19396>" & GetLocalResourceObject("tcnCaextexpCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnCaextexp", 6, CStr(.nCaextexp),  , GetLocalResourceObject("tcnCaextexpToolTip"),  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=100142><A NAME=""Valores"">" & GetLocalResourceObject("AnchorValoresCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=14757>" & GetLocalResourceObject("tctRoureserCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.TextControl("tctRoureser", 12, .sRoureser,  , GetLocalResourceObject("tctRoureserToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=14757>" & GetLocalResourceObject("tctsRoutresriskCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.TextControl("tctsRoutresrisk", 12, .sRoutresrisk,  , GetLocalResourceObject("tctsRoutresriskToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("tctRouClaTecCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.TextControl("tctRouClaTec", 12, .sRouClaTec,  , GetLocalResourceObject("tctRouClaTecToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=14758>" & GetLocalResourceObject("tctRousurreCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctRousurre", 12, .sRousurre,  , GetLocalResourceObject("tctRousurreToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("</TABLE>")

		
	End With
	lclsLife_cover = Nothing
End Sub

</script>
<%Response.Expires = 0
mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "dp50bp"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



	
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP50BP", "DP50BP.aspx"))
		mobjMenu = Nothing
	End If
End With
%>

<SCRIPT>
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:04 $"
</SCRIPT>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP50BP" ACTION="valCoverseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP50BP"))
Call insPreDP50BP()
%>
</FORM>
<BODY>
<HTML>





