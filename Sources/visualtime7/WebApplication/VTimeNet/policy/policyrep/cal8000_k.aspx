<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.47.59
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


Dim mstrYear As Object
Dim mstrMonth As Object


Private Sub insPreCOL010()
	mstrYear = Year(Today)
	mstrMonth = Month(Today)
	Response.Write("<SCRIPT>")
	Response.Write("sDefinitiveYear='" & mstrYear & "';")
	Response.Write(" sDefinitiveMonth='" & mstrMonth & "';")
	Response.Write("</" & "Script>")
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cal8000_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cal8000_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
	<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 2 $|$$Date: 9/10/09 3:55p $|$$Author: Gletelier $"
    </SCRIPT>


<HTML>
<HEAD>
<SCRIPT>

//% insStateZone: se manejan los campos de la página
//-----------------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------------
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("CAL80000", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.MakeMenu("CAL8000", "cal8000_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing
Call insPreCOL010()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAutoAnnulment" ACTION="valpolicyRep.aspx?mode=1">
<BR></BR>
    <%Response.Write(mobjValues.ShowWindowsName("CAL8000"))%>
    <TABLE WIDTH="100%" border="0">
        <TR>
            <TD></TD>
            <TD></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnYear", 4, mstrYear,  , GetLocalResourceObject("tcnYearToolTip"),  , 0)%></TD>

        </TR>
            <TD></TD>
            <TD></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cboMonthCaption") %></LABEL></TD>
            <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cboMonth", "table7013", eFunctions.Values.eValuesType.clngComboType, mstrMonth,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboMonthToolTip")))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59
Call mobjNetFrameWork.FinishPage("col009_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





