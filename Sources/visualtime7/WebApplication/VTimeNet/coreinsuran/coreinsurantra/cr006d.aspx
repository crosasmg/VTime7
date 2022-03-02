<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo de las rutinas genéricas
    Dim mobjMenu As eFunctions.Menues

    '- Se define la variable modular utilizada para la carga y actualización de datos de la forma    
    Dim mclsCuentecn As eCoReinsuran.Cuentecn


    '% insPreCR006D: Realiza la lectura para la carga de los datos de la forma
    '------------------------------------------------------------------------------------------------
    Private Sub insPreCR006D()
        '------------------------------------------------------------------------------------------------
        Call mclsCuentecn.Find(CInt(Request.QueryString.Item("nNumber")), CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nType")), CInt(Request.QueryString.Item("nCompany")), CInt(Request.QueryString.Item("nPerType")), CInt(Request.QueryString.Item("nPerNum")), Request.QueryString.Item("sBussiType"), CInt(Request.QueryString.Item("nCurrency")), CInt(Request.QueryString.Item("nIdConsec")))

        Call mclsCuentecn.DefaultValues("CR006D", CInt(Request.QueryString.Item("nPerType")), CInt(Request.QueryString.Item("nPerNum")), CInt(Request.QueryString.Item("nMainAction")))
    End Sub

</script>
<%Response.Expires = -1
    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues
    mclsCuentecn = New eCoReinsuran.Cuentecn

    mobjValues.ActionQuery = Session("bQuery")
    Call insPreCR006D()

    mobjValues.sCodisplPage = "cr006d"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//% DisabledFields: Desabilita los campos cuando la cuenta técnica si tiene orden de pago. 
//--------------------------------------------------------------------------------------------
function DisabledFields(Reinsurance){
//--------------------------------------------------------------------------------------------
	self.document.forms[0].tcnPremCed.disabled=true;
	if (Reinsurance!=3){
		self.document.forms[0].tcnPartBenef.disabled=true;
		self.document.forms[0].tcnDevResPre.disabled=true;
		self.document.forms[0].tcnDevResCla.disabled=true;
		self.document.forms[0].tcnInterPrem.disabled=true;
		self.document.forms[0].tcnInterSin.disabled=true;
		self.document.forms[0].tcnECarPrem.disabled=true;
		self.document.forms[0].tcnECarSin.disabled=true;
	}
}
//% AmountResum: Actualiza el total del asegurador y el saldo.
//--------------------------------------------------------------------------------------------
function AmountResum(Reinsurance){
//--------------------------------------------------------------------------------------------
    //PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - INICIO
	with(self.document.forms[0]){
		if(typeof(Reinsurance)=='undefined'){
			//Total reasegurador
			tcnTotRei_tmp = insConvertNumber(tcnPremCed.value) + insConvertNumber(tcnPartBenef.value) + 
							insConvertNumber(tcnDevResPre.value) + insConvertNumber(tcnDevResCla.value) + 
							insConvertNumber(tcnInterPrem.value) + insConvertNumber(tcnInterSin.value) + 
							insConvertNumber(tcnECarPrem.value) + insConvertNumber(tcnECarSin.value);
			
			tcnTotRei.value = VTFormat(tcnTotRei_tmp, "", "", "", 6, true);
			$(tcnTotRei).change();
			
			//Saldo
			tcnBalance_tmp = insConvertNumber(tcnTotRei.value) - insConvertNumber(tcnTotInsu.value);

			if (tcnBalance_tmp < 0){
				tcnBalance.value = VTFormat(tcnBalance_tmp * -1, "", "", "", 6, true);
			}else{
				tcnBalance.value = VTFormat(tcnBalance_tmp, "", "", "", 6, true);
			}
			$(tcnBalance).change();

//Actualiza cuando el reaseguro es "no proporcional"
		}else{
			//Total reasegurador
			tcnTotRei_tmp = insConvertNumber(tcnPremCed.value);
			tcnTotRei.value = VTFormat(tcnTotRei_tmp, "", "", "", 6, true);

			//Saldo
			tcnBalance_tmp = insConvertNumber(tcnTotRei.value) - insConvertNumber(tcnTotInsu.value);

			if (tcnBalance_tmp < 0){
				tcnBalance.value = VTFormat(tcnBalance_tmp * -1, "", "", "", 6, true);
			}else{
				tcnBalance.value = VTFormat(tcnBalance_tmp, "", "", "", 6, true);
			}
			$(tcnBalance).change();
		}
	}
	//PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - FIN
}

//PutZero: Función que no permite que algún campo quede sin valor(en blanco).
//--------------------------------------------------------------------------------------------
function PutZero(Field){
//--------------------------------------------------------------------------------------------
	if(Field.value=='')
		Field.value=0
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <%With Response
            .Write(mobjValues.StyleSheet() & vbCrLf)
            .Write(mobjMenu.setZone(2, "CR006D", "CR006D.aspx"))
            .Write(mobjValues.ShowWindowsName("CR006D"))
            .Write("<BR><BR>")
        End With
        mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCR006D" ACTION="valCoReinsuranTra.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <TABLE WIDTH="100%">
		
		<%If Request.QueryString.Item("nReinsurance") = "3" Then%>
			<TR>
				<TD WIDTH="30%">&nbsp;</TD>
			    <TD WIDTH="20%"><LABEL><%= GetLocalResourceObject("tcnPremCedCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnPremCed", 18, CStr(mclsCuentecn.nPrem_ced),  , GetLocalResourceObject("tcnPremCedToolTip"), True, 6,  ,  ,  , "PutZero(this);AmountResum(" & Request.QueryString.Item("nReinsurance") & ");", False, 1)%></TD>
			</TR>
		<%Else%>
			<TR>
				<TD><LABEL><%= GetLocalResourceObject("tcnPremCedCaption") %></LABEL></TD>
				<TD><%=mobjValues.NumericControl("tcnPremCed", 18, CStr(mclsCuentecn.nPrem_ced),  , GetLocalResourceObject("tcnPremCedToolTip"), True, 6,  ,  ,  , "PutZero(this);AmountResum();", mclsCuentecn.blnPremCed, 1)%></TD>
				<TD><LABEL><%= GetLocalResourceObject("tcnPartBenefCaption") %></LABEL></TD>
				<TD><%=mobjValues.NumericControl("tcnPartBenef", 18, CStr(mclsCuentecn.nPart_benef),  , GetLocalResourceObject("tcnPartBenefToolTip"), True, 6,  ,  ,  , "PutZero(this);AmountResum();", mclsCuentecn.blnPartBenef, 2)%></TD>
			</TR>
			<TR>
			    <TD><LABEL><%= GetLocalResourceObject("tcnDevResPreCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnDevResPre", 18, CStr(mclsCuentecn.nDev_respre),  , GetLocalResourceObject("tcnDevResPreToolTip"), True, 6,  ,  ,  , "PutZero(this);AmountResum();", mclsCuentecn.blnDevResPre, 3)%></TD>
			    <TD><LABEL><%= GetLocalResourceObject("tcnDevResClaCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnDevResCla", 18, CStr(mclsCuentecn.nDev_rescla),  , GetLocalResourceObject("tcnDevResClaToolTip"), True, 6,  ,  ,  , "PutZero(this);AmountResum();", mclsCuentecn.blnDevResCla, 4)%></TD>
			</TR>
			<TR>
			    <TD><LABEL><%= GetLocalResourceObject("tcnInterPremCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnInterPrem", 18, CStr(mclsCuentecn.nInter_prem),  , GetLocalResourceObject("tcnInterPremToolTip"), True, 6,  ,  ,  , "PutZero(this);AmountResum();", mclsCuentecn.blnInterPrem, 5)%></TD>
			    <TD><LABEL><%= GetLocalResourceObject("tcnInterSinCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnInterSin", 18, CStr(mclsCuentecn.nInter_sin),  , GetLocalResourceObject("tcnInterSinToolTip"), True, 6,  ,  ,  , "PutZero(this);AmountResum();", mclsCuentecn.blnInterSin, 6)%></TD>
			</TR>
			<TR>
			    <TD><LABEL><%= GetLocalResourceObject("tcnECarPremCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnECarPrem", 18, CStr(mclsCuentecn.nE_car_prem),  , GetLocalResourceObject("tcnECarPremToolTip"), True, 6,  ,  ,  , "PutZero(this);AmountResum();", mclsCuentecn.blnECarPrem, 7)%></TD>
			    <TD><LABEL><%= GetLocalResourceObject("tcnECarSinCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnECarSin", 18, CStr(mclsCuentecn.nE_car_sin),  , GetLocalResourceObject("tcnECarSinToolTip"), True, 6,  ,  ,  , "PutZero(this);AmountResum();", mclsCuentecn.blnECarSin, 8)%></TD>
			</TR>
			<TR>	
				<TD>&nbsp;</TD>  
			</TR>   	                            
			<TR>
			    <TD><LABEL><%= GetLocalResourceObject("tcnTotReiCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnTotRei", 18, CStr(mclsCuentecn.nSal_f_rein),  , GetLocalResourceObject("tcnTotReiToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
			    <TD><LABEL><%= GetLocalResourceObject("tcnTotInsuCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnTotInsu", 18, CStr(mclsCuentecn.nSal_f_comp),  , GetLocalResourceObject("tcnTotInsuToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
			</TR>
			<TR>
			    <TD><LABEL><%= GetLocalResourceObject("tcnBalanceCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnBalance", 18, CStr(mclsCuentecn.nSal_f_rein - mclsCuentecn.nSal_f_comp),  , GetLocalResourceObject("tcnBalanceToolTip"), True, 6,  ,  ,  ,  , True)%></TD>        
			</TR>   
			<TR>
			    <TD><LABEL><%= GetLocalResourceObject("tcnPayOrderCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnPayOrder", 10, CStr(mclsCuentecn.nRequestnu),  , GetLocalResourceObject("tcnPayOrderToolTip"), True,  , True)%></TD>
			    <TD><LABEL><%= GetLocalResourceObject("tcnDateOrderPayCaption") %></LABEL></TD>
			    <TD><%=mobjValues.TextControl("tcnDateOrderPay", 10, mobjValues.DateToString(mclsCuentecn.dDatePay),  , GetLocalResourceObject("tcnDateOrderPayToolTip"), True)%></TD>        
			</TR>    
		<%End If%>
		<TD><%=mobjValues.HiddenControl("hddPayOrder", CStr(mclsCuentecn.nRequestnu))%></TD>
		<TD><%=mobjValues.HiddenControl("hddnIdConsec", CStr(mclsCuentecn.nIdConsec))%></TD>		
	</TABLE>	
</FORM>
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"     
</SCRIPT>
<%
    If mclsCuentecn.nRequestnu > 0 Then
        Response.Write("<SCRIPT>DisabledFields(" & Request.QueryString.Item("nReinsurance") & ");</SCRIPT>")
    End If
%>
</BODY>
</HTML>




