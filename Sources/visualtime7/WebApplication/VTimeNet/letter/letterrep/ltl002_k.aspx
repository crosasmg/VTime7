<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**-Objetive: Object for the handling of LOG
'-Objetivo: Objeto para el manejo de LOG
Dim mobjNetFrameWork As eNetFrameWork.Layout

'**-Objetive: Object for the handling of the general functions of load of values
'-Objetivo: Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'**-Objetive: Object for the handling of the generics routines
'-Objetivo: Objeto para el manejo de las rutinas genéricas
Dim mobjMenues As eFunctions.Menues


'**% insPreLTL002: This function allows to load the fields of the header
'%   insPreLTL002: Permite cargar los campos del encabezado
'-----------------------------------------------------------------------------------------
Private Sub insPreLTL002()
	'-----------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    ")


Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))


Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<BR><BR><BR><BR>" & vbCrLf)
Response.Write("		<BR><BR><BR><BR>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""25%""></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""25%""><LABEL ID=15754>Fecha</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""25%"">")


'Response.Write(mobjValues.DateControl("tcdEffecdate",  ,  ,"Fecha en que se ejecuta el proceso",  ,  ,  , "insValueLTL002(1);", False, 1))
Response.Write(mobjValues.DateControl("tcdEffecdate",  ,  ,"Fecha en que se ejecuta el proceso",  ,  ,  , , False, 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""25%""><TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""25%""></TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=""25%""><LABEL ID=15756>Solicitud</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=""25%"">")


'Response.Write(mobjValues.NumericControl("tcnLettRequest", 5, "",  ,"Número de la solicitud de correspondencia",  ,  ,  ,  ,  , "insValueLTL002(2);", False))
Response.Write(mobjValues.NumericControl("tcnLettRequest", 5, "",  ,"Número de la solicitud de correspondencia",  ,  ,  ,  ,  , , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=""25%""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""25%""></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""25%""><LABEL ID=15757>Cliente</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=""25%"">")


'Response.Write(mobjValues.ClientControl("tctClient", "",  ,"Código del cliente destinatario de la carta ", "insValueLTL002(3);", False, "lblClieName"))
Response.Write(mobjValues.ClientControl("tctClient", "",  ,"Código del cliente destinatario de la carta ", , False, "lblClieName"))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=""25%""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("	</TABLE>")

End Sub

</script>
<%Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
<SCRIPT>
//**-Objetive: This line keeps the version coming from VSS
//-Objeto: Esta línea guarda la versión procedente de VSS
//----------------------------------------------------------------------------------------------------------------------
    document.VssVersion="$$Revision: 8 $|$$Date: 9/16/04 11:40a $$Author: Jramirez $"
//----------------------------------------------------------------------------------------------------------------------

//**% insCancel: This function is executed when the page is cancelled
//%   insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//**% insValueLTL002: Enable/Disable the windows field
//%   insValueLTL002: Habilita/desabilita los capos de la ventana
//------------------------------------------------------------------------------------------
function insValueLTL002(lintValue){
//------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		switch(lintValue)
		//alert(lintValue);
			{
			case 1:
				if (tcdEffecdate.value != "")
					{
					tcnLettRequest.value = "";
					tctClient.value = "";
					tcdEffecdate.disabled = false; 
					tcnLettRequest.disabled = false;
					tctClient.disabled = true;
					}
				else
					{
					tcdEffecdate.value = "";
					tcnLettRequest.value = "";
					tctClient.value = "";
					tcdEffecdate.disabled = false; 
					tcnLettRequest.disabled = false;
					tctClient.disabled = false;
					}
				break;
			case 2:
				if (tcnLettRequest.value != "")
					{
					tcdEffecdate.value = "";
					tcdEffecdate.disabled = true; 
					tcnLettRequest.disabled = false;
					tctClient.disabled = false;
					}
				else
					{
					tcdEffecdate.value = "";
					tcnLettRequest.value = "";
					tctClient.value = "";
					tcdEffecdate.disabled = false; 
					tcnLettRequest.disabled = false;
					tctClient.disabled = false;
					}
				break;
			case 3:
				tcdEffecdate.value = "";
				tcdEffecdate.disabled = true; 
				tcnLettRequest.disabled = false;
				tctClient.disabled = false;
				break;
			default:
				break;
		}
	}
}
</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->
    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
  <%
With Request
	mobjValues.ActionQuery = (.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
	mobjMenues = New eFunctions.Menues
	mobjMenues.sSessionID = Session.SessionID
	Response.Write(mobjMenues.MakeMenu(.QueryString.Item("sCodispl"), .QueryString.Item("sCodispl") & "_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	mobjMenues = Nothing
End With
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="<%=Request.QueryString.Item("sCodispl")%>" ACTION="valLetterReP.aspx?sZone=1">
<BR><BR>
<%
Call insPreLTL002()
mobjValues = Nothing
mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
%>

</FORM>
</BODY>
</HTML>









