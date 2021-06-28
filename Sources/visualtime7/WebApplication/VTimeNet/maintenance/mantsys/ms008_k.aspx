<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "ms008_k"
%>


<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/ValFunctions.js"></SCRIPT>


<HTML>
	<HEAD>
	<SCRIPT>
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return(true);
}
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
	self.document.forms[0].cbeInquiry.disabled = false

}
//------------------------------------------------------------------------------------------
function insPreZone(llngAction)
//------------------------------------------------------------------------------------------
{
}
//------------------------------------------------------------------------------------------
function insCancel()		
//------------------------------------------------------------------------------------------
{
	return true
}
</SCRIPT>
		<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
		<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("MS008", "MS008_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
	</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<TABLE border="0">
		<TD><BR></TD>
		<TD></BR></TD>
		<FORM METHOD="post" ID="FORM" NAME="MS008" ACTION="valMantsys.aspx?x=1">
			<TR>
				<td WIDTH="15%"><label ID="101880"><%= GetLocalResourceObject("cbeInquiryCaption") %></LABEL></TD>
				<TD><%=mobjValues.PossiblesValues("cbeInquiry", "TABLE1014", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeInquiryToolTip"))%></TD>
			</TR>
		</FORM>
		</TABLE>
	</BODY>
</HTML>




