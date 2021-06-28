<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable modular utilizada para la carga y actualización de datos de la forma
Dim mclsContrproc As eCoReinsuran.Contrproc
'- Se define la variable modular utilizada para recuperar los datos de contrmaster
Dim mclsContrmaster As eCoReinsuran.Contrmaster

Dim sCap_nom As Object
Dim sCap_ri As Object
Dim sFrom_cheq As Object
Dim sFrom_trans As Object


'% insPreCR302: Realiza la lectura para la carga de los datos de la forma
'------------------------------------------------------------------------------------------------
Private Sub insPreCR302()
	'------------------------------------------------------------------------------------------------	
	Call mclsContrproc.insreaBrancht(Session("nBranch"))
	Call mclsContrmaster.Find(1, Session("nNumber"), Session("nType"), Session("nBranch"), Session("dEffecdate"))
	
	Call mclsContrproc.Find(Session("nNumber"), Session("nType"), Session("nBranch"), Session("dEffecdate"), True)
	mclsContrmaster.Find_Num(Session("nNumber"))
	
	If mclsContrmaster.sFormpay = "2" Then
		sFrom_cheq = 1
	ElseIf mclsContrmaster.sFormpay = "1" Then 
		sFrom_trans = 1
	End If
	
	If mclsContrproc.sCap_nom_ri = "1" Then
		sCap_nom = 1
	ElseIf mclsContrproc.sCap_nom_ri = "2" Then 
		sCap_ri = 1
	End If
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsContrproc = New eCoReinsuran.Contrproc
mclsContrmaster = New eCoReinsuran.Contrmaster

mobjValues.ActionQuery = Session("bQuery")
Call insPreCR302()

mobjValues.sCodisplPage = "cr302"
%>
<SCRIPT>
// OnChange_reser: habilita o desabilita los campos de la reserva
//-----------------------------------------------------------------------------------
function OnChange_reser(){
//-----------------------------------------------------------------------------------
    if((self.document.forms[0].tcnFact_reser.value!="0" &&
	   self.document.forms[0].tcnFact_reser.value!="0,00" && 
	   self.document.forms[0].tcnFact_reser.value!="0.00" && 
	   self.document.forms[0].tcnFact_reser.value!="") || 
	   self.document.forms[0].tcnPrem_dep.value!="0" &&
	   self.document.forms[0].tcnPrem_dep.value!="0,00" && 
	   self.document.forms[0].tcnPrem_dep.value!="0.00" && 
	   self.document.forms[0].tcnPrem_dep.value!="")
	    self.document.forms[0].tcnInt_prem.disabled = false
	else
	{
	    self.document.forms[0].tcnInt_prem.disabled = true;
	    self.document.forms[0].tcnInt_prem.value = '';
	}
	    
	    
	
    if(self.document.forms[0].tcnFact_reser.value!="0" &&
	   self.document.forms[0].tcnFact_reser.value!="0,00" && 
	   self.document.forms[0].tcnFact_reser.value!="0.00" && 
	   self.document.forms[0].tcnFact_reser.value!="")
	   self.document.forms[0].tcnPrem_dep.disabled = true
	else
	   self.document.forms[0].tcnPrem_dep.disabled = false;
	
	   
    if (self.document.forms[0].tcnPrem_dep.value!="0" &&
	    self.document.forms[0].tcnPrem_dep.value!="0,00" && 
	    self.document.forms[0].tcnPrem_dep.value!="0.00" && 
		self.document.forms[0].tcnPrem_dep.value!="")	   
	    self.document.forms[0].tcnFact_reser.disabled = true
	else
	    self.document.forms[0].tcnFact_reser.disabled = false;
}

// OnChange_cess: habilita o desabilita los campos de la Cesion
//-----------------------------------------------------------------------------------
function OnChange_cess(){
//-----------------------------------------------------------------------------------
   if (self.document.forms[0].chkCessprcov.checked) 
   {
       self.document.forms[0].tcnRate.disabled = true; 
       self.document.forms[0].tcnCessprfix.disabled = true;  
       self.document.forms[0].tcnRate.value='';
       self.document.forms[0].tcnCessprfix.value='';
   }
   else
   {
       if (self.document.forms[0].chkInd_age.checked)
           self.document.forms[0].valInd_age.disabled = false;      
       else
       {
       self.document.forms[0].tcnRate.disabled = false; 
       self.document.forms[0].tcnCessprfix.disabled=false;
       self.document.forms[0].tcnCessprfix.disabled = false;  
       self.document.forms[0].valInd_age.disabled = true;
       self.document.forms[0].valInd_age.value = '';            
       }
   }
}

// OnChange_comm: habilita o desabilita los campos de la comisión
//-----------------------------------------------------------------------------------
function OnChange_comm(){
//-----------------------------------------------------------------------------------
	if (self.document.forms[0].chkCommcov.checked)
	{
		self.document.forms[0].tcnFixed_prat.disabled = true;
		self.document.forms[0].tcnFixed_prat.value = '';
		self.document.forms[0].chkCommcov.value = "1";
	}
	else
	{
		self.document.forms[0].tcnFixed_prat.disabled = false;
		self.document.forms[0].chkCommcov.value = "2";
	}

}

// EnabledFields: Habilita los campos de acuerdo a la Opción y el valor de los campos
//-----------------------------------------------------------------------------------
function EnabledFields(sOption,sField,nAction,sBrancht){
//-----------------------------------------------------------------------------------
	if(nAction!=401)
	{
		if(sBrancht==1)
		{
			self.document.forms[0].elements["optCap_nom_ri"][0].disabled=true;		
			self.document.forms[0].elements["optCap_nom_ri"][1].disabled=true;				
		}
		else 
		{
			self.document.forms[0].elements["optCap_nom_ri"][0].disabled=false;		
			self.document.forms[0].elements["optCap_nom_ri"][1].disabled=false;					
		}	

		switch(sOption)
		{
			case "First":
			{	
				if(self.document.forms[0].tcnInt_prem.value=="0" || 
				   self.document.forms[0].tcnInt_prem.value=="0,00" || 
				   self.document.forms[0].tcnInt_prem.value=="0.00") 					
						self.document.forms[0].tcnInt_prem.disabled=true;					

				if(self.document.forms[0].tcnInt_claim.value=="0" || 
				   self.document.forms[0].tcnInt_claim.value=="0,00" || 
				   self.document.forms[0].tcnInt_claim.value=="0.00")
						self.document.forms[0].tcnInt_claim.disabled=true;			
					
				if(sField==10)			
					self.document.forms[0].elements["optCap_nom_ri"][0].checked=true;						
				else if(sField==20)				
					self.document.forms[0].elements["optCap_nom_ri"][1].checked=true;
				else
					self.document.forms[0].elements["optCap_nom_ri"][1].checked=true;
						
				if(self.document.forms[0].tcnInt_prem.value!="0" ||
				   self.document.forms[0].tcnInt_prem.value!="0,00" || 
				   self.document.forms[0].tcnInt_prem.value!="0.00" && 
					   
				   self.document.forms[0].tcnInt_claim.value!="0" || 
				   self.document.forms[0].tcnInt_claim.value!="0,00" || 
				   self.document.forms[0].tcnInt_claim.value!="0.00")
						self.document.forms[0].tcnFact_reser.disabled=true;
				if(self.document.forms[0].tcnFact_reser.value!=0)
				{
					self.document.forms[0].tcnFact_reser.disabled=false;
					self.document.forms[0].tcnPrem_dep.disabled=true;	
				}	
				break;			
			}
			case "Second":
			{
				if(sField.value!="0" && sField.value!="0,00" && sField.value!="0.00" && sField.value!='')
				{
					self.document.forms[0].tcnFact_reser.disabled=true;
					self.document.forms[0].tcnInt_prem.disabled=false;
				}	
				else
				{
					self.document.forms[0].tcnFact_reser.disabled=false;
					self.document.forms[0].tcnInt_prem.disabled=true;
					self.document.forms[0].tcnInt_prem.value="0,00";
				}	
				break;
			}	
			case "Third":
			{
				if(sField.value!="0" && sField.value!="0,00" && sField.value!="0.00")
				{
					self.document.forms[0].tcnPrem_dep.disabled=true;
					self.document.forms[0].tcnInt_prem.disabled=false;
				}	
				else
					self.document.forms[0].tcnInt_prem.disabled=true;
				if(sField.value=="0" || sField.value=="0,00" || sField.value=="0.00")
					self.document.forms[0].tcnPrem_dep.disabled=false;
				break;
			}	
			case "Fourth":
		    {
				if(sField.checked)
				{
					self.document.forms[0].tcnInt_claim.disabled=false;	
					self.document.forms[0].tcnInt_prem.disabled=false;
					self.document.forms[0].valCurrpay.disabled=false;
				}
				else
				{
					self.document.forms[0].tcnInt_claim.disabled=true;
					self.document.forms[0].tcnInt_claim.value="0,00";
					self.document.forms[0].tcnInt_prem.disabled=true;
					self.document.forms[0].tcnInt_prem.value="0,00";
					self.document.forms[0].valCurrpay.disabled=true;
				}
			}		
		}	
	}	
}	
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "CR302", "CR302.aspx"))
End With
mobjMenu = Nothing%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">

<TD><BR></TD>
<FORM METHOD="POST" ID="FORM" NAME="frmCR302" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <P ALIGN="Center">    
    <LABEL ID=100602><A HREF="#Ces"><%= GetLocalResourceObject("AnchorCaption") %></A></LABEL></LABEL><LABEL ID=0> | </LABEL>
    <LABEL ID=100602><A HREF="#Reserva primas"><%= GetLocalResourceObject("AnchorReserva primasCaption") %></A></LABEL><LABEL ID=0> | </LABEL>
    <LABEL ID=100603><A HREF="#Reserva siniestros"><%= GetLocalResourceObject("AnchorReserva siniestrosCaption") %></A></LABEL><LABEL ID=0> | </LABEL>
    <LABEL ID=100604><A HREF="#Capital"><%= GetLocalResourceObject("AnchorCapitalCaption") %></A></LABEL>
    </P>
<%=mobjValues.ShowWindowsName("CR302")%>  
    <TABLE WIDTH="100%">
		<TR>                       
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=100605><A NAME="Ces"><%= GetLocalResourceObject("AnchorCesión de PrimasCaption") %></A></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=100606>Comisión<A NAME="Com"></A></LABEL></TD>
        </TR>       	
        <TR>
		    <TD COLSPAN="4"><HR></TD>		    
		    <TD WIDTH="10%">&nbsp;</TD>
		    <TD COLSPAN="4"><HR></TD>
        </TR>    
        <TR>
           <TD COLSPAN="4"><%= mobjValues.CheckControl("chkCessprcov", GetLocalResourceObject("chkCessprcov"), mclsContrproc.sCessprcov, "1", "OnChange_cess();", , , GetLocalResourceObject("chkCessprcovTT"))%></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <<TD COLSPAN="4"><%= mobjValues.CheckControl("chkCommcov", GetLocalResourceObject("chkCommcov"), mclsContrproc.sCommCov, "1", "OnChange_comm();", , , GetLocalResourceObject("chkCommcovToolTip"))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4"><LABEL><%= mobjValues.CheckControl("chkCesscia", GetLocalResourceObject("chkCesscia"), mclsContrproc.sCesscia, "1", "OnChange_cess();", , , GetLocalResourceObject("chkCessciaTT"))%></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4"></TD> 
        </TR>
        
        
        <TR>
            <TD COLSPAN="4"><LABEL>
              <%If mclsContrproc.nInd_Age = 0 Then%>
                    <%=mobjValues.CheckControl("chkInd_age", GetLocalResourceObject("chkInd_ageCaption"), "", "1", "OnChange_cess();",  ,  , GetLocalResourceObject("chkInd_ageToolTip"))%>
              <%Else%>
                    <%=mobjValues.CheckControl("chkInd_age", GetLocalResourceObject("chkInd_ageCaption"), "1", "1", "OnChange_cess();",  ,  , GetLocalResourceObject("chkInd_ageToolTip"))%>
              <%End If%>   
            </LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4"></TD> 
        </TR>
        <TR>
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("valInd_ageCaption") %> </LABEL>
            
            
            
                <%If mclsContrproc.nInd_Age = 0 Then%>
                      <%=mobjValues.PossiblesValues("valInd_age", "Table8010", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valInd_ageToolTip"),  ,  , True)%></TD>
                 <%Else%>
                      <%=mobjValues.PossiblesValues("valInd_age", "Table8010", 1, CStr(mclsContrproc.nInd_Age),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("valInd_ageToolTip"),  ,  , True)%></TD>
                 <%End If%>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4"></TD> 
        </TR>
        <TR>
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("tcnRateCaption") %></LABEL>
            <%=mobjValues.NumericControl("tcnRate", 4, CStr(mclsContrproc.nRate),  , GetLocalResourceObject("tcnRateToolTip"), True, 2,  ,  ,  , "OnChange_cess();")%></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("tcnFixed_pratCaption") %></LABEL>
            <%=mobjValues.NumericControl("tcnFixed_prat", 4, CStr(mclsContrproc.nFixed_prat),  , GetLocalResourceObject("tcnFixed_pratToolTip"), True, 2,  ,  ,  , "OnChange_comm();")%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("Anchor5Caption") %> </LABEL><%=mobjValues.NumericControl("tcnCessprfix", 18, CStr(mclsContrproc.nCessprfix),  , "Monto de cesión del contrato", True, 6,  ,  ,  , "OnChange_cess();")%></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4"></TD>
        </TR>      
        <TR>
            <TD COLSPAN="4"><%= mobjValues.CheckControl("chkExtraprem", GetLocalResourceObject("chkExtraprem"), mclsContrproc.sExtraprem, "1", "EnabledFields(""Fourth"",this)", False, , GetLocalResourceObject("chkExtrapremToolTip"))%></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4"></TD>
        </TR>      
        <TR>
             <TD COLSPAN="4"><%= mobjValues.CheckControl("chkGencess", GetLocalResourceObject("chkGencess"), mclsContrproc.sGencess, "1", "EnabledFields(""Fourth"",this)", , , GetLocalResourceObject("chkGencessToolTip"))%></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4"></TD>
        </TR>
        <TR> 
                              
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Reserva de primas"><%= GetLocalResourceObject("AnchorReserva de primasCaption") %></A></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Reserva de siniestros"><%= GetLocalResourceObject("AnchorReserva de siniestrosCaption") %></A></LABEL></TD>
        </TR>        
        <TR>
		    <TD COLSPAN="4"><HR></TD>		    
		    <TD WIDTH="10%">&nbsp;</TD>
		    <TD COLSPAN="4"><HR></TD>
        </TR>      								            
        <TR>
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("tcnPrem_depCaption") %></LABEL>
            <%=mobjValues.NumericControl("tcnPrem_dep", 4, CStr(mclsContrproc.nPrem_dep),  , GetLocalResourceObject("tcnPrem_depToolTip"), True, 2,  ,  ,  , "OnChange_reser();")%></TD>            
            <TD WIDTH="10%">&nbsp;</TD>   
            <TD COLSPAN="4"><%= mobjValues.CheckControl("chkReser_clai", GetLocalResourceObject("chkReser_clai"), mclsContrproc.sReser_clai, "1", "EnabledFields(""Fourth"",this)", , , GetLocalResourceObject("chkReser_claiTooTip"))%></TD>
        </TR>    
        <TR>
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("tcnFact_reserCaption") %></LABEL>
            <%=mobjValues.NumericControl("tcnFact_reser", 18, CStr(mclsContrproc.nFact_reser),  , GetLocalResourceObject("tcnFact_reserToolTip"), True, 6,  ,  ,  , "OnChange_reser();")%></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("tcnInt_claimCaption") %></LABEL>
            <%=mobjValues.NumericControl("tcnInt_claim", 4, CStr(mclsContrproc.nInt_claim),  , GetLocalResourceObject("tcnInt_claimToolTip"), True, 2,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("tcnInt_claimCaption") %></LABEL>
            <%=mobjValues.NumericControl("tcnInt_prem", 4, CStr(mclsContrproc.nInt_prem),  , GetLocalResourceObject("tcnInt_premToolTip"), True, 2,  ,  ,  ,  , True)%></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("valCurrpayCaption") %></LABEL>
			<%=mobjValues.PossiblesValues("valCurrpay", "Table11", 1, CStr(mclsContrmaster.nCurr_pay),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCurrpayToolTip"),  ,  , True)%></TD>
        </TR>
    <BR>
    <TD><LABEL ><%= GetLocalResourceObject("tcnMinimumCapitalCaption") %></LABEL>
    <%=mobjValues.NumericControl("tcnMinimumCapital", 18, CStr(mclsContrproc.nMincapcess),  , GetLocalResourceObject("tcnMinimumCapitalToolTip"), True, 6,  ,  ,  ,  , False)%></TD>
    <BR>
		<TR>                       
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Forma de pago"><%= GetLocalResourceObject("AnchorForma de pagoCaption") %></A></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Capital"><%= GetLocalResourceObject("AnchorCapital2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
		    <TD COLSPAN="4"><HR></TD>		    
		    <TD WIDTH="10%">&nbsp;</TD>
		    <TD COLSPAN="4"><HR></TD>		    
        </TR>  
        <TR>
            <TD COLSPAN="4"><%=mobjValues.OptionControl(100617, "optFormpay", GetLocalResourceObject("optFormpay_2Caption"), sFrom_cheq, "2")%></TD>
		    <TD WIDTH="10%">&nbsp;</TD>
		    <TD COLSPAN="4"><%=mobjValues.OptionControl(100617, "optCap_nom_ri", GetLocalResourceObject("optCap_nom_ri_1Caption"), sCap_nom, "1")%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4"><%=mobjValues.OptionControl(100618, "optFormpay", GetLocalResourceObject("optFormpay_1Caption"), sFrom_trans, "1")%></TD>
		    <TD WIDTH="10%">&nbsp;</TD>
		    <TD COLSPAN="4"><%=mobjValues.OptionControl(100618, "optCap_nom_ri", GetLocalResourceObject("optCap_nom_ri_2Caption"), sCap_ri, "2")%></TD>
		</TR>
        <TR>                       
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=100605><A NAME="Cuentas tecnicas"><%= GetLocalResourceObject("AnchorCuentas tecnicasCaption") %></A></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=100606><A NAME="Frecuencia de pago"><%= GetLocalResourceObject("AnchorFrecuencia de pagoCaption") %></A></LABEL></TD>
        </TR>     
		<TR>
		    <TD COLSPAN="4"><HR></TD>		    
		    <TD WIDTH="10%">&nbsp;</TD>
		    <TD COLSPAN="4"><HR></TD>
        </TR>    
        <TR>
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("cboFqcy_accCaption") %></LABEL>
            <%mobjValues.List = "5"%>
            <%mobjValues.TypeList = 2%>
            <%
If mclsContrproc.nFqcy_acc > 0 Then
	%>
					<%=mobjValues.PossiblesValues("cboFqcy_acc", "table97", 1, CStr(mclsContrproc.nFqcy_acc),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboFqcy_accToolTip"))%>
			<%	
Else
	%>
					<%=mobjValues.PossiblesValues("cboFqcy_acc", "table97", 1, CStr(1),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboFqcy_accToolTip"))%>
			<%	
End If
%>
            </TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("cboFreqpayCaption") %></LABEL>
            <%mobjValues.List = "5"%>
            <%mobjValues.TypeList = 2%>
            <%
If mclsContrproc.nFreqpay > 0 Then
	%>
					<%=mobjValues.PossiblesValues("cboFreqpay", "table97", 1, CStr(mclsContrproc.nFreqpay),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboFreqpayToolTip"))%>
			<%	
Else
	%>
					<%=mobjValues.PossiblesValues("cboFreqpay", "table97", 1, CStr(1),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboFreqpayToolTip"))%>
			<%	
End If
%>
			</TD>
        </TR>        
        <TR>
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("tcnextmonthcCaption") %></LABEL>
            <%If mclsContrproc.nNextmonthc > 0 Then%>
				<%=mobjValues.NumericControl("tcnextmonthc", 2, CStr(mclsContrproc.nNextmonthc),  , GetLocalResourceObject("tcnextmonthcToolTip"), True,  ,  ,  ,  ,  , True)%></TD>
			<%Else%>
				<%=mobjValues.NumericControl("tcnextmonthc", 2, CStr(Month(Session("dEffecdate"))),  , GetLocalResourceObject("tcnextmonthcToolTip"), True,  ,  ,  ,  ,  , True)%></TD>
			<%End If%>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("tcnextmonthcCaption") %></LABEL>
            <%If mclsContrproc.nNextmonthp > 0 Then%>
				<%=mobjValues.NumericControl("tcnNextmonthpa", 2, CStr(mclsContrproc.nNextmonthp),  , GetLocalResourceObject("tcnNextmonthpaToolTip"), True,  ,  ,  ,  ,  , True)%></TD>
			<%Else%>
				<%=mobjValues.NumericControl("tcnNextmonthpa", 2, CStr(Month(Session("dEffecdate"))),  , GetLocalResourceObject("tcnNextmonthpaToolTip"), True,  ,  ,  ,  ,  , True)%></TD>
			<%End If%>
        </TR>
        <TR>
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("tcnNextyearcCaption") %></LABEL>
            <%If mclsContrproc.nNextyearc > 0 Then%>
				<%=mobjValues.NumericControl("tcnNextyearc", 4, CStr(mclsContrproc.nNextyearc),  , GetLocalResourceObject("tcnNextyearcToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
			<%Else%>
				<%=mobjValues.NumericControl("tcnNextyearc", 4, CStr(Year(Session("dEffecdate"))),  , GetLocalResourceObject("tcnNextyearcToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
			<%End If%>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD COLSPAN="4"><LABEL><%= GetLocalResourceObject("tcnNextyearcCaption") %></LABEL>
            <%If mclsContrproc.nNextyearp > 0 Then%>
				<%=mobjValues.NumericControl("tcnNextyearpa", 4, CStr(mclsContrproc.nNextyearp),  , GetLocalResourceObject("tcnNextyearpaToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
			<%Else%>
				<%=mobjValues.NumericControl("tcnNextyearpa", 4, CStr(Year(Session("dEffecdate"))),  , GetLocalResourceObject("tcnNextyearpaToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
			<%End If%>
        </TR>        
    </TABLE>
    <%Response.Write(mobjValues.BeginPageButton)%>
</FORM>
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 2 $|$$Date: 27/03/06 19:34 $"     
</SCRIPT>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>OnChange_comm();</SCRIPT>")
%>






