<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

Dim dQuot_date As Date
Dim sStatus_ord As String


</script>
<%Response.Expires = -1
Response.CacheControl = "private"

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "si830_k"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 5/12/03 1:29 $|$$Author: Nvaplat22 $"

//% insStateZone: habilita los campos de la forma
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
	self.document.forms[0].tcnClaim.disabled = false;
	self.document.forms[0].tcdQuot_date.disabled = false;
	self.document.forms[0].btn_tcdQuot_date.disabled = false;
	switch(top.frames['fraSequence'].plngMainAction)
	{
		case 301:
			self.document.forms[0].elements["valServ_Ord"].Parameters.Param4.sValue = '2';
			break;
		case 302:
			self.document.forms[0].elements["valServ_Ord"].Parameters.Param4.sValue = '3';
			break;
	}	
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return true
}

//% insFinish: Ejecuta rutinas necesarias en el momento de Finalizar la página
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
   return true
}

//ChangeValues: Cambia y asigna los valores según la opción seleccionada.
//------------------------------------------------------------------------------------------
function ChangeValues(Field){
//------------------------------------------------------------------------------------------
	var strParams; 
	switch(Field.name){ 
		case "tcnClaim": 
			with(self.document.forms[0]){ 
				if(tcnClaim.value!="") {
					strParams = "nClaim=" + tcnClaim.value;
					insDefValues('Claim_SI830',strParams,'/VTimeNet/Claim/Claim'); 
				}
			}
			break;
		case "cbeCase_Num": 
			var lstrCase_num = '';
			var lstrDeman_type = '';
			var lstrClient = '';
			var lstrString = '';
			var lstrLocation = '';
	
			lstrString += Field.value;
			lstrCase_num = lstrString.substring(0,(lstrString.indexOf("/")));
			lstrDeman_type = lstrString.substr(lstrString.indexOf("/")+1,1);
			lstrClient += lstrString.replace(/.*\//,""); 
			lstrLocation += document.location.href; 
			lstrLocation = lstrLocation.replace(/&nCase_num.*/,"");
			lstrLocation = lstrLocation + "&nCase_num=" + lstrCase_num + "&nDeman_type=" + lstrDeman_type + "&sClient=" + lstrClient + "&nCaseNumber=" + Field.value;
			document.location.href = lstrLocation;
			break;
		case "valServ_Ord": 
			with(self.document.forms[0]){ 
                if(tcnClaim.value!="" && tcnClaim.value>0){
    				strParams = 'nServiceOrder=' + Field.value + 
    							'&nCaseNumber=' + cbeCase_Num.value + 
				                '&nClaim='   + elements["tcnClaim"].value +
				                '&sCertype=' + elements["tctCertype"].value + 
				                '&nBranch='  + elements["tcnBranch"].value + 
				                '&nProduct=' + elements["tcnProduct"].value +
				                '&nPolicy='  + elements["tcnPolicy"].value +
				                '&nCertif='  + elements["tcnCertif"].value +
				                '&sForm='    + sForm  
				    insDefValues('ServiceOrder',strParams,'/VTimeNet/Claim/Claim');
				}
			}
			break;
	}
}

//%ReloadPage: Dado el nro. de siniestro, se recarga la página con los valores necesarios para 
//             obtener el caso-tipo de demandante
// es llamado de la insdefvalue
//---------------------------------------------------------------------------------------------
function ReloadPage(){
//---------------------------------------------------------------------------------------------
	var lstrLocation = '';

	lstrLocation += document.location.href;
	lstrLocation = lstrLocation.replace(/&nClaim.*/,"");
	lstrLocation = lstrLocation + "&nClaim=" + self.document.forms[0].elements["tcnClaim"].value;
	lstrLocation = lstrLocation + "&nMainAction=" + top.frames['fraSequence'].plngMainAction;
	lstrLocation = lstrLocation + "&dQuot_date=" + self.document.forms[0].elements["tcdQuot_date"].value;
	lstrLocation = lstrLocation + "&nPolicy=" + self.document.forms[0].elements["tcnPolicy"].value;

	document.location.href = lstrLocation;
}
</SCRIPT>
    <%Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("SI830", "SI830_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmClaimPayment" ACTION="valClaim.aspx?sOriginalForm=<%=Request.QueryString("sOriginalForm")%>">
<BR><BR>
	<TABLE WIDTH="100%">
		<TR>
			<TD><LABEL>Siniestro</LABEL></TD> 
			<TD><%=mobjValues.NumericControl("tcnClaim", 10, Request.QueryString("nClaim"),  , "Número de siniestro al que se le registra la ", False, 0,  ,  ,  , "ChangeValues(this);", True)%></TD> 
			<TD><LABEL>Poliza</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 10, Request.QueryString("nPolicy"),  , "Número de la Poliza asociada al siniestro",  ,  ,  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
			<TD><LABEL>Caso</LABEL></TD>
			<TD>
				<%
With mobjValues
	.Parameters.Add("nClaim", mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeCase_Num", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType, Request.QueryString("nCaseNumber"), True,  ,  ,  , 1, "ChangeValues(this)", Request.QueryString("nClaim") = vbNullString,  , "Número de Caso al que se le registra la cotización", eFunctions.Values.eTypeCode.eString))
	If Request.QueryString("nCase_Num") = vbNullString Then
		Response.Write("<SCRIPT>if(self.document.forms[0].elements['cbeCase_Num'].value!='') ChangeValues(self.document.forms[0].elements['cbeCase_Num']);</SCRIPT>")
	End If
	Response.Write(mobjValues.HiddenControl("tcnCase_Num", Request.QueryString("nCase_Num")))
	Response.Write(mobjValues.HiddenControl("cbeDeman_type", Request.QueryString("nDeman_type")))
End With
%>
			</TD>
			<TD></TD>
			<TD></TD>
		</TR>
        <TR>
			<%
If Request.QueryString("dQuot_date") = vbNullString Then
	'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
	dQuot_date = Today
Else
	dQuot_date = Request.QueryString("dQuot_date")
End If
%>
            <TD><LABEL>Fecha</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdQuot_date", CStr(dQuot_date),  , "Fecha de efecto de la transacción",  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=0>Orden de servicio</LABEL></TD>
         	<TD><%
With mobjValues.Parameters
	.Add("nClaim", Request.QueryString("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nCase_Num", Request.QueryString("nCase_Num"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nDeman_Type", Request.QueryString("nDeman_Type"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	If Request.QueryString("nMainAction") = 301 Then
		sStatus_ord = "2"
	Else
		sStatus_ord = "3"
	End If
	.Add("sStatus_ord", sStatus_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("sOrdertype", "6", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("valServ_Ord", "Tab_Prof_OrdBudget", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , Request.QueryString("nClaim") = vbNullString, 10, "Número de la orden de servicio asociada a la cotización"))
%>
		    </TD>				            
		</TR>
    </TABLE>

<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>






