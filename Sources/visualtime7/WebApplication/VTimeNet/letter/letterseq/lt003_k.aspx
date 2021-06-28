<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

Const CN_LETTREQUEST As Short = 77

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout



'**********************************************************************************************************
'*************************************** FUNCIONES VBScript ***********************************************
'**********************************************************************************************************

'%insPreLT003_K: carga los valores de la página inicial de la secuencia
'--------------------------------------------------------------------------------------------
Private Sub insPreLT003_K()
	'--------------------------------------------------------------------------------------------    
	
	'+  Se limpia la variable de sesión del cliente para evitar que esta contenga data falsa
	
	Session("sClient") = vbNullString
	
	'+ Se cargan los valores que vienen de la base de datos	
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=7329>Solicitud</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnLettRequest", 5, "",  ,"Número de la solicitud de envío.", False, 0,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=15%>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=7330>Fecha de solicitud</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'

Response.Write(mobjValues.DateControl("tcdEffecdate", mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate),  ,"Fecha en que se está realizando la solicitud de envío.",  ,  ,  , "setParameters()", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=7331>Modelo de Carta</LABEL></TD>" & vbCrLf)
Response.Write("			<TD colspan=2>" & vbCrLf)
Response.Write("				")

	
	'UPGRADE_WARNING: Use of Null/IsNull() detected. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
	mobjValues.Parameters.Add("dEffecdate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("tcnLetterNum", "tabTab_Letters1", eFunctions.Values.eValuesType.clngWindowType,  , True, False,  ,  ,  ,  , True, 5,"Código del modelo de carta a utilizar para el envío de la correspondencia."))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
End Sub

</script>
<%

Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("LT003_k")
'~End Header Block VisualTimer Utility

'+ Se crean los objetos propios para el manejo de la página

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "LT003_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
mobjMenu.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

'+ Se realiza el manejo inicial de la página

Response.Write(mobjValues.StyleSheet())
%>

<HTML>
	<HEAD>
<SCRIPT>
function setParameters()
{
	self.document.forms[0].tcnLetterNum.Parameters.Param1.sValue = self.document.forms[0].tcdEffecdate.value;
}
</SCRIPT>
	
		<META NAME = "GENERATOR" Content="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/Constantes.js">		</SCRIPT>
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js">	</SCRIPT>
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/tmenu.js">			</SCRIPT>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

		<%
Response.Write(mobjMenu.MakeMenu("LT003_K", "LT003_K", 1, ""))
mobjMenu = Nothing
%>
	</HEAD>
	<BODY>
		<P>&nbsp;</P>
		<FORM Method = "Post" ACTION = "valLetterSeq.aspx?Parameter=1" id=form1 name=form1>
			<%
insPreLT003_K()
mobjValues = Nothing
%>
		</FORM>
	</BODY>
</HTML>

<%
'**********************************************************************************************************
'************************************** FIN FUNCIONES VBScript ********************************************
'**********************************************************************************************************
%>

<SCRIPT>

//*********************************************************************************************************
//******************************** FUNCIONES JavaScript ***************************************************
//*********************************************************************************************************

//%insStateZone: se encarga de habilitar los controles cuando se selecciona una acción
//-----------------------------------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------------------------------
	with (self.document.forms[0]) {
		
		if (top.fraSequence.plngMainAction==401){
			tcnLettRequest.value = ""
			tcnLetterNum.value = ""
			tcnLettRequest.disabled = false
			tcnLetterNum.disabled = false
			tcdEffecdate.disabled   = true
			self.document.images["btn_tcdEffecdate"].disabled = true
			self.document.images["btntcnLetterNum"].disabled = false
			}
		else{
			if (top.fraSequence.plngMainAction==301){
				tcnLettRequest.value = ""
				tcnLetterNum.value = ""
				tcnLetterNum.disabled = false
				tcnLettRequest.disabled = true
				tcdEffecdate.disabled   = true
				self.document.images["btntcnLetterNum"].disabled = false	
				}
			}
	}
}

//%insFinish: se activa al finalizar las acciones de la secuencia
//------------------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------------------
   return true;
}

//%insCancel: se activa al cancelar las acciones de la secuencia
//------------------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------------------
	ShowPopUp("/VTimeNet/Letter/LetterSeq/ShowDefValues.aspx?Field=Cancel" + "&nMainAction=" + top.fraSequence.plngMainAction , "ShowDefValuesLetter", 1, 1,"no","no",2000,2000);
}

//*********************************************************************************************************
//******************************** FIN FUNCIONES JavaScript ***********************************************
//*********************************************************************************************************
	setParameters();
</SCRIPT>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Call mobjNetFrameWork.FinishPage("LT003_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








