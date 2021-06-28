<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsProdLifeSeq As eProduct.ProdLifeSeq


</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mclsProdLifeSeq = New eProduct.ProdLifeSeq
End With

mobjValues.ActionQuery = session("bQuery")

Call mclsProdLifeSeq.insPreDP043A(session("nBranch"), session("nProduct"), mobjValues.StringToType(session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))

mobjValues.sCodisplPage = "dp043a"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


	<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
	.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "DP043A.aspx"))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
document.VssVersion="$$Revision: 2 $|$$Date: 17/02/06 13:03 $|$$Author: Clobos $"

//%insHandInterest:(Des)habilita campos si rutina (no) tiene valor
//---------------------------------------------------------------------------------
function insHandInterest(Field){
//---------------------------------------------------------------------------------
    with(self.document.forms[0]){
		if (Field.value.replace(/ */,'') == ''){
		    tcnQMEPLoans.value=0;
		    tcnQMMLoans.value=0;
		    tcnQMYLoans.value=0;
		    tcnAMinLoans.value=0;
		    tcnAMaxLoans.value=0;
		    tcnPerVSLoans.value=0;
		    tcnTaxes.value=0;
		    tcnPercTol.value=0;
		    tcnInterest.value=0;
		    cbePayInter.value=0;
		    cbeAnlifint.value=0;
		    tctRouInterest.value='';
		    cbeBill_item.value=0;
		    tcnQMEPLoans.disabled=true;
		    tcnQMMLoans.disabled=true;
		    tcnQMYLoans.disabled=true;
			tcnAMinLoans.disabled=true;
		    tcnAMaxLoans.disabled=true;
		    tcnPerVSLoans.disabled=true;
		    tcnTaxes.disabled=true;
		    tcnPercTol.disabled=true;
		    tcnInterest.disabled=true;
		    cbeAnlifint.disabled=true;
		    cbePayInter.disabled=true;
		    tctRouInterest.disabled=true;
		    cbeBill_item.disabled=true;
		}
		else{
		    tcnQMEPLoans.disabled=false;
		    tcnQMMLoans.disabled=false;
		    tcnQMYLoans.disabled=false;
		    tcnAMinLoans.disabled=false;
		    tcnAMaxLoans.disabled=false;
		    tcnPerVSLoans.disabled=false;
		    tcnTaxes.disabled=false;
		    tcnPercTol.disabled=false;
		    tcnInterest.disabled=false;
		    cbeAnlifint.disabled=false;
		    cbePayInter.disabled=false;
		    tctRouInterest.disabled=false;
		    cbeBill_item.disabled=false;
		}
    }
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP043A" ACTION="valProdLifeSeq.aspx?mode=1">
    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="40%"><LABEL ID=14910><%= GetLocalResourceObject("tctRouAdvanCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctRouAdvan", 12, mclsProdLifeSeq.DefaultValueDP043A("tctRouAdvan"),  , GetLocalResourceObject("tctRouAdvanToolTip"),  ,  ,  , "insHandInterest(this)")%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnQMEPLoansCaption") %></LABEL></TD>
			<TD WIDTH="10%"><%=mobjValues.NumericControl("tcnQMEPLoans", 5, mclsProdLifeSeq.DefaultValueDP043A("tcnQMEPLoans"),  , GetLocalResourceObject("tcnQMEPLoansToolTip"),  ,  ,  ,  ,  ,  , mclsProdLifeSeq.DefaultValueDP043A("tcnQMEPLoans.disabled"))%></TD>
		</TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnQMMLoansCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnQMMLoans", 5, mclsProdLifeSeq.DefaultValueDP043A("tcnQMMLoans"),  , GetLocalResourceObject("tcnQMMLoansToolTip"),  ,  ,  ,  ,  ,  , mclsProdLifeSeq.DefaultValueDP043A("tcnQMMLoans.disabled"))%></TD>
			<TD WIDTH="30%"><LABEL ID=0><%= GetLocalResourceObject("tcnQMYLoansCaption") %></LABEL></TD>
			<TD WIDTH="20%"><%=mobjValues.NumericControl("tcnQMYLoans", 5, mclsProdLifeSeq.DefaultValueDP043A("tcnQmyLoans"),  , GetLocalResourceObject("tcnQMYLoansToolTip"),  ,  ,  ,  ,  ,  , mclsProdLifeSeq.DefaultValueDP043A("tcnQmyLoans.disabled"))%></TD>
		</TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnAMinLoansCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAMinLoans", 18, mclsProdLifeSeq.DefaultValueDP043A("tcnAminLoans"),  , GetLocalResourceObject("tcnAMinLoansToolTip"), True, 6,  ,  ,  ,  , mclsProdLifeSeq.DefaultValueDP043A("tcnAminLoans.disabled"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnAMaxLoansCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAMaxLoans", 18, mclsProdLifeSeq.DefaultValueDP043A("tcnAmaxLoans"),  , GetLocalResourceObject("tcnAMaxLoansToolTip"), True, 6,  ,  ,  ,  , mclsProdLifeSeq.DefaultValueDP043A("tcnAmaxLoans.disabled"))%></TD>
		</TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnPerVSLoansCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPerVSLoans", 5, mclsProdLifeSeq.DefaultValueDP043A("tcnPervsLoans"),  , GetLocalResourceObject("tcnPerVSLoansToolTip"), True, 2,  ,  ,  ,  , mclsProdLifeSeq.DefaultValueDP043A("tcnPervsLoans.disabled"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnTaxesCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnTaxes", 5, mclsProdLifeSeq.DefaultValueDP043A("tcnTaxes"),  , GetLocalResourceObject("tcnTaxesToolTip"), True, 2,  ,  ,  ,  , mclsProdLifeSeq.DefaultValueDP043A("tcnTaxes.disabled"))%></TD>
		</TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnPercTolCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPercTol", 5, mclsProdLifeSeq.DefaultValueDP043A("tcnPercTol"),  , GetLocalResourceObject("tcnPercTolToolTip"), True, 2,  ,  ,  ,  , mclsProdLifeSeq.DefaultValueDP043A("tcnPercTol.disabled"))%></TD>
            <TD><LABEL><%= GetLocalResourceObject("cbeOrigin_LoanCaption") %></LABEL></TD>
            <TD><%mobjValues.Parameters.Add("nBranch", session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nProduct", session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("cbeOrigin_Loan", "tab_ord_origin", eFunctions.Values.eValuesType.clngComboType, mclsProdLifeSeq.DefaultValueDP043A("cbeOrigin_Loan"), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOrigin_LoanToolTip")))%></TD>
		</TR>
        <TR>
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="4" CLASS="Horline"></TD>
		</TR>
		<TR>
			<TD><LABEL ID=14898><%= GetLocalResourceObject("tcnInterestCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnInterest", 4, mclsProdLifeSeq.DefaultValueDP043A("tcnInterest"),  , GetLocalResourceObject("tcnInterestToolTip"), True, 2)%></TD>
			<TD><LABEL ID=19397><%= GetLocalResourceObject("cbeAnlifintCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeAnlifint", "Table34", 1, mclsProdLifeSeq.DefaultValueDP043A("cbeAnlifint"),  ,  ,  ,  ,  ,  , mclsProdLifeSeq.DefaultValueDP043A("cbeAnlifint.disabled"),  , GetLocalResourceObject("cbeAnlifintToolTip"))%></TD>
		</TR>
        <TR>
			<TD><LABEL ID=14903><%= GetLocalResourceObject("cbePayInterCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbePayInter", "Table74", eFunctions.Values.eValuesType.clngComboType, mclsProdLifeSeq.DefaultValueDP043A("cbePayInter"),  ,  ,  ,  ,  ,  , mclsProdLifeSeq.DefaultValueDP043A("cbePayInter.disabled"),  , GetLocalResourceObject("cbePayInterToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tctRouInterestCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctRouInterest", 12, mclsProdLifeSeq.DefaultValueDP043A("tctRouInterest"),  , GetLocalResourceObject("tctRouInterestToolTip"),  ,  ,  ,  , mclsProdLifeSeq.DefaultValueDP043A("tctRouInterest.disabled"))%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=14903><%= GetLocalResourceObject("cbeBill_itemCaption") %></LABEL></TD>
			<TD><%
With mobjValues
	.Parameters.Add("nBranch", .StringToType(session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", .StringToType(session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", .StringToType(session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(.PossiblesValues("cbeBill_item", "tabTab_bill_i", eFunctions.Values.eValuesType.clngComboType, mclsProdLifeSeq.DefaultValueDP043A("cbeBill_item"), True,  ,  ,  ,  ,  , mclsProdLifeSeq.DefaultValueDP043A("cbeBill_item.disabled"),  , GetLocalResourceObject("cbeBill_itemToolTip")))
End With
%>
			</TD>
			<TD COLSPAN="2">&nbsp;</TD>
        </TR>
	</TABLE>
<%=mobjValues.BeginPageButton%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mclsProdLifeSeq = Nothing
%>




