<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'% insPreDP025: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP025()
	'--------------------------------------------------------------------------------------------
	Dim lclsProduct_li As eProduct.Product
	Dim lclsProduct As eProduct.Product
	lclsProduct_li = New eProduct.Product
	lclsProduct = New eProduct.Product
	
	Call lclsProduct.Find(Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	With mobjValues
		Call lclsProduct_li.FindProduct_li(.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	End With
	With lclsProduct_li
		
Response.Write("" & vbCrLf)
Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=14877>" & GetLocalResourceObject("tcnClaim_PresCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.NumericControl("tcnClaim_Pres", 3, CStr(lclsProduct.nClaim_pres),  , GetLocalResourceObject("tcnClaim_PresToolTip"),  , 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD >&nbsp;</TD>" & vbCrLf)
Response.Write("				" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("tcnClaim_NoticeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.NumericControl("tcnClaim_Notice", 3, CStr(lclsProduct.nClaim_Notice),  , GetLocalResourceObject("tcnClaim_NoticeToolTip"),  , 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD >&nbsp;</TD>	" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("tcnClaim_PayCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.NumericControl("tcnClaim_Pay", 3, CStr(lclsProduct.nClaim_Pay),  , GetLocalResourceObject("tcnClaim_PayToolTip"),  , 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD >&nbsp;</TD>	" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=""5"" CLASS=""HorLine""></TD>	" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.CheckControl("chkClSimpai", GetLocalResourceObject("chkClSimpaiCaption"),  , CStr(1)))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=""2"">&nbsp;</TD>	" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.CheckControl("chkClnoprei", GetLocalResourceObject("chkClnopreiCaption"),  , CStr(1)))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD >&nbsp;</TD>	" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.CheckControl("chkClSurrei", GetLocalResourceObject("chkClSurreiCaption"),  , CStr(1)))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=""2"">&nbsp;</TD>	" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.CheckControl("chkClpaypri", GetLocalResourceObject("chkClpaypriCaption"),  , CStr(1)))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>	" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.CheckControl("chkClAllpre", GetLocalResourceObject("chkClAllpreCaption"),  , CStr(1)))


Response.Write("</TD>            " & vbCrLf)
Response.Write("				<TD COLSPAN=""2"">&nbsp;</TD>	" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.CheckControl("chkClTransi", GetLocalResourceObject("chkClTransiCaption"),  , CStr(1),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>	" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.CheckControl("chkClAmountap", GetLocalResourceObject("chkClAmountapCaption"),  , CStr(1)))


Response.Write("</TD>            " & vbCrLf)
Response.Write("				<TD COLSPAN=""2"">&nbsp;</TD>	" & vbCrLf)
Response.Write("				<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=14874>" & GetLocalResourceObject("tctClannpeiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD >")


Response.Write(mobjValues.TextControl("tctClannpei", 12, .sClannpei,  , GetLocalResourceObject("tctClannpeiToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>	" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=14876>" & GetLocalResourceObject("tctCllifeaiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD >")


Response.Write(mobjValues.TextControl("tctCllifeai", 12, .sCllifeai,  , GetLocalResourceObject("tctCllifeaiToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>	" & vbCrLf)
Response.Write("		</TABLE>")

		
		Response.Write("<SCRIPT language=Javascript>")
		Response.Write("Checked(" & mobjValues.StringToType(.sClallpre, eFunctions.Values.eTypeData.etdDouble) & "," & mobjValues.StringToType(.sClnoprei, eFunctions.Values.eTypeData.etdDouble) & "," & mobjValues.StringToType(.sClpaypri, eFunctions.Values.eTypeData.etdDouble) & "," & mobjValues.StringToType(.sClsimpai, eFunctions.Values.eTypeData.etdDouble) & "," & mobjValues.StringToType(.sClsurrei, eFunctions.Values.eTypeData.etdDouble) & "," & mobjValues.StringToType(.sCltransi, eFunctions.Values.eTypeData.etdDouble) & "," & mobjValues.StringToType(CStr(.nProdClas), eFunctions.Values.eTypeData.etdDouble) & "," & mobjValues.StringToType(.sIndCl_Pay, eFunctions.Values.eTypeData.etdDouble) & ");")
		Response.Write("</" & "Script>")
	End With
	lclsProduct_li = Nothing
End Sub

</script>
<%Response.Expires = 0
mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "dp025"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT LANGUAGE=JAVASCRIPT>
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:08 $"
//%insStateZone: Permite habilitar/deshabilitar los campos de la ventana
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}
//% insCancel: Se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	top.close()
	return true;
}
//% insFinish: Ejecuta la acción de Finalizar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	return true;
}    
//% Checked: Setea los valores de chechek.
//------------------------------------------------------------------------------------------
function Checked(sClallpre,sClnoprei,sClpaypri,sClsimpai,sClsurrei,sCltransi,nProdClas,sIndCl_Pay)
//------------------------------------------------------------------------------------------
{
    with (self.document.forms[0])
    {
		if (sClallpre == 1)
			{chkClAllpre.checked = true;}
		if (sClnoprei == 1)
			{chkClnoprei.checked = true;}
		if (sClpaypri == 1)
			{chkClpaypri.checked = true;}
		if (sClsimpai == 1)
			{chkClSimpai.checked = true;}
		if (sClsurrei == 1)
			{chkClSurrei.checked = true;}
		if (nProdClas == 5)
			{chkClTransi.disabled = false;
			 if (sCltransi == 1)
				{chkClTransi.checked = true;}
			}
		else
			{chkClTransi.disabled = true;}
		if (sIndCl_Pay == 1)
			{chkClAmountap.checked = true;}
    }
}
</SCRIPT>
    <%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP025", "DP025.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP025" ACTION="valProdLifeSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP025"))
Call insPreDP025()
%></FORM>
</BODY>
</HTML>




