<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del menu
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de las cuentas de caja
Dim mobjCash_acc As eCashBank.Cash_acc
'- Objeto para el manejo de las cuentas bancarias	
Dim mobjBank_acc As eCashBank.Bank_acc
'- Variables auxiliares	
Dim mdblAvailable As Object
Dim mdtmEffecdate As Object
Dim mstrStatregt As String
Dim mintCurrency As Integer
Dim mintOffice As Integer
Dim mintAccType As Integer
Dim mintCompany As Integer
Dim mintMint_Amount As Object
Dim mintnBk_agency As Object

Dim mintnCaja As Object


'%insPreOP004: Esta función se encaga de obtener los datos de la cuenta bancaria y de caja
'--------------------------------------------------------------------------------------------
Private Sub insPreOP004()
	'--------------------------------------------------------------------------------------------
	
	mobjCash_acc = New eCashBank.Cash_acc
	mobjBank_acc = New eCashBank.Bank_acc
	
	'- Se inicializan los valores de las variables auxiliares
	mstrStatregt = ""
	mdtmEffecdate = eRemoteDB.Constants.dtmNull
	mintAccType = 0
	mdblAvailable = eRemoteDB.Constants.intNull
	mintOffice = 0
	mintCurrency = 0
	
	'- Se realiza la lectura de las tablas correspondientes a Cuentas de caja y cuentas bancarias
	
	If mobjValues.StringToType(Session("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble) = 9998 Or mobjValues.StringToType(Session("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble) = 9999 Or mobjValues.StringToType(Session("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble) = 9997 Or mobjValues.StringToType(Session("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble) = 9996 Then
		
		If mobjValues.StringToType(Session("nOffice"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull And mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
			
			If mobjCash_acc.Find(mobjValues.StringToType(Session("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCash_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCompany"), eFunctions.Values.eTypeData.etdDouble, True)) Then
				
				
				
				With mobjCash_acc
					mstrStatregt = .sStatregt
					mdtmEffecdate = .dEffecdate
					mintMint_Amount = .nMin_Amount
					mintnCaja = .ncashnum
					If mobjValues.StringToType(Session("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble) = 9998 Then
						mintAccType = 8
					Else
						mintAccType = 9
					End If
					mdblAvailable = .nAvailable
					mintOffice = mobjValues.StringToType(Session("nOffice"), eFunctions.Values.eTypeData.etdDouble)
					mintCurrency = mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
					Session("nOldOffice") = Session("nOffice")
					Session("nOldCurrency") = Session("nCurrency")
				End With
			End If
		End If
	Else
		If mobjBank_acc.Find_O(mobjValues.StringToType(Session("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble), True) Then
			With mobjBank_acc
				mintAccType = .nAcc_type
				mintOffice = .nOffice
				mintCurrency = .nCurrency
				mstrStatregt = .sStatregt
				mdtmEffecdate = .dEffecdate
				mdblAvailable = .nAvailable
				mintCompany = .nCompany
			End With
		End If
	End If
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "OP004_K"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
    <%=mobjValues.StyleSheet()%>
    <%mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("OP004"))
	.Write(mobjMenu.MakeMenu("OP004", "OP004_K.aspx", 1, ""))
	.Write("<BR><BR><BR>")
	.Write(mobjValues.ShowWindowsName("OP004"))
End With
mobjMenu = Nothing
%>    
	
	<SCRIPT LANGUAGE="JavaScript">    
var mblnValid=true;
//-------------------------------------------------------------------------------------------------
//%	insStatefraBank: Se habilitan o inhabilitan campos del frame de banco
//-------------------------------------------------------------------------------------------------
function insStatefraBank(lblnLocked){
//-------------------------------------------------------------------------------------------------
	with (self.document.forms[0])
	{
		if (!lblnLocked)
		{
			tctAccNumber.value = ''
			cbeBank.value = ''			
			valBk_agency.value = 0
			UpdateDiv("valBk_agencyDesc"," ")
			cbeAvailType.value = 0
			tcnTransit1.value = 0
			tcnTransit2.value = 0
			tcnTransit3.value = 0
			tcnTransit4.value = 0
			tcnTransit5.value = 0
		}
		tctAccNumber.disabled = !lblnLocked		
		cbeBank.disabled = !lblnLocked
		cbeAvailType.disabled = !lblnLocked

		if (top.fraSequence.plngMainAction == 301)
		{
			tcnTransit1.disabled = !lblnLocked
			tcnTransit2.disabled = !lblnLocked
			tcnTransit3.disabled = !lblnLocked
			tcnTransit4.disabled = !lblnLocked
			tcnTransit5.disabled = !lblnLocked
		}
		else
		{
			tcnTransit1.disabled = true
			tcnTransit2.disabled = true
			tcnTransit3.disabled = true
			tcnTransit4.disabled = true
			tcnTransit5.disabled = true
		}
	}	
}
//-------------------------------------------------------------------------------------------------
//%	insStatefraAccount: Se habilitan o inhabilitan campos del frame de contabilidad
//-------------------------------------------------------------------------------------------------
function insStatefraAccount(lblnLocked){
//-------------------------------------------------------------------------------------------------
	with (self.document.forms[0])
	{
		if (!lblnLocked)
		{
	        valLedCompan.value = ''
	        UpdateDiv("valLedCompanDesc"," ")
		    valAccLedger.value = ''
		    UpdateDiv("valAccLedgerDesc"," ")
			valAuxAccount.value = ''
			UpdateDiv("valAuxAccountDesc"," ")			
		}		
		if (chkAccCash.value==true)
		{
	        valLedCompan.disabled = false
	        btnvalLedCompan.disabled = false
        }
	    else
		{
	        valLedCompan.disabled = !lblnLocked
	        btnvalLedCompan.disabled = !lblnLocked
        }
	    
		valAccLedger.disabled = !lblnLocked
		btnvalAccLedger.disabled = !lblnLocked
	}
}

//-------------------------------------------------------------------------------------------------
//%	ShowValues: Se muestran los datos de la cuenta de caja o bancaria
//-------------------------------------------------------------------------------------------------
function ShowValuesAccBankCash(sField){
    //-------------------------------------------------------------------------------------------------
	if (mblnValid)
	{
		if (top.fraSequence.plngMainAction != 301)
		{
 			with(self.document.forms[0])
			{
				switch(sField)
				{
				    case "AccBankCash":

				        if (valAccBankCash.value.replace(" ", "") != '' &&
							valAccBankCash.value.replace(" ", "") != 9998 &&
							valAccBankCash.value.replace(" ", "") != 9999 &&
							valAccBankCash.value.replace(" ", "") != 9997 &&
							valAccBankCash.value.replace(" ", "") != 9996) {
				            insDefValues('AccBankCash', 'nAccBankCash=' + valAccBankCash.value + "&nMainAction=" + top.fraSequence.plngMainAction, '/VTimeNet/CashBank/CashBank/');
				        }
				        else {
				            cbeOffice.disabled = false;
				            cbeCurrency.disabled = false;
				            mblnValid = true;
				        }
				    case "Currency":
				        if (valAccBankCash.value.replace(" ", "") != '' &&
							 cbeOffice.value != 0 &&
							 cbeCurrency.value != 0 &&
							(valAccBankCash.value.replace(" ", "") == 9996 ||
							valAccBankCash.value.replace(" ", "") == 9997 ||
							valAccBankCash.value.replace(" ", "") == 9998 ||
							valAccBankCash.value.replace(" ", "") == 9999))
				            insDefValues('AccBankCash', "nAccBankCash=" + valAccBankCash.value + "&nMainAction=" + top.fraSequence.plngMainAction + "&nOffice=" + cbeOffice.value + "&nCurrency=" + cbeCurrency.value + "&nCashNum=" + tcnCash.value, '/VTimeNet/CashBank/CashBank/');
				}
			}
		}else{
 			with(self.document.forms[0])
			{
				if (valAccBankCash.value == 9998)
					tcnAvailable.disabled = true;

				if (valAccBankCash.value.replace(" ","") == '' || 
					valAccBankCash.value.replace(" ","") == 9998 ||
					valAccBankCash.value.replace(" ","") == 9999 ||
					valAccBankCash.value.replace(" ","") == 9997 ||
					valAccBankCash.value.replace(" ","") == 9996)
					{
						chkAccCash.checked = true;
						chkAccCash.disabled = true;
						document.forms[0].elements['valAccBankCash'].sTabName='tabCash_acc';
                    }
                else
                {
					chkAccCash.checked = false;
					chkAccCash.disabled = false;
                }
            }
		}
	}				
}	
//-------------------------------------------------------------------------------------------------
//%	LockControl: Se habilitan o inhabilitan campos dependiendo del tipo de cuenta (bancaria o caja)
//-------------------------------------------------------------------------------------------------
function LockControl(){
//-------------------------------------------------------------------------------------------------
	
	with (self.document.forms[0])
	{
		if (!valAccBankCash.disabled && valAccBankCash.value.replace(" ","") != '')
		{
		    if (top.fraSequence.plngMainAction == 302)		    
		    {		    
		    	    
		        cbeStatregt.disabled = false;
				cbeAccType.disabled = false;
				cbeOffice.disabled = false;
				tctAccNumber.disabled = false; 					
				cbeBank.disabled = false;
				valBk_agency.disabled = chkAccCash.checked;
				btnvalBk_agency.disabled = valBk_agency.disabled;
				cbeAvailType.disabled = false;
				valLedCompan.disabled = false;
				valAccLedger.disabled = false;
				btnvalLedCompan.disabled=false;
				btnvalAccLedger.disabled=false;
				btnvalAuxAccount.disabled=false;

				if (valAccBankCash.value == 9998 || valAccBankCash.value == 9999
				    || valAccBankCash.value == 9996 || valAccBankCash.value == 9997)
			        tcnAvailable.value=0;	
			        tcnAvailable.disabled=false;		     

				if (valAccBankCash.value == 9998 )
			        tcnAmountMin.disabled=false;
			    else
					tcnAmountMin.disabled=true;
					
				if(chkAccCash.checked)
				   cbeCompany.disabled=true;
				else				   
					cbeCompany.disabled=false;

				if(chkAccCash.checked)
				   tcnCash.disabled=false;
				else				   
					tcnCash.disabled=true;					
					
		    }
		    else

			if (valAccBankCash.value == 9998 )
			    tcnAmountMin.disabled=false;
			else
				tcnAmountMin.disabled=true;

			if(chkAccCash.checked)
			   cbeCompany.disabled=true;
			else				   
				cbeCompany.disabled=false;			

			if(chkAccCash.checked)
			   tcnCash.disabled=false;
			else				   
				tcnCash.disabled=true;
		    
			{
				if (valAccBankCash.value == 9998 || valAccBankCash.value == 9999
				 || valAccBankCash.value == 9996 || valAccBankCash.value == 9997)
 				{ 				
					if (valAccBankCash.value == 9998)
						cbeAccType.value = 8
					
					else
					{	
					    if (valAccBankCash.value == 9997)
							cbeAccType.value = 7
						
						if (valAccBankCash.value == 9996)
							cbeAccType.value = 6
						
						if (valAccBankCash.value == 9999)						
							cbeAccType.value = 9
						
						cbeAccType.disabled = true;
						cbeOffice.disabled = false;						
						cbeCurrency.disabled= false;
						insStatefraBank(false);
						insStatefraAccount(false);
			        }
			        tcnAvailable.value=0;
			        tcnAvailable.disabled=true;
			    }
			    else
			    {
					if (top.fraSequence.plngMainAction == 401 ||
						top.fraSequence.plngMainAction == 303)
					{						
						cbeAccType.disabled = true;
						cbeOffice.disabled = true;
						cbeCurrency.disabled = true;
						cbeCompany.disabled = true;
						insStatefraBank(false);
						insStatefraAccount(false);
					}
					else
					{	
						cbeAccType.disabled = false;
						cbeOffice.disabled = false;
						cbeCurrency.disabled = false
						insStatefraBank(true);
						insStatefraAccount(true);
					}
				}
			}
		}
	}	
}

//-------------------------------------------------------------------------------------------
//%	ChangeValue: Realiza el pase de parámetros para hacer la búsqueda de Cuenta contable o Auxiliar
//-------------------------------------------------------------------------------------------
function ChangeValue(nParamType,nValue){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0])
	{
		switch (nParamType)
		{
			case 1:
			{
				valAccLedger.Parameters.Param1.sValue=nValue;
				valAuxAccount.Parameters.Param2.sValue=nValue;
				break;
			}
			case 2:
			{
				valAuxAccount.Parameters.Param1.sValue=nValue;			
				break;
			}
			case 3:
			{
				valBk_agency.Parameters.Param1.sValue=nValue;
				valBk_agency.disabled = cbeBank.value=='0';
				btnvalBk_agency.disabled = valBk_agency.disabled;
				if (valBk_agency.disabled)
					valBk_agency.value = '';
			}
		}
	}
}

//-------------------------------------------------------------------------------------------------
function insStateZone(){	
//-------------------------------------------------------------------------------------------------
	with (self.document.forms[0])
	{	    
		btnvalAccBankCash.disabled = false;
		valAccBankCash.disabled = false;
		tcdEffecdate.value = '';
		valAccBankCash.value = '';
		UpdateDiv("valAccBankCashDesc","");
		tcnAvailable.value = 0;
		cbeOffice.value = 0;
		cbeCurrency.value = 0;
		cbeCompany.value = 0;
		tcnAmountMin.value= 0;
		tcnCash.value= '';
		cbeAccType.value = 0;
        cbeStatregt.value = 0;        
        cbeStatregt.disabled = true;
		insStatefraBank(false);
		insStatefraAccount(false);
		if (top.fraSequence.plngMainAction != 301)
		{		
			tcdEffecdate.disabled = true;
			btn_tcdEffecdate.disabled = true;
			tcnAvailable.disabled = true;
		}
		else
		{		
			cbeCurrency.disabled= false;
			tcdEffecdate.disabled = false;
			btn_tcdEffecdate.disabled = false;
			tcnAvailable.disabled = false;
			cbeOffice.disabled = false;		
//Si está registrando una cuenta, este campo se encuentra con valor 'En proceso de instalación' e inhabilitado
            cbeStatregt.value = 2;
            cbeStatregt.disabled = true;
		}
	}
}
//-------------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------------
	return true;
}
//-------------------------------------------------------------------------------------------------
function insFinish(){
//-------------------------------------------------------------------------------------------------
    return true;
}

//-------------------------------------------------------------------------------------------------
function ClearField(nValue)
//-------------------------------------------------------------------------------------------------
{
	if((nValue.value==9998 || nValue.value==9999 || nValue.value==9996 || nValue.value==9997 ) && self.document.forms[0].elements["tcnAvailable"].value>0)
		self.document.forms[0].elements["tcnAvailable"].value=0;
}

//-------------------------------------------------------------------------------------------------
function ShowUserCash(Cashnum)
//-------------------------------------------------------------------------------------------------
{
    insDefValues('Cashnum', 'nCashnum=' + Cashnum.value, '/VTimeNet/CashBank/CashBank/')
}

//-------------------------------------------------------------------------------------------------
function insUpdateAcc(bValue){
//-------------------------------------------------------------------------------------------------
	if (bValue)
	{
		document.forms[0].elements['valAccBankCash'].sTabName='tabCash_acc';
		self.document.forms[0].valLedCompan.disabled=false;
		self.document.forms[0].btnvalLedCompan.disabled=false;
	}	
	else
	{
		document.forms[0].elements['valAccBankCash'].sTabName='tabBank_acc';
		self.document.forms[0].valLedCompan.disabled=true;
		self.document.forms[0].btnvalLedCompan.disabled=true;
	}	
}

</SCRIPT>

	
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<BR>
<%Call insPreOP004()%>
<FORM METHOD="post" ID="FORM" NAME="frmAccBankUpd" ACTION="ValCashBank.aspx?x=1">
    <P ALIGN="Center">
		<LABEL ID=40058><A HREF="#Bancos"><%= GetLocalResourceObject("AnchorBancosCaption") %></A></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40060><A HREF="#Relación con contabilidad"> <%= GetLocalResourceObject("AnchorRelación con contabilidadCaption") %></A></LABEL>
    </P>
    <TABLE WIDTH="100%">
		
		<TR>
			<TD><%=mobjValues.CheckControl("chkAccCash", GetLocalResourceObject("chkAccCashCaption"),  , "1", "insUpdateAcc(this.checked);")%></TD>
			<TD COLSPAN="3"></TD>
		</TR>
		
		 <TR>
			<TD><LABEL ID=8542><%= GetLocalResourceObject("valAccBankCashCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valAccBankCash", "tabBank_acc", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(Session("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  , "ClearField(this);ShowValuesAccBankCash(""AccBankCash"");LockControl();", True, 4, GetLocalResourceObject("valAccBankCashToolTip"),  ,  ,  , True)%></TD>
            
            <TD><LABEL ID=8558><%= GetLocalResourceObject("tcnCashCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.NumericControl("tcnCash", 5, mintnCaja,  , GetLocalResourceObject("tcnCashToolTip"),  ,  ,  ,  ,  , "ShowUserCash(this);", True,  ,  , False))
Response.Write(mobjValues.DIVControl("Usercashnum", False, " "))
%>
            </TD>
            
        </TR>
        <TR>
            
            <TD><LABEL ID=8555><%= GetLocalResourceObject("cbeStatregtCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeStatregt", "table26", eFunctions.Values.eValuesType.clngComboType, CStr(mstrStatregt),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStatregtToolTip"), eFunctions.Values.eTypeCode.eString)%></TD>
            <TD><LABEL ID=8552><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdEffecdate", mobjValues.TypeToString(mdtmEffecdate, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
           
       
        </TR>
        </TR>
			 <TD><LABEL ID=8545><%= GetLocalResourceObject("cbeAccTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeAccType", "table190", eFunctions.Values.eValuesType.clngComboType, mobjValues.TypeToString(mintAccType, eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeAccTypeToolTip"))%></TD>
        <TD><LABEL ID=8554><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeOffice", "table9", eFunctions.Values.eValuesType.clngComboType, mobjValues.TypeToString(mintOffice, eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOfficeToolTip"))%></TD>
			
        </TR>
        <TR>
			<TD></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD><LABEL ID=8551><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCurrency", "tabcurrency_b", eFunctions.Values.eValuesType.clngComboType, mobjValues.TypeToString(mintCurrency, eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  , "ShowValuesAccBankCash(""Currency"")", True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
            <TD><LABEL ID=8547><%= GetLocalResourceObject("tcnAvailableCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAvailable", 18, mdblAvailable,  , GetLocalResourceObject("tcnAvailableToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40061><A NAME="Bancos"><%= GetLocalResourceObject("AnchorBancos2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
        </TR>
        
      
			<TD><LABEL ID=8557><%= GetLocalResourceObject("tcnAmountMinCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAmountMin", 18, mintMint_Amount,  , GetLocalResourceObject("tcnAmountMinToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
               <TD><LABEL ID=8549><%= GetLocalResourceObject("cbeBankCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBank", "table7", eFunctions.Values.eValuesType.clngComboType, CStr(mobjBank_acc.nBank_code),  ,  ,  ,  ,  , "ChangeValue(3,this.value)", True,  , GetLocalResourceObject("cbeBankToolTip"))%></TD>
            <%
If mobjValues.StringToType(CStr(mobjBank_acc.nBank_code), eFunctions.Values.eTypeData.etdDouble) > 0 Then
	Session("nBankCode") = mobjBank_acc.nBank_code
Else
	Session("nBankCode") = eRemoteDB.Constants.intNull
End If
%>
        </TR>
        
         <TR>
			<TD><LABEL ID=8544><%= GetLocalResourceObject("cbeCompanyCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCompany", "Company", eFunctions.Values.eValuesType.clngComboType, mobjValues.TypeToString(mintCompany, eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCompanyToolTip"))%></TD>
          	
        </TR>
        
        
        <TR>
			<TD><LABEL ID=8544><%= GetLocalResourceObject("tctAccNumberCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctAccNumber", 25, mobjBank_acc.sAcc_number,  , GetLocalResourceObject("tctAccNumberToolTip"),  ,  ,  ,  , True)%></TD>
          	<TD><LABEL ID=8550><%= GetLocalResourceObject("valBk_agencyCaption") %></LABEL></TD>
			
            <TD><%mobjValues.Parameters.Add("nBank_code", Session("nBankCode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
If mobjBank_acc.nBk_agency = eRemoteDB.Constants.intNull Then
	mintnBk_agency = 0
Else
	mintnBk_agency = mobjBank_acc.nBk_agency
End If
Response.Write(mobjValues.PossiblesValues("valBk_agency", "tabTab_bk_age", eFunctions.Values.eValuesType.clngWindowType, mintnBk_agency, True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valBk_agencyToolTip")))
%></TD>	
        </TR>
        
        <TR>
			<TD><LABEL ID=8548><%= GetLocalResourceObject("cbeAvailTypeCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeAvailType", "table197", eFunctions.Values.eValuesType.clngComboType, CStr(mobjBank_acc.nAvail_type),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeAvailTypeToolTip"))%></TD>
            <TD><LABEL ID=8556><%= GetLocalResourceObject("tcnTransit1Caption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnTransit1", 18, CStr(mobjBank_acc.nTransit_1),  , GetLocalResourceObject("tcnTransit1ToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=8557><%= GetLocalResourceObject("tcnTransit2Caption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnTransit2", 18, CStr(mobjBank_acc.nTransit_2),  , GetLocalResourceObject("tcnTransit2ToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=8558><%= GetLocalResourceObject("tcnTransit3Caption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnTransit3", 18, CStr(mobjBank_acc.nTransit_3),  , GetLocalResourceObject("tcnTransit3ToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=8559><%= GetLocalResourceObject("tcnTransit4Caption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnTransit4", 18, CStr(mobjBank_acc.nTransit_4),  , GetLocalResourceObject("tcnTransit4ToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=8560><%= GetLocalResourceObject("tcnTransit5Caption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnTransit5", 18, CStr(mobjBank_acc.nTransit_5),  , GetLocalResourceObject("tcnTransit5ToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40062><A NAME="Relación con contabilidad"><%= GetLocalResourceObject("AnchorRelación con contabilidad2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
        </TR>
        <TR>
			<TD><LABEL ID=8553><%= GetLocalResourceObject("valLedCompanCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valLedCompan", "TabLed_compan", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjBank_acc.nLed_compan),  ,  ,  ,  ,  , "ChangeValue(1,this.value);", True,  , GetLocalResourceObject("valLedCompanToolTip"))%></TD>
            <TD></TD>
            <TD></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8739><%= GetLocalResourceObject("valAccLedgerCaption") %></LABEL></TD>
            <TD><%With mobjValues
	.Parameters.Add("nLed_compan", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valAccLedger", "tabLedger_acc1", eFunctions.Values.eValuesType.clngWindowType, mobjBank_acc.sAcc_ledger, True,  ,  ,  ,  , "ChangeValue(2,this.value)", True, 20, GetLocalResourceObject("valAccLedgerToolTip"), eFunctions.Values.eTypeCode.eString))
End With
%></TD>
			<TD></TD>
            <TD></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8546><%= GetLocalResourceObject("valAuxAccountCaption") %></LABEL></TD>
			<TD><%With mobjValues
	.Parameters.Add("sAccount", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nLed_compan", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(.PossiblesValues("valAuxAccount", "tabLedger_acc2", eFunctions.Values.eValuesType.clngWindowType, mobjBank_acc.sAux_accoun, True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valAuxAccountToolTip"), eFunctions.Values.eTypeCode.eString))
End With
%></TD>            
			<TD></TD>
			<TD></TD>
        </TR>
        <TR>
            <TD WIDTH=100% COLSPAN=4><%Response.Write(mobjValues.BeginPageButton)%></TD>
        </TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjCash_acc = Nothing
mobjBank_acc = Nothing
Response.Write("<SCRIPT>mblnValid=true</script>")
%>




