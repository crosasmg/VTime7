<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As Object
Dim mobjMenu As eFunctions.Menues


'---------------------------------------------------------------------------
Private Sub insPreOPC001_K()
	'---------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("valCashNumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("valCashNum", "tabusercashnum", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCashNumToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("tcdCollectCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdCollect",  ,  , GetLocalResourceObject("tcdCollectToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeStatusCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.ComboControl("cbeStatus", "1|Completa,2|Incompleta,3|Anulada",  ,  ,  , GetLocalResourceObject("cbeStatusToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "OPC824_K"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


        
<SCRIPT>
//%Variable para el control de Versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 11/02/04 17:25 $"
</SCRIPT>            
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false
        document.images["btn_tcdCollect"].disabled = false
        document.images["btnvalCashNum"].disabled = false
}
//------------------------------------------------------------------------------------------
function  insDisabledControl(){
//------------------------------------------------------------------------------------------
	document.forms[0].cbeStatus.disabled = true
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("OPC824"))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "OPC824.aspx"))
	.Write(mobjMenu.MakeMenu("OPC824", "OPC824_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmQIncomCash" ACTION="ValCashBank.aspx?Zone=1">
<BR></BR>
<%
Call insPreOPC001_K()
%>
<SCRIPT>insDisabledControl();</SCRIPT>
</FORM>
</BODY>
</HTML>

<%mobjValues = Nothing
%>





