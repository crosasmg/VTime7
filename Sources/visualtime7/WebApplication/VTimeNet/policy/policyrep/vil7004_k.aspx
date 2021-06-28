<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues



'%insPreVIL7004: Se cargan los controles de la ventana.
'----------------------------------------------------------------------------
Private Sub insPreVIL7004()
	'----------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("		")


Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))


Response.Write("" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>        " & vbCrLf)
Response.Write("            <TD <LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  , "insChargeProduct(this)",  , 3))


Response.Write("</TD>            " & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>                       " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
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
Response.Write("		<TR>                        " & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	")

	mobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1
Response.Cache.SetCacheability(HttpCacheability.NoCache)

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With
%>

<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 8/10/04 18:05 $"

//%insStateZone: Se habilita/deshabilita los campos de la ventana.
//------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------
}

//%insCancel: Acciones a efectuar al cancelar la transacción.
//------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------
	return true;
}

//%insFinish: Acciones a efectuar al finalizar la transacción.
//------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------
	return true;
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
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>


	
<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "VIL7004_K.aspx", 1, ""))
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
	
	'+ Se agrega zona para dejar des-habilitado el botón aceptar.
	.Write(mobjMenu.setZone(1, "VIL7004", ""))
End With

mobjMenu = Nothing
%>
</HEAD>
<BR></BR>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="VIL7004_K" ACTION="ValPolicyRep.aspx?mode=1">
<%
Call insPreVIL7004()
%>
</FORM>
</BODY>
</HTML>




