<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
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
Dim mobjClaim As eClaim.Claim

'- Objeto para el manejo Campos de la pagina    
Dim mstrBranch As Integer
Dim mstrProduct As Integer
Dim mstrPolicy As Double
Dim mstrCertif As Double
Dim mblnFound As Boolean
Dim mintOffice As Integer


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sil005_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")

mobjValues.sCodisplPage = "sil005_k"

Response.Write("<SCRIPT>var mlngClaim</SCRIPT>")
mblnFound = False
If Request.QueryString("nClaimNumber") <> vbNullString Then
	Response.Write("<SCRIPT>mlngClaim=" & Request.QueryString("nClaimNumber") & "</SCRIPT>")
	mobjClaim = New eClaim.Claim
	If mobjClaim.Find(mobjValues.StringToType(Request.QueryString("nClaimNumber"), eFunctions.Values.eTypeData.etdDouble)) Then
		mstrBranch = mobjClaim.nBranch
		mstrProduct = mobjClaim.nProduct
		mstrPolicy = mobjClaim.nPolicy
		mstrCertif = mobjClaim.nCertif
		mintOffice = mobjClaim.nOffice
		mblnFound = True
	End If
	'UPGRADE_NOTE: Object mobjClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjClaim = Nothing
End If

If Not mblnFound Then
	mstrBranch = Request.QueryString("nBranch")
	mstrProduct = Request.QueryString("nProduct")
	mstrPolicy = Request.QueryString("nPolicy")
	mstrCertif = Request.QueryString("nCertif")
	mintOffice = Request.QueryString("nOffice")
End If

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


//%ShowChangeValues: Evento OnChange de CbeBranch
//-----------------------------------------------------------------------------
function ShowChangeValues(Control)
//-----------------------------------------------------------------------------
{
	if(Control=="0"){
	    self.document.forms[0].valProduct.disabled=true
	    self.document.forms[0].tcnPolicy.disabled=true
	}
	else{	    
	    self.document.forms[0].valProduct.disabled=false
	    self.document.forms[0].tcnPolicy.disabled=false	    
   }     
}
//%ShowPolicy: Busca el tipo de la póliza para habilitar o desabilitar el campo certificado.
//-----------------------------------------------------------------------------------------
function ShowPolicy(nBranch, nProduct, nPolicy)
//-----------------------------------------------------------------------------------------
{	
	if (nBranch.value!=0 && nProduct.value!=0 && nPolicy.value!=0)	
		insDefValues('SIL005', 'sCertype=2' + '&nBranch=' + nBranch.value + '&nProduct=' + nProduct.value + '&nPolicy=' + nPolicy.value,'/VTimeNet/Claim/ClaimRep')
}

//%insParam: Asigna los valores a los campos ocultos
//%------------------------------------------------------------------------------------------
function insParam() 
//%------------------------------------------------------------------------------------------
{
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
       self.document.forms[0].tcnCaseNum.value = lstrCase_num
       self.document.forms[0].tcnDeman_Type.value = lstrDemanType
     }
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
		   cbeBranch.value=0
		   valProduct.value=0
			UpdateDiv("valProductDesc","")
		}	
    }	
    mstrString = mstrString.replace(/&nClaimNumber=.*/, ""); 
    mstrString = mstrString + "&nClaimNumber=" + Field.value ; 
    document.location = mstrString; 
}

//%Llamado a la página ShowdefValues
//-----------------------------------------------------------------------------------------
function insDefValues(sKey,sParameters,sPath){
//-------------------------------------------------------------------------------------------
    if (typeof(top)!='undefined')
        if (typeof(top.frames)!='undefined')
            if (typeof(top.frames["fraGeneric"])!='undefined'){
                sPath = (typeof(sPath)=='undefined'?'':sPath + '/')
                sParameters = (typeof(sParameters)=='undefined'?'':'&' + sParameters)
                top.frames["fraGeneric"].location.href = sPath + 'ShowDefValues.aspx?Field=' + sKey  + sParameters;
            }
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
Response.Write(mobjMenu.MakeMenu("SIL005", "SIL005_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>


<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SIL005" ACTION="valClaimRep.aspx?sMode=1">
	<BR><BR>
		<%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
	<BR><BR>
	<TABLE WIDTH="100%">
	    <TR>
			<TD><LABEL ID=0>Sucursal</LABEL></TD>
			<TD COLSPAN="2"><%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, CStr(mintOffice),  ,  ,  ,  ,  ,  ,  ,  , "Sucursal a la que corresponden los finiquitos")%></TD>
	    </TR>
	    <TR>
			<TD WIDTH="20%"><LABEL ID=0>Ramo</LABEL></TD>			
			<TD COLSPAN="2" WIDTH="30%"><%=mobjValues.BranchControl("cbeBranch", "Ramo al que pertenecen los finiquitos.", CStr(mstrBranch),  ,  ,  ,  , "ShowChangeValues(this.value);ShowPolicy(this, valProduct, tcnPolicy);")%></TD>
			<TD WIDTH="15%"><LABEL ID=0>Producto</LABEL></TD>
			<TD COLSPAN="3"><%=mobjValues.ProductControl("valProduct", "Producto al que pertenecen los finiquitos", CStr(mstrBranch), eFunctions.Values.eValuesType.clngWindowType,  , CStr(mstrProduct),  ,  ,  , "ShowPolicy(cbeBranch, this, tcnPolicy);")%></TD>
	    </TR>
	    <TR>
			<TD><LABEL ID=0>Poliza</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 6, CStr(mstrPolicy), False, "Póliza a la que pertenecen los finiquitos",  ,  ,  ,  ,  , "ShowPolicy(cbeBranch, valProduct, this);", mstrPolicy = CDbl(vbNullString))%></TD>
			<TD WIDTH="15%">&nbsp</TD>
			<TD><LABEL ID=0>Certificado</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCertif", 6, CStr(mstrCertif), False, "Certificado al que pertenecen los finiquitos",  ,  ,  ,  ,  ,  , mstrCertif = CDbl(vbNullString))%></TD>
	    </TR>
	    <TR>
		    <TD><LABEL ID=0>Siniestro</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnClaim", 10, Request.QueryString("nClaimNumber"),  , "Número que identifica al siniestro al que se le realiza el pago",  , 0,  ,  ,  , "ReloadPage(this)")%></TD>
	    </TR>
	    <TR>
		    <TD><LABEL ID=0>Finiquito</LABEL></TD>
		    <TD><%=mobjValues.NumericControl("tcnFinishNum", 6, Request.QueryString("nFinishNum"), False, "Número de finiquito a listar")%></TD>
			<TD>&nbsp</TD>	    
		    <TD><LABEL ID=0>Caso</LABEL></TD>
			<%
If Request.QueryString("nClaimNumber") = vbNullString Then
	mobjValues.Parameters.Add("nClaim", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Else
	mobjValues.Parameters.Add("nClaim", mobjValues.StringToType(Request.QueryString("nClaimNumber"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End If
mobjValues.BlankPosition = False
%>
			<TD><% =mobjValues.PossiblesValues("cbeCase", "TabBuildingCase", eFunctions.Values.eValuesType.clngComboType, "", True,  ,  ,  ,  , "insParam()", Request.QueryString("nClaimNumber") = vbNullString,  , "Caso asociado al beneficiario, del cual sale el finiquito")%></TD>
			<%=mobjValues.HiddenControl("tcnCaseNum", CStr(eRemoteDB.Constants.intNull))%>
			<%=mobjValues.HiddenControl("tcnDeman_Type", CStr(eRemoteDB.Constants.intNull))%>
	    </TR>
  </TABLE>
</FORM>
</BODY>
</HTML>

<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("sil005_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




