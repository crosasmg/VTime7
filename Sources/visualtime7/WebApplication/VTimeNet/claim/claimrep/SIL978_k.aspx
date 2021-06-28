<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

'- Objeto para el manejo de Siniestro   
Dim mobjClaim As Object


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("SIL978_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")

mobjValues.sCodisplPage = "SIL978_k"
Response.Write("<SCRIPT>var mlngClaim</SCRIPT>")

%>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->



<HTML>
<HEAD>

<SCRIPT> 

//% insStateZone: se manejan los campos de la página
//-----------------------------------------------------------------------------
function insStateZone()
//-----------------------------------------------------------------------------
{
}
//% insPreZone: Se maneja la Acción para la Busqueda por Condición
//-----------------------------------------------------------------------------
function insPreZone(llngAction)
//-----------------------------------------------------------------------------
{
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel()
//-----------------------------------------------------------------------------
{
   return true
}


//%insParam: Asigna los valores a los campos ocultos
//%------------------------------------------------------------------------------------------
function insParam() 
//%------------------------------------------------------------------------------------------
{
	var mstrString = ""; 
    mstrString += document.location; 
	var lstrCampo=self.document.forms[0].cbeCase.value;
	var lstrStart=lstrCampo.indexOf("/");
	var lstrCase_num = unescape(lstrCampo.substring(0,lstrStart));
	var lstrCampo1 = lstrCampo.substring(lstrStart+1,lstrCampo.legth);
    var lstrStart1 = lstrCampo1.indexOf("/");		
	var lstrDemanType = unescape(lstrCampo1.substring(0,lstrStart1));

    if (self.document.forms[0].cbeCase.value==0){
       self.document.forms[0].tcnCaseNum.value = -32768;
       self.document.forms[0].tcnDeman_Type.value = -32768;
	}
	else{
        self.document.forms[0].tcnCaseNum.value = lstrCase_num;
        self.document.forms[0].tcnDeman_Type.value = lstrDemanType;
     }
	mstrString = mstrString.replace(/&sCase=.*/, "");
	mstrString = mstrString.replace(/&nCase_num=.*/, ""); 
	mstrString = mstrString.replace(/&nDeman_type=.*/, "");  
	mstrString = mstrString + "&sCase=" + lstrCampo ;
	mstrString = mstrString + "&nCase_num=" + lstrCase_num; 
	mstrString = mstrString + "&nDeman_type=" + lstrDemanType ;  
	document.location = mstrString;     
     
}


//% ReloadPage: se recarga la página para asignar valor al combo de Casos
//-------------------------------------------------------------------------------------------
function ReloadPage(Field){
//-------------------------------------------------------------------------------------------
	var mstrString = ""; 
    mstrString += document.location; 
	with(self.document.forms[0]){
		if(tcnClaim.value==0){
		   cbeCase.value=0
		}	
    }	
    mstrString = mstrString.replace(/&nClaimNumber=.*/, ""); 
    mstrString = mstrString + "&nClaimNumber=" + Field.value ; 
    document.location = mstrString; 
}

</SCRIPT>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("SIL978", "SIL978_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>


<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 28-03-13 3:11 $"</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SIL978" ACTION="valClaimRep.aspx?sMode=1">
	<BR><BR>
		<%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
	<BR><BR>
	<TABLE WIDTH="100%">
	    <TR>
		    <TD><LABEL ID=0>Siniestro</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnClaim", 10, Request.QueryString("nClaimNumber"),  , "Número que identifica al siniestro al que se le realiza el pago",  , 0,  ,  ,  , "ReloadPage(this)")%></TD>
	    </TR>
	    <TR>
			<TD><LABEL ID=0>Caso</LABEL></TD>
			<%
If Request.QueryString("nClaimNumber") = vbNullString Then
	mobjValues.Parameters.Add("nClaim", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Else
	mobjValues.Parameters.Add("nClaim", mobjValues.StringToType(Request.QueryString("nClaimNumber"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End If
'mobjValues.BlankPosition = False
%>
			<TD><%=mobjValues.PossiblesValues("cbeCase", "tabClaim_cases", eFunctions.Values.eValuesType.clngComboType, Request.QueryString("sCase"), True,  ,  ,  ,  , "insParam()", Request.QueryString("nClaimNumber") = vbNullString,  , "Caso asociado al beneficiario, del cual sale el finiquito")%></TD>
			<%=mobjValues.HiddenControl("tcnCaseNum", Request.QueryString("nCase_Num"))%>
			<%=mobjValues.HiddenControl("tcnDeman_Type", Request.QueryString("nDeman_Type"))%>
	    </TR>
  </TABLE>
</FORM>
</BODY>
</HTML>

<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("SIL978_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




