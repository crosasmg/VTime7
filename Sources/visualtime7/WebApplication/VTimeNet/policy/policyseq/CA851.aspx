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

Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de las funciones generales de carga de valores 
Dim mobjValues As eFunctions.Values
Dim mobjCertificat As ePolicy.Certificat
'- String que envia a control de cliente llave de busca de la poliza 
Dim lstrQueryString As Object


</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA851")

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

Call mobjProduct_li.FindProduct_li(Session("nBranch"), Session("nProduct"), Today)
Call mobjCertificat.insPreCA004(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nTransaction"), Session("sSche_code"))

%>   



	
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
    
<script LANGUAGE="javascript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 20/04/04 18:09 $|$$Author: Nvaplat15 $"

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
// y la clase de producto es "VidActiva"		
	switch (lobjPayFreq.value) {
		case "8":
		    self.document.forms[0].cbeQuota.value = lintcbeQuota
			self.document.forms[0].cbeQuota.disabled = false
			break
		default:
		    self.document.forms[0].cbeQuota.value = 0
			self.document.forms[0].cbeQuota.disabled = true
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
function InsChangeWayPay(nCertif, nDirTyp , nDuration) {
//-------------------------------------------------------------------------------------------
	
	var lstrquery='';
	with (self.document.forms[0]){
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
			elements["valOrigin"].Parameters.Param3.sValue = cbeWayPay.value;
//+ Si Vía de pago es 1 ó 2 : 
			if (cbeWayPay.value == '1' ||
			    cbeWayPay.value == '2'){
                //lstrquery = "sClient=" + tctClient.value + "&dEffecdate=" + <%=Session("dEffecdate")%> + "&nWayPay=" + cbeWayPay.value + "&optDirTyp=" + nDirTyp + "&hhDirTyp=" + hhDirTyp.value;
				//if (nDuration == '1')
				//    lstrquery += "&nDuration=" + tcnDuration.value + "&dStartDate=" + tcdStartDate.value;
	
				//insDefValues('WayPay',lstrquery , '/VTimeNet/Policy/PolicySeq');

				optDirTyp[0].disabled = false;
				optDirTyp[1].disabled = false;
				optDirTyp[0].checked = true;
			}
			else{
				optDirTyp[0].checked = false;
				optDirTyp[1].checked = false;
				optDirTyp[0].disabled = true;
				optDirTyp[1].disabled = true;				
			}
			if (cbeWayPay.value == '3'){
				if (nNoChange == '2') {
                    if (self.document.forms[0].hhprodclas.value == 4){
						self.document.forms[0].valOrigin.value = '2';
						self.document.forms[0].valOrigin.disabled = true;
						self.document.forms[0].btnvalOrigin.disabled = true;
						$(self.document.forms[0].valOrigin).change();
				    }
				}    
			}
			else {
				if (nNoChange == '2') {
    				if (self.document.forms[0].hhprodclas.value == 4){
    					self.document.forms[0].valOrigin.value = '';
	    				UpdateDiv('valOriginDesc',' ');
		    			self.document.forms[0].valOrigin.disabled = false;
			    		self.document.forms[0].btnvalOrigin.disabled = false;
			    	}
			    }	
			}	
			if (nNoChange == '2')
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
		else{
			self.document.forms[0].cbeWayPay.disabled = true;
		}		
	}
    nNoChange = '2'
    

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
//function InsChangeAgreement() {
//-------------------------------------------------------------------------------------------	
//	with (self.document.forms[0]){
//		if (valAgreement.value != '')
//			ShowPopUp("/VTimeNet/Policy/PolicySeq/ShowDefValues.aspx?Field=Agreement&nCod_Agree=" + valAgreement.value, "ShowDefValues", 1, 1,"no","no",2000,2000);
//	}
//}

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
<%If (Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyIssue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifIssue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngRecuperation Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotation Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifQuotation Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyProposal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifProposal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyReissue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifReissue) And CStr(Session("sBrancht")) <> "1" And ((Session("nCertif") > 0 And mobjCertificat.sColtimre <> "1") Or Session("nCertif") = 0) Then
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
			tcdExpirDate.disabled = false
			insDefValues('SumDate', "nDuration=" + Field.value + "&dStartDate=" + tcdStartDate.value + "&sColtimre=" + lstrColtimre + "&sColinvot=" + lstrColinvot, '/VTimeNet/Policy/PolicySeq');
		}
	}
}   
</script>
</script>
<html>
<head>
    <%Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
mobjMenu = Nothing
%>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <%=mobjValues.StyleSheet()%>
</head>
<body>
<form METHOD="post" ID="FORM" NAME="frmCA851" ACTION="valPolicySeq.aspx?nMainAction=301&amp;nHolder=1">
    <%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))%>
    <table WIDTH="100%">
		<tr>
		    <td COLSPAN="2" CLASS="HighLighted"><label ID="0"><a NAME="Datos de la cobranza"></a></label></td> 
			<td>&nbsp;</td>
			<td COLSPAN="2" CLASS="HighLighted"><label ID="0"><a NAME="Vía de cobro"><%= GetLocalResourceObject("AnchorVía de cobroCaption") %></a></label></td> 
		</tr>
		<tr>
			<td COLSPAN="2" CLASS=""></td>	    
			<td></td>
			<td COLSPAN="2" CLASS="Horline"></td>	    
		</tr>        	
		<tr>
			<td><label ID="12971"><%= GetLocalResourceObject("cbeWayPayCaption") %></label></td>
			<td><%With mobjValues
	.BlankPosition = True
	.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	If mobjProduct_li.nProdClas = 4 Then
		Response.Write(mobjValues.PossiblesValues("cbeWayPay", "tabway_pay_prod", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nWay_Pay), True,  ,  ,  ,  , "InsChangeWayPay(" & Session("nCertif") & ",0,0);", mobjCertificat.nWay_Pay = 7,  , GetLocalResourceObject("cbeWayPayToolTip"),  , 17))
	Else
		Response.Write(mobjValues.PossiblesValues("cbeWayPay", "tabway_pay_prod", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nWay_Pay), True,  ,  ,  ,  , "InsChangeWayPay(" & Session("nCertif") & ",0,0);", mobjCertificat.StateVarCA004(23),  , GetLocalResourceObject("cbeWayPayToolTip"),  , 17))
	End If
End With
%>
			</td>
			<td>&nbsp;</td><%'mobjCertificat.StateVarCA004(20) , mobjCertificat.StateVarCA004(20)%>
			<td COLSPAN="2"><%=mobjValues.OptionControl(0, "optDirTyp", GetLocalResourceObject("optDirTyp_CStr1Caption"), mobjCertificat.sDirind, CStr(1),  , False, 20, GetLocalResourceObject("optDirTyp_CStr1ToolTip"))%> </td>
		</tr>
		<TR>
		    <TD><LABEL ID="10295"><%= GetLocalResourceObject("valCollectorCaption") %></LABEL></TD>
			<TD> <%=mobjValues.PossiblesValues("valCollector", "tabCollector_Cliname", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjCertificat.nCollector),  ,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("valCollectorToolTip"))%></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optDirTyp", GetLocalResourceObject("optDirTyp_CStr2Caption"), CStr(CShort(mobjCertificat.sDirind) - 1), CStr(2),  , False,  , GetLocalResourceObject("optDirTyp_CStr2ToolTip"))%>  </TD>
        </TR>
		<tr>
			<td><label ID="0"><%= GetLocalResourceObject("valOriginCaption") %></label></td>
			<td><%mobjValues.BlankPosition = True
'+ Líneas comentadas por cambio APV2. Los valores de las cuentas origen se leen de la tabla "TAB_ORD_ORIGIN" - ACM - 26/09/2003
mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nCollecDocTyp", mobjCertificat.nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
If mobjProduct_li.nProdClas = 4 Then
	Response.Write(mobjValues.PossiblesValues("valOrigin", "TAB_ORIGIN", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjCertificat.nOrigin), True,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("valOriginToolTip")))
Else
	' mobjCertificat.StateVarCA004(37)
	Response.Write(mobjValues.PossiblesValues("valOrigin", "TAB_ORIGIN", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjCertificat.nOrigin), True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valOriginToolTip")))
End If
%>
			</td>
		</tr>
		<%If mobjProduct_li.nProdClas = 4 Then%>
		<tr>
			<td><label ID="0"><%= GetLocalResourceObject("tcnAFPCommiCaption") %></label></td>
			<%	'mobjCertificat.StateVarCA004(38)%>
			<td><%=mobjValues.NumericControl("tcnAFPCommi", 18, CStr(mobjCertificat.nAFP_Commiss),  , GetLocalResourceObject("tcnAFPCommiToolTip"), True, 6,  ,  ,  ,  , False)%></td>
			<td>&nbsp;</td>
			<td><label ID="0"><%= GetLocalResourceObject("cbeCurrencyCaption") %></label></td>
			<td><%	'mobjCertificat.StateVarCA004(39)
	mobjValues.BlankPosition = True
	Response.Write(mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nAFP_Comm_Curr),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeCurrencyToolTip")))
	%>
			</td>
		</tr>
		<%End If%>
		<tr>
			<td><label ID="12972"><%= GetLocalResourceObject("tcnBillDayCaption") %></label></td>
			<%'mobjCertificat.StateVarCA004(22)%>
			<td><%=mobjValues.NumericControl("tcnBillDay", 2, CStr(mobjCertificat.nBill_day),  , GetLocalResourceObject("tcnBillDayToolTip"), False,  ,  ,  ,  ,  , False, 18)%></td>
			<td>&nbsp;</td>
		</tr>
        
    </table> 
	<%'	Response.Write mobjValues.BeginPageButton 
Response.Write(mobjValues.HiddenControl("hhDirTyp", vbNullString))
Response.Write(mobjValues.HiddenControl("hhprodclas", CStr(mobjProduct_li.nProdClas)))
%>
</form>
</body>
</html>
<%
If Not mobjValues.ActionQuery Then
	'Response.Write "<NOTSCRIPT>var nNoChange = '1'; InsChangeWayPay(" & Session("nCertif") & " , " & mobjCertificat.sDirind & " , 1 " & "); </SCRIPT>"
	Response.Write("<SCRIPT>var nNoChange = '1'; </SCRIPT>")
End If

mobjValues = Nothing
mobjMenu = Nothing
mobjCertificat = Nothing
mobjProduct_li = Nothing
%> 
	
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
Call mobjNetFrameWork.FinishPage("CA004")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




