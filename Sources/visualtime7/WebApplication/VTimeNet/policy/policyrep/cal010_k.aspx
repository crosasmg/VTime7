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
Response.Write("    <TABLE border=""0"" WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD> <LABEL ID=0>" & GetLocalResourceObject("cbenTypeReportCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD> ")
        mobjValues.TypeList = Values.ecbeTypeList.Inclution
        mobjValues.List = "3"
        Response.Write(mobjValues.PossiblesValues("cbenTypeReport", "Table98", eFunctions.Values.eValuesType.clngComboType, vbNullString, , , , , , "insEnabledFields(this)", , , "", , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp</TD>" & vbCrLf)
Response.Write("        </TR>			" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD> <LABEL ID=41208>" & GetLocalResourceObject("cbeBranch1Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD> ")


        Response.Write(mobjValues.PossiblesValues("cbeBranch1", "Table10", eFunctions.Values.eValuesType.clngComboType, vbNullString, , , , , , "insChargeProduct(this);insChangeValue(this);", , , "", , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=40011>" & GetLocalResourceObject("valProduct1Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			")

	With mobjValues
		.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	
Response.Write("" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.PossiblesValues("valProduct1", "tabProdmaster", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "insEnabledPolicy(this);insChangeValue(this);",  ,  , "", eFunctions.Values.eTypeCode.eString, 5))


Response.Write("</TD>			" & vbCrLf)
Response.Write("		</TR>        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("	  		<TD> <LABEL ID=40281>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "insEnabledCertif('nPolicy');insChangeValue(this);",  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp</TD>" & vbCrLf)
Response.Write("			<TD> <LABEL ID=41370>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.NumericControl("tcnCertif", 5, vbNullString,  , "",  , 0,  ,  ,  , "insEnabledCertif('nCertif')", True, 6))


Response.Write("			" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("dtcClientCOCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.ClientControl("dtcClientCO", "",  , GetLocalResourceObject("dtcClientCOToolTip"),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("dtcClientASCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.ClientControl("dtcClientAS", "",  , GetLocalResourceObject("dtcClientASToolTip"),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>       " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcniniCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnini", 10,  ,  , GetLocalResourceObject("tcniniToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnendCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnend", 10,  ,  , GetLocalResourceObject("tcnendToolTip")))

        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)

        'inicio nuevos campos --Fecha de endoso
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=5>" & vbCrLf)
        Response.Write("				<DIV ID=""divType_end"" style=""left=5000;top=0"">        " & vbCrLf)
        Response.Write("					<TABLE border=""0"" WIDTH=""100%"">" & vbCrLf)
        Response.Write("						<TR>" & vbCrLf)
        Response.Write("							<TD COLSPAN=5 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Cartas"">" & GetLocalResourceObject("AnchorCartasCaption") & "</A></LABEL></TD>	    " & vbCrLf)
        Response.Write("						</TR>" & vbCrLf)
        Response.Write("						<TR>" & vbCrLf)
        Response.Write("							<TD COLSPAN=5 CLASS=""HORLINE""></TD>		" & vbCrLf)
        Response.Write("						</TR>      " & vbCrLf)
        Response.Write("						<BR>      " & vbCrLf)
        Response.Write("						<TR>" & vbCrLf)
        Response.Write("							<TD><LABEL>" & GetLocalResourceObject("tcdEffecEndCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("							<TD>")
        Response.Write(mobjValues.DateControl("tcdEffecEnd", "", , GetLocalResourceObject("tcdEffecEndToolTip"),,,,"insChangeValue(this);"))
        Response.Write("                            </TD>" & vbCrLf)
        Response.Write("							<TD width=""50%"">&nbsp;</TD>" & vbCrLf)        
        Response.Write("                        </TR>" & vbCrLf)
        Response.Write("						<TR>" & vbCrLf)
        Response.Write("							<TD><LABEL>" & GetLocalResourceObject("tcnLetterCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("							<TD>")
        Response.Write(mobjValues.TextControl("tcnLetter", 10, "", , GetLocalResourceObject("tcnLetterToolTip")))
        Response.Write("                            </TD>" & vbCrLf)
        Response.Write("                        </TR>" & vbCrLf)
        Response.Write("						<TR>" & vbCrLf)

        Response.Write("							<TD><LABEL>" & GetLocalResourceObject("txtComentCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("							<TD>")
        
        mobjValues.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("nPolicy", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("dEffecdate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        Response.Write(mobjValues.PossiblesValues("ValNote", "TABPOLICY_HIS_NOTE", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , , , 10))
        
        Response.Write("                            </TD>" & vbCrLf)
        Response.Write("                        </TR>" & vbCrLf)
        
        
        Response.Write("						<TR>" & vbCrLf)
        Response.Write("							<TD><LABEL>" & GetLocalResourceObject("txtComentCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("							<TD>")
        Response.Write(mobjValues.TextAreaControl("txtComent", 4, 40, "", False, GetLocalResourceObject("txtComentToolTip"), False, False, , ""))
        Response.Write("                            </TD>" & vbCrLf)

        Response.Write("                        </TR>" & vbCrLf)
        
        Response.Write("					</TABLE> " & vbCrLf)
        Response.Write("				</DIV>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        
        
        
        'fin nuevos campos
        
Response.Write("		<TR >" & vbCrLf)
Response.Write("			<TD COLSPAN=5>" & vbCrLf)
Response.Write("				<DIV ID=""divType_mas"" style=""left=5000;top=0"">        " & vbCrLf)
        Response.Write("					<TABLE border=""0"" WIDTH=""100%"">" & vbCrLf)
Response.Write("						<TR>" & vbCrLf)
Response.Write("							<TD COLSPAN=5 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Cartas"">" & GetLocalResourceObject("AnchorCartasCaption") & "</A></LABEL></TD>	    " & vbCrLf)
Response.Write("						</TR>" & vbCrLf)
Response.Write("						<TR>" & vbCrLf)
Response.Write("							<TD COLSPAN=5 CLASS=""HORLINE""></TD>		" & vbCrLf)
Response.Write("						</TR>      " & vbCrLf)
Response.Write("						<TR>" & vbCrLf)
Response.Write("							<TD> <LABEL ID=0>" & GetLocalResourceObject("cbenTypeLetterCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("							<TD> ")

        mobjValues.TypeList = Values.ecbeTypeList.Inclution
        mobjValues.List = "4"
Response.Write(mobjValues.PossiblesValues("cbenTypeLetter", "Table99", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , "insTextComent(this)",  ,  , "",  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("							<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("							<TD><LABEL ID=0>" & GetLocalResourceObject("tcnLetterCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("							<TD>")


Response.Write(mobjValues.NumericControl("tcnLetter", 10,  ,  , GetLocalResourceObject("tcnLetterToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("						</TR>" & vbCrLf)
Response.Write("						<TR>" & vbCrLf)
Response.Write("							<TD><LABEL>" & GetLocalResourceObject("tctAtentionCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("							<TD>")


Response.Write(mobjValues.TextControl("tctAtention", 40,  , True, GetLocalResourceObject("tctAtentionToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("							<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("							<TD> <LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateCaption") & " </LABEL></TD>" & vbCrLf)
Response.Write("							<TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("						</TR>" & vbCrLf)
Response.Write("						<TR>" & vbCrLf)
Response.Write("							<TD><LABEL>" & GetLocalResourceObject("txtComentCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("							<TD>")


Response.Write(mobjValues.TextAreaControl("txtComent", 4, 40, "", False, GetLocalResourceObject("txtComentToolTip"), False, False,  , ""))
        Response.Write("</TD>" & vbCrLf)
Response.Write("							<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("							<TD> <LABEL ID=0>&nbsp; </LABEL></TD>" & vbCrLf)
Response.Write("							<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("						</TR>" & vbCrLf)
Response.Write("					</TABLE> " & vbCrLf)
Response.Write("				</DIV>" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)


	
'+ Se agrega fecha para los certificado cobertura e impresion de poliza        
        
        Response.Write("<DIV ID=""divdEffecdate"" style=""left=5000;top=0"">        " & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("	    <TD> <LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateCaption") & " </LABEL></TD>" & vbCrLf)
        Response.Write("	    <TD>")

        Response.Write(mobjValues.DateControl("tcdEffecdateRpt", "", , GetLocalResourceObject("tcdEffecdateRptToolTip"), , , , , False))

        Response.Write("            </TD>" & vbCrLf)
        Response.Write("							<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("							<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("							<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("</DIV>" & vbCrLf)


        Response.Write("<SCRIPT>" & vbCrLf)
        Response.Write("   insShowInitial();" & vbCrLf)
        Response.Write("</" & "SCRIPT>	")

Response.Write("	</TABLE> " & vbCrLf)        
        
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CAL010_K"
mstrQuote = """"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
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
//% insShowInitial: Oculta los campos de la página al entrar en ella
//------------------------------------------------------------------------------------------
function  insShowInitial(){
//------------------------------------------------------------------------------------------
	document.all.tags("TR")[4].style.display='';
	document.getElementsByTagName("TR")[5].style.display='';
	document.getElementsByTagName("TR")[8].style.display='none';	
    ShowDiv('divType_mas', 'hide');
    ShowDiv('divType_end', 'hide');
    ShowDiv('divdEffecdate', 'hide');
}			

//%   insBlankFields: Blanque los campos al cambiar el tipo
//------------------------------------------------------------------------------------------
function insBlankFields(){
//------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		cbeBranch1.value = "";
		valProduct1.value = "";	
		UpdateDiv("valProduct1Desc", "");	
		tcnPolicy.value = "";
		tcnCertif.value = "";
		dtcClientCO.value = "";
		UpdateDiv("dtcClientCO_Name", "");	
		dtcClientCO_Digit.value = "";
		dtcClientAS.value = "";
		UpdateDiv("dtcClientAS_Name", "");	
		dtcClientAS_Digit.value = "";		
		tcnini.value = "";
		tcnend.value = "";
		cbenTypeLetter.value = "";
		tcnLetter.value = "";
		txtComent.value = "";
		tcdEffecdate.value = "";
	}
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
            ValNote.Parameters.Param2.sValue=lobject.value;
		}
    }
}

//%   d: Permite habilitar e inhabilitar los campos de la página.
//------------------------------------------------------------------------------------------
function insEnabledFields(lobject){
//------------------------------------------------------------------------------------------
	insBlankFields();
	switch(lobject.value){
//Ninguno	
	    case "0":
        {
            insShowInitial();
            break;
        }
//Cuadro póliza	
	    case "1":
        {
			document.getElementsByTagName("TR")[4].style.display='none';
			document.getElementsByTagName("TR")[5].style.display='none';
			document.getElementsByTagName("TR")[8].style.display='';
			ShowDiv('divType_mas', 'hide');
            ShowDiv('divType_end', 'hide');
            ShowDiv('divdEffecdate', 'show');
			break;
		}
//Cartas		
	    case "2":
        {
			document.getElementsByTagName("TR")[4].style.display='';
			document.getElementsByTagName("TR")[5].style.display='';
			document.getElementsByTagName("TR")[8].style.display='none';
			ShowDiv('divType_mas', 'show');
            ShowDiv('divType_end', 'hide');
            ShowDiv('divdEffecdate', 'hide');
			break;
		}
//Certificados de coberturas		
		case "3":
        {
			document.getElementsByTagName("TR")[4].style.display='';
			document.getElementsByTagName("TR")[5].style.display='';
			document.getElementsByTagName("TR")[8].style.display='none';
			ShowDiv('divType_mas', 'hide');
            ShowDiv('divType_end', 'hide');
            ShowDiv('divdEffecdate', 'show');
            break;
		}

//Certificados de Endosos
		case "4":
        {
			document.getElementsByTagName("TR")[4].style.display='';
			document.getElementsByTagName("TR")[5].style.display='';
			document.getElementsByTagName("TR")[8].style.display='none';
            ShowDiv('divType_mas', 'hide');			
            ShowDiv('divType_end', 'show');
            ShowDiv('divdEffecdate', 'hide');
			break;
		}
	}
}

//%   insEnabledPolicy(): Permite habilitar e inhabilitar el campo Póliza.
//------------------------------------------------------------------------------------------
function insEnabledPolicy(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value) 

        with(self.document.forms[0]){
                tcnPolicy.disabled=false;
		        tcnPolicy.value = "";
		        tcnCertif.value = "";
		        dtcClientCO.value = "";
		        UpdateDiv("dtcClientCO_Name", "");	
		        dtcClientCO_Digit.value = "";
		        dtcClientAS.value = "";
		        UpdateDiv("dtcClientAS_Name", "");	
		        dtcClientAS_Digit.value = "";		
        }

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
    var llngTypeRpt = 0;
	
	lintBranch  = self.document.forms[0].elements[<%=mstrQuote%>cbeBranch1<%=mstrQuote%>].value
	lintProduct = self.document.forms[0].elements[<%=mstrQuote%>valProduct1<%=mstrQuote%>].value
	llngPolicy  = self.document.forms[0].elements[<%=mstrQuote%>tcnPolicy<%=mstrQuote%>].value
    llngCertif  = self.document.forms[0].elements[<%=mstrQuote%>tcnCertif<%=mstrQuote%>].value
    llngTypeRpt = self.document.forms[0].elements[<%=mstrQuote%>cbenTypeReport<%=mstrQuote%>].value

    if (lobject == "nPolicy"){
		insDefValues('ShowDataPolicy',	'nBranch='   + lintBranch  +
										'&nProduct=' + lintProduct +
										'&sCodispl=CAL010_K' +
                                        '&nTypeRpt='+ llngTypeRpt   +
										'&nPolicy='  + llngPolicy,'/VTimeNet/policy/policyrep/');
    }
    else
		if (lobject == "nCertif" && llngCertif != ""){
			insDefValues('ShowDataCertif', 'nBranch='+ lintBranch    +
				                           '&nProduct='+ lintProduct +
					                       '&nPolicy='+ llngPolicy   +
					                       '&sCodispl=CAL010_K' +
                                           '&nTypeRpt='+ llngTypeRpt   +
						                   '&nCertif='+ llngCertif,'/VTimeNet/policy/policyrep/');
        }
}
//%   insEnabledCertif(): Permite habilitar e inhabilitar el campo Certificado.
//------------------------------------------------------------------------------------------
function insTextComent(lobject){
//------------------------------------------------------------------------------------------
var lstrtext;
	if (lobject.value == 3) {
		lstrtext = 'Las horas médicas para la realización de exámenes deberán ser coordinadas ';
		lstrtext = lstrtext + 'previamente en la Compañia de Seguros con la Srta. Sylvia Cárdenas Matus, ';
		lstrtext = lstrtext + 'Encargada de Evaluación, al teléfono 461 87 63.';

		self.document.forms[0].txtComent.value = lstrtext;
	}
}

//%   insChangeValue: Se cargan parámetros del campo Nota
//------------------------------------------------------------------------------------------
function insChangeValue(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value!=0) {
		with(self.document.forms[0]){
			switch(cbenTypeReport.value){
				case "4":{
					switch(lobject.name){
						case "valProduct1":{
							ValNote.Parameters.Param3.sValue=lobject.value;
							break;
						}
						case "tcnPolicy":{
							ValNote.Parameters.Param4.sValue=lobject.value;
							break;
						}
						case "tcdEffecEnd":{
							ValNote.Parameters.Param5.sValue=lobject.value;
							break;
						}
                        case "cbeBranch1":{
							ValNote.Parameters.Param2.sValue=lobject.value;
							break;
						}
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
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CAL010_k.aspx", 1, ""))
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With

mobjMenu = Nothing
%>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CAL010" ACTION="valPolicyRep.aspx?Mode=1">
	<BR><BR>
<%
Call insDefineHeader()

mobjValues = Nothing
%>

</FORM>
</BODY>
</HTML>






