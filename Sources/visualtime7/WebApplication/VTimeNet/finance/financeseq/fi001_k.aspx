<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 11.58.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'% LoadHeader: Se cargan los controles de la página cuando se encuentra dentro de la secuencia
'--------------------------------------------------------------------------------------------
Private Sub LoadHeader()
	'--------------------------------------------------------------------------------------------
	Response.Write("<SCRIPT> ntransaction='" & Session("ntransaction") & "'</" & "Script>")
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH = ""100%"">" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD WIDTH=5 ><LABEL>" & GetLocalResourceObject("tcnContrat_HCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""10%"">")


Response.Write(mobjValues.NumericControl("tcnContrat_H", 10, Session("nContrat"),  , GetLocalResourceObject("tcnContrat_HToolTip"),  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""10%"" ALIGN= LEFTH><LABEL>" & GetLocalResourceObject("cbeCurrency_HCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD >")


Response.Write(mobjValues.PossiblesValues("cbeCurrency_H", "table11", 1, Session("nCurrency"),  , True,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeCurrency_HToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN = 2>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD ALIGN= lefth><LABEL>" & GetLocalResourceObject("lblCliename_HCaption") & "</LABEL></TD>	" & vbCrLf)
Response.Write("		<TD >")


Response.Write(mobjValues.TextControl("lblCliename_H", 20, Session("sCliename"),  , GetLocalResourceObject("lblCliename_HToolTip"), True))


Response.Write("</TD> " & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("")

	
	Response.Write("<SCRIPT>insShowNextWindow();</" & "Script>")
End Sub

'% insPreFI001_K: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreFI001_K()
	'--------------------------------------------------------------------------------------------
	Dim lclsOptFinance As eFinance.OptFinance
	Dim lclsUser As eSecurity.User
	Dim lbnsCh_opt_com As Boolean
	lclsOptFinance = New eFinance.OptFinance
	lclsUser = New eSecurity.User
	
	Call lclsOptFinance.Find()
	Call lclsUser.Find(Session("nUsercode"))
	
	Session("nOpt_comm") = lclsOptFinance.nOpt_comm
	Session("sOpt_intere") = lclsOptFinance.sOpt_intere
	
	With lclsOptFinance
		lbnsCh_opt_com = CDbl(.sCh_opt_com) = 2
		
		'+Se almacenan opciones de financiamiento en objeto
		Response.Write("<SCRIPT>insLoadOpt_Financ('" & .nCurrency & "'," & "'" & .ndefaulti & "'," & "'" & .nOpt_comm & "'," & "'" & .sOpt_intere & "'," & "'" & .nDscto_pag & "'," & "'" & .sCh_opt_int & "'," & "'" & lclsUser.nOffice & "');</" & "Script>")
		
		
Response.Write("  " & vbCrLf)
Response.Write("        <BR>" & vbCrLf)
Response.Write("	    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL><A NAME=""Clave"">" & GetLocalResourceObject("AnchorClaveCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    		<TD><LABEL>" & GetLocalResourceObject("cbeTransactioCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD>")

		mobjValues.BlankPosition = False
Response.Write("" & vbCrLf)
Response.Write("	    		    ")


Response.Write(mobjValues.PossiblesValues("cbeTransactio", "table249", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  , "DisabledItems();insInitialValues();",  ,  , GetLocalResourceObject("cbeTransactioToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD></TD>	    		    " & vbCrLf)
Response.Write("	    		<TD><LABEL>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate", CStr(Today()),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "insShowDefValues(""Frequency"");QueryContrat()"))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>	" & vbCrLf)
Response.Write("	    		<TD><LABEL>" & GetLocalResourceObject("tcnContrat_HCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD>")


Response.Write(mobjValues.NumericControl("tcnContrat", 10, "",  , GetLocalResourceObject("tcnContratToolTip"),  ,  ,  ,  ,  , "insShowDefValues(""Contrat"")"))




Response.Write(mobjValues.TextControl("lbl_nContrat_H", 30, "",  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    		<TD></TD>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    	    <TD COLSPAN=""5"">" & vbCrLf)
Response.Write("	    			<TABLE WIDTH=""30%"" ALIGN=""CENTER"">" & vbCrLf)
Response.Write("	    			    <TR>" & vbCrLf)
Response.Write("	    					<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL><A NAME=""Tipo de contrato"">" & GetLocalResourceObject("AnchorTipo de contratoCaption") & "</A></LABEL></TD>	    				" & vbCrLf)
Response.Write("	    				</TR>" & vbCrLf)
Response.Write("	    				<TR>" & vbCrLf)
Response.Write("	    					<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	    				</TR>" & vbCrLf)
Response.Write("	    				<TR>	" & vbCrLf)
Response.Write("	    					<TD>")


Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_1Caption"), CStr(1), "1", "insChangeType(this.value);"))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    					<TD>")


Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_2Caption"), CStr(0), "2", "insChangeType(this.value);"))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    				</TR>" & vbCrLf)
Response.Write("	    			</TABLE>" & vbCrLf)
Response.Write("	    		</TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""5"">&nbsp</TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL><A NAME=""Póliza"">" & GetLocalResourceObject("AnchorPólizaCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD></TD>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL><A NAME=""Cliente"">" & GetLocalResourceObject("AnchorClienteCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	    		<TD></TD>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    		<TD><LABEL>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD>")


Response.Write(mobjValues.NumericControl("tcnPolicy", 10, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "insShowDefValues('Policy');"))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD></TD>" & vbCrLf)
Response.Write("	    		<TD><LABEL>" & GetLocalResourceObject("tctclientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD>")


Response.Write(mobjValues.ClientControl("tctclient", "",  , GetLocalResourceObject("tctclientToolTip"),  ,  , "lblCliename", True,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>	    		" & vbCrLf)
Response.Write("	    		<TD><LABEL>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD>")


Response.Write(mobjValues.DIVControl("lblnBranch",  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    		<TD></TD>" & vbCrLf)
Response.Write("	    		<TD><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD>")


Response.Write(mobjValues.DIVControl("lblCliename",  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>	    		" & vbCrLf)
Response.Write("	    		<TD><LABEL>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD>")


Response.Write(mobjValues.DIVControl("lblnProduct",  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""3""></TD>	    		" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>	    		" & vbCrLf)
Response.Write("	    		<TD><LABEL>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD>")


Response.Write(mobjValues.DIVControl("lbldEffecdate",  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""3""></TD>	    		" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>	    		" & vbCrLf)
Response.Write("	    		<TD><LABEL>" & GetLocalResourceObject("Anchor5Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD>")


Response.Write(mobjValues.DIVControl("lbldExpirdate",  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""3""></TD>	    		" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL><A NAME=""Información del contrato"">" & GetLocalResourceObject("AnchorInformación del contratoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    		<TD><LABEL>" & GetLocalResourceObject("cbeOfficeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD>")


Response.Write(mobjValues.PossiblesValues("cbeOffice", "table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeOfficeToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD></TD>	    		" & vbCrLf)
Response.Write("	    		<TD><LABEL>" & GetLocalResourceObject("cbeCurrency_HCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""2"">")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    	    <TD><LABEL>" & GetLocalResourceObject("tcdLedger_datCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	    <TD>")


Response.Write(mobjValues.DateControl("tcdLedger_dat", CStr(Today()),  , GetLocalResourceObject("tcdLedger_datToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	    <TD></TD>" & vbCrLf)
Response.Write("	            <TD><LABEL>" & GetLocalResourceObject("cbePay_comCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	            <TD>")


Response.Write(mobjValues.PossiblesValues("cbePay_com", "table251", eFunctions.Values.eValuesType.clngComboType, CStr(.nOpt_comm),  ,  ,  ,  ,  ,  , lbnsCh_opt_com, 1, GetLocalResourceObject("cbePay_comToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	        </TR>	    " & vbCrLf)
Response.Write("	        <TR>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL><A NAME=""Datos de la cuota inicial"">" & GetLocalResourceObject("AnchorDatos de la cuota inicialCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    		<TD COLSPAN=""5"" CLASS=""HorLine""></TD>	" & vbCrLf)
Response.Write("	        </TR>" & vbCrLf)
Response.Write("	    	<TR>" & vbCrLf)
Response.Write("	    		<TD><LABEL>" & GetLocalResourceObject("tcnInitialCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    		<TD>")


Response.Write(mobjValues.NumericControl("tcnInitial", 18, "",  , GetLocalResourceObject("tcnInitialToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD></TD>	    		" & vbCrLf)
Response.Write("	    		<TD COLSPAN = ""2"">")


Response.Write(mobjValues.CheckControl("chkPayment_in", GetLocalResourceObject("chkPayment_inCaption"),  , CStr(1),  , .sCh_opt_int <> "1", , GetLocalResourceObject("chkPayment_inToolTip"))) 


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	</TR>" & vbCrLf)
Response.Write("	    </TABLE>" & vbCrLf)
Response.Write("        <TABLE  WIDTH=""100%"">" & vbCrLf)
Response.Write("	        <TR>" & vbCrLf)
Response.Write("	        	<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL><A NAME=""Datos de las cuotas"">" & GetLocalResourceObject("AnchorDatos de las cuotasCaption") & "</A></LABEL></TD>        " & vbCrLf)
Response.Write("	        </TR>" & vbCrLf)
Response.Write("	        <TR>" & vbCrLf)
Response.Write("	        	<TD COLSPAN=""5"" CLASS=""HorLine""></TD>	" & vbCrLf)
Response.Write("	        </TR>" & vbCrLf)
Response.Write("	        <TR>" & vbCrLf)
Response.Write("	            <TD WIDTH=""""><LABEL>" & GetLocalResourceObject("tcnQ_draftCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	            <TD>")


Response.Write(mobjValues.NumericControl("tcnQ_draft", 2, "",  , GetLocalResourceObject("tcnQ_draftToolTip"), True,  ,  ,  ,  , "insShowDefValues(""LastDateDraft"")"))


Response.Write("</TD>" & vbCrLf)
Response.Write("	            <TD WIDTH="""">&nbsp</TD>		        " & vbCrLf)
Response.Write("	            <TD><LABEL>" & GetLocalResourceObject("tcnInterestCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	            <TD>")


Response.Write(mobjValues.NumericControl("tcnInterest", 4, "",  , GetLocalResourceObject("tcnInterestToolTip"),  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("	        </TR>" & vbCrLf)
Response.Write("	        <TR>" & vbCrLf)
Response.Write("	            <TD><LABEL>" & GetLocalResourceObject("cbeFrequencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeFrequency", "table250", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insShowDefValues(""Frequency"")",  , 5, GetLocalResourceObject("cbeFrequencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	            <TD>&nbsp</TD>		        	            " & vbCrLf)
Response.Write("                <TD><LABEL>" & GetLocalResourceObject("tcnBillDayCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD>")


Response.Write(mobjValues.NumericControl("tcnBillDay", 2,  ,  , GetLocalResourceObject("tcnBillDayToolTip"), False,  ,  ,  ,  , "insShowDefValues(""Frequency"")"))


Response.Write("</TD>" & vbCrLf)
Response.Write("	        </TR> " & vbCrLf)
Response.Write("	        <TR>" & vbCrLf)
Response.Write("	            <TD><LABEL>" & GetLocalResourceObject("tcdFirst_drafCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	            <TD>")


Response.Write(mobjValues.DateControl("tcdFirst_draf", "",  , GetLocalResourceObject("tcdFirst_drafToolTip"),  ,  ,  , "insShowDefValues(""LastDateDraft"")"))


Response.Write("" & vbCrLf)
Response.Write("	                ")


Response.Write(mobjValues.HiddenControl("hddFirst_draf", ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("                <TD>&nbsp</TD>		        	            	                    " & vbCrLf)
Response.Write("	            <TD><LABEL>" & GetLocalResourceObject("tcdLast_drafCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	            <TD>")


Response.Write(mobjValues.DateControl("tcdLast_draf", "",  , GetLocalResourceObject("tcdLast_drafToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	        </TR>" & vbCrLf)
Response.Write("	        <TR>" & vbCrLf)
Response.Write("	            <TD><LABEL>" & GetLocalResourceObject("tcnDscto_pagCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	            <TD>")


Response.Write(mobjValues.NumericControl("tcnDscto_pag", 4, "",  , GetLocalResourceObject("tcnDscto_pagToolTip"), , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("                <TD>&nbsp</TD>" & vbCrLf)
Response.Write("	            <TD><LABEL>" & GetLocalResourceObject("cbeWay_payCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	            <TD>")

		mobjValues.BlankPosition = True
		Response.Write(mobjValues.PossiblesValues("cbeWay_pay", "Table5002", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeWay_payToolTip")))
Response.Write("" & vbCrLf)
Response.Write("				</TD>" & vbCrLf)
Response.Write("	        </TR>" & vbCrLf)
Response.Write("	    </TABLE> " & vbCrLf)
Response.Write("	    ")

		
		Response.Write("<SCRIPT>insInitialValues();</" & "Script>")
		
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues.sCodisplPage = "fi001_k"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>

<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 12 $|$$Date: 9/09/04 19:21 $|$$Author: Nvaplat40 $"

//- Variable que contiene la transacción a ejecutar
	var ntransaction  
	var blnDisabled = "";
	var objOpt_Financ = new Object();
	
//% insStateZone: 
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insFinish: Ejecuta la acción de Finalizar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    if (ntransaction != 2){
	    ShowPopUp("/VTimeNet/Finance/FinanceSeq/FI008.aspx?sCodispl=FI008&nAction=392","EndFinancIssue",400,170)
    }
	else{
		top.document.location="/VTimeNet/Common/secWHeader.aspx?sCodispl=FI001&sProject=FinanceSeq&sModule=Finance"
	}
}

//% insCancel: Ejecuta la acción cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	if (ntransaction == 1 || ntransaction == 4){	
		ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=FI001_K","EndProcess",300,150)
	}else{	
	    return true;
	}
}

// insExchange: Se encarga de obtener el factor de cambio al modificar la moneda
//-------------------------------------------------------------------------------------------
function insExchange(){
//-------------------------------------------------------------------------------------------

	var lstrQueryString;
	
	with(self.document.forms[0]){	    
		if (cbeCurrency.value!=0 && insConvertNumber(tcnInitial.value)>0){
			lstrQueryString = "nCurrency="+ cbeCurrency.value + "&dValuedate=" + tcdEffecdate.value + "&nAmount=" + tcnInitial.value + "&sCodispl=FI001_K";
			if (lstrQueryString!='') {
				insDefValues("getExchange", lstrQueryString, '/VTimeNet/Finance/Financeseq');
			}
		}
	}
}

//% Llama a la ventana ShowDefValues 
//-------------------------------------------------------------------------------------------
function insShowDefValues(sField){
//-------------------------------------------------------------------------------------------
	var objForm = self.document.forms[0];
	
	switch(sField)
	{
		case "Contrat":
			DisabledItems2();
			insDefValues("Contrat", "nContrat=" + objForm.tcnContrat.value + 
			                        "&dEffecdate=" + objForm.tcdEffecdate.value + 
			                        "&nTransaction=" + objForm.cbeTransactio.value);
			break; 
		case "Frequency":
			insDefValues("Frequency","nFrequency=" + objForm.cbeFrequency.value +   
			                         "&deffecdate=" + objForm.tcdEffecdate.value + 
			                         "&nTransaction=" + objForm.cbeTransactio.value +
			                         "&nQ_Draft=" + objForm.tcnQ_draft.value + 
			                         "&nBillDay=" + objForm.tcnBillDay.value) ;
			break; 
		case "Dates":
			insDefValues("Dates", "deffecdate=" + objForm.tcdEffecdate.value + 
			                      "&nTransaction=" + objForm.cbeTransactio.value);
			break; 
		case "Policy":
		    if (objForm.tcnPolicy.value !=0)
				insDefValues("Policy", "nPolicy=" + objForm.tcnPolicy.value + 
				                       "&nTransaction=" + objForm.cbeTransactio.value);
			else{
				UpdateDiv('lblnBranch','','Normal');
				UpdateDiv('lblnProduct','','Normal');
				UpdateDiv('lbldEffecdate','','Normal');
				UpdateDiv('lbldExpirdate','','Normal');
				self.document.forms[0].tctclient.value='';
				self.document.forms[0].tctclient_Digit.value='';
				UpdateDiv('lblCliename','','Normal');
				self.document.forms[0].cbeOffice.value = objOpt_Financ.nOffice;
				self.document.forms[0].cbeCurrency.value = objOpt_Financ.nCurrency;
				self.document.forms[0].cbeWay_pay.value = 0;
				self.document.forms[0].tcnBillDay.value = '';				
				self.document.forms[0].cbeCurrency.disabled=false;
				self.document.forms[0].tcnBillDay.disabled=false;
				self.document.forms[0].cbeWay_pay.disabled=false;
			}
			break; 
        case "LastDateDraft":
			insDefValues("LastDateDraft","nFrequency=" + objForm.cbeFrequency.value +   
			                            "&dEffecdate=" + objForm.tcdFirst_draf.value + 
			                            "&nTransaction=" + objForm.cbeTransactio.value +
			                            "&nQ_Draft=" + objForm.tcnQ_draft.value + 
			                            "&nBillDay=" + objForm.tcnBillDay.value) ;
			break; 
            
			
	}
}

//%insLoadOpt_Financ : Carga las opciones de financiamiento en objeto
//-----------------------------------------------------------------------------------------------------------
function insLoadOpt_Financ(nCurrency, nInterest, nOpt_comm, sOpt_intere, nDscto_pag, sCh_opt_int, nOffice){
//-----------------------------------------------------------------------------------------------------------    
    
    objOpt_Financ.nCurrency     = nCurrency;
    objOpt_Financ.nInterest     = nInterest;
    objOpt_Financ.nOpt_comm     = nOpt_comm;
    objOpt_Financ.sOpt_intere   = sOpt_intere;
    objOpt_Financ.nDscto_pag    = nDscto_pag;
    objOpt_Financ.sCh_opt_int   = sCh_opt_int;
    objOpt_Financ.nOffice       = nOffice;
}


// Asigna valores por defecto si la opción seleccionada en la transacción es registro de datos
//-----------------------------------------------------------------------------------------------------------
function insInitialValues(){
//-----------------------------------------------------------------------------------------------------------

    with (self.document.forms[0]){			        
//+ Si selecciono Registro de datos del contrato 
		if (cbeTransactio.value == 1 ){ 
		    cbeCurrency.value = objOpt_Financ.nCurrency;
			tcnInterest.value = objOpt_Financ.nInterest;
			cbeOffice.value   = objOpt_Financ.nOffice;
            cbePay_com.value  = objOpt_Financ.nOpt_comm;

//+Se habilita campo de via de pago		    
			chkPayment_in.checked = (objOpt_Financ.sOpt_intere == 1)
			tcnDscto_pag.value = objOpt_Financ.nDscto_pag; 

            cbeWay_pay.disabled = false;
			
			DisabledItems2();
			insShowDefValues('Dates');
			optType[0].disabled=false;
  			optType[1].disabled=false;
  			tcnPolicy.disabled= false;
  			insChangeType('1');
		}else{
		
//+Se deshabilita campo de via de pago		       
            cbeWay_pay.disabled = true;
		    if (cbeTransactio.value == 3){	
		        insShowDefValues('Dates');
		    }
			
			cbeCurrency.value = 0;
			tcnInterest.value = "";
			cbePay_com.value  = 0;		
			cbeOffice.value   = 0;          
		
			tcnDscto_pag.value = "";
			tcdLedger_dat.value = ""
			if (cbeTransactio.value == 4 )
				tcdEffecdate.value = "";
			else
			    tcdEffecdate.disabled = false;
			    
			optType[0].disabled=true;
			optType[1].disabled=true;
			insChangeType('1');
			tcnPolicy.disabled= true;
		}
		tctclient.value = "";		
		tcnInitial.value = ""; 
		tcnQ_draft.value = ""; 
		tcdFirst_draf.value = ""; 
		tcnContrat.value = "";
		optType[0].checked=1;		
	}
}

//insChangeType: Activa o desactiva los campos al modificar el tipo de contrato
//-------------------------------------------------------------------------------------------
function insChangeType(nValue){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tcnPolicy.value = "";
		UpdateDiv('lblnBranch','','Normal');
		UpdateDiv('lblnProduct','','Normal');
		UpdateDiv('lbldEffecdate','','Normal');
		UpdateDiv('lbldExpirdate','','Normal');			
		UpdateDiv('lblCliename','','Normal');
		tctclient.value = "";
  		tctclient_Digit.value = "";
  		cbeOffice.value = objOpt_Financ.nOffice;
		cbeCurrency.value = objOpt_Financ.nCurrency;
		cbeWay_pay.value = 0;
		tcnBillDay.value = '';        
        cbeCurrency.disabled=false;
        tcnBillDay.disabled=false;
        cbeWay_pay.disabled=false;

		if (nValue == "1"){
			tcnPolicy.disabled = false;
			tcnPolicy.focus();
			tctclient.disabled = true;
  		    tctclient_Digit.disabled = true;
		}	
		else{
  			tcnPolicy.disabled = true;
			tctclient.disabled = false;
  			tctclient_Digit.disabled = false;
			tctclient.focus();
		}	
		
    }
}    

//QueryContrat: Verifica si la está en modo consulta para deshabilitar los campos
//-------------------------------------------------------------------------------------------
function QueryContrat()
//-------------------------------------------------------------------------------------------
{
	with (self.document.forms[0])
	{
		if (cbeTransactio.value == '2'){
			tcnContrat.value = "";
			DisabledItems();
		}	
    }
}    

//Inhabilita los campos si la transacción no corresponsde a Registro de datos
//-------------------------------------------------------------------------------------------
function DisabledItems()
//-------------------------------------------------------------------------------------------
{
	with (self.document.forms[0]){			
		cbeOffice.disabled = true;
		cbeCurrency.disabled = true;
		tctclient.disabled = true;
		tctclient_Digit.disabled = true;
		tcnInterest.disabled = true;
		tcdLedger_dat.disabled = true; 
		tcnInitial.disabled = true; 
		chkPayment_in.disabled = true; 
		tcnQ_draft.disabled = true; 
		cbeFrequency.disabled = true; 
		tcdFirst_draf.disabled = true; 
		tcnDscto_pag.disabled = true;
		btn_tcdFirst_draf.disabled = true;
		btn_tcdLedger_dat.disabled = true;
		cbeWay_pay.disabled = true;
		
		cbeWay_pay.value = '';
		cbeOffice.value = 0;
		cbeCurrency.value = 0;
		tctclient.value = "";
		tctclient_Digit.value = "";
		UpdateDiv('tctclient_Name', '');
		tcnInterest.value = "";
		tcdLedger_dat.value = ""; 
		cbePay_com.value = 0;
		tcnInitial.value = ""; 
		chkPayment_in.checked = false; 
		tcnQ_draft.value = ""; 
		tcdFirst_draf.value = ""; 
		tcnDscto_pag.value = "";
        tcnBillDay.value = "";
	}
}
//% DisabledItems2: Deshabilita los campos de la ventana
//-------------------------------------------------------------------------------------------
function DisabledItems2()
//-------------------------------------------------------------------------------------------
{
	with (self.document.forms[0])
	{	
		if ((tcdEffecdate.value != "" && 
		     tcnContrat.value != "" && 
		     cbeTransactio.value != 2) || 
		    (cbeTransactio.value == 1) ||
		    (cbeTransactio.value == 4))
		{	
			cbeOffice.disabled = false;
			cbeCurrency.disabled = false;
			tctclient.disabled = false;
			tctclient_Digit.disabled = false;
			tcnInterest.disabled = false;
			tcdLedger_dat.disabled = false; 
			tcnInitial.disabled = false; 
			tcnQ_draft.disabled = false; 
			cbeFrequency.disabled = false; 
			tcdFirst_draf.disabled = false; 
			tcnDscto_pag.disabled = false;
			btn_tcdFirst_draf.disabled = false;
			btn_tcdLedger_dat.disabled = false;
	        tcnBillDay.disabled = false;

            if(blnDisabled=="False"){
               cbePay_com.disabled = false;
            }
		}else{
		    if(cbeTransactio.value == 2){
	            tcnBillDay.disabled=true;
                cbePay_com.disabled=true;	                   
		    }
		}
	}
}

//%insShowNextWindow: Se encarga de mostrar la siguiente ventana a ser mostrada
//--------------------------------------------------------------------------------------------
function insShowNextWindow(){
//--------------------------------------------------------------------------------------------
	var lblnDoIt=true;
	if (typeof(top.frames['fraSequence'])!='undefined')
	    if (typeof(top.frames['fraSequence'].NextWindows)!='undefined'){
			top.frames['fraSequence'].NextWindows('');
			lblnDoIt = false;
	    }
	if (lblnDoIt) setTimeOut('insShowNextWindow()',50)
}
</SCRIPT>



	
    <%mobjMenu = New eFunctions.Menues
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("FI001_K", "FI001_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmFinanceProcess" ACTION="valFinanceSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write("<BR>")
Session("AmountInitial") = 0
Session("nAmountQDraft") = 0
Session("nAuxAmount_fi") = ""
Session("nAuxInterest") = ""
Session("nAuxQ_draft") = ""
Session("nAuxFrequency") = ""

If Request.QueryString.Item("sConfig") = "InSequence" Then
	'Call LoadHeader()
Else
	Call insPreFI001_K()
End If
%>

</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 11.58.23
Call mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>





