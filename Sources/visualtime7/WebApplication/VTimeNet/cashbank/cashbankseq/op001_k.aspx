<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eOptionSystem" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim lstrAlert As String
Dim lblnAccess As Boolean
Dim lobjErrors As eGeneral.GeneralFunction
Dim lobjOptionSystem As eGeneral.Opt_system
Dim lintCompany As Object


'% insReaOP001: Lectura de movimientos de caja
'--------------------------------------------------------------------------------------------
Sub insReaOP001()
	'--------------------------------------------------------------------------------------------
	Dim lcolCash_mov As eCashBank.Cash_movs
	Dim lclsCash_mov As eCashBank.Cash_mov
	Dim lintCount As Integer
	Dim lobjValues As eFunctions.Values
	lobjValues = New eFunctions.Values
	lcolCash_mov = New eCashBank.Cash_movs
	lclsCash_mov = New eCashBank.Cash_mov
	With lclsCash_mov
		.nTransac = lobjValues.StringToType(Request.QueryString.Item("nTransac"), eFunctions.Values.eTypeData.etdInteger, True)
		.nMov_type = lobjValues.StringToType(Request.QueryString.Item("nMov_type"), eFunctions.Values.eTypeData.etdInteger, True)
		.dEffecdate = lobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
		.nCash_id = lobjValues.StringToType(Request.QueryString.Item("nCash_Id"), eFunctions.Values.eTypeData.etdDouble)
		.nOffice = lobjValues.StringToType(Request.QueryString.Item("nOffice"), eFunctions.Values.eTypeData.etdInteger, True)
		.dValDate = lobjValues.StringToType(Request.QueryString.Item("dValDate"), eFunctions.Values.eTypeData.etdDate)
		.nOri_Curr = lobjValues.StringToType(Request.QueryString.Item("nOri_Curr"), eFunctions.Values.eTypeData.etdInteger, True)
		.nOri_Amount = lobjValues.StringToType(Request.QueryString.Item("nOri_Amount"), eFunctions.Values.eTypeData.etdDouble)
		.nCurrency = lobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger, True)
		.nAmount = lobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble)
		.nCompany = lobjValues.StringToType(Request.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdInteger, True)
		.nConcept = lobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdInteger, True)
		.nAcc_bank = lobjValues.StringToType(Request.QueryString.Item("nAcc_Bank"), eFunctions.Values.eTypeData.etdInteger, True)
		.sDocnumbe = Request.QueryString.Item("nDocNumber")
		.sCard_num = Request.QueryString.Item("nCreditCardNumber")
		.nCard_typ = lobjValues.StringToType(Request.QueryString.Item("nCreditCardType"), eFunctions.Values.eTypeData.etdInteger, True)
		.nChequeLocat = lobjValues.StringToType(Request.QueryString.Item("nChequelocat"), eFunctions.Values.eTypeData.etdInteger, True)
		.nInputChannel = lobjValues.StringToType(Request.QueryString.Item("nInputChannel"), eFunctions.Values.eTypeData.etdInteger, True)
		.nBank_code = lobjValues.StringToType(Request.QueryString.Item("nBank"), eFunctions.Values.eTypeData.etdInteger, True)
		.nBordereaux = lobjValues.StringToType(Request.QueryString.Item("nBordereaux"), eFunctions.Values.eTypeData.etdInteger, True)
	End With
	With lcolCash_mov
		If .FindOP001(lclsCash_mov.nMov_type, lclsCash_mov.dEffecdate, lclsCash_mov.nCash_id, lclsCash_mov.nOffice, lclsCash_mov.dValDate, lclsCash_mov.nOri_Curr, lclsCash_mov.nOri_Amount, lclsCash_mov.nCurrency, lclsCash_mov.nAmount, lclsCash_mov.nCompany, lclsCash_mov.nConcept, lclsCash_mov.nAcc_bank, lclsCash_mov.sDocnumbe, lclsCash_mov.sCard_num, lclsCash_mov.nCard_typ, lclsCash_mov.nChequeLocat, lclsCash_mov.nInputChannel, lclsCash_mov.nBank_code, lclsCash_mov.nBordereaux, lclsCash_mov.nTransac, eRemoteDB.Constants.intNull, lclsCash_mov.nInsur_area) Then
			For lintCount = 1 To .Count
				Response.Write("<SCRIPT>" & "insAddOP001(""" & lobjValues.DateToString(.Item(lintCount).dEffecdate) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nTransac, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.StringToType(CStr(.Item(lintCount).nAmount), eFunctions.Values.eTypeData.etdDouble) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nCompanyc, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nIntermed, eFunctions.Values.eTypeData.etdInteger) & """," & """" & .Item(lintCount).sClient & """," & """" & lobjValues.TypeToString(.Item(lintCount).nAcc_bank, eFunctions.Values.eTypeData.etdInteger) & """," & """" & .Item(lintCount).sDocnumbe & """," & """" & lobjValues.DateToString(.Item(lintCount).dDoc_date) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nBank_code, eFunctions.Values.eTypeData.etdInteger) & """," & """" & .Item(lintCount).sCard_num & """," & """" & lobjValues.TypeToString(.Item(lintCount).nCard_typ, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.DateToString(.Item(lintCount).dCard_expir) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nTyp_acco, eFunctions.Values.eTypeData.etdInteger) & """," & """" & .Item(lintCount).sType_acc & """," & """" & .Item(lintCount).sNumForm & """," & """" & lobjValues.TypeToString(.Item(lintCount).nBordereaux, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nClaim, eFunctions.Values.eTypeData.etdDouble) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nContrat, eFunctions.Values.eTypeData.etdDouble) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nDraft, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nConcept, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nCurrency, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nOffice, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nMov_type, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nChequeLocat, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nCompany, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.StringToType(CStr(.Item(lintCount).nOri_Amount), eFunctions.Values.eTypeData.etdDouble) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nFin_int, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nInputChannel, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nCash_id, eFunctions.Values.eTypeData.etdInteger) & """," & """" & lobjValues.TypeToString(.Item(lintCount).dValDate, eFunctions.Values.eTypeData.etdDate) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nNoteNum, eFunctions.Values.eTypeData.etdDouble) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nBranch, eFunctions.Values.eTypeData.etdDouble) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nProduct, eFunctions.Values.eTypeData.etdDouble) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nProponum, eFunctions.Values.eTypeData.etdDouble) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nCod_Agree, eFunctions.Values.eTypeData.etdDouble) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nBank_Agree, eFunctions.Values.eTypeData.etdDouble) & """," & """" & lobjValues.TypeToString(.Item(lintCount).dCollection, eFunctions.Values.eTypeData.etdDate) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nOri_Curr, eFunctions.Values.eTypeData.etdDouble) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nTypesupport, eFunctions.Values.eTypeData.etdDouble) & """," & """" & lobjValues.TypeToString(.Item(lintCount).nSupport_Id, eFunctions.Values.eTypeData.etdDouble) & """," & """" & .Item(lintCount).sDigit & """," & """" & .Item(lintCount).sCliename & """," & """" & lobjValues.TypeToString(.Item(lintCount).nCase_Num, eFunctions.Values.eTypeData.etdInteger) & """" & ");</" & "Script>")
			Next 
			Response.Write("<SCRIPT> mlngCurrentIndex = 0;ShowFields(0);</" & "Script>")
		End If
	End With
	lcolCash_mov = Nothing
	lclsCash_mov = Nothing
	lobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
lobjErrors = New eGeneral.GeneralFunction
lobjOptionSystem = New eGeneral.Opt_system

mobjValues.sCodisplPage = "OP001_K"
lblnAccess = True
If Session("nCashNum") < 1 Then
	lstrAlert = "Err. 60104 " & lobjErrors.insLoadMessage(60104)
	Response.Write("<SCRIPT>alert('" & lstrAlert & "')</SCRIPT>")
	lblnAccess = False
End If
lobjErrors = Nothing

If lobjOptionSystem.find() Then
	lintCompany = lobjOptionSystem.nCompany
End If

lobjOptionSystem = Nothing

%>
<SCRIPT LANGUAGE=JavaScript>
    var mArray = []
    var mlngCurrentIndex = -1
    var sReloadPage = '<%=Request.QueryString.Item("blnReloadPage")%>';

//%ReloadPage: Dado el nro. de siniestro, se recarga la página con los valores necesarios para 
//             obtener el caso-tipo de demandante
//---------------------------------------------------------------------------------------------
function ReloadPage(){
//---------------------------------------------------------------------------------------------
	var lstrLocation = '';

	lstrLocation += document.location.href;
	lstrLocation = lstrLocation.replace(/&nClaim.*/,"");
	lstrLocation = lstrLocation + "&nClaim=" + self.document.forms[0].elements["tcnClaim"].value;
	lstrLocation = lstrLocation + "&blnReloadPage=True"
	lstrLocation = lstrLocation + "&nTransac=" + self.document.forms[0].elements["tcnTransac"].value;
    lstrLocation = lstrLocation + "&dEffecdate=" + self.document.forms[0].elements["tcdEffecDate"].value;
    lstrLocation = lstrLocation + "&nComprob=" + self.document.forms[0].elements["tcncomprob"].value;
    lstrLocation = lstrLocation + "&nMovType=" + self.document.forms[0].elements["cbeMovType"].value;
    lstrLocation = lstrLocation + "&nOffice=" + self.document.forms[0].elements["cbeOffice"].value;
    lstrLocation = lstrLocation + "&nCurrency=" + self.document.forms[0].elements["cbeCurrency"].value;
    lstrLocation = lstrLocation + "&nAmount=" + self.document.forms[0].elements["tcnAmount"].value;
    lstrLocation = lstrLocation + "&nAmounting=" + self.document.forms[0].elements["tcnAmounting"].value;
	lstrLocation = lstrLocation + "&nCompany=" + self.document.forms[0].elements["cbeCompany"].value;
    lstrLocation = lstrLocation + "&nInsur_area=" + self.document.forms[0].elements["cbeArea"].value;
    lstrLocation = lstrLocation + "&nConcept=" + self.document.forms[0].elements["valConcept"].value;
    lstrLocation = lstrLocation + "&nBussinesType=" + self.document.forms[0].elements["cbeBussiType"].value;
    lstrLocation = lstrLocation + "&nDocNumber=" + self.document.forms[0].elements["tctDocNumbe"].value;
    lstrLocation = lstrLocation + "&nChequelocat=" + self.document.forms[0].elements["cbeChequelocat"].value;
    lstrLocation = lstrLocation + "&dDocdate=" + self.document.forms[0].elements["tcdDocDate"].value;
    lstrLocation = lstrLocation + "&nBank=" + self.document.forms[0].elements["cbeBank"].value;
    lstrLocation = lstrLocation + "&nAccountBank=" + self.document.forms[0].elements["valAccBank"].value;
    lstrLocation = lstrLocation + "&nFinancInt=" + self.document.forms[0].elements["tcnFinancInt"].value;
    lstrLocation = lstrLocation + "&nInputChannel=" + self.document.forms[0].elements["cbeinway"].value;
    lstrLocation = lstrLocation + "&nBank_Agree=" + self.document.forms[0].elements["valBank_agree"].value;
    lstrLocation = lstrLocation + "&nCreditCardNumber=" + self.document.forms[0].elements["tctCardNum"].value;
    lstrLocation = lstrLocation + "&nCod_Agree=" + self.document.forms[0].elements["tcnCod_Agree"].value;
    lstrLocation = lstrLocation + "&nCreditCardType=" + self.document.forms[0].elements["cbeCardType"].value;
    lstrLocation = lstrLocation + "&dDatecollect=" + self.document.forms[0].elements["tcdDatecollect"].value;
    lstrLocation = lstrLocation + "&dCardExpir=" + self.document.forms[0].elements["tcdCardExpir"].value;
    lstrLocation = lstrLocation + "&nTypeDocsupport=" + self.document.forms[0].elements["cbeTypeDocsupport"].value;
    lstrLocation = lstrLocation + "&nFolioSupport=" + self.document.forms[0].elements["tcnFolioSupport"].value;
    lstrLocation = lstrLocation + "&nBulletins=" + self.document.forms[0].elements["tcnBulletins"].value;
    lstrLocation = lstrLocation + "&dtcClient=" + self.document.forms[0].elements["dtcClient"].value;
    lstrLocation = lstrLocation + "&nIntermed=" + self.document.forms[0].elements["valIntermed"].value;
    lstrLocation = lstrLocation + "&nValCompanyCR=" + self.document.forms[0].elements["valCompanyCR"].value;
    lstrLocation = lstrLocation + "&nNumForm=" + self.document.forms[0].elements["tcnProponum"].value;
    lstrLocation = lstrLocation + "&nBordereaux=" + self.document.forms[0].elements["tcnBordereaux"].value;
    lstrLocation = lstrLocation + "&nContract=" + self.document.forms[0].elements["tcnContract"].value;
    lstrLocation = lstrLocation + "&nDraft=" + self.document.forms[0].elements["tcnDraft"].value;
	document.location.href = lstrLocation;
}
//ChangeValues: Cambia y asigna los valores según la opción seleccionada.
//Enlace NovaRed.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ChangeValues(){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
    	if(dtcClient.value!=""){
    		insDefValuesNR('Client', 'sClient=' + dtcClient.value, 'sDigit=' + dtcClient_Digit.value , 'sForm=' + self.document.forms[0].name,'/VTimeNet/CashBank/CashBankSeq')
            dtcClient_Digit.disabled=true;
        }
    }
}
//%	insAddOP001: Carga el arreglo
//-------------------------------------------------------------------------------------------
function insAddOP001(dEffecdate   , nTransac    ,
					 nAmount      , nCompanyc   ,
					 nIntermed    , sClient     ,
					 nAcc_bank    , sDocnumbe   ,
					 dDoc_date    , nBank_code  ,
					 sCard_num    , nCard_typ   ,
					 dCard_expir  , nTyp_acco   ,
					 sType_acc    , sNumForm    ,
					 nBordereaux  , nClaim      ,
					 nContrat     , nDraft      ,
					 nConcept     , nCurrency   ,
					 nOffice      , nMov_type   ,
					 nChequeLocat , nCompany    ,
					 nOri_Amount  , nFin_int    ,
					 nInputChannel, nCash_id    ,
					 dValDate     , nNoteNum    ,
					 nBranch      , nProduct    ,
					 nProponum    , nCod_Agree  ,
					 nBank_Agree  , dCollection ,
                     nOri_Curr    , nTypesupport,
                     nSupport_Id  , sDigit      ,
                     sCliename    , nCase_num){
//-------------------------------------------------------------------------------------------
    var ludtOP001 = []

    ludtOP001[0]  = dEffecdate;
    ludtOP001[1]  = dEffecdate;
    ludtOP001[2]  = nTransac;
    ludtOP001[3]  = nAmount;
    ludtOP001[4]  = nCompanyc;
    ludtOP001[5]  = nIntermed;
    ludtOP001[6]  = sClient;
    ludtOP001[7]  = nAcc_bank;
    ludtOP001[8]  = sDocnumbe;
    ludtOP001[9]  = dDoc_date;
    ludtOP001[10] = nBank_code;
    ludtOP001[11] = sCard_num;
    ludtOP001[12] = nCard_typ;
    ludtOP001[13] = dCard_expir;
    ludtOP001[14] = nTyp_acco;
    ludtOP001[15] = sType_acc;
    ludtOP001[16] = sNumForm;
    ludtOP001[17] = nBordereaux;
    ludtOP001[18] = nClaim;
    ludtOP001[19] = nContrat;
    ludtOP001[20] = nDraft;
    ludtOP001[21] = nConcept;
    ludtOP001[22] = nCurrency;
    ludtOP001[23] = nMov_type;
    ludtOP001[24] = nOffice;
    ludtOP001[25] = nChequeLocat;
    ludtOP001[26] = nCompany;
    ludtOP001[27] = nOri_Amount;
    ludtOP001[28] = nFin_int;
    ludtOP001[29] = nInputChannel;
    ludtOP001[30] = nCash_id;        
    ludtOP001[31] = dValDate;
	ludtOP001[32] = nNoteNum;
	ludtOP001[33] = nBranch;
	ludtOP001[34] = nProduct;
	ludtOP001[35] = nProponum;
    ludtOP001[36] = nCod_Agree;
    ludtOP001[37] = nBank_Agree;
    ludtOP001[38] = dCollection;
    ludtOP001[39] = nOri_Curr;
    ludtOP001[40] = nTypesupport;
    ludtOP001[41] = nSupport_Id;
    ludtOP001[42] = sDigit;
    ludtOP001[43] = sCliename;
    ludtOP001[44] = nCase_num;
    mArray[++mlngCurrentIndex] = ludtOP001;
}
//%	ShowFields: Asigna los valores obtenidos a través de la consulta por condición
//-------------------------------------------------------------------------------------------
function ShowFields(lintIndex){
//-------------------------------------------------------------------------------------------
    document.forms[0].elements["tcdEffecdate"].value = mArray[lintIndex][0];
    document.forms[0].elements["tcnTransac"].value   = mArray[lintIndex][2];
    document.forms[0].elements["tcnAmountIng"].value = mArray[lintIndex][3];
    document.forms[0].elements["valCompanyCR"].value = mArray[lintIndex][4];
    document.forms[0].elements["valIntermed"].value = mArray[lintIndex][5];
    document.forms[0].elements["dtcClient"].value = mArray[lintIndex][6];
    document.forms[0].elements["valAccBank"].value = mArray[lintIndex][7];
    document.forms[0].elements["tctDocNumbe"].value = mArray[lintIndex][8];
    document.forms[0].elements["tcdDocDate"].value = mArray[lintIndex][9];
    document.forms[0].elements["cbeBank"].value = mArray[lintIndex][10];
	document.forms[0].elements["tctCardNum"].value = mArray[lintIndex][11];
    document.forms[0].elements["cbeCardType"].value = mArray[lintIndex][12];
    document.forms[0].elements["tcdCardExpir"].value = mArray[lintIndex][13];
    document.forms[0].elements["valCurrAcc"].value = mArray[lintIndex][14];
    if(typeof(top.fraHeader.document.forms[0].cbeBussiType)!='undefined')
        document.forms[0].elements["cbeBussiType"].value = mArray[lintIndex][15];
    document.forms[0].elements["tcnProponum"].value = mArray[lintIndex][16];
    document.forms[0].elements["tcnBordereaux"].value = mArray[lintIndex][17];
    document.forms[0].elements["tcnClaim"].value = mArray[lintIndex][18];
    document.forms[0].elements["tcnContract"].value = mArray[lintIndex][19];
    document.forms[0].elements["tcnDraft"].value = mArray[lintIndex][20];
    document.forms[0].elements["valConcept"].value = mArray[lintIndex][21];
    document.forms[0].elements["cbeCurrency"].value = mArray[lintIndex][22];
    document.forms[0].elements["cbeMovType"].value = mArray[lintIndex][23];
    document.forms[0].elements["cbeOffice"].value = mArray[lintIndex][24];
    document.forms[0].elements["cbeChequelocat"].value = mArray[lintIndex][25];
    document.forms[0].elements["cbeCompany"].value = mArray[lintIndex][26];
    document.forms[0].elements["tcnAmount"].value = mArray[lintIndex][27];
    document.forms[0].elements["tcnFinancInt"].value = mArray[lintIndex][28];
    document.forms[0].elements["cbeinway"].value = mArray[lintIndex][29];
    document.forms[0].elements["tcncomprob"].value = mArray[lintIndex][30];
    document.forms[0].elements["tcdValorDate"].value = mArray[lintIndex][31];
	document.forms[0].elements["tcnNoteNum"].value = mArray[lintIndex][32];
	document.forms[0].elements["cbeBranch"].value = mArray[lintIndex][33];
	document.forms[0].elements["valProduct"].Parameters.Param1.sValue = mArray[lintIndex][33];
	document.forms[0].elements["valProduct"].value = mArray[lintIndex][34];
	document.forms[0].elements["tcnProponum"].value = mArray[lintIndex][35];
	document.forms[0].elements["tcnCod_Agree"].value = mArray[lintIndex][36];
	document.forms[0].elements["valBank_agree"].value = mArray[lintIndex][37];
	document.forms[0].elements["tcdDatecollect"].value = mArray[lintIndex][38];
	document.forms[0].elements["cbeTypeDocsupport"].value = mArray[lintIndex][40];
	document.forms[0].elements["tcnFolioSupport"].value = mArray[lintIndex][41];
	document.forms[0].elements["dtcClient_Digit"].value = mArray[lintIndex][42];    
    UpdateDiv('lblCliename',mArray[lintIndex][43],'Normal');
    document.forms[0].elements["tcnCaseNumber"].value = mArray[lintIndex][44];
	
//* Activa el evento "change" de los "PossiblesValues" para cuando la acción sea "Consulta por condición"
	ShowPopUp('/VTimeNet/CashBank/CashBankSeq/ShowDefValues.aspx?Field=ActivateOnBlur' + '&nCompany=' +  document.forms[0].elements['cbeCompany'].value, 'ShowDefValues', 1, 1,'no','no',2000,2000);

	if(top.frames['frasequence'].plngMainAction == 402){
		A390.disabled=true;
		A392.disabled=true;
		A301.disabled=true;
 		A402.disabled=true;
		A303.disabled=false;
    }
}
//%EnabledFields: Habilita los campos de acuerdo a la acción.
//--------------------------------------------------------------------------------------------
function EnabledFields(){
//--------------------------------------------------------------------------------------------
	if(top.frames['frasequence'].plngMainAction != 303)
		with(document.forms[0]){
			tcdEffecDate.disabled = false;
			btn_tcdEffecDate.disabled = false;
			cbeMovType.disabled = false;
			cbeOffice.disabled = false;
			cbeCurrency.disabled = false;
			tcnAmount.disabled = false;
			tcnAmounting.disabled = false;
			cbeCompany.disabled = false;
			cbeArea.disabled=false;
			tcdValorDate.disabled = false;
			btn_tcdValorDate.disabled = false;
			cbeCurrencying.disabled = false;
			cbeinway.disabled = false;
			tcnBordereaux.disabled = false;
        if (sReloadPage!='True'){
			tcnAmounting.value = '';
			cbeArea.value='';
			tcnFinancInt.value='';
		}
	}
		if(top.frames['frasequence'].plngMainAction == 402){
			document.forms[0].elements["tcncomprob"].disabled = false;
			document.forms[0].elements["tcnTransac"].disabled = false;
			document.forms[0].elements["tcdValorDate"].value = '';
			document.forms[0].elements["tcnAmounting"].value = '';
			document.forms[0].elements["cbeOffice"].value = '';
			document.forms[0].elements["tcdEffecDate"].value = '';
			document.forms[0].elements["cbeCurrencying"].value = '';
			document.forms[0].elements["cbeBussiType"].value = '';
			document.forms[0].elements["cbeCompany"].value = '';			
		}
}
//% insDisableAll: Permite deshabilitar todos los controles de la ventana.
//-----------------------------------------------------------------------
function insDisableAll(){
//-----------------------------------------------------------------------
    var lintIndex = 0;
    with(self.document.forms[0]){
        for (lintIndex=0;lintIndex < document.forms[0].length;lintIndex++)
			elements[lintIndex].disabled=true
		A301.disabled=true;
		A303.disabled=true;
		A402.disabled=true;
	}
}
//%	insShowConver: Cuando se cambia  el monto origen
//-------------------------------------------------------------------------------------------
function insShowConver(Field){
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
		if (cbeCurrencying.value!=0 && cbeCurrency.value!=0)
			insDefValues("ConvertAmounting", "nCurrency_ing=" + cbeCurrencying.value + "&nAmount=" + Field.value + "&nCurrency=" + cbeCurrency.value + "&dDocDate=" + tcdDocDate.value + "&dReqDate=" + tcdValorDate.value + "&nAmounting=" + tcnAmounting.value, "/VTimeNet/CashBank/CashBankSeq");
     }
     ShowFinanInt("Amount");
}
//%	inShowAmouting: Cuando se cambia  de la moneda de ingreso o la fecha de valorización
//-------------------------------------------------------------------------------------------
function inShowAmouting(Field,sChange){ 
//-------------------------------------------------------------------------------------------
	switch(sChange){
		case "Currency":
	        with (self.document.forms[0]){
	            if (cbeCurrency.value!=0 && tcnAmount.value!=0)
	        	    insDefValues("ConvertAmounting", "nCurrency_ing=" + Field.value + "&nAmount=" + tcnAmount.value + "&nCurrency=" + cbeCurrency.value + "&dDocDate=" + tcdDocDate.value + "&dReqDate=" + tcdValorDate.value + "&nAmounting=" + tcnAmounting.value, "/VTimeNet/CashBank/CashBankSeq");
		    }	
		break;
	    case "Valuedate":
	        with (self.document.forms[0]){
	            if (cbeCurrency.value!=0 && tcnAmount.value!=0)
	        	    insDefValues("ConvertAmounting", "nCurrency_ing=" + cbeCurrencying.value + "&nAmount=" + tcnAmount.value + "&nCurrency=" + cbeCurrency.value + "&dDocDate=" + tcdDocDate.value + "&dReqDate=" + Field.value + "&nAmounting=" + tcnAmounting.value, "/VTimeNet/CashBank/CashBankSeq");
		    }
		break;
	}
}
//%	insShowCurrency: Cuando se cambia  de la moneda origina
//-------------------------------------------------------------------------------------------
function insShowCurrency(Field){
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
		if (cbeCurrencying.value!=0 && tcnAmount.value!=0)
		    insDefValues("ConvertAmounting", "nCurrency_ing=" + cbeCurrencying.value + "&nAmount=" + tcnAmount.value + "&nCurrency=" + Field.value + "&dDocDate=" + tcdDocDate.value + "&dReqDate=" + tcdValorDate.value + "&nAmounting=" + tcnAmounting.value, "/VTimeNet/CashBank/CashBankSeq");
    }
}
//%	insShowDifference: Muestra la diferencia entre el monto ingreso y el introducido por el usuario
//-------------------------------------------------------------------------------------------
function insShowDifference(Field){ 
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
	    if(cbeCurrency.value!=0)
		    insDefValues("ShowDifference", "nCurrency_ing=" + cbeCurrencying.value + "&nAmount=" + tcnAmount.value + "&nCurrency=" + cbeCurrency.value + "&dDocDate=" + tcdDocDate.value + "&dEffecDate=" + tcdEffecDate.value + "&nAmounting=" + tcnAmounting.value , "/VTimeNet/CashBank/CashBankSeq");
	}
	ShowFinanInt("Amounting");
}
//insCalcInter: Calcula el interes generado por el cheque a fecha
//--------------------------------------------------------------------------------------------
function insCalcInter(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		insDefValues("insCalcInter", "nAmounting=" + tcnAmounting.value + "&dDocDate=" + tcdDocDate.value + "&dEffecDate=" + tcdEffecDate.value, "/VTimeNet/CashBank/CashBankSeq");
    }
}
//%	insPolicyByPolicy: Muestra la diferencia entre el monto ingreso y el introducido por el usuario
//-------------------------------------------------------------------------------------------
function insPolicyByPolicy(Field){ 
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
	    if(tcnProponum.value!=0 && 
	       tcnProponum.value!='')
		    insDefValues("PolicyByPolicy", "sCertype=1&nPolicy=" + tcnProponum.value);
		else{
			cbeBranch.value='';
			valProduct.value='';
			UpdateDiv('valProductDesc', '');
		}
	}
}
//%	insShowClient: Muestra el cliente asociado al convenio
//-------------------------------------------------------------------------------------------
function insShowClient(){ 
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if(tcnCod_Agree.value!=0 &&
		   tcnCod_Agree.value!='')
	         insDefValues("ClientAgreement", "sAgreement=" + tcnCod_Agree.value);
	}
}

</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>






	<%=mobjValues.StyleSheet()%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 21/01/03 12:11p $|$$Author: Nvaplat20 $";
//ShowNotesPopUp1: Llama a la ventana de notas
//------------------------------------------------------------------------------------------------
function ShowNotesPopUp1(){
//--------------------------------------------------------------------------------------------------------------------------------
	if (top.frames["fraSequence"].plngMainAction == 402)
		ShowNotesPopUp("SCA2-I",document.forms[0].elements["tcnNoteNum"].value,401,0,0,0,"");
	else
		ShowNotesPopUp("SCA2-I",document.forms[0].elements["tcnNoteNum"].value,303,0,0,0,"");
}
//%insStateZone: Habilita/deshabilita los campos de la ventana
//-------------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------------
	if(sReloadPage=='')
	    EnabledFields();
	else
		if(sReloadPage=='False')
		    EnabledFields();
		else{
			var lstrLocation = self.document.location.href;
			lstrLocation=lstrLocation.replace(/blnReloadPage=True/,'blnReloadPage=False');
			self.document.location.href=lstrLocation;
		}
}
//%insCancel: Controla la acción "Cancelar" de la página
//-------------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------------
	return true;
}
//%insFinish: Controla la acción "Finalizar" de la página
//-------------------------------------------------------------------------------------------------
function insFinish(){
//-------------------------------------------------------------------------------------------------  
    return true;
}
//% ShowFinanInt: Cálcula el interés generado por el cheque a fecha
//----------------------------------------------------------------
function ShowFinanInt(sField){
//----------------------------------------------------------------
	switch(sField){
		case "Amount":
	        if(self.document.forms[0].cbeMovType.value == '10' &&
	           self.document.forms[0].tcnAmount.value != '' &&
	           self.document.forms[0].tcdDocDate.value != '')
                insDefValues("FinanInt","nAmount_Cheq=" + self.document.forms[0].tcnAmount.value +
                             "&dDoc_date=" + self.document.forms[0].tcdDocDate.value + "&dEffecdate=" +
                             self.document.forms[0].tcdEffecDate.value,"/VTimeNet/CashBank/CashBankSeq")
			break;
	    case "Amounting":
	        if(self.document.forms[0].cbeMovType.value == '10' &&
	           self.document.forms[0].tcnAmount.value != '' &&
	           self.document.forms[0].tcdDocDate.value != '')
	            insDefValues("FinanInt","nAmount_Cheq=" + self.document.forms[0].tcnAmounting.value +
					         "&dDoc_date=" + self.document.forms[0].tcdDocDate.value + "&dEffecdate=" +
					         self.document.forms[0].tcdEffecDate.value,"/VTimeNet/CashBank/CashBankSeq")
			break;
	}
}
//%insChangeMovType: Se habilitan/deshabilitan campos y se asignan valores según el tipo de movimiento
//----------------------------------------------------------------------------------------------------
function insChangeMovType(){
//----------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
	    if (cbeMovType.value == "10") 
			tcnFinancInt.disabled = false;
        else{
			tcnFinancInt.disabled = true;
			tcnFinancInt.value = "";
		}
		if (tctDocNumbe.disabled) tctDocNumbe.value = "";
		tcdDocDate.disabled = cbeMovType.value == "1";
		btn_tcdDocDate.disabled = tcdDocDate.disabled;
		cbeChequelocat.disabled=!(cbeMovType.value == "2" || cbeMovType.value == "16" || cbeMovType.value == "10") ;
		tctDocNumbe.disabled = (cbeMovType.value == "1") ;
		if (cbeChequelocat.disabled) cbeChequelocat.value="";
		if (tcdDocDate.disabled) tcdDocDate.value = "";
		cbeBank.disabled = cbeMovType.value == "1" || cbeMovType.value == "4";
		if (cbeBank.disabled) cbeBank.val
        if (cbeMovType.value == "4" || cbeMovType.value == "6" || cbeMovType.value == "7" || cbeMovType.value == "8" || cbeMovType.value == "3"  || cbeMovType.value == "9"){
			valAccBank.disabled = false;
			btnvalAccBank.disabled = false;
		}
		else{
			valAccBank.disabled = true;
			btnvalAccBank.disabled = true;
		}
		if (valAccBank.disabled){
		    valAccBank.value = "";
		    UpdateDiv('valAccBankDesc','','Normal');
		}
		tctCardNum.disabled = cbeMovType.value != "5";
		if (tctCardNum.disabled) tctCardNum.value = "";
		cbeCardType.disabled = cbeMovType.value != "5";
		if (cbeCardType.disabled) cbeCardType.value = "";
		tcdCardExpir.disabled = cbeMovType.value != "5";
		btn_tcdCardExpir.disabled = tcdCardExpir.disabled;
		if (tcdCardExpir.disabled) tcdCardExpir.value = "";
	}
}
//insChangeConcept: Se habilitan/deshabilitan campos según el concepto en tratamiento.
//--------------------------------------------------------------------------------------------------
function insChangeConcept(){
//--------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){

		if (top.frames['frasequence'].plngMainAction != 402){ 
		    valCurrAcc.value = "";
		    UpdateDiv('valCurrAccDesc','','Normal');
		}

        if (top.frames['frasequence'].plngMainAction == 402)
		    dtcClient.disabled = true;
		btndtcClient.disabled = dtcClient.disabled;
		dtcClient_Digit.disabled =dtcClient.disabled;

		if (dtcClient.disabled && top.frames['frasequence'].plngMainAction != 402) dtcClient.value = "";

		valIntermed.disabled = valConcept.value != "2" && valConcept.value != "25" && valConcept.value != "26";
		btnvalIntermed.disabled = valIntermed.disabled;

		if (valIntermed.disabled)
			valIntermed.value = "";
			
		valCompanyCR.disabled = valConcept.value != "3";
		btnvalCompanyCR.disabled = valCompanyCR.disabled;
		if (valCompanyCR.disabled) valCompanyCR.value = "";
		tcnProponum.disabled = (valConcept.value != "26" && valConcept.value != "34") || top.frames['frasequence'].plngMainAction == 402 ;
		if (tcnProponum.disabled && top.frames['frasequence'].plngMainAction != 402) tcnProponum.value = "";
        if (top.frames['frasequence'].plngMainAction != 402){
            cbeBranch.disabled = valConcept.value != "26"
            valProduct.disabled = valConcept.value != "26"
            btnvalProduct.disabled = valConcept.value != "26"
        }
        else{
            cbeBranch.disabled = true;
            valProduct.disabled = true;
            btnvalProduct.disabled = true;
        }
		tcnBordereaux.disabled = valConcept.value != "1" && valConcept.value != "2" && valConcept.value != "3" && valConcept.value != "33" || top.frames['frasequence'].plngMainAction == 402;
		if (tcnBordereaux.disabled && top.frames['frasequence'].plngMainAction != 402) tcnBordereaux.value = "";
		tcnClaim.disabled = valConcept.value != "4" && valConcept.value != "30" && valConcept.value != "31" && valConcept.value != "32" || top.frames['frasequence'].plngMainAction == 402;
		//if (tcnClaim.disabled) tcnClaim.value = "";
		tcnContract.disabled = valConcept.value != "6" && valConcept.value != "7" && nCollect_P == "2" || top.frames['frasequence'].plngMainAction == 402;
		if (tcnContract.disabled && top.frames['frasequence'].plngMainAction != 402) tcnContract.value = "";
		tcnDraft.disabled = (valConcept.value == "6" || valConcept.value == "7"  && nCollect_P == "2") || top.frames['frasequence'].plngMainAction == 402;
		if (tcnDraft.disabled && top.frames['frasequence'].plngMainAction != 402) tcnDraft.value = "";
		tcnBulletins.disabled = valConcept.value != "35" ;
		if (tcnBulletins.disabled) tcnBulletins.value = "";
        if (top.frames['frasequence'].plngMainAction != 402)
		   valBank_agree.disabled = valConcept.value != "36" && valConcept.value != "29";
		else
		   valBank_agree.disabled = true;
		btnvalBank_agree.disabled=valBank_agree.disabled
		if(valConcept.value != "29"){
			if(valBank_agree.disabled){
			    valBank_agree.value="";
			    UpdateDiv('valBank_agreeDesc','','Normal');
			}
        }
        
        if(valConcept.value == "29")//PAC/Transbank
            valBank_agree.Parameters.Param1.sValue=1;
		else if(valConcept.value == "36")//Pago en ventanilla
			valBank_agree.Parameters.Param1.sValue=2;
		if (top.frames['frasequence'].plngMainAction != 402)
		   tcnCod_Agree.disabled= valConcept.value != "36" && valConcept.value != "38" && valConcept.value != "29"
		else
		   tcnCod_Agree.disabled= true;
        if (tcnCod_Agree.disabled && top.frames['frasequence'].plngMainAction != 402) tcnCod_Agree.value="";
		cbeBank.disabled = valConcept.value == "36"  || top.frames['frasequence'].plngMainAction == 402 || cbeMovType.value == "1" || cbeMovType.value == "4";
		if (cbeBank.disabled && top.frames['frasequence'].plngMainAction != 402) cbeBank.value = "";
        if (top.frames['frasequence'].plngMainAction != 402){
		    cbeTypeDocsupport.disabled = ((tcnClaim.disabled) && (tcnDraft.disabled) && (tcnBordereaux.disabled) && (valCompanyCR.disabled) && (valIntermed.disabled) && (dtcClient.disabled))
		    tcnFolioSupport.disabled = ((tcnClaim.disabled) && (tcnDraft.disabled) && (tcnBordereaux.disabled) && (valCompanyCR.disabled) && (valIntermed.disabled) && (dtcClient.disabled))
		}
		else{
			cbeTypeDocsupport.disabled = true;
		    tcnFolioSupport.disabled = true;
		}
		tcdDatecollect.disabled=(valConcept.value != "36" && valConcept.value != "38" && valConcept.value != "29") || top.frames['frasequence'].plngMainAction == 402;
		btn_tcdDatecollect.disabled=tcdDatecollect.disabled
		cbeBussiType.disabled=valConcept.value != "3" && valConcept.value != "10" || top.frames['frasequence'].plngMainAction == 402;
		if (cbeBussiType.disabled) cbeBussiType.value="";

		if(valConcept.value == "2"){
			valCurrAcc.TypeList="1";
			valCurrAcc.List="1,10";
		}
        if(valConcept.value == "3"){
			valCurrAcc.TypeList="1";
			valCurrAcc.List="2,3,8";
		}
        if(valConcept.value != "2" && valConcept.value != "3"){
			valCurrAcc.TypeList="2";
			valCurrAcc.List="9";
		}
		
		if(valConcept.value == "26" || valConcept.value == "33"){
			valCurrAcc.TypeList="1";
			valCurrAcc.List="5";
			valCurrAcc.value ="5";
		}

        if (top.frames['frasequence'].plngMainAction == 402){
		   valCurrAcc.disabled = true;
		   btnvalCurrAcc.disabled = valCurrAcc.disabled;
		}   
        else{
		   valCurrAcc.disabled = valConcept.value != "2" && valConcept.value != "3" && valConcept.value != "10";
		   btnvalCurrAcc.disabled = valCurrAcc.disabled;
		}


        if((valConcept.value == "29" || valConcept.value == "36") && top.frames['frasequence'].plngMainAction == 402)
           $(valBank_agree).change();
		if((valConcept.value == "29" || valConcept.value == "36") && top.frames['frasequence'].plngMainAction != 402) {
			valAccBank.value ="";
			valBank_agree.value ="";
			tcnCod_Agree.value ="";
			cbeBank.value ="";
			UpdateDiv('valAccBankDesc','','Normal');
			UpdateDiv('valBank_agreeDesc','','Normal');
		}
        insDefValues("Valuedate", "nConcept=" + valConcept.value + "&nAmount=" + tcnAmount.value + "&nCurrency=" + cbeCurrency.value +  "&nCurrency_ing=" + cbeCurrencying.value + "&dDocDate=" + tcdDocDate.value + "&dReqDate=" + tcdValorDate.value + "&nAction=" + top.frames['frasequence'].plngMainAction + "&nAmounting=" + tcnAmounting.value, "/VTimeNet/CashBank/CashBankSeq");
	}
}
//insChangeCurrAcc: Se habilitan/deshabilitan campos según la cuenta corriente en tratamiento.
//-------------------------------------------------------------------------------------------------------
function insChangeCurrAcc(){
//-------------------------------------------------------------------------------------------------------
//
//
//
//            
//
//
//					PROBLEMAS CON SORCESAFE
//
//
//
//					BORRA EL CODIGO DE ESTAS LINEAS.
//
//
//                  NO BORRAR LA
//
//
//					PROBLEMAS CON SORCESAFE
//
//
//
//					BORRA EL CODIGO DE ESTAS LINEAS.
//
//
//
//                  NO BORRAR LAS LINEAS EN BLANCO
//
//
//					PROBLEMAS CON SORCESAFE
//
//
//
//					BORRA EL CODIGO DE ESTAS LINEAS.
//
//
//
//
//
//
//
//
//
//
//
//                  NO BORRAR LAS LINEAS EN BLANCO
//
//
//					PROBLEMAS CON SORCESAFE
//
//
//
//					BORRA EL CODIGO DE ESTAS LINEAS.
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
	with (self.document.forms[0]){
		valIntermed.disabled = valCurrAcc.value != "1" && valCurrAcc.value != "10";
		btnvalIntermed.disabled = valIntermed.disabled;
		if (valIntermed.disabled) valIntermed.value = "";
		valCompanyCR.disabled = valCurrAcc.value != "2" && valCurrAcc.value != "3" && valCurrAcc.value != "8";
		btnvalCompanyCR.disabled = valCompanyCR.disabled;
		if (valCompanyCR.disabled) valCompanyCR.value = "";
	}
}
//
//
//
//
//                  NO BORRAR LAS LINEAS EN BLANCO
//
//
//					PROBLEMAS CON SORCESAFE
//
//
//
//					BORRA EL CODIGO DE ESTAS LINEAS.
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//        
//
//
//					PROBLEMAS CON SORCESAFE
//
//
//
//					BOR
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//                  NO BORRAR LAS LINEAS EN BLANCO
//
//
//					PROBLEMAS CON SORCESAFE
//
//
//
//					BORRA EL CODIGO DE ESTAS LINEAS.
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//                  NO BORRAR LAS 
//
//
//					PROBLEMAS CON SORCESAFE
//
//
//
//					BORRA EL CODIGO DE ESTAS LINEAS.
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//

//
//
//                  NO BORRAR LAS LINEAS EN BLANCO
//
//
//					PROBLEMAS CON SORCESAF
//
//
//
//					BORRA EL CODIGO DE ESTAS LINEAS.
//
//
//
//
//
//
//
//
//
//
//

//%EnabledConcept: Habilita y deshabilita el campo "Concepto" dependiendo del valor del campo "Compañía"
//-------------------------------------------------------------------------------------------------------
function EnabledConcept(Field){
//-------------------------------------------------------------------------------------------------------	
	if(Field.value!=0)
	{
		self.document.forms[0].valConcept.Parameters.Param1.sValue=self.document.forms[0].cbeCompany.value;
		self.document.forms[0].valConcept.disabled=false;
		self.document.forms[0].btnvalConcept.disabled=false;
		if(nCollect_P=="1"){
			self.document.forms[0].valConcept.TypeList="2";
			self.document.forms[0].valConcept.List="1";
		}
	}
	else
	{
		self.document.forms[0].valConcept.disabled=true;
		self.document.forms[0].btnvalConcept.disabled=true;
		self.document.forms[0].valConcept.value='';
		UpdateDiv('valConceptDesc','','Normal');
	}
}
//%ChangeCaseNumber: Extrae el nro. de caso, demandante y cliente de la cadena obtenida
//---------------------------------------------------------------------------------------------
function ChangeCaseNumber(Field)
//---------------------------------------------------------------------------------------------
{
	var lstrCase_num = '';
	var lstrDeman_type = '';
	var lstrClient = '';
	var lstrString = '';
	var lstrLocation = '';
	lstrString += Field.value
	lstrCase_num = lstrString.substring(0,(lstrString.indexOf("/")))
	lstrDeman_type = lstrString.substr(lstrString.indexOf("/")+1,1)
	lstrClient += lstrString.replace(/.*\//,"")
	lstrLocation += document.location.href
	lstrLocation = lstrLocation.replace(/&nCase_num.*/,"")
	lstrLocation = lstrLocation + "&nCase_num=" + lstrCase_num + "&nDeman_type=" + lstrDeman_type + "&sClient=" + lstrClient + "&nCaseNumber=" + Field.value;
	self.document.forms[0].elements["cbeCaseNumber_AUX"].value = lstrCase_num;
	self.document.forms[0].elements["tcnDeman_type_h"].value = lstrDeman_type;
}
//%ChangedEffecdate: Cambio de fecha de efecto
//-----------------------------------------------------------------------------
function ChangedEffecdate()
//-----------------------------------------------------------------------------
{     
	insDefValues('ValCash_dEffecdate','sCodispl=OP001&dEffecdate=' + self.document.forms[0].tcdEffecDate.value + '&nPage=OP001','/VTimeNet/CashBank/CashBankSeq');
}
//% AddClaimParameter: Actualiza el Valor del Parametro para el control de Casos 
//%                    de Siniestros y la Ubicación
//-----------------------------------------------------------------------------
function AddClaimParameter(nValue){
//-----------------------------------------------------------------------------

	if (nValue!=0)
		insDefValues('UpdateCase','nClaim=' + nValue + '&nPage=OP001','/VTimeNet/CashBank/CashBankSeq');
	else{
		self.document.forms[0].cbeCaseNumber.options.length=0;
		self.document.forms[0].cbeCaseNumber.disabled=true;	
        self.document.forms[0].cbeCaseNumber_AUX.value="";
        self.document.forms[0].tcnDeman_type_h.value="";
		}
}
//%GetCod_Agree: Obtiene el código interno de la cuenta bancaria para asignarla al número de convenio 
//               Si los conceptos son: Pago en ventanilla o Deposito PAC/Transbank
//-------------------------------------------------------------------------------------------------------------------------
function GetCod_Agree(){
//-------------------------------------------------------------------------------------------------------------------------
    if(self.document.forms[0].valConcept.value == "36" || self.document.forms[0].valConcept.value == "29")
        insDefValues("Cod_Agree", "nBank_Agree=" + self.document.forms[0].valBank_agree.value + "&nConcept=" + self.document.forms[0].valConcept.value, '/VTimeNet/CashBank/CashBankSeq');
}

</SCRIPT>
    <%
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("OP001", "OP001.aspx", 1, ""))
Response.Write("<BR><BR><BR>")
Response.Write(mobjValues.ShowWindowsName("OP001"))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmIncommingCash" ACTION="valCashBankSeq.aspx?Zone=1">
    <P ALIGN="Center">
		<LABEL><A HREF="#Documento"> <%= GetLocalResourceObject("AnchorDocumentoCaption") %></LABEL></A><LABEL> | </LABEL>
        <LABEL><A HREF="#informaciónconvenio"><%= GetLocalResourceObject("AnchorinformaciónconvenioCaption") %></A><LABEL> | </LABEL>
        <LABEL><A HREF="#Soporte"> <%= GetLocalResourceObject("AnchorSoporteCaption") %></LABEL></A><LABEL> | </LABEL>
        <LABEL><A HREF="#Tarjeta de crédito"> <%= GetLocalResourceObject("AnchorTarjeta de créditoCaption") %></A><LABEL> | </LABEL>
        <LABEL><A HREF="#Financiamiento"> <%= GetLocalResourceObject("AnchorFinanciamientoCaption") %></LABEL></A></LABEL>
	</P>
    <TABLE WIDTH="100%" BORDER=0>
	<%If Request.QueryString.Item("blnReloadPage") = "True" Then%>
	    <TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnTransacCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnTransac", 4, Request.QueryString.Item("nTransac"),  , GetLocalResourceObject("tcnTransacToolTip"), False, 0,  ,  ,  ,  , True, 1)%></TD>

            <TD><LABEL><%= GetLocalResourceObject("tcdEffecDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecDate", Request.QueryString.Item("dEffecdate"),  , GetLocalResourceObject("tcdEffecDateToolTip"),  ,  ,  , "ChangedEffecdate();", True, 2)%></TD>
            <TD><%=mobjValues.TextControl("lblTime", 10, "",  , "", True,  ,  ,  , True, 3)%></TD>

        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("tcncomprobCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcncomprob", 10, Request.QueryString.Item("nComprob"),  , GetLocalResourceObject("tcncomprobToolTip"), False, 0,  ,  ,  ,  , True, 4)%></TD>

			<TD><LABEL><%= GetLocalResourceObject("cbeMovTypeCaption") %></LABEL></TD>
			<TD><%	mobjValues.BlankPosition = True
	Response.Write(mobjValues.PossiblesValues("cbeMovType", "Table78", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nMovType"),  ,  ,  ,  ,  , "ChangedEffecdate();insChangeMovType();", True,  , GetLocalResourceObject("cbeMovTypeToolTip"),  , 4))%></TD>
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
            <TD><%	 
        mobjValues.Parameters.Add("nUsercode", CDbl(Session("nUsercode")), 1, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, 64)
		mobjValues.BlankPosition = False
        Response.Write(mobjValues.PossiblesValues("cbeOffice", "tabOfficeUser", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(Request.QueryString.Item("nOffice"), eFunctions.Values.eTypeData.etdDouble), True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOfficeToolTip"),  , 5))
		mobjValues.BlankPosition = True
	        %>
            </TD>
            <TD><LABEL><%= GetLocalResourceObject("tcdValorDateCaption") %></LABEL></TD>
            <TD><%=	mobjValues.DateControl("tcdValorDate", CStr(Today),  , GetLocalResourceObject("tcdValorDateToolTip"),  ,  ,  , "inShowAmouting(this,""Valuedate"");", True, 7)%></TD>
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
			<TD><%	Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nCurrency"),  ,  ,  ,  ,  , "ShowFinanInt(""Amounting"")", True,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 6))%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcnAmountCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAmount", 18, mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnAmountToolTip"), True, 6,  ,  ,  , "insShowConver(this);ShowFinanInt(""Amount"");", True, 7)%></TD>
        </TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeCurrencyingCaption") %></LABEL></TD>
			<TD><%	Response.Write(mobjValues.PossiblesValues("cbeCurrencying", "TabCurrency_b", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  , "inShowAmouting(this,""Currency"");", True,  , GetLocalResourceObject("cbeCurrencyingToolTip"),  , 8))%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcnAmountingCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAmounting", 18, mobjValues.StringToType(Request.QueryString.Item("nAmounting"), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnAmountingToolTip"), True, 6,  ,  ,  , "insShowConver(this);", True, 9)%></TD>
        </TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnDiferenceCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnDiference", 18, CStr(0),  , GetLocalResourceObject("tcnDiferenceToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        </TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeCompanyCaption") %></LABEL></TD>
			
			<%	If Request.QueryString.Item("nCompany") <> vbNullString Then
		lintCompany = Request.QueryString.Item("nCompany")
	End If
	%>
			
			<TD><%	Response.Write(mobjValues.PossiblesValues("cbeCompany", "company", eFunctions.Values.eValuesType.clngComboType, lintCompany,  ,  ,  ,  ,  , "EnabledConcept(this);", True,  , GetLocalResourceObject("cbeCompanyToolTip"),  , 11))%></TD>
			
			
			<TD><LABEL><%= GetLocalResourceObject("cbeAreaCaption") %></LABEL></TD>
			<TD><%	Response.Write(mobjValues.PossiblesValues("cbeArea", "table5001", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nInsur_area"),  ,  ,  ,  ,  , "EnabledConcept(this);", True,  , GetLocalResourceObject("cbeAreaToolTip"),  , 11))%></TD>
		</TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("valConceptCaption") %></LABEL></TD>			
			
	            <%	With mobjValues.Parameters
		.Add("nCompany", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With%>
			<TD><%=mobjValues.PossiblesValues("valConcept", "tabconceptscash", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nConcept"), True,  ,  ,  ,  , "document.forms[0].valCurrAcc.Parameters.Param1.sValue=this.value;insChangeConcept();", IsNothing(Request.QueryString.Item("nConcept")), 12, GetLocalResourceObject("valConceptToolTip"))%></TD>
			<TD><LABEL><%= GetLocalResourceObject("valCurrAccCaption") %></LABEL></TD>
			<TD><%	mobjValues.Parameters.Add("nConcept", mobjValues.StringToType(Request.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdInteger, True), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valCurrAcc", "tabTable400", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nValCurrAcc"), True,  ,  ,  ,  , "insChangeCurrAcc();", True,  , GetLocalResourceObject("valCurrAccToolTip"),  , 9))%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeBussiTypeCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeBussiType", "Table20", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nBussinesType"),  ,  ,  ,  ,  ,  , True,  , "", 1, 10)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("SCA2-ICaption") %></LABEL></TD>
			<TD><%=mobjValues.ButtonNotes("SCA2-I", CDbl(Request.QueryString.Item("nNoteNum")),  , True,  ,  ,  ,  ,  , "btnNotenum")%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Documento"><%= GetLocalResourceObject("AnchorDocumento2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HorLine"></TD>
        </TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tctDocNumbeCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctDocNumbe", 10, Request.QueryString.Item("nDocNumber"),  , GetLocalResourceObject("tctDocNumbeToolTip"),  ,  ,  ,  , True, 11)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("cbeChequelocatCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeChequelocat", "Table5553", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nChequelocat"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeChequelocatToolTip"),  , 17)%></TD>
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcdEffecDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdDocDate", Request.QueryString.Item("dDocdate"),  , GetLocalResourceObject("tcdDocDateToolTip"),  ,  ,  , "ShowFinanInt(""Amounting"");", True, 6)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("cbeBankCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeBank", "Table7", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nBank"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBankToolTip"),  , 13)%></TD>
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("valAccBankCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("valAccBank", "tabBank_acc", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nAccountBank"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valAccBankToolTip"),  , 14)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcnFinancIntCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnFinancInt", 18, Request.QueryString.Item("nFinancInt"),  , GetLocalResourceObject("tcnFinancIntToolTip"), True, 6,  ,  ,  ,  , True, 21)%></TD>
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeinwayCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeinway", "Table5554", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nInputChannel"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeinwayToolTip"),  , 22)%></TD>
		</TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD WIDTH="50%" COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="informaciónconvenio"><%= GetLocalResourceObject("Anchorinformaciónconvenio2Caption") %></A></LABEL></TD>
            <TD WIDTH="40%" COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="Tarjeta de crédito"><%= GetLocalResourceObject("AnchorTarjeta de crédito2Caption") %></A></LABEL></TD>
        </TR>
		<TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("valBank_agreeCaption") %></LABEL></TD>
			<TD COLSPAN="1"><%	mobjValues.Parameters.Add("sType_BankAgree", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valBank_agree", "tabBank_Agree_Banks", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nBank_Agree"), True,  ,  ,  ,  , "GetCod_Agree()", True,  , GetLocalResourceObject("valBank_agreeToolTip"),  , 26))%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tctDocNumbeCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctCardNum", 20, Request.QueryString.Item("nCreditCardNumber"),  , GetLocalResourceObject("tctCardNumToolTip"),  ,  ,  ,  , True, 15)%></TD>
        </TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnCod_AgreeCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tcnCod_Agree", 5, Request.QueryString.Item("nCod_Agree"),  , GetLocalResourceObject("tcnCod_AgreeToolTip"),  ,  ,  , "insShowClient();", True, 27)%>
			    <%=mobjValues.ClientControl("dtcClient_Agree", Request.QueryString.Item("dtcClient"),  , GetLocalResourceObject("dtcClient_AgreeToolTip"),  , CBool("True"), "lblCliename_Agree")%></TD>
			<TD><LABEL><%= GetLocalResourceObject("cbeMovTypeCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCardType", "Table183", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nCreditCardType"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCardTypeToolTip"),  , 16)%></TD>
        </TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcdDatecollectCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdDatecollect", Request.QueryString.Item("dDatecollect"),  , GetLocalResourceObject("tcdDatecollectToolTip"),  ,  ,  ,  , True, 28)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcdCardExpirCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdCardExpir", Request.QueryString.Item("dCardExpir"),  , GetLocalResourceObject("tcdCardExpirToolTip"),  ,  ,  ,  , True, 17)%></TD>
		</TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=100677><A NAME="Soporte"><%= GetLocalResourceObject("AnchorSoporte2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="HorLine"></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeTypeDocsupportCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeTypeDocsupport", "table5570", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nTypeDocsupport"),  ,  ,  ,  ,  ,  , IsNothing(Request.QueryString.Item("nTypeDocsupport")),  , GetLocalResourceObject("cbeTypeDocsupportToolTip"),  , 29)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcnFolioSupportCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tcnFolioSupport", 10, Request.QueryString.Item("nFolioSupport"),  , GetLocalResourceObject("tcnFolioSupportToolTip"),  ,  ,  ,  , IsNothing(Request.QueryString.Item("nFolioSupport")), 30)%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnBulletinsCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnBulletins", 10, Request.QueryString.Item("nBulletins"),  , GetLocalResourceObject("tcnBulletinsToolTip"),  ,  ,  ,  ,  ,  , IsNothing(Request.QueryString.Item("nBulletins")), 31)%></TD>
			<TD><LABEL ID=9036><%= GetLocalResourceObject("dtcClientCaption") %></LABEL></TD>
			<TD COLSPAN="3"><%=mobjValues.ClientControl("dtcClient", Request.QueryString.Item("dtcClient"),  , GetLocalResourceObject("dtcClientToolTip"), "ChangeValues()",  , "lblCliename",  ,  ,  ,  ,  , 18,  , True,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=9046><%= GetLocalResourceObject("valIntermedCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("valIntermed", "Intermedia", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nIntermed"),  ,  ,  ,  ,  ,  , IsNothing(Request.QueryString.Item("nIntermed")),  , GetLocalResourceObject("valIntermedToolTip"),  , 19)%></TD>
            <TD><LABEL ID=9037><%= GetLocalResourceObject("valCompanyCRCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valCompanyCR", "Company", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nValCompanyCR"),  ,  ,  ,  ,  ,  , IsNothing(Request.QueryString.Item("nValCompanyCR")),  , GetLocalResourceObject("valCompanyCRToolTip"),  , 20)%></TD>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "if(typeof(document.forms[0].valProduct)!=""undefined"")document.forms[0].valProduct.Parameters.Param1.sValue=this.value", True,  , GetLocalResourceObject("cbeBranchToolTip"),  , 36)%></TD>
  			<TD><LABEL><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%	With mobjValues
		.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , vbNullString, True, 4, GetLocalResourceObject("valProductToolTip"),  , 37))
	End With
	%>
			</TD>
        </TR>
            <TD COLSPAN="0"><LABEL ID=9048><%= GetLocalResourceObject("tcnProponumCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnProponum", 10, Request.QueryString.Item("nNumForm"),  , GetLocalResourceObject("tcnProponumToolTip"),  , 0,  ,  ,  , "insPolicyByPolicy(this)", True)%></TD>
            <TD><LABEL ID=9030><%= GetLocalResourceObject("tcnBordereauxCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnBordereaux", 10, Request.QueryString.Item("nBordereaux"),  , GetLocalResourceObject("tcnBordereauxToolTip"), False, 0,  ,  ,  ,  , True, 22)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=9035><%= GetLocalResourceObject("tcnClaimCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnClaim", 10, mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble, True),  , GetLocalResourceObject("tcnClaimToolTip"), False, 0,  ,  ,  , "ReloadPage();", False, 39)%></TD>
            <%	'AddClaimParameter(this.value);%>
            <TD><LABEL><%= GetLocalResourceObject("cbeCaseNumberCaption") %></LABEL></TD>
            <TD><%	mobjValues.Parameters.Add("nClaim", mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeCaseNumber", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType, "", True,  ,  ,  ,  , "ChangeCaseNumber(this);", False, 19, GetLocalResourceObject("cbeCaseNumberToolTip"), eFunctions.Values.eTypeCode.eString))
	
	Response.Write(mobjValues.HiddenControl("cbeCaseNumber_AUX", Request.QueryString.Item("nCase_Num")))
	Response.Write(mobjValues.HiddenControl("tcnDeman_type_h", Request.QueryString.Item("nDeman_type")))
	%>
			</TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=100678><A NAME="Financiamiento"><%= GetLocalResourceObject("AnchorFinanciamiento2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="HorLine"></TD>
        </TR>
        <TR>
			<TD><LABEL ID=9039><%= GetLocalResourceObject("tcnContractCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnContract", 10, Request.QueryString.Item("nContract"),  , GetLocalResourceObject("tcnContractToolTip"), False, 0,  ,  ,  ,  , True, 28)%></TD>
            <TD><LABEL ID=9044><%= GetLocalResourceObject("tcnDraftCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnDraft", 5, Request.QueryString.Item("nDraft"),  , GetLocalResourceObject("tcnDraftToolTip"), False, 0,  ,  ,  ,  , True, 29)%></TD>
        </TR>
            <Script>
                EnabledFields();
                insChangeMovType();
            </Script>
        <%Else%>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnTransacCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnTransac", 4, "",  , GetLocalResourceObject("tcnTransacToolTip"), False, 0,  ,  ,  ,  , True, 1)%></TD>
            <TD><LABEL><%= GetLocalResourceObject("tcdEffecDateCaption") %></LABEL></TD>
            <TD><%=	mobjValues.DateControl("tcdEffecDate", CStr(Today),  , GetLocalResourceObject("tcdEffecDateToolTip"),  ,  ,  , "ChangedEffecdate();", True, 2)%></TD>
            <TD><%=mobjValues.TextControl("lblTime", 10, "",  , "", True,  ,  ,  , True, 3)%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("tcncomprobCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcncomprob", 10, "",  , GetLocalResourceObject("tcncomprobToolTip"), False, 0,  ,  ,  ,  , True, 4)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("cbeMovTypeCaption") %></LABEL></TD>
			<TD><%	mobjValues.BlankPosition = True
	                Response.Write(mobjValues.PossiblesValues("cbeMovType", "Table78", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "ChangedEffecdate();insChangeMovType();", True,  , GetLocalResourceObject("cbeMovTypeToolTip"),  , 5))%>
			</TD>
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
            <TD><%	
                    mobjValues.Parameters.Add("nUsercode", CDbl(Session("nUsercode")), 1, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, 64)
		            Response.Write(mobjValues.PossiblesValues("cbeOffice", "tabOfficeUser", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOfficeToolTip"),  , 6))
		            mobjValues.BlankPosition = True
	            %>
            </TD>
            <TD><LABEL><%= GetLocalResourceObject("tcdValorDateCaption") %></LABEL></TD>
            <TD><%=	mobjValues.DateControl("tcdValorDate", CStr(Today),  , GetLocalResourceObject("tcdValorDateToolTip"),  ,  ,  , "inShowAmouting(this,""Valuedate"");", True, 7)%></TD>
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
			<%	If CStr(Session("nCurrency_OP752")) <> "" Then%>
				<TD><%		mobjValues.TypeList = 2
		                    Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, Session("nCurrency_OP752"),  ,  ,  ,  ,  , "insShowCurrency(this)", True,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 8))%></TD>
			<%	Else%>
				<TD><%		Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insShowCurrency(this)", True,  , "",  , 8))%></TD>
			<%	End If%>
			<TD><LABEL><%= GetLocalResourceObject("tcnAmountCaption") %></LABEL></TD>
			<%	If CStr(Session("nAmount_OP752")) <> "" Then%>
				<TD><%=mobjValues.NumericControl("tcnAmount", 18, mobjValues.StringToType(Session("nAmount_OP752"), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnAmountToolTip"), True, 6,  ,  ,  , "insShowConver(this)", True, 9)%></TD>
            <%	Else%>
				<TD><%=mobjValues.NumericControl("tcnAmount", 18, "",  , "", True, 6,  ,  ,  , "insShowConver(this)", True, 9)%></TD>
            <%	End If%>
        </TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeCurrencyingCaption") %></LABEL></TD>
			<TD><%	Response.Write(mobjValues.PossiblesValues("cbeCurrencying", "TabCurrency_b", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  , "inShowAmouting(this,""Currency"")", True,  , GetLocalResourceObject("cbeCurrencyingToolTip"),  , 10))%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcnAmountingCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAmounting", 18, mobjValues.StringToType(Request.QueryString.Item("nAmountIng"), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnAmountingToolTip"), True, 6,  ,  ,  , "insShowDifference(this)", True, 11)%></TD>
        </TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnDiferenceCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnDiference", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnDiferenceToolTip"), True, 6,  ,  ,  ,  , True, 12)%></TD>
        </TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeCompanyCaption") %></LABEL></TD>
			
			<%	If CStr(Session("nCompany_OP752")) <> "" Then%>
				<TD><%		Response.Write(mobjValues.PossiblesValues("cbeCompany", "company", eFunctions.Values.eValuesType.clngComboType, Session("nCompany_OP752"),  ,  ,  ,  ,  , "EnabledConcept(this);", True,  , GetLocalResourceObject("cbeCompanyToolTip"),  , 13))%></TD>
			<%	Else
		If Request.QueryString.Item("nCompany") <> vbNullString Then
			lintCompany = Request.QueryString.Item("nCompany")
		End If%>
			      			  
              <TD><%		Response.Write(mobjValues.PossiblesValues("cbeCompany", "company", eFunctions.Values.eValuesType.clngComboType, lintCompany,  ,  ,  ,  ,  , "EnabledConcept(this);", True,  , GetLocalResourceObject("cbeCompanyToolTip"),  , 13))%></TD>
		    <%	End If%>
		    
			<TD><LABEL><%= GetLocalResourceObject("cbeAreaCaption") %></LABEL></TD>
			<TD><%	Response.Write(mobjValues.PossiblesValues("cbeArea", "table5001", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nCompany"),  ,  ,  ,  ,  , "EnabledConcept(this);", True,  , GetLocalResourceObject("cbeAreaToolTip"),  , 11))%></TD>
		</TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("valConceptCaption") %></LABEL></TD>
	            <%	With mobjValues.Parameters
		If CStr(Session("nCompany_OP752")) <> "" Then
			.Add("nCompany", Session("nCompany_OP752"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Else
			.Add("nCompany", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
	End With
	If CStr(Session("nCompany_OP752")) <> "" Then%>
					<TD><%=mobjValues.PossiblesValues("valConcept", "tabconceptscash", eFunctions.Values.eValuesType.clngWindowType, CStr(37), True,  ,  ,  ,  , "insChangeConcept();document.forms[0].elements[""valCurrAcc""].Parameters.Param1.sValue=this.value;", True,  , GetLocalResourceObject("valConceptToolTip"),  , 14)%></TD>
                <%	Else%>
					<TD><%=mobjValues.PossiblesValues("valConcept", "tabconceptscash", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "insChangeConcept();document.forms[0].elements[""valCurrAcc""].Parameters.Param1.sValue=this.value;", True,  , GetLocalResourceObject("valConceptToolTip"),  , 14)%></TD>
				<%	End If%>
			<TD><LABEL><%= GetLocalResourceObject("valCurrAccCaption") %></LABEL></TD>
			<TD><%	mobjValues.Parameters.Add("nConcept", mobjValues.StringToType(Request.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdInteger, True), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valCurrAcc", "TabTable400", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "insChangeCurrAcc();", True,  , GetLocalResourceObject("valCurrAccToolTip"),  , 15))%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeBussiTypeCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeBussiType", "Table20", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , True,  , "", 1, 10)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("SCA2-ICaption") %></LABEL></TD>
			<%	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionCondition) Then%>
				<TD><%=mobjValues.AnimatedButtonControl("btnNotes", "/VTimeNet/Images/btnWONotes.png", GetLocalResourceObject("btnNotesToolTip"),  , "javascript:ShowNotesPopUp1()")%></TD>
				<TD><%=mobjValues.HiddenControl("tcnNoteNum", "")%></TD>
			<%	Else%>
				<TD><%=mobjValues.ButtonNotes("SCA2-I", eRemoteDB.Constants.intNull,  , False,  ,  ,  ,  ,  , "btnNotenum")%></TD>
			<%	End If%>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Documento"><%= GetLocalResourceObject("AnchorDocumento2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HorLine"></TD>
        </TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tctDocNumbeCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctDocNumbe", 10, "",  , GetLocalResourceObject("tctDocNumbeToolTip"),  ,  ,  ,  , True, 16)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("cbeChequelocatCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeChequelocat", "Table5553", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeChequelocatToolTip"),  , 17)%></TD>
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcdEffecDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdDocDate", "",  , GetLocalResourceObject("tcdDocDateToolTip"),  ,  ,  , "ShowFinanInt(""Amounting"");", True, 18)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("cbeBankCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeBank", "Table7", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBankToolTip"),  , 19)%></TD>	
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("valAccBankCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("valAccBank", "tabBank_acc", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valAccBankToolTip"),  , 20)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcnFinancIntCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnFinancInt", 18, Request.QueryString.Item("nFinancInt"),  , GetLocalResourceObject("tcnFinancIntToolTip"), True, 6,  ,  ,  ,  , True, 21)%></TD>
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeinwayCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeinway", "Table5554", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeinwayToolTip"),  , 22)%></TD>
		</TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD WIDTH="50%" COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="informaciónconvenio"><%= GetLocalResourceObject("Anchorinformaciónconvenio2Caption") %></A></LABEL></TD>
            <TD WIDTH="40%" COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="Tarjeta de crédito"><%= GetLocalResourceObject("AnchorTarjeta de crédito2Caption") %></A></LABEL></TD>
        </TR>
		<TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("valBank_agreeCaption") %></LABEL></TD>
			<TD COLSPAN="1"><%	mobjValues.Parameters.Add("sType_BankAgree", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valBank_agree", "tabBank_Agree_Banks", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "GetCod_Agree()", True,  , GetLocalResourceObject("valBank_agreeToolTip"),  , 26))%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tctDocNumbeCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctCardNum", 20, "",  , GetLocalResourceObject("tctCardNumToolTip"),  ,  ,  ,  , True, 23)%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnCod_AgreeCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tcnCod_Agree", 5, "",  , GetLocalResourceObject("tcnCod_AgreeToolTip"),  ,  ,  , "insShowClient();", True, 27)%>
			    <%=mobjValues.ClientControl("dtcClient_Agree", Request.QueryString.Item("dtcClient"),  , GetLocalResourceObject("dtcClient_AgreeToolTip"),  , CBool("True"), "lblCliename_Agree")%></TD>
			<TD><LABEL><%= GetLocalResourceObject("cbeMovTypeCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCardType", "Table183", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCardTypeToolTip"),  , 24)%></TD>
        </TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcdDatecollectCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdDatecollect", "",  , GetLocalResourceObject("tcdDatecollectToolTip"),  ,  ,  ,  , True, 28)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcdCardExpirCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdCardExpir", "",  , GetLocalResourceObject("tcdCardExpirToolTip"),  ,  ,  ,  , True, 25)%></TD>
		</TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Soporte"><%= GetLocalResourceObject("AnchorSoporte3Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="HorLine"></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeTypeDocsupportCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeTypeDocsupport", "table5570", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypeDocsupportToolTip"),  , 29)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcnFolioSupportCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tcnFolioSupport", 10, "",  , GetLocalResourceObject("tcnFolioSupportToolTip"),  ,  ,  ,  , True, 30)%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnBulletinsCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnBulletins", 10, "",  , GetLocalResourceObject("tcnBulletinsToolTip"),  ,  ,  ,  ,  ,  , True, 31)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("dtcClientCaption") %></LABEL></TD>
			<TD COLSPAN="3"><%=mobjValues.ClientControl("dtcClient", "",  , GetLocalResourceObject("dtcClientToolTip"), "ChangeValues()",  , "lblCliename",  ,  ,  ,  ,  , 32,  , True,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("valIntermedCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("valIntermed", "Intermedia", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valIntermedToolTip"),  , 33)%></TD>
            <TD><LABEL><%= GetLocalResourceObject("valCompanyCRCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valCompanyCR", "Company", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCompanyCRToolTip"),  , 34)%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "if(typeof(document.forms[0].valProduct)!=""undefined"")document.forms[0].valProduct.Parameters.Param1.sValue=this.value", True,  , GetLocalResourceObject("cbeBranchToolTip"),  , 36)%></TD>
  			<TD><LABEL><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%	With mobjValues
		.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , vbNullString, True, 4, GetLocalResourceObject("valProductToolTip"),  , 37))
	End With
	%>
			</TD>
        </TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnProponumCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tcnProponum", 10, "",  , GetLocalResourceObject("tcnProponumToolTip"),  ,  ,  , "insPolicyByPolicy(this)", True, 35)%></TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnBordereauxCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnBordereaux", 10, "",  , GetLocalResourceObject("tcnBordereauxToolTip"), False, 0,  ,  ,  ,  , True, 38)%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnClaimCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnClaim", 10, "",  , GetLocalResourceObject("tcnClaimToolTip"), False, 0,  ,  ,  , "ReloadPage();", True, 39)%></TD>
            <%	'AddClaimParameter(this.value);%>
            <TD><LABEL><%= GetLocalResourceObject("cbeCaseNumberCaption") %></LABEL></TD>
            <TD><%	If Request.QueryString.Item("nMainAction") = "402" Then
		Response.Write(mobjValues.NumericControl("tcnCaseNumber", 5, "",  , GetLocalResourceObject("tcnCaseNumberToolTip"),  ,  ,  ,  ,  ,  , True))
	Else
		mobjValues.Parameters.Add("nClaim", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjValues.BlankPosition = False
		Response.Write(mobjValues.PossiblesValues("cbeCaseNumber", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType, "", True,  ,  ,  ,  , "ChangeCaseNumber(this);", True, 19, GetLocalResourceObject("cbeCaseNumberToolTip"), eFunctions.Values.eTypeCode.eString))
		
		Response.Write(mobjValues.HiddenControl("cbeCaseNumber_AUX", Request.QueryString.Item("nCase_Num")))
		Response.Write(mobjValues.HiddenControl("tcnDeman_type_h", Request.QueryString.Item("nDeman_type")))
	End If
	%>
			</TD>			
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Financiamiento"><%= GetLocalResourceObject("AnchorFinanciamiento3Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="HorLine"></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnContractCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnContract", 10, "",  , GetLocalResourceObject("tcnContractToolTip"), False, 0,  ,  ,  ,  , True, 40)%></TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnDraftCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnDraft", 5, "",  , GetLocalResourceObject("tcnDraftToolTip"), False, 0,  ,  ,  ,  , True, 41)%></TD>
        </TR>
	<%End If%>
    </TABLE>
    <%If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionCondition) Then
	Call insReaOP001()
End If
Response.Write(mobjValues.BeginPageButton)
%>
</FORM>
<%
'+ Si el usuario no tiene una caja asociada no se le permite el acceso a la transacción.
%>
<%If Not lblnAccess Then%>
    <SCRIPT>insDisableAll();</SCRIPT>
<%End If%>
</BODY>
</HTML>
<%
If Request.QueryString.Item("blnReloadPage") = "False" Then
	Response.Write("<SCRIPT>insStateZone();</SCRIPT>")
End If
'+	Se obtiene la sucursal asociada al usuario.
Response.Write("<SCRIPT>insDefValues('Office');</SCRIPT>")
Response.Write("<SCRIPT>var nNoteNum;</SCRIPT>")
Response.Write("<SCRIPT>var nCollect_P;</SCRIPT>")
Response.Write("<SCRIPT>nCollect_P=0" & Session("nCollect_P") & "</SCRIPT>")
%>





