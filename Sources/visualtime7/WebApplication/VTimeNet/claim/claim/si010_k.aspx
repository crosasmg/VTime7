<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.12
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim lclsProduct As eProduct.Product
Dim lstrCase As String

Dim lclsClaim As eClaim.Claim


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si010_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si010_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
lclsProduct = New eProduct.Product
%>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tmenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>
 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 12.31 $"

//%OnChangeClaim: Carga el combo de casos asociados al siniestro
//----------------------------------------------------------------------------------------------------
function OnChangeClaim(Field){
//----------------------------------------------------------------------------------------------------
	var lstrClaim = "";
	var lstrLocation = "";

	if(Field.value!=="")
	{
		lstrClaim = Field.value;
		lstrLocation = document.location.href;
		lstrLocation = lstrLocation.replace(/&nClaim.*/,"");
		lstrLocation = lstrLocation + "&nClaim=" + lstrClaim + "&tcdEffecdate=" + self.document.forms[0].tcdEffecdate.value ;
		document.location = lstrLocation;
	}
}

//%ExtractCaseNumber: Extrae el número de caso de la cadena contenida en el combo de casos
//----------------------------------------------------------------------------------------------------
function ExtractCaseNumber(Field){
//----------------------------------------------------------------------------------------------------
	var lstrCase_num = '';
	var lstrDeman_type = '';
	var lstrClient = '';
	var lstrString = '';
	var lstrLocation = '';
	
	if(Field.value!=='')
	{
		lstrString = Field.value
		lstrCase_num = lstrString.substring(0,(lstrString.indexOf("/")))
		lstrDeman_type = lstrString.substr(lstrString.indexOf("/")+1,1)
		lstrClient = lstrString.replace(/.*\//,"")
		
		self.document.forms[0].nCase_num.value = lstrCase_num
		self.document.forms[0].nDeman_type.value = lstrDeman_type
		self.document.forms[0].sClient.value = lstrClient		
	}
}

//%ChangeValues: Se asignan los valores según el caso en tratamiento
//----------------------------------------------------------------------------------------------------
function ChangeValues()
//----------------------------------------------------------------------------------------------------
{	var lstrString

	lstrString = self.document.forms[0].cbeCase.value
	self.document.forms[0].nCase_num.value = lstrString.substring(0,(lstrString.indexOf("/")));
	self.document.forms[0].nDeman_type.value = lstrString.substr(lstrString.indexOf("/")+1,1);
	self.document.forms[0].sClient.value = lstrString.replace(/.*\//,"") ;

}

//% insStateZone: se controla el estado de los campos de la página
//----------------------------------------------------------------------------------------------------
function insStateZone(){
//----------------------------------------------------------------------------------------------------

}
//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("SI010", "SI010_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
End With
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="post" ID="FORM" NAME="frmManReceiptk" ACTION="ValClaim.aspx?sMode=1">
<%lclsClaim = New eClaim.Claim

If Request.QueryString("nClaim") <> "" Then
	Call lclsClaim.Find(Request.QueryString("nClaim"))
	'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
	Call lclsProduct.Find(lclsClaim.nBranch, lclsClaim.nProduct, Today)
End If
%>	
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=9145>Fecha</LABEL></TD>
            <TD><%
If Request.QueryString("tcdEffecdate") <> vbNullString Then
	Response.Write(mobjValues.DateControl("tcdEffecdate", Request.QueryString("tcdEffecdate"),  , "Fecha efectiva del reverso."))
Else
	'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
	Response.Write(mobjValues.DateControl("tcdEffecdate", CStr(Today),  , "Fecha efectiva del reverso."))
End If
%></TD>
            <TD COLSPAN="4">&nbsp;</TD>
		</TR>
		<TR>
		    <TD><LABEL ID=9144>Siniestro</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnClaim", 10, Request.QueryString("nClaim"),  , "Número del siniestro al que se le reversan los movimientos.",  , 0,  ,  ,  , "OnChangeClaim(this);")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=9142>Caso</LABEL></TD>
            <TD COLSPAN=2>
<%
With mobjValues
	.Parameters.Add("nClaim", .StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	If Request.QueryString("nCase_num") <> vbNullString And Request.QueryString("nDeman_type") <> vbNullString And Request.QueryString("sClient") <> vbNullString Then
		lstrCase = CStr(Request.QueryString("nCase_num")) & "/" & CStr(Request.QueryString("nDeman_type")) & "/" & Request.QueryString("sClient")
		Response.Write(mobjValues.PossiblesValues("cbeCase", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType, "" & lstrCase, True,  ,  ,  ,  , "ExtractCaseNumber(this);", Request.QueryString("nClaim") = vbNullString))
		Response.Write(mobjValues.HiddenControl("nCase_num", Request.QueryString("nCase_num")))
		Response.Write(mobjValues.HiddenControl("nDeman_type", Request.QueryString("nDeman_type")))
	Else
		.BlankPosition = False
		Response.Write(.PossiblesValues("cbeCase", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  , "ExtractCaseNumber(this);", Request.QueryString("nClaim") = vbNullString))
	End If
End With
%>
	        </TD> 
		</TR>            
		<TR>            
			<TD><LABEL ID=9147>Póliza</LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("lblBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, CStr(lclsClaim.nBranch),  , True,  ,  ,  ,  ,  ,  , "Ramo, producto, póliza y certificado asociados al siniestro en tratamiento.")%></TD>
			<TD><%=mobjValues.TextControl("lblProduct", 30, lclsProduct.sShort_des,  , "", True)%></TD>
			<TD>&nbsp;</TD>
			<TD WIDTH="10%"><%=mobjValues.TextControl("lblPolicy", 30, mobjValues.TypeToString(lclsClaim.nPolicy, eFunctions.Values.eTypeData.etdDouble),  , "", True)%></TD>
			<TD><LABEL ID=9140>/</LABEL>
				<%=mobjValues.TextControl("lblCertif", 30, mobjValues.TypeToString(lclsClaim.nCertif, eFunctions.Values.eTypeData.etdDouble),  , "", True)%>
			</TD>
			<%=mobjValues.HiddenControl("nDeman_type", CStr(lclsClaim.nDeman_type))%>
			<%=mobjValues.HiddenControl("nCase_num", CStr(lclsClaim.nCase_num))%>
			<%=mobjValues.HiddenControl("sClient", CStr(lclsClaim.nCase_num))%>
        </TR>
    </TABLE>
<%
Response.Write("<SCRIPT>ChangeValues();</script>")
'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
lclsClaim = Nothing
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
lclsProduct = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.12
Call mobjNetFrameWork.FinishPage("si010_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




