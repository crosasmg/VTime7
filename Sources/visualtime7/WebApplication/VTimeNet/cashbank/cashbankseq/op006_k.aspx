<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mclsCheques As eCashBank.Cheque
Dim mstrCodispl As String
Dim mobjMenu As eFunctions.Menues
Dim mblnDisabled As Object
Dim mstrAccountValue As Object
Dim mstrLedCompan As Object
Dim mstrQueryString As String
Dim mdtmEffecdate As Date
Dim llngOffice As Object
Dim llngOfficeAgen As Object
Dim llngAgency As Object
Dim lclsgeneral As eGeneral.Users
Dim tctDescriptOP063 As String
Dim tctOperacion As String
Dim mblnTypeSupport As Boolean
Dim nAmount_exe As Object
Dim nAmount_afe As Object

Dim lstrBenef As Object


'% insReaOP006: Se buscan los datos a mostrar en la página
'--------------------------------------------------------------------------------------------
Private Sub insReaOP006()
	'--------------------------------------------------------------------------------------------
	Dim lcolOP006 As eCashBank.Cheques
	Dim lclsOP006 As eCashBank.Cheque
	Dim lintCount As Integer
	Dim lobjValues As eFunctions.Values
	Dim ldtmEffecdate As Object
	Dim lintAcc_bank As Object
	Dim lstrCheque As Object
	
	lobjValues = New eFunctions.Values
	
	lcolOP006 = New eCashBank.Cheques
	lclsOP006 = New eCashBank.Cheque
	
	With lclsOP006
		.nRequest_nu = lobjValues.StringToType(Request.QueryString.Item("nRequest_nu"), eFunctions.Values.eTypeData.etdDouble, True)
		.sCheque = Request.QueryString.Item("sCheque")
		.nCompany = lobjValues.StringToType(Request.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdDouble, True)
		.nConcept = lobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdDouble, True)
		.sDescript = Request.QueryString.Item("sDescript")
		.nCurrencyOri = lobjValues.StringToType(Request.QueryString.Item("nCurrencyOri"), eFunctions.Values.eTypeData.etdDouble, True)
		.nAmount = lobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True)
		.nOffice = lobjValues.StringToType(Request.QueryString.Item("nOffice"), eFunctions.Values.eTypeData.etdDouble, True)
		.nOfficeAgen = lobjValues.StringToType(Request.QueryString.Item("nOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True)
		.nAgency = lobjValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble, True)
		.nCurrencyPay = lobjValues.StringToType(Request.QueryString.Item("nCurrencyPay"), eFunctions.Values.eTypeData.etdDouble, True)
		.nAmountpay = lobjValues.StringToType(Request.QueryString.Item("nAmountpay"), eFunctions.Values.eTypeData.etdDouble, True)
		.nTypesupport = lobjValues.StringToType(Request.QueryString.Item("nTypesupport"), eFunctions.Values.eTypeData.etdDouble, True)
		.nDocSupport = lobjValues.StringToType(Request.QueryString.Item("nDocSupport"), eFunctions.Values.eTypeData.etdDouble, True)
		.nTax_code = lobjValues.StringToType(Request.QueryString.Item("nTax_code"), eFunctions.Values.eTypeData.etdDouble, True)
		.nTax_percent = lobjValues.StringToType(Request.QueryString.Item("nTax_percent"), eFunctions.Values.eTypeData.etdDouble, True)
		.nTax_amount = lobjValues.StringToType(Request.QueryString.Item("nTax_amount"), eFunctions.Values.eTypeData.etdDouble, True)
		.nAfect = lobjValues.StringToType(Request.QueryString.Item("nAfect"), eFunctions.Values.eTypeData.etdDouble, True)
		.nExcent = lobjValues.StringToType(Request.QueryString.Item("nExcent"), eFunctions.Values.eTypeData.etdDouble, True)
		.sClient = Request.QueryString.Item("sClient")
		.dDat_propos = lobjValues.StringToType(Request.QueryString.Item("dDat_propos"), eFunctions.Values.eTypeData.etdDate)
		.dLedger_dat = lobjValues.StringToType(Request.QueryString.Item("dLedger_dat"), eFunctions.Values.eTypeData.etdDate)
		.nUser_sol = lobjValues.StringToType(Request.QueryString.Item("nUser_sol"), eFunctions.Values.eTypeData.etdDouble, True)
		.sRequest_ty = Request.QueryString.Item("sRequest_ty")
		.dIssue_dat = lobjValues.StringToType(Request.QueryString.Item("dIssue_dat"), eFunctions.Values.eTypeData.etdDate)
		.nBranch = lobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True)
		.nBranch_Led = lobjValues.StringToType(Request.QueryString.Item("nBranch_Led"), eFunctions.Values.eTypeData.etdDouble, True)
		.nProduct = lobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True)
		.nPolicy = lobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True)
	End With
	
	With lcolOP006
		If .FindOP006(lclsOP006) Then
			For lintCount = 1 To .Count
                    '"""" & lobjValues.TypeToString(.Item(lintCount).nAmountpay,eFunctions.Values.eTypeData.etdDouble) & """," &                 
                    Response.Write("<SCRIPT>" & _
                        "insAddOP006(""" & lobjValues.StringToType(.Item(lintCount).nRequest_nu, eFunctions.Values.eTypeData.etdDouble) & """," & _
                        """" & .Item(lintCount).sCheque & """," & _
                        """" & lobjValues.StringToType(.Item(lintCount).nCompany, eFunctions.Values.eTypeData.etdDouble) & """," & _
                        """" & lobjValues.StringToType(.Item(lintCount).nConcept, eFunctions.Values.eTypeData.etdDouble) & """," & _
                        """" & .Item(lintCount).sDescript & """," & _
                        """" & lobjValues.TypeToString(.Item(lintCount).nCurrencyOri, eFunctions.Values.eTypeData.etdDouble) & """," & _
            "VTFormat('" & lobjValues.TypeToString(.Item(lintCount).nAmount, eFunctions.Values.eTypeData.etdDouble) & "', '', '', '', 2)" & "," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nOffice, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nOfficeAgen, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nAgency, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & .Item(lintCount).sDesOffice & """," & _
            """" & .Item(lintCount).sDesOfficeAgen & """," & _
            """" & .Item(lintCount).sDesAgency & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nCurrencyPay, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nAmount_Local, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nTypesupport, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nDocSupport, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nTax_code, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nTax_Percent, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nTax_Amount, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nAfect, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nExcent, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & .Item(lintCount).sClient & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).dDat_propos, eFunctions.Values.eTypeData.etdDate) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).dLedger_dat, eFunctions.Values.eTypeData.etdDate) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nUser_sol, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & .Item(lintCount).sRequest_ty & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).dIssue_Dat, eFunctions.Values.eTypeData.etdDate) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nAmountPay, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nOffice, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nBranch, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nBranch_Led, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nProduct, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & lobjValues.TypeToString(.Item(lintCount).nPolicy, eFunctions.Values.eTypeData.etdDouble) & """," & _
            """" & .Item(lintCount).sClient_Digit & """" & _
                        ");</" & "Script>")
                         
			Next 
                'Response.Write("<SCRIPT> mlngCurrentIndex = 0;ShowFields(" & lintCount - 1 & ");</" & "Script>")
                Response.Write("<SCRIPT> mlngCurrentIndex = 0;ShowFields(0);</" & "Script>")
		End If
	End With
	
	lcolOP006 = Nothing
	lclsOP006 = Nothing
	lobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

Session("OP006_nDoc_type") = Request.QueryString.Item("nTypesupport")

mstrCodispl = Trim(Request.QueryString.Item("sCodispl"))

mobjValues.sCodisplPage = mstrCodispl

mdtmEffecdate = mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)

If mdtmEffecdate = eRemoteDB.Constants.dtmNull Then
	mdtmEffecdate = Today
End If

If CStr(Session("OP006_nLoanType")) = "1" Then
	tctOperacion = "Préstamo"
ElseIf CStr(Session("OP006_nLoanType")) = "2" Then 
	tctOperacion = "Anticipo"
End If

If mstrCodispl = "OP06-3" Then
	tctDescriptOP063 = tctOperacion & " - " & "Monto : " & Session("OP006_nAmountPay") & " - " & "Intermediario : " & Session("valIntermedia")
End If

If CStr(Session("nOffice")) = vbNullString Then
	lclsgeneral = New eGeneral.Users
	If lclsgeneral.Find(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
		Session("nOffice") = lclsgeneral.nOffice
	End If
	lclsgeneral = Nothing
End If

mclsCheques = New eCashBank.Cheque

Response.Write("<script>var nOffice=" & Session("nOffice") & "</script>")

If Request.QueryString.Item("sCodispl") <> "OP06-1" Or (Request.QueryString.Item("sCodispl") = "OP06-1" And (Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = "402")) Then
	lstrBenef = Request.QueryString.Item("sBenef")
	If lstrBenef = vbNullString Then
		lstrBenef = Session("OP006_sBenef")
	End If
	
	Call mclsCheques.insPreOP006(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("OP006_nPayOrderTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("OP006_nCurrency"), eFunctions.Values.eTypeData.etdDouble), lstrBenef, mobjValues.StringToType(Session("OP006_nConcept"), eFunctions.Values.eTypeData.etdDouble), CStr(eRemoteDB.Constants.strNull), mobjValues.StringToType(Session("OP006_dReqDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("OP006_nAmountPay"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull)
End If
%>
<HTML>
<HEAD>
<SCRIPT>

 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 15/02/06 19:04 $|$$Author: Clobos $"
    
    var mArray = new Array(0)
    var mlngCurrentIndex = -1
    var mstrInSubmit = "2"    
    var mstrCodispl = <%="""" & Trim(Request.QueryString.Item("sCodispl")) & """"%>

//%    insAddOP006: Carga el arreglo
//-------------------------------------------------------------------------------------------
function insAddOP006(nRequest_nu , sCheque,
                     nCompany    , nConcept,
                     sDescript   , nCurrencyOri,
                     nAmount     , nOffice,
                     nOfficeAgen , nAgency,
                     sDesOffice  , sDesOfficeAgen,
                     sDesAgency  , nCurrencyPay , 
                     nAmountpay  , nTypesupport , 
                     nDocSupport , nTax_code , 
                     nTax_percent, nTax_amount , 
                     nAfect      , nExcent , 
                     sInter_pay  , dDat_propos , 
                     dLedger_dat , nUser_sol , 
                     sRequest_ty , dIssue_dat , 
                     nTotalPay   , nOfficePay, 
                     nBranch     , nBranch_Led, 
                     nProduct    , nPolicy, 
                     sDigit){
//-------------------------------------------------------------------------------------------

    var ludtOP006 = new Array(35)

    ludtOP006[0]  = nRequest_nu;
    ludtOP006[1]  = sCheque;
    ludtOP006[2]  = nCompany;
    ludtOP006[3]  = nConcept;
    ludtOP006[4]  = sDescript;
    ludtOP006[5]  = nCurrencyOri;
    ludtOP006[6]  = nAmount;
    ludtOP006[7]  = nOffice;
    ludtOP006[8]  = nCurrencyPay;
    ludtOP006[9]  = nAmountpay;
    ludtOP006[10] = nTypesupport;
    ludtOP006[11] = nDocSupport;
    ludtOP006[12] = nTax_code;
    ludtOP006[13] = nTax_percent;
    ludtOP006[14] = nTax_amount;
    ludtOP006[15] = nAfect;
    ludtOP006[16] = nExcent;
    ludtOP006[17] = sInter_pay;
    ludtOP006[18] = dDat_propos;
    ludtOP006[19] = dLedger_dat;
    ludtOP006[20] = nUser_sol;
    ludtOP006[21] = sRequest_ty;
    ludtOP006[22] = dIssue_dat;
    ludtOP006[23] = nTotalPay;
    ludtOP006[24] = nOfficePay;
    ludtOP006[25] = nBranch;
    ludtOP006[26] = nBranch_Led;
    ludtOP006[27] = nProduct;
    ludtOP006[28] = nPolicy;
    ludtOP006[29] = sDigit;
    ludtOP006[30] = nOfficeAgen;
    ludtOP006[31] = nAgency;    
    ludtOP006[32] = sDesOffice;
    ludtOP006[33] = sDesOfficeAgen;
    ludtOP006[34] = sDesAgency;
    
    
    mArray[++mlngCurrentIndex] = ludtOP006;
}

//%    ShowFields:Carga los Registros en el Arreglo
//-------------------------------------------------------------------------------------------
function ShowFields(lintIndex){
//-------------------------------------------------------------------------------------------
    with (document.forms[0]){    
        elements["tcnRequestNu"].value = mArray[lintIndex][0];
        elements["tctChequeNum"].value = mArray[lintIndex][1];
        elements["cbeCompany"].value = mArray[lintIndex][2]; 
        elements["valConcept"].value = mArray[lintIndex][3];
        elements["tctDescript"].value = mArray[lintIndex][4];
        elements["cbeCurrency"].value = mArray[lintIndex][5];
        elements["tcnAmount"].value = mArray[lintIndex][6];
        elements["cbeOffice"].value = mArray[lintIndex][7];
        elements["cbeCurrencyPay"].value = mArray[lintIndex][8];
        elements["tcnAmountpay"].value = mArray[lintIndex][9]; 
        elements["cbeTypeSupport"].value = mArray[lintIndex][10];
        elements["tcnDoc_support"].value = mArray[lintIndex][11] ;
        cbeTax_code.Parameters.Param1.sValue = mArray[lintIndex][10];        
        elements["cbeTax_code"].value = mArray[lintIndex][12];        
        elements["tcnPercent"].value = mArray[lintIndex][13];
        elements["tcnTax_amount"].value = mArray[lintIndex][14];
        elements["tcnAfect"].value = mArray[lintIndex][15];
        elements["tcnExcent"].value = mArray[lintIndex][16];
        elements["dtcBenef"].value = mArray[lintIndex][17];
        elements["tcdReqDate"].value = mArray[lintIndex][18];
        elements["tcdAccDate"].value = mArray[lintIndex][19];
        elements["valReqUser"].value = mArray[lintIndex][20];
        elements["cbePayOrderTyp"].value = mArray[lintIndex][21];
        elements["tcdChequeDate"].value = mArray[lintIndex][22];
        elements["tcnAmounttotal"].value = mArray[lintIndex][23];
        elements["cbeBranch"].value = mArray[lintIndex][25];
        elements["valBranch_Led"].value = mArray[lintIndex][26];
        elements["valProduct"].value = mArray[lintIndex][27];
        elements["tcnPolicy"].value = mArray[lintIndex][28];
        elements["dtcBenef_Digit"].value = mArray[lintIndex][29];
        elements["cbeOfficeAgen"].value = mArray[lintIndex][30];
        elements["cbeAgency"].value = mArray[lintIndex][31];
        
        UpdateDiv('cbeOfficeDesc',mArray[lintIndex][32],'Normal');
        UpdateDiv('cbeOfficeAgenDesc',mArray[lintIndex][33],'Normal');
        UpdateDiv('cbeAgencyDesc',mArray[lintIndex][34],'Normal');
        
        
    }
    ShowPopUp('/VTimeNet/CashBank/CashBankSeq/ShowDefValues.aspx?Field=ActivateOnBlur1'  + '&nBranch=' +  document.forms[0].elements['cbeBranch'].value, 'ShowDefValues', 1, 1,'no','no',2000,2000);
}

//%    ClearFields: Borra los Datos de la Ventana
//-------------------------------------------------------------------------------------------
function ClearFields(){
//-------------------------------------------------------------------------------------------
    with (document.forms[0]){
        tcdReqDate.value = GetDateSystem() ;
        cbePayOrderTyp.value = "" ;
        tcnRequestNu.value = "" ;
        tctChequeNum.value = "" ;
        valConcept.value = "" ;
        tctDescript.value = "" ;
        cbeCurrency.value = "" ;
        tcnAmount.value = "" ;
        cbeCurrency.value = "" ;
        cbeCurrencyPay.value = "" ;
        tcnAmountpay.value = "" ;
        cbeTypeSupport.value = "" ;
        tcnDoc_support.value = "" ;
        cbeTax_code.value = "" ;
        tcnPercent.value = "" ;
        tcnTax_amount.value = "" ;
        tcnAfect.value = "" ;
        tcnExcent.value = "" ;
        tcnAmounttotal.value = "" ;
        dtcBenef.value = "" ;
        dtcBenef_Digit.value = "" ;
        tcdChequeDate.value = "" ;
        tcdAccDate.value = "" ;
        cbeBranch.value= "";
        valBranch_Led.value= "";
        valProduct.value= "";
        tcnPolicy.value= "" ;
        valReqUser.value = <%=Session("nUsercode")%>;
        
        if(top.frames["fraSequence"].plngMainAction == 401 || top.frames["fraSequence"].plngMainAction == 402)
            valReqUser.value = "";
        
        UpdateDiv('valProductDesc','','Normal');
        UpdateDiv('valBranch_LedDesc','','Normal');
        UpdateDiv('lblBenefname','','Normal');        
        
        elements["valConcept"].onblur()
        
        elements["cbeTax_code"].onblur()
        elements["valReqUser"].onblur()
        elements["valProduct"].onblur()
        elements["valBranch_Led"].onblur()
   }
}
//%    inshowCurrency: Cuando hay cambio de moneda origen se realiza la actualización de los datos 
//--------------------------------------------------------------------------------------------------
function inshowCurrency(sField){ 
//--------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){                           
        if (cbeCurrency.value!=0 && tcnAmount.value > '0,00' && cbeCurrencyPay.value>0 ) {          
            insShowconver();
			}
        else{
            tcnAmountpay.value='';
            tcnExcent.value = '';
            tcnAfect.value = '';
       }
    }  
} 

//%    InsValidDesc: Valida que la descripción no supere los 650 caracteres. 
//--------------------------------------------------------------------------------------------------
function InsValidDesc(sValue){ 
//--------------------------------------------------------------------------------------------------
	if (sValue.length>650){ 
	   alert('El campo descripción debe tener un máximo de 650 caracteres');
	   return(false);
	   }
	else   
	    return(true);	
} 



//%    insShowCalcAmountT: Realiza la suma de los impuestos
//-------------------------------------------------------------------------------------------
function insShowCalcAmountT(){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        ShowPopUp("/VTimeNet/CashBank/CashBankSeq/ShowDefValues.aspx?Field=CalTotalAmount" + "&nTax_amount=" + tcnTax_amount.value + "&nAfect=" + tcnAfect.value + "&nExcent=" + tcnExcent.value  + "&dEffecdate=" + tcdReqDate.value + "&nCode=" + cbeTax_code.value, "ShowDefValues", 1, 1,"no","no",2000,2000);        
    }       
}


//%    insShowconver: Cuando se cambia  el monto origen
//-------------------------------------------------------------------------------------------
function insShowconver(){
//-------------------------------------------------------------------------------------------
    var lstrCodispl =  <%="'" & Session("OP006_sCodispl") & "'"%>  
    var ldtmDateIncrease
    with (self.document.forms[0]){    
        ldtmDateIncrease = (lstrCodispl!='CO009' && lstrCodispl!='OP091')?self.document.forms[0].tcdReqDate.value:'<%=mobjValues.StringToType(Request.QueryString.Item("dDateIncrease"), eFunctions.Values.eTypeData.etdDate)%>';     
        if (cbeCurrencyPay.value!=0){           
            insDefValues("ConvertAmount" , "nCurrency_targ=" + cbeCurrencyPay.value + "&nAmount=" + tcnAmount.value + "&nCurrency=" + cbeCurrency.value + "&dReqDate=" + ldtmDateIncrease + "&nTypeSupport=" + cbeTypeSupport.value + "&nCode=" + cbeTax_code.value  + "&dEffecdate=" + tcdReqDate.value, "/VTimeNet/CashBank/CashBankSeq");             
            }        
        //if (cbeTax_code.value!=0){
        //    ShowPopUp("/VTimeNet/CashBank/CashBankSeq/ShowDefValues.aspx?Field=Tax_amount" + "&nCode=" + cbeTax_code.value + "&nAmount=" + tcnAmountpay.value + "&nAfect=" + tcnAfect.value + "&nExcent=" + tcnExcent.value + "&nTypeSupport=" + cbeTypeSupport.value  + "&dEffecdate=" + tcdReqDate.value, "ShowDefValues", 1, 1,"no","no",2000,2000);
        //    }
   }
}


//%    insShowPercent: Muestra el porcentaje seleccionado en nPercent
//-------------------------------------------------------------------------------------------
function insShowPercent(){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if (cbeTax_code.value!=0) {
            insDefValues("Tax_amount" , "nCode=" + cbeTax_code.value + "&nAmount=" + tcnAmountpay.value + "&nAfect=" + tcnAfect.value + "&nExcent=" + tcnExcent.value  + "&dEffecdate=" + tcdReqDate.value, "/VTimeNet/CashBank/CashBankSeq");         
        }
    }
}

//%    insConvertAmount: Realiza la conversión a moneda
//-------------------------------------------------------------------------------------------
function insConvertAmount(Field){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){        
        insDefValues("ConvertAmount" , "nCurrency_targ=" + cbeCurrencyPay.value + "&nAmount=" + tcnAmount.value + "&nCurrency=" + cbeCurrency.value + "&dReqDate=" + tcdReqDate.value + "&nTypeSupport=" + cbeTypeSupport.value + "&nCode=" + cbeTax_code.value, "/VTimeNet/CashBank/CashBankSeq");
    }
}

//% insProponum: Realiza la busqueda del monto y la moneda
//-------------------------------------------------------------------------------------------
function insProponum(Field){
//-------------------------------------------------------------------------------------------
	<%If Request.QueryString.Item("sCodispl") = "OP06-2" Then%>
	with (self.document.forms[0]){ 	   
	   insDefValues("nProponum" , "nProponum=" + Field + "&dEffecdate=" + tcdReqDate.value, "/VTimeNet/CashBank/CashBankSeq");       
	}
	<%Else%>
	with (self.document.forms[0]){ 	   
	   insDefValues("nProponum" , "nProponum=" + Field.value + "&dEffecdate=" + tcdReqDate.value, "/VTimeNet/CashBank/CashBankSeq");       
	}
	<%End If%>
}

//%insParamFixval: Se calcula el impuesto de acuerdo al tipo de documento asociado al proveedor
//---------------------------------------------------------------------------------------------
function insParamFixval(){
//---------------------------------------------------------------------------------------------    
    with (self.document.forms[0]){    
        if (cbeTypeSupport.value!=0){
            insDefValues("Tax_FixVal", "nTypeSupport=" + cbeTypeSupport.value + "&dEffecdate=" + tcdReqDate.value);
        }    
        else {        
            cbeTax_code.value="";
            cbeTax_code.disabled=true;
               btncbeTax_code.disabled=true;
        }
        if (cbeTypeSupport.value==1){   // Monto Afecto
            tcnAfect.disabled=false;
            tcnExcent.value=0;
            tcnExcent.disabled=true;
            tcnAfect.value = tcnAmountpay.value;
            }
        else
            if (cbeTypeSupport.value==2){ // Monto excento
            tcnExcent.disabled=false;
            tcnAfect.value=0;  
            tcnAfect.disabled=true;
            tcnTax_amount.value = 0;
            tcnExcent.value = tcnAmountpay.value;
            tcnAmounttotal.value = tcnAmountpay.value;
            }
            else 
                 if (cbeTypeSupport.value==3 || cbeTypeSupport.value==5){
                    tcnAfect.value = tcnAmountpay.value;
                    tcnExcent.value = 0;
                 }
                 else {
                	tcnAfect.disabled=true;
					tcnAfect.value='';
					tcnExcent.value='';
					   tcnExcent.disabled=true;
					   tcnPercent.value='';
					   tcnPercent.disabled=true;
					   tcnAmounttotal.value='';
					   tcnTax_amount.value='';
					   tcnExcent.value = tcnAmountpay.value;
					   tcnAmounttotal.value = tcnAmountpay.value;
					   UpdateDiv('cbeTax_codeDesc','','Normal');
               }
    }
}

//%cbePayOrderTypChange: Se habilitan y deshabilitan campos según el tipo de Orden de Pago 
//--------------------------------------------------------------------------------------------
function setDivTransfer(){
//--------------------------------------------------------------------------------------------
   
    with (self.document.forms[0]){
        if (cbePayOrderTyp.value == "5")
            ShowDiv('DivTransfer', 'show');
        else ShowDiv('DivTransfer', 'hide');
    }
}
//%cbePayOrderTypChange: Se habilitan y deshabilitan campos según el tipo de Orden de Pago 
//--------------------------------------------------------------------------------------------
function cbePayOrderTypChange(mstrCodispl){
//--------------------------------------------------------------------------------------------
    var lstrAction = top.frames["fraSequence"].plngMainAction;
    
    with (self.document.forms[0]){
        if (cbePayOrderTyp.value != "0"){        
            tcdReqDate.disabled = true;
            btn_tcdReqDate.disabled = tcdReqDate.disabled;
            tcnRequestNu.disabled = true;
//            tcnRequestNu.value = "";
            tctChequeNum.disabled = true;
            tctChequeNum.value = "";
            if (lstrAction == "401" || lstrAction == "301"){
                if (lstrAction == "301" && mstrCodispl != "OP06-5"){
                    if (cbePayOrderTyp.value != "4") tcdChequeDate.value = GetDateSystem();
                    else tcdChequeDate.value = ""
                }
                    
                if (cbePayOrderTyp.value == "3"){                
                    tcnRequestNu.disabled = true;
//                    tcnRequestNu.value = "";
                    tctChequeNum.disabled = false;
                    tcdReqDate.disabled = lstrAction != "301";
                    btn_tcdReqDate.disabled = tcdReqDate.disabled;
                    if (lstrAction == "301") {
                        tcdChequeDate.disabled = false;
                        btn_tcdChequeDate.disabled = tcdChequeDate.disabled;
                    }
                }
                else {
                    if (cbePayOrderTyp.value == "1"){
                        if(lstrAction != "401")
                            ShowPopUp("/VTimeNet/CashBank/CashBankSeq/ShowDefValues.aspx?Field=AccountNum&nAcc_bank=9998" , "ShowDefValues", 1, 1,"no","no",2000,2000);
                        if (mstrCodispl == "OP06-1"){
                            if (lstrAction == "401") tcnRequestNu.disabled = false;
                            tcdReqDate.disabled = false;
                            btn_tcdReqDate.disabled = tcdReqDate.disabled;
                            cbeCurrency.disabled = false;
                        }
                    }
                    else {                    
                        if (mstrCodispl == "OP06-1"){
                            tcdReqDate.disabled = lstrAction != "301";
                            btn_tcdReqDate.disabled = tcdReqDate.disabled;
                            tcnRequestNu.disabled = lstrAction == "301";
//                            if (tcnRequestNu.disabled) tcnRequestNu.value = "";
                            tcdChequeDate.disabled = lstrAction != "301";
                            btn_tcdChequeDate.disabled = tcdChequeDate.disabled;
                            if (tcdChequeDate.disabled) tcdChequeDate.value = "";
                        }
                    }
                    tctChequeNum.disabled = true;
                    tctChequeNum.value = "";
                }
            }

//+ Si la acción es: Condición.
            if (lstrAction == "402"){
                if (cbePayOrderTyp.value == "1"){
                    tcdChequeDate.disabled = false;
                    btn_tcdChequeDate.disabled = tcdChequeDate.disabled;
                    tcdReqDate.disabled = false;
                    btn_tcdReqDate.disabled = tcdReqDate.disabled;
                    tcnRequestNu.disabled = false;
                    tctChequeNum.disabled = true;
                    tctChequeNum.value = "";
                    cbeCurrency.disabled = false;
                }
                else {         
                    tcdChequeDate.disabled = false;
                    btn_tcdChequeDate.disabled = tcdChequeDate.disabled;
                    tcdReqDate.disabled = false;
                    btn_tcdReqDate.disabled = tcdReqDate.disabled;
                    tctChequeNum.disabled = cbePayOrderTyp.value != "3";
                    if (tctChequeNum.disabled) tctChequeNum.value = "";
                    tcnRequestNu.disabled = cbePayOrderTyp.value != "2" & cbePayOrderTyp.value != "4";
                    if (tcnRequestNu.disabled) tcnRequestNu.value = "0";
                }
            }

            if (mstrCodispl != "OP06-1"){
                if (cbePayOrderTyp.value != "1"){
                    if (cbePayOrderTyp.value == "3") tctChequeNum.disabled = false;
                }
            }
            if (cbePayOrderTyp.value == "5")                
                ShowDiv('DivTransfer', 'show');
            else ShowDiv('DivTransfer', 'hide');

            if (cbePayOrderTyp.value == "6"){
                cbeAcc_Type.value = 10;
            }
            else
                cbeAcc_Type.value = "";
        }
        else {
        
//            tcnRequestNu.value = "";
            tcnRequestNu.disabled = true;
            ShowDiv('DivTransfer', 'hide');
        }
        if (lstrAction == "301" &
            cbePayOrderTyp.value != "3" &
            tcnRequestNu.value == ""){
            ShowPopUp("/VTimeNet/CashBank/CashBankSeq/ShowDefValues.aspx?Field=PayOrderTyp&sCheque=" + tctChequeNum.value, "ShowDefValues", 1, 1,"no","no",2000,2000);
        }
    }

}


//%insStateZone: Habilita/deshabilita los campos de la ventana según la acción
//---------------------------------------------------------------------------------------------------
function insStateZone(){
//---------------------------------------------------------------------------------------------------

    var lblnCondition = (top.frames["fraSequence"].plngMainAction == 401)
    var lstrCodispl =  <%="'" & Session("OP006_sCodispl") & "'"%>      
        if (top.frames["fraSequence"].plngMainAction == 392)
            insHandImage("A392", true);
    
        with (self.document.forms[0]){
            if (mstrCodispl != "OP06-1") {        
                cbePayOrderTyp.disabled = <%=mclsCheques.DefaultValueOP006("cbePayOrderTyp_disabled")%>;
        
                valConcept.disabled = lblnCondition;
                btnvalConcept.disabled = lblnCondition;
            
                if (valConcept.disabled) {
                    valConcept.value = "";
                    valConcept_Enabled.value = "0";
                }
                else valConcept_Enabled.value = "1";
            
                valConcept.disabled = <%=mclsCheques.DefaultValueOP006("cbeConcept_disabled")%>;
                btnvalConcept.disabled = <%=mclsCheques.DefaultValueOP006("cbeConcept_disabled")%>;
               
                tctDescript.disabled = lblnCondition;
            
                if (tctDescript.disabled) tctDescript.value="";
            
                if (<%=mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble)%> <= 0)
                    tcnAmount.disabled =  (<%=mclsCheques.DefaultValueOP006("tcnAmount_disabled")%> || (lblnCondition));
                else 
                    tcnAmount.disabled = true;
        
                if (<%=mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble)%> > 0)
                    cbeCurrency.disabled = true;
                    
                if(<%=mobjValues.StringToType(mclsCheques.DefaultValueOP006("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble)%> > 0)
                    cbeCurrency.disabled = true;
                 
                if (lstrCodispl=='CO009')
                    cbeCurrencyPay.disabled = true;

//Estos campos se inhabilitaron al venir de pagos de siniestros, ya que los mismos se indican en dicha transaccion.
                if (lstrCodispl=='SI008')
                {
                    cbeCurrencyPay.disabled = true;
                    cbeOffice.disabled = true;
                    cbeOfficeAgen.disabled = true;
                    cbeAgency.disabled = true;
                    btncbeOfficeAgen.disabled = true;
                    btncbeAgency.disabled = true;
                    cbeCompany.disabled = true;
                }
                 
//Estos campos se habilitan al venir de Tratamiento de cotizaciones/propuestas para indicar donde se despacha el cheque.
//                if (lstrCodispl=='CA099A')
//                {
//                    cbeOffice.disabled = false;
//                    cbeOfficeAgen.disabled = false;
//                    cbeAgency.disabled = false;
//                    btncbeOfficeAgen.disabled = false;
//                    btncbeAgency.disabled = false;                   
//                }                 
                                                
                if (lblnCondition) tcnAmount.value="";

                dtcBenef.disabled = (<%=mclsCheques.DefaultValueOP006("dtcBenef_disabled")%> || (lblnCondition));
                btndtcBenef.disabled = dtcBenef.disabled;
                if (lblnCondition ) dtcBenef.value="";

                tcdChequeDate.disabled = lblnCondition;
                btn_tcdChequeDate.disabled = tcdChequeDate.disabled;
                if (tcdChequeDate.disabled) tcdChequeDate.value="";

                tcdReqDate.disabled = <%=mclsCheques.DefaultValueOP006("tcdAccDate_disabled")%>
                tcdAccDate.disabled = (<%=mclsCheques.DefaultValueOP006("tcdAccDate_disabled")%> || (lblnCondition));
                btn_tcdAccDate.disabled = tcdAccDate.disabled;
                if (lblnCondition) tcdAccDate.value="";

                valReqUser.disabled = lblnCondition;
                btnvalReqUser.disabled = valReqUser.disabled;
                if (valReqUser.disabled) valReqUser.value="";
            }
            else            
                insShowValuesOp06();
	        
	        EnabledConcept(cbeCompany);        
			if (mstrCodispl == "OP06-2"){
				valConcept.disabled = true;			
				btnvalConcept.disabled = true;		
				valReqUser.disabled = true;	
				btnvalReqUser.disabled = true;
			}
        }
    }

//%insCancel: Controla la acción "Cancelar" de la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
    var lstrQueryString = 'sCodispl='
    <%
If Request.QueryString.Item("sCodisplOri") <> vbNullString Then
	If Request.QueryString.Item("sCodisplOri") = "CA099A" Then
		%>
                lstrQueryString = lstrQueryString + 'CA099&sConfig=InSequence';
    <%		
	Else
		%>
                lstrQueryString = lstrQueryString + '<%=Request.QueryString.Item("sCodisplOri")%>';
    <%		
	End If
	%>
            top.document.location.href='/VTimeNet/common/GoTo.aspx?' + lstrQueryString;
    <%	
Else
	If CStr(Session("OP006_sCodispl")) = "SI021" Then
		%>
                top.window.close();
                top.opener.location.reload();
    <%		
	Else
		%>
                return (true);
    <%		
	End If
End If
%>  
}

//-----------------------------------------------------------------------------
function insCancelanterior(){
//-----------------------------------------------------------------------------
     <%If Request.QueryString.Item("sCodispl") <> "OP006" Then%>
         top.document.location.href='/VTimeNet/common/GoTo.aspx?sCodispl=<%=Session("OP006_sCodispl")%>'
     <%Else%>
         return (true)
     <%End If%> 
}   

//%insFinish: Controla la acción "Finalizar" de la página
//-----------------------------------------------------------------------------
function insFinish(){
//-----------------------------------------------------------------------------
    if (mstrInSubmit=="1")
        return (false);
    else
        mstrInSubmit="1";        
        
    if (InsValidDesc(self.document.forms[0].tctDescript.value)==true){
		<%If CStr(Session("OP006_sCodispl")) = "SI021" Then%>
		    top.window.close();
		    top.opener.location.reload();
		<%Else%>		      
	        return (true);
        <%End If%>     		
        }
    else
        return (false);                 
}

//%insShowValuesOp06: Habilita/deshabilita y asignar valores a los campos según la acción
//--------------------------------------------------------------------------------------------
function insShowValuesOp06(){
//--------------------------------------------------------------------------------------------

    var lintAction = top.frames["fraSequence"].plngMainAction
    switch(lintAction){
        case 301:
            with (self.document.forms[0]){
                tcdReqDate.disabled = false ;
                cbePayOrderTyp.disabled = false ;
                tcnRequestNu.disabled = true ;
                tctChequeNum.disabled = true ;
                cbeCompany.disabled = true ;
                tctDescript.disabled = false ;
                cbeCurrency.disabled = false ;
                tcnAmount.disabled = false ;
                cbeOffice.disabled = false ;
                cbeOfficeAgen.disabled = false ;                
                cbeAgency.disabled = false ;
                btncbeOfficeAgen.disabled = false ;                
                btncbeAgency.disabled = false ;
                cbeCurrency.disabled = false ;              
                cbeCurrencyPay.disabled = false ;
                tcnAmountpay.disabled = true ;
                cbeTypeSupport.disabled = false ;
                tcnDoc_support.disabled = false ;
                cbeTax_code.disabled = true ;
                btncbeTax_code.disabled = true ;
                tcnPercent.disabled = true ;
                tcnTax_amount.disabled = true ;
                tcnAfect.disabled = true ;
                tcnExcent.disabled = true ;
                tcnAmounttotal.disabled = true ;
                dtcBenef.disabled = false ;
                btndtcBenef.disabled = false ;
                valTypeprov.disabled = false ;
                btnvalTypeprov.disabled = false ;
                tcdChequeDate.disabled = false ;
                tcdAccDate.disabled = false ;
                btn_tcdAccDate.disabled = false ;
                valReqUser.disabled = false ;
                btnvalReqUser.disabled = false ;
                ClearFields();
            }
            break;
        case 302:
            with (self.document.forms[0]){
                tcdReqDate.disabled = false ;
                cbePayOrderTyp.disabled = true ;
                tcnRequestNu.disabled = true ;
                tctChequeNum.disabled = true ;
                cbeCompany.disabled = true ;
                cbeOffice.disabled = false;
                cbeOfficeAgen.disabled = false;                
                cbeAgency.disabled = false;
                btncbeOfficeAgen.disabled = false ;                
                btncbeAgency.disabled = false ;                
                if (mstrCodispl != "OP06-2"){
					btnvalConcept.disabled = false ;
					valConcept.disabled = false ;
				}
                tctDescript.disabled = false ;
                cbeCurrency.disabled = false ;
                tcnAmount.disabled = false ;
                cbeCurrency.disabled = false ;
                cbeCurrencyPay.disabled = false ;
                tcnAmountpay.disabled = true ;                
                if(valConcept.value==20){
                    cbeBranch.disabled = false ;
                    valBranch_Led.disabled = false ;
                    btnvalBranch_Led.disabled = false ;
                    tcnPolicy.disabled = false ;
                }    
                cbeTypeSupport.disabled = false ;
                tcnDoc_support.disabled = false ;
                cbeTax_code.disabled = true ;
                btncbeTax_code.disabled = true ;
                tcnPercent.disabled = true ;
                tcnTax_amount.disabled = true ;
                tcnAfect.disabled = true ;
                tcnExcent.disabled = true ;
                tcnAmounttotal.disabled = true ;
                dtcBenef.disabled = false ;
                valTypeprov.disabled = false ;
                btndtcBenef.disabled = false ;
                btnvalTypeprov.disabled = false ;
                tcdChequeDate.disabled = false ;
                tcdAccDate.disabled = false ;
                valReqUser.disabled = false ;
            }
            break;
        case 401:
            with (self.document.forms[0]){
                tcdReqDate.disabled = true ;
                cbePayOrderTyp.disabled = false ;
                tcnRequestNu.disabled = true ;
                tctChequeNum.disabled = true ;
                cbeCompany.disabled =  true ;
                tctDescript.disabled = true ;
                cbeCurrency.disabled = true ;
                tcnAmount.disabled = true ;
                cbeOffice.disabled = true ;
                cbeOfficeAgen.disabled = true;                
                cbeAgency.disabled = true;
                btncbeOfficeAgen.disabled = false ;                
                btncbeAgency.disabled = false ;                
                cbeCurrency.disabled = true ;
                cbeCurrencyPay.disabled = true ;
                tcnAmountpay.disabled = true ;
                cbeTypeSupport.disabled = true ;
                tcnDoc_support.disabled = true ;
                cbeTax_code.disabled = true ;
                btncbeTax_code.disabled = true ;
                tcnPercent.disabled = true ;
                tcnTax_amount.disabled = true ;
                tcnAfect.disabled = true ;
                tcnExcent.disabled = true ;
                tcnAmounttotal.disabled = true ;
                dtcBenef.disabled = true ;
                btndtcBenef.disabled = true ;
                valTypeprov.disabled = true ;
                dtcBenef_Digit.disabled = true ;
                btnvalTypeprov.disabled = true ;
                tcdChequeDate.disabled = true ;
                tcdAccDate.disabled = true ;
                valReqUser.disabled = true ;
                btnvalReqUser.disabled = true ;
                ClearFields();
                tcdReqDate.value ="";
                valReqUser.value =""; 
            }
            break;
        case 402:
            with (self.document.forms[0]){
                tcdReqDate.disabled = false ;
                cbePayOrderTyp.disabled = false ;
                tcnRequestNu.disabled = false ;
                tctChequeNum.disabled = true ;
                cbeCompany.disabled = true ;
                tctDescript.disabled = false ;
                cbeCurrency.disabled = false ;
                tcnAmount.value = "";
                tcnAmount.disabled = false ;
                cbeOffice.disabled = false ;
                cbeOfficeAgen.disabled = false;                
                cbeAgency.disabled = false;                
                btncbeOfficeAgen.disabled = false ;                
                btncbeAgency.disabled = false ;                
                cbeCurrency.disabled = true ;
                cbeCurrencyPay.disabled = true ;
                tcnAmountpay.disabled = true ;
                cbeTypeSupport.disabled = false ;
                tcnDoc_support.disabled = false ;
                cbeTax_code.disabled = true ;
                btncbeTax_code.disabled = true ;
                tcnPercent.disabled = true ;
                tcnTax_amount.disabled = true ;
                tcnAfect.disabled = true ;
                tcnExcent.disabled = true ;
                tcnAmounttotal.disabled = true ;
                dtcBenef.disabled = false ;
                valTypeprov.disabled = false ;
                btndtcBenef.disabled = false ;
                btnvalTypeprov.disabled = false ;
                tcdChequeDate.disabled = false ;
                tcdAccDate.disabled = false ;
                valReqUser.disabled = false ;
                btnvalReqUser.disabled = false ;
                cbeBranch.disabled=false;
                valBranch_Led.disabled=false;
                btnvalBranch_Led.disabled=false;
                tcnPolicy.disabled=false;
                ClearFields();
                tcdReqDate.value ="";
                cbeOfficeAgen.value ="";
                cbeAgency.value ="";
                valReqUser.value =""; 
            }
            break;
    }
}

//% EnabledFields:Habilita los campos (ramo, ramo contable, producto, póliza) sólo si el concepto 
//                corresponde a "Gastos de Suscripción" (20)
//-----------------------------------------------------------------------------------------------------
function EnabledFields(Field){
//-----------------------------------------------------------------------------------------------------
    if(top.frames["fraSequence"].plngMainAction!=401 && top.frames["fraSequence"].plngMainAction!=402){
        if(Field.value==20)
        {
            self.document.forms[0].cbeBranch.disabled=false;
            self.document.forms[0].valBranch_Led.disabled=false;
            self.document.forms[0].btnvalBranch_Led.disabled=false;
            self.document.forms[0].tcnPolicy.disabled=false;
            self.document.forms[0].cbeTypeSupport.disabled=true;
        }
        else
        {
            
            self.document.forms[0].cbeBranch.disabled=true;
            self.document.forms[0].valBranch_Led.disabled=true;
            self.document.forms[0].btnvalBranch_Led.disabled=true;
            self.document.forms[0].valProduct.disabled=true;
            self.document.forms[0].btnvalProduct.disabled=true;
            self.document.forms[0].tcnPolicy.disabled=true;
            self.document.forms[0].cbeTypeSupport.disabled=false;
            self.document.forms[0].cbeBranch.value='';
            self.document.forms[0].valBranch_Led.value='';
            self.document.forms[0].tcnPolicy.value='';
            self.document.forms[0].valProduct.value='';
            UpdateDiv('valBranch_LedDesc','','Normal');
            UpdateDiv('valProductDesc','','Normal');
        }
        if(Field.value==19)
		{
		<%If mstrCodispl = "OP06-2" Then%>
		self.document.forms[0].tcnProponum.disabled=true;
		self.document.forms[0].tcnProponum.value=<%=Request.QueryString.Item("nProponum")%>
		<%Else%>
			self.document.forms[0].tcnProponum.disabled=false;
			self.document.forms[0].tcnProponum.value='';
		    self.document.forms[0].tcnAmount.disabled = true ;
		    self.document.forms[0].cbePayOrderTyp.value = '2';
		    self.document.forms[0].cbeTypeSupport.value = '4';
		    ShowPopUp("/VTimeNet/CashBank/CashBankSeq/ShowDefValues.aspx?Field=PayOrderTyp&sCheque=" + self.document.forms[0].tctChequeNum.value, "ShowDefValues", 1, 1,"no","no",2000,2000);
self.document.forms[0].tcdChequeDate.value = '<% %>
<%=	Today%>'
self.document.forms[0].tcdAccDate.value = '<% %>
<%=	Today%>'
		<%End If%>
		   self.document.forms[0].tcnAmount.disabled = true ;
           self.document.forms[0].cbeCurrency.disabled = true ;
		}
		else
		{
		   <%If mstrCodispl = "OP06-1" Then%>
		   self.document.forms[0].tcnProponum.disabled=true;
		   self.document.forms[0].tcnProponum.value='';
 		   self.document.forms[0].tcnAmount.disabled = false ;
           self.document.forms[0].cbeCurrency.disabled = false ;
           self.document.forms[0].cbeCurrencyPay.disabled = false ;
		//   self.document.forms[0].cbePayOrderTyp.value = '';
		   <%End If%>
		}		
    }            
}

//%EnabledConcept: Habilita y deshabilita el campo "Concepto" dependiendo del valor del campo "Compañía"
//-------------------------------------------------------------------------------------------------------
function EnabledConcept(Field){
//-------------------------------------------------------------------------------------------------------
    if(Field.value!=0)
    {
        self.document.forms[0].valConcept.Parameters.Param1.sValue=Field.value;
        self.document.forms[0].valConcept.disabled=false;
        self.document.forms[0].btnvalConcept.disabled=false;
	}		
    else
    {
        self.document.forms[0].valConcept.disabled=true;
        self.document.forms[0].btnvalConcept.disabled=true;
        self.document.forms[0].valConcept.value='';
        UpdateDiv('valConceptDesc','','Normal');
    }    
}

//%FindProvider: Se obtiene el documento asociado al proveedor (Beneficiario)
//--------------------------------------------------------------------------------------
function FindProvider(){
//--------------------------------------------------------------------------------------
    //if(Field.value!="" && self.document.forms[0].valConcept.value==20)
    
	with (self.document.forms[0]){
	    if(dtcBenef.value!="" && valTypeprov.value!=0){    
	        insDefValues("Find_Provider", "nTypeSupport=" + valTypeprov_nTypeSupport.value + "&dEffecdate=" + tcdReqDate.value + "&nAmount_afe=" + tcnAfect.value + "&nAmount_exe=" + tcnExcent.value + "&nAmountpay=" + tcnAmountpay.value, '/VTimeNet/cashbank/cashbankseq');    
	    }
	    else{
	        cbeTypeSupport.value=0;
	        cbeTax_code.value='';
			tcnPercent.value='';
			cbeTax_code.disabled=true;
			btncbeTax_code.disabled=true;
			tcnTax_amount.value=0;
			tcnAfect.value=0;
			tcnExcent.value=0;
			cbeTax_code.onblur();				
			tcnAmounttotal.value=tcnAmountpay.value;
	    }    
    }    
}
//%ShowValueParam: Se pasa el cliente como parametro
//--------------------------------------------------------------------------------------
function ShowValueParam(){
//--------------------------------------------------------------------------------------

	self.document.forms[0].valTypeprov.Parameters.Param1.sValue=self.document.forms[0].dtcBenef.value;

}

//% InsChange_Bank: Cambia valor de banco 
//-----------------------------------------------------
function InsChange_Bank(Field){
//-----------------------------------------------------

    with (self.document.forms[0]){
        if (Field.value == "") {
            valBankAccount.disabled = true;
            valBankAccount.value = "";
            btnvalBankAccount.disabled = valBankAccount.disabled;
        }
        else {
			valBankAccount.disabled = false;
			valBankAccount.value = "";
			btnvalBankAccount.disabled = valBankAccount.disabled;
			valBankAccount.Parameters.Param2.sValue=Field.value         
        }
    }
}


//% InsChangeAccountHolder: Cambia valor del titular de la cuenta
//-----------------------------------------------------
function InsChangeAccountHolder(Field){
//-----------------------------------------------------

    with (self.document.forms[0]){
        if (Field.value == "") {
            valAccount.disabled = true;
            valAccount.value = "";
            btnvalAccount.disabled = valAccount.disabled;
        }
        else {
			valAccount.disabled = false;
			valAccount.value = "";
			btnvalAccount.disabled = valAccount.disabled;
			valAccount.Parameters.Param1.sValue=Field.value         
        }
    }
}



</SCRIPT>
<META HTTP-EQUIV="Content-Language" CONTENT="es">
<SCRIPT SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT SRC="/VTimeNet/Scripts/Op006_K.js"></SCRIPT>    




<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu(CStr(mstrCodispl), "OP06-1_K.aspx", 1, ""))
mobjMenu = Nothing
With Request
	mstrQueryString = "nTyp_Acco=" & .QueryString.Item("nTyp_Acco") & _
                      "&sType_Acc=" & .QueryString.Item("sType_Acc") & _
                      "&sClient=" & .QueryString.Item("sClient") & _
                      "&nCurrency=" & .QueryString.Item("nCurrency") & _
                      "&nAmount=" & .QueryString.Item("nAmount") & _
                      "&dEffecDate=" & .QueryString.Item("dEffecDate") & _
                      "&nProcess=" & .QueryString.Item("nProcess") & _
                      "&nPayOrderTyp=" & .QueryString.Item("nPayOrderTyp") & _
                      "&nTypeTrans=" & .QueryString.Item("nTypeTrans") & _
                      "&nRemNum=" & .QueryString.Item("nRemNum") & _
                      "&nConcept=" & .QueryString.Item("nConcept") & _
                      "&sCertype=" & .QueryString.Item("sCertype") & _
                      "&nBranch=" & .QueryString.Item("nBranch") & _
                      "&nProduct=" & .QueryString.Item("nProduct") & _
                      "&nPolicy=" & .QueryString.Item("nPolicy") & _
                      "&nCertif=" & .QueryString.Item("nCertif") & _
                      "&dRescdate=" & .QueryString.Item("dRescdate") & _
                      "&sSurrType=" & .QueryString.Item("sSurrType") & _
                      "&sProcessType=" & .QueryString.Item("sProcessType") & _
                      "&sRequest=" & .QueryString.Item("sRequest") & _
                      "&sSurrPayWay=" & .QueryString.Item("sSurrPayWay") & _
                      "&nSurrAmount=" & .QueryString.Item("nSurrAmount") & _
                      "&nBranchPay=" & .QueryString.Item("nBranchPay") & _
                      "&nProductPay=" & .QueryString.Item("nProductPay") & _
                      "&nPolicyPay=" & .QueryString.Item("nPolicyPay") & _
                      "&nCertifPay=" & .QueryString.Item("nCertifPay") & _
                      "&nProponum=" & .QueryString.Item("nProponum") & _
                      "&nBalance=" & .QueryString.Item("nBalance") & _
                      "&nOperat=" & .QueryString.Item("nOperat") & _
                      "&sSurrTot=" & .QueryString.Item("sSurrTot") & _
                      "&nCoverCost=" & .QueryString.Item("nCoverCost") & _
                      "&nRetention=" & .QueryString.Item("nRetention") & _
                      "&nSurrAmt=" & .QueryString.Item("nSurrAmt") & _
                      "&sAnulReceipt=" & .QueryString.Item("sAnulReceipt") & _
                      "&sCodisplOri=" & .QueryString.Item("sCodisplOri") & _
                      "&nAmotax=" & .QueryString.Item("nAmotax") & _
                      "&nInterest=" & .QueryString.Item("nInterest") & _
                      "&nOffice=" & .QueryString.Item("nOffice") & _
                      "&nOfficeAgen=" & .QueryString.Item("nOfficeAgen") & "&nAgency=" & .QueryString.Item("nAgency") & _
                      "&sReport=" & .QueryString.Item("sReport") & "&tcnCapital=" & .QueryString.Item("tcnCapital") & _
                      "&nMainaction_op006=" & .QueryString.Item("nMainaction") & "&nSurrVal=" & .QueryString.Item("nSurrVal") & _
                      "&nMaxAmount=" & .QueryString.Item("nMaxAmount") & "&nLoans=" & .QueryString.Item("nLoans") & _
                      "&nOrigin_apv=" & .QueryString.Item("nOrigin_apv") & "&nReceipt=" & .QueryString.Item("nReceipt") 
                          
    mstrQueryString = mstrQueryString & "&dExpirDat=" & .QueryString.Item("dExpirDat") & "&nSource=" & .QueryString.Item("nSource") & _
                      "&nTypeReceipt=" & .QueryString.Item("nTypeReceipt") & "&sOrigReceipt=" & .QueryString.Item("sOrigReceipt") & _
                      "&sKey=" & .QueryString.Item("sKey") & "&sAdjust=" & .QueryString.Item("sAdjust") & _
                      "&nAdjReceipt=" & .QueryString.Item("nAdjReceipt") & "&nAdjAmount=" & .QueryString.Item("nAdjAmount") & _
                      "&nTypePay=" & .QueryString.Item("nTypePay") & "&nSurrReas=" & .QueryString.Item("nSurrReas") & _
                      "&nEntity=" & .QueryString.Item("nEntity") & "&sClientEnt=" & .QueryString.Item("sClientEnt") & _
                      "&hddTaxSurr=" & .QueryString.Item("hddTaxSurr") & "&hddSurrValue_Tax=" & .QueryString.Item("hddSurrValue_Tax") 
                      
    mstrQueryString = mstrQueryString & "&tcdPaymentDate=" & .QueryString.Item("tcdPaymentDate") & "&tcnPremium=" & .QueryString.Item("tcnPremium") & _
                      "&tcnSurrVal=" & .QueryString.Item("tcnSurrVal") & "&tcnLoans=" & .QueryString.Item("tcnLoans") & _
                      "&tcnInterest=" & .QueryString.Item("tcnInterest") & "&tcnSurrCostPar=" & .QueryString.Item("tcnSurrCostPar")
	
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR>
<BR>
<FORM METHOD="POST" ID="FORM" NAME="frmPayOrder" ACTION="valCashBankSeq.aspx?<%=mstrQueryString%>">
    <TABLE WIDTH=100%>
        <TR>
            <TD><LABEL ID=9073><%= GetLocalResourceObject("tcdReqDateCaption") %></LABEL></TD>
            <TD>
            <%
                If CStr(Session("OP006_sCodispl")) = "CA099A" Then
	                Response.Write(mobjValues.DateControl("tcdReqDate", CStr(Today()),  , GetLocalResourceObject("tcdReqDateToolTip"),  ,  ,  ,  , True))
                Else
	                Response.Write(mobjValues.DateControl("tcdReqDate", mobjValues.TypeToString(mdtmEffecdate, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdReqDateToolTip"),  ,  ,  ,  , True))
                End If
            %>
            </TD>
			<%If mstrCodispl = "OP06-1" Then%>        
					<TD><LABEL ID=9078><%= GetLocalResourceObject("cbePayOrderTypCaption") %></LABEL></TD>
					<TD>
						<%	mobjValues.TypeList = 2
	            mobjValues.List = "3"
	            If Request.QueryString.Item("nPayOrderTyp") <> vbNullString Then
		            Response.Write(mobjValues.PossiblesValues("cbePayOrderTyp", "table193", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nPayOrderTyp"),  ,  ,  ,  ,  , "cbePayOrderTypChange(""" & mstrCodispl & """);", True,  , GetLocalResourceObject("cbePayOrderTypToolTip")))
	            Else
		            Response.Write(mobjValues.PossiblesValues("cbePayOrderTyp", "table193", eFunctions.Values.eValuesType.clngComboType, mclsCheques.DefaultValueOP006("cbePayOrderTyp"),  ,  ,  ,  ,  , "cbePayOrderTypChange(""" & mstrCodispl & """);", True,  , GetLocalResourceObject("cbePayOrderTypToolTip")))
	            End If
	
            Else%>
					<TD><LABEL ID=9078><%= GetLocalResourceObject("cbePayOrderTypCaption") %></LABEL></TD>
					<TD>
						<%	If Request.QueryString.Item("nPayOrderTyp") <> vbNullString Then
						        If Request.QueryString.Item("nPayOrderTyp") = 2 Then
						            mobjValues.TypeList = 1
						            mobjValues.List = "2,5,6"
						        End If
		                        Response.Write(mobjValues.PossiblesValues("cbePayOrderTyp", "table193", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nPayOrderTyp"),  ,  ,  ,  ,  , "cbePayOrderTypChange(""" & mstrCodispl & """);", True,  , GetLocalResourceObject("cbePayOrderTypToolTip")))
	                        Else
		                        Response.Write(mobjValues.PossiblesValues("cbePayOrderTyp", "table193", eFunctions.Values.eValuesType.clngComboType, mclsCheques.DefaultValueOP006("cbePayOrderTyp"),  ,  ,  ,  ,  , "cbePayOrderTypChange(""" & mstrCodispl & """);", True,  , GetLocalResourceObject("cbePayOrderTypToolTip")))
	                        End If
            End If%>
			</TD>
        </TR>
        <TR>
            <TD><LABEL ID=9079><%= GetLocalResourceObject("tcnRequestNuCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnRequestNu", 10, mclsCheques.DefaultValueOP006("tcnRequestNu", Session("nUsercode")),  , GetLocalResourceObject("tcnRequestNuToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=9071><%= GetLocalResourceObject("tctChequeNumCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctChequeNum", 10, "",  , GetLocalResourceObject("tctChequeNumToolTip"),  ,  ,  ,  , mclsCheques.DefaultValueOP006("tctChequeNum_disabled"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeCompanyCaption") %></LABEL></TD>
            <TD>
				<%If CStr(Session("OP006_sCodispl")) = "SI021" Then
	Response.Write(mobjValues.PossiblesValues("cbeCompany", "company", eFunctions.Values.eValuesType.clngComboType, Session("nCompanyUser"),  ,  ,  ,  ,  , "EnabledConcept(this);", True,  , GetLocalResourceObject("cbeCompanyToolTip")))
Else
	Response.Write(mobjValues.PossiblesValues("cbeCompany", "company", eFunctions.Values.eValuesType.clngComboType, Session("nCompanyUser"),  ,  ,  ,  ,  , "EnabledConcept(this);", Request.QueryString.Item("sCodispl") = "OP06-2" Or Request.QueryString.Item("sCodispl") = "OP06-1",  , GetLocalResourceObject("cbeCompanyToolTip")))
End If
%>
            </TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valAccountCaption") %></LABEL></TD>
            <TD COLSPAN="3">
            <%

If Request.QueryString.Item("nOffice") <> vbNullString Then
	llngOffice = mobjValues.StringToType(Request.QueryString.Item("nOffice"), eFunctions.Values.eTypeData.etdDouble)
Else
	llngOffice = Session("nOffice")
End If

With mobjValues
	.Parameters.Add("nOffice", llngOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valAccount", "tabbank_accoficce", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , CStr(Session("OP006_sCodispl")) <> "AG004" And CStr(Session("OP006_sCodispl")) <> "SI777", 20, GetLocalResourceObject("valAccountToolTip"), eFunctions.Values.eTypeCode.eString, 4,  , True))
End With

%>
            </TD>
            
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13378><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
            <TD>
            <%

If Request.QueryString.Item("nOfficeAgen") <> vbNullString Then
	llngOfficeAgen = mobjValues.StringToType(Request.QueryString.Item("nOfficeAgen"), eFunctions.Values.eTypeData.etdDouble)
Else
	llngOfficeAgen = Session("nOfficeAgen")
End If

If Request.QueryString.Item("nAgency") <> vbNullString Then
	llngAgency = mobjValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble)
Else
	llngAgency = Session("nAgency")
End If

mobjValues.TypeOrder = 1
Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", 1, llngOffice,  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1)", True,  , GetLocalResourceObject("cbeOfficeToolTip")))
%>
            </TD>
            
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeOfficeAgenCaption") %></LABEL></TD>
            <TD>
            <%
With mobjValues
	.Parameters.Add("nOfficeAgen", llngOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	Response.Write(.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngWindowType, llngOfficeAgen, True,  ,  ,  ,  , "insInitialAgency(2)", True,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
End With
%>
            </TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL></TD>
            <TD>
            <%
With mobjValues
	.Parameters.Add("nOfficeAgen", llngOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
	.Parameters.ReturnValue("sDesAgen",  ,  , True)
	Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5555", eFunctions.Values.eValuesType.clngWindowType, llngAgency, True,  ,  ,  ,  , "insInitialAgency(3)", True,  , GetLocalResourceObject("cbeAgencyToolTip")))
End With
%>
            </TD>
        </TR>
        <TR>        
            <TD COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>       
            <TD><LABEL ID=9038><%= GetLocalResourceObject("valConceptCaption") %></LABEL></TD>
            <%If mstrCodispl = "OP06-1" Then
	If mobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
		mobjValues.Parameters.Add("nCompany", Session("nLedcompan"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		%>
						<TD COLSPAN="3"><%=mobjValues.PossiblesValues("valConcept", "tabconceptscompany", eFunctions.Values.eValuesType.clngWindowType, mclsCheques.DefaultValueOP006("cbeConcept"), True,  ,  ,  ,  , "EnabledFields(this);", Request.QueryString.Item("sCodispl") = "OP06-1", 8, GetLocalResourceObject("valConceptToolTip"))%></TD>
			<%		Response.Write(mobjValues.HiddenControl("valConcept_Enabled", "1"))
	Else
		mobjValues.Parameters.Add("nCompany", Session("nLedcompan"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
						<TD COLSPAN="3"><%=mobjValues.PossiblesValues("valConcept", "tabconceptscompany", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdDouble), True,  ,  ,  ,  , "EnabledFields(this);", Request.QueryString.Item("sCodispl") = "OP06-1", 8, GetLocalResourceObject("valConceptToolTip"))%></TD>
            <%		Response.Write(mobjValues.HiddenControl("valConcept_Enabled", "1"))
	End If
Else
	If mobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
		mobjValues.Parameters.Add("nCompany", Session("nLedcompan"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		%>
			            <TD COLSPAN="3"><%=mobjValues.PossiblesValues("valConcept", "tabconceptscompany", eFunctions.Values.eValuesType.clngWindowType, mclsCheques.DefaultValueOP006("cbeConcept"), True,  ,  ,  ,  , "EnabledFields(this);", Request.QueryString.Item("sCodispl") = "OP06-1", 8, GetLocalResourceObject("valConceptToolTip"))%></TD>
            <%		Response.Write(mobjValues.HiddenControl("valConcept_Enabled", "1"))
	Else
		mobjValues.Parameters.Add("nCompany", Session("nLedcompan"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		%>                        
						<TD COLSPAN="3"><%=mobjValues.PossiblesValues("valConcept", "tabconceptscompany", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdDouble), True,  ,  ,  ,  , "EnabledFields(this);", Request.QueryString.Item("sCodispl") = "OP06-1", 8, GetLocalResourceObject("valConceptToolTip"))%></TD>
            <%		Response.Write(mobjValues.HiddenControl("valConcept_Enabled", "1"))
	End If
End If
%>
        </TR>
        <TR>
            <TD><LABEL ID=9075><%= GetLocalResourceObject("tctDescriptCaption") %></LABEL></TD>
            <%If mstrCodispl = "OP06-3" Then%>
		        <TD COLSPAN="3"><%=mobjValues.TextAreaControl("tctDescript", 2, 30, tctDescriptOP063,  , GetLocalResourceObject("tctDescriptToolTip"),  , False,  , "InsValidDesc(this.value)")%></TD>
		    <%Else%>
		        <TD COLSPAN="3"><%=mobjValues.TextAreaControl("tctDescript", 2, 30, mclsCheques.DefaultValueOP006("tctDescript"),  , GetLocalResourceObject("tctDescriptToolTip"),  , False,  , "InsValidDesc(this.value)")%></TD>
            <%End If%>
        </TR>                
        <TR>
            <TD><LABEL ID=9074><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>            
            <%If mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then%>
					<TD><%=mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, mclsCheques.DefaultValueOP006("cbeCurrency"),  ,  ,  ,  ,  , "inshowCurrency(this)", Request.QueryString.Item("sCodispl") = "OP06-1",  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
            <%Else%>                
					<TD>
						<%	If CStr(Session("OP006_sCodispl")) = "SI021" Then
		Response.Write(mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  , "inshowCurrency(this)", False,  , GetLocalResourceObject("cbeCurrencyToolTip")))
	Else
		Response.Write(mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  , "inshowCurrency(this)", True,  , GetLocalResourceObject("cbeCurrencyToolTip")))
	End If%>
					</TD>
            <%End If%>
            <TD><LABEL ID=9068><%= GetLocalResourceObject("tcnAmountCaption") %></LABEL></TD>
            <%If mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then%>            
					<TD>
						<%	
	If CStr(Session("OP006_sCodispl")) = "SI021" Then
		Response.Write(mobjValues.NumericControl("tcnAmount", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnAmountToolTip"), True, 6,  ,  ,  ,  , False))
	Else
		Response.Write(mobjValues.NumericControl("tcnAmount", 18, mclsCheques.DefaultValueOP006("tcnAmount"),  , GetLocalResourceObject("tcnAmountToolTip"), True, 6,  ,  ,  , "inshowCurrency(this);", True))
	End If
	%>
					</TD>
            <%Else%>            
					<TD><%=mobjValues.NumericControl("tcnAmount", 18, Request.QueryString.Item("nAmount"),  , GetLocalResourceObject("tcnAmountToolTip"), True, 6,  ,  ,  , "inshowCurrency(this);", True)%></TD>
            <%End If%>
        </TR>
        <TR>
            <TD><LABEL ID=9074><%= GetLocalResourceObject("cbeCurrencyPayCaption") %></LABEL></TD>
            <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeCurrencyPay", "tabcurrency_b", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nCurrencypay"),  ,  ,  ,  ,  , "inshowCurrency(this);", Request.QueryString.Item("sCodispl") = "OP06-1" Or Request.QueryString.Item("sCodispl") = "OP06-2",  , GetLocalResourceObject("cbeCurrencyPayToolTip")))%></TD>
            <TD><LABEL ID=9068><%= GetLocalResourceObject("tcnAmountpayCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAmountpay", 18, Request.QueryString.Item("nAmountpay"),  , GetLocalResourceObject("tcnAmountpayToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Request.QueryString.Item("nBranch"),  ,  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valBranch_LedCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valBranch_Led", "table75", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valBranch_LedToolTip"))%></TD>
        </TR>
        
        <%If CStr(Session("OP006_sCodispl")) = "CA099A" Then%>
			<%	If IsNothing(Request.QueryString.Item("nPolicy")) And Not IsNothing(Request.QueryString.Item("nProponum")) Then%>
				<TR>            
				    <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
				    <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Request.QueryString.Item("nBranch"), eFunctions.Values.eValuesType.clngWindowType, True, Request.QueryString.Item("nProduct"),  ,  ,  ,  ,  , True)%></TD>
				    <TD><LABEL ID=0><%= GetLocalResourceObject("tcnProponumCaption") %></LABEL></TD>
				    <TD><%=mobjValues.NumericControl("tcnProponum", 10, Request.QueryString.Item("nProponum"),  , GetLocalResourceObject("tcnProponumToolTip"),  ,  ,  ,  ,  , "insProponum(this);", True)%></TD>
				</TR>        
			<%	Else%>        
				<TR>            
				    <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
				    <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Request.QueryString.Item("nBranch"), eFunctions.Values.eValuesType.clngWindowType, True, Request.QueryString.Item("nProduct"),  ,  ,  ,  ,  , True)%></TD>
				    <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
				    <TD><%=mobjValues.NumericControl("tcnPolicy", 10, Request.QueryString.Item("nPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
				</TR>        
				<TR>            
				    <TD><LABEL ID=0><%= GetLocalResourceObject("tcnProponumCaption") %></LABEL></TD>
				    <TD><%=mobjValues.NumericControl("tcnProponum", 10, Request.QueryString.Item("nProponum"),  , GetLocalResourceObject("tcnProponumToolTip"),  ,  ,  ,  ,  , "insProponum(this);", True)%></TD>
					<TD></TD>
					<TD></TD>
				</TR>        
			<%	End If%>
        <%Else%>
			<TR>            
			    <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			    <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Request.QueryString.Item("nBranch"), eFunctions.Values.eValuesType.clngWindowType, True, Request.QueryString.Item("nProduct"),  ,  ,  ,  ,  , True)%></TD>
			    <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnPolicy", 10, Request.QueryString.Item("nPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
			</TR>        
			<TR>            
			    <TD><LABEL ID=0><%= GetLocalResourceObject("tcnProponumCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnProponum", 10, Request.QueryString.Item("nProponum"),  , GetLocalResourceObject("tcnProponumToolTip"),  ,  ,  ,  ,  , "insProponum(this);", True)%></TD>
				<TD></TD>
				<TD></TD>
			</TR>        
        <%End If%>
        
        <TR>        
            <TD COLSPAN="4">&nbsp;</TD>
        </TR>                
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HorLine"></TD>            
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeTypeSupportCaption") %></LABEL></TD>
            <%If Request.QueryString.Item("sCodispl") = "OP06-1" Or CStr(Session("OP006_sCodispl")) = "AG004" Or (CStr(Session("OP006_sCodispl")) = "SI008" And Not IsNothing(Request.QueryString.Item("nTypesupport"))) Then
	mblnTypeSupport = True
Else
	mblnTypeSupport = False
End If
%>
            <TD><%=mobjValues.PossiblesValues("cbeTypeSupport", "table5570", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nTypesupport"),  ,  ,  ,  ,  , "insParamFixval()", mblnTypeSupport,  , GetLocalResourceObject("cbeTypeSupportToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnDoc_supportCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnDoc_support", 10, Request.QueryString.Item("nDoc_support"),  , GetLocalResourceObject("tcnDoc_supportToolTip"),  , 0,  ,  ,  ,  , Request.QueryString.Item("sCodispl") = "OP06-1")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeTax_codeCaption") %></LABEL></TD>
            <%
                mobjValues.Parameters.Add("nTypesupport", mobjValues.StringToType(Request.QueryString.Item("nTypesupport"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("dEffecdate", mobjValues.StringToType(mdtmEffecdate, eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.ReturnValue("nPercent", False, vbNullString, True)
%>
            <TD><%=mobjValues.PossiblesValues("cbeTax_code", "Tabtax_fixval", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nTax_code"), True,  ,  ,  ,  , "insShowPercent()", IsNothing(Request.QueryString.Item("nTypesupport")), 5, GetLocalResourceObject("cbeTax_codeToolTip"))%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPercentCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPercent", 6, Request.QueryString.Item("nPercent"),  , GetLocalResourceObject("tcnPercentToolTip"), 6, 4,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
        <%If Request.QueryString.Item("nTypesupport") = "2" Or Request.QueryString.Item("nTypesupport") = "4" Then
	If IsNothing(Request.QueryString.Item("nExcent")) And CStr(Session("sCodispl_Aux")) <> "SI008_K" Then
		nAmount_exe = Request.QueryString.Item("nAmountpay")
		nAmount_afe = 0
	Else
		nAmount_exe = Request.QueryString.Item("nExcent")
	End If
Else
	If IsNothing(Request.QueryString.Item("nAfect")) And CStr(Session("sCodispl_Aux")) <> "SI008_K" Then
		nAmount_exe = 0
		nAmount_afe = Request.QueryString.Item("nAmountpay")
	Else
		nAmount_afe = Request.QueryString.Item("nAfect")
	End If
End If%>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnTax_amountCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnTax_amount", 18, Request.QueryString.Item("nTax_amount"),  , GetLocalResourceObject("tcnTax_amountToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnAfectCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAfect", 18, nAmount_afe,  , GetLocalResourceObject("tcnAfectToolTip"), True, 6,  ,  ,  , "insShowCalcAmountT()", True)%></TD>            
        </TR>        
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnExcentCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnExcent", 18, nAmount_exe,  , GetLocalResourceObject("tcnExcentToolTip"), True, 6,  ,  ,  , "insShowCalcAmountT()", True)%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnAmounttotalCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAmounttotal", 18, Request.QueryString.Item("nAmounttotal"),  , GetLocalResourceObject("tcnAmounttotalToolTip"), True, 6,  ,  ,  ,  , True)%></TD>            
        </TR>                      
        <TR>
            <TD><LABEL ID=9069><%= GetLocalResourceObject("dtcBenefCaption") %></LABEL></TD>
			<TD><%=mobjValues.ClientControl("dtcBenef", mclsCheques.DefaultValueOP006("dtcBenef"),  , GetLocalResourceObject("dtcBenefToolTip"), "ShowValueParam();", True, "lblBenefname", False,  ,  ,  ,  ,  , True, True)%></TD>
            <TD><LABEL ID=9069><%= GetLocalResourceObject("valTypeprovCaption") %></LABEL></TD>
            <%
mobjValues.Parameters.Add("sClient", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.ReturnValue("TypDoc", True, "Tipo Documento", True)
mobjValues.Parameters.ReturnValue("nTypeSupport", False, "Tipo Documento", True)

%>
            
			<TD><%=mobjValues.PossiblesValues("valTypeprov", "TAB_PROVIDERCLIENT", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "FindProvider();", True,  , GetLocalResourceObject("valTypeprovToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=9070><%= GetLocalResourceObject("tcdChequeDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdChequeDate", mclsCheques.DefaultValueOP006("tcdChequeDate"),  , GetLocalResourceObject("tcdChequeDateToolTip"),  ,  ,  , "if (this.value != """") document.forms[0].elements[""tcdAccDate""].value = this.value;insParamFixval();", False)%></TD>
            <TD><LABEL ID=9066><%= GetLocalResourceObject("tcdAccDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdAccDate", mclsCheques.DefaultValueOP006("tcdChequeDate"),  , GetLocalResourceObject("tcdAccDateToolTip"),  ,  ,  , "insParamFixval();", Request.QueryString.Item("sCodispl") = "OP06-1")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=9080><%= GetLocalResourceObject("valReqUserCaption") %>&nbsp;</LABEL></TD>
            <TD COLSPAN="3"><%=mobjValues.PossiblesValues("valReqUser", "tabusers", eFunctions.Values.eValuesType.clngWindowType, Session("nUsercode"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valReqUserToolTip"))%></TD>
        </TR>
    </TABLE>

<%
Response.Write("    <DIV ID=""DivTransfer"">" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%""  CELLSPACING=""10"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD ALIGN=""LEFT"" CLASS=""HighLighted"" COLSPAN = 5><LABEL ID=0><A NAME=""Datos de la transferencia"">" & GetLocalResourceObject("AnchorTransferCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=5 CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("dtcAccountHolderCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")
Response.Write(mobjValues.ClientControl("dtcAccountHolder", mclsCheques.DefaultValueOP006("dtcAccountHolder"),  , GetLocalResourceObject("dtcAccountHolderToolTip"),"InsChangeAccountHolder(this);", False, "lblAccountHoldername", False,  ,  ,  ,  ,  , True, True))
Response.Write("            </TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeBankExtCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")
Response.Write(mobjValues.PossiblesValues("cbeBankExt", "table7", eFunctions.Values.eValuesType.clngWindowType,mclsCheques.DefaultValueOP006("cbeBankExt"), False,  ,  ,  ,  , "InsChange_Bank(this);", False,  , GetLocalResourceObject("cbeBankExtToolTip"),  , 3))
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeAcc_TypeCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")
Response.Write(mobjValues.PossiblesValues("cbeAcc_Type", "table190", eFunctions.Values.eValuesType.clngComboType, mclsCheques.DefaultValueOP006("cbeAcc_Type"), False,  ,  ,  ,  ,  ,False,  , GetLocalResourceObject("cbeAcc_TypeToolTip"),  , 3))
Response.Write("            </TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("valBankAccountCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")
Call mobjValues.Parameters.Add("sClient", mclsCheques.DefaultValueOP006("dtcAccountHolder"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Call mobjValues.Parameters.Add("nBankExt", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("valBankAccount", "tabbk_account", eFunctions.Values.eValuesType.clngWindowType, mclsCheques.DefaultValueOP006("valAccount"), True,  ,  ,  ,  ,  , , 20, GetLocalResourceObject("valBankAccountToolTip"), eFunctions.Values.eTypeCode.eString, 4, False, True))
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("    </TABLE> " & vbCrLf)
Response.Write("    </DIV>   " & vbCrLf)
%>

    <%
If mstrCodispl = "OP06-5" Then
	Response.Write("<SCRIPT>cbePayOrderTypChange( '" & mstrCodispl & "'); </SCRIPT>")
End If
If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionCondition) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	Call insReaOP006()
End If
Response.Write(mobjValues.BeginPageButton)
%>
</FORM>
</BODY>
</HTML>
<%
mclsCheques = Nothing
If Request.QueryString.Item("sCodispl") <> "OP06-1" Then
	Response.Write("<SCRIPT>ClientRequest(301,5);</SCRIPT>")
End If
Response.Write("<SCRIPT>setDivTransfer();</SCRIPT>")

'+Se habilita el campo beneficiario cuando es llamado desde el reverso de cobro (CO09)
'+O cuando es llamado desde el pago de siniestro (SI777)
If CStr(Session("OP006_sCodispl")) = "CO009" Or CStr(Session("OP006_sCodispl")) = "SI777" Then
	Response.Write("<SCRIPT>self.document.forms[0].dtcBenef.disabled=false;</SCRIPT>")
	Response.Write("<SCRIPT>self.document.forms[0].dtcBenef_Digit.disabled=false;</SCRIPT>")
	Response.Write("<SCRIPT>self.document.forms[0].btndtcBenef.disabled=false;</SCRIPT>")
End If

If CStr(Session("OP006_sCodispl")) = "SI008" Then
	Response.Write("<SCRIPT> insParamFixval(); </SCRIPT>")
End If

If Request.QueryString.Item("sCodispl") = "OP06-2" And Request.QueryString.Item("nConcept") = "19" Then
	Response.Write("<SCRIPT> insProponum(" & Request.QueryString.Item("nProponum") & "); </SCRIPT>")
End If

If Request.QueryString.Item("sCodispl") <> "OP06-1" Then
	Response.Write("<SCRIPT>top.fraHeader.document.A301.disabled = true;</SCRIPT>")
	Response.Write("<SCRIPT>top.fraHeader.document.A302.disabled = true;</SCRIPT>")
	Response.Write("<SCRIPT>top.fraHeader.document.A303.disabled = true;</SCRIPT>")
	Response.Write("<SCRIPT>top.fraHeader.document.A401.disabled = true;</SCRIPT>")
End If

If CStr(Session("OP006_sCodispl")) = "OP091" Or CStr(Session("OP006_sCodispl")) = "CA099A" Or CStr(Session("OP006_sCodispl")) = "CO009" Then
	Response.Write("<SCRIPT>insShowconver(); </SCRIPT>")
End If

If CStr(Session("OP006_sCodispl")) = "CA099A" Then
	Response.Write("<SCRIPT>insInitialAgency(2); </SCRIPT>")
End If

%>




