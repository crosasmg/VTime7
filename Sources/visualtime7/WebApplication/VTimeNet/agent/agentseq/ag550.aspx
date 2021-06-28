<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim llngAction As Object


'% InsPreAG550: Carga de valores iniciales de la ventana - ACM - 13/05/2002
'---------------------------------------------------------------------------------------------------
Private Sub InsPreAG550()
	'---------------------------------------------------------------------------------------------------
	Dim lclsIntermed_partic As eAgent.Intermed_partic
	Dim lintSuperin_num As Object
	Dim ldtmSuperin_num As Object
	Dim lintWarran_pol As Object
	Dim lblnNotBroker As Object
	Dim llngIntermedia As String
	Dim lblnIntermedia As Boolean
	
	lclsIntermed_partic = New eAgent.Intermed_partic
	
	Response.Write(mobjValues.ShowWindowsName("AG550"))
	
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 306 And CStr(Session("nLastIntermediary")) <> vbNullString Then
		lblnIntermedia = lclsIntermed_partic.InsPreAG550(mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble))
		If Not lblnIntermedia Then
			lblnIntermedia = lclsIntermed_partic.InsPreAG550(mobjValues.StringToType(Session("nLastIntermediary"), eFunctions.Values.eTypeData.etdDouble))
		End If
		llngIntermedia = Session("nLastIntermediary")
	Else
		llngIntermedia = Session("nIntermed")
		lblnIntermedia = lclsIntermed_partic.InsPreAG550(mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble))
	End If
	
	With lclsIntermed_partic
		ldtmSuperin_num = .dSuperin_num
		lblnNotBroker = .blnNotBroker
		lintSuperin_num = .nSuperin_num
		lintWarran_pol = .nWarran_pol
	End With
	
	mobjValues.ActionQuery = llngAction = eFunctions.Menues.TypeActions.clngactionquery
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">    " & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""5"">&nbsp;</TD></TR>" & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=40045><A NAME=""Corredores"">" & GetLocalResourceObject("AnchorCorredoresCaption") & "</A></LABEL></TD></TR>        " & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""Horline""></TD>        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>		" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("             <TD WIDTH=""15%"">&nbsp;</TD>		" & vbCrLf)
Response.Write("             <TD><LABEL ID=0>" & GetLocalResourceObject("tcnSuperin_numCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("             <TD>")


Response.Write(mobjValues.NumericControl("tcnSuperin_num", 10, lintSuperin_num,  , GetLocalResourceObject("tcnSuperin_numToolTip"),  ,  ,  ,  ,  ,  , lblnNotBroker))


Response.Write("</TD>" & vbCrLf)
Response.Write("             <TD WIDTH=""15%"">&nbsp;</TD>		" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""15%"">&nbsp;</TD>		" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcdSuperin_numCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdSuperin_num", ldtmSuperin_num,  , GetLocalResourceObject("tcdSuperin_numToolTip"),  ,  ,  ,  , lblnNotBroker))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""15%"">&nbsp;</TD>		" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("             <TD WIDTH=""15%"">&nbsp;</TD>		" & vbCrLf)
Response.Write("             <TD><LABEL ID=0>" & GetLocalResourceObject("tcnWarran_polCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("             <TD>")


Response.Write(mobjValues.NumericControl("tcnWarran_pol", 10, lintWarran_pol,  , GetLocalResourceObject("tcnWarran_polToolTip"),  ,  ,  ,  ,  ,  , lblnNotBroker))


Response.Write("</TD>" & vbCrLf)
Response.Write("             <TD WIDTH=""15%"">&nbsp;</TD>" & vbCrLf)
Response.Write("             ")


Response.Write(mobjValues.HiddenControl("blnNotBroker", lblnNotBroker))


Response.Write("" & vbCrLf)
Response.Write("        </TR>		" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("    ")

	
	lclsIntermed_partic = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
llngAction = Request.QueryString.Item("nMainAction")
%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<HTML>
    <HEAD>
        <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 13.22 $"        
</SCRIPT>    





        <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("AG550"))
	.Write(mobjMenu.setZone(2, "AG550", "AG550.aspx"))
End With
mobjMenu = Nothing
%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmAG550" ACTION="valAgentSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
        	<%Call InsPreAG550()%>
        </FORM>
    </BODY>
</HTML>

<%
mobjValues = Nothing
%>





