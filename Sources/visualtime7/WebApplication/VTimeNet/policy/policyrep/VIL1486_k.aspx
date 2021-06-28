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
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp</TD>" & vbCrLf)
Response.Write("        </TR>			" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD> <LABEL ID=41208>" & GetLocalResourceObject("cbeBranch1Caption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.PossiblesValues("cbeBranch1", "Table10", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , "insChargeProduct(this)",  ,  , "",  , 3))


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


Response.Write(mobjValues.PossiblesValues("valProduct1", "tabProdmaster", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "insEnabledPolicy(this)",  ,  , "", eFunctions.Values.eTypeCode.eString, 5))


Response.Write("</TD>			" & vbCrLf)
Response.Write("		</TR>        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("	  		<TD> <LABEL ID=40281>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD> ")


        Response.Write(mobjValues.NumericControl("tcnPolicy", 10, vbNullString, , GetLocalResourceObject("tcnPolicyToolTip"), , , , , , "insEnabledCertif('nPolicy')", , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp</TD>" & vbCrLf)
Response.Write("			<TD> <LABEL ID=41370>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD> ")


        Response.Write(mobjValues.NumericControl("tcnCertif", 5, vbNullString, , "", , 0, , , , , True, 6))


Response.Write("			" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)



Response.Write("						<TR>" & vbCrLf)

       Response.Write("							<TD> <LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateCaption") & " </LABEL></TD>" & vbCrLf)
Response.Write("							<TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , False))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("							<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("							<TD> <LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateCaption2") & " </LABEL></TD>" & vbCrLf)
        Response.Write("							<TD>")


        Response.Write(mobjValues.DateControl("tcdEffecdate2", "", , GetLocalResourceObject("tcdEffecdateToolTip2"), , , , , False))


        Response.Write("</TD>" & vbCrLf)
Response.Write("						</TR>" & vbCrLf)

Response.Write("					</TABLE> " & vbCrLf)
Response.Write("				</DIV>" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE> " & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("   insShowInitial();" & vbCrLf)
Response.Write("</" & "SCRIPT>	")

	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "VIL1486_K"
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
//% insShowInitial: Oculta los campos de la página al entrar en ella
//------------------------------------------------------------------------------------------
function  insShowInitial(){
//------------------------------------------------------------------------------------------
	
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
		tcnini.value = "";
		tcnend.value = "";
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
		}
    }
}

//%   insEnabledFields: Permite habilitar e inhabilitar los campos de la página.
//------------------------------------------------------------------------------------------
function insEnabledFields(lobject){
//------------------------------------------------------------------------------------------
	insBlankFields();
	switch(lobject.value){
//Ninguno	
	    case "0":{
            insShowInitial();
            break;
        }
//Cuadro póliza	
	    case "1":{
			document.getElementsByTagName("TR")[4].style.display='none';
			document.getElementsByTagName("TR")[5].style.display='none';
			document.getElementsByTagName("TR")[8].style.display='';
			ShowDiv('divType_mas', 'hide');
			break;
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
		insDefValues('ShowDataPolicy',	'nBranch='   + lintBranch  +
										'&nProduct=' + lintProduct +
										'&sCodispl=VIL1486_K' +
										'&nPolicy='  + llngPolicy,'/VTimeNet/policy/policyrep/');
    }
    else
		if (lobject == "nCertif"){
			insDefValues('ShowDataCertif', 'nBranch='+ lintBranch    +
				                           '&nProduct='+ lintProduct +
					                       '&nPolicy='+ llngPolicy   +
					                       '&sCodispl=VIL1486_K' +
						                   '&nCertif='+ llngCertif,'/VTimeNet/policy/policyrep/');
        }
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "VIL1486_k.aspx", 1, ""))
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With

mobjMenu = Nothing
%>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="VIL1486" ACTION="valPolicyRep.aspx?Mode=1">
	<BR><BR>
<%
Call insDefineHeader()

mobjValues = Nothing
%>

</FORM>
</BODY>
</HTML>






