<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable modular utilizada para la carga y actualización de datos de la forma
Dim mclsContrnpro As eCoReinsuran.Contrnpro

Dim mclsContrMaster As eCoReinsuran.Contrmaster


'% insPreCR304: Realiza la lectura para la carga de los datos de la forma
'------------------------------------------------------------------------------------------------
Private Sub insPreCR304()
	'------------------------------------------------------------------------------------------------	
	
	Call mclsContrnpro.Find(Session("nNumber"), Session("nType"), Session("nBranch_rei"), Session("dEffecdate"), True)
	
	Call mclsContrMaster.Find(mclsContrnpro.nType_rel, Session("nNumber"), 0, 0, CDate(Nothing))
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsContrnpro = New eCoReinsuran.Contrnpro
mclsContrMaster = New eCoReinsuran.Contrmaster

mobjValues.ActionQuery = Session("bQuery")
Call insPreCR304()

mobjValues.sCodisplPage = "cr304"
%>
<SCRIPT>
// EnabledFields: Habilita los campos de acuerdo a la acción
//--------------------------------------------------------------------------------
function EnabledFields(nAction){
//--------------------------------------------------------------------------------
		
	if(nAction==302 || nAction==301 && nAction!=401){
		self.document.forms[0].cboCurrencyContract.disabled=false;	
		self.document.forms[0].tctDescript.disabled=false;
		self.document.forms[0].tcnAmount.disabled=false;	
		self.document.forms[0].tcnExcess.disabled=false;		
		self.document.forms[0].tcnRetention.disabled=false;
		self.document.forms[0].tcnMax_even.disabled=false;
		self.document.forms[0].tcnDeducible.disabled=false;				
		self.document.forms[0].tcnNumber_rep.disabled=false;		
		self.document.forms[0].tcnPorc_rep.disabled=false;
		self.document.forms[0].tcnMaxRespEven.disabled = false;
		self.document.forms[0].tcnNumberRepEven.disabled = false;
}	
}	
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "CR304", "CR304.aspx"))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCR304" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <P ALIGN="Center">    
    <LABEL ID=100630><A><%= GetLocalResourceObject("AnchorCaption") %> </A></LABEL></A></LABEL>    
    <LABEL ID=100630><A><%= GetLocalResourceObject("Anchor2Caption") %> </A></LABEL></A></LABEL>    
    <LABEL ID=100630><A><%= GetLocalResourceObject("Anchor3Caption") %></A></LABEL></A></LABEL>    
    </P>

<%=mobjValues.ShowWindowsName("CR304")%>    
	<BR>
    <TABLE WIDTH="100%" COLSPAN="4">
        <TR>
            <TD WIDTH="25%"><LABEL ID=100630><%= GetLocalResourceObject("dEndDateCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.DateControl("dEndDate", CStr(mclsContrMaster.dExpirdat),  , GetLocalResourceObject("dEndDateToolTip"), False,  ,  ,  , False)%></TD>
            <TD WIDTH="25%"><LABEL ID=100630><%= GetLocalResourceObject("cboCurrencyContractCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.PossiblesValues("cboCurrencyContract", "table11", 1, CStr(mclsContrMaster.nCurrency),  ,  ,  ,  ,  ,  ,  , True, GetLocalResourceObject("cboCurrencyContractToolTip"))%></TD>
		</TR>
		<TR>
            <TD WIDTH="25%"><LABEL ID=100630><%= GetLocalResourceObject("cboCurrencyPaymentCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.PossiblesValues("cboCurrencyPayment", "table11", 1, CStr(mclsContrMaster.nCurr_pay),  ,  ,  ,  ,  ,  ,  , True, GetLocalResourceObject("cboCurrencyPaymentToolTip"))%></TD>
            <TD WIDTH="25%"><LABEL ID=100630><%= GetLocalResourceObject("tcnMoraCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.NumericControl("tcnMora", 4, CStr(mclsContrnpro.nInterest),  , GetLocalResourceObject("tcnMoraToolTip"), False, 2,  ,  ,  ,  , False)%></TD>
		</TR>
		<TR>
            <TD WIDTH="25%"><LABEL ID=100631><%= GetLocalResourceObject("tctRoutineCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.TextControl("tctRoutine", 12, mclsContrnpro.sRouCessCL,  , GetLocalResourceObject("tctRoutineToolTip"),  ,  ,  ,  , False)%></TD>
            <TD WIDTH="25%"><LABEL ID=100631><%= GetLocalResourceObject("tctDescriptCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.TextControl("tctDescript", 30, mclsContrnpro.sDescript,  , GetLocalResourceObject("tctDescriptToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
			<TD WIDTH="25%"><LABEL ID=100632><%= GetLocalResourceObject("tcnRetentionCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.NumericControl("tcnRetention", 18, CStr(mclsContrnpro.nRetention),  , GetLocalResourceObject("tcnRetentionToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
		</TR>
	</TABLE>
	<TABLE WIDTH="100%">    				
		<TR>            
            <TD><LABEL ID=100633><%= GetLocalResourceObject("tcnExcessCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnExcess", 18, CStr(mclsContrnpro.nExcess),  , GetLocalResourceObject("tcnExcessToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
			<TD><LABEL ID=100634><%= GetLocalResourceObject("tcnMax_evenCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnMax_even", 18, CStr(mclsContrnpro.nMax_even),  , GetLocalResourceObject("tcnMax_evenToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=100635><%= GetLocalResourceObject("tcnDeducibleCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnDeducible", 18, CStr(mclsContrnpro.nDeducible),  , GetLocalResourceObject("tcnDeducibleToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=100636><%= GetLocalResourceObject("tcnAmountCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAmount", 18, CStr(mclsContrnpro.nAmount),  , GetLocalResourceObject("tcnAmountToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
		</TR>
        <TR>
            <TD colspan="2"></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnMaxRespEvenCaption")%></LABEL></TD>
            <TD><%= mobjValues.NumericControl("tcnMaxRespEven", 18, CStr(mclsContrnpro.nMaxRespEven), , GetLocalResourceObject("tcnMaxRespEvenToolTip"), True, 6, , , , , True)%></TD>
		</TR>

        <TR>
            <TD colspan="2"></TD>
            <TD><LABEL ID=LABEL1><%= GetLocalResourceObject("tcnLifeNum")%></LABEL></TD>
            <TD><%= mobjValues.NumericControl("tcnLifeNum", 10, CStr(mclsContrnpro.nLifeNum), , GetLocalResourceObject("tcnLifeNumToolTip"), True, 0, , , , ,False)%></TD>
		</TR>
		<TR>
			<TD WIDTH="45%" COLSPAN="5" CLASS="HighLighted"><LABEL ID=LABEL2><A NAME="Reposiciones"><%= GetLocalResourceObject("SpecialConditionCaption") %></A></LABEL></TD>
        </TR>
        <TR>
		    <TD COLSPAN="5"><HR></TD>
        </TR>
        <TR>
            <TD><LABEL ID=LABEL7><%= GetLocalResourceObject("tcnSpcpriority")%></LABEL></TD>
            <TD><%= mobjValues.NumericControl("tcnSpcpriority", 10, CStr(mclsContrnpro.nSpcpriority), , GetLocalResourceObject("tcnSpcpriorityToolTip"), True, 0, , , , , False)%></TD>
            <TD><LABEL ID=LABEL6><%= GetLocalResourceObject("tcnSpclimit")%></LABEL></TD>
            <TD><%= mobjValues.NumericControl("tcnSpclimit", 10, CStr(mclsContrnpro.nspclimit), , GetLocalResourceObject("tcnSpclimitToolTip"), True, 0, , , , , False)%></TD>
		</TR>
    </TABLE>
	<TABLE WIDTH="100%">
		<TR>
			<TD WIDTH="45%" COLSPAN="5" CLASS="HighLighted"><LABEL ID=100637><A NAME="Reposiciones"><%= GetLocalResourceObject("AnchorReposicionesCaption") %></A></LABEL></TD>
        </TR>
        <TR>
		    <TD COLSPAN="5"><HR></TD>
        </TR>
	</TABLE>
	<TABLE  WIDTH="100%">
		<TR>		
            <TD WIDTH="40%">
	            <TABLE  WIDTH="100%">
		            <TR>		
                        <TD><LABEL ID=LABEL3><%= GetLocalResourceObject("tcnNumber_repCaption") %></LABEL></TD>
                        <TD><%=mobjValues.NumericControl("tcnNumber_rep", 3, CStr(mclsContrnpro.nNumber_rep),  , GetLocalResourceObject("tcnNumber_repToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
                    </TR>
		            <TR>
                        <TD><LABEL ID=LABEL4><%= GetLocalResourceObject("tcnPorc_repCaption") %></LABEL></TD>            
                        <TD><%=mobjValues.NumericControl("tcnPorc_rep", 3, CStr(mclsContrnpro.nPorc_rep),  , GetLocalResourceObject("tcnPorc_repToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
                    </TR>
		            <TR>
                        <TD><LABEL ID=LABEL5><%= GetLocalResourceObject("tcnNumberRepEvenCaption")%></LABEL></TD>
                        <TD><%= mobjValues.NumericControl("tcnNumberRepEven", 3, CStr(mclsContrnpro.nNumberRepEven), , GetLocalResourceObject("tcnNumberRepEvenToolTip"), , 0, , , , , True)%></TD>
                    </TR>
                </TABLE>

            </TD>
            <TD>&nbsp;</TD>
            <TD WIDTH="40%">
	            <TABLE  WIDTH="100%">
                    <TR>
                        <TD CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorProrrataCaption")%></LABEL></TD>
                    </TR>
                    <TR>
		                <TD><HR></TD>
                    </TR>
                    <TR>
                        <TD>
                            <%  With Response
                                    .Write(mobjValues.OptionControl(0, "optProrateRep", GetLocalResourceObject("optProrateRep_CStr1Caption"), CStr(IIf(mclsContrnpro.sProrateRep = "1", 1, 0)), CStr(1), , , , GetLocalResourceObject("optProrateRep_CStr1ToolTip")))
                                End With
                            %>
                        </TD>
                    </TR>
		            <TR>
                        <TD>
                            <%  With Response
                                    .Write(mobjValues.OptionControl(0, "optProrateRep", GetLocalResourceObject("optProrateRep_CStr2Caption"), CStr(IIf(mclsContrnpro.sProrateRep = "2", 1, 0)), CStr(2), , , , GetLocalResourceObject("optProrateRep_CStr2ToolTip")))
                                End With
                            %>
                        </TD>
                    </TR>
		            <TR>
                        <TD>
                            <%  With Response
                                    .Write(mobjValues.OptionControl(0, "optProrateRep", GetLocalResourceObject("optProrateRep_CStr3Caption"), CStr(IIf(mclsContrnpro.sProrateRep = "3" Or mclsContrnpro.sProrateRep = String.Empty, 1, 0)), CStr(3), , , , GetLocalResourceObject("optProrateRep_CStr3ToolTip")))
                                End With
                            %>
                        </TD>
                    </TR>
                </TABLE>

            </TD>
        </TR>
    </TABLE>

	<TABLE WIDTH="100%">
		<TR>
			<TD WIDTH="45%" COLSPAN="5" CLASS="HighLighted"><LABEL ID=100637><A NAME="Periodicidad"><%= GetLocalResourceObject("AnchorPeriodicidadCaption") %></A></LABEL></TD>
        </TR>
        <TR>
		    <TD COLSPAN="5"><HR></TD>
        </TR>
	</TABLE>
	<TABLE  WIDTH="100%">
		<TR>		
            <TD><LABEL ID=100638><%= GetLocalResourceObject("cboPeriodCaption") %></LABEL></TD>
            <%mobjValues.List = "5"%>
            <%mobjValues.TypeList = 2%>
            <TD><%=mobjValues.PossiblesValues("cboPeriod", "Table97", 1, CStr(mclsContrnpro.nFreqct), False, False, "", "",  ,  , False,  , GetLocalResourceObject("cboPeriodToolTip"))%></TD>
            <TD><LABEL ID=100639><%= GetLocalResourceObject("tcnMonthCTCaption") %></LABEL></TD>
            <%If mclsContrnpro.nNextmonthc > 0 Then%>
				<TD><%=mobjValues.NumericControl("tcnMonthCT", 2, CStr(mclsContrnpro.nNextmonthc),  , GetLocalResourceObject("tcnMonthCTToolTip"), False, 0,  ,  ,  ,  , True)%></TD>
			<%Else%>
				<TD><%=mobjValues.NumericControl("tcnMonthCT", 2, CStr(Month(mclsContrnpro.dEffecdate)),  , GetLocalResourceObject("tcnMonthCTToolTip"), False, 0,  ,  ,  ,  , True)%></TD>
			<%End If%>
            <TD><LABEL ID=100639><%= GetLocalResourceObject("tcnYearCTCaption") %></LABEL></TD>
            <%If mclsContrnpro.nNextyearc > 0 Then%>
				<TD><%=mobjValues.NumericControl("tcnYearCT", 4, CStr(mclsContrnpro.nNextyearc),  , GetLocalResourceObject("tcnYearCTToolTip"), False, 0,  ,  ,  ,  , True)%></TD>
			<%Else%>
				<TD><%=mobjValues.NumericControl("tcnYearCT", 4, CStr(Year(mclsContrnpro.dEffecdate)),  , GetLocalResourceObject("tcnYearCTToolTip"), False, 0,  ,  ,  ,  , True)%></TD>
			<%End If%>
        </TR>
    </TABLE>
    
	<TABLE WIDTH="100%">
		<TR>
			<TD WIDTH="45%" COLSPAN="5" CLASS="HighLighted"><LABEL ID=100637><A NAME="Frecuencia"><%= GetLocalResourceObject("AnchorFrecuenciaCaption") %></A></LABEL></TD>
        </TR>
        <TR>
		    <TD COLSPAN="5"><HR></TD>
        </TR>
	</TABLE>
	<TABLE  WIDTH="100%">
		<TR>		
            <TD><LABEL ID=100638><%= GetLocalResourceObject("cboFrequencyCaption") %></LABEL></TD>
            <%mobjValues.List = "5"%>
            <%mobjValues.TypeList = 2%>
            <TD><%=mobjValues.PossiblesValues("cboFrequency", "Table97", 1, CStr(mclsContrnpro.nFreqpay), False, False, "", "",  ,  , False,  , GetLocalResourceObject("cboFrequencyToolTip"))%></TD>
            <TD><LABEL ID=100639><%= GetLocalResourceObject("tcnMonthCTCaption") %></LABEL></TD>
            <%If mclsContrnpro.nNextmonthpa > 0 Then%>
				<TD><%=mobjValues.NumericControl("tcnMonthPF", 2, CStr(mclsContrnpro.nNextmonthpa),  , GetLocalResourceObject("tcnMonthPFToolTip"), False, 0,  ,  ,  ,  , True)%></TD>
			<%Else%>
				<TD><%=mobjValues.NumericControl("tcnMonthPF", 2, CStr(Month(mclsContrnpro.dEffecdate)),  , GetLocalResourceObject("tcnMonthPFToolTip"), False, 0,  ,  ,  ,  , True)%></TD>
			<%End If%>			
            <TD><LABEL ID=100639><%= GetLocalResourceObject("tcnYearCTCaption") %></LABEL></TD>
            <%If mclsContrnpro.nNextyearpa > 0 Then%>
				<TD><%=mobjValues.NumericControl("tcnYearPF", 4, CStr(mclsContrnpro.nNextyearpa),  , GetLocalResourceObject("tcnYearPFToolTip"), False, 0,  ,  ,  ,  , True)%></TD>
			<%Else%>
				<TD><%=mobjValues.NumericControl("tcnYearPF", 4, CStr(Year(mclsContrnpro.dEffecdate)),  , GetLocalResourceObject("tcnYearPFToolTip"), False, 0,  ,  ,  ,  , True)%></TD>
			<%End If%>			
        </TR>
    </TABLE>
     <%Response.Write(mobjValues.BeginPageButton)%>
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"     
</SCRIPT>     
</FORM>
</BODY>
</HTML>
<SCRIPT>
EnabledFields(<%=Request.QueryString.Item("nMainAction")%>)
</SCRIPT>
