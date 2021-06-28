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
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=5>" & vbCrLf)
Response.Write("        <DIV ID=""divType_pun"" style=""left=5000;top=0"">" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.OptionControl(0, "optEje", GetLocalResourceObject("optEje_1Caption"), CStr(1), "1",  , False))


Response.Write(" </TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.OptionControl(0, "optEje", GetLocalResourceObject("optEje_2Caption"),  , "2",  , False))


Response.Write(" </TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=41208>" & GetLocalResourceObject("cbeBranch1Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.PossiblesValues("cbeBranch1", "Table10", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , "insChargeProduct(this)",  ,  , "",  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=40011>" & GetLocalResourceObject("valProduct1Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				")

	With mobjValues
		.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	
Response.Write("" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.PossiblesValues("valProduct1", "tabProdmaster", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "insEnabledPolicy(this)",  ,  , "", eFunctions.Values.eTypeCode.eString, 4))


Response.Write("</TD>			" & vbCrLf)
Response.Write("			</TR>        " & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("		  		<TD> <LABEL ID=40281>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "insEnabledCertif('nPolicy')",  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=41370>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.NumericControl("tcnCertif", 5, vbNullString,  , "",  , 0,  ,  ,  , "insEnabledCertif('nCertif')", False, 6))


Response.Write("			" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			<tr>" & vbCrLf)
Response.Write("	      <td><label>" & GetLocalResourceObject("cbeOfficeCaption") & "</label></td>" & vbCrLf)
Response.Write("	      <td>")


Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1)", False,  , GetLocalResourceObject("cbeOfficeToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("	    <tr>" & vbCrLf)
Response.Write("	      <td><label>" & GetLocalResourceObject("cbeOfficeAgenCaption") & "</label></td>" & vbCrLf)
Response.Write("		  <td>")

	
	mobjValues.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.ReturnValue("nBran_off",  ,  , True)
	Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "insInitialAgency(2)", False,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		   </td>" & vbCrLf)
Response.Write("		   <TR>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("cbeAgencyCaption") & "</label></td>" & vbCrLf)
Response.Write("		  <td>")

	
	mobjValues.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.ReturnValue("nBran_off",  ,  , True)
	mobjValues.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
	mobjValues.Parameters.ReturnValue("sDesAgen",  ,  , True)
	Response.Write(mobjValues.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "insInitialAgency(3)", False,  , GetLocalResourceObject("cbeAgencyToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		   </td>" & vbCrLf)
Response.Write("		   </TR>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=LABEL1>" & GetLocalResourceObject("dtcClientCOCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.ClientControl("dtcClientCO", "",  , GetLocalResourceObject("dtcClientCOToolTip"),  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=LABEL2>" & GetLocalResourceObject("dtcClientASCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.ClientControl("dtcClientAS", "",  , GetLocalResourceObject("dtcClientASToolTip"),  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdChangdatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdChangdat", "",  , GetLocalResourceObject("tcdChangdatToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdPrintDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdPrintDate", CStr(Today),  , GetLocalResourceObject("tcdPrintDateToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>		" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=LABEL3>" & GetLocalResourceObject("tcdIniDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdIniDate", "",  , GetLocalResourceObject("tcdIniDateToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD><LABEL ID=LABEL4>" & GetLocalResourceObject("tcdEndDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdEndDate", "",  , GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<tr>	" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("cbePolicyTypeCaption") & "</label></td>" & vbCrLf)
Response.Write("	      <td>")


Response.Write(mobjValues.PossiblesValues("cbePolicyType", "TABLE5632", eFunctions.Values.eValuesType.clngComboType, "2",  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbePolicyTypeToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("	    <tr>" & vbCrLf)
Response.Write("	      <td><label>" & GetLocalResourceObject("tcnPolicyIniCaption") & "</label></td>" & vbCrLf)
Response.Write("	      <td>")


Response.Write(mobjValues.NumericControl("tcnPolicyIni", 9, "",  , GetLocalResourceObject("tcnPolicyIniToolTip"),  ,  ,  ,  ,  ,  , False))


Response.Write(" / ")


Response.Write(mobjValues.NumericControl("tcnPolicyFin", 9, "",  , GetLocalResourceObject("tcnPolicyFinToolTip"),  ,  ,  ,  ,  ,  , False))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	    <tr>	    " & vbCrLf)
Response.Write("	      <td><label>" & GetLocalResourceObject("valTypeAmendCaption") & "</label></td>" & vbCrLf)
Response.Write("		  <td>")

	
	With mobjValues
		.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valTypeAmend", "tabtype_amend", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("valTypeAmendToolTip")))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("		   </td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("	    <tr>" & vbCrLf)
Response.Write("	      <td>")


Response.Write(mobjValues.HiddenControl("tcdEffecdate", ""))


Response.Write("</td>" & vbCrLf)
Response.Write("	      <td>")


Response.Write(mobjValues.HiddenControl("tcdExpirdat", ""))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		</TABLE>" & vbCrLf)
Response.Write(" " & vbCrLf)
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

mobjValues.sCodisplPage = "CAL01502_K"
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
			valProduct1.disabled=false;
			btnvalProduct1.disabled=false;
			valProduct1.value="";
			UpdateDiv("valProduct1Desc", "")
			valProduct1.Parameters.Param1.sValue=lobject.value;
			valProduct1.Parameters.Param2.sValue=0;

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




//% BlankOfficeDepend: Blanquea los campos OFICINA y AGENCIA si y sólo si el valor del
//%                 campo SUCURSAL cambia
//-------------------------------------------------------------------------------------
function BlankOfficeDepend()
//-------------------------------------------------------------------------------------
{
    with(document.forms[0]){
        cbeOfficeAgen.value="";
        cbeAgency.value="";
        cbeOfficeAgen_nBran_off.value = "";
        cbeAgency_nBran_off.value = "";
        cbeAgency_nOfficeAgen.value = "";
        cbeAgency_sDesAgen.value = "";
    }
    UpdateDiv('cbeOfficeAgenDesc','');
    UpdateDiv('cbeAgencyDesc','');
}


//% insInitialAgency: manejo de sucursal/oficina/agencia
//-------------------------------------------------------------------------------------------
function insInitialAgency(nInd){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
//+ Cambia la sucursal 
        if (nInd == 1){
            cbeOfficeAgen.value = '';
            UpdateDiv('cbeOfficeAgenDesc','');
            cbeOfficeAgen.Parameters.Param1.sValue = cbeOffice.value;
            cbeOfficeAgen.Parameters.Param2.sValue = '0';
            cbeAgency.value = '';
            UpdateDiv('cbeAgencyDesc','');
            cbeAgency.Parameters.Param1.sValue = cbeOffice.value;
            cbeAgency.Parameters.Param2.sValue = '0';
        }
//+ Cambia la oficina 
        else{
            if (nInd == 2){
                if(cbeOfficeAgen.value != ''){
                    cbeOffice.value = cbeOfficeAgen_nBran_off.value;
                    cbeAgency.value = '';
                    UpdateDiv('cbeAgencyDesc','');
                    cbeAgency.Parameters.Param1.sValue = cbeOffice.value;
                    cbeAgency.Parameters.Param2.sValue = cbeOfficeAgen.value;
                }
                else{
                    cbeAgency.Parameters.Param2.sValue = '0';
                }
            }
//+ Cambia la Agencia
            else{
                if (nInd == 3){
                    if(cbeAgency.value != ''){
                        cbeOffice.value = cbeAgency_nBran_off.value;
                        if (cbeOfficeAgen.value == ''){
                            cbeOfficeAgen.value = cbeAgency_nOfficeAgen.value;
                            UpdateDiv('cbeOfficeAgenDesc',cbeAgency_sDesAgen.value);
                        }
                        cbeOfficeAgen.Parameters.Param1.sValue = cbeOffice.value;
                        cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?'0':cbeOfficeAgen.value);
                    }
                }
            }
        }
    }
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CAL01502_K.aspx", 1, ""))
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With

mobjMenu = Nothing
%>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CAL01415" ACTION="valPolicyRep.aspx?Mode=1">
	<BR><BR>
<%
Call insDefineHeader()

mobjValues = Nothing
%>

</FORM>
</BODY>
</HTML>






