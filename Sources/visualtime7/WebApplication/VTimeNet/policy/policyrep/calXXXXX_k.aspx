<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim sCodispl As String
Dim sCodisplPage As String

Dim mstrQuote As String
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.14
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'%   insDefineHeader: Permite cargar los campos del encabezado
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=5 CLASS=""HighLighted"">&nbsp</TD>	    " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("  	            <DIV ID=""divOptExec"">			" & vbCrLf)
Response.Write("				    <TD COLSPAN=4> ")


Response.Write(mobjValues.HiddenControl("optEje", "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</DIV>" & vbCrLf)
Response.Write("			</TR>        	" & vbCrLf)
Response.Write("        <TD COLSPAN=5>" & vbCrLf)
Response.Write("        <DIV ID=""divType_pun"" style=""left=5000;top=0"">" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=41208>" & GetLocalResourceObject("cbeBranch1Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")

	mobjValues.BlankPosition = False
        'mobjValues.TypeList = 1
        'mobjValues.List = "2"
        Response.Write(mobjValues.PossiblesValues("cbeBranch1", "Table10", eFunctions.Values.eValuesType.clngComboType, vbNullString, , , , , , , False, , "", , 3))
	
Response.Write("" & vbCrLf)
Response.Write("				</TD>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=40011>" & GetLocalResourceObject("valProduct1Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				")

	With mobjValues
		.Parameters.Add("nBranch", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	
Response.Write("" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.PossiblesValues("valProduct1", "tabProdmaster", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "insEnabledPolicy(this)", True,  , "", eFunctions.Values.eTypeCode.eString, 4))


Response.Write("</TD>			" & vbCrLf)
Response.Write("			</TR>        " & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("		  		<TD> <LABEL ID=40281>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "insChangePolicy()",  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=41370>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


        Response.Write(mobjValues.NumericControl("tcnCertif", 5, vbNullString, , "", , 0, , , , "insEnabledCertif('nCertif')", False, 6))


Response.Write("" & vbCrLf)
Response.Write("				     ")


Response.Write(mobjValues.HiddenControl("optTrans", "1"))


Response.Write("			" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			")

        If sCodispl = "CAL01512" Then
            
            Response.Write("" & vbCrLf)
            Response.Write("				<TR>" & vbCrLf)
            Response.Write("		  			<TD><LABEL ID=40281>" & GetLocalResourceObject("tcdCopydateCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("					<TD>")


            Response.Write(mobjValues.DateControl("tcdCopydate", "", , GetLocalResourceObject("tcdCopydateToolTip"), , , , , False))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("					<TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("					<TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("				</TR>" & vbCrLf)
            Response.Write("			")

        End If
Response.Write("" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=4 CLASS=""HighLighted""><LABEL ID=LABEL2><A NAME=""Datos de la póliza"">" & GetLocalResourceObject("AnchorDatos de la pólizaCaption") & "</A></LABEL></TD>	    " & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=4 CLASS=""HORLINE""></TD>		" & vbCrLf)
Response.Write("			</TR>       " & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=LABEL3>" & GetLocalResourceObject("dtcClientCOCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.ClientControl("dtcClientCO", "",  , GetLocalResourceObject("dtcClientCOToolTip"),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=LABEL4>" & GetLocalResourceObject("dtcClientASCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.ClientControl("dtcClientAS", "",  , GetLocalResourceObject("dtcClientASToolTip"),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>        " & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=LABEL5>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD><LABEL ID=LABEL6>" & GetLocalResourceObject("tcdExpirdatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdExpirdat", "",  , GetLocalResourceObject("tcdExpirdatToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=LABEL7>" & GetLocalResourceObject("tcdChangdatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdChangdat", "",  , GetLocalResourceObject("tcdChangdatToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.HiddenControl("chkRe_im1", "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.HiddenControl("tcnCost", "0"))




Response.Write(mobjValues.HiddenControl("cbeCurrency", "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>        " & vbCrLf)
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
Response.Write("		  		<TD> <LABEL ID=LABEL10>" & GetLocalResourceObject("cbeOfficeCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>&nbsp</TD>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=LABEL11>" & GetLocalResourceObject("cbeAgencyCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.PossiblesValues("cbeAgency", "Table5555", eFunctions.Values.eValuesType.clngComboType, ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>        " & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=LABEL12>" & GetLocalResourceObject("cbeBranch2Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.PossiblesValues("cbeBranch2", "Table10", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , "insChargeProduct(this)",  ,  , "",  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>&nbsp</TD>			" & vbCrLf)
Response.Write("				<TD> <LABEL ID=LABEL13>" & GetLocalResourceObject("valProduct2Caption") & "</LABEL> </TD>" & vbCrLf)
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
Response.Write("				<TD COLSPAN=2 CLASS=""HighLighted""><LABEL ID=LABEL14><A NAME=""Tipo de Información"">" & GetLocalResourceObject("AnchorTipo de InformaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
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
Response.Write("				<TD COLSPAN=2 CLASS=""HighLighted""><LABEL ID=LABEL15><A NAME=""Periodo"">" & GetLocalResourceObject("AnchorPeriodoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("				<TD>&nbsp</TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=2 CLASS=""HighLighted""><LABEL ID=LABEL16><A NAME=""Rango"">" & GetLocalResourceObject("AnchorRangoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD CLASS=""HorLine"" COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("				<TD></TD>" & vbCrLf)
Response.Write("				<TD CLASS=""HorLine"" COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=LABEL17>" & GetLocalResourceObject("tcdInitialCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdInitial", "",  , GetLocalResourceObject("tcdInitialToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>			            " & vbCrLf)
Response.Write("				<TD></TD>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=LABEL18>" & GetLocalResourceObject("tcnProponum1Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.NumericControl("tcnProponum1", 10, vbNullString,  , "",  , 0,  ,  ,  ,  , False, 6))


Response.Write("			" & vbCrLf)
Response.Write("			</TR>                  " & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=LABEL19>" & GetLocalResourceObject("tcdFinishCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdFinish", "",  , GetLocalResourceObject("tcdFinishToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>			            " & vbCrLf)
Response.Write("				<TD></TD>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=LABEL20>" & GetLocalResourceObject("tcnProponum2Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.NumericControl("tcnProponum2", 10, vbNullString,  , "",  , 0,  ,  ,  ,  , False, 6))


Response.Write("			" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			</TR>                  " & vbCrLf)
Response.Write("        </TABLE> " & vbCrLf)
Response.Write("        </DIV>	        " & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("   ShowDiv('divType_mas', 'hide');" & vbCrLf)
Response.Write("</" & "SCRIPT>	")

End Sub

</script>
<%sCodispl = Trim(Request.QueryString.Item("sCodispl"))
sCodisplPage = LCase(sCodispl) & "_k"
mstrQuote = """"

Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(sCodisplPage)

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
mobjValues.sCodisplPage = sCodisplPage
'~End Body Block VisualTimer Utility

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>

<HTML>
<HEAD>
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
			//tcnPolicy.disabled=true;
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

function insChangePolicy(){
//------------------------------------------------------------------------------------------

    
    with (self.document.forms[0]){
        if (tcnPolicy.value != ''){
            insDefValues('ShowDataProduct', 'nBranch=' + cbeBranch1.value +
                                         '&nProduct=' + valProduct1.value +
                                         '&nPolicy=' + tcnPolicy.value  +
                                         '&sCodispl=<%=sCodispl%>'   );
                                          //insEnabledCertif('nPolicy');

        }
       }
} 


</SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu(sCodispl, sCodispl & "_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM method="post" id="FORM" name="Policy" action="valPolicyRep.aspx?mode=1">
	<BR><BR>
<%
Call insDefineHeader()

mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.14
Call mobjNetFrameWork.FinishPage(sCodisplPage)
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





