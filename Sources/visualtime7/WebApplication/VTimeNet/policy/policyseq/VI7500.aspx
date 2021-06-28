<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSaapv" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mobjProduct_li As Object

Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de las funciones generales de carga de valores 
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As Object

Dim mobjsAapv As eSaapv.Saapv
Dim lclsGeneral As eGeneral.OptionsInstallation
Dim mintInstitution As Integer
Dim mintType_Saapv As Integer
Dim minNtype_ameapv As Integer
Dim minDlimitdate As Date
Dim minScertype2 As String
Dim minNBranch2 As Double
Dim minNProduct2 As Double
Dim minNpolicy2 As Double


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjsAapv = New eSaapv.Saapv

lclsGeneral = New eGeneral.OptionsInstallation

Call mobjsAapv.Find_policy(CStr(Session("sCertype")), CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")), CDbl(Session("nCertif")))

If mobjsAapv.nInstitution = eRemoteDB.Constants.intNull Then
	If lclsGeneral.FindOptPolicy Then
		mintInstitution = lclsGeneral.nInstitution
	End If
Else
	mintInstitution = mobjsAapv.nInstitution
	
End If

If mobjsAapv.nType_saapv = eRemoteDB.Constants.intNull Then
	mintType_Saapv = 1
Else
	mintType_Saapv = mobjsAapv.nType_saapv
End If

minNtype_ameapv = mobjsAapv.Ntype_ameapv
minDlimitdate = mobjsAapv.Dlimitdate
minScertype2 = mobjsAapv.sCertype2
minNBranch2 = mobjsAapv.nBranch2
minNProduct2 = mobjsAapv.nProduct2
minNpolicy2 = mobjsAapv.npolicy2

mobjValues.sCodisplPage = Request.QueryString("sCodispl")
mobjMenu = New eFunctions.Menues
'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
lclsGeneral = Nothing
%>   
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->
	
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    
<SCRIPT LANGUAGE=javascript>

//% ShowSubSequence: Muestra la subsecuencia de características de vida
//--------------------------------------------------------------------------------------------
function ShowSubSequence(){
//--------------------------------------------------------------------------------------------
   var lstrstring = '';
    
	lstrstring = "&Policy=1&nCod_saapv=" + self.document.forms[0].tcncod_saapv.value + 
	            "&nInstitution=" + self.document.forms[0].hddInstitution.value +
	            "&nStatus=" + self.document.forms[0].cbestatus_saapv.value +
	            "&nType_saapv=" + self.document.forms[0].cbeType_saapv.value +
	            "&dEffecdate=" + self.document.forms[0].tcdissue_dat.value +
	            "&Ntype_ameapv=" + self.document.forms[0].hddNtype_ameapv.value +  
	            "&Dlimitdate=" + self.document.forms[0].hddDlimitdate.value + 
                "&Scertype2=" + self.document.forms[0].hddScertype2.value +
                "&NBranch2=" + self.document.forms[0].hddNBranch2.value +
                "&NProduct2=" + self.document.forms[0].hddNProduct2.value +
	            "&npolicy2=" + self.document.forms[0].hddNpolicy2.value +
				"&nMainAction=302";

    ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Policy&sProject=Policytra&sCodispl=VI7501_K&nHeight=270'+ lstrstring , 'Policytra', 8500, 7000, 'yes','yes', 0, 0,'yes')  
}
function insShowSaapv(){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
	insDefValues('ShowSaapv', 'ncod_saapv=' + tcncod_saapv.value + '&nInstitution=' + hddInstitution.value, '/VTimeNet/Policy/PolicySeq/')
	}
	
	
}

</SCRIPT>
<HTML> 
<HEAD>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl")))
Response.Write(mobjMenu.setZone(2, Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%>
    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="VI7500" ACTION = "valPolicySeq.aspx?nMainAction=301&amp;nHolder=1">

<%

'+Se definen las variables locales que nos permiten manejar campos a habilitar o deshabilitar segun su contenido
%>
	<TABLE>
	    <TR>
            <TD><LABEL ID=0>Folio</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcncod_saapv", 10, CStr(mobjsAapv.nCod_saapv),  , "Número de Folio",  ,  ,  ,  ,  , "insShowSaapv(this.value)", mobjsAapv.nType_saapv <> 34, CStr(Session("sCertype")) <> "1")%></TD>
            
            <TD><LABEL ID=14942>Tipo de SAAPV</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeType_saapv", "table5742", eFunctions.Values.eValuesType.clngComboType, CStr(mintType_Saapv),  ,  ,  ,  ,  ,  , True,  , "Tipo de SAAPV")%></TD>
            
            <TD><%=mobjValues.HiddenControl("hddInstitution", CStr(mintInstitution))%></TD>
            <TD><%=mobjValues.HiddenControl("hddNtype_ameapv", CStr(minNtype_ameapv))%></TD> 
            <TD><%=mobjValues.HiddenControl("hddDlimitdate", CStr(minDlimitdate))%></TD>
            <TD><%=mobjValues.HiddenControl("hddScertype2", minScertype2)%></TD>  
            <TD><%=mobjValues.HiddenControl("hddNBranch2", CStr(minNBranch2))%></TD>  
            <TD><%=mobjValues.HiddenControl("hddNProduct2", CStr(minNProduct2))%></TD>  
            <TD><%=mobjValues.HiddenControl("hddNpolicy2", CStr(minNpolicy2))%></TD>  
                       
         </TR>
        <TR>

            <TD><LABEL ID=14941>Fecha </LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdissue_dat", CStr(mobjsAapv.dissue_dat),  , "Fecha en que se crea la saapv",  ,  ,  ,  , True)%></TD>
            
	        <TD><LABEL ID=14942>Estado</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbestatus_saapv", "table5741", eFunctions.Values.eValuesType.clngComboType, CStr(mobjsAapv.nstatus_saapv),  ,  ,  ,  ,  ,  , True,  , "Estado")%></TD>
 
        </TR>
        <TR>    
            <TD ALIGN="CENTER">
                <LABEL ID=41434><A HREF="JAVASCRIPT:ShowSubSequence()">Secuencia interna de SAAPV</A></LABEL>
                &nbsp;
                <%=mobjValues.AnimatedButtonControl("btnSequence", "/VTimeNet/Images/clfolder.png", "Subsequencia de saapv",  , "ShowSubSequence()")%>
            </TD>
        </TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
'Response.Write"<script>alert(""" &  minNpolicy2  & """)</script>"
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>





