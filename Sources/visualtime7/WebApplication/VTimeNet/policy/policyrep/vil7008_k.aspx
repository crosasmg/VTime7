<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% inspreVIL7008: se definen los campos de la forma
'--------------------------------------------------------------------------------------------
Private Sub insPreVIL7008()
	'--------------------------------------------------------------------------------------------
	
Response.Write("	" & vbCrLf)
Response.Write("<BR><BR>	" & vbCrLf)
Response.Write("	")

	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Response.Write("" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("valCompanyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            ")

	mobjValues.Parameters.ReturnValue("sclient", True, "Cod Cliente", True)
Response.Write("" & vbCrLf)
Response.Write("         	<TD>")


Response.Write(mobjValues.PossiblesValues("valCompany", "TABTAB_FN_INSTITU2", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , False, 10, GetLocalResourceObject("valCompanyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  , "insChargeProduct(this)",  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(0), eFunctions.Values.eValuesType.clngWindowType, True, vbNullString,  ,  ,  , "ChangeProduct();", 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD> <LABEL ID=0>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "ChangePolicy();", True, 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD> <LABEL ID=0>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.NumericControl("tcnCertif", 10, vbNullString,  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  ,  , True, 6))


Response.Write("" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdatefromCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdEffecdatefrom",  ,  , GetLocalResourceObject("tcdEffecdatefromToolTip"),  ,  ,  ,  , False, 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdatetoCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdEffecdateto",  ,  , GetLocalResourceObject("tcdEffecdatetoToolTip"),  ,  ,  ,  , False, 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 21-08-09 2:18 $|$$Author: Mpalleres $"
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

//% ChangePolicy: se maneja el cambio de valor de los campos de la página
//-------------------------------------------------------------------------------------------
function ChangePolicy(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if(tcnPolicy.value=='')	{
			tcnCertif.value = 0;
			tcnCertif.disabled = false;
		}
		else
			insDefValues('ValPolitype', 'nBranch=' + cbeBranch.value + '&nProduct=' + valProduct.value + '&nPolicy=' + tcnPolicy.value + "&sExecCertif=1");
	}
}

//% ChangeProduct: se maneja el cambio de valor del Producto 
//-------------------------------------------------------------------------------------------
 function ChangeProduct(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if (cbeBranch.value != ''){
			if (cbeBranch.value != "0"){ 
				tcnPolicy.disabled = false;
			}
		}
	}
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

</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "VIL7008_k.aspx", 1, ""))
	.Write(mobjMenu.setZone(1, "VIL7008", "VIL7008_k.aspx"))
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With
mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="VIL7008" ACTION="valPolicyRep.aspx?sMode=1">
<%Call insPreVIL7008()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>






