<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'-   Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mstrQuote As String


'%   insDefineHeader: Permite cargar los campos del encabezado
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""60%"">" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=5>" & vbCrLf)
Response.Write("        <DIV ID=""divType_pun"" style=""left=5000;top=0"">" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"" BORDER=0>			" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=4 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Datos de la póliza"">" & GetLocalResourceObject("AnchorDatos de la pólizaCaption") & "</A></LABEL></TD>	    " & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=4 CLASS=""HORLINE""></TD>		" & vbCrLf)
Response.Write("			</TR>       " & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<td><LABEL ID=""8704"">" & GetLocalResourceObject("tcdIniDateCaption") & " </LABEL></td>" & vbCrLf)
Response.Write("                <td>")


Response.Write(mobjValues.DateControl("tcdIniDate",  ,  , GetLocalResourceObject("tcdIniDateToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("				<td><LABEL ID=""8703"">" & GetLocalResourceObject("tcdEndDateCaption") & "</LABEL></td>" & vbCrLf)
Response.Write("                <td>")


Response.Write(mobjValues.DateControl("tcdEndDate",  ,  , GetLocalResourceObject("tcdEndDateToolTip")))


Response.Write("</td> " & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("		</TABLE> " & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<br />" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("        	<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=4 CLASS=""HighLighted""><LABEL ID=LABEL2><A NAME=""Datos Opcionales"">" & GetLocalResourceObject("AnchorDatos OpcionalesCaption") & "</A></LABEL></TD>	    " & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=4 CLASS=""HORLINE""></TD>		" & vbCrLf)
Response.Write("			</TR>  " & vbCrLf)
Response.Write("       " & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=41208>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , "insChargeProduct(this)",  ,  , "",  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=40011>" & GetLocalResourceObject("valProductCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				")

	With mobjValues
		.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	
Response.Write("" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  ,  ,  , "", eFunctions.Values.eTypeCode.eString, 4))


Response.Write("</TD>	" & vbCrLf)
Response.Write("			</TR>  " & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD><LABEL ID=LABEL1>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("               <TD>")


Response.Write(mobjValues.OptionControl(0, "optAddressType", GetLocalResourceObject("optAddressType_0Caption"), CStr(1), "0",  , False))


Response.Write("" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.OptionControl(0, "optAddressType", GetLocalResourceObject("optAddressType_1Caption"),  , "1",  , False))


Response.Write("" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.OptionControl(0, "optAddressType", GetLocalResourceObject("optAddressType_2Caption"),  , "2",  , False))


Response.Write(" </TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("			   <TD><LABEL ID=LABEL3>" & GetLocalResourceObject("cbeBankCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeBank", "Table7", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeBankToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			    " & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("			   <TD><LABEL ID=LABEL6>" & GetLocalResourceObject("cbeWayPayCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	mobjValues.TypeList = 1
	mobjValues.List = "1,2"
	Response.Write(mobjValues.PossiblesValues("cbeWayPay", "Table5002", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeWayPayToolTip")))
Response.Write(" </TD>" & vbCrLf)
Response.Write("			    " & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			      " & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("		</TABLE>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("        </DIV>	        " & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("")

End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CAL00832_K"
mstrQuote = """"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>




<SCRIPT>
var nCost = 0;
var nCurrency = 0;

//%   insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%   insStateZone: Se controla el estado de los campos de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
//%   insChargeProduct: Se cargan los parámetros del campo producto.
//------------------------------------------------------------------------------------------
function insChargeProduct(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value!=0) {
	
		with(self.document.forms[0]){
			valProduct.disabled=false;
			btnvalProduct.disabled=false;
			valProduct1.value="";
			UpdateDiv("valProductDesc", "")
			valProduct.Parameters.Param1.sValue=lobject.value;
			valProduct.Parameters.Param2.sValue=0;

		}
    }
}

//%   insEnabledFields: Permite habilitar e inhabilitar los campos de la página.
//------------------------------------------------------------------------------------------
function insEnabledFields(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value!=1) {
	
	    ShowDiv('divType_pun', 'hide')
    }
    else{

	    ShowDiv('divType_pun', 'show')
	    
        with(self.document.forms[0]){
			cbeBranch1.value="";
			valProduct1.value="";
			tcnPolicy.value="";
			tcnCertif.value="";
			valProduct1Desc.value="";
			UpdateDiv("valProduct1Desc", "")
            dtcClientCO.value="";
            dtcClientCO_Digit.value="";
            UpdateDiv("dtcClientCO_Name", "")
            dtcClientAS.value="";
            dtcClientAS_Digit.value="";
            UpdateDiv("dtcClientAS_Name", "")
            tcdEffecdate.value="";
            tcdExpirdat.value="";
        }			
    }    
}

//%   insEnabledPolicy(): Permite habilitar e inhabilitar el campo Póliza.
//------------------------------------------------------------------------------------------
function insEnabledPolicy(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value) 
		self.document.forms[0].tcnPolicy.disabled=false;
    else{
        with(self.document.forms[0]){
			tcnPolicy.disabled=true;
			tcnPolicy.value="";
        }			
    }    
}


//%   insEnabledCertif(): Permite habilitar e inhabilitar el campo Certificado.
//------------------------------------------------------------------------------------------
function insEnabledCertif(lobject){
//------------------------------------------------------------------------------------------
    var lstrQueryString;
	var lintBranch  = 0;
	var lintProduct = 0;
    var llngPolicy  = 0;
    var llngCertif  = 0;

	lintBranch  = self.document.forms[0].elements[<%=mstrQuote%>cbeBranch1<%=mstrQuote%>].value
	lintProduct = self.document.forms[0].elements[<%=mstrQuote%>valProduct1<%=mstrQuote%>].value
	llngPolicy  = self.document.forms[0].elements[<%=mstrQuote%>tcnPolicy<%=mstrQuote%>].value
    llngCertif  = self.document.forms[0].elements[<%=mstrQuote%>tcnCertif<%=mstrQuote%>].value

    if (lobject == "nPolicy"){
		insDefValues('ShowDataProp','nBranch='   + lintBranch  +
									'&nProduct=' + lintProduct +
									'&nPolicy='  + llngPolicy,'/VTimeNet/policy/policyrep/');
    }
    else
		if (lobject == "nCertif"){
			insDefValues('ShowDataPropCer', 'nBranch='+ lintBranch    +
				                           '&nProduct='+ lintProduct +
					                       '&nPolicy='+ llngPolicy   +
						                   '&nCertif='+ llngCertif,'/VTimeNet/policy/policyrep/');
        }
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CAL1415_K.aspx", 1, ""))
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With

mobjMenu = Nothing
%>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CAL1415" ACTION="valPolicyRep.aspx?Mode=1">
	<BR><BR>
<%
Call insDefineHeader()

mobjValues = Nothing
%>

</FORM>
</BODY>
</HTML>






