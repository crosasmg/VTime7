<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de los datos de la ventana
Dim mclsFire_risk As eClaim.Fire_risk


</script>
<%Response.Expires = -1

mclsFire_risk = New eClaim.Fire_risk
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "os592_2"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
        document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $"
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "OS592_2", "OS592_2.aspx"))
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="OS592_2" ACTION="valProf_ordseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName("OS592_2"))

Call mclsFire_risk.Find(Session("nServ_order"))
If mclsFire_risk.nElecStat = eRemoteDB.Constants.intNull Then
	mclsFire_risk.nElecStat = 1
End If
%>
    <TABLE WIDTH="100%">
		<TR>            
		    <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><A NAME="Clave"><%= GetLocalResourceObject("AnchorClaveCaption") %></A></LABEL></TD>
		</TR>
		<TR>
		    <TD COLSPAN="5" CLASS="Horline"></TD>	    
		</TR>  
        <TR>
			<TD WIDTH=25%><LABEL ID=0><%= GetLocalResourceObject("cbeTypeCaption") %></LABEL></TD>
			<TD WIDTH=30%><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeType", "Table5595", eFunctions.Values.eValuesType.clngComboType, CStr(mclsFire_risk.nElecProt),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypeToolTip")))
%>
			</TD>
			<TD WIDTH=10%>&nbsp;</TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="3"></TD>
			<TD COLSPAN="3" CLASS="HorLine"></TD>
		</TR>
		<TR>
			<TD COLSPAN="3">&nbsp;</TD>
            <TD><%=mobjValues.OptionControl(0, "optSta_local", GetLocalResourceObject("optSta_local_1Caption"), CStr(mclsFire_risk.nElecStat), "1",  ,  ,  , GetLocalResourceObject("optSta_local_1ToolTip"))%></TD>
		</TR>
		<TR>
			<TD COLSPAN="3">&nbsp;</TD>
            <TD><%=mobjValues.OptionControl(0, "optSta_local", GetLocalResourceObject("optSta_local_2Caption"), CStr(3 - mclsFire_risk.nElecStat), "2",  ,  ,  , GetLocalResourceObject("optSta_local_2ToolTip"))%></TD>
		</TR>
		<TR>
			<TD COLSPAN="3">&nbsp;</TD>
            <TD><%=mobjValues.OptionControl(0, "optSta_local", GetLocalResourceObject("optSta_local_3Caption"), CStr(4 - mclsFire_risk.nElecStat), "3",  ,  ,  , GetLocalResourceObject("optSta_local_3ToolTip"))%></TD>
		</TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%
mobjMenu = Nothing
mobjValues = Nothing
mclsFire_risk = Nothing
%>





