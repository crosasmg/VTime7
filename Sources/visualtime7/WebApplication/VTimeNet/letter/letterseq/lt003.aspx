<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjLettRequest As eLetter.LettRequest
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout



'**********************************************************************************************************
'*************************************** FUNCIONES VBScript ***********************************************
'**********************************************************************************************************

'% insDefineFields : load the grid columns in the start of transaction
'------------------------------------------------------------------------
Private Function insDefineFields() As Object
	'------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=45% CLASS = ""HIGHLIGHTED"" COLSPAN = 2><LABEL ID=7322>Tipo de solicitud</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=10%>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=10%>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=45% CLASS = ""HIGHLIGHTED""><LABEL ID=7323>Tipo de envío</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD CLASS = ""HORLINE"" COLSPAN = 2></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=15%></TD>" & vbCrLf)
Response.Write("			<TD CLASS = ""HORLINE"" COLSPAN = 2></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN = ""3"">" & vbCrLf)
Response.Write("				<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("					<TR ROWSPAN=""3"">" & vbCrLf)
Response.Write("						<TD COLSPAN = ""4"">" & vbCrLf)
Response.Write("							")

	
	With mobjValues
		Response.Write(mobjValues.OptionControl(7324, "optTypeReq","Individual", CStr(mobjLettRequest.DefaultValuesLT003("optIndividual")), CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  ,"La solicitud es de una sola carta"))
		Response.Write(mobjValues.OptionControl(7325, "optTypeReq","Masivo", CStr(mobjLettRequest.DefaultValuesLT003("optMasive")), CStr(2),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  ,"La solicitud es de más de una carta"))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("						</TD>" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("				</TABLE>" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN = ""2"">" & vbCrLf)
Response.Write("				<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("					<TR ROWSPAN=""3"">" & vbCrLf)
Response.Write("						<TD>")

	Response.Write(mobjValues.CheckControl("chkSendEmail","E-Mail", CStr(mobjLettRequest.DefaultValuesLT003("chkEMail")), CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  ,"Indica que la correspondencia se enviará vía e-mail"))
Response.Write("</TD>" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("					<TR ROWSPAN=""3"">" & vbCrLf)
Response.Write("						<TD>")

	Response.Write(mobjValues.CheckControl("chkSendMail","Correo", CStr(mobjLettRequest.DefaultValuesLT003("chkMail")), CStr(2),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  ,"Indica que la correspondencia se enviará vía correo"))
Response.Write("</TD>" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("					<TR ROWSPAN=""3"">" & vbCrLf)
Response.Write("						<TD>")

	Response.Write(mobjValues.CheckControl("chkSendFax","Fax", CStr(mobjLettRequest.DefaultValuesLT003("chkFax")), CStr(4),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  ,"Indica que la correspondencia se enviará vía fax"))
Response.Write("</TD>" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("				</TABLE>" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("	<TABLE>" & vbCrLf)
Response.Write("		<TR>&nbsp;" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	    <TR>&nbsp;" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=7316>Fecha máxima de permanencia</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>" & vbCrLf)
Response.Write("				")

	Response.Write(mobjValues.DateControl("tcdExpDate", CStr(mobjLettRequest.dExpDate),  ,"Fecha máxima de permanencia de las cartas que se generen de la solicitud.",  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=7317>Fecha de impresión</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>" & vbCrLf)
Response.Write("				")

	'Response.Write mobjValues.DateControl("tcdPrintDate",mobjLettRequest.dPrintDate ,,"Fecha de impresión de la correspondencia",,,,,true)
	Response.Write(mobjValues.DateControl("tcdPrintDate",  CStr(mobjLettRequest.dPrintDate),  ,"Fecha de impresión de la correspondencia",  ,  ,  ,  , True))
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=7318>Solicitante</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=2>")

	If mobjValues.ActionQuery Then
		Response.Write(mobjValues.PossiblesValues("valUser", "tabUsers", 2, CStr(mobjLettRequest.nUser_sol),  ,  ,  ,  ,  ,  , True, 4,"Código del usuario que realiza la solicitud de envío."))
		'				    Response.Write mobjValues.TextControl("tctClient",,,"Solicitante","",Request.QueryString("nMainAction")=401,"Cliename",,false,,,,,true)
	Else
		Response.Write(mobjValues.PossiblesValues("valuser", "tabUsers", 2, Session("nUsercode"),  ,  ,  ,  ,  ,  , True, 4,"Código del usuario que realiza la solicitud de envío"))
	End If
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN = ""2"">" & vbCrLf)
Response.Write("				")

	Response.Write(mobjValues.CheckControl("chkPrint","Imprimir", Session("sPrint") ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  ,"Indica que la correspondencia será impresa inmediatamente"))
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>    ")

	
End Function
'**% SelAttach: This function shows the window that contains the attacment document
'% SelAttach: Esta función muestra la ventana que contiene el documento adjunto
'------------------------------------------------------------------------------------------		
Public Function InsExecute() As Object
	'------------------------------------------------------------------------------------------		
	
	
Response.Write("")


Response.Write("<SCRIPT LANGUAGE=""JavaScript"">")


Response.Write("" & vbCrLf)
Response.Write("     ShowPopUp('/VTimeNet/Common/GoTo.aspx?sPopUp=1&sOriginalForm=LTL002&sCodispl=LTL002','LTL002', window.screen.availWidth, window.screen.availHeight, 'no','no',0,0,true,false)" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
End Function

</script>
<%

Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("LT003")
'~End Header Block VisualTimer Utility

'+ Se instancian los objetos necesarios para trabajr las particularidades de creación de la forma por rutinas genéricas

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "LT003"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
mobjMenu.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility
mobjLettRequest = New eLetter.LettRequest

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
%> 

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js">


</SCRIPT>
<%="<SCRIPT LANGUAGE=""JavaScript"">"%>
    var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;
    top.fraHeader.document.forms[0].tcnLettRequest.value='<%=Session("nLettRequest")%>'
</SCRIPT>
<HTML>
	<HEAD>
		<META NAME		 = "GENERATOR" Content="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/Constantes.js">		</SCRIPT>
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js">	</SCRIPT>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

		<%=mobjValues.StyleSheet()%>
    		<%=mobjValues.ShowWindowsName("LT003", Request.QueryString.Item("sWindowDescript"))%>
	</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "LT003", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End If

mobjMenu = Nothing
%>
		<FORM METHOD="POST" ACTION="valLetterSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>" id=form1 name=form1>
		<%

mobjLettRequest.insPreLT003(Session("nLettRequest"))
insDefineFields()

mobjValues = Nothing
mobjLettRequest = Nothing
%>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Call mobjNetFrameWork.FinishPage("LT003")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








