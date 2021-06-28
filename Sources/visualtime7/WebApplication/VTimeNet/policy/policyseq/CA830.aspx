<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.03
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores 
Dim mobjValues As New eFunctions.Values

Dim mobjMenu As eFunctions.Menues
Dim mclsCertificat As ePolicy.Certificat 
Dim mstrQueryString As String


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA830")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

'mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mclsCertificat  = New ePolicy.Certificat 
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%> 

<SCRIPT>

    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 4 $|$$Date: 12/11/03 18:06 $|$$Author: Nvaplat18 $"


</SCRIPT>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ACTION="valPolicySeq.aspx?Action=Add">
<%
Call mclsCertificat.insPreCA830(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) )

mobjValues.ActionQuery = Session("bQuery") 

%>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnCoverageCertificateCaption") %></LABEL></TD> 
            <TD><%=mobjValues.NumericControl("tcnCoverageCertificate",12,mclsCertificat.nCoverageCertificate,  , GetLocalResourceObject("tcnCoverageCertificateToolTip"))%></TD>  
        </TR>

        <TR>
            <TD><LABEL ID=0><%=GetLocalResourceObject("ValIntermedCaption") %></LABEL></TD> 
            <TD><%=mobjValues.PossiblesValues("ValIntermed", "tabintermedia", 2, mclsCertificat.nIntermed, , , , , , ,True,10)%></TD>  
        </TR>

        <TR>
            <TD><LABEL ID=0><%=GetLocalResourceObject("tctStatusCaption") %></LABEL></TD> 
            <TD><%=mobjValues.TextControl("tctStatus", 20, mclsCertificat.sStatusCoverageCertificate,,,,,,,True)%></TD>  
        </TR>

    </TABLE>
<%

mobjValues = Nothing
mclsCertificat = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
Call mobjNetFrameWork.FinishPage("CA830")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>




