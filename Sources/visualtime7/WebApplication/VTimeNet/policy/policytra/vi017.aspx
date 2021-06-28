<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">

'**- The object to handling the general function to load values is defined
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'**- The object to handling the generic routines is defined
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

'**- The variable mobjGrid to handling the GRID of the window is defined
'- Se define la variable mobjGrid para el manejo del Grid de la ventana
    'Dim mobjGrid As eFunctions.Grid

'**- The variables to loads valores are defined
'- Se definen las variables para la carga de los valores
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

    'Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
    mColBatch = New eBatch.tmp_switchs
    mclsBatch = New eBatch.tmp_switch

    Dim lnameControl As String
    Dim lShowControl As Object
    Dim mintOrigin As Integer
    mintOrigin = mobjValues.StringToType(Request.QueryString("nOrigin"), Values.eTypeData.etdDouble)

    Dim bFound As Boolean
    bFound = False

    If mintOrigin = eRemoteDB.Constants.intNull Then
        'rea_count_surr_origins -- TAB_ORIGINPOL--
        Call mColBatch.Find_TabOriginPol("2", "", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
        If mColBatch.nCountOrigin = 1 Then
            For Each mclsBatch In mColBatch
                mintOrigin = mclsBatch.nOrigin
                bFound = True
            Next
        End If
    End If
    
    '**- The variable mobjGrid to handling the GRID of the window is defined
    '- Se define la variable mobjGrid para el manejo del Grid de la ventana

    '**- The variables to loads valores are defined
    '- Se definen las variables para la carga de los valores

%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable Para Control de Versiones de Source Safe
    document.VssVersion="$$Revision: 3 $|$$Date: 23-12-13 13:17 $|$$Author: Mgonzalez $"

//-------------------------------------------------------------------------------------------
function InsChangePercent(nId,nType,sField){
//-------------------------------------------------------------------------------------------
    var lstrParams;
	lstrParam = 'nAction=1&nId=' + nId + '&nType=' + nType + '&nPercent=' + sField.value
	if (sField.value > 100){
		alert('Porcentaje debe ser menor o igual que 100');	
		sField.value = "";
	} 
	else
	{
	    if ('<%=Request.QueryString("sChkByAccount")%>'!='1'){
	        insDefValues('Switch_UpdPercent', lstrParam,'/VtimeNet/Policy/policytra');
	    }
	} 
} 

//% ShowSell: muestra los fondos para la venta
//--------------------------------------------------------------------------------------------
function ShowSell(nFund_sell,nTyp_profitworker_sell,nId){
//--------------------------------------------------------------------------------------------
    ShowPopUp('VI017A.aspx?sCodispl=VI017A&nFund_sell=' + nFund_sell + '&nTyp_profitworker_sell=' + nTyp_profitworker_sell +
              '&nId_orig=' + nId + '&nOrigin=' + '<%=Request.QueryString("nOrigin")%>' + '&nMainAction=' + nMainAction ,'VI017A',500,500,'yes','no',300,280);
}

//% insReload: Se encarga de recargar la página al cambiar cualquier valor de los campos del encabezado del grid.
//---------------------------------------------------------------------------------------------------------------
function insReload(){
//---------------------------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
        var lstrQuery = "&sChkByAccount=" + '<%=Request.QueryString("sChkByAccount")%>' + "&nOrigin=" + cbeOrigin.value;
        UpdateDiv('lblWaitProcess','<MARQUEE>Procesando, por favor espere...</MARQUEE>','');
        top.fraFolder.document.location.href = top.fraFolder.document.location.href.replace(/&sChkByAccount=.*/,'') + lstrQuery;
    }
} 

//% InsAccept: Aceptar o cancelar los cambios
//---------------------------------------------------------------------------------------------------------------
function InsAccept(bCancel){
//---------------------------------------------------------------------------------------------------------------
    var sQueryString = '';
    var arrayId = new Array();
    var arrayPercent = new Array();
    var nCount = 0;
    with (self.document.forms[0]) {
        if (typeof(hddId)!='undefined'){
            if (typeof(hddId.length) == 'undefined'){
                var tcnPercent = eval('tcnPercent1' + hddId.value);
                var hddPercent = eval('hddtcnPercent1' + hddId.value);
                if (bCancel){
                    tcnPercent.value = hddPercent.value;
                }
                else{
	                if (tcnPercent.value != hddPercent.value){
                        arrayId[nCount] = hddId.value;
                        arrayPercent[nCount] = tcnPercent.value;
                        hddPercent.value = tcnPercent.value;
	                }
	            }
            }
            else{
	            for(var lintIndex=0; lintIndex<hddId.length;lintIndex++){
	                var tcnPercent = eval('tcnPercent1' + hddId[lintIndex].value);
                    var hddPercent = eval('hddtcnPercent1' + hddId[lintIndex].value);
                    if (bCancel){
                        tcnPercent.value = hddPercent.value;
                    }
                    else{
	                    if (tcnPercent.value != hddPercent.value){
                            arrayId[nCount] = hddId[lintIndex].value;
                            arrayPercent[nCount] = tcnPercent.value;
                            hddPercent.value = tcnPercent.value;
                            nCount++;
	                    }
	                }
	            }
            }
            if (bCancel){
                sQueryString = 'nAction=3';
	            insDefValues('Switch_UpdPercent', sQueryString,'/VtimeNet/Policy/policytra');
            }
            else{
                //Si hay cambios en las ventas o en las compras
                if (arrayId.length > 0 ||
                    hddBuysChanged.value == '1'){
                    sQueryString = 'nAction=2&nType=1&nId=' + arrayId.join(';') + '&nPercent=' + arrayPercent.join(';');
	                insDefValues('Switch_UpdPercent', sQueryString,'/VtimeNet/Policy/policytra');
                    hddBuysChanged.value = '0';
                }
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

<%
    Response.Write("<SCRIPT>var sTypeWindow='" & Request.QueryString("Type") & "'</SCRIPT>")
	With Response
        .Write(mobjMenu.setZone(2, "VI017", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
        .Write(mobjValues.ShowWindowsName("VI017", Request.QueryString("sWindowDescript")))
        .Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
    End With
    Response.Write(mobjValues.HiddenControl("optProcessType", "1"))
    Response.Write(mobjValues.HiddenControl("hddBuysChanged", "0"))
    Response.Write(mobjValues.HiddenControl("hddChkByAccount", Request.QueryString("sChkByAccount")))

    'Si se indica traspasar por cuenta entonces se debe indicar la cuenta
    If Request.QueryString("sChkByAccount") = "1" Then
        If Request.QueryString("nOrigin") <> vbNullString Then
            bFound = true
        End If
    Else
        bFound = true
    End If
    
    if bFound then
        bFound = mColBatch.Find (Session("sKey"), mintOrigin)
    end if


%>
    <TABLE width="100%">
    <%If Request.QueryString("sChkByAccount") = "1" Then%>
        <TR>
            <TD width="10%"><LABEL ID=0><%= GetLocalResourceObject("cbeOriginCaption") %></LABEL></TD>
            <TD>
            <%
                With mobjValues
                    .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End With
                Response.Write(mobjValues.PossiblesValues("cbeOrigin", "TAB_ORIGINPOL", eFunctions.Values.eValuesType.clngWindowType, mintOrigin, True, , , , , "insReload()", , , GetLocalResourceObject("cbeOriginToolTip"), , 1))
            %>
            </TD>
        </TR>
        <TR>
        </TR>
    <%End If%>
    </TABLE>

    <TABLE width="100%" border=0>
        <TR>
			<TD><%= mobjValues.CheckControl("chkProponum", GetLocalResourceObject("chkProponumCaption"), "1", "1")%></TD>
            <%If Request.QueryString("sChkByAccount") = "1" Then%>
                <TD ALIGN='RIGHT' width="5%"><%= mobjValues.AnimatedButtonControl("btnaccept", "/VtimeNet/images/btnAcceptOff.png", "Aceptar", , "InsAccept(false)", False)%></TD>
                <TD ALIGN='RIGHT' width="3%"><%= mobjValues.AnimatedButtonControl("btncancel", "/VtimeNet/images/btnCancelOff.png", "Cancelar", , "InsAccept(true)", False)%></TD>
            <%End If%>
        </TR>
    </TABLE>

    <TABLE WIDTH="100%" COLS=7 CLASS=grddata>
        <TR>
            <TH COLSPAN = 5 ALIGN = "center"><LABEL ID=0><%= GetLocalResourceObject("lblsaleCaption")%></LABEL></TH>
            <TH COLSPAN = 2 ALIGN = "center"><LABEL ID=0><%= GetLocalResourceObject("lblbuyCaption")%></LABEL></TH>
        </TR>
        <TR>
            <TH ALIGN = CENTER><LABEL ID=0><%= GetLocalResourceObject("tctFunds1Caption")%></LABEL></TH>
            <TH ALIGN = CENTER><LABEL ID=0><%= GetLocalResourceObject("tctBenef1Caption")%></LABEL></TH>
            <TH ALIGN = CENTER><LABEL ID=0><%= GetLocalResourceObject("tctquot1Caption")%></LABEL></TH>
            <TH ALIGN = CENTER><LABEL ID=0><%= GetLocalResourceObject("tctuf1Caption")%></LABEL></TH>                        
            <TH ALIGN = CENTER><LABEL ID=0><%= GetLocalResourceObject("tcnPercent1Caption")%></LABEL></TH>
            <TH ALIGN = CENTER><LABEL ID=0><%= GetLocalResourceObject("tctBuy1Caption")%></LABEL></TH>
            <TH ALIGN = CENTER><LABEL ID=0><%= GetLocalResourceObject("tctCheck1Caption")%></LABEL></TH>
        </TR>
        <% If bFound then 
			    For Each mclsBatch In mColBatch
			        If mclsBatch.nQuan_avail_sell_uf > 0 Then
		%>
        
        <TR>
			<TD> <% lnameControl = "tctFunds1" & mclsBatch.nId  
			         Response.Write(mobjValues.HiddenControl("hddId", mclsBatch.nId))
			         Response.Write(mobjValues.TextControl("tctFunds", 20, mclsBatch.sFund_sell, , , True))
			     %>
			</TD>
			<TD> <% lnameControl = "tctBenef1" & mclsBatch.nId  
			         Response.Write(mobjValues.TextControl(lnameControl, 20, mclsBatch.sTyp_profitworker_sell, , , True))
			     %>
			</TD>
			<TD> <%lnameControl = "tctquot1" & mclsBatch.nId  
			         Response.Write(mobjValues.TextControl(lnameControl, 20, mclsBatch.nQuan_avail_sell, , , True))
			     %>
			</TD>
			<TD> <% 
					lnameControl = "tctuf1" & mclsBatch.nId  
			         Response.Write(mobjValues.TextControl(lnameControl, 20, mclsBatch.nQuan_avail_sell_uf, , , True))
				%>
			</TD>
			<TD> 
				<% 
					lShowControl = "InsChangePercent(" & mclsBatch.nId & ",1,this);"
					lnameControl = "tcnPercent1" & mclsBatch.nId
				    Response.Write(mobjValues.NumericControl(lnameControl, 5, mclsBatch.nPercent_sell, , , , 2, , , , lShowControl, , , , False))
				    Response.Write(mobjValues.HiddenControl("hdd" & lnameControl, mobjValues.TypeToString(mclsBatch.nPercent_sell, Values.eTypeData.etdDouble, False, 2)))
				%>
			</TD>
			<TD>
				<% lnameControl = "tctBuy1" & mclsBatch.nId  
				   lShowControl = "ShowSell(" & mclsBatch.nFunds_sell  & ","  & mclsBatch.nTyp_profitworker_sell & ","  & mclsBatch.nId & ");"
				    Response.Write(mobjValues.AnimatedButtonControl(lnameControl, "/VTimeNet/Images/clfolder.png", , , lShowControl))
				%>
			</TD>
			<TD>
				<% lnameControl = "tctCheck1" & mclsBatch.nId  
				    Response.Write(mobjValues.CheckControl(lnameControl, "", mclsBatch.nCount_Sell, mclsBatch.nCount_Sell, , True))
				%>
			</TD>

        </TR>
        <%
                   End If
               Next
		   End if	
'mColBatch = Server.CreateObject("eBatch.tmp_switchs")
'mclsBatch = Server.CreateObject("eBatch.tmp_switch")
		%>
    </TABLE>
</FORM>
</BODY>
</HTML>