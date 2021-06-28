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


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("col507_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "col507_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 29/12/03 19:02 $|$$Author: Nvaplat7 $"
    </SCRIPT>


<SCRIPT>
// insStateZone :
//-----------------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------------
}

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

// SetBank: Establece el valor según el banco en selección para obtener una lista de sus cuentas.
//-----------------------------------------------------------------------------------
function SetBank(Field){
//-----------------------------------------------------------------------------------
    if (Field != "" && Field != 0){
        with(self.document.forms[0]){
            valAcc_number.Parameters.Param1.sValue=valBank.value}
	        self.document.forms[0].valAcc_number.disabled = false;
	        self.document.forms[0].btnvalAcc_number.disabled = false;
	        }
    else{
	     self.document.forms[0].valAcc_number.disabled = true;
	     self.document.forms[0].btnvalAcc_number.disabled = true;
	    }
}
</SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("COL507", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.MakeMenu("COL507", "COL507_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDirPayBank" ACTION="valCollectionRep.aspx?mode=1" ENCTYPE="multipart/form-data">
<BR></BR>
    <%Response.Write(mobjValues.ShowWindowsName("COL507", Request.QueryString.Item("sWindowDescript")))%>
    <TABLE WIDTH="100%">
        <TR>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=9912><%= GetLocalResourceObject("valBankCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valBank", "table7", 1,  ,  ,  ,  ,  ,  , "SetBank(this.value)",  ,  , GetLocalResourceObject("valBankToolTip"))%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=9915><%= GetLocalResourceObject("valAcc_numberCaption") %></LABEL></TD>
                <%mobjValues.Parameters.Add("nBank_code", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
            <TD><%=mobjValues.PossiblesValues("valAcc_number", "TabBank_accbank", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valAcc_numberToolTip"))%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=9916><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD><%=mobjValues.FileControl("tctFile", 30)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=9914><%= GetLocalResourceObject("tcdPayDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdPayDate", CStr(Today),  , GetLocalResourceObject("tcdPayDateToolTip"))%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=9913><%= GetLocalResourceObject("tcdLimit_payCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdLimit_pay",  ,  , GetLocalResourceObject("tcdLimit_payToolTip"),  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=9916><%= GetLocalResourceObject("tcnAmountPayCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAmountPay", 20, CStr(0),  ,  , True, 6,  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59
Call mobjNetFrameWork.FinishPage("col507_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




