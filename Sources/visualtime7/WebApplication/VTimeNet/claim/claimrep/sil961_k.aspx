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

'- Variables para establecer el número de siniestro a trabajar en la página.
'- La primera para el combo de casos.  La segunda, para el campo de siniestros.
Dim mlngClaim As Object
Dim mstrClaim As String
Dim mstrCase_num As Object

'- Variables para establecer el ramo.
Dim mstrBranch As String

'- Variables para establecer el producto..    
Dim mstrProduct As String

'- Variables para establecer el producto..    
Dim mstrPolicy As String
Dim mstrCertif As String


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sil961_k")


'UPGRADE_WARNING: Use of Null/IsNull() detected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1049.aspx'
mlngClaim = System.DBNull.Value
mstrClaim = ""
mstrCase_num = ""
mstrBranch = ""
mstrProduct = ""
mstrPolicy = ""
mstrCertif = ""


mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "sil961_k"

Response.Write("<SCRIPT>var mlngClaim</SCRIPT>")

If Request.QueryString("nBranch") <> vbNullString Then
	mstrBranch = Request.QueryString("nBranch")
End If

If Request.QueryString("nProduct") <> vbNullString Then
	mstrProduct = Request.QueryString("nProduct")
End If

If Request.QueryString("nPolicy") <> vbNullString Then
	mstrPolicy = Request.QueryString("nPolicy")
End If

If Request.QueryString("nCertif") <> vbNullString Then
	mstrCertif = Request.QueryString("nCertif")
End If

If Request.QueryString("nClaim") <> vbNullString Then
	mlngClaim = Request.QueryString("nClaim")
	mstrClaim = Request.QueryString("nClaim")
	mstrCase_num = 1
	Response.Write("<SCRIPT>mlngClaim=" & Request.QueryString("nClaim") & "</SCRIPT>")
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
   return true;
}

function ShowPolicy(nBranch, nProduct, nPolicy)
//-----------------------------------------------------------------------------------------
{	
	if (nBranch.value!=0 && nProduct.value!=0 && nPolicy.value!=0)	
		insDefValues('SIL762', 'sCertype=2' + '&nBranch=' + nBranch.value + '&nProduct=' + nProduct.value + '&nPolicy=' + nPolicy.value,'/VTimeNet/Claim/ClaimRep')
}

//%ShowChangeValues: Evento OnChange de CbeBranch
//-----------------------------------------------------------------------------
function ShowChangeValues(Control)
//-----------------------------------------------------------------------------
{
//% Los valores 1,2 y 6 corresponden al ramo VIDA...TABLE10
//-----------------------------------------------------------------------------
	
	    self.document.forms[0].cbeBranch.disabled=false;
	    self.document.forms[0].valProduct.disabled=false;
	    self.document.forms[0].tcnPolicy.disabled=false;
	    self.document.forms[0].tcnClaim.disabled=false;
	     
}

//% AddClaimParameter: Actualiza el Valor del Parametro para el control de Casos 
//%                    de Siniestros y la Ubicación
//-----------------------------------------------------------------------------
function AddClaimParameter(nValue)
//-----------------------------------------------------------------------------
{
	var nindChech
	var lstrQString

	with(self.document.forms[0])
	{
		if(tcnClaim.value==0)
		{
			cbeCase.options.length=0;
			cbeCase.disabled=true;
			tcnCaseNum.value="";
			tcnDeman_Type.value="";
		}
		else
		{
			if(mlngClaim!=tcnClaim.value)


		    self.document.location.href = "SIL961_K.aspx?sCodispl=SIL961" +
											             "&nBranch=" + cbeBranch.value +
											             "&nProduct=" + valProduct.value +
											             "&nPolicy=" + tcnPolicy.value +
											             "&nCertif=" + tcnCertif.value +
											             "&nClaim=" + tcnClaim.value + "&sConfig=InSequence" +
														 "&nHeight=200";
        }
    }
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

	  
    if (self.document.forms[0].cbeCase.value==0)
    {
       self.document.forms[0].tcnCaseNum.value = -32768;
       self.document.forms[0].tcnDeman_Type.value = -32768;
	}
	else
	{
       self.document.forms[0].tcnCaseNum.value = lstrCase_num;
       self.document.forms[0].tcnDeman_Type.value = lstrDemanType;
    }
}

//%Llamado a la página ShowdefValues
//-----------------------------------------------------------------------------------------
function insDefValues(sKey,sParameters,sPath){
//-------------------------------------------------------------------------------------------
    if (typeof(top)!='undefined')
        if (typeof(top.frames)!='undefined')
            if (typeof(top.frames["fraGeneric"])!='undefined')
            {
                sPath = (typeof(sPath)=='undefined'?'':sPath + '/')
                sParameters = (typeof(sParameters)=='undefined'?'':'&' + sParameters)
                top.frames["fraGeneric"].location.href = sPath + 'ShowDefValues.aspx?Field=' + sKey  + sParameters;
            }
}

//%insStateCheck: Habilita las fechas cuando el proceso es masivo
//-------------------------------------------------------------
function insStateCheck(){
//-------------------------------------------------------------
//self.document.forms[0].tcdDate_ini.required

	with(self.document.forms[0]){
		if (optOption[0].checked){
			tcdDate_ini.disabled=true;	
			btn_tcdDate_ini.disabled=true;	
			tcdDate_end.disabled=true;	
			btn_tcdDate_end.disabled=true;	
		}	
		else{
			tcdDate_ini.disabled=false;	
			btn_tcdDate_ini.disabled=false;	
			tcdDate_end.disabled=false;	
			btn_tcdDate_end.disabled=false;	
			
			
		}	
	}
}
//% ClaimField: Limpia campo ramo-producto, si existe siniestro, si se ingresa ramo limpia siniestro
//------------------------------------------------------------------------------------------
function ClaimField(Field){
//------------------------------------------------------------------------------------------
var lstrQString

with (self.document.forms[0]){

		lstrQString = 'nClaim=' + Field.value  
        insDefValues('ShowClaim',lstrQString,'/VTimeNet/Claim/Claim');
 }
}

//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision:  $|$$Date:  $|$$Author: $" 

</SCRIPT>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("SIL961", "SIL961_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing

If Request.QueryString("nClaim") <> vbNullString Then
	'Response.Write "<NOTSCRIPT> alert(""" & Request.QueryString("nClaim") & """)</script>" 
	
	Response.Write("<SCRIPT>insDefValues('ShowClaim','nClaim= " & Request.QueryString("nClaim") & "','/VTimeNet/Claim/ClaimRep')</SCRIPT>")
End If
%>    
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SIL961" ACTION="valClaimRep.aspx?sMode=1">
<BR><BR><BR>
	<%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
<BR><BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0>Ramo</LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", "Código del Ramo.", mstrBranch,  ,  ,  ,  , "ShowChangeValues(this.value);",  , 1)%></TD>
            <TD><LABEL ID=0>Producto</LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", "Código del Producto a procesar.",  , eFunctions.Values.eValuesType.clngWindowType,  , mstrProduct,  ,  ,  ,  , 2)%></TD>

        </TR>
        <TR>
            <TD><LABEL ID=0>Póliza</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, mstrPolicy,  , "Póliza que declara el siniestro",  , 0,  ,  ,  , "ShowPolicy(cbeBranch, valProduct, this);",  , 3)%></TD>
	       	<TD><LABEL ID=9381>Certificado</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCertif", 10, mstrCertif,  , "Número de certificado al cual corresponde el siniestro",  , 0,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0>Siniestro</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnClaim", 10, mstrClaim,  , "Número del Siniestro",  , 0,  ,  ,  , "AddClaimParameter(this.value)",  , 4)%></TD>              
            <TD><LABEL ID=0>Caso</LABEL></TD>
  			  	<%mobjValues.Parameters.Add("nClaim", mlngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
					<%mobjValues.BlankPosition = False%>
            <TD><%=mobjValues.PossiblesValues("cbeCase", "tabClaim_cases", eFunctions.Values.eValuesType.clngComboType, "", True,  ,  ,  ,  , "insParam()", False,  , "Caso asociado al siniestro",  , 5)%></TD>
   			    <%=mobjValues.HiddenControl("tcnCaseNum", mstrCase_num)%>
			    <%=mobjValues.HiddenControl("tcnDeman_Type", "")%>
        </TR>
         <TR>
            <TD><LABEL ID=0>Nota</LABEL></TD>
  			  	<%mobjValues.Parameters.Add("nClaim", mlngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
  			  	<%mobjValues.BlankPosition = False%>
            <TD><%=mobjValues.PossiblesValues("nNotenum", "tabClaim_Hisnot", eFunctions.Values.eValuesType.clngComboType, "", True,  ,  ,  ,  , "", False,  , "Caso asociado al siniestro",  , 5)%></TD>
        </TR>
        
  </TABLE>
  <SCRIPT>
      ShowChangeValues(self.document.forms[0].cbeBranch.value);
  </SCRIPT>
</FORM>
</BODY>
</HTML>

<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("sil961_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




