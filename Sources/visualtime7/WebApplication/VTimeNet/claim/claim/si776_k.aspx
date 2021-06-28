<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
Response.CacheControl = "private"

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "si776_k"
%>

<SCRIPT>
    document.VssVersion="$$Revision: 3 $|$$Date: 5/12/03 1:28 $|$$Author: Nvaplat22 $"

//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

//% insFinish: Ejecuta rutinas necesarias en el momento de Finalizar la página
//-----------------------------------------------------------------------------
function insFinish(){
//-----------------------------------------------------------------------------
   return true
}

//% insChangeCase: Actualiza los parámetros del valores posibles del servicio cuando cambia el caso
//-----------------------------------------------------------------------------
function insChangeCase(sCase){
//-----------------------------------------------------------------------------
    var sCasenum, sDeman_type;
            
    sCasenum = new String;
    sDeman_type = new String;   

    if (sCase != 0){
// Se obtiene el número de caso nCase_num    
	    sCasenum = sCase.substr(0,sCase.length-(sCase.length-sCase.indexOf('/')))
// Se obtiene el número de nDeman_type    
		sDeman_type = sCase.substr(sCase.indexOf('/')+1,(sCase.indexOf('/',sCase.indexOf('/')+1)-sCase.indexOf('/'))-1)
		if(typeof(document.forms[0].tcnServiceOrder)!='undefined'){
			document.forms[0].tcnServiceOrder.Parameters.Param2.sValue=sCasenum;
			document.forms[0].tcnServiceOrder.Parameters.Param3.sValue=sDeman_type;
			document.forms[0].tcnServiceOrder.value='';
			UpdateDiv('tcnServiceOrderDesc',' ','Normal');			
			UpdateDiv('cbeMark',' ','Normal');
			UpdateDiv('cbeModel',' ','Normal');
			UpdateDiv('tcnYear',' ','Normal');
			UpdateDiv('tctChasisCode',' ','Normal');			
		}
	}	    
	else{
		if(typeof(document.forms[0].tcnServiceOrder)!='undefined'){
			document.forms[0].tcnServiceOrder.Parameters.Param2.sValue=0;
			document.forms[0].tcnServiceOrder.Parameters.Param3.sValue=0;
		}
    }         
}

//% LoadClaimData: Asigna el valor del parámetro que recibe el campo "valCaseNumber"
//%                así como realizar la búsqueda de los datos pertinentes al siniestro
//%                y caso en tratamiento - ACM - 18/07/2002
//-----------------------------------------------------------------------------
function LoadClaimData(nValue){
//-----------------------------------------------------------------------------
	if(nValue!="" && nValue>0)        
		ShowPopUp("/VTimeNet/Claim/Claim/ShowDefValues.aspx?Field=Claim_SI774&sForm=" + "SI776" + 
													   "&nClaim=" + nValue, "ShowDefValuesClaim", 1, 1,"no","no",2000,2000);    
}

//% ReloadPage: Recarga la página y asigna los valores almacenados en el QueryString - ACM - 18/07/2002
//-----------------------------------------------------------------------------------------------------
function ReloadPage(nValue){
//-----------------------------------------------------------------------------------------------------
	var lstrLocation = '';
	
	if(nValue!="" && nValue>0)
	{
		lstrLocation += document.location.href;
		lstrLocation = lstrLocation.replace(/&nClaim.*/,"")
		lstrLocation = lstrLocation.replace(/&dEffecdate.*/,"")
		lstrLocation = lstrLocation.replace(/&nCaseNumber.*/,"")
		lstrLocation = lstrLocation + "&nClaim=" + self.document.forms[0].elements["tcnClaim"].value;		
		lstrLocation = lstrLocation + "&dEffecdate=" + self.document.forms[0].elements["tcdEffecdate"].value;		
		document.location.href = lstrLocation;
	}
}

//%LoadServiceOrderData: Obtiene los datos de la orden de servicio.
//-----------------------------------------------------------------------------------------------------
function LoadServiceOrderData(nValue){
//-----------------------------------------------------------------------------------------------------
	if(nValue!="" && nValue>0)
		insDefValues('ServiceOrder','nServiceOrder='+ nValue + '&sForm=SI776' + 
		                            '&nCaseNumber=' + self.document.forms[0].valCaseNumber.value +
		                            '&nClaim='      + self.document.forms[0].elements["tcnClaim"].value,'/VTimeNet/Claim/Claim');
}
</SCRIPT>

<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("SI776", "SI776_k.aspx", 1, vbNullString))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SI776" ACTION="ValClaim.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="6%"><LABEL ID=0>Fecha</LABEL></TD>
            <TD WIDTH="40%"><%'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'%>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today), True, "Fecha de efecto de la transacción",  ,  ,  ,  , False)%></TD>
            <TD WIDTH="1%">&nbsp;</TD>
            <TD WIDTH="15%"><LABEL ID=0>Siniestro</LABEL></TD>
            <TD WIDTH="30%"><%=mobjValues.NumericControl("tcnClaim", 10, Request.QueryString("nClaim"), True, "Número de siniestro al que se le registra la orden de compra de repuestos",  ,  ,  ,  ,  , "LoadClaimData(this.value); ReloadPage(this.value);", False)%></TD>
            <TD WIDTH="1%">&nbsp;</TD>
		</TR>
		<TR>
            <TD><LABEL ID=0>Caso</LABEL></TD>
			<TD><%
With mobjValues
	.Parameters.Add("nClaim", mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.BlankPosition = False
	If mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
		Response.Write(mobjValues.PossiblesValues("valCaseNumber", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType, Request.QueryString("nCase_Num"), True,  ,  ,  ,  , "insChangeCase(this.value)", False,  , ""))
	Else
		Response.Write(mobjValues.PossiblesValues("valCaseNumber", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  ,  , True,  , ""))
	End If
End With
%>
			</TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0>Orden de servicio</LABEL></TD>
         	<TD><%
With mobjValues.Parameters
	.Add("nClaim", Request.QueryString("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nCase_Num", Request.QueryString("nCase_Num"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nDeman_Type", Request.QueryString("nDeman_Type"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("sStatus_ord", "8", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("sOrdertype", "4,7", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("tcnServiceOrder", "Tab_Prof_OrdQuotPart", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "LoadServiceOrderData(this.value);", Request.QueryString("nClaim") = "", 10, "Número de la orden de servicio asociada a la cotización"))
%>
		    </TD>				            
        </TR>
	</TABLE>
        <DIV ID="lblauto">
			<TABLE WIDTH="100%">
				<TR>
				    <TD WIDTH="6%"><LABEL ID=0>Marca</LABEL></TD>
				    <TD WIDTH="40%"><%=mobjValues.DIVControl("cbeMark",  , "")%></TD>
				    <TD WIDTH="1%">&nbsp;</TD>
				    <TD WIDTH="15%"><LABEL ID=0>Modelo</LABEL></TD>
				    <TD WIDTH="30%"><%=mobjValues.DIVControl("cbeModel",  , "")%></TD>
				    <TD WIDTH="1%">&nbsp;</TD>
				</TR>
				<TR>
				    <TD><LABEL ID=0>Año</LABEL></TD>
				    <TD><%=mobjValues.DIVControl("tcnYear",  , "")%></TD>            
				    <TD>&nbsp;</TD>
				    <TD><LABEL>Chasis</LABEL></TD>
				    <TD><%=mobjValues.DIVControl("tctChasisCode",  , "")%></TD>
				</TR>
				<TR>                
				    <%Response.Write(mobjValues.HiddenControl("tcnOrder_Type", CStr(0)))%>
				    <%Response.Write(mobjValues.HiddenControl("tcnTypeOrder", CStr(0)))%>
				    <%Response.Write(mobjValues.HiddenControl("tctStateOrder", ""))%>            
				    <%Response.Write(mobjValues.HiddenControl("tcnTransaction", CStr(0)))%>
				    <%Response.Write(mobjValues.HiddenControl("tctChasisCode", ""))%>
				    <%Response.Write(mobjValues.HiddenControl("tcnBranch_Fire", CStr(0)))%>
				</TR>
			</TABLE>
		</DIV>
	</TABLE>
<%
Response.Write("<SCRIPT>insChangeCase(document.forms[0].valCaseNumber.value)</SCRIPT>")
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




