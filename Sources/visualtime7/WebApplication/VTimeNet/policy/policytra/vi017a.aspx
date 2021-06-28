<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">

    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues

    Dim mColBatch As eBatch.tmp_switchs
    Dim mclsBatch As eBatch.tmp_switch
    

</script>

<%  Response.Expires = -1

'**- The object to handling the general function to load values is defined
'- Objeto para el manejo de las funciones generales de carga de valores
    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility
    mobjValues.sCodisplPage = "VI017"

'**- The object to handling the generic routines is defined
'- Objeto para el manejo de las rutinas genéricas
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

'**- The variable mobjGrid to handling the GRID of the window is defined
'- Se define la variable mobjGrid para el manejo del Grid de la ventana

'**- The variables to loads valores are defined
'- Se definen las variables para la carga de los valores
    Dim mColBatch = New eBatch.tmp_switchs
    Dim mclsBatch = New eBatch.tmp_switch
    Dim lnameControl As Object

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable Para Control de Versiones de Source Safe
    document.VssVersion="$$Revision: 2 $|$$Date: 18-10-13 13:14 $|$$Author: Mgonzalez $"

//% CloseWindow: Controla el cierre de la ventana
//-------------------------------------------------------------------------------------------
function InsAccept(bCancel){
//-------------------------------------------------------------------------------------------
    if (bCancel){
        self.window.close();
    }
    else{
        var arrayId = new Array();
        var arrayPercent = new Array();
        var nCount = 0;
        var nTotalPercent = 0;
        var nPercent = 0;
        with (self.document.forms[0]) {
            if (typeof(hddId.length) == 'undefined'){
                var tcnPercent = eval('tcnPercent2' + hddId.value);
                var hddPercent = eval('hddtcnPercent2' + hddId.value);
	            if (tcnPercent.value != hddPercent.value){
                    arrayId[nCount] = hddId.value;
                    arrayPercent[nCount] = tcnPercent.value;
                    hddPercent.value = tcnPercent.value;
	            }
	            if (tcnPercent.value != ''){
                    //Se acumulan los porcentajes
                    nPercent = parseFloat(tcnPercent.value.replace(',', '.'));
                    nTotalPercent = nTotalPercent + nPercent;
	            }
            }
            else{
	            for(var lintIndex=0; lintIndex<hddId.length;lintIndex++){
	                var tcnPercent = eval('tcnPercent2' + hddId[lintIndex].value);
                    var hddPercent = eval('hddtcnPercent2' + hddId[lintIndex].value);
	                if (tcnPercent.value != hddPercent.value){
                        arrayId[nCount] = hddId[lintIndex].value;
                        arrayPercent[nCount] = tcnPercent.value;
                        hddPercent.value = tcnPercent.value;
                        nCount++;
	                }
	                if (tcnPercent.value != ''){
                        //Se acumulan los porcentajes
                        nPercent = parseFloat(tcnPercent.value.replace(',', '.'));
                        nTotalPercent = nTotalPercent + nPercent;
	                }
	            }
            }
            if (nTotalPercent == 100){
                if (arrayId.length > 0){
                    var sPage = "/VTimeNet/Policy/Policytra/ShowDefValues.aspx?Field=Switch_UpdPercent&nAction=2&nId_orig=" + <%=Request.QueryString("nId_orig")%> + "&nType=2&nId=" + arrayId.join(';') + "&nPercent=" + arrayPercent.join(';');
                    ShowPopUp(sPage, "Switch_UpdPercent", 1, 1,"no","no",2000,2000);
                }
                else{
                    self.window.close();
                }
            }
            else{
                alert('El porcentaje total debe ser igual a 100');	
            }
        }
    }
}

</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%Response.Write(mobjValues.StyleSheet() & vbCrLf)%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="VI017" ACTION="valPolicyTra.aspx?x=1">
    <TABLE WIDTH="100%" COLS=3 CLASS=grddata>
        <BR></BR>
        <TR>
            <TH COLSPAN = 3 ALIGN = CENTER><LABEL ID=0><%=GetLocalResourceObject("txtBuyCaption")%></LABEL></TH>
        </TR>
        <TR>
            <TH ALIGN = CENTER><LABEL ID=0><%=GetLocalResourceObject("tctFunds2Caption")%></LABEL></TH>
            <TH ALIGN = CENTER><LABEL ID=0><%=GetLocalResourceObject("tctBenef2Caption")%></LABEL></TH>
            <TH ALIGN = CENTER><LABEL ID=0><%=GetLocalResourceObject("tcnPercent2Caption")%></LABEL></TH>    
        </TR>
        <% 
            If mColBatch.Find_1(Session("sKey"), _
                         mobjValues.StringToType(Request.QueryString("nFund_sell"), Values.eTypeData.etdDouble), _
                         mobjValues.StringToType(Request.QueryString("nTyp_profitworker_sell"), Values.eTypeData.etdDouble), _
                         mobjValues.StringToType(Request.QueryString("nOrigin"), Values.eTypeData.etdLong)) Then
                For Each mclsBatch In mColBatch
		%>
        
        <TR>
			<TD>
				<% 	
				    Response.Write(mobjValues.HiddenControl("hddId", mclsBatch.nId))
				    lnameControl = "tctFunds2" & mclsBatch.nId
				    Response.Write(mobjValues.TextControl(lnameControl, 20, mclsBatch.sFund_buy, , , True))
                %>
			</TD>
			<TD>
				<% lnameControl = "tctBenef2" & mclsBatch.nId  
				    Response.Write(mobjValues.TextControl(lnameControl, 20, mclsBatch.sTyp_profitworker_buy, , , True))
				%>
			</TD>
			<TD>
				<% lnameControl = "tcnPercent2" & mclsBatch.nId  
				    Response.Write(mobjValues.NumericControl(lnameControl, 5, mclsBatch.nPercent_buy, , , , 2, , , , , , , , False))
				    Response.Write(mobjValues.HiddenControl("hdd" & lnameControl, mobjValues.TypeToString(mclsBatch.nPercent_buy, Values.eTypeData.etdDouble, False, 2)))
				%>
			</TD>
        </TR>
	       
        <%
               Next
		   End if	
    		%>
    </TABLE>
    <TABLE WIDTH=100% border=0>
        <TD><TD>
        <TD ALIGN='RIGHT' width="10%"><%= mobjValues.AnimatedButtonControl("btn_Cancel", "/VtimeNet/images/btnAcceptOff.png", "Aceptar", , "InsAccept(false);")%></TD>
        <TD ALIGN='RIGHT' width="10%"><%= mobjValues.AnimatedButtonControl("btn_Cancel", "/VtimeNet/images/btnCancelOff.png", "Cerrar", , "InsAccept(true);")%></TD>
    </TABLE>    

</FORM>
</BODY>
</HTML>
