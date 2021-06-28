<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.55
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility


'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjAgent As eAgent.Loans_int
Dim mobjMenu As eFunctions.Menues
Dim mobjIntermedia As eAgent.Intermedia

Dim mobjDate As Object
Dim mintState As String


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout

mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AG004")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.55
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "AG004"
mobjAgent = New eAgent.Loans_int
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.55
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "AG004", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End If

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>


    <%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.WindowsTitle("AG004", Request.QueryString.Item("sWindowDescript")))
Response.Write(mobjMenu.setZone(2, "AG004", "AG004.aspx"))
mobjMenu = Nothing
%>
<SCRIPT> 
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 18.00 $"        

// SetControl : Establece el valor de varios campos de la página
//-----------------------------------------------------------------------------------
function SetControl(){
//-----------------------------------------------------------------------------------
	with (self.document.forms[0]){
// Se establece el estado de porcentaje
		if (cbePayForm.value != 0){
		    if (cbePayForm.value==1 || cbePayForm.value==4){
		        tcnPercent_ant.disabled = false;
		        tcnInterest_ant.value = VTFormat(0, '', '', '', 2, true);
		        tcnInterest_ant.disabled = true;
		        tcnMonthly.value = VTFormat(0, '', '', '', 6, true);
		        tcnMonthly.disabled = true;
		    }
		    else if(cbePayForm.value==2||cbePayForm.value==3){
                tcnPercent_ant.disabled = true;
                tcnPercent_ant.value = VTFormat(0, '', '', '', 2, true);
                tcnInterest_ant.value = VTFormat(0, '', '', '', 2, true);
                tcnMonthly.disabled = false;
                if(cbePayForm.value==3)
                    tcnInterest_ant.disabled = false;
                else
                    tcnInterest_ant.disabled = true;
		    }
		}
	}
}

// SetCuotes : Establece el valor de las cuotas en base al importe original cuando es un anticipo
//             Actualiza el valor del saldo
//-----------------------------------------------------------------------------------
function SetCuotes(){
//-----------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if (cbeLoanType.value==2){
            tcnMonthly.value=tcnLoanAmount.value;
            cbePayForm.value=2;
        }
        tcnLoanBalance.value = tcnLoanAmount.value;
    }
}

// SetCurrency : Habilita el campo "Ramo" del frame "Aplica Sobre" para asegurar en que moneda
// se van a mostrar los montos.
//-----------------------------------------------------------------------------------
function SetTypeCurrency(){
//-----------------------------------------------------------------------------------

    with(self.document.forms[0]){
        if (cbeLoanType.value==2){
            if(cbeCurrency.value > 0){
                cbeBranch.disabled=false;
            }else{
                cbeBranch.disabled=true;
            }
        }
    
        if(tcnLoanAmount.value>0){
            if(lintCurrency != cbeCurrency.value){
                var strParams
        
    	    	strParams = "nLoanAmount=" + tcnLoanAmount.value + 
		    				"&nCurrencyDes=" + cbeCurrency.value +
		    				"&Date=" + tcdEffecDate.value;

                insDefValues("Exchange", strParams, '/VTimeNet/Agent/Agent'); 
            }
        }
    }
}

// SetType : Establece el estado de la página según el tipo de anticipo
//-----------------------------------------------------------------------------------
function SetType(){
//-----------------------------------------------------------------------------------
    with (self.document.forms[0]){
		if (cbeLoanType.value != 0){
			cbeCurrency.disabled = false;
			cbePayOrder.disabled = false;
//+Anticipos
			if (cbeLoanType.value==2){
                tcnLoanAmount.disabled = true;
			    cbePayForm.disabled=true;
   			    tcnMonthly.disabled=true;
			    tcnInterest_ant.disabled=true;
			    tcnPercent_ant.disabled=true;
                tcnMonthly.disabled=true;
			    tcnMonthly.value = VTFormat(tcnLoanAmount.value, '', '', '', 6, true);
			    cbePayForm.value = 2;

//+Se habilita si el campo moneda tiene valor 
                if(cbeCurrency.value>0){
			        cbeBranch.disabled=false;
                }else{
                    cbeBranch.disabled=true;
                }
//+Prestamos
			}else{
			    tcnLoanAmount.disabled = false;
			    cbeBranch.disabled=true;
			    valProduct.disabled=true;
			    btnvalProduct.disabled=true;
			    tcnPolicy.disabled=true;
			    cbeMode.disabled=true;
			    tcnPercent.disabled=true;
                cbePayForm.disabled=false;
                tcnLoanBalance.value=VTFormat(0, '', '', '', 6, true);
                tcnMonthly.value = VTFormat(0, '', '', '', 6, true);
                cbePayForm.value = 0;
                cbeBranch.value=0;
                valProduct.value='';
                $(valProduct).change();
                tcnPolicy.value='';
                tcnPercent_ant.value=VTFormat(0, '', '', '', 2, true);
                tcnCommBase.value=VTFormat(0, '', '', '', 6, true);
                cbeMode.value=0;
                tcnPercent.value=VTFormat(0, '', '', '', 2, true);
			    tcnLoanAmount.value=VTFormat(0, '', '', '', 6, true);
			}
//+Sin valor
		}else{
		    SetFieldsToAction();
		}
    }
}

// ChangeValues : Habilita o deshabilita campos en la ventana
//--------------------------------------------------------------------------------------------
function ChangeValues(Field,key){
//--------------------------------------------------------------------------------------------
	var strParams; 
	with (self.document.forms[0]){
        switch (key){
			case 'Product' :
                if(valProduct.value>0){
                    tcnPolicy.disabled = false; 
                    strParams = "nBranch=" + cbeBranch.value + 
								"&nProduct=" + valProduct.value +
								"&dEffecdate=" + tcdEffecDate.value;
                    insDefValues("Product", strParams, '/VTimeNet/Agent/Agent');                       
                }                    
                break;
			case 'Policy' :
    			strParams = "nBranch=" + cbeBranch.value + 
							"&nProduct=" + valProduct.value + 
							"&nPolicy=" + Field.value +
							"&nLoanAmount=" + tcnLoanAmount.value +
							"&nCurrency=" + cbeCurrency.value + 
							"&Date=" + tcdEffecDate.value;
				insDefValues("Policy",strParams,'/VTimeNet/Agent/Agent'); 
                break; 
			case 'Percent' :
				if (Field.value != 0 && Field.value != ''){
					tcnLoanAmount.value = VTFormat(insConvertNumber(tcnCommBase.value) * insConvertNumber(Field.value)/100, '', '', '', 6, true);
					$(tcnLoanAmount).change();
                }
                break;
		}		
	}		
}		
// SetFieldsToAction : Establece el estado inicial de la forma según el tipo de acción
//                     ejecutada.
//-----------------------------------------------------------------------------------
function SetFieldsToAction(){
//-----------------------------------------------------------------------------------
    with (self.document.forms[0]){
        switch (top.fraSequence.plngMainAction){
            case 302 :
                cbeLoanType.disabled = false;
                cbeCurrency.disabled = false;
                tcnLoanAmount.disabled = false;
                cbePayForm.disabled = false;
                tcnMonthly.disabled = false;
                cbePayOrder.disabled = false;
                if (cbeLoanSta.value == 5){
					tcdEffecDate.disabled = true;
					btn_tcdEffecDate.disabled = true;
					cbeLoanType.disabled = true;
					cbeCurrency.disabled = true;
					tcnLoanAmount.disabled = true;
					cbeBranch.disabled = true;
					valProduct.disabled = true;
					tcnPolicy.disabled = true;                
					cbePayForm.disabled = true;
					tcnMonthly.disabled = true;
					cbePayOrder.disabled = true;               
				}
                break;
            case 301 :
			        cbeMode.disabled = true;
                    tcnPercent.disabled = true;
					cbeCurrency.disabled = true;
					tcnLoanAmount.disabled = true;
					cbeBranch.disabled = true;
					valProduct.disabled = true;
					tcnPolicy.disabled = true;                
					cbePayForm.disabled = true;
					tcnMonthly.disabled = true;
					cbePayOrder.disabled = true;               
					tcnPercent_ant.disabled = true;               

                    tcnCommBase.value = VTFormat(0, '', '', '', 6, true);
					cbeCurrency.value = 0;
					tcnLoanAmount.value = VTFormat(0, '', '', '', 6, true);
					tcnPercent_ant.value = VTFormat(0, '', '', '', 2, true);
					cbeBranch.value = 0;
					valProduct.value = '';
					$(valProduct).change();
					tcnPolicy.value = '';
					cbePayForm.value = 0;
					tcnMonthly.value = VTFormat(0, '', '', '', 6, true);
					cbePayOrder.value = 0;               
					tcnLoanBalance.value=VTFormat(0, '', '', '', 6, true);
			        cbeMode.value = 0;
                    tcnPercent.value = VTFormat(0, '', '', '', 2, true);
        }
    }
}
</SCRIPT>
</HEAD>
<%
Response.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeLoanId.value=" & Session("cbeLoanId") & ";</SCRIPT>")
Call mobjAgent.Find(mobjValues.StringToType(Session("valIntermedia"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("cbeLoanId"), eFunctions.Values.eTypeData.etdDouble))
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then
	mobjDate = Today
	mintState = "1"
Else
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
		mobjValues.ActionQuery = True
	End If
	mobjDate = mobjAgent.dDateLoan
	mintState = mobjAgent.sStatLoan
	Session("tcdEffecdate") = mobjDate
End If
%>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmIntermLoans" ACTION="ValAgent.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName("AG004", Request.QueryString.Item("sWindowDescript")))%>
    <BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Anticipo"><%= GetLocalResourceObject("AnchorAnticipoCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcdEffecDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecDate", mobjValues.StringToType(mobjDate, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdEffecDateToolTip"))%></TD>
            <TD><LABEL><%= GetLocalResourceObject("cbeLoanTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeLoanType", "Table245", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(CStr(mobjAgent.nTypeLoan), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  , "SetType();",  ,  , GetLocalResourceObject("cbeLoanTypeToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("cbeLoanStaCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeLoanSta", "Table191", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(mintState, eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeLoanStaToolTip"))%></TD>
			<TD><LABEL><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(CStr(mobjAgent.nCurrency), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  , "SetTypeCurrency();", True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnLoanAmountCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnLoanAmount", 18, CStr(mobjAgent.nAmoLoan),  , GetLocalResourceObject("tcnLoanAmountToolTip"), True, 6,  ,  ,  , "SetCuotes()", True)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcnLoanBalanceCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnLoanBalance", 18, mobjValues.StringToType(CStr(mobjAgent.nBalanLoan), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnLoanBalanceToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Aplica"><%= GetLocalResourceObject("AnchorAplicaCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
		<TR>	
			<TD><LABEL><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), CStr(mobjAgent.nBranch),  ,  ,  ,  ,  , True)%></TD>	
			<TD><LABEL><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
		    <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType,  , CStr(mobjAgent.nProduct),  ,  ,  , "ChangeValues(this,""Product"");")%></TD> 
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, CStr(mobjAgent.nPolicy),  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "ChangeValues(this, ""Policy"");", True)%></TD> 
			<TD><LABEL ><%= GetLocalResourceObject("tcnCommBaseCaption") %></LABEL></TD> 
			<TD><%=mobjValues.NumericControl("tcnCommBase", 18, mobjValues.StringToType(CStr(mobjAgent.nCommBase), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnCommBaseToolTip"), True, 6,  ,  ,  ,  , True)%></TD> 
        </TR> 
        <TR> 
			<TD><LABEL><%= GetLocalResourceObject("cbeModeCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeMode", "Table5601", eFunctions.Values.eValuesType.clngComboType, CStr(mobjAgent.nCodmodpay),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeModeToolTip"))%></TD>  
            <TD><LABEL ><%= GetLocalResourceObject("tcnPercentCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPercent", 5, mobjValues.StringToType(CStr(mobjAgent.nLoan_perc), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnPercentToolTip"), False, 2,  ,  ,  , "ChangeValues(this, ""Percent"");", True)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ><A NAME="Pago"><%= GetLocalResourceObject("AnchorPagoCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
		<TR>	
			<TD><LABEL ><%= GetLocalResourceObject("cbePayFormCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbePayForm", "Table180", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(CStr(mobjAgent.nFor_pay), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  , "SetControl();", True,  , GetLocalResourceObject("cbePayFormToolTip"))%></TD>
			<TD><LABEL ><%= GetLocalResourceObject("tcnPercent_antCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPercent_ant", 5, mobjValues.StringToType(CStr(mobjAgent.nRate_ret), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnPercent_antToolTip"), False, 2,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ><%= GetLocalResourceObject("tcnInterest_antCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnInterest_ant", 5, mobjValues.StringToType(CStr(mobjAgent.nRate_int), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnInterest_antToolTip"), False, 2,  ,  ,  ,  , True)%></TD>
			<TD><LABEL ><%= GetLocalResourceObject("tcnMonthlyCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnMonthly", 18, mobjValues.StringToType(CStr(mobjAgent.nCuoMonth), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnMonthlyToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
		</TR>	
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ><A NAME="Orden de pago"><%= GetLocalResourceObject("AnchorOrden de pagoCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
			<TD><LABEL ><%= GetLocalResourceObject("cbePayOrderCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbePayOrder", "Table193", eFunctions.Values.eValuesType.clngComboType, mobjAgent.sPayOrder,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbePayOrderToolTip"))%></TD>
			<TD><LABEL ><%= GetLocalResourceObject("tctReqCheqCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tctReqCheq", 14, CStr(mobjAgent.nRequest_nu),  , GetLocalResourceObject("tctReqCheqToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
		</TR>	
    </TABLE>
    <%
mobjIntermedia = New eAgent.Intermedia
mobjIntermedia.Find(mobjValues.StringToType(Session("valIntermedia"), eFunctions.Values.eTypeData.etdDouble))
Response.Write(mobjValues.HiddenControl("hddLegal_sch", CStr(mobjIntermedia.nLegal_sch)))
Select Case mobjIntermedia.nLegal_sch
	'boleta
	Case CInt("1")
		Response.Write(mobjValues.HiddenControl("hddTypesupport", "3"))
		'Factura    
	Case CInt("inter_type")
		Response.Write(mobjValues.HiddenControl("hddTypesupport", "1"))
	Case Else
		Response.Write(mobjValues.HiddenControl("hddTypesupport", "0"))
End Select

mobjIntermedia = Nothing
%>
    <SCRIPT>
        var lintCurrency
        lintCurrency = <%=mobjAgent.nCurrency%>;
        if(top.fraSequence.plngMainAction==302){SetFieldsToAction();}
    </SCRIPT>
    
    <%=mobjValues.BeginPageButton%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjAgent = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.55
Call mobjNetFrameWork.FinishPage("AG004")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




