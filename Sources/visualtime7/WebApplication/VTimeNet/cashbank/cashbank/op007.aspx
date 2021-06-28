<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mclsCheque As eCashBank.Cheque
Dim mobjMenu As eFunctions.Menues


'%insPreC007: Esta rutina se encarga de cargar todos los controles de la forma
'--------------------------------------------------------------------------------------------
Private Function insPreC007() As Object
	'--------------------------------------------------------------------------------------------
	'799
	With mclsCheque
		Call .FindByPayOrder(mobjValues.StringToType(Session("nRequest_nu"), eFunctions.Values.eTypeData.etdDouble), CStr(eRemoteDB.Constants.strnull), CShort(eRemoteDB.Constants.intNull))
		Call .FindChqWithNumber(mobjValues.StringToType(Session("nRequest_nu"), eFunctions.Values.eTypeData.etdDouble))
	End With
	
	'+ Se reasignan los valores del ancabezado de la forma
	
	
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnRequeNum.value=" & Session("nRequest_nu") & "; </" & "Script>")
End Function

</script>
<%Response.Expires = -1

With Server
	mclsCheque = New eCashBank.Cheque
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With

mobjValues.sCodisplPage = "op007"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "OP007", "OP007.aspx"))
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
		mobjValues.ActionQuery = True
	Else
		mobjValues.ActionQuery = False
	End If
End With
mobjMenu = Nothing%>
<SCRIPT>
//%	insStateQuanPayAmount: Permite condicionar que la cantidad introducida se diferente a "Vacio"
//-------------------------------------------------------------------------------------------
function insStateQuanPayAmount(Field){
//-------------------------------------------------------------------------------------------
    if (Field.value==0 || Field.value=='')
	    Field.value=0
}
//%insShowControls: Permite mostrar a los campos Por medio de, y Fecha de contablilización
//%con el valor que previamente se haya introducido en los campos Beneficiario y 
//%Fecha de emisión respectivamente
//-------------------------------------------------------------------------------------------
function insShowControls(Field,sField){

    if (sField=='Interm')
        document.forms[0].dtcInterm.value=document.forms[0].dtcBenef.value
    else
        document.forms[0].tcdLedgerDat.value=Field.value
}

//% ShowChangeCurrency: Se habilita/deshabilita el campo moneda
//-------------------------------------------------------------------------------------------
function ShowChangeCompany(Field){
//-------------------------------------------------------------------------------------------

	with (self.document.forms[0]){
		valConcept.value="";
		valConcept.Parameters.Param1.sValue=Field.value;
	}
}	

//% ShowChangeCurrency: Se habilita/deshabilita el campo moneda
//-------------------------------------------------------------------------------------------
function ShowChangeAccountNum(Field){
//-------------------------------------------------------------------------------------------
 	with (self.document.forms[0]){
 	    if (Field=='1')
	 	    ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=CurrencyAccount&Account_number=" + valAccountNum.value ,"ShowDefValuesMove_acc", 1, 1,"no","no",2000,2000);	
	    else
			ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=CurrencyAccount&Account_number=" + htcAccountNum.value ,"ShowDefValuesMove_acc", 1, 1,"no","no",2000,2000);	
	}
}	

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="valCashBank.aspx?mode=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <%Call insPreC007()%>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=8643><%= GetLocalResourceObject("tcdDatProposCaption") %></LABEL></TD>
            <%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 301 Then%>
                <TD COLSPAN="3"><%=mobjValues.DateControl("tcdDatPropos", mobjValues.TypeToString(mclsCheque.dDat_propos, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdDatProposToolTip"))%><TD>
            <%Else%>
<TD COLSPAN="3"><% %>
<%=	mobjValues.DateControl("tcdDatPropos", CStr(Today),  , GetLocalResourceObject("tcdDatProposToolTip"))%><TD>
            <%End If%>
        </TR>      
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40067><A NAME="Cuenta bancaria"><%= GetLocalResourceObject("AnchorCuenta bancariaCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8637><%= GetLocalResourceObject("valAccountNumCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valAccountNum", "tabBank_acc", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsCheque.nAcc_Bank),  ,  ,  ,  ,  , "ShowChangeAccountNum('1')",  , 5, GetLocalResourceObject("valAccountNumToolTip"))%><%=mobjValues.HiddenControl("htcAccountNum", CStr(mclsCheque.nAcc_Bank))%></TD>
			<TD><LABEL ID=8642><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("lblCurrency")%></TD>
        </TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCompanyCaption") %></LABEL></TD>
			<TD COLSPAN="3"><% =mobjValues.PossiblesValues("cbeCompany", "company", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCheque.nCompany),  ,  ,  ,  ,  , "ShowChangeCompany(this);",  ,  , "")%></TD>
        <TR>
            <TD><LABEL ID=8641><%= GetLocalResourceObject("valConceptCaption") %></LABEL></TD>
   	            <%With mobjValues.Parameters
	.Add("nCompany", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With%>			
		    <TD COLSPAN="3"><%=mobjValues.PossiblesValues("valConcept", "tabconceptscompany", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsCheque.nConcept), True,  ,  ,  ,  ,  ,  , 8, GetLocalResourceObject("valConceptToolTip"))%></TD>            
        </TR>    
        <TR>
            <TD><LABEL ID=8644><%= GetLocalResourceObject("tctDescriptCaption") %></LABEL></TD>
            <TD COLSPAN="3"><%=mobjValues.TextControl("tctDescript", 60, mclsCheque.sDescript,  , GetLocalResourceObject("tctDescriptToolTip"))%></TD>
        </TR>    
        <TR>
            <TD><LABEL ID=8639><%= GetLocalResourceObject("dtcBenefCaption") %></LABEL></TD>
            <%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then%>
				<TD COLSPAN="3"><%=mobjValues.ClientControl("dtcBenef", mclsCheque.sClient,  , GetLocalResourceObject("dtcBenefToolTip"), "insShowControls(this.value,""Interm"");",  , "lblBenefname", False,  ,  ,  ,  ,  , True)%></TD>
			<%Else%>
                <TD COLSPAN="3"><%=mobjValues.TextControl("dtcBenef", 14, mclsCheque.sClient,  , GetLocalResourceObject("dtcBenefToolTip"),  ,  ,  ,  , True)%></TD>
            <%End If%>
        </TR>
        <TR>
            <TD><LABEL ID=8645><%= GetLocalResourceObject("dtcIntermCaption") %></LABEL></TD>
            <%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then%>
	            <TD COLSPAN="3"><%=mobjValues.ClientControl("dtcInterm", mclsCheque.sInter_pay,  , GetLocalResourceObject("dtcIntermToolTip"),  ,  , "lblInterName", False,  ,  ,  ,  ,  , True)%></TD>
			<%Else%>
			    <TD COLSPAN="3"><%=mobjValues.TextControl("dtcInterm", 14, mclsCheque.sInter_pay,  , GetLocalResourceObject("dtcIntermToolTip"),  ,  ,  ,  , True)%></TD>
            <%End If%>            
        </TR>
        <TR>
            <TD><LABEL ID=8638><%= GetLocalResourceObject("tcnAmountCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAmount", 18, CStr(mclsCheque.nAmount),  , GetLocalResourceObject("tcnAmountToolTip"), True, 6,  ,  ,  , "insStateQuanPayAmount(this)")%></TD>
            <TD><LABEL ID=8649><%= GetLocalResourceObject("tcnQuanPayCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnQuanPay", 2, CStr(mclsCheque.nQ_pays),  , GetLocalResourceObject("tcnQuanPayToolTip"),  , 0,  ,  ,  , "insStateQuanPayAmount(this)")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8648><%= GetLocalResourceObject("cbePayFrecCaption") %></LABEL></TD>
                <%mobjValues.TypeList = 1
mobjValues.BlankPosition = True
mobjValues.List = "1,2,3,4,5"%>
            <TD><%=mobjValues.PossiblesValues("cbePayFrec", "Table36", eFunctions.Values.eValuesType.clngComboType, mclsCheque.sPay_freq,  ,  ,  ,  ,  ,  ,  , 1, GetLocalResourceObject("cbePayFrecToolTip"))%></TD>
            
            <TD><LABEL ID=8640><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("lblCheqPrint",  , mobjValues.TypeToString(mclsCheque.nCount_pend, eFunctions.Values.eTypeData.etdDouble))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8646><%= GetLocalResourceObject("tcdIssueDatCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdIssueDat", mobjValues.TypeToString(mclsCheque.dIssue_dat, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdIssueDatToolTip"),  ,  ,  , "insShowControls(this,""IssueDat"")")%></TD>
            <TD><LABEL ID=8647><%= GetLocalResourceObject("tcdLedgerDatCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdLedgerDat", mobjValues.TypeToString(mclsCheque.dLedger_dat, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdLedgerDatToolTip"))%></TD>
        </TR>
        <TR>
            <TD WIDTH="26%"><LABEL ID=8651><%= GetLocalResourceObject("valReqUserCaption") %></LABEL></TD>
            <%If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionadd) And Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then%>
                <TD COLSPAN="3"><%=mobjValues.PossiblesValues("valReqUser", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsCheque.nUser_sol),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valReqUserToolTip"))%></TD>
            <%ElseIf Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then %>
				<TD COLSPAN="3"><%=mobjValues.PossiblesValues("valReqUser", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsCheque.nUser_sol),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valReqUserToolTip"))%></TD>
			<%Else%>
			    <TD COLSPAN="3"><%=mobjValues.PossiblesValues("valReqUser", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, Session("nUsercode"),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valReqUserToolTip"))%></TD>
            <%End If%>        
        </TR>
        <%With Response
	.Write(mobjValues.HiddenControl("tctBenefname", mclsCheque.sBenef_name))
	.Write(mobjValues.HiddenControl("tctInterName", mclsCheque.sInter_name))
End With%>
    </TABLE>
    <%=mobjValues.BeginPageButton%>
</FORM>
</BODY>
</HTML>
<%
mclsCheque = Nothing
mobjValues = Nothing%>
<SCRIPT>
ShowChangeAccountNum('2');
</SCRIPT>




