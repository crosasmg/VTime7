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
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcdDate_iniCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdDate_ini", CStr(Today), True, GetLocalResourceObject("tcdDate_iniToolTip"),  ,  ,  , "self.document.forms[0].tcdDate_end.value = self.document.forms[0].tcdDate_ini.value", True, 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcdDate_endCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdDate_end", CStr(Today), True, GetLocalResourceObject("tcdDate_endToolTip"),  ,  ,  ,  , True, 2))


Response.Write("</TD>            " & vbCrLf)
Response.Write("        </TR>    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnCashnumCaption") & "</LABEL></TD>        " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnCashnum", 5, "", True, GetLocalResourceObject("tcnCashnumToolTip"),  , 0,  ,  ,  ,  , True, 3))


Response.Write("</TD>        " & vbCrLf)
Response.Write("            <TD><LABEL ID=8842>" & GetLocalResourceObject("cbeOfficeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOfficeToolTip"),  , 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8837>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8841>" & GetLocalResourceObject("cbeMovTypeCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeMovType", "TabMove_type", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeMovTypeToolTip"),  , 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>        " & vbCrLf)
Response.Write("            <TD><LABEL ID=8841>" & GetLocalResourceObject("cbeConceptCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            ")

	mobjValues.TypeList = 1
	mobjValues.List = "39,45"
Response.Write("" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeConcept", "Table22", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeConceptToolTip"),  , 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("")

	
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "OPC001_K"
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
        document.images["btn_tcdDate_ini"].disabled = false
        document.forms[0].tcdDate_end.disabled = false
        document.images["btn_tcdDate_end"].disabled = false
        document.forms[0].cbeConcept.disabled = false
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("OPC001"))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "OPC001.aspx"))
	.Write(mobjMenu.MakeMenu("OPC001", "OPC001_k.aspx", 1, ""))
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
</FORM>
</BODY>
</HTML>

<%mobjValues = Nothing
%>





