<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues
Dim sStatus_ord As String
Dim sOrdertype As String


</script>
<%Response.Expires = -1
Response.CacheControl = "private"

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "si831_k"
%>
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

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 5/12/03 1:29 $|$$Author: Nvaplat22 $"

//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(Act){
//------------------------------------------------------------------------------------------
	self.document.forms[0].tcnClaim.disabled = false;
	self.document.forms[0].tcdEffecdate.disabled = false;
	self.document.forms[0].btn_tcdEffecdate.disabled = false;
	switch(Act){ 
		case 301: 
			self.document.forms[0].tcnServiceOrder.Parameters.Param4.sValue = '8'; 
			self.document.forms[0].tcnServiceOrder.Parameters.Param5.sValue = '6'; 
			break; 
		case 401: 
			self.document.forms[0].tcnServiceOrder.Parameters.Param4.sValue = '3'; 
			self.document.forms[0].tcnServiceOrder.Parameters.Param5.sValue = '7'; 
			break; 
	} 
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

//ChangeValues: Cambia y asigna los valores según la opción seleccionada.
//------------------------------------------------------------------------------------------
function ChangeValues(Field){
//------------------------------------------------------------------------------------------
	var strParams; 
	switch(Field.name){ 
// Numero del siniestro  
		case "tcnClaim": 
			with(self.document.forms[0]){ 
				if(tcnClaim.value!="") {
					strParams = "nClaim=" + tcnClaim.value;
					insDefValues('Claim_SI831',strParams,'/VTimeNet/Claim/Claim'); 
				}
			}
			break;
//		Obtiene los datos de la orden de servicio.
		case "tcnServiceOrder": 
			with(self.document.forms[0]){ 
                if(tcnServiceOrder.value!="" && tcnServiceOrder.value>0){
    				strParams = 'nServiceOrder='+ nValue + 
								'&sForm=SI831' + 
								'&nCaseNumber=' + self.document.forms[0].valCaseNumber.value +
								'&nClaim='      + self.document.forms[0].elements["tcnClaim"].value
				    insDefValues('ServiceOrder',strParams,'/VTimeNet/Claim/Claim');
				}
			}
			break;
	}
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
			self.document.forms[0].elements["tcnCase_Num"].value = sCasenum
			self.document.forms[0].elements["tcnDeman_type"].value = sDeman_type
		}
	}	    
	else{
		if(typeof(document.forms[0].tcnServiceOrder)!='undefined'){
			document.forms[0].tcnServiceOrder.Parameters.Param2.sValue=0;
			document.forms[0].tcnServiceOrder.Parameters.Param3.sValue=0;
		}
    }         
}

//% ReloadPage: Recarga la página y asigna los valores almacenados en el QueryString - ACM - 18/07/2002
//%             es llamada desde la showdefvalues.aspx 
//-----------------------------------------------------------------------------------------------------
function ReloadPage(){
//-----------------------------------------------------------------------------------------------------
	var lstrLocation = '';

	lstrLocation += document.location.href;
	lstrLocation = lstrLocation.replace(/&nClaim.*/,"");
	lstrLocation = lstrLocation.replace(/&dEffecdate.*/,"")
	lstrLocation = lstrLocation.replace(/&nCaseNumber.*/,"")
	lstrLocation = lstrLocation + "&nClaim=" + self.document.forms[0].elements["tcnClaim"].value; 
	lstrLocation = lstrLocation + "&nMainAction=" + top.frames['fraSequence'].plngMainAction;
	lstrLocation = lstrLocation + "&dEffecdate=" + self.document.forms[0].elements["tcdEffecdate"].value; 
	lstrLocation = lstrLocation + "&nPolicy=" + self.document.forms[0].elements["tcnPolicy"].value; 
	document.location.href = lstrLocation; 
}

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("SI831", "SI831_k.aspx", 1, vbNullString))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SI831" ACTION="ValClaim.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0>Fecha</LABEL></TD>
            <TD><%'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'%>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today), True, "Fecha de efecto de la transacción",  ,  ,  ,  , Request.QueryString("nClaim") = vbNullString)%></TD>
		</TR>
        <TR>
            <TD><LABEL ID=0>Siniestro</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnClaim", 10, Request.QueryString("nClaim"), True, "Número de siniestro al que se le registra la orden de compra de repuestos",  ,  ,  ,  ,  , "ChangeValues(this);", Request.QueryString("nClaim") = vbNullString)%></TD>
			<TD><LABEL>Poliza</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 10, Request.QueryString("nPolicy"),  , "Poliza asociada al siniestro",  ,  ,  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
            <TD><LABEL ID=0>Caso</LABEL></TD>
			<TD><%
With mobjValues
	.Parameters.Add("nClaim", mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.BlankPosition = False
	If mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
		Response.Write(mobjValues.PossiblesValues("valCaseNumber", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType, Request.QueryString("nCase_Num"), True,  ,  ,  ,  , "ChangeValues(this)", False,  , "Número de Caso al que se le registra la cotización", eFunctions.Values.eTypeCode.eString))
	Else
		Response.Write(mobjValues.PossiblesValues("valCaseNumber", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  ,  , True,  , ""))
	End If
	Response.Write(mobjValues.HiddenControl("tcnCase_Num", Request.QueryString("nCase_Num")))
	Response.Write(mobjValues.HiddenControl("tcnDeman_type", Request.QueryString("nDeman_type")))
End With
%>
			</TD>
            <TD><LABEL ID=0>Orden de servicio</LABEL></TD>
         	<TD><%
With mobjValues.Parameters
	.Add("nClaim", Request.QueryString("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nCase_Num", Request.QueryString("nCase_Num"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nDeman_Type", Request.QueryString("nDeman_Type"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	If Request.QueryString("nMainAction") = 301 Then
		sStatus_ord = "8"
		sOrdertype = "6"
	Else
		sStatus_ord = "3"
		sOrdertype = "7"
	End If
	.Add("sStatus_ord", sStatus_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("sOrdertype", sOrdertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("tcnServiceOrder", "Tab_Prof_OrdQuotPart", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , Request.QueryString("nClaim") = vbNullString, 10, "Número de la orden de servicio asociada a la cotización"))
%>
		    </TD>				            
        </TR>
<%
Response.Write("<SCRIPT>insChangeCase(document.forms[0].valCaseNumber.value)</SCRIPT>")
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





