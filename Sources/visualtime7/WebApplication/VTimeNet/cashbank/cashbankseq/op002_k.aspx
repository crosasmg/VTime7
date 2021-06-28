<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values

Dim lintCashNum As Object
Dim lintCompany As Object
Dim lstrAlert As String
Dim lobjUser_CashNum As eCashBank.User_cashnum
Dim lobjErrors As eGeneral.GeneralFunction
Dim lobjCash_Stat As eCashBank.Cash_stat


</script>
<%Response.Expires = -1

    lobjErrors = New eGeneral.GeneralFunction   
    mobjValues = New eFunctions.Values    
    lobjUser_CashNum = New eCashBank.User_cashnum
lobjCash_Stat = New eCashBank.Cash_stat

mobjValues.sCodisplPage = "OP002_K"

If IsNothing(Request.QueryString.Item("nCashNum")) Then
	If lobjUser_CashNum.Find_nUser(Session("nUserCode")) Then
		lintCashNum = lobjUser_CashNum.nCashNum
            If lobjCash_Stat.valCash_statClosed(lintCashNum, Today) Then
                lstrAlert = "Err. 60129 " & lobjErrors.insLoadMessage(60129)
                Response.Write("<SCRIPT>alert('" & lstrAlert & "')</SCRIPT>")
                lobjCash_Stat = Nothing
            End If
	Else
		lintCashNum = ""
		lstrAlert = "Err. 60104 " & lobjErrors.insLoadMessage(60104)
		Response.Write("<SCRIPT>alert('" & lstrAlert & "')</SCRIPT>")
	End If
Else
	lintCashNum = Request.QueryString.Item("nCashNum")
End If

lintCompany = Session("nCompanyUser")

    lobjErrors = Nothing
    lobjUser_CashNum = Nothing
%>
<HTML>
<HEAD>


	<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>	
	<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 2/10/03 10:29 $"

//%insStateZone: Habilita/Deshabilita los campos de la ventana
//--------------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		optToDeposit[0].disabled = false;
		optToDeposit[1].disabled = false;

		optSelection[0].disabled = false;
		optSelection[1].disabled = false;
                    
		tcdEffecDate.disabled = false;
		tcdRealEffecDate.disabled = false;
		btn_tcdEffecDate.disabled = tcdEffecDate.disabled;
		btn_tcdRealEffecDate.disabled = tcdRealEffecDate.disabled;
		tctDepositNum.disabled = false;
		valAccCash.disabled = false;
		btnvalAccCash.disabled = valAccCash.disabled;
        tcnCash.disabled = false;
        	
    }
}
//%insCancel: Controla la acción "Cancelar" de la página
//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------
	return true;
}   
//%insFinish: Controla la acción "Finalizar" de la página
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}

//%insShowCurrency: Función que muestra la descripción de la moneda asociada a la cuenta
//--------------------------------------------------------------------------------------------------
function insShowCurrency(Field,sLinkSpecial){
//--------------------------------------------------------------------------------------------------
	var strParams; 

	if (Field.value != "" )
	{
	    if (self.document.forms[0].optToDeposit[0].checked)
			strParams = "nAccCash=" + self.document.forms[0].valAccCash.value +  
			            "&sDepositNum=" + self.document.forms[0].tctDepositNum.value +		            
			            "&sLinkSpecial=" + sLinkSpecial +
			            "&nOptDeposit=1" +
			            "&nCashNum=" + self.document.forms[0].tcnCash.value +
			            "&sFieldName=" + Field.name;
		else
			strParams = "nAccCash=" + self.document.forms[0].valAccCash.value +  
			            "&sDepositNum=" + self.document.forms[0].tctDepositNum.value +		            
			            "&sLinkSpecial=" + sLinkSpecial +
			            "&nOptDeposit=2" +
			            "&nCashNum=" + self.document.forms[0].tcnCash.value +
			            "&sFieldName=" + Field.name;
	    insDefValues('OP002',strParams,'/VTimeNet/CashBank/CashBankSeq');
    }
}   

//%ChangeOption: Función que activa o desactiva los campos de acuerdo a la opción seleccionada
//--------------------------------------------------------------------------------------------------
function ChangeOption(Field){
//--------------------------------------------------------------------------------------------------
	var strParams; 

	if(Field.value == 2) //2- Cheque
    {
		self.document.forms[0].item("cbeChequeLocat").disabled=false;
		ShowDiv('DIVlbllocat', 'show');
		ShowDiv('DIVcbelocat', 'show');
	}
	else{
		self.document.forms[0].item("cbeChequeLocat").disabled=true;		
		ShowDiv('DIVlbllocat', 'hide');
		ShowDiv('DIVcbelocat', 'hide');
		}
		
    self.document.forms[0].item("cbeChequeLocat").value=0;

}

//%ChangeSelect: Función que activa o desactiva el campo intermediario
//--------------------------------------------------------------------------------------------------
function ChangeSelect(Field) {
//--------------------------------------------------------------------------------------------------

    if (Field.value == 2) // 2- Intermediario
    {

        with (self.document.forms[0]){
            valIntermed.disabled = false;
            btnvalIntermed.disabled = valIntermed.disabled;
            
            tcnCash.value = '';
            tcnCash.disabled = true;
        }

    }
    else {

        with (self.document.forms[0]){
            valIntermed.value = '';
            UpdateDiv('valIntermedDesc', '');
            valIntermed.disabled = true;
            btnvalIntermed.disabled = valIntermed.disabled;
            
            tcnCash.disabled = false;
        }

    }

}
 
//%ChangedEffecdate: Cambio de fecha de efecto
//-----------------------------------------------------------------------------
function ChangedEffecdate(nValue)
//-----------------------------------------------------------------------------
{
	insDefValues('ValCash_dEffecdate','sCodispl=OP002&dEffecdate=' + nValue + '&nPage=OP002','/VTimeNet/CashBank/CashBankSeq');
}
//%ReloadPage: Recarga la página según la opción seleccionada
//--------------------------------------------------------------------------------------------------
function ReloadPage(Field){
//--------------------------------------------------------------------------------------------------
	var lstrLocation=""
	var lintCheck=0;
	var lintCreditCard=0;
	
	
	if(Field!="")
	{
		self.document.forms[0].elements["optToDeposit"][0].disabled=false;
		self.document.forms[0].elements["optToDeposit"][1].disabled=false;

		lintCheck = self.document.forms[0].elements["optToDeposit"][0].value;
		lintToDeposit = self.document.forms[0].elements["optToDeposit"][0].value;
		lintCreditCard = self.document.forms[0].elements["optToDeposit"][1].value;

		lstrLocation += document.location.href;
		lstrLocation = lstrLocation.replace(/&nAccount=.*/, "");
		lstrLocation = lstrLocation.replace(/&nOptionDeposit=.*/, "");
		lstrLocation = lstrLocation.replace(/&dEffecdate=.*/, "");
		lstrLocation = lstrLocation.replace(/&sVoucherNumber=.*/, "");
		lstrLocation = lstrLocation + "&nAccount="+Field;
		if(lintCheck>0 && lintCheck!=2)
			lstrLocation = lstrLocation + "&nOptionDeposit="+lintCheck;

		if(lintCreditCard>0 && lintCheck!=1)
			lstrLocation = lstrLocation + "&nOptionDeposit="+lintCreditCard;
			
		lstrLocation = lstrLocation + "&dEffecdate="+self.document.forms[0].elements["tcdEffecDate"].value + "&dRealEffecdate="+self.document.forms[0].elements["tcd´RealEffecDate"].value;
		lstrLocation = lstrLocation + "&sVoucherNumber="+self.document.forms[0].elements["tctDepositNum"].value;
		document.location.href = lstrLocation;
		
		self.document.forms[0].elements["optToDeposit"][0].disabled=true;
		self.document.forms[0].elements["optToDeposit"][1].disabled=true;
	}
}
	</SCRIPT>
	<META HTTP-EQUIV="Content-Language" CONTENT="es">
	    <%mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("OP002", "OP002.aspx", 1, ""))
	        mobjMenu = Nothing
%>
	    <BR>
</HEAD>
<BODY Class="Header" VLINK=white LINK=white ALINK=white >
<BR>
<FORM METHOD="post" ID="FORM" NAME="frmDeposit" ACTION="ValCashBankSeq.aspx?sMode=1">
    <TABLE WIDTH="100%">    
		<TR>
            <TD WIDTH="15%" CLASS="HIGHLIGHTED"><LABEL ID=40211><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD COLSPAN="2">&nbsp</TD>
            <TD COLSPAN="2" CLASS="HIGHLIGHTED"><LABEL ID=0><%= GetLocalResourceObject("Anchor3Caption")%></LABEL></TD>
        </TR>
        <TR>
            <TD CLASS="HORLINE"></TD>
            <TD COLSPAN="2"></TD>
            <TD COLSPAN="2" CLASS="HORLINE"></TD>
        </TR>    
        <TR>
            <%If Request.QueryString.Item("nOptDeposit") = "1" Or IsNothing(Request.QueryString.Item("nOptDeposit")) Then%>
			    <TD><%=mobjValues.OptionControl(0, "optToDeposit", GetLocalResourceObject("optToDeposit_1Caption"), "1", "1", "ChangeOption(this);", True, 1, GetLocalResourceObject("optToDeposit_1Caption"))%></TD>
			<%Else%>
			    <TD><%=mobjValues.OptionControl(0, "optToDeposit", GetLocalResourceObject("optToDeposit_1Caption"), , "1", "ChangeOption(this);", True, 1)%></TD>                
			<%End If%>
    		<TD><LABEL><%= GetLocalResourceObject("cbeCompanyCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCompany", "company", eFunctions.Values.eValuesType.clngComboType, lintCompany, , , , , , , True, , GetLocalResourceObject("cbeCompanyToolTip"), , 2)%></TD>

            <TD><%=mobjValues.OptionControl(0, "optSelection", GetLocalResourceObject("optSelection_1Caption"), "1", "1", "ChangeSelect(this);", True, 3, GetLocalResourceObject("optSelection_1Caption"))%></TD>
            <TD><%=mobjValues.OptionControl(0, "optSelection", GetLocalResourceObject("optSelection_2Caption"), , "2", "ChangeSelect(this);", True, 4, GetLocalResourceObject("optSelection_2Caption"))%></TD>                

        </TR>

        <TR>
			<%If Request.QueryString.Item("nOptDeposit") = "1" Or IsNothing(Request.QueryString.Item("nOptDeposit")) Then%>
			    <TD><%=mobjValues.OptionControl(100679, "optToDeposit", GetLocalResourceObject("optToDeposit_2Caption"), , "2", "ChangeOption(this);", True, 5, GetLocalResourceObject("optToDeposit_2Caption"))%></TD>       
			<%Else%>
			    <TD><%=mobjValues.OptionControl(100679, "optToDeposit", GetLocalResourceObject("optToDeposit_2Caption"), "1", "2", "ChangeOption(this);", True, 5)%></TD>       
			<%End If%>
            <TD><LABEL><%= GetLocalResourceObject("tcdEffecDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecDate", CStr(Today), , GetLocalResourceObject("tcdEffecDateToolTip"), , , , "ChangedEffecdate(this.value);", True, 6)%></TD>
            
			<TD><LABEL><%= GetLocalResourceObject("tcnCashCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCash", 5, lintCashNum,  , GetLocalResourceObject("tcnCashToolTip"),  ,  ,  ,  ,  ,  , True, 7)%>            
        </TR>

        <TR>
            <TD COLSPAN="1">&nbsp</TD>
			<TD><LABEL><%= GetLocalResourceObject("valAccCashCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("valAccCash", "tabBank_acc_com", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nAccount"),  ,  ,  ,  ,  , "insShowCurrency(this,0);", True, 4, GetLocalResourceObject("valAccCashToolTip"),  , 10)%></TD>
            <TD><LABEL><%= GetLocalResourceObject("tcdRealEffecDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdRealEffecDate", CStr(Today),  , GetLocalResourceObject("tcdRealEffecDateToolTip"),  ,  ,  ,  , True, 8)%></TD>
        </TR>
        <TR>
		    <TD COLSPAN="1">&nbsp</TD>
			<TD><DIV ID="DIVlbllocat"><LABEL><%= GetLocalResourceObject("cbeChequeLocatCaption") %></LABEL></DIV></TD>
			<TD><DIV ID="DIVcbelocat"><%=mobjValues.PossiblesValues("cbeChequeLocat", "Table5553", eFunctions.Values.eValuesType.clngComboType, , , , , , , , True, , GetLocalResourceObject("cbeChequeLocatToolTip"), , 9)%></DIV></TD>
			<TD><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
			<TD><%=mobjValues.DIVControl("lblCurrency",  , "")%></TD> 			
		</TR>	
		<TR>
            <TD COLSPAN="3">&nbsp</TD>
            <TD><LABEL><%= GetLocalResourceObject("tctDepositNumCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctDepositNum", 12, Request.QueryString.Item("sVoucherNumber"), , GetLocalResourceObject("tctDepositNumToolTip"), , , , "insShowCurrency(this,0);", True, 10)%></TD>			            
		</TR>
        <TR>
            <TD COLSPAN="3">&nbsp</TD>
            <TD><LABEL><%= GetLocalResourceObject("valIntermedCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("valIntermed", "Intermedia", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True, 10 , GetLocalResourceObject("valIntermedToolTip"),  , 11)%></TD>	
        </TR>
    </TABLE>
    
</FORM>
</BODY>
</HTML>

<%Response.Write("<SCRIPT>ChangeOption(1);</script>")
If Request.QueryString.Item("sLinkSpecial") = "1" Then
	Response.Write("<SCRIPT>insShowCurrency(self.document.forms[0].valAccCash,1);</SCRIPT>")
End If


mobjValues = Nothing
%>




