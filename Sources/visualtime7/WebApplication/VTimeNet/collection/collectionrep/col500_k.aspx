<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.47.59
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim variable As Object
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = SESSION.SessionID
mobjNetFrameWork.nUsercode = SESSION("nUsercode")
Call mobjNetFrameWork.BeginPage("col500_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjValues.sSessionID = SESSION.SessionID
mobjValues.nUsercode = SESSION("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "col500_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjMenu.sSessionID = SESSION.SessionID
mobjMenu.nUsercode = SESSION("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <SCRIPT>
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 5 $|$$Date: 6/08/04 20:44 $|$$Author: Nvapla10 $"
	</SCRIPT>


<SCRIPT>

//% insCancel(): Retorna un valor booleano para aceptar cancelación de operación.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insChangeWayPay: Actualiza los objetos de la forma, según el tipo de vía de pago.
//-------------------------------------------------------------------------------------------
function insChangeWayPay(lobjWayPay) {
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){		
		switch (lobjWayPay.value) {
//+         Vía de pago es PAC 
			case "1":
                valAgreement.value = "";
                valAgreement.disabled = true ;
                btnvalAgreement.disabled = true ;
         		UpdateDiv('valAgreementDesc','');
			    cbeBank.value = "";
				cbeBank.disabled = false;
				btncbeBank.disabled = false;
				UpdateDiv('cbeBankDesc', '')
				cbeTyp_CreCard.disabled = true;
				cbeTyp_CreCard.value = "";
				//optGenera[0].disabled = true ;
				//optGenera[1].disabled = true ;
				//optGenera[2].disabled = true ;
     			optGenera[2].checked="1"; 
	    		optGenera[2].value="3";	
				optCurrency[0].disabled = false ;
				optCurrency[1].disabled = false;
     			optCurrency[1].checked="2"; 
	    		optCurrency[1].value="2";	
				tcdIncrease.disabled = false;
				btn_tcdIncrease.disabled = false;
				//chkTakeOld.checked = true;
				//chkTakeOld.disabled = true;
				tcdIncrease.value = "";
				break
//+         Vía de pago es Transbank 
			case "2":
                valAgreement.value = "";
                valAgreement.disabled = true; 
                btnvalAgreement.disabled = true; 
         		UpdateDiv('valAgreementDesc','');
			    cbeBank.value = "";
				cbeBank.disabled = true;
				btncbeBank.disabled = true;
				UpdateDiv('cbeBankDesc', '');
				cbeTyp_CreCard.disabled = false;
				//optGenera[0].disabled = true ;
				//optGenera[1].disabled = true ;
				//optGenera[2].disabled = true ;
     			optGenera[2].checked="1"; 
	    		optGenera[2].value="3";
	    		optCurrency[0].disabled = false;
	    		optCurrency[1].disabled = false;
     			optCurrency[1].checked="2"; 
	    		optCurrency[1].value="2";	
				tcdIncrease.disabled = false;
				btn_tcdIncrease.disabled = false;
				//chkTakeOld.checked = true;
				tcdIncrease.value = "";
				//chkTakeOld.disabled = true;
				break;
//+         Vía de pago es Planilla --> Convenio
			case "3":
                valAgreement.value = "";
                valAgreement.disabled = false; 
                btnvalAgreement.disabled = false; 
			    cbeBank.value = "";
				cbeBank.disabled = true;
				btncbeBank.disabled = true;
				UpdateDiv('cbeBankDesc', '');
				cbeTyp_CreCard.disabled = true;
				cbeTyp_CreCard.value = "";
				//optGenera[0].disabled = false ;
				//optGenera[1].disabled = false ;
				//optGenera[2].disabled = false ;
     			optGenera[1].checked="2"; 
	    		optGenera[1].value="2";	
				optCurrency[0].disabled = false ;
				optCurrency[1].disabled = false;
				optCurrency[1].checked = "2";
				optCurrency[1].value = "2";	
				tcdIncrease.disabled = false;
				btn_tcdIncrease.disabled = false;
				tcdIncrease.value = tcdExpirDat.value;
				//chkTakeOld.checked = false;
				//chkTakeOld.disabled = false;
				break
//+         Vía de pago es Boletin --> Aviso
			case "4": 
                valAgreement.value = "";
                valAgreement.disabled = true; 
                btnvalAgreement.disabled = true; 
         		UpdateDiv('valAgreementDesc','');
			    cbeBank.value = "";
				cbeBank.disabled = true;
				btncbeBank.disabled = true;
				UpdateDiv('cbeBankDesc', '');
				cbeTyp_CreCard.disabled = true;
				cbeTyp_CreCard.value = "";
				//optGenera[0].disabled = false ; 
				//optGenera[1].disabled = false ; 
				//optGenera[2].disabled = false ; 
     			optGenera[1].checked="2";  
	    		optGenera[1].value="2";	 
				optCurrency[0].disabled = false ;
				optCurrency[1].disabled = false;
				optCurrency[0].checked = "2";
				optCurrency[0].value = "1";	
				//chkTakeOld.checked = false;
				tcdIncrease.disabled = false;
				btn_tcdIncrease.disabled = false;
				tcdIncrease.value = tcdExpirDat.value;
				//chkTakeOld.disabled = false;
				break;
//+         Vía de pago es Default 
			default:
                valAgreement.value = "";
                valAgreement.disabled = true; 
                btnvalAgreement.disabled = true; 
         		UpdateDiv('valAgreementDesc','');
			    cbeBank.value = "";
				cbeBank.disabled = true;
				btncbeBank.disabled = true;
				UpdateDiv('cbeBankDesc', '');
				cbeTyp_CreCard.disabled = true;
				cbeTyp_CreCard.value = "";
				//optGenera[0].disabled = false ;
				//optGenera[1].disabled = false ;
				//optGenera[2].disabled = false ;
     			optGenera[0].checked="1"; 
	    		optGenera[0].value="1";	
				optCurrency[0].disabled = false ;
				optCurrency[1].disabled = false ;
     			optCurrency[0].checked="1"; 
	    		optCurrency[0].value="1";	
			    tcdIncrease.value = "";
				tcdIncrease.value = "";
				//chkTakeOld.checked = false;
				//chkTakeOld.disabled = false;
		}
    }
}

//% insChangeCurrency: Actualiza los objetos de la forma, según el tipo de via de pago.
//-------------------------------------------------------------------------------------------
function insChangeCurrency(lobjCurrency) {
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
	    if (cbeWay_pay.value!=4)
		    switch (lobjCurrency.value) {
//+         Moneda de pago Origen 
				case "1":
				    tcdIncrease.value = "";
					tcdIncrease.disabled = true;
					btn_tcdIncrease.disabled = true;
					break;
//+         Moneda de pago Local 
				case "2":
				    tcdIncrease.value = "";
					tcdIncrease.disabled = false;
					btn_tcdIncrease.disabled = false;
					break;
			}
    }
}

// SetAgree: Asigna valor al parámetro de la lista de valores de Contratantes
//-----------------------------------------------------------------------------------
function SetAgree(Field){
//-----------------------------------------------------------------------------------
    if (Field != "" && Field != 0){
        with(self.document.forms[0]){
            valClient.Parameters.Param1.sValue=valAgreement.value}
	        self.document.forms[0].valClient.disabled = false;
	        self.document.forms[0].btnvalClient.disabled = false;
	        }
    else{
	     self.document.forms[0].valClient.disabled = true;
	     self.document.forms[0].btnvalClient.disabled = true;
	    }
}

// SetClient: Asigna ceros al código del contratante
//-----------------------------------------------------------------------------------
function SetClient(Field){
//-----------------------------------------------------------------------------------
    
    if (Field != ""){
        self.document.forms[0].valClient.value = InsValuesCero(Field);
    }
}
</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("COL500", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.MakeMenu("COL500", "COL500_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), SESSION("sDesMultiCompany"), SESSION("sSche_code")))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "COL500_k.aspx"))
End With
    mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCollectGen" ACTION="valCollectionRep.aspx?mode=1">
	<BR><BR>
    <%Response.Write(mobjValues.ShowWindowsName("COL500", Request.QueryString.Item("sWindowDescript")))%>
	<TABLE WIDTH="100%">
        <TR>
            <TD WIDTH = 25%>&nbsp;</TD>
            <TD WIDTH = 35%>&nbsp;</TD>
            <TD WIDTH = 5%>&nbsp;</TD>
            <TD WIDTH = 20%>&nbsp;</TD>
            <TD WIDTH = 15%>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=9909><%= GetLocalResourceObject("tcdExpirDatCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdExpirDat", CStr(Today),  , GetLocalResourceObject("tcdExpirDatToolTip"))%></TD>
		    <TD>&nbsp;</TD>
		    <TD COLSPAN="2" CLASS="HighLighted" ><LABEL ID=0><A NAME="Tipo de generación"><%= GetLocalResourceObject("AnchorTipo de generaciónCaption") %></A></LABEL></TD>
        </TR> 
        <TR>
		    <TD COLSPAN="2" ></TD>
		    <TD></TD>
		    <TD COLSPAN="2" CLASS="HorLine" ></TD>
        </TR>
        <TR> 
            <TD><LABEL ID=9906><%= GetLocalResourceObject("cbeWay_payCaption") %></LABEL></TD> 
            <TD><LABEL TITLE="Vía de pago">
            <%With Response
	            mobjValues.TypeList = 1
	            mobjValues.List = "1,2,3,4"
	            .Write(mobjValues.PossiblesValues("cbeWay_pay", "table5002", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insChangeWayPay(this)",  ,  , GetLocalResourceObject("cbeWay_payToolTip")))
            End With
            %>&nbsp;</LABEL></TD> 
            <TD>&nbsp;</TD> 
		    <TD COLSPAN="2"><%= mobjValues.OptionControl(0, "optGenera", GetLocalResourceObject("optGenera_1Caption"), , "1")%> </TD>
        </TR> 
        <TR> 
        	<TD><LABEL ID=12973><%= GetLocalResourceObject("valAgreementCaption") %></LABEL></TD> 
            <TD><%=mobjValues.PossiblesValues("valAgreement", "tabAgreement", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  , "SetAgree(this.value)", True,  , GetLocalResourceObject("valAgreementToolTip"))%></TD> 
            <TD>&nbsp;</TD> 
		    <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optGenera", GetLocalResourceObject("optGenera_2Caption"), "1"  , "2")%> </TD>
        </TR> 
        <TR> 
            <TD><LABEL ID=9906><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD> 
            <TD><%=mobjValues.BranchControl("cbeBranch", "Ramos de Seguros")%>&nbsp;</TD> 
           
            <TD>&nbsp;</TD> 
		    <TD	COLSPAN="2"><%=mobjValues.OptionControl(0, "optGenera", GetLocalResourceObject("optGenera_3Caption"),  , "3")%> </TD>
        
        </TR>	
        
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("ValProductCaption")%></LABEL></TD>
		    <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("ValProductToolTip"), , eFunctions.Values.eValuesType.clngWindowType, , , , , , )%></TD> 
        </TR>
        <TR> 
            <TD><LABEL ID=LABEL1><%= GetLocalResourceObject("cbeInsur_areaCaption") %></LABEL></TD>
         	<TD COLSPAN="4"><%=mobjValues.PossiblesValues("cbeInsur_area", "Table5001", eFunctions.Values.eValuesType.clngComboType, Session("nInsur_area"), ,  ,  ,  ,  ,  , ,  , GetLocalResourceObject("cbeInsur_areaToolTip"))%></TD>  
        </TR> 

        <TR> 
            <TD><LABEL ID=9906><%= GetLocalResourceObject("cbeBankCaption") %></LABEL></TD>
				<%mobjValues.Parameters.Add("sType_BankAgree", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%> 
         	<TD COLSPAN="4"><%=mobjValues.PossiblesValues("cbeBank", "TabBank_Agree_Banks", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBankToolTip"))%></TD>  
        </TR> 

        <TR> 
            <TD><LABEL ID=0><%= GetLocalResourceObject("valClientCaption") %></LABEL></TD>
				<%mobjValues.Parameters.Add("nCod_Agree", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
         	<TD COLSPAN="4"><%=mobjValues.PossiblesValues("valClient", "TabRole_Agree", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "SetClient(this)", True, 14, GetLocalResourceObject("valClientToolTip"))%></TD>  
        </TR>

        <TR> 
            <TD><LABEL ID=LABEL2><%= GetLocalResourceObject("cbeTyp_CreCardCaption") %></LABEL></TD>
         	<TD COLSPAN="4"><%=mobjValues.PossiblesValues("cbeTyp_CreCard", "Table183", eFunctions.Values.eValuesType.clngComboType, , , , , , , ,True, ,GetLocalResourceObject("cbeTyp_CreCardToolTip"))%></TD>  
        </TR> 
    
        <TR>
		    <TD COLSPAN="2" CLASS="HighLighted" ><LABEL ID=0><A NAME="Moneda de generacion"><%= GetLocalResourceObject("AnchorMoneda de generacionCaption") %></A></LABEL></TD>
		    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Proceso"><%= GetLocalResourceObject("AnchorProcesoCaption") %></A></LABEL></TD>
		</TR>
		<TR>
		    <TD COLSPAN="2" CLASS="HorLine"></TD>
            <TD></TD> 
		    <TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>
		<TR>
		    <TD COLSPAN="3"><%=mobjValues.OptionControl(0, "optCurrency", GetLocalResourceObject("optCurrency_1Caption"), "1", "1")%> </TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), "1", "1")%> </TD>
        </TR>
		<TR>
		    <TD COLSPAN="3"><%=mobjValues.OptionControl(0, "optCurrency", GetLocalResourceObject("optCurrency_2Caption"), "2", "2")%> </TD>
		    <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"),  , "2")%> </TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdIncreaseCaption") %></LABEL></TD>
            <TD COLSPAN="2"><%=mobjValues.DateControl("tcdIncrease",  ,  , GetLocalResourceObject("tcdIncreaseToolTip"))%></TD>
            <TD><%=mobjValues.HiddenControl("chkTakeOld", "")%> </TD>
        </TR> 
      
    </TABLE> 
</FORM> 
</BODY> 
</HTML> 
<%mobjValues = Nothing%>
<%  '^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59    
    Call mobjNetFrameWork.FinishPage("col500_k")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





