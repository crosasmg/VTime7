<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable modular utilizada para la carga y actualización de datos de la forma
Dim mclsFinanceCo As eFinance.financeCO


'% insPreFI012: Realiza la lectura para la carga de los datos de la forma
'------------------------------------------------------------------------------------------------
Private Sub insPreFI012()
	'------------------------------------------------------------------------------------------------	
	
	Call mclsFinanceCo.insPreFI012(mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nQ_Draft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dStat_date"), eFunctions.Values.eTypeData.etdDate))
	
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsFinanceCo = New eFinance.financeCO

mobjValues.ActionQuery = Session("bQuery")
Call insPreFI012()

mobjValues.sCodisplPage = "fi012"
%>
<SCRIPT>
//% ShowDefVal: Muestra los valores del giro según el factor de cambio
//-------------------------------------------------------------------------------------------
function ShowDefVal(sField){
//-------------------------------------------------------------------------------------------
//	alert(<%=Session("nQ_Draft")%>)
	ShowPopUp("/VTimeNet/Finance/Financing/ShowDefValues.aspx?Field=" + sField +  "&nCurr_cont=" + self.document.forms[0].cbeCurr_cont.value + "&nContrat=" + <%=Session("nContrat")%> + "&nQ_Draft=" + <%=Session("nQ_Draft")%> + "&nInterest=" + self.document.forms[0].tcnInterest.value + "&Dscto_amo=" + self.document.forms[0].tcnDscto_amo.value + "&nExpenses=" + self.document.forms[0].tcnExpenses.value, "ShowDefValuesFinance" , 1, 1,"no","no",2000,2000);
}
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "FI012", "FI012.aspx"))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<FORM METHOD="post" ID="FORM" NAME="frmDraftCollection" ACTION="valFinancing.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <P ALIGN="Center">    
    <LABEL><A HREF="#Datos giro"><%= GetLocalResourceObject("AnchorDatos giroCaption") %></LABEL></A></LABEL><LABEL> | </LABEL>
    <LABEL><A HREF="#Datos cobro"><%= GetLocalResourceObject("AnchorDatos cobroCaption") %></A></LABEL>    
    </P>

    <TABLE WIDTH="100%">
		<TR>                       
			<TD>&nbsp;</TD>            
			<TD>&nbsp;</TD>            
			<TD>&nbsp;</TD>            
			<TD WIDTH="45%" COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="Datos giro"><%= GetLocalResourceObject("AnchorDatos giro2Caption") %></A></LABEL></TD>
        </TR>        
        <TR>			       
		    <TD COLSPAN="5"><HR></TD>		    
        </TR>      				
	</TABLE>
	<TABLE>		        				        		            
        <TR>
            <TD><LABEL ID=11159><%= GetLocalResourceObject("tcdExpirDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdExpirDate", CStr(mclsFinanceCo.dExpirDate),  , GetLocalResourceObject("tcdExpirDateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>    
		<TR>
			<TD><LABEL ID=11153><%= GetLocalResourceObject("tctClientCodCaption") %></LABEL></TD>
			<TD><%=mobjValues.ClientControl("tctClientCod", mclsFinanceCo.sClient,  , GetLocalResourceObject("tctClientCodToolTip"),  , True, "tctClieName", False)%></TD>
		</TR>
	</TABLE>
	<TABLE WIDTH="100%">		
		<TR>                       
			<TD>&nbsp;</TD>            
			<TD>&nbsp;</TD>            
			<TD>&nbsp;</TD>            
			<TD WIDTH="45%" COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="Datos cobro"><%= GetLocalResourceObject("AnchorDatos cobro2Caption") %></A></LABEL></TD>
        </TR>        
        <TR>			       
		    <TD COLSPAN="5"><HR></TD>		    
        </TR>      								            
        <TR>
			<TD><LABEL ID=11155><%= GetLocalResourceObject("tcnDraft_amountCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnDraft_amount", 18, CStr(mclsFinanceCo.nDraft_amo),  ,  , True, 6,  ,  ,  ,  , True)%></TD>
		</TR>
        <TR>
            <TD><LABEL ID=11149><%= GetLocalResourceObject("cbeCurr_contCaption") %></LABEL></TD>            
            <TD><%=mobjValues.PossiblesValues("cbeCurr_cont", "table11", 1, CStr(mclsFinanceCo.nCurr_cont),  ,  ,  ,  ,  , "ShowDefVal(""Exchange_2"");",  ,  , GetLocalResourceObject("cbeCurr_contToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=11157><%= GetLocalResourceObject("tcnExchangeCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnExchange", 18, CStr(mclsFinanceCo.nExchange),  ,  , True, 6,  ,  ,  ,  , True)%></TD>            
        </TR>			
        <TR>
            <TD><LABEL ID=11150><%= GetLocalResourceObject("tcnAmountCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAmount", 18, CStr(mclsFinanceCo.nAmount),  ,  , True, 6,  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=11156><%= GetLocalResourceObject("tcnDscto_amoCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnDscto_amo", 18, CStr(mclsFinanceCo.nDscto_amo),  ,  , True, 6,  ,  ,  ,  , mclsFinanceCo.blnDscto_amo)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=11160><%= GetLocalResourceObject("tcnInterestCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnInterest", 18, CStr(mclsFinanceCo.nInterest),  ,  , True, 6,  ,  ,  ,  , mclsFinanceCo.blnInterest)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=11158><%= GetLocalResourceObject("tcnExpensesCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnExpenses", 18, CStr(mclsFinanceCo.nExpenses),  ,  , True, 6)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=11163><%= GetLocalResourceObject("tcnTotalAmoCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnTotalAmo", 18, CStr(mclsFinanceCo.nTotalAmo),  ,  , True, 6,  ,  ,  ,  , True)%></TD>
        </TR>
	</TABLE>
	<TABLE WIDTH="100%">
		<TR>	
			<TD>&nbsp;</TD>  
		</TR>    	            	        
        <TR>
			<TD WIDTH="10%">&nbsp;</TD>
			<TD WIDTH="10%">&nbsp;</TD>					
            <TD WIDTH="20%"><LABEL ID=11161><%= GetLocalResourceObject("cbePayWayCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbePayWay", "table258", 1,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbePayWayToolTip"))%></TD>
        </TR>
    </TABLE>   
    <%Response.Write(mobjValues.BeginPageButton)%>
</FORM>
</BODY>
</HTML>




