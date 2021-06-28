<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'-	Objeto para el manejo de las funciones asociadas a la grilla

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de la tabla temporal
Dim mobjField As eInterface.Field
Dim mobjMastersheet As eInterface.MasterSheet


</script>
	<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("GI1403")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "GI1403"

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjField = New eInterface.Field

mobjValues.ActionQuery = Session("bQuery")
mobjMastersheet = New eInterface.MasterSheet
Call mobjMastersheet.Find(mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdDouble))
%>
<HTML>
<HEAD>
<SCRIPT>
//% ControlNextBack: Se encarga de amumentar o disminuir la consulta de los registros
//-------------------------------------------------------------------------------------------
function ControlNextBack(Option){
//-------------------------------------------------------------------------------------------
    var lstrURL = self.document.location.href
    var llngRow = lstrURL.substr(lstrURL.indexOf("&nRow=") + 6)
    lstrURL = lstrURL.replace(/&nRow=.*/,'')
	switch(Option){
		case "Next":
			if(isNaN(llngRow))
				lstrURL = lstrURL + "&nRow=51"
			else{
				llngRow = insConvertNumber(llngRow) + 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
			break;

		case "Back":
			if(!isNaN(llngRow)){
				llngRow = insConvertNumber(llngRow) - 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
	}
	self.document.location.href = lstrURL;
}
</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "GI1403", "Datos E/S del proceso"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="Datos E/S del proceso" ACTION="ValInterfaceSeq.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("GI1403", Request.QueryString.Item("sWindowDescript")))%>
</FORM>
</BODY>
</HTML>
	<%If mobjMastersheet.sHeader = "1" Then%>
    <TABLE WIDTH="100%">
        <TR>
			<TD CLASS="HighLighted"><LABEL ID=0><A NAME="Encabezado"><%= GetLocalResourceObject("AnchorEncabezadoCaption") %></A></LABEL></TD>
		</TR>
		<TR>
			<TD CLASS="HorLine"></TD>
		</TR>
    </TABLE>
	<%=mobjField.MakeGI1403(Session("sKey"), Session("nSheet"), mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdLong), 4)%>
		<TABLE WIDTH="100%">
		    <TR>
				<TD CLASS="HighLighted"><LABEL ID=0><A NAME="Encabezado"><%= GetLocalResourceObject("cmdBack2Caption") %></A></LABEL></TD>
			</TR>
			<TR>
				<TD CLASS="HorLine"></TD>
			</TR>
		</TABLE>

	<%End If%>
<%=mobjField.MakeGI1403(Session("sKey"), Session("nSheet"), mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdLong), 2)%>
<%=mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow")))%>
<%=mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')")%>

	<%If mobjMastersheet.sTotal = "1" Then%>
    <TABLE WIDTH="100%">
        <TR>
			<TD CLASS="HighLighted"><LABEL ID=0><A NAME="Totales"><%= GetLocalResourceObject("AnchorTotalesCaption") %></A></LABEL></TD>
		</TR>
		<TR>
			<TD CLASS="HorLine"></TD>
		</TR>
    </TABLE>
	<%=mobjField.MakeGI1403(Session("sKey"), Session("nSheet"), mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdLong), 5)%>
	<%End If%>
	
    <%If Session("nContent") = 0 Then
	Session("nContent") = 1
	Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
	If CStr(Session("nContent")) = "1" Then
		Response.Write("<SCRIPT>top.frames[""fraHeader""].nContent= 1; </SCRIPT>")
	End If
	
End If
mobjValues = Nothing
mobjField = Nothing
mobjMastersheet = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("GI1403")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




