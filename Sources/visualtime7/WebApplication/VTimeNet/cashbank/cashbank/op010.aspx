<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mclsCheq_book As eCashBank.Cheq_book
Dim mobjMenu As eFunctions.Menues


'% insPreOP010(): Esta función es la encargada de consultar la información general de una
'% chequera en particular (asociada a un código de cuenta bancaria)
'--------------------------------------------------------------------------------------------
Private Sub insPreOP010()
	'--------------------------------------------------------------------------------------------
	Call mclsCheq_book.Find(mobjValues.StringToType(Session("nAcc_bank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")))
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsCheq_book = New eCashBank.Cheq_book

mobjValues.sCodisplPage = "OP010"

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 11/02/04 17:25 $|$$Author: Nvaplat7 $"

//%	ShowChangeValues: Permite hacer el llamado a la ventana que aculizalos datos de la página
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
    with (document.forms[0]){
		switch(sField){
			case "CheqInitEnd":
				ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=CheqInitEnd" + "&nCheqInit="+ tcnCheqInit.value + "&nCheqEnd=" + tcnCheqEnd.value + "&nCheqIssue=" + tcnCheqIssue.value + "&nCheqCancel=" + tcnCheqCancel.value + "&nCheqDan=" + tcnCheqDan.value,"ShowDefValuesCheques",1, 1,"no","no",2000,2000);
				break;
			case "CheqDan":
				 ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=CheqDan" + "&nCheqInit="+ tcnCheqInit.value + "&nCheqEnd=" + tcnCheqEnd.value + "&nCheqIssue=" + tcnCheqIssue.value + "&nCheqCancel=" + tcnCheqCancel.value + "&nCheqDan=" + tcnCheqDan.value,"ShowDefValuesCheques",1, 1,"no","no",2000,2000);
				 break;
			case "CheqIssueOutstand":
				ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=CheqIssueOutstand" + "&nCheqInit="+ tcnAuxCheqInit.value + "&nCheqEnd=" + tcnAuxCheqEnd.value + "&nCheqIssue=" + tcnCheqIssue.value + "&nCheqCancel=" + tcnCheqCancel.value + "&nCheqDan=" + tcnAuxCheqDan.value,"ShowDefValuesCheques",1, 1,"no","no",2000,2000);
				 break;
       }
	}
}
function insShowHeader(){
    var lblnContinue=true
    if (typeof(top.fraHeader.document)!='undefined') {
	    if (typeof(top.fraHeader.document.forms[0])!='undefined') {
			if (typeof(top.fraHeader.document.forms[0].tcdChequeDate)!='undefined'){
				top.fraHeader.document.forms[0].tcdChequeDate.value= '<%=Session("dEffecdate")%>'
				top.fraHeader.document.forms[0].valAccountNum.value=  '<%=Session("nAcc_bank")%>' 
				top.fraHeader.$('#valAccountNum').change();
				lblnContinue = false
			}
		}
	}
    if (lblnContinue)
		setTimeout("insShowHeader()",50);
}
setTimeout("insShowHeader()",50)
</SCRIPT>
    <%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "OP010", "OP010.aspx"))
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
mobjMenu = Nothing
%>
</HEAD>
<%Call insPreOP010()%>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCheqUpdate" ACTION="ValCashBank.aspx?mode=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <TABLE WIDTH="100%">
		<BR>
		<%=mobjValues.ShowWindowsName("OP010")%>
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40086><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40087><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="2"><HR></TD>
            <TD>&nbsp;</TD>
            <TD WIDTH="100%" COLSPAN="2"><HR></TD>
        </TR>
        <TR>
			<TD><LABEL ID=8704><%= GetLocalResourceObject("tcnCheqInitCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCheqInit", 10, mclsCheq_book.sCheque_sta,  , "",  , 0,  ,  ,  , "ShowChangeValues(""CheqInitEnd"")")%></TD>
            <TD>&nbsp;</TD>
			<TD><LABEL ID=8705><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("lblCheqIssue",  , CStr(0))%></TD>
		</TR>
        <TR>
			<TD><LABEL ID=8703><%= GetLocalResourceObject("tcnCheqEndCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCheqEnd", 10, mclsCheq_book.sCheque_end,  , "",  ,  ,  ,  ,  , "ShowChangeValues(""CheqInitEnd"")")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=8701><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("lblCheqCancel",  , CStr(mclsCheq_book.nQ_che_null))%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=8706><%= GetLocalResourceObject("tcnCheqLastCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCheqLast", 10, mclsCheq_book.sCheque_las)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=8702><%= GetLocalResourceObject("tcnCheqDanCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCheqDan", 2, CStr(mclsCheq_book.nQ_che_dama),  , "",  ,  ,  ,  ,  , "ShowChangeValues(""CheqDan"")")%></TD>
		</TR>
        <TR>
            <TD colspan=3>&nbsp;</TD>
            <TD><LABEL ID=8707><%= GetLocalResourceObject("Anchor5Caption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("lblCheqOutstand",  , CStr(0))%></TD>
        </TR>
    </TABLE>
    <%With Response
	.Write(mobjValues.HiddenControl("tcnAuxCheqInit", mclsCheq_book.sCheque_sta))
	.Write(mobjValues.HiddenControl("tcnAuxCheqEnd", mclsCheq_book.sCheque_end))
	.Write(mobjValues.HiddenControl("tcnAuxCheqDan", CStr(mclsCheq_book.nQ_che_dama)))
	.Write(mobjValues.HiddenControl("tcnCheqIssue", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnCheqCancel", CStr(mclsCheq_book.nQ_che_null)))
	.Write(mobjValues.HiddenControl("tcnCheqRangeChange", CStr(0)))
End With%>
</FORM>
</BODY>
</HTML>
<SCRIPT>ShowChangeValues("CheqIssueOutstand")</SCRIPT>
<%
mobjValues = Nothing
mclsCheq_book = Nothing
%>




