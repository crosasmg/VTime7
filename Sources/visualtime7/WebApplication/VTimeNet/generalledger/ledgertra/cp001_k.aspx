<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CP001_K"

%>

<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}

//% insStateZone: Se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------

	if(top.fraSequence.plngMainAction==301)
	{
		with(self.document.forms[0])
		{
			lstrAction = self.document.location.href
			lstraux = lstrAction
			lstrAction = lstrAction.replace(/\?.*/, '') + '?sCodispl=CP001' + '&nMainAction=' + top.fraSequence.plngMainAction
			self.document.location.href=lstrAction;
		}        
		self.document.forms[0].valLedCompan.disabled=false;
	}
	else
	{
		self.document.forms[0].valLedCompan.disabled=false;
		self.document.forms[0].btnvalLedCompan.disabled=false;
	}
}

//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $" 

</SCRIPT>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>

<%With Response
	.Write(mobjValues.StyleSheet)
	.Write(mobjMenu.MakeMenu("CP001", "CP001_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmInsLedCompan" ACTION="ValLedGerTra.aspx?sTime=1">
	<BR><BR>
	<TABLE WIDTH="100%">
		<TR>
			<TD WIDTH="25%">&nbsp</TD>
            <TD WIDTH="15%"><LABEL><%= GetLocalResourceObject("valLedCompanCaption") %></LABEL></TD>
            <%
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 301 Then
	%>
						<TD>
							<%=mobjValues.PossiblesValues("valLedCompan", "tabled_compan", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valLedCompanToolTip"),  ,  ,  , False)%>
						</TD>
			<%	
Else
	%>
						<TD>
							<%=mobjValues.PossiblesValues("valLedCompan", "tabled_compan", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , False, 4, GetLocalResourceObject("valLedCompanToolTip"),  ,  ,  , True)%>
						</TD>
				
					<%	Response.Write("<SCRIPT>self.document.forms[0].elements['btnvalLedCompan'].disabled=true;</SCRIPT>")
End If
%>
		</TR>
	</TABLE>		
</BODY>
</FORM>
</HTML>





