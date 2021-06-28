<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'**- Object for the managing of the general functions of load of values.
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    

'**- Object for the managing of the table Life.
'- Objeto para el manejo de la tabla Life.

Dim mclsProduct As eProduct.Product
Dim mclsGroups As ePolicy.Groups
Dim mclsSituation As ePolicy.Situation
Dim mclsCertificat As ePolicy.Certificat
Dim mclsProduct_li As eProduct.Product
Dim mclsLife As ePolicy.Life
Dim mclsFunds As ePolicy.Funds 

Dim mblnGroups As Boolean
Dim mblnSituation As Boolean
Dim mblnFunds As Boolean 

Dim nTypdurins As Integer
Dim nTypdurpay As Object
Dim nPay_time As Object


'% insPreVI7001: Realiza la lectura de los campos a mostrar en pantalla
'----------------------------------------------------------------------------------------------
Private Sub insPreVI7001()
	'----------------------------------------------------------------------------------------------
	With mobjValues
		Call mclsProduct.insInitialVI7001(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nTransaction"))
		
		nTypdurins = mclsProduct.nTypdurins
		
		mblnGroups = mclsGroups.valGroupExist(Session("sCertype"), .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), .StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"))
		
		mblnSituation = mclsSituation.insReaSituation(Session("sCertype"), .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), .StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble))
            
        mblnFunds = mclsFunds.Find(.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), Session("dEffecdate"))
		
		Call mclsProduct_li.FindProduct_li(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), True)
		
		Call mclsLife.Find(Session("sCertype"), .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), .StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("ncertif"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"))
		
		If mclsLife.nTypdurpay <> eRemoteDB.Constants.intNull And mclsLife.nTypdurpay <> 0 Then
			nTypdurpay = mclsLife.nTypdurpay
			nPay_time = mclsLife.nPay_time
		Else
			nTypdurpay = mclsProduct_li.nTypdurpay
			nPay_time = mclsProduct.nPay_time
		End If
		
	End With
	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
mclsProduct = New eProduct.Product
mclsGroups = New ePolicy.Groups
mclsSituation = New ePolicy.Situation
mclsCertificat = New ePolicy.Certificat
mclsProduct_li = New eProduct.Product
mclsLife = New ePolicy.Life
mclsFunds = New ePolicy.Funds 

mobjValues.ActionQuery = Session("bQuery")

Call insPreVI7001()
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 23-09-09 18:56 $|$$Author: Ljimenez $"


//**% DisabledFields: Disabled the fields according to the characteristics of the product.
//% DisabledFields: Inhabilita los campos de acuerdo a las características del producto.
//-------------------------------------------------------------------------------------------
function DisabledFields(sOption){
//-------------------------------------------------------------------------------------------
	switch(sOption){
		case "sId2": 
			self.document.forms[0].tcnInsurTimeAgeLimit.disabled=true
			break;
		case "sId3":
			self.document.forms[0].tcnInsurTimeAge.disabled=true
			break;
		case "sPd2":
			self.document.forms[0].tcnInsurPayTimeAgeLimit.disabled=true
			break;
		case "sPd3":
			self.document.forms[0].tcnInsurPayTimeAge.disabled=true
			break;
	}
}
//% insChangeField: se controla la modificación de los campos de parametros
//--------------------------------------------------------------------------------------------
function insChangeField(nPremdeal){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		if (insConvertNumber(hddnRatepayf.value) == 0 && cbePayFreq.value == 1){
			ldblRatepayf = 1
		}
		else{
			ldblRatepayf = insConvertNumber(hddnRatepayf.value)
		}
        if (nPremdeal!=''){
			llngPremDeal = insConvertNumber(nPremdeal) * ldblRatepayf;
            tcnPremdeal.value = VTFormat(llngPremDeal, '', '', '', 6, true);
        }
        else tcnPremdeal.value = '';
    }
}


//%insEnabled: Si no incluye información en el campo "%Inversión en cuentas de ahorro", 
//% (nSaving_pct) no se habilitan el resto de los campos de esta ventana.
//------------------------------------------------------------------------------
function insEnabled(sSaving_pct){
//------------------------------------------------------------------------------

	var lblnDisabled;
	lblnDisabled = (sSaving_pct=='0' || sSaving_pct=='')?true:false;

	with (self.document.forms[0]){
        if (lblnDisabled){
		    cbeIndex_table.value='';
		    valWarrn_table.value='';
		    UpdateDiv('valWarrn_tableDesc','','NoPopUp');
		}
		cbeIndex_table.disabled=lblnDisabled;
		valWarrn_table.disabled=lblnDisabled;
		btnvalWarrn_table.disabled=lblnDisabled;
			
		switch (sSaving_pct) {
			case '0'  : tcnDisc_save_pct.value=0;
			            tcnDisc_unit_pct.value=100;
			            tcnDisc_save_pct.disabled=true;
			            tcnDisc_unit_pct.disabled=true;
			            break;
			case '100': tcnDisc_save_pct.value=100;
			            tcnDisc_unit_pct.value=0;
			            tcnDisc_save_pct.disabled=true;
			            tcnDisc_unit_pct.disabled=true;
			            break;
			default   : tcnDisc_save_pct.disabled=false;
			            tcnDisc_unit_pct.disabled=false;
	    }
	}

}

//%insEnabledWarrn_table: 
//------------------------------------------------------------------------------
function insEnabledWarrn_table(Index_table) {
    //------------------------------------------------------------------------------
    var lblnDisabled;
    lblnDisabled = (Index_table == 1 || Index_table == 2 || Index_table == 3) ? false : true;

    with (self.document.forms[0]) {
        if (lblnDisabled) {
            valWarrn_table.value = '';
            UpdateDiv('valWarrn_tableDesc', '', 'NoPopUp');
        }
        valWarrn_table.disabled = lblnDisabled;
        btnvalWarrn_table.disabled = lblnDisabled;

    }

}

//% Oculta campos
function insInitialFieldVisibility(){
//------------------------------------------------------------------------------
//+ Oculta División entre campos
	document.all.tags("TD")[61].style.display='none'
	
//+ Oculta Prima proyectada anual
	document.getElementsByTagName("TD")[62].style.display='none'
	document.getElementsByTagName("TD")[63].style.display='none'

//+ Oculta Frecuencia de pago
	document.getElementsByTagName("TD")[64].style.display='none'
	document.getElementsByTagName("TD")[65].style.display='none'

//+ Oculta División entre campos
	document.getElementsByTagName("TD")[66].style.display='none'

//+ Oculta Prima s/ frecuencia de pago
	document.getElementsByTagName("TD")[67].style.display='none'
	document.getElementsByTagName("TD")[68].style.display='none'
	
//+ Oculta % rentabilidad proyectada
	document.getElementsByTagName("TD")[69].style.display='none'
	document.getElementsByTagName("TD")[70].style.display='none'

//+ Oculta División entre campos
	document.getElementsByTagName("TD")[71].style.display='none'

//+ Oculta Prima mínima
	document.getElementsByTagName("TD")[72].style.display='none'
	document.getElementsByTagName("TD")[73].style.display='none'

}

//% insChangeValues: Se controla el estado de valor de los campos
//-------------------------------------------------------------------------------------------
function insChangeValues(Option, nTypDurins, Field){
//-------------------------------------------------------------------------------------------
	
	with(self.document.forms[0]){
		switch(Option){
			case "Dur_paymix":		

				if(typeof(btntcnPay_Time)!='undefined'){

					if(cbeTypDurpay.value=='' ||
                       cbeTypDurpay.value==0){
						tcnPay_Time.disabled=true;
						btntcnPay_Time.disabled=true;
					}
					else{
						tcnPay_Time.disabled=false;
						btntcnPay_Time.disabled=false;
					}
					
					tcnPay_Time.value='';
					tcnPay_Time.Parameters.Param5.sValue = cbeTypDurpay.value;
					UpdateDiv('btntcnPay_Time', '');
					//insDefValues("Pay_Time", "nInsur_Time=" + tcnInsur_Time.value + "&nTypDurins=" + cbeTypDurins.value + "&nTypDurpay=" + cbeTypDurpay.value, '/VTimeNet/Policy/PolicySeq');
		    }
		}
	}
}
</SCRIPT>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))

mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmVI7001" ACTION="../../Policy/PolicySeq/ValPolicySeq.aspx?nMainAction=301&nHolder=1">
    <P ALIGN="Center">
        <LABEL ID=0><A HREF="#Seguro"><%= GetLocalResourceObject("AnchorSeguroCaption") %></A></LABEL><LABEL ID=0> | </LABEL>
        <LABEL ID=0><A HREF="#Edades"><%= GetLocalResourceObject("AnchorEdadesCaption") %></A></LABEL><LABEL ID=0> | </LABEL>
        <LABEL ID=0><A HREF="#Adicional"><%= GetLocalResourceObject("AnchorAdicionalCaption") %></A></LABEL><LABEL ID=0> | </LABEL>
<!-- VI7001 - Interes Asegurable - Unit Linked -->
        <LABEL ID=0><A HREF="#Ahorros"><%= GetLocalResourceObject("AnchorAhorrosCaption") %></A></LABEL><LABEL ID=0> | </LABEL>
        <LABEL ID=0><A HREF="#Costos"><%= GetLocalResourceObject("AnchorCostosCaption") %></A></LABEL>
    </P>
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
	<TD>&nbsp;</TD>
    <TABLE WIDTH="100%" BORDER = "0">
        <TR>
			<TD WIDTH="25%"><LABEL ID=0><%= GetLocalResourceObject("cbovalGroupCaption") %></LABEL></TD>
<%With mobjValues.Parameters
	.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With%>

				<TD><%=mobjValues.PossiblesValues("cbovalGroup", "tabGroups", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsCertificat.nGroup), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbovalGroupToolTip"))%></TD>
				<TD WIDTH=8%>&nbsp;</TD>
				<TD><LABEL ID=0><%= GetLocalResourceObject("cbovalSituationCaption") %></LABEL></TD>

<%With mobjValues.Parameters
	.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With%>

				<TD><%=mobjValues.PossiblesValues("cbovalSituation", "tabSituation", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsCertificat.nSituation), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbovalSituationToolTip"))%></TD>
				</TR>
            <TD COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Seguro"><%= GetLocalResourceObject("AnchorSeguro2Caption") %></A></LABEL></TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Edades"><%= GetLocalResourceObject("AnchorEdades2Caption") %></A></LABEL></TD>
        </TR>

        </TR>
        <TR>
		    <TD COLSPAN="2" CLASS="Horline"></TD>
		    <TD></TD>
		    <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>

        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnInsurTimeAgeCaption") %></LABEL></TD>
			<%If mclsProduct_li.sIdurvari = "1" Or mclsProduct.nTypdurins = 5 Or mclsProduct.nTypdurins = 6 Then%>
					<TD><%=mobjValues.NumericControl("tcnInsurTimeAge", 2, CStr(mclsProduct.nInsurTimeAge),  , GetLocalResourceObject("tcnInsurTimeAgeToolTip"),  ,  ,  ,  ,  ,  , mclsProduct.bInsurTimeAge)%></TD>
			<%Else
	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nTypdurins", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With%>
					<TD><%=mobjValues.PossiblesValues("tcnInsurTimeAge", "Tabdurinsu_prod", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct.nInsurTimeAge), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnInsurTimeAgeToolTip"))%></TD>
			<%End If%>
            <TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnAgeCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAge", 2, CStr(mclsProduct.nAge),  , GetLocalResourceObject("tcnAgeToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
        </TR>

        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnInsurTimeAgeLimitCaption") %></LABEL></TD>
			<%If mclsProduct_li.sIdurvari = "1" Or mclsProduct.nTypdurins = 5 Or mclsProduct.nTypdurins = 6 Then%>
					<TD><%=mobjValues.NumericControl("tcnInsurTimeAgeLimit", 2, CStr(mclsProduct.nInsurTimeAgeLimit),  , GetLocalResourceObject("tcnInsurTimeAgeLimitToolTip"),  ,  ,  ,  ,  ,  , mclsProduct.bInsurTimeAgeLimit)%></TD>
			<%Else
	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nTypdurins", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With%>
					<TD><%=mobjValues.PossiblesValues("tcnInsurTimeAgeLimit", "Tabdurinsu_prod", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct.nInsurTimeAgeLimit), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnInsurTimeAgeLimitToolTip"))%></TD>
			<%End If%>
            <TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnAgeReinsuCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAgeReinsu", 2, CStr(mclsProduct.nAge_reinsu),  , GetLocalResourceObject("tcnAgeReinsuToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnAgeLimitCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAgeLimit", 2, CStr(mclsProduct.nAgeLimit),  , GetLocalResourceObject("tcnAgeLimitToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
        </TR>
        
        
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Seguro"><%= GetLocalResourceObject("AnchorSeguro3Caption") %></A></LABEL></TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
        </TR>                
        
        <TR>
		    <TD COLSPAN="2" CLASS="Horline"></TD>
		    <TD></TD>
		    <TD></TD>		    
        </TR>        
        
        
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeTypDurpayCaption") %></LABEL></TD>
			<%If mclsProduct_li.nTypdurpay = 7 Then%>
				<TD><%	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nTypdurins", mclsProduct_li.nTypdurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("cbeTypDurpay", "tabDurpay_Prod_mix2", eFunctions.Values.eValuesType.clngWindowType, nTypdurpay, True,  ,  ,  ,  , "insChangeValues(""Dur_paymix"", this.value, this)",  ,  , GetLocalResourceObject("cbeTypDurpayToolTip")))
	%>
				</TD>
			<%Else%>
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeTypDurpay", "Table5589", eFunctions.Values.eValuesType.clngWindowType, nTypdurpay,  ,  ,  ,  ,  , "insChangeValues(""Dur_paymix"", this.value, this)",  ,  , GetLocalResourceObject("cbeTypDurpayToolTip")))%></TD>
			<%End If%>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>        
        
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPay_TimeCaption") %></LABEL></TD>
			<%If mclsProduct_li.sPdurvari = "1" Or mclsProduct_li.nTypdurpay = 4 Then%>
			    <TD><%=mobjValues.NumericControl("tcnPay_Time", 5, nPay_time,  , GetLocalResourceObject("tcnPay_TimeToolTip"),  ,  ,  ,  ,  ,  , mclsProduct.bPay_Time, 9)%></TD>
			<%Else%>
                <TD>
                <%	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nTypdurins", mclsProduct_li.nTypdurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nTypdurpay", nTypdurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("tcnPay_Time", "tabDurpay_prod_2", eFunctions.Values.eValuesType.clngWindowType, nPay_time, True,  ,  ,  ,  ,  , False, 5, GetLocalResourceObject("tcnPay_TimeToolTip"),  ,  , False))%>
				</TD>
			<%End If%>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>        
        
        
        <TR>
			<TD COLSPAN="5">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><A NAME="Adicional"><%= GetLocalResourceObject("AnchorAdicional2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
		    <TD COLSPAN="5" CLASS="Horline"></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL><%=mobjValues.DIVControl("lblCurrency", True, mclsProduct.nCurrency & " " & mclsProduct.sCurrency)%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL><%= GetLocalResourceObject("tcnCapitalCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCapital", 18, mobjValues.StringToType(CStr(mclsProduct.nCapital_ca), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnCapitalToolTip"), True, 6)%></TD></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("valOptionCaption") %></LABEL></TD>
			<TD><%With mobjValues.Parameters
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			            .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			            
			            .Add("nModulec", mclsProduct.nModules, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			            
	.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
			        Response.Write(mobjValues.PossiblesValues("valOption", "TAB_OPTIONMODUL", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsProduct.nOption), True, , , , , , mclsProduct.bOption, , GetLocalResourceObject("valOptionToolTip")))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL><%= GetLocalResourceObject("tcnPremdeal_anuCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPremdeal_anu", 18, CStr(mclsProduct.nPremdeal_anu),  , GetLocalResourceObject("tcnPremdeal_anuToolTip"),  , 6,  ,  ,  , "insChangeField(this.value)", mclsProduct.bPremdeal)%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("cbePayFreqCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbePayFreq", "Table36", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct.nPayFreq),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbePayFreqToolTip"))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL><%= GetLocalResourceObject("tcnPremdealCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPremdeal", 18, CStr(mclsProduct.nPremdeal),  , GetLocalResourceObject("tcnPremdealToolTip"),  , 6,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnIntwarrCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnIntwarr", 8, CStr(mclsProduct.nIntwarr),  , GetLocalResourceObject("tcnIntwarrToolTip"),  , 6,  ,  ,  ,  , Not mclsProduct.bIntwarr)%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL><%= GetLocalResourceObject("tcnPremminCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPremmin", 18, CStr(mclsProduct_li.nPremMin),  , GetLocalResourceObject("tcnPremminToolTip"),  , 6,  ,  ,  ,  , True)%></TD>
        </TR>
			<TD COLSPAN = "5">&nbsp;</TD>
        <TR>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Ahorros"><%= GetLocalResourceObject("AnchorAhorros2Caption") %></A></LABEL></TD>
   			<TD>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Costos"><%= GetLocalResourceObject("AnchorCostos2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
		    <TD COLSPAN="2" CLASS="Horline"></TD>
   			<TD></TD>
		    <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>
        <TR>
            <% If mblnFunds Then %>
                <TD><LABEL><%= GetLocalResourceObject("tcnSaving_pctCaption") %></LABEL></TD>
                <TD><%=mobjValues.NumericControl("tcnSaving_pct", 3, CStr(mclsProduct.nSaving_pct_L),  , GetLocalResourceObject("tcnSaving_pctToolTip"),  , 0,  ,  ,  , "insEnabled(this.value)", mclsProduct.bDisc_save_pct_L)%></TD>
			    <TD>&nbsp;</TD>
			    <TD ><LABEL ID=LABEL1><%= GetLocalResourceObject("tcnDisc_save_pctCaption") %></LABEL></TD>
                <TD><%=mobjValues.NumericControl("tcnDisc_save_pct", 3, CStr(mclsProduct.nDisc_save_pct_L),  , GetLocalResourceObject("tcnDisc_save_pctToolTip"),  , 0,  ,  ,  ,  , mclsProduct.bDisc_save_pct_L)%></TD>
            <%Else%>
                <TD><LABEL><%= GetLocalResourceObject("tcnSaving_pctCaption") %></LABEL></TD>
                <TD><%=mobjValues.NumericControl("tcnSaving_pct", 3, 100,  , GetLocalResourceObject("tcnSaving_pctToolTip"),  , 0,  ,  ,  , "insEnabled(this.value)", True)%></TD>
			    <TD>&nbsp;</TD>
			    <TD ><LABEL ID=LABEL2><%= GetLocalResourceObject("tcnDisc_save_pctCaption") %></LABEL></TD>
                <TD><%=mobjValues.NumericControl("tcnDisc_save_pct", 3, 100,  , GetLocalResourceObject("tcnDisc_save_pctToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
            <%End If%>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeIndex_tableCaption") %></LABEL></TD>
            <%With mobjValues.Parameters
	            .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	            .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	            .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	            .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With%>

            <TD><%=mobjValues.PossiblesValues("cbeIndex_table", "TABPLAN_INTWAR", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsLife.nIndex_table), True, , , , , "insEnabledWarrn_table(this.value)", , 5, GetLocalResourceObject("cbeIndex_tableToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <% If mblnFunds Then %>
			    <TD><LABEL ID=0><%= GetLocalResourceObject("tcnDisc_unit_pctCaption") %></LABEL></TD>
                <TD><%=mobjValues.NumericControl("tcnDisc_unit_pct", 3, CStr(mclsProduct.nDisc_unit_pct_L),  , GetLocalResourceObject("tcnDisc_unit_pctToolTip"),  , 0,  ,  ,  ,  , mclsProduct.bDisc_unit_pct_L)%></TD>
            <%Else%>
			    <TD><LABEL ID=LABEL3><%= GetLocalResourceObject("tcnDisc_unit_pctCaption") %></LABEL></TD>
                <TD><%=mobjValues.NumericControl("tcnDisc_unit_pct", 3, 0,  , GetLocalResourceObject("tcnDisc_unit_pctToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
            <%End If%>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valWarrn_tableCaption") %></LABEL></TD>
			<%
                With mobjValues.Parameters
	                .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	                .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            %>
			<TD><%  mclsProduct.nWarrn_table_L = -32768
			        Response.Write(mobjValues.PossiblesValues("valWarrn_table", "TABTAB_APV_WARRAN", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsProduct.nWarrn_table_L), True, , , , , , mclsProduct.bWarrn_table_L, 5, GetLocalResourceObject("valWarrn_tableToolTip"), eFunctions.Values.eTypeCode.eNumeric))%></TD>

			<TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
        </TR>
    </TABLE>

	<TD><%=mobjValues.HiddenControl("tctIduraind", CStr(mclsProduct.nTypdurins))%></TD>
	<%

If Not mobjValues.ActionQuery Then
	If Not mblnGroups Then
		With Response
			.Write("<SCRIPT>")
			.Write("self.document.forms[0].cbovalGroup.disabled=true;")
			.Write("self.document.btncbovalGroup.disabled=true;")
			.Write("</script>")
		End With
	End If
	
	If Not mblnSituation Then
		With Response
			.Write("<SCRIPT>")
			.Write("self.document.forms[0].cbovalSituation.disabled=true;")
			.Write("self.document.btncbovalSituation.disabled=true;")
			.Write("</script>")
		End With
	End If
	
	If nTypdurins = 1 Then
		Response.Write("<SCRIPT>DisabledFields(""sId2"")</SCRIPT>")
	ElseIf nTypdurins = 2 Then 
		Response.Write("<SCRIPT>DisabledFields(""sId3"")</SCRIPT>")
	ElseIf nTypdurins = 3 Then 
		Response.Write("<SCRIPT>DisabledFields(""sId4"")</SCRIPT>")
	ElseIf nTypdurins = 4 Then 
		Response.Write("<SCRIPT>DisabledFields(""sId5"")</SCRIPT>")
	ElseIf nTypdurins = 5 Then 
		Response.Write("<SCRIPT>DisabledFields(""sId6"")</SCRIPT>")
	End If
	
End If

Response.Write(mobjValues.HiddenControl("tcnModulec", CStr(mclsProduct.nModules)))
Response.Write(mobjValues.HiddenControl("hddnRatepayf", CStr(mclsProduct.nRatepayf)))
Response.Write("<SCRIPT>insInitialFieldVisibility()</SCRIPT>")

mobjMenu = Nothing
mclsProduct = Nothing
mclsGroups = Nothing
mclsSituation = Nothing
mclsCertificat = Nothing
mclsProduct_li = Nothing
mclsLife = Nothing
Response.Write(mobjValues.BeginPageButton)
mobjValues = Nothing
%>

</FORM>
</BODY>
</HTML>





