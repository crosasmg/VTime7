<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del menu
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "OP752_K"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>

	
<%mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("OP752"))
	.Write(mobjMenu.MakeMenu("OP752", "OP752_K.aspx", 1, ""))
	.Write("<BR>")
End With
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>" & vbCrLf)
%>    
<SCRIPT LANGUAGE="JavaScript">    
//% insStateZone: se maneja el estado de los campos de la página
//-------------------------------------------------------------------------------------------------
function insStateZone(nAction){	
//-------------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		valMoveType.TypeList=1;
		if(nAction==301){
			valMoveType.List=1;
			valMoveType.value=1;
			//UpdateDiv('valMoveTypeDesc','Ingreso');
			insChangeControl(valMoveType, 'MoveType', nAction)			
			ShowDiv('divChebank', 'show');	
		}
		else{
			valMoveType.List="2,3,4,7,8";
			valMoveType.value='';
			ShowDiv('divChebank', 'hide');	
			//UpdateDiv('valMoveTypeDesc','');
		}
		valMoveType.disabled=(nAction==301)?true:false;
		//btnvalMoveType.disabled=valMoveType.disabled;
		tcdDateMove.disabled=false;
		btn_tcdDateMove.disabled=false;
		cbeBank.disabled=false;
		tctChequeNum.disabled=false;
	}
}

//% insCancel: se maneja la acción Cancelar de la transacción
//-------------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------------
	return true;
}
//% insFinish: se maneja la acción Finalizar de la transacción
//-------------------------------------------------------------------------------------------------
function insFinish(){
//-------------------------------------------------------------------------------------------------
    return true;
}
//% insChangeControl: Se controla el cambio de valor de los campos de la página
//-------------------------------------------------------------------------------------------------
function insChangeControl(Field, Option, nAction){
//-------------------------------------------------------------------------------------------------
	var lblnDeposit = (Field.value==7 || Field.value==8)
	with(self.document.forms[0]){
		switch(Option){
			case "MoveType":
				if(!lblnDeposit){
					cbeChequeLocat.value='';
					valBankAccount.value='';
					UpdateDiv('valBankAccountDesc','');
					cbeCurrency.value='';
					tctDep_number.value='';
					tcdExpirdat.value='';
				}
				else
					if(!optTypeDocu[1].checked)
						optTypeDocu[0].checked=true;
				if(Field.value==1 && 
				   nAction != 301)
					valMoveType.value=''
				cbeBank.disabled=lblnDeposit;
				tctChequeNum.disabled=lblnDeposit;
				optTypeDocu[0].disabled=!lblnDeposit;
				optTypeDocu[1].disabled=!lblnDeposit;
				cbeChequeLocat.disabled=!lblnDeposit || optTypeDocu[1].checked;
				valBankAccount.disabled=!lblnDeposit;
				btnvalBankAccount.disabled=!lblnDeposit;
				tctDep_number.disabled=!lblnDeposit;
				tcdExpirdat.disabled=!lblnDeposit;
				btn_tcdExpirdat.disabled=!lblnDeposit;
				if (!lblnDeposit){
				    ShowDiv('divDeposit', 'hide');
				    if (Field.value!=0)
						ShowDiv('divChebank', 'show');
					else
						ShowDiv('divChebank', 'hide');	
				    }
				else{    
				    ShowDiv('divDeposit', 'show');
				    ShowDiv('divChebank', 'hide');
				    }
				break;

			case "TypeDocu":
				cbeChequeLocat.disabled=optTypeDocu[1].checked;
				if(cbeChequeLocat.disabled)
					cbeChequeLocat.value='';
				break;
				
			case "BankAccount":
				cbeCurrency.value=valBankAccount_nCurrency.value;
		}
	}
	
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCheques" ACTION="ValCashBank.aspx?x=1">
	<BR>	
    <TABLE WIDTH="100%">
		<TR>
			<TD WIDTH="20%"><LABEL ID="0"><%= GetLocalResourceObject("valMoveTypeCaption") %></LABEL></TD>
			<TD WIDTH="29%"><%mobjValues.TypeList = 1
mobjValues.List = "1,2,3,4,7,8"
Response.Write(mobjValues.PossiblesValues("valMoveType", "Table5575", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insChangeControl(this,""MoveType"")", True,  , GetLocalResourceObject("valMoveTypeToolTip"),  , 1))
%>
			</TD>
			<TD WIDTH="2%">&nbsp;</TD>
			<TD WIDTH="24%"><LABEL ID="0"><%= GetLocalResourceObject("tcdDateMoveCaption") %></LABEL></TD>
			<TD WIDTH="25%"><%=mobjValues.DateControl("tcdDateMove",  ,  , GetLocalResourceObject("tcdDateMoveToolTip"),  ,  ,  ,  , True, 2)%></TD>
		</TR>
    </TABLE>			
    <DIV ID="divChebank">
		<TABLE WIDTH="100%">    
			<TR>
				<TD WIDTH="20%"><LABEL ID="0"><%= GetLocalResourceObject("cbeBankCaption") %></LABEL></TD>
				<TD WIDTH="29%"><%=mobjValues.PossiblesValues("cbeBank", "Table7", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBankToolTip"),  , 3)%></TD>
				<TD WIDTH="2%">&nbsp;</TD>
				<TD WIDTH="24%"><LABEL ID="0"><%= GetLocalResourceObject("tctChequeNumCaption") %></LABEL></TD>
				<TD WIDTH="25%"><%=mobjValues.TextControl("tctChequeNum", 10, "",  , GetLocalResourceObject("tctChequeNumToolTip"),  ,  ,  ,  , True, 4)%></TD>
			</TR>
		</TABLE>	
	</DIV>
	<DIV ID="divDeposit">
	<TABLE WIDTH="100%">
		<TR>
			<TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="5" CLASS="HorLine"></TD>
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID="0"><%= GetLocalResourceObject("cbeChequeLocatCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeChequeLocat", "Table5553", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeChequeLocatToolTip"))%></TD>
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD></TD>
			<TD COLSPAN="2"></TD>
		</TR>
		<TR>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optTypeDocu", GetLocalResourceObject("optTypeDocu_10Caption"), "1", "10", "insChangeControl(this,""TypeDocu"")", True,  , GetLocalResourceObject("optTypeDocu_10ToolTip"))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID="0"><%= GetLocalResourceObject("valBankAccountCaption") %></LABEL></TD>
			<TD><%mobjValues.Parameters.ReturnValue("nCurrency", False, vbNullString, True)
mobjValues.Parameters.ReturnValue("nBank_code", False, vbNullString, True)
Response.Write(mobjValues.PossiblesValues("valBankAccount", "tabBank_acc_com", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  , "insChangeControl(this,""BankAccount"")", True, 10, GetLocalResourceObject("valBankAccountToolTip")))
%>
			</TD>
		</TR>
		<TR>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optTypeDocu", GetLocalResourceObject("optTypeDocu_5Caption"),  , "5", "insChangeControl(this,""TypeDocu"")", True,  , GetLocalResourceObject("optTypeDocu_5ToolTip"))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID="0"><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
		</TR>
		<TR>
			<TD COLSPAN="2"></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID="0"><%= GetLocalResourceObject("tctDep_numberCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctDep_number", 12,  ,  , GetLocalResourceObject("tctDep_numberToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
			<TD COLSPAN="2"></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID="0"><%= GetLocalResourceObject("tcdExpirdatCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdExpirdat", CStr(Today),  , GetLocalResourceObject("tcdExpirdatToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>
	</TABLE>
	</DIV>
</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>ShowDiv('divDeposit', 'hide');</SCRIPT>")
Response.Write("<SCRIPT>ShowDiv('divChebank', 'hide');</SCRIPT>")
mobjValues = Nothing
%>




