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


    '% insPreCR006H: Realiza la lectura para la carga de los datos de la forma
    '------------------------------------------------------------------------------------------------
    Private Sub insPreCR006H()
        '------------------------------------------------------------------------------------------------
        Call mclsCuentecn.Find(CInt(Request.QueryString.Item("nNumber")), CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nType")), CInt(Request.QueryString.Item("nCompany")), CInt(Request.QueryString.Item("nPerType")), CInt(Request.QueryString.Item("nPerNum")), Request.QueryString.Item("sBussiType"), CInt(Request.QueryString.Item("nCurrency")), CInt(Request.QueryString.Item("nIdConsec")))

        Call mclsCuentecn.DefaultValues("CR006H", CInt(Request.QueryString.Item("nPerType")), CInt(Request.QueryString.Item("nPerNum")), CInt(Request.QueryString.Item("nMainAction")))
    End Sub

</script>
<%Response.Expires = -1
    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues
    mclsCuentecn = New eCoReinsuran.Cuentecn

    mobjValues.ActionQuery = Session("bQuery")

    Call insPreCR006H()

    mobjValues.sCodisplPage = "cr006h"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//% DisabledFields: Desabilita los campos cuando la cuenta técnica tiene orden de pago. 
//--------------------------------------------------------------------------------------------
function DisabledFields(Reinsurance){
//--------------------------------------------------------------------------------------------
	self.document.forms[0].tcnImpuesto.disabled=true;
	self.document.forms[0].tcnClaimCed.disabled=true;
	if(Reinsurance!=3){
		self.document.forms[0].tcnRetResPre.disabled=true;
		self.document.forms[0].tcnResSinPen.disabled=true;
		self.document.forms[0].tcnRCarPrem.disabled=true;
		self.document.forms[0].tcnRCarSin.disabled=true;
		self.document.forms[0].tcnGastoReas.disabled=true;
		self.document.forms[0].tcnCommission.disabled=true;
	}
}

//% AmountResum: Actualiza el total del Asegurador y el saldo.
//--------------------------------------------------------------------------------------------
function AmountResum(Reinsurance){
//--------------------------------------------------------------------------------------------
    //PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - INICIO
	with(self.document.forms[0]){
		if(typeof(Reinsurance)=='undefined'){
			//Total Asegurador

			tcnTotInsu_tmp = insConvertNumber(tcnRetResPre.value)  + insConvertNumber(tcnResSinPen.value)  + 
							 insConvertNumber(tcnRCarPrem.value)  + insConvertNumber(tcnRCarSin.value)    + 
							 insConvertNumber(tcnGastoReas.value)  + insConvertNumber(tcnCommission.value) + 
							 insConvertNumber(tcnClaimCed.value)  + insConvertNumber(tcnImpuesto.value);
			tcnTotInsu.value = VTFormat(tcnTotInsu_tmp, "", "", "", 6, true);
			
			//Saldo
			tcnBalance_tmp = insConvertNumber(tcnTotInsu.value) - insConvertNumber(tcnTotRei.value);

			if (tcnBalance_tmp < 0){
				tcnBalance.value = VTFormat(tcnBalance_tmp * -1, "", "", "", 6, true);
			}else{
				tcnBalance.value = VTFormat(tcnBalance_tmp, "", "", "", 6, true);
			}
			$(tcnBalance).change();			
			
//Actualiza cuando el reaseguro es "no proporcional"
		}else{
			//Total Asegurador
			tcnTotInsu_tmp = insConvertNumber(tcnClaimCed.value) + insConvertNumber(tcnImpuesto.value);
			tcnTotInsu.value = VTFormat(tcnTotInsu_tmp, "", "", "", 6, true);
			
			//Saldo
			tcnBalance_tmp = insConvertNumber(tcnTotInsu.value)- insConvertNumber(tcnTotRei.value);		

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
            .Write(mobjMenu.setZone(2, "CR006H", "CR006H.aspx"))
            .Write(mobjValues.ShowWindowsName("CR006H"))
            .Write("<BR><BR>")
        End With
        mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCR006H" ACTION="valCoReinsuranTra.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <TABLE WIDTH="100%">            
		<%If Request.QueryString.Item("nReinsurance") <> "3" Then%>
			<TR>
			    <TD><LABEL><%= GetLocalResourceObject("tcnRetResPreCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnRetResPre", 18, CStr(mclsCuentecn.nRet_respre),  , GetLocalResourceObject("tcnRetResPreToolTip"), True, 6,  ,  ,  , "AmountResum();", mclsCuentecn.blnRetResPre, 1, True, True)%></TD>
			    <TD><LABEL><%= GetLocalResourceObject("tcnResSinPenCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnResSinPen", 18, CStr(mclsCuentecn.nRes_sinpen),  , GetLocalResourceObject("tcnResSinPenToolTip"), True, 6,  ,  ,  , "AmountResum();", mclsCuentecn.blnResSinPen, 2, True, True)%></TD>
			</TR>
			<TR>
			    <TD><LABEL><%= GetLocalResourceObject("tcnRCarPremCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnRCarPrem", 18, CStr(mclsCuentecn.nR_car_prem),  , GetLocalResourceObject("tcnRCarPremToolTip"), True, 6,  ,  ,  , "AmountResum();", mclsCuentecn.blnRCarPrem, 3, True, True)%></TD>
			    <TD><LABEL><%= GetLocalResourceObject("tcnRCarSinCaption") %></LABEL></TD>
			    <TD><%=mobjValues.NumericControl("tcnRCarSin", 18, CStr(mclsCuentecn.nR_car_sin),  , GetLocalResourceObject("tcnRCarSinToolTip"), True, 6,  ,  ,  , "AmountResum();", mclsCuentecn.blnRCarSin, 4, True, True)%></TD>
			</TR>
			<TR>
			    <TD><LABEL><%= GetLocalResourceObject("tcnGastoReasCaption") %></LABEL></TD>
			    <td><%=mobjValues.NumericControl("tcnGastoReas", 18, CStr(mclsCuentecn.nGasto_reas),  , GetLocalResourceObject("tcnGastoReasToolTip"), True, 6,  ,  ,  , "AmountResum();", mclsCuentecn.blnGastoReas, 5, True, True)%></TD>
			    <TD><LABEL><%= GetLocalResourceObject("tcnCommissionCaption") %></LABEL></TD>
			    <td><%=mobjValues.NumericControl("tcnCommission", 18, CStr(mclsCuentecn.nComision),  , GetLocalResourceObject("tcnCommissionToolTip"), True, 6,  ,  ,  , "AmountResum();", mclsCuentecn.blnCommission, 6, True, True)%></TD>
			</TR>
			<TR>
			    <TD><LABEL><%= GetLocalResourceObject("tcnClaimCedCaption") %></LABEL></TD>
			    <td><%=mobjValues.NumericControl("tcnClaimCed", 18, CStr(mclsCuentecn.nClaim_ced),  , GetLocalResourceObject("tcnClaimCedToolTip"), True, 6,  ,  ,  , "AmountResum();", mclsCuentecn.blnClaimCed, 7, True, True)%></TD>
			    <TD><LABEL><%= GetLocalResourceObject("tcnImpuestoCaption") %></LABEL></TD>
			    <td><%=mobjValues.NumericControl("tcnImpuesto", 18, CStr(mclsCuentecn.nImpuesto),  , GetLocalResourceObject("tcnImpuestoToolTip"), True, 6,  ,  ,  , "AmountResum();", mclsCuentecn.blnImpuesto, 8, True, True)%></TD>
			</TR>
		<%Else%>
			<TR>
			    <TD><LABEL><%= GetLocalResourceObject("tcnClaimCedCaption") %></LABEL></TD>
			    <td><%=mobjValues.NumericControl("tcnClaimCed", 18, CStr(mclsCuentecn.nClaim_ced),  , GetLocalResourceObject("tcnClaimCedToolTip"), True, 6,  ,  ,  , "AmountResum(" & Request.QueryString.Item("nReinsurance") & ");", False, 1, True, True)%></TD>
			    <TD><LABEL><%= GetLocalResourceObject("tcnImpuestoCaption") %></LABEL></TD>
			    <td><%=mobjValues.NumericControl("tcnImpuesto", 18, CStr(mclsCuentecn.nImpuesto),  , GetLocalResourceObject("tcnImpuestoToolTip"), True, 6,  ,  ,  , "AmountResum(" & Request.QueryString.Item("nReinsurance") & ");", False, 2, True, True)%></TD>
			</TR>		
		<%End If%>
		<TR>	
			<TD>&nbsp;</TD>  
		</TR>    	                                    
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnTotInsuCaption") %></LABEL></TD>
		    <TD><%=mobjValues.NumericControl("tcnTotInsu", 18, CStr(mclsCuentecn.nSal_f_comp),  , GetLocalResourceObject("tcnTotInsuToolTip"), True, 6,  ,  ,  ,  , True,  , True, True)%></TD>
		    <TD><LABEL><%= GetLocalResourceObject("tcnTotReiCaption") %></LABEL></TD>
		    <TD><%=mobjValues.NumericControl("tcnTotRei", 18, CStr(mclsCuentecn.nSal_f_rein),  , GetLocalResourceObject("tcnTotReiToolTip"), True, 6,  ,  ,  ,  , True,  , True, True)%></TD>
		</TR>
		<TR>
		    <TD><LABEL><%= GetLocalResourceObject("tcnBalanceCaption") %></LABEL></TD>
		    <TD><%=mobjValues.NumericControl("tcnBalance", 18, CStr(mclsCuentecn.nSal_f_comp - mclsCuentecn.nSal_f_rein),  , GetLocalResourceObject("tcnBalanceToolTip"), True, 6,  ,  ,  ,  , True,  , True, True)%></TD>
		</TR>
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




