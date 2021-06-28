<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eCoReinsuran" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As Object

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

Dim mobjContrmaster As eCoReinsuran.Contrmaster
Dim mobjCon_netret As Object

'    On Error GoTo insValCR769_Err



'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


</script>
<%Response.Expires = -1

mobjContrmaster = New eCoReinsuran.Contrmaster
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
'UPGRADE_NOTE: The 'eCoreinsuran.Con_netret' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
mobjCon_netret = Server.CreateObject("eCoreinsuran.Con_netret")

mobjValues.sCodisplPage = "CR769"

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CR769", "CR769.aspx"))
	Response.Write(mobjValues.ShowWindowsName("CR769"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

'Response.Write "<NOTSCRIPT>alert(""" & Request.QueryString & """);</script>"


If mobjContrmaster.Find(CInt("2"), CInt(Request.QueryString.Item("nNumber")), CInt(Request.QueryString.Item("nType")), CInt(Request.QueryString.Item("nBranch_rei")), Today) Then
	
	Call mobjCon_netret.Inscalcr769(Request.QueryString.Item("sCodispl"), Session("nUsercode"), Today, mobjContrmaster.dExpirdat, mobjContrmaster.dStartdate, Request.QueryString.Item("nNumber"), Request.QueryString.Item("nType"), Request.QueryString.Item("nBranch_rei"), Request.QueryString.Item("nExec"))
	
End If

%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CR769" ACTION="valCoReinsuranTra.aspx?sMode=2">
 <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("dIniDateCaption") %></LABEL>
             <%=mobjValues.DateControl("dIniDate", CStr(mobjContrmaster.dStartdate),  , GetLocalResourceObject("dIniDateToolTip"), False,  ,  ,  , True)%></TD>
             <TD><LABEL><%= GetLocalResourceObject("dEndDateCaption") %></LABEL>
             <%=mobjValues.DateControl("dEndDate", CStr(mobjContrmaster.dExpirdat),  , GetLocalResourceObject("dEndDateToolTip"), False,  ,  ,  , True)%></TD>
        </TR>      
        <TR>
            <TD>&nbsp;</TD>
        </TR>      
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnCessprfixCaption") %> </LABEL>
            <%=mobjValues.NumericControl("tcnCessprfix", 18, mobjCon_netret.nAmountnrifv,  , GetLocalResourceObject("tcnCessprfixToolTip"), True, 6)%></TD>
            <TD>&nbsp;</TD>
        </TR>      
        <TR>
            <TD>&nbsp;</TD>
        </TR>      
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("AnchorCaption") %> </LABEL>
			 <%=mobjValues.CheckControl("chkprint", "", CStr(2), "1",  , False)%></TD>
		    <TD>&nbsp;</TD>
        </TR>
     </TABLE>   
<SCRIPT LANGUAGE="JavaScript">
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 3 $|$$Date: 5/07/06 22:35 $" 
</SCRIPT>
</FORM> 
</BODY>
</HTML>





