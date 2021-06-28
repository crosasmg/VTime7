<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'-Variable que guarda el año y el mes de la fecha del día
Dim mintYear As Object
Dim mintMonth As Object


</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("val601_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "val601_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mintYear = DatePart(Microsoft.VisualBasic.DateInterval.Year, Today)
mintMonth = DatePart(Microsoft.VisualBasic.DateInterval.Month, Today)

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>

<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
document.VssVersion="$$Revision: 2 $|$$Date: 3/05/04 18:28 $|$$Author: Nvaplat2 $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
    return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//% insShowDefValues: Muestra los datos de verificación
//--------------------------------------------------------------------------------------------
function insShowDefValues(sKey, nCertif){
//--------------------------------------------------------------------------------------------
    var lstrQueryString
    if (typeof(nCertif)=='undefined') nCertif = 0;
    
    with (self.document.forms[0]){
        lstrQueryString = 'nBranch=' + cbeBranch.value;
        lstrQueryString = lstrQueryString + '&nProduct=' + valProduct.value;
        lstrQueryString = lstrQueryString + '&nPolicy=' + tcnPolicy.value;
        lstrQueryString = lstrQueryString + '&nCertif=' + nCertif;
        insDefValues(sKey, lstrQueryString, '/VTimeNet/Policy/PolicyRep');    
    }

}
//% insShowDiv: Muestra la parte de la página según el modo de ejecución
//--------------------------------------------------------------------------------------------
function insShowDiv(nYear, nMonth){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        cbeBranch.disabled = optEjecution[0].checked;
        tcnPolicy.disabled = optEjecution[0].checked;
        tcnCertif.disabled = optEjecution[0].checked;
        if (cbeBranch.disabled){
            valProduct.disabled = true;
            btnvalProduct.disabled = true;
            cbeBranch.value = '0';
            tcnPolicy.value = '';
            tcnCertif.value = '';
            valProduct.value = '';
            UpdateDiv('valProductDesc','');
            tcnYearP.value = '';
            cbeMonthP.value = '';
            dtcClient.value = '';
            UpdateDiv('lblCliename','');
            valIntermed.value = '';
            UpdateDiv('valIntermedDesc','');
        }
        else{
            tcnYear.value = nYear;
            cbeMonth.value = nMonth;
        }
        ShowDiv('divMassive', (optEjecution[0].checked?'show':'hide'));
        ShowDiv('divPuntual', (optEjecution[1].checked?'show':'hide'));
    }
}

//% insShowRep: Muestra la parte de la página según el modo de ejecución
//--------------------------------------------------------------------------------------------
function insShowRep(){
//--------------------------------------------------------------------------------------------
    self.document.forms[0].chkRep.disabled = self.document.forms[0].optType[0].checked;
    if  (self.document.forms[0].optType[0].checked){
       self.document.forms[0].chkRep.checked = self.document.forms[0].optType[0].checked;
    }
}

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("VAL601", "VAL601_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VAL601" ACTION="valPolicyRep.aspx?sMode=2">
<BR><BR>
<TABLE WIDTH="100%">
    <TR><TD COLSPAN=2 CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        <TD COLSPAN="53"></TD>
        <TD COLSPAN=2 CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        <TD COLSPAN="53"></TD>
    </TR>
    <TR>
        <TD COLSPAN="2" CLASS="HorLine"></TD>
        <TD COLSPAN="53"></TD>
        <TD COLSPAN="2" CLASS="HorLine"></TD>
    </TR>
    <TR> <TD><%=mobjValues.OptionControl(0, "optEjecution", GetLocalResourceObject("optEjecution_1Caption"), "1", "1", "insShowDiv(" & mintYear & "," & mintMonth & ")")%></TD>
		 <TD COLSPAN="55">&nbsp</TD>
         <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_2Caption"), "1", "2", "insShowRep()")%></TD>
	</TR>        
	<TR>
        <TD><%=mobjValues.OptionControl(0, "optEjecution", GetLocalResourceObject("optEjecution_2Caption"), "2", "2", "insShowDiv(" & mintYear & "," & mintMonth & ")")%></TD>
        <TD COLSPAN="55">&nbsp</TD>
        <TD><%=mobjValues.OptionControl(1, "optType", GetLocalResourceObject("optType_1Caption"), "2", "1", "insShowRep()")%></TD>
        
        <TD COLSPAN="1"><%=mobjValues.CheckControl("chkRep", GetLocalResourceObject("chkRepCaption"), "1", "1",  , CBool("1"), 13, GetLocalResourceObject("chkRepToolTip"))%></TD> 
		<TD COLSPAN="1">&nbsp;</TD> 
		
    </TR>

</TABLE>

<BR>
<TABLE WIDTH="100%">
    <TD><DIV ID ="divMassive">
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="Horline"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnYear", 4, mintYear,  , GetLocalResourceObject("tcnYearToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeMonthCaption") %></LABEL></TD>
            <TD>
            <%
mobjValues.TypeOrder = 1
Response.Write(mobjValues.PossiblesValues("cbeMonth", "table7013", eFunctions.Values.eValuesType.clngComboType, mintMonth,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeMonthToolTip")))
%>
            </TD>
        </TR>
    </DIV></TD>
    </TABLE>
    <TD><DIV ID ="divPuntual">
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="Horline"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, "",  , GetLocalResourceObject("tcnPolicyToolTip"), False,  ,  ,  ,  , "insShowDefValues('GetPolicyData');")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 10, "",  , GetLocalResourceObject("tcnCertifToolTip"), False,  ,  ,  ,  , "insShowDefValues('GetCertifData', this.value);")%></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor5Caption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="Horline"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("dtcClientCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.ClientControl("dtcClient", "",  , GetLocalResourceObject("dtcClientToolTip"),  , True, "lblCliename")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valIntermedCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.PossiblesValues("valIntermed", "tabintermedia_o", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valIntermedToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnYearP", 4, "",  , GetLocalResourceObject("tcnYearPToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeMonthCaption") %></LABEL></TD>
            <TD><%
With mobjValues
	Response.Write(.PossiblesValues("cbeMonthP", "table7013", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeMonthPToolTip")))
	Response.Write(.HiddenControl("hdddVp_neg", ""))
End With
%>
            </TD>
        </TR>
    </TABLE>
    </DIV></TD>
</TABLE>
</FORM> 
</BODY>
</HTML>
<SCRIPT>
    insShowDiv();
</SCRIPT>
<%
mobjValues = Nothing%> 

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("val601_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




