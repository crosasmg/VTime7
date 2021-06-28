<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim sCodispl As String
Dim sCodisplPage As String
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.14
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'%   insDefineHeader: Permite cargar los campos del encabezado
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
Response.Write("" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=41208>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				<TD> " & vbCrLf)
Response.Write("				")

	mobjValues.BlankPosition = False
	mobjValues.TypeList = 1
	mobjValues.List = "2"
	
Response.Write("" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  ,  , "",  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD> <LABEL ID=40011>" & GetLocalResourceObject("valProductCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("				")

	With mobjValues
		.Parameters.Add("nBranch", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	
Response.Write("" & vbCrLf)
Response.Write("				<TD> ")


Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  , False,  , "", eFunctions.Values.eTypeCode.eString, 4))


Response.Write("</TD>			" & vbCrLf)
Response.Write("			</TR>        " & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				<TD><LABEL>" & GetLocalResourceObject("tcdBeginDateCaption") & " </LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdBeginDate", CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, Today)),  , GetLocalResourceObject("tcdBeginDateToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD><LABEL>" & GetLocalResourceObject("tcdEndDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdEndDate", CStr(Today),  , GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("		     	</TR>   " & vbCrLf)
Response.Write("		</TABLE> " & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("        ")

End Sub

</script>
<%

sCodispl = Trim(Request.QueryString.Item("sCodispl"))
sCodisplPage = LCase(sCodispl) & "_k"

Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(sCodisplPage)

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
mobjValues.sCodisplPage = sCodisplPage
'~End Body Block VisualTimer Utility

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>

<HTML>
<HEAD>
<SCRIPT>


//%   insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%   insStateZone: Se controla el estado de los campos de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    return true;
}


</SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu(sCodispl, sCodispl & "_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM method="post" id="FORM" name="Policy" action="valPolicyRep.aspx?mode=1">
	<BR><BR>
<%
Call insDefineHeader()

mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.14
Call mobjNetFrameWork.FinishPage(sCodisplPage)
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





