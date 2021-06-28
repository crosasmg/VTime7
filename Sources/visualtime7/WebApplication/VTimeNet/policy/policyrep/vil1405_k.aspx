<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% inspreVIL1405: se definen los campos de la forma
'--------------------------------------------------------------------------------------------
Private Sub insPreVIL1405()
	'--------------------------------------------------------------------------------------------
	
Response.Write("	" & vbCrLf)
Response.Write("<BR><BR>" & vbCrLf)
Response.Write("	")

	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	    <BR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  , "insChargeProduct(this)",  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(0), eFunctions.Values.eValuesType.clngWindowType, True, vbNullString,  ,  ,  , "GetDate_value()",  , 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdEffecDate",  ,  , GetLocalResourceObject("tcdEffecDateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN = 3>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	Response.Write(mobjValues.HiddenControl("hddnIdproces", ""))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "VIL1405_K"
mobjMenu = New eFunctions.Menues
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 11/09/03 12:45 $|$$Author: Nvaplat37 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insStateZone: Se controla el estado de los campos de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

//% insChargeProduct: Se cargan los parámetros del campo producto.
//------------------------------------------------------------------------------------------
function insChargeProduct(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value!=0) {
		with(self.document.forms[0]){
			valProduct.disabled=false;
			btnvalProduct.disabled=false;
			valProduct.value="";
			UpdateDiv("valProductDesc", "");
		}
    }
}
//% GetDate_value: Obtiene la última fecha válida y le suma 1 día
//------------------------------------------------------------------------------------------
function GetDate_value()
//------------------------------------------------------------------------------------------
{
	insDefValues("Date_value", "nBranch=" + self.document.forms[0].elements['cbeBranch'].value +
	                           "&nProduct=" + self.document.forms[0].elements['valProduct'].value + "&sCodisplOri=VIL1405", '/VTimeNet/Policy/PolicyRep');
}
</SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "VIL1405_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, "VIL1405", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="VIL1405" ACTION="valPolicyRep.aspx?sMode=1">
<%Call insPreVIL1405()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>






