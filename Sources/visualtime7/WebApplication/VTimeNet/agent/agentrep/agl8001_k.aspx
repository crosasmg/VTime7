<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values

Dim mstrYear As Object
Dim mstrMonth As Object



Private Sub insPreAGL8001()
	Dim oCtrolDate As eGeneral.Ctrol_date
	Dim dCurrProcessDate As Date
	oCtrolDate = New eGeneral.Ctrol_date
	If oCtrolDate.Find(90) Then
		dCurrProcessDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, oCtrolDate.dEffecdate)
	Else
		dCurrProcessDate = Today
	End If
	mstrYear = Year(dCurrProcessDate)
	mstrMonth = Month(dCurrProcessDate)
	Response.Write("<SCRIPT>")
	Response.Write("sDefinitiveYear='" & mstrYear & "';")
	Response.Write(" sDefinitiveMonth='" & mstrMonth & "';")
	Response.Write("</" & "Script>")
	
	oCtrolDate = Nothing
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>


<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">

var sDefinitiveYear; 
var sDefinitiveMonth;

//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------
	return true;
}   
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}

function insStateZone(){
//--------------------------------------------------------------------------------------------------

}

function HandleFields(sPreliminary)
//--------------------------------------------------------------------------------------------------
{
    var bDisabled = (sPreliminary!='1');

    with (document.forms[0])
    {
        tcnYear.value = sDefinitiveYear;
        cboMonth.value = sDefinitiveMonth;
    
        tcnYear.disabled = bDisabled;
        cboMonth.disabled = bDisabled;
    }
    
}

</SCRIPT>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("AGL8001", "AGL8001_K.aspx", 1, ""))
mobjMenu = Nothing

Call insPreAGL8001()

%>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAGL8001" ACTION="valAgentRep.aspx?X=1">
    <BR><BR>
    <TABLE WIDTH="100%" border="0">
        <TR>
        <TR>
            <TD COLSPAN="1" CLASS="HighLighted"><LABEL ID=0><A NAME="Intermediarios"><%= GetLocalResourceObject("AnchorIntermediariosCaption") %></A></LABEL></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=10><A NAME="Parametros"><%= GetLocalResourceObject("AnchorParametrosCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="1" CLASS="HORLine"></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="HORLine"></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(101108, "optPreliminary", GetLocalResourceObject("optPreliminary_1Caption"), "1", "1", "HandleFields(this.value);")%></TD>
            <TD></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnYear", 4, mstrYear,  , GetLocalResourceObject("tcnYearToolTip"),  , 0)%></TD>

        </TR>
            <TD><%=mobjValues.OptionControl(101109, "optPreliminary", GetLocalResourceObject("optPreliminary_2Caption"),  , "2", "HandleFields(this.value);")%></TD>
            <TD></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cboMonthCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cboMonth", "table7013", eFunctions.Values.eValuesType.clngComboType, mstrMonth,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboMonthToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>





