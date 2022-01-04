<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">    

    '^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.03
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility
    Dim mobjProduct_li As eProduct.Product
    Dim mobjProduct As eProduct.Product
    Dim nDaysDur As Object
    Dim mobjMenu As eFunctions.Menues
    '- Objeto para el manejo de las funciones generales de carga de valores 
    Dim mobjValues As eFunctions.Values
    Dim mobjCertificat As ePolicy.Certificat

    '- String que envia a control de cliente llave de busca de la poliza 
    Dim lstrQueryString As String

    Dim mintTerm_grace As Object
    
    Dim lstrApv As String = 0
    Dim lblmultiannual As Boolean


</script>
<%Response.Expires = -1
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("CA004")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility
    mobjCertificat = New ePolicy.Certificat

    mobjValues.ActionQuery = Session("bQuery")

    mobjProduct_li = New eProduct.Product

    mobjProduct = New eProduct.Product

    Call mobjProduct_li.FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                                       mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger), _
                                       Today)

    Call mobjProduct.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                          mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger), _
                          mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))

    Call mobjCertificat.insPreCA004(Session("sCertype"), _
                                    mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                                    mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger), _
                                    mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong), _
                                    mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong), _
                                    mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                    mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdInteger), _
                                    Session("sSche_code"))

    If mobjCertificat.nTerm_grace = eRemoteDB.Constants.intNull Or mobjCertificat.nTerm_grace = 0 Then
        mintTerm_grace = mobjProduct.nAdvance
    Else
        mintTerm_grace = mobjCertificat.nTerm_grace
    End If
    nDaysDur = 0
    If mobjCertificat.nDuration <= 0 And mobjCertificat.dExpirdat <> eRemoteDB.Constants.dtmNull Then
	nDaysDur = System.Math.Abs(DateDiff(Microsoft.VisualBasic.DateInterval.Day, mobjCertificat.dExpirdat, mobjCertificat.dStartDate))
        End If
    
    '+ Cuando se está haciendo el llamado a la funcion "InsChangeWayPay" al carga la página, da error de JS si 
    '+ la propiedad "mobjProduct_li.sApv" esta vacia, se le coloca un valor por defecto
    If String.IsNullOrEmpty(mobjProduct_li.sApv) Then
        lstrApv = "0"
    Else
        lstrApv = mobjProduct_li.sApv
    End If
    
  
    If Session("nTransaction") <= 3 Or Session("nTransaction") = 18 Or Session("nTransaction") = 19 Then
        lblmultiannual = False
    Else
         lblmultiannual = true
    End If
%>
<script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<script type='text/javascript'>
    $(document).ready(function () { // ehh reconocimiento de ingresos 29122021
        $('input[name=optFreq]').click(function () {
            let valoptFreq = $('input:radio[name=optFreq]:checked').val();
            if (valoptFreq == 2) {
                $('#cbePayFreq').val('6');
                $("#cbePayFreq").prop("disabled", true);
            } else {
                $("#cbePayFreq").prop("disabled", false);
                $('#cbePayFreq').val('5');
            }
        });

    });
//+ Cantidad de cuotas 
    var lintcbeQuota

// ChangeSubmit: Cambia la accion de la forma
//-------------------------------------------------------------------------------------------
function ChangeSubmit(Option, Holder) {
//-------------------------------------------------------------------------------------------	
	switch (Option) {
		case "Add":
			document.forms[0].action = "valPolicySeq.aspx?&nMainAction=301&nHolder=" + Holder
	}
}
	
// ChangeFreq: Actualiza los objetos de la forma, según el tipo de frecuencia
//-------------------------------------------------------------------------------------------
function ChangeFreq(lobjPayFreq) {
//-------------------------------------------------------------------------------------------	
// Actualizar como requerida la transacción VA595 si el producto es de Vida
// y la clase de producto es "Vida Especial"		
	switch (lobjPayFreq.value) {
		case "8":
		    self.document.forms[0].cbeQuota.value = lintcbeQuota
			self.document.forms[0].cbeQuota.disabled = false
            self.document.forms[0].chksIndqsame.disabled = false
            self.document.forms[0].cbenPromissory_Note.disabled = false
            break
		default:
            self.document.forms[0].chksIndqsame.checked = false
            self.document.forms[0].chksIndqsame.disabled = true
		    self.document.forms[0].cbeQuota.value = 0
			self.document.forms[0].cbeQuota.disabled = true
            self.document.forms[0].cbenPromissory_Note.value = 0
            self.document.forms[0].cbenPromissory_Note.disabled = true
	}
}
	
// Changeapplication: Actualiza los objetos de la forma, según el tipo de revalorización
//-------------------------------------------------------------------------------------------
function ChangeApplication(lobjIndexType) {
//-------------------------------------------------------------------------------------------	
    if (lobjIndexType.value==4){
        self.document.forms[0].cbeIndexApl.value = 3
        self.document.forms[0].cbeIndexApl.disabled = true
        self.document.forms[0].tcnIndexRate.disabled = true
        self.document.forms[0].tcnIndexRate.value = 0
    }
    else{
        if (lobjIndexType.value!=3){
            self.document.forms[0].tcnIndexRate.value = 0
            self.document.forms[0].tcnIndexRate.disabled = true
        }
        else
            self.document.forms[0].tcnIndexRate.disabled = false

        self.document.forms[0].cbeIndexApl.disabled = false
    }
}
//InsChangeWayPay();
//-------------------------------------------------------------------------------------------
function InsChangeWayPay(nCertif, nDirTyp , nDuration, sApv) {
//-------------------------------------------------------------------------------------------
	
	var lstrquery='';

    var sColinvot = '<%=mobjCertificat.sColinvot%>';

	with (self.document.forms[0]){
        
        // Si la via de pago es aviso o cuponera se habilita el campo Cobrador
		if (cbeWayPay.value == '4' ||
			cbeWayPay.value == '5'){
			valCollector.disabled = false;
			btnvalCollector.disabled = false;
		}
		else{
			valCollector.disabled = true;
			btnvalCollector.disabled = true;
			valCollector.value = '';
			UpdateDiv('valCollectorDesc',' ');
		}

		if (nCertif=='0'){
//			if (sApv=='1')
//				elements["valOrigin"].Parameters.Param3.sValue = 0; //Corresponde a tipo de documento
//+ Si Vía de pago es 1 ó 2 : 
			if (cbeWayPay.value == '1' ||
			    cbeWayPay.value == '2'){
                lstrquery = "sClient=" + tctClient.value + "&dEffecdate=" + <%=Session("dEffecdate")%> + "&nWayPay=" + cbeWayPay.value + "&optDirTyp=" + nDirTyp + "&hhDirTyp=" + hhDirTyp.value;
				if (nDuration == '1')
				    lstrquery += "&nDuration=" + tcnDuration.value + "&dStartDate=" + tcdStartDate.value;
	
				insDefValues('WayPay',lstrquery , '/VTimeNet/Policy/PolicySeq');

				optDirTyp[0].disabled = false;
				optDirTyp[1].disabled = false;
			}
			else{
				optDirTyp[0].checked = false;
				optDirTyp[1].checked = false;
				optDirTyp[0].disabled = true;
				optDirTyp[1].disabled = true;				
			}
			if (cbeWayPay.value == '3'){
				self.document.forms[0].valAgreement.disabled = false;
        		self.document.forms[0].tcnBillDay.disabled = true;
		        self.document.forms[0].tcnBillDay.value = "";
				if (self.document.forms[0].valAgreement.value==''){
					insDefValues('GenAgreement','' , '/VTimeNet/Policy/PolicySeq');
				}	
				self.document.forms[0].btnvalAgreement.disabled = false;
				if (nNoChange == '2') {
//                    if (self.document.forms[0].hhprodclas.value == 4){
						//self.document.forms[0].valOrigin.value = '2';
						//self.document.forms[0].valOrigin.disabled = true;
						//self.document.forms[0].btnvalOrigin.disabled = true;
//				    }
				}    
			}
			else {
			    self.document.forms[0].valAgreement.value = '';
				self.document.forms[0].valAgreement.disabled = true;
				self.document.forms[0].btnvalAgreement.disabled = true;
                UpdateDiv('valAgreementDesc',' ');
                self.document.forms[0].tcnBillDay.disabled = false;
                self.document.forms[0].tcnBillDay.value = self.document.forms[0].hddBillDay.value;
		        
			}	
			if (nNoChange == '2'){
			    if(self.document.forms[0].hhprodclas.value == 4) {
    				self.document.forms[0].tcnAFPCommi.value = '';
	    			self.document.forms[0].cbeCurrency.value = '';
			
					if (cbeWayPay.value == '7'){
				    	self.document.forms[0].tcnAFPCommi.disabled = false;
					    self.document.forms[0].cbeCurrency.disabled = false;
				    }
				    else {
					    self.document.forms[0].tcnAFPCommi.disabled = true;
					    self.document.forms[0].cbeCurrency.disabled = true;
				    }
				}
		}


		}
		
//        else if (nCertif != 0 && sColinvot == 2){
//            self.document.forms[0].cbeWayPay.disabled = false;
//        }

//        else{
		    //self.document.forms[0].valAgreement.disabled = true;
			//self.document.forms[0].btnvalAgreement.disabled = true;
//			self.document.forms[0].cbeWayPay.disabled = true;
//		}		
		}		
    nNoChange = '2';
   	
//+ se agrega manejo de habilitación de campos para los endosos    
<%If (Session("nTransaction") = 12 Or Session("nTransaction") = 14 Or Session("nTransaction") = 26 Or Session("nTransaction") = 27) Then
	
	Response.Write("self.document.forms[0].tcdIssuedat.disabled = true;")
	Response.Write("self.document.forms[0].tcnDuration.disabled = true;")
    Response.Write("self.document.forms[0].tcnDurationDays.disabled = true;")
	Response.Write("self.document.forms[0].tcdStartDate.disabled = true;")
	Response.Write("self.document.forms[0].tcdExpirDate.disabled = true;")
	Response.Write("self.document.forms[0].optFreq[0].disabled = true;")
	Response.Write("self.document.forms[0].optFreq[1].disabled = true;")
	Response.Write("self.document.forms[0].cbeIndexType.disabled = true;")
	Response.Write("self.document.forms[0].optFreq[2].disabled = true;")
	Response.Write("self.document.forms[0].cbePayFreq.disabled = true;")
	Response.Write("self.document.forms[0].cbeQuota.disabled = true;")
    Response.Write("self.document.forms[0].cbenPromissory_Note.disabled = true;")
	Response.Write("self.document.forms[0].chkExemption.disabled = true;")
	Response.Write("self.document.forms[0].chksLeg.disabled = true;")
	Response.Write("self.document.forms[0].chksReinst.disabled = true;")
	
Else
	If Session("nTransaction") = 30 Or Session("nTransaction") = 31 Then
		Response.Write("self.document.forms[0].optFreq[0].disabled = false;")
		Response.Write("self.document.forms[0].optFreq[1].disabled = false;")
		Response.Write("self.document.forms[0].optFreq[2].disabled = false;")
		Response.Write("self.document.forms[0].cbePayFreq.disabled = false;")
		Response.Write("self.document.forms[0].cbenPromissory_Note.disabled = false;")
        Response.Write("self.document.forms[0].cbeQuota.disabled = false;")
	End If
End If
If (Session("nTransaction") = 12 Or Session("nTransaction") = 13 Or Session("nTransaction") = 14 Or Session("nTransaction") = 15 Or Session("nTransaction") = 24 Or Session("nTransaction") = 25 Or Session("nTransaction") = 26 Or Session("nTransaction") = 27)
    If (Session("dEffecdate") = mobjCertificat.dExpirdat) Then
        Response.Write("self.document.forms[0].cbePayFreq.disabled = false;")
        If mobjCertificat.nPayFreq = 8  Then
            Response.Write("self.document.forms[0].cbeQuota.disabled = false;")
             Response.Write("self.document.forms[0].cbenPromissory_Note.disabled = false;")
        End If
    End If
End If
%>      


}

//insValLeg: valida que no se seleccione el check de cálculo de tope de capital por evaluaciòn siempre y cuando la póliza sea innominada
//------------------------------------------------------------------------------------------------------------
function insValLeg(chkNoPayRoll){
//------------------------------------------------------------------------------------------------------------
	if (self.document.forms[0].chksNopayroll.checked)
    {
        self.document.forms[0].chksLeg.checked = false;
    }
	self.document.forms[0].chksLeg.disabled = self.document.forms[0].chksNopayroll.checked;
}

//InsChangeAgreement();
//-------------------------------------------------------------------------------------------
function InsChangeAgreement() {
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
		if (valAgreement.value != '')
			ShowPopUp("/VTimeNet/Policy/PolicySeq/ShowDefValues.aspx?Field=Agreement&nCod_Agree=" + valAgreement.value, "ShowDefValues", 1, 1,"no","no",2000,2000);
	}
}

//%ShowSumValues: Suma los meses de duracion a la fecha de fin
//-------------------------------------------------------------------------------------------
function ShowSumDate(Field, nNewData){
//-------------------------------------------------------------------------------------------
    var lintDay = 0;
    var lintDayEx = 0;        
	var lstrBrancht = '<%=Session("sBrancht")%>'
	var lstrColinvot = '<%=mobjCertificat.sColinvot%>'
    var lstrColtimre = '<%=mobjCertificat.sColtimre%>'
	
	with (self.document.forms[0]){
		if(Field.value==''){
			tcnDuration.value='';
<%If (Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyIssue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifIssue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngRecuperation Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotation Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifQuotation Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyProposal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifProposal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyReissue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifReissue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyPropRenewal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotRenewal) And CStr(Session("sBrancht")) <> "1" And ((Session("nCertif") > 0 And mobjCertificat.sColtimre <> "1") Or Session("nCertif") = 0) Then
	%>
		if(nNewData==1){
			hddExpirDate.value='';
			tcdExpirDate.value='';
		}
		tcdExpirDate.disabled=false;
		btn_tcdExpirDate.disabled=false;
		$(tcdExpirDate).change();
<%	
End If
%>
		}
		else{
			tcnDurationDays.value='';
			tcdExpirDate.disabled = false
			insDefValues('SumDate', "nDuration=" + Field.value + "&dStartDate=" + tcdStartDate.value + "&sColtimre=" + lstrColtimre + "&sColinvot=" + lstrColinvot, '/VTimeNet/Policy/PolicySeq');
		}
	}
}   

//%ShowSumValues: Suma los meses de duracion a la fecha de fin
//-------------------------------------------------------------------------------------------
function ShowSumDaysDate(Field, nNewData){
//-------------------------------------------------------------------------------------------
    var lintDay = 0;
    var lintDayEx = 0;        
	var lstrBrancht = '<%=Session("sBrancht")%>'
	var lstrColinvot = '<%=mobjCertificat.sColinvot%>'
    var lstrColtimre = '<%=mobjCertificat.sColtimre%>'
	
	with (self.document.forms[0]){
		if(Field.value=='' || Field.value=='0'){
			tcnDurationDays.value='';
			ShowSumDate(tcnDuration, 1);
		}
		else{
			tcnDuration.value='';
			ShowSumDate(tcnDuration, 1);
			insDefValues('SumDateDays', "nDurationDays=" + Field.value + "&dStartDate=" + tcdStartDate.value + "&sColtimre=" + lstrColtimre + "&sColinvot=" + lstrColinvot, '/VTimeNet/Policy/PolicySeq');
		}
	}
} 
//%ShowSumValues: Suma los meses de duracion a la fecha de fin
//-------------------------------------------------------------------------------------------
function ShowSumExtraDaysDate(Field, nNewData){
//-------------------------------------------------------------------------------------------
    var lintDay = 0;
    var lintDayEx = 0;        
	var lstrBrancht = '<%=Session("sBrancht")%>'
	var lstrColinvot = '<%=mobjCertificat.sColinvot%>'
    var lstrColtimre = '<%=mobjCertificat.sColtimre%>'
	
	with (self.document.forms[0]){
		if(Field.value=='' || Field.value=='0'){
			tcnExtraDay.value='0';			
            insDefValues('SumDateExtraDays', "nExtraday=" + "0" + "&dStartDate=" + tcdStartDate.value + "&nDurationDays=" + tcnDurationDays.value + "&nDuration=" + tcnDuration.value, '/VTimeNet/Policy/PolicySeq');
		}
		else{			
			insDefValues('SumDateExtraDays', "nExtraday=" + Field.value + "&dStartDate=" + tcdStartDate.value + "&nDurationDays=" + tcnDurationDays.value + "&nDuration=" + tcnDuration.value, '/VTimeNet/Policy/PolicySeq');
		}
	}
} 
//InsChangeReceipt_ind();
//-------------------------------------------------------------------------------------------
function InsChangeReceipt_ind() {
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
		if (cbeReceipt_ind.value != '')
			ShowPopUp("/VTimeNet/Policy/PolicySeq/ShowDefValues.aspx?Field=Receipt_ind&nReceipt_ind=" + cbeReceipt_ind.value, "ShowDefValues", 1, 1,"no","no",2000,2000);
	}
}
//InsChangeReceipt_ind();
//-------------------------------------------------------------------------------------------
function Billday() {
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
		if (tcnBillDay.value != '')
			  hddBillDay.value = tcnBillDay.value;
	}
}
</SCRIPT>
</SCRIPT>
<HTML>
<HEAD>
    <%Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
        mobjMenu = Nothing
    %>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</head>
<body>
    <form method="post" id="FORM" name="frmCA004" action="valPolicySeq.aspx?nMainAction=301&amp;nHolder=1">
    <p align="center">
        <label>
            <a href="#Facturación">
                <%= GetLocalResourceObject("AnchorFacturaciónCaption") %></a></label><label>
                    |
                </label>
        <label>
            <a href="#Revalorización">
                <%= GetLocalResourceObject("AnchorRevalorizaciónCaption") %></a></label><label>
                    |
                </label>
        <label>
            <a href="#Datos de la cobranza">
                <%= GetLocalResourceObject("AnchorDatos de la cobranzaCaption") %></a></label><label>
                    |
                </label>
        <label>
            <a href="#Vía de cobro">
                <%= GetLocalResourceObject("AnchorVía de cobroCaption") %></a></label><label>
                    |
                </label>
        <label>
            <a href="#Varios">
                <%= GetLocalResourceObject("AnchorVariosCaption") %></a></label>
    </p>
    <%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))%>
    <table width="100%">
        <TR>
            <td colspan="2" class="HighLighted">
                <label id="0">
                    <%= GetLocalResourceObject("AnchorCaption") %></label>
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2" class="HighLighted">
                <label id="0">
                    <%= GetLocalResourceObject("Anchor2Caption") %></label>
            </td>
        </TR>
        <TR>
            <td colspan="2" class="Horline">
            </td>
            <td>
            </td>
            <td colspan="2" class="Horline">
            </td>
        </TR>
        <TR>
            <td>
                <label id="12972">
                    <%= GetLocalResourceObject("tcdIssuedatCaption") %></label>
            </td>
            <td>
                <%=mobjValues.DateControl("tcdIssuedat", CStr(mobjCertificat.dIssuedat),  , GetLocalResourceObject("tcdIssuedatToolTip"),  ,  ,  ,  , mobjCertificat.StateVarCA004(0), 1)%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="0">
                    <%= GetLocalResourceObject("tcdDate_origiCaption") %></label>
            </td>
            <td>
                <%=mobjValues.DateControl("tcdDate_origi", CStr(mobjCertificat.dDate_origi),  , GetLocalResourceObject("tcdDate_origiToolTip"),  ,  ,  ,  , True)%>
            </td>
        </TR>
        <TR>
            <TD>
                <label id="12974">
                    <%= GetLocalResourceObject("tcdReqDateCaption") %></label>
            </TD>
            <TD>
                <%=mobjValues.DateControl("tcdReqDate", CStr(mobjCertificat.dPropodat),  , GetLocalResourceObject("tcdReqDateToolTip"),  ,  ,  ,  , mobjCertificat.StateVarCA004(1), 2)%>
            </TD>
            <TD>
                &nbsp;
            </TD>
            <TD>
                <label id="12975">
                    <%= GetLocalResourceObject("tcnDurationCaption") %></label>
            </TD>
            <TD>
                <%=mobjValues.NumericControl("tcnDuration", 4, CStr(mobjCertificat.nDuration),  , GetLocalResourceObject("tcnDurationToolTip"),  ,  ,  ,  ,  , "ShowSumDate(this, 1);", mobjCertificat.StateVarCA004(35), 3)%>
            </TD>

        </TR>
        <TR>
            <td colspan="3">
                  <%=mobjValues.CheckControl("chksInd_Multiannual", GetLocalResourceObject("chksInd_MultiannualCaption"), mobjCertificat.sInd_Multiannual, , ,  lblmultiannual , 28, GetLocalResourceObject("chksInd_MultiannualToolTip"))%>
           
               </td>
            <td>
                <label id="0">
                    <%= GetLocalResourceObject("tcnDurationDaysCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnDurationDays", 4, nDaysDur, , GetLocalResourceObject("tcnDurationDaysToolTip"), , , , , , "ShowSumDaysDate(this, 1);", mobjCertificat.sColtimre = "1" And mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong) <> 0, 4)%>
            </td>
        </TR>
        <TR>
            <td colspan="3">           
               </td>
            <td>
                <label id="Label13">
                    <%= GetLocalResourceObject("tcnExtraDayCaption")%></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnExtraDay", 4, mobjCertificat.nExtraDay, , GetLocalResourceObject("tcnExtraDayToolTip"), , , , , , "", mobjCertificat.sColtimre = "1", 4)%>
            </td>
        </TR>
        <TR>
            <td colspan="3">
                &nbsp;
            </td>
            <td>
                <label id="12975">
                    <%= GetLocalResourceObject("tcdStartDateCaption") %></label>
            </td>
            <td>
                <%=mobjValues.DateControl("tcdStartDate", CStr(mobjCertificat.dStartDate),  , GetLocalResourceObject("tcdStartDateToolTip"),  ,  ,  ,  , True, 4)%>
            </td>
        </TR>
        <TR>
            <td colspan="3">
                &nbsp;
            </td>
            <td>
                <label id="12968">
                    <%= GetLocalResourceObject("tcdExpirDateCaption") %></label>
            </td>
            <td>
                <%=mobjValues.DateControl("tcdExpirDate", CStr(mobjCertificat.dExpirdat),  , GetLocalResourceObject("tcdExpirDateToolTip"),  ,  ,  ,  , mobjCertificat.StateVarCA004(3), 5)%>
            </td>
            <%=mobjValues.HiddenControl("hddExpirDate", vbNullString)%>
        </TR>
        <tr>
            <td colspan="5" class="HighLighted">
                <label id="0">
                    <%= GetLocalResourceObject("Anchor3Caption") %></label>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="Horline">
            </td>
        </tr>
        <TR>
            <td>
                <label>
                    <%= GetLocalResourceObject("tctClientCaption") %></label>
            </td>
            <td colspan="4">
                <%  mobjValues.TypeList = 1
                    lstrQueryString = "&sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&dEffecdate=" & Session("dEffecdate")
                    Response.Write(mobjValues.ClientControl("tctClient", mobjCertificat.sClient, , GetLocalResourceObject("tctClientToolTip"), , mobjCertificat.StateVarCa004(19), , , , , , eFunctions.Values.eTypeClient.SearchClientPolicy, 6, , , lstrQueryString))
                %>
            </td>
        </TR>
        <% If Session("sBrancht") = "7" Then%>
        <TR>
            <td colspan="5" class="HighLighted">
                <label id="LABEL1">
                    <a name="Facturación">
                        <%= GetLocalResourceObject("AnchorFacturación2Caption") %></a></label>
            </td>
        </TR>
        <TR>
            <td colspan="5" class="Horline">
            </td>
        </TR>
        <% Else%>
        <TR>
            <td colspan="2" class="HighLighted">
                <label id="LABEL3">
                    <a name="Facturación">
                        <%= GetLocalResourceObject("AnchorFacturación2Caption") %></a></label>
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2" class="HighLighted">
                <label id="LABEL4">
                    <a name="Revalorización">
                        <%= GetLocalResourceObject("AnchorRevalorización2Caption") %></a></label>
            </td>
        </TR>
        <TR>
            <td colspan="2" class="Horline">
            </td>
            <td>
            </td>
            <td colspan="2" class="Horline">
            </td>
        </TR>
        <% End If%>
        <TR>
            <td>
                <label id="0">
                    <%= GetLocalResourceObject("tcdTariffDateCaption") %></label>
            </td>
            <td>
                <%=mobjValues.DateControl("tcdTariffDate", CStr(mobjCertificat.dTariffDate),  , GetLocalResourceObject("tcdTariffDateToolTip"),  ,  ,  ,  , True)%>
            </td>
            <td>
                &nbsp;
            </td>
            <% If Session("sBrancht") = "7" Then%>
            <td>
                <label id="LABEL6">
                    <%= GetLocalResourceObject("cbeQuotaCaption") %></label>
            </td>
            <td>
                <%  With mobjValues
                        '+Se carga el valor de la cantidad de cuotas disponibles            	
                        .BlankPosition = True
                        .Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        Response.Write(.PossiblesValues("cbeQuota", "tabPay_FractiQCA004", 1, CStr(mobjCertificat.nQuota),  True, , , , , ,  mobjCertificat.StateVarCa004(9), , GetLocalResourceObject("cbeQuotaToolTip"), , 11))
                    End With
                    Response.Write("<script>lintcbeQuota=" & mobjCertificat.nQuota & "</script>")
                %>
            </td>
            <% Else%>
            <td>
                <label id="LABEL5">
                    <%= GetLocalResourceObject("cbeIndexTypeCaption") %></label>
            </td>
            <td>
                <%  mobjValues.BlankPosition = False
                    Response.Write(mobjValues.PossiblesValues("cbeIndexType", "table46", 1, mobjCertificat.sIndextyp, , , , , , "ChangeApplication(this)", mobjCertificat.StateVarCa004(17), , GetLocalResourceObject("cbeIndexTypeToolTip"), , 14))
                %>
            </td>
            <% End If%>
        </TR>
        <TR>
            <td>
                <%=mobjValues.OptionControl(0, "optFreq", GetLocalResourceObject("optFreq_1Caption"), mobjCertificat.sProrshort, "1",  , mobjCertificat.StateVarCA004(4), 7, GetLocalResourceObject("optFreq_1ToolTip"))%>
            </td>
            <td>
                <%=mobjValues.OptionControl(0, "optFreq", GetLocalResourceObject("optFreq_2Caption"), CStr(CShort(mobjCertificat.sProrshort) - 1), "2",  , mobjCertificat.StateVarCA004(5), 8, GetLocalResourceObject("optFreq_2ToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
            <% If Session("sBrancht") = "7" Then%>
            <td>
                <label id="LABEL7">
                    <%= GetLocalResourceObject("cbeBill_indCaption") %></label>
            </td>
            <td>
                <%=mobjValues.ComboControl("cbeBill_ind",  GetLocalResourceObject("cbeBill_indOptions"), mobjCertificat.sBill_ind, False, 16, GetLocalResourceObject("cbeBill_indToolTip"),  , mobjCertificat.StateVarCA004(36))%>
            </td>
            <!--<TD COLSPAN="2"><%=mobjValues.CheckControl("chkBill_Ind", GetLocalResourceObject("chkBill_IndCaption"), mobjCertificat.sBill_ind, "1",  , mobjCertificat.StateVarCA004(36), 13, GetLocalResourceObject("chkBill_IndToolTip"))%></TD> -->
            <% Else%>
            <td>
                <label id="12969">
                    <%= GetLocalResourceObject("cbeIndexAplCaption") %></label>
            </td>
            <td>
                <% mobjValues.BlankPosition = False
                    Response.Write(mobjValues.PossiblesValues("cbeIndexApl", "table154", eFunctions.Values.eValuesType.clngComboType, mobjCertificat.sRevalapl, , , , , , , mobjCertificat.StateVarCa004(18), , GetLocalResourceObject("cbeIndexAplToolTip"), , 15))
                                   %>
            </td>
            <% End If%>
        </TR>
        <tr>
            <td>
                <%=mobjValues.OptionControl(0, "optFreq", GetLocalResourceObject("optFreq_3Caption"), CStr(CShort(mobjCertificat.sProrshort) - 2), "3",  , mobjCertificat.StateVarCA004(6), 9, GetLocalResourceObject("optFreq_3ToolTip"))%>
            </td>
            <td>
                <%  With mobjValues
                        '+ Se carga el valor por defecto del campo Facturación-Según frecuencia (COMBO)
                        .BlankPosition = False
                        .Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nQuota", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        Response.Write(.PossiblesValues("cbePayFreq", "tabPay_fracti", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nPayfreq), True, , , , , "ChangeFreq(this)", mobjCertificat.StateVarCa004(8), , GetLocalResourceObject("cbePayFreqToolTip"), , 10))
                    End With
                %>
            </td>
            <td>
                &nbsp;
            </td>
            <% If Session("sBrancht") = "7" Then%>
            <td>
                <label id="LABEL9">
                    <%= GetLocalResourceObject("cbeReceipt_indCaption") %></label>
            </td>
            <td>
                <%= mobjValues.ComboControl("cbeReceipt_ind", GetLocalResourceObject("cbeReceipt_indOptions"), mobjCertificat.sReceipt_ind, False, 16, GetLocalResourceObject("cbeReceipt_indToolTip"), "InsChangeReceipt_ind();", mobjCertificat.StateVarCa004(45))%>
            </td>
            <td>
                &nbsp;
            </td>
            <!--<TD><LABEL ID=0><%= GetLocalResourceObject("tcnTerm_graceCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnTerm_grace", 3, mintTerm_grace,  , GetLocalResourceObject("tcnTerm_graceToolTip"),  ,  ,  ,  ,  ,  , mobjCertificat.StateVarCA004(43), 19)%></TD>		    -->
            <%Response.Write(mobjValues.HiddenControl("tcnTerm_grace", mintTerm_grace))%>
            <!--<TD COLSPAN="2"><%=mobjValues.CheckControl("chkBill_Ind", GetLocalResourceObject("chkBill_IndCaption"), mobjCertificat.sBill_ind, "1",  , mobjCertificat.StateVarCA004(36), 13, GetLocalResourceObject("chkBill_IndToolTip"))%></TD> -->
            <% Else%>
            <td>
                <label id="12970">
                    <%= GetLocalResourceObject("tcnIndexRateCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnIndexRate", 3, CStr(mobjCertificat.nIndexFac),  , GetLocalResourceObject("tcnIndexRateToolTip"),  ,  ,  ,  ,  ,  , mobjCertificat.StateVarCA004(16), 16)%>
            </td>
            <% End If%>
        </tr>
        <%  If Session("sBrancht") = "7" Then
                '+ Para las pólizas de atención médica no se debe mostrar el bloque de "Revalorización", por lo que los campos asociados a dicha sección se crean 
                '+ como campos ocultos para mantener los valores.  
                '+ Los demás campos de la sección se redistribuyen para no dejar el espacio en blanco
                Response.Write(mobjValues.HiddenControl("cbeIndexTypeCaption", mobjCertificat.sIndextyp))
                Response.Write(mobjValues.HiddenControl("cbeIndexAplCaption", mobjCertificat.sRevalapl))
                Response.Write(mobjValues.HiddenControl("tcnIndexRateCaption", CStr(mobjCertificat.nIndexfac)))
            Else%>
        <tr>
            <td>
                <label id="LABEL2">
                    <%= GetLocalResourceObject("cbeQuotaCaption") %></label>
            </td>
            <td>
                <% With mobjValues
                        '+Se carga el valor de la cantidad de cuotas disponibles            	
                        .BlankPosition = True
                        .Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        Response.Write(.PossiblesValues("cbeQuota", "tabPay_FractiQCA004", 1, CStr(mobjCertificat.nQuota), True, , , , , , mobjCertificat.StateVarCa004(9), , GetLocalResourceObject("cbeQuotaToolTip"), , 11))
                    End With
                    Response.Write("<script>lintcbeQuota=" & mobjCertificat.nQuota & "</script>")
                %>
         
            </td>
            <td>
                &nbsp;
            </td>
             <% If Session("nBranch") = "6" Then%>
            <td>
                    <label id="Label10">
                        <%= GetLocalResourceObject("cbeDepreciationtableCaption") %></label>
                </td>
                <td>
                    <%=mobjValues.PossiblesValues("cbeDepreciationtable", "table7210", eFunctions.Values.eValuesType.clngComboType, mobjCertificat.nDepreciationTable, , , , , , , , , GetLocalResourceObject("cbeIndexAplToolTip"), , 15) %>
                </td>
            <% End If%>
        </tr>
           <tr>
            <td>
           
              <%=mobjValues.CheckControl("chksIndqsame", GetLocalResourceObject("chksIndqsamevalueCaption"), mobjCertificat.sIndqsamevalue, , , IIf (Str(mobjCertificat.nPayfreq) =  8 , false , True)  , 28, GetLocalResourceObject("chksInd_MultiannualToolTip"))%>
           
           </td>
            <td>
                &nbsp;  
            </td>
             <td>
                &nbsp;  
            </td>
           <td>
            <label id="Label16">
	            <%= GetLocalResourceObject("cbenPromissory_NoteCaption")%></label>
            </td>
            <td>
	            <%=mobjValues.PossiblesValues("cbenPromissory_Note", "table9300", eFunctions.Values.eValuesType.clngComboType, mobjCertificat.nPromissory_Note, , , , , , , mobjCertificat.StateVarCa004(9), , GetLocalResourceObject("cbenPromissory_NoteToolTip")) %>
            </td>
             </tr>
                <tr>
            <td>
                <label id="Label11">
                    <%= GetLocalResourceObject("cbeBill_indCaption") %></label>
            </td>
            <td>
                <%=mobjValues.ComboControl("cbeBill_ind",  GetLocalResourceObject("cbeBill_indOptions"), mobjCertificat.sBill_ind, False, 16, GetLocalResourceObject("cbeBill_indToolTip"),  , mobjCertificat.StateVarCA004(36))%>
            </td>
            <!--<TD COLSPAN="2"><%=mobjValues.CheckControl("chkBill_Ind", GetLocalResourceObject("chkBill_IndCaption"), mobjCertificat.sBill_ind, "1",  , mobjCertificat.StateVarCA004(36), 13, GetLocalResourceObject("chkBill_IndToolTip"))%></TD> -->
            <td>
                &nbsp;
            </td>
        </tr>
            <td>
                <label id="LABEL8">
                    <%= GetLocalResourceObject("cbeReceipt_indCaption") %></label>
            </td>
            <td>
                <%= mobjValues.ComboControl("cbeReceipt_ind", GetLocalResourceObject("cbeReceipt_indOptions"), mobjCertificat.sReceipt_ind, False, 16, GetLocalResourceObject("cbeReceipt_indToolTip"), "InsChangeReceipt_ind();", mobjCertificat.StateVarCa004(45))%>
            </td>
            <td>
                &nbsp;
            </td>
            <!--<TD><LABEL ID=0><%= GetLocalResourceObject("tcnTerm_graceCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnTerm_grace", 3, mintTerm_grace,  , GetLocalResourceObject("tcnTerm_graceToolTip"),  ,  ,  ,  ,  ,  , mobjCertificat.StateVarCA004(43), 19)%></TD>		    -->
            <%Response.Write(mobjValues.HiddenControl("tcnTerm_grace", mintTerm_grace))%>
            <!--<TD COLSPAN="2"><%=mobjValues.CheckControl("chkBill_Ind", GetLocalResourceObject("chkBill_IndCaption"), mobjCertificat.sBill_ind, "1",  , mobjCertificat.StateVarCA004(36), 13, GetLocalResourceObject("chkBill_IndToolTip"))%></TD> -->
        </tr>
        <% End If%>
        <tr>
            <td colspan="2" class="HighLighted">
                <label id="0">
                    <a name="Datos de la cobranza">
                        <%= GetLocalResourceObject("AnchorDatos de la cobranza2Caption") %></a></label>
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2" class="HighLighted">
                <label id="0">
                    <a name="Vía de cobro">
                        <%= GetLocalResourceObject("AnchorVía de cobro2Caption") %></a></label>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="Horline">
            </td>
            <td>
            </td>
            <td colspan="2" class="Horline">
            </td>
        </tr>
        <tr>
            <td>
                <label id="12971">
                    <%= GetLocalResourceObject("cbeWayPayCaption") %></label>
            </td>
            <td>
                <%		
                    With mobjValues
                        .BlankPosition = True
                        .Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nPolicy", mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nCertif", mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("ncodigint", mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        Response.Write(mobjValues.PossiblesValues("cbeWayPay", "TABWAY_PAY_CA004", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nWay_pay), True, , , , , "InsChangeWayPay(" & Session("nCertif") & ",0,0," & lstrApv & ");", mobjCertificat.StateVarCa004(21), , GetLocalResourceObject("cbeWayPayToolTip"), , 17))
                    End With
                %>
            </td>
            <td>
                &nbsp;
            </td>
              <td style="display:none">
                <label id="Label15">
                    <%= GetLocalResourceObject("cbenFormPayCaption") %></label>
            </td>
            <td style="display:none">
                <%
                    Response.Write(mobjValues.PossiblesValues("cbenFormPay", "table9050", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nFormPay), , , , , , , , , GetLocalResourceObject("cbenFormPayToolTip")))
                    
                    %>
            </td>
        </tr>
        <tr>
            <td>
                <label id="10295">
                    <%= GetLocalResourceObject("valCollectorCaption") %></label>
            </td>
            <td>
                <%=mobjValues.PossiblesValues("valCollector", "tabCollector_Cliname", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjCertificat.nCollector),  ,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("valCollectorToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="1">
                <%=mobjValues.OptionControl(0, "optDirTyp", GetLocalResourceObject("optDirTyp_CStr1Caption"), mobjCertificat.sDirind, CStr(1),  , mobjCertificat.StateVarCA004(20), 20, GetLocalResourceObject("optDirTyp_CStr1ToolTip"))%>
            </td>
            <td colspan="2">
                <%=mobjValues.OptionControl(0, "optDirTyp", GetLocalResourceObject("optDirTyp_CStr2Caption"), CStr(CShort(mobjCertificat.sDirind) - 1), CStr(2),  , mobjCertificat.StateVarCA004(20),  , GetLocalResourceObject("optDirTyp_CStr2ToolTip"))%>
            </td>
            
        </tr>
        <!--		<TR>
            <TD COLSPAN="2"></TD>
			<TD COLSPAN="2"><LABEL ID=0><%= GetLocalResourceObject("valOriginCaption") %></LABEL></TD>

			<TD><%	
			        mobjValues.BlankPosition = True
			        
'+ Los valores de las cuentas origen se leen de la tabla "TAB_ORD_ORIGIN"
			        mobjValues.Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			        mobjValues.Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			        mobjValues.Parameters.Add("nCollecDocTyp", mobjCertificat.nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			        
			        If mobjProduct_li.nProdClas = 4 Then
			            If mobjProduct_li.sApv = "1" Then
			                Response.Write(mobjValues.PossiblesValues("valOrigin", "TAB_ORIGIN", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjCertificat.nOrigin), True, , , , , , False, , GetLocalResourceObject("valOriginToolTip")))
			            Else
			                Response.Write(mobjValues.PossiblesValues("valOrigin", "TABLE5633", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjCertificat.nOrigin), False, , , , , , True, , GetLocalResourceObject("valOriginToolTip"), , , , , mobjCertificat.sOrigin))
			            End If
			        Else
			            Response.Write(mobjValues.PossiblesValues("valOrigin", "TAB_ORIGIN", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjCertificat.nOrigin), True, , , , , , mobjCertificat.StateVarCa004(37), , GetLocalResourceObject("valOriginToolTip")))
			        End If
%>
			</TD>
		</TR>-->
        <%
            If mobjValues.insGetSetting("Hide", "No", "CA004_CommissionAFP").ToUpper = "YES" Then
                Response.Write(mobjValues.HiddenControl("tcnAFPCommi", ""))
                Response.Write(mobjValues.HiddenControl("cbeCurrency", ""))
            Else%>
        <tr>
            <td>
                <label id="0">
                    <%= GetLocalResourceObject("tcnAFPCommiCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnAFPCommi", 18, CStr(mobjCertificat.nAFP_Commiss),  , GetLocalResourceObject("tcnAFPCommiToolTip"), True, 6,  ,  ,  ,  , mobjCertificat.StateVarCA004(38))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="0">
                    <%= GetLocalResourceObject("cbeCurrencyCaption") %></label>
            </td>
            <td>
                <%
                    mobjValues.BlankPosition = True
                    Response.Write(mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nAFP_Comm_Curr), , , , , , , mobjCertificat.StateVarCa004(39), , GetLocalResourceObject("cbeCurrencyToolTip")))
                %>
            </td>
        </tr>
        <% End If%>
        <tr>
            <td>
                <label id="12972">
                    <%= GetLocalResourceObject("tcnBillDayCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnBillDay", 2, CStr(mobjCertificat.nBill_day), , GetLocalResourceObject("tcnBillDayToolTip"), False, , , , , " Billday();", mobjCertificat.StateVarCa004(22), 18)%>
                <%=mobjValues.HiddenControl("hddBillDay", CStr(mobjCertificat.nBill_day))%>
            </td>
            <td>
                &nbsp;
            </td>
        
              
      
            <td>
                <label id="12973">
                    <%= GetLocalResourceObject("valAgreementCaption") %></label>
            </td>
            <% 
                '+ Los valores de los convenios pueden ser por polizas.
                mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            %>
            <td>
                <%= mobjValues.PossiblesValues("valAgreement", "tabAgreementpol", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjCertificat.nCod_Agree), True , , , , , "InsChangeAgreement();", mobjCertificat.StateVarCa004(23), 5, GetLocalResourceObject("valAgreementToolTip"))%>
            </td>
            <tr>
              <td style="display:none">
                  <%=mobjValues.CheckControl("chksInd_IFI", GetLocalResourceObject("chksInd_IFICaption"), mobjCertificat.sInd_IFI, "1", , , 13, GetLocalResourceObject("chksInd_IFIToolTip"))%>
                </td>
            <td colspan="2">
                &nbsp;
            </td>
              <td>
                <label id="Label14">
                  &nbsp; </label>
            </td>
            <td>
               &nbsp;
            </td>
            </tr>
        </tr>
        <tr>
            <td>
                <label id="Label12">
                    <%= GetLocalResourceObject("cbenSpecialbusinessCaption") %></label>
                
            </td>
            <td>
                <%
                    Response.Write(mobjValues.PossiblesValues("cbenSpecialbusiness", "table5770", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nSpecialbusiness), , , , , , , , , GetLocalResourceObject("cbenSpecialbusinessToolTip")))
                    
                    %>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HighLighted">
                <label id="0">
                    <a name="Varios">
                        <%= GetLocalResourceObject("AnchorVarios2Caption") %></a></label>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="Horline">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <%=mobjValues.CheckControl("chkDeclarative", GetLocalResourceObject("chkDeclarativeCaption"), mobjCertificat.sDeclari,  ,  , mobjCertificat.StateVarCA004(10), 28, GetLocalResourceObject("chkDeclarativeToolTip"))%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="12966">
                    <%= GetLocalResourceObject("tcnCopiesCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnCopies", 5, CStr(mobjCertificat.nCopies),  , GetLocalResourceObject("tcnCopiesToolTip"),  ,  ,  ,  ,  ,  , mobjCertificat.StateVarCA004(12), 23)%>
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <%=mobjValues.CheckControl("chkRenewalAut", GetLocalResourceObject("chkRenewalAutCaption"), mobjCertificat.sRenewal,  ,  , mobjCertificat.StateVarCA004(11), 29, GetLocalResourceObject("chkRenewalAutToolTip"))%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="12967">
                    <%= GetLocalResourceObject("tcnDaysNullCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnDaysNull", 5, CStr(mobjCertificat.nNotice),  , GetLocalResourceObject("tcnDaysNullToolTip"),  ,  ,  ,  ,  ,  , mobjCertificat.StateVarCA004(13), 24)%>
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <%=mobjValues.CheckControl("chkNoNull", GetLocalResourceObject("chkNoNullCaption"), mobjCertificat.sNoNull,  ,  , mobjCertificat.StateVarCA004(14), 30, GetLocalResourceObject("chkNoNullToolTip"))%>
            </td>
        </tr>
        <tr>
            <%If Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotation Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifQuotation Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyProposal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifProposal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotAmendent Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyPropAmendent Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifQuotAmendent Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifPropAmendent Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotRenewal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyPropRenewal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifQuotRenewal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifPropRenewal Then%>
            <td>
                <label id="0">
                    <%= GetLocalResourceObject("tcnDays_quotCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnDays_quot", 4, CStr(mobjCertificat.nDays_quot),  , GetLocalResourceObject("tcnDays_quotToolTip"),  ,  ,  ,  ,  ,  , mobjCertificat.StateVarCA004(32), 25)%>
            </td>
            <%Else%>
            <td colspan="2">
                &nbsp;
            </td>
            <%End If%>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <%=mobjValues.CheckControl("chkExemption", GetLocalResourceObject("chkExemptionCaption"), mobjCertificat.sExemption, "1",  , True, 31, GetLocalResourceObject("chkExemptionToolTip"))%>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <%=mobjValues.CheckControl("chksNopayroll", GetLocalResourceObject("chksNopayrollCaption"), mobjCertificat.sNopayroll,  , "insValLeg(this);", mobjCertificat.StateVarCA004(34), 27, GetLocalResourceObject("chksNopayrollToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <%
                    If mobjCertificat.sNopayroll = "1" Then
                        Response.Write(mobjValues.CheckControl("chksLeg", GetLocalResourceObject("chksLegCaption"), CStr(False), "1", "insValLeg(this);", True, 26, GetLocalResourceObject("chksLegToolTip")))
                    Else
                        Response.Write(mobjValues.CheckControl("chksLeg", GetLocalResourceObject("chksLegCaption"), mobjCertificat.sLeg, "1", "insValLeg(this);", mobjCertificat.StateVarCa004(27), 26, GetLocalResourceObject("chksLegToolTip")))
                    End If
                %>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <%=mobjValues.CheckControl("chksReinst", GetLocalResourceObject("chksReinstCaption"), mobjCertificat.sReinst,  ,  , True, 32, GetLocalResourceObject("chksReinstToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
            <%If CStr(Session("sPolitype")) = "2" Then%>
            <td>
                <label id="0">
                    <%= GetLocalResourceObject("cbeRepInsuredCaption") %></label>
            </td>
            <td>
                <%	
                    mobjValues.BlankPosition = False
                    Response.Write(mobjValues.PossiblesValues("cbeRepInsured", "Table5677", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nRepInsured), , , , , , , mobjCertificat.StateVarCa004(42), , GetLocalResourceObject("cbeRepInsuredToolTip")))
                %>
            </td>
            <%Else%>
            <td colspan="2">
                &nbsp;
            </td>
            <%End If%>
            </TD>
        </tr>
        <tr>
            <td colspan="2">
                <%=mobjValues.CheckControl("chksInsubank", GetLocalResourceObject("chksInsubankCaption"), mobjCertificat.sInsubank,  ,  , mobjCertificat.StateVarCA004(33), 33, GetLocalResourceObject("chksInsubankToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <%=mobjValues.CheckControl("chkFracReceip", GetLocalResourceObject("chkFracReceipCaption"), mobjCertificat.sFracReceip, mobjCertificat.sFracReceip,  , True, 34, GetLocalResourceObject("chkFracReceipToolTip"))%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="12973">
                    <%= GetLocalResourceObject("valgroup_AgreeCaption") %></label>
            </td>
            <td>
                <%=mobjValues.PossiblesValues("valgroup_Agree", "tabAgreement", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjCertificat.nGroup_Agree),  ,  ,  ,  ,  ,  , mobjCertificat.StateVarCA004(41), 5, GetLocalResourceObject("valgroup_AgreeToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="0">
                    <%= GetLocalResourceObject("tctcumul_codeCaption") %></label>
            </td>
            <td>
                <%=mobjValues.TextControl("tctcumul_code", 14, mobjCertificat.sCumul_code,  , GetLocalResourceObject("tctcumul_codeToolTip"),  ,  ,  ,  , mobjCertificat.StateVarCA004(44))%>
            </td>
        </tr>
    </table>
    <%Response.Write(mobjValues.BeginPageButton)
        Response.Write(mobjValues.HiddenControl("hhDirTyp", vbNullString))
        Response.Write(mobjValues.HiddenControl("hhprodclas", CStr(mobjProduct_li.nProdClas)))
        Response.Write(mobjValues.HiddenControl("cbeSendAddr", CStr(1)))
    %>
    </form>
</body>
</html>
<%
    If Not mobjValues.ActionQuery And Not mobjCertificat.StateVarCa004(21) Then
        Response.Write("<script>var nNoChange = '1'; InsChangeWayPay(" & Session("nCertif") & " , " & mobjCertificat.sDirind & " , 1 ," & lstrApv & "); ChangeApplication(self.document.forms[0].cbeIndexType);  </script>")
    End If

    mobjValues = Nothing
    mobjMenu = Nothing
    mobjCertificat = Nothing
    mobjProduct_li = Nothing
    mobjProduct = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
    Call mobjNetFrameWork.FinishPage("CA004")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
