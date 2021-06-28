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
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=5 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Tipo de ejecución"">" & GetLocalResourceObject("AnchorTipo de ejecuciónCaption") & "</A></LABEL></TD>	    " & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=5 CLASS=""HORLINE""></TD>		" & vbCrLf)
Response.Write("        </TR>        	" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=2> ")


Response.Write(mobjValues.OptionControl(0, "optEje", GetLocalResourceObject("optEje_CStr1Caption"), CStr(1), CStr(1), "insEnabledFields(this)",  , 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=2> ")


Response.Write(mobjValues.OptionControl(0, "optEje", GetLocalResourceObject("optEje_CStr2Caption"), CStr(2), CStr(2), "insEnabledFields(this)",  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>        	" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=5>&nbsp</TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=5>" & vbCrLf)
Response.Write("        <DIV ID=""divType_pun"" style=""left=5000;top=0"">" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"">" & vbCrLf)
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


Response.Write(mobjValues.NumericControl("tcnCertif", 5, vbNullString,  , "",  , 0,  ,  ,  , "insEnabledCertif('nCertif')", True, 6))


Response.Write("			" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=2 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Tipo de Información"">" & GetLocalResourceObject("AnchorTipo de InformaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("				<TD></TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD CLASS=""HorLine"" COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("				<TD></TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.OptionControl(0, "optTrans", GetLocalResourceObject("optTrans_CStr1Caption"), CStr(1), CStr(1),  ,  , 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp </TD>" & vbCrLf)
Response.Write("				<TD colspan=""2"">&nbsp</TD>			" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.OptionControl(0, "optTrans", GetLocalResourceObject("optTrans_CStr2Caption"), CStr(0), CStr(2),  ,  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp </TD>" & vbCrLf)
Response.Write("				<TD colspan=""2"">&nbsp</TD>				" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.OptionControl(0, "optTrans", GetLocalResourceObject("optTrans_CStr3Caption"), CStr(0), CStr(3),  ,  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp </TD>" & vbCrLf)
Response.Write("				<TD colspan=""2"">&nbsp</TD>				" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.OptionControl(0, "optTrans", GetLocalResourceObject("optTrans_CStr4Caption"), CStr(0), CStr(4),  ,  , 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp </TD>" & vbCrLf)
Response.Write("				<TD></TD>" & vbCrLf)
Response.Write("				<TD></TD>			  " & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=4 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Datos de la póliza"">" & GetLocalResourceObject("AnchorDatos de la pólizaCaption") & "</A></LABEL></TD>	    " & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=4 CLASS=""HORLINE""></TD>		" & vbCrLf)
Response.Write("			</TR>       " & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("dtcClientCOCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.ClientControl("dtcClientCO", "",  , GetLocalResourceObject("dtcClientCOToolTip"),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("dtcClientASCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.ClientControl("dtcClientAS", "",  , GetLocalResourceObject("dtcClientASToolTip"),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>        " & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("tcdExpirdatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdExpirdat", "",  , GetLocalResourceObject("tcdExpirdatToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("tcdChangdatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdChangdat", "",  , GetLocalResourceObject("tcdChangdatToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.CheckControl("chkRe_im1", GetLocalResourceObject("chkRe_im1Caption"), CStr(False), "1", "insDisabledCost(this)", False))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD></TD>" & vbCrLf)
Response.Write("			</TR>        " & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("tcnCostCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.NumericControl("tcnCost", 18, CStr(0),  , GetLocalResourceObject("tcnCostToolTip"),  , 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL>" & GetLocalResourceObject("tcdPrintDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdPrintDate", CStr(Today),  , GetLocalResourceObject("tcdPrintDateToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>        " & vbCrLf)
Response.Write("		</TABLE> " & vbCrLf)
Response.Write("        </DIV>	        " & vbCrLf)
Response.Write("        <DIV ID=""divType_mas"" style=""left=5000;top=0"">        " & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("		  		<TD> <LABEL ID=0>" & GetLocalResourceObject("cbeOfficeCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>&nbsp</TD>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=0>" & GetLocalResourceObject("cbeAgencyCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.PossiblesValues("cbeAgency", "Table5555", eFunctions.Values.eValuesType.clngComboType, ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>        " & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=41208>" & GetLocalResourceObject("cbeBranch1Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.PossiblesValues("cbeBranch2", "Table10", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , "insChargeProduct(this)",  ,  , "",  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>&nbsp</TD>			" & vbCrLf)
Response.Write("				<TD> <LABEL ID=40011>" & GetLocalResourceObject("valProduct1Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				")

	With mobjValues
		.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	
Response.Write("" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.PossiblesValues("valProduct2", "tabProdmaster", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  ,  ,  , "", eFunctions.Values.eTypeCode.eString, 4))


Response.Write("</TD>			" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=2 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Tipo de Información"">" & GetLocalResourceObject("AnchorTipo de InformaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=3>&nbsp</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD CLASS=""HorLine"" COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("				<TD></TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.OptionControl(0, "optTrans1", GetLocalResourceObject("optTrans1_CStr2Caption"), CStr(1), CStr(2),  ,  , 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD> </TD>" & vbCrLf)
Response.Write("				<TD> </TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.OptionControl(0, "optTrans1", GetLocalResourceObject("optTrans1_CStr6Caption"), CStr(0), CStr(6),  ,  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.OptionControl(0, "optTrans1", GetLocalResourceObject("optTrans1_CStr3Caption"), CStr(0), CStr(3),  ,  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp </TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=3> </TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.OptionControl(0, "optTrans1", GetLocalResourceObject("optTrans1_CStr1Caption"), CStr(0), CStr(1),  ,  , 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp </TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=3> </TD>" & vbCrLf)
Response.Write("			</TR>                  " & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=2 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Periodo"">" & GetLocalResourceObject("AnchorPeriodoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("				<TD>&nbsp</TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=2 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Rango"">" & GetLocalResourceObject("AnchorRangoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD CLASS=""HorLine"" COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("				<TD></TD>" & vbCrLf)
Response.Write("				<TD CLASS=""HorLine"" COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("tcdInitialCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdInitial", "",  , GetLocalResourceObject("tcdInitialToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>			            " & vbCrLf)
Response.Write("				<TD></TD>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=41370>" & GetLocalResourceObject("tcnProponum1Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.NumericControl("tcnProponum1", 10, vbNullString,  , "",  , 0,  ,  ,  ,  , False, 6))


Response.Write("			" & vbCrLf)
Response.Write("			</TR>                  " & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("tcdFinishCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdFinish", "",  , GetLocalResourceObject("tcdFinishToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>			            " & vbCrLf)
Response.Write("				<TD></TD>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=41370>" & GetLocalResourceObject("tcnProponum2Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.NumericControl("tcnProponum2", 10, vbNullString,  , "",  , 0,  ,  ,  ,  , False, 6))


Response.Write("			" & vbCrLf)
Response.Write("			</TR>                  " & vbCrLf)
Response.Write("        </TABLE> " & vbCrLf)
Response.Write("        </DIV>	        " & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("   ShowDiv('divType_mas', 'hide')" & vbCrLf)
Response.Write("</" & "SCRIPT>	")

End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CAL001_K"
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
			
			valProduct2.disabled=false;
			btnvalProduct2.disabled=false;
			valProduct2.value="";
			UpdateDiv("valProduct2Desc", "")
			valProduct2.Parameters.Param1.sValue=lobject.value;
			valProduct2.Parameters.Param2.sValue=0;
		}
    }
}

//%   insEnabledFields: Permite habilitar e inhabilitar los campos de la página.
//------------------------------------------------------------------------------------------
function insEnabledFields(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value!=1) {
	
	    ShowDiv('divType_pun', 'hide')
	    ShowDiv('divType_mas', 'show')
	
		with(self.document.forms[0]){
			cbeBranch2.value="";
			valProduct2.value="";
			valProduct2Desc.value="";
			UpdateDiv("valProduct2Desc", "")
			cbeOffice.value="";
			cbeAgency.value="";
		}
    }
    else{

        ShowDiv('divType_mas', 'hide')
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
            tcdChangdat.value="";			
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

//%   insDisabledCost(): Permite habilitar e inhabilitar el campo Costo.
//------------------------------------------------------------------------------------------
function insDisabledCost(lObject){
//------------------------------------------------------------------------------------------
	if (lObject.checked==true){ 
		self.document.forms[0].tcnCost.disabled=false;
		self.document.forms[0].tcnCost.value = nCost;
		self.document.forms[0].cbeCurrency.value = nCurrency;
		}
    else{
		self.document.forms[0].tcnCost.disabled=true;
		self.document.forms[0].tcnCost.value =0;
		self.document.forms[0].cbeCurrency.value = 0;
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
		insDefValues('ShowDataPolicy',	'nBranch='   + lintBranch  +
										'&nProduct=' + lintProduct +
										'&nPolicy='  + llngPolicy,'/VTimeNet/policy/policyrep/');
    }
    else
		if (lobject == "nCertif"){
			insDefValues('ShowDataCertif', 'nBranch='+ lintBranch    +
				                           '&nProduct='+ lintProduct +
					                       '&nPolicy='+ llngPolicy   +
						                   '&nCertif='+ llngCertif,'/VTimeNet/policy/policyrep/');
        }
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CAL001_k.aspx", 1, ""))
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With

mobjMenu = Nothing
%>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CAL001" ACTION="valPolicyRep.aspx?Mode=1">
	<BR><BR>
<%
Call insDefineHeader()

mobjValues = Nothing
%>

</FORM>
</BODY>
</HTML>






