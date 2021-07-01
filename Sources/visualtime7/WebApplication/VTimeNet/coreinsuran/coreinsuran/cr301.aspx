<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 11.58.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable modular utilizada para la carga y actualización de datos de la forma
    Dim mclsContrproc As eCoReinsuran.Contrproc

'- Se define la variable modular utilizada para la carga de los datos de contrmaster
    Dim mclsContrMaster As eCoReinsuran.Contrmaster
    
    Dim mclsContrprocQS As eCoReinsuran.Contrproc


'% insPreCR301: Realiza la lectura para la carga de los datos de la forma
'------------------------------------------------------------------------------------------------
    Private Sub insPreCR301()
        '------------------------------------------------------------------------------------------------	
        Call mclsContrproc.insPreCR301(CInt(Request.QueryString.Item("nMainAction")), Session("nNumber"), Session("nType"), Session("nBranch_rei"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	
        Call mclsContrMaster.Find(mclsContrproc.lintType_rel, Session("nNumber"), 0, 0, eRemoteDB.Constants.dtmNull)
	
        Call mclsContrproc.defaulValuesCR301(mclsContrproc.sCumulpol)
	
        '+cuando el contrato es diferente a retencion.    
        If Session("nType") <> 1 Then
		
            '+ Se busca la moneda asociado al contrato de retencion.
            Call mclsContrMaster.Find_Type(mclsContrproc.lintType_rel, 1, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nBranch_Rei"))
            
        End If
                
        '+cuando el contrato es diferente a retencion y cuota parte.    
        If Session("nType") > 2 Then
            If (mclsContrproc.nQuota_sha = 0 Or mclsContrproc.nQuota_sha = eRemoteDB.Constants.intNull) Or (mclsContrproc.nAmount = 0 Or mclsContrproc.nAmount = eRemoteDB.Constants.intNull) Then
                If mclsContrprocQS.Find(eRemoteDB.Constants.intNull, 2, Session("nBranch_rei"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
                    If mclsContrproc.nQuota_sha = 0 Or mclsContrproc.nQuota_sha = eRemoteDB.Constants.intNull Then
                        mclsContrproc.nQuota_sha = mclsContrprocQS.nQuota_sha
                    End If
                
                    If mclsContrproc.nAmount = 0 Or mclsContrproc.nAmount = eRemoteDB.Constants.intNull Then
                        mclsContrproc.nAmount = mclsContrprocQS.nAmount
                    End If
                End If
            End If
        End If
	
    End Sub

</script>
<%  Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
mobjValues = New eFunctions.Values
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
mobjMenu = New eFunctions.Menues
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
    mclsContrproc = New eCoReinsuran.Contrproc
    mclsContrMaster = New eCoReinsuran.Contrmaster
    mclsContrprocQS = New eCoReinsuran.Contrproc

mobjValues.ActionQuery = Session("bQuery")
mobjValues.sCodisplPage = "CR301"

Call insPreCR301()

%>
<SCRIPT>

var nType=<%=Session("nType")%>

//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 29/03/06 10:11 $"

// onChangeRetCover: Habilita los campos de rehabilitación según la selección 
// en Retención por cobertura
//-----------------------------------------------------------------------------------------
function onChangeRetCover(){
//-----------------------------------------------------------------------------------------
    if (self.document.forms[0].chkRetCover.checked == true) 
        {
        self.document.forms[0].chkRetZone.disabled = true;
	    self.document.forms[0].tcnReten.disabled = true;
	    self.document.forms[0].tcnReten.value='';
	    }
	else
	    {
	    self.document.forms[0].chkRetZone.disabled = false;
	    if (!self.document.forms[0].chkRetZone.checked == true)
			self.document.forms[0].tcnReten.disabled = false;
	    }
}
 
// onChangeRetZone: Habilita los campos de reabilitación según la selección 
// en Retención por Zona
//-----------------------------------------------------------------------------------------
function onChangeRetZone(){
//-----------------------------------------------------------------------------------------
    if (self.document.forms[0].chkRetZone.checked == true) 
        {
        self.document.forms[0].chkRetCover.disabled = true;
	    self.document.forms[0].tcnReten.disabled = true;
	    self.document.forms[0].tcnReten.value='';
	    }
	else
	    {
	    self.document.forms[0].chkRetCover.disabled = false;
	    if (!self.document.forms[0].chkRetCover.checked == true)
			self.document.forms[0].tcnReten.disabled = false;
	    }
}

// onChangeLimcover: Habilita los campos segun los cambios en Limites por Coberture
//-----------------------------------------------------------------------------------------
function onChangeLimCover(){
//-----------------------------------------------------------------------------------------
    if (self.document.forms[0].ChkLimCover.checked==true) 
		switch(nType)
		{
			case(2):
			{
				self.document.forms[0].tcnQuota_sha.value='';
				self.document.forms[0].tcnAmount.value='';
				self.document.forms[0].tcnQuota_sha.disabled=true;
				self.document.forms[0].tcnAmount.disabled=true;
				break;				
			}
			case(3):
			{
				self.document.forms[0].tcnQuota_sha.value='';
				self.document.forms[0].tcnAmount.value='';
				self.document.forms[0].tcnQuota_sha.disabled=true;
				self.document.forms[0].tcnAmount.disabled=true;
				break;				
			}
			case(5):
			{
				self.document.forms[0].tcnLines.value='';
				self.document.forms[0].tcnLines.disabled=true;
				break;
			}
			case(6):
			{
				self.document.forms[0].tcnLines.value='';
				self.document.forms[0].tcnLines.disabled=true;
				break;				
			}
			case(7):
			{
				self.document.forms[0].tcnLines.value='';
				self.document.forms[0].tcnLines.disabled=true;
				break;				
			}
			case(8):
			{
				self.document.forms[0].tcnLines.value='';
				self.document.forms[0].tcnLines.disabled=true;
				break;				
			}
			case(9):
			{
		   	    self.document.forms[0].tcnReten_min.value='';
			    self.document.forms[0].tcnMax_even.value='';
		   	    self.document.forms[0].tcnReten_min.disabled=true;
			    self.document.forms[0].tcnMax_even.disabled=true;
			    break;
			}
			case(10):
			{
		   	    self.document.forms[0].tcnReten_min.value='';
			    self.document.forms[0].tcnMax_even.value='';
		   	    self.document.forms[0].tcnReten_min.disabled=true;
			    self.document.forms[0].tcnMax_even.disabled=true;
			    break;
			}
			
		}
	else			
		switch(nType)
		{
			case(2):
			{
				self.document.forms[0].tcnQuota_sha.disabled=false;
				if (!self.document.forms[0].chkRetCover.checked && !self.document.forms[0].chkRetZone.checked)
					self.document.forms[0].tcnAmount.disabled=false;
				break;				
			}
			case(3):
			{
				self.document.forms[0].tcnQuota_sha.disabled=false;
				if (!self.document.forms[0].chkRetCover.checked && !self.document.forms[0].chkRetZone.checked)
					self.document.forms[0].tcnAmount.disabled=false;
				break;				
			}
			case(5):
			{
				self.document.forms[0].tcnLines.disabled=false;
				break;
			}
			case(6):
			{
				self.document.forms[0].tcnLines.disabled=false;
				break;				
			}
			case(7):
			{
				self.document.forms[0].tcnLines.disabled=false;
				break;				
			}
			case(8):
			{				
				self.document.forms[0].tcnLines.disabled=false;
				break;				
			}
			case(9):
			{
		   	    self.document.forms[0].tcnReten_min.disabled=false;
			    self.document.forms[0].tcnMax_even.disabled=false;
			    break;
			}
			case(10):
			{
		   	    self.document.forms[0].tcnReten_min.disabled=false;
			    self.document.forms[0].tcnMax_even.disabled=false;
			    break;
			}
			
		}
}

// onChangeCumulo: Habilita los campos segun los cambios en Tipo de cumulo
//-----------------------------------------------------------------------------------------
function onChangeCumulo(){
//-----------------------------------------------------------------------------------------
    if (self.document.forms[0].cbeCumulo.value == 4)
    {
        self.document.forms[0].cbeMethod.value = 0;
        self.document.forms[0].cbeMethod.disabled = true;         
        self.document.forms[0].OptCumulpol[0].disabled = true;	
		self.document.forms[0].OptCumulpol[1].disabled = true;	
		self.document.forms[0].OptCumulpol[2].disabled = true;
    }
    else
	{
		self.document.forms[0].cbeMethod.disabled = false
		self.document.forms[0].OptCumulpol[0].disabled = false;	
		self.document.forms[0].OptCumulpol[1].disabled = false;	
		self.document.forms[0].OptCumulpol[2].disabled = false;
    }
}

// EnabledFields: Habilita los campos de acuerdo al tipo de contrato y la acción
//--------------------------------------------------------------------------------
function EnabledFields(nAction,nType){
//--------------------------------------------------------------------------------
	onChangeLimCover();   		
	if (nAction==301 || nAction==302)
		if (nType ==1)
		{
			onChangeRetZone();	
			onChangeRetCover();
		}	
		else
			onChangeCumulo();				
}
  // PRY-REASEGUROS VT - CALCULO DE PORCENTAJE CEDIDO  - RAOD - INI
// insCalAmount: Calcula el porcentaje cedido y/o el importe límite 

//-----------------------------------------------------------------------------------------
function insCalAmount(Field){  
 //-----------------------------------------------------------------------------------------
    var nQuota_sha //--% CEDIDO
    var nAmount // --MONTO LIMITE
    var nReten 	//--MONTO DE RETENCION
    
    nQuota_sha = insConvertNumber(self.document.forms[0].tcnQuota_sha.value);
    nAmount    = insConvertNumber(self.document.forms[0].tcnAmount.value);
    nReten     = insConvertNumber(self.document.forms[0].tcnReten.value);
	
    if (Field.value!=0 && !isNaN(nReten)){
        if (Field.name=='tcnQuota_sha'){
            self.document.forms[0].tcnQuota_sha.value = VTFormat(nQuota_sha, '', '', '', 0, true);		   
            self.document.forms[0].tcnAmount.value = VTFormat((nReten * nQuota_sha) / (100 - nQuota_sha) ,'', '', '', 0, true);
        }
            /*
			if (!ValNumber(self.document.forms[0].tcnAmount,".","'","false",0))
			    self.document.forms[0].tcnQuota_sha.value='';
			//alert('quota valida ValNumber de nAmount => ' + nAmount)
			if (nQuota_sha > 100){
			    alert('El porcentaje de cesion no puede ser superior a 100%');
			    self.document.forms[0].tcnQuota_sha.value='';
			    self.document.forms[0].tcnAmount.value='';
			}*/
	   
        else{
            if (nReten != 0 ) {
                self.document.forms[0].tcnQuota_sha.value = VTFormat(((nAmount * 100)/(nAmount + nReten)), '', '', '', 6, true);
                //nQuota_sha = insConvertNumber((nAmount * 100)/(nAmount + nReten));
                //self.document.forms[0].tcnQuota_sha.value = VTFormat(nQuota_sha, '', '', '', 6, true);;
            } 		           		 
            /*
			if(!ValNumber(self.document.forms[0].tcnQuota_sha,".",",","false",6))
				self.document.forms[0].tcnAmount.value = '';

            
            if(self.document.forms[0].tcnQuota_sha.value > 100){
                alert('El porcentaje de cesion no puede ser superior a 100%');
			    self.document.forms[0].tcnQuota_sha.value='';
			    self.document.forms[0].tcnAmount.value='';
		    }*/

        }
    }
}		
    // PRY-REASEGUROS VT - CALCULO DE PORCENTAJE CEDIDO  - RAOD - FIN
</SCRIPT>





<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "CR301", "CR301.aspx"))
	.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<FORM METHOD="post" ID="FORM" NAME="frmCR301" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%=mobjValues.ShowWindowsName("CR301")%>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="100%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=100590><A NAME="Cuota"><%= GetLocalResourceObject("AnchorCuotaCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><HR></TD>
        </TR>
    </TABLE>
    
    <TABLE WIDTH="100%" BORDER ="0">
        <TR>
            <TD WIDTH="20%"><LABEL ID=100589><%= GetLocalResourceObject("tcdStartdateCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.DateControl("tcdStartdate", CStr(mclsContrMaster.dStartdate),  , GetLocalResourceObject("tcdStartdateToolTip"),  ,  ,  ,  , True)%></TD>
			<TD WIDTH="10%">&nbsp;</TD>
			<TD WIDTH="20%"><LABEL ID=100588><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
          	<TD WIDTH="20%">
  				<%If Session("nType") = 1 Then%>
					<%=mobjValues.PossiblesValues("cbeCurrency", "table11", 1, CStr(mclsContrMaster.nCurrency),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeCurrencyToolTip"))%>
				<%Else%>
					<%=mobjValues.PossiblesValues("cbeCurrency", "table11", 1, CStr(mclsContrMaster.nCurrency),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%>
				<%End If%>			
			</TD>
        </TR>
        <TR>
            <TD WIDTH="20%"><LABEL ID=100589><%= GetLocalResourceObject("tcdExpiredateCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.DateControl("tcdExpiredate", CStr(mclsContrMaster.dExpirdat),  , GetLocalResourceObject("tcdExpiredateToolTip"),  ,  ,  ,  , False)%></TD>
            <TD WIDTH="10%">&nbsp;</TD>
            <TD WIDTH="20%"><LABEL ID=100589><%= GetLocalResourceObject("tcnInterestCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.NumericControl("tcnInterest", 4, CStr(mclsContrproc.nInterest),  , GetLocalResourceObject("tcnInterestToolTip"), True, 2)%></TD>
        </TR>
        <TR>
            <TD WIDTH="29%"><%=mobjValues.CheckControl("ChkLimCover", GetLocalResourceObject("ChkLimCoverCaption"), mclsContrproc.sLimitcov,  , "onChangeLimCover()", CStr(Session("nType")) = "1",  , GetLocalResourceObject("ChkLimCoverToolTip"))%></TD>
            <TD WIDTH="20%">&nbsp;</TD>
            <TD WIDTH="10%" COLSPAN="4">&nbsp;</TD>
		</TR>
        <TR>
			<TD WIDTH="40%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=100590><A NAME="Cuota"><%= GetLocalResourceObject("AnchorCuota2Caption") %></A></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD WIDTH="40%"COLSPAN="4" CLASS="HighLighted"><LABEL ID=100591><A NAME="Excedentes"><%= GetLocalResourceObject("AnchorExcedentesCaption") %></A></LABEL></TD>
		</TR>
		<TR>
			<TD WIDTH="40%" COLSPAN="2" ><HR></TD>
			<TD WIDTH="10%">&nbsp;</TD>
			<TD WIDTH="20%" COLSPAN="2" ><HR></TD>
        </TR>
        <TR>
			<%If CStr(Session("nType")) = "1" Then%>
				<TD WIDTH="20%"><%=mobjValues.CheckControl("chkRetCover", GetLocalResourceObject("chkRetCoverCaption"), mclsContrproc.sRetcover, CStr(1), "onChangeRetCover()", CStr(Session("nType")) <> "1",  , GetLocalResourceObject("chkRetCoverToolTip"))%></TD>
			<%Else%>
				<TD WIDTH="20%"><%=mobjValues.CheckControl("chkRetCover", GetLocalResourceObject("chkRetCoverCaption"), Session("sRetCover"), CStr(1), "onChangeRetCover()", CStr(Session("nType")) <> "1",  , GetLocalResourceObject("chkRetCoverToolTip"))%></TD>
			<%End If%>		
			
			
			<TD WIDTH="1%">&nbsp;</TD>	            
			<TD WIDTH="10%">&nbsp;</TD>
			<TD WIDTH="20%"><LABEL ID=100592><%= GetLocalResourceObject("tcnQuota_shaCaption") %></LABEL></TD>			
			<TD WIDTH="20%"><%=mobjValues.NumericControl("tcnQuota_sha", 9, CStr(mclsContrproc.nQuota_sha),  , GetLocalResourceObject("tcnQuota_shaToolTip"),  , 6,  ,  ,  , "insCalAmount(this)", CStr(Session("nType")) <> "2" And CStr(Session("nType")) <> "3")%></TD>
		</TR>
		<TR>
			<%If CStr(Session("nType")) = "1" Then%>
				<TD WIDTH="20%"><%=mobjValues.CheckControl("chkRetZone", GetLocalResourceObject("chkRetZoneCaption"), mclsContrproc.sRetzone, CStr(1), "onChangeRetZone()", CStr(Session("nType")) <> "1",  , GetLocalResourceObject("chkRetZoneToolTip"))%></TD>
			<%Else%>		
				<TD WIDTH="20%"><%=mobjValues.CheckControl("chkRetZone", GetLocalResourceObject("chkRetZoneCaption"), Session("sRetZone"), CStr(1), "onChangeRetZone()", CStr(Session("nType")) <> "1",  , GetLocalResourceObject("chkRetZoneToolTip"))%></TD>
			<%End If%>							
			<TD WIDTH="1%">&nbsp;</TD>
			<TD WIDTH="10%">&nbsp;</TD>
			<TD WIDTH="20%"><LABEL ID=100594><%= GetLocalResourceObject("tcnAmountCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.NumericControl("tcnAmount", 19, CStr(mclsContrproc.nAmount),  , GetLocalResourceObject("tcnAmountToolTip"), True, 6,  ,  ,  , "insCalAmount(this)", CStr(Session("nType")) = "1" Or CStr(Session("sRetZone")) = "1" Or CStr(Session("sRetCover")) = "1")%></TD>
		</TR>	
		<TR>
			<TD WIDTH="20%"><LABEL ID=100588><%= GetLocalResourceObject("tcnRetenCaption") %></LABEL></TD>  	
            <TD WIDTH="20%">
				<%' no borrar-->>>=mobjvalues.NumericControl("tcnReten",15,3500,, GetLocalResourceObject("tcnRetenToolTip"),True,2,,,,,Session("nType")<>"1")%>
				<%If Session("nType") = 1 Then%>	
					<%	If mclsContrproc.nRetention = eRemoteDB.Constants.intNull Then%>	
						<%=mobjValues.NumericControl("tcnReten", 19, CStr(0),  , GetLocalResourceObject("tcnRetenToolTip"), True, 6,  ,  ,  ,  , CStr(Session("nType")) <> "1")%>
					<%	Else%>	
						<%=mobjValues.NumericControl("tcnReten", 19, CStr(mclsContrproc.nRetention),  , GetLocalResourceObject("tcnRetenToolTip"), True, 6,  ,  ,  ,  , CStr(Session("nType")) <> "1")%>
					<%	End If%>	
				<%Else%>
					<%=mobjValues.NumericControl("tcnReten", 19, Session("dblRetention"),  , GetLocalResourceObject("tcnRetenToolTip"), True, 6,  ,  ,  ,  , CStr(Session("nType")) <> "1")%>
				<%End If%>
			</TD>
			<TD WIDTH="10%">&nbsp;</TD>
			<TD WIDTH="20%"></TD>
			<TD WIDTH="20%"></TD>
		</TR>
		<TR>
			<TD WIDTH="20%"><LABEL ID=LABEL1><%= GetLocalResourceObject("tcnMaxRetAmountCaption")%></LABEL></TD>  	
            <TD WIDTH="20%">
				<%' no borrar-->>>=mobjvalues.NumericControl("tcnReten",15,3500,, GetLocalResourceObject("tcnRetenToolTip"),True,2,,,,,Session("nType")<>"1")%>
				<%If Session("nType") = 1 Then%>	
					<% If mclsContrproc.nMaxRetAmount = eRemoteDB.Constants.intNull Then%>	
						<%=mobjValues.NumericControl("tcnMaxRetAmount", 19, CStr(0), , GetLocalResourceObject("tcnMaxRetAmountToolTip"), True, 6, , , , , CStr(Session("nType")) <> "1")%>
					<%	Else%>	
						<%=mobjValues.NumericControl("tcnMaxRetAmount", 19, CStr(mclsContrproc.nMaxRetAmount), , GetLocalResourceObject("tcnRetenToolTip"), True, 6, , , , , CStr(Session("nType")) <> "1")%>
					<%	End If%>	
				<%Else%>
                    <%=mobjValues.NumericControl("tcnMaxRetAmount", 19, CStr(0), , GetLocalResourceObject("tcnMaxRetAmountToolTip"), True, 6, , , , , CStr(Session("nType")) <> "1")%>
				<%End If%>
			</TD>
			<TD WIDTH="10%">&nbsp;</TD>
			<TD WIDTH="20%"></TD>
			<TD WIDTH="20%"></TD>
		</TR>        	
		<TR>                       
			<TD WIDTH="40%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=100590><A NAME="Cuota"><%= GetLocalResourceObject("AnchorCuota3Caption") %></A></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD WIDTH="40%" COLSPAN="4" CLASS="HighLighted"><LABEL ID=100591><A NAME="Excedentes"><%= GetLocalResourceObject("AnchorExcedentes2Caption") %></A></LABEL></TD>
        </TR>        
        <TR>
		    <TD COLSPAN="2"><HR></TD>		    
		    <TD WIDTH="10%">&nbsp;</TD>
		    <TD COLSPAN="4"><HR></TD>
        </TR>      								
		<TR>	
			<TD WIDTH="20%"><LABEL ID=100593><%= GetLocalResourceObject("tcnLinesCaption") %></LABEL></TD>
			<TD WIDTH="20%"><%=mobjValues.NumericControl("tcnLines", 5, CStr(mclsContrproc.nLines),  , GetLocalResourceObject("tcnLinesToolTip"),  , 2,  ,  ,  ,  , CStr(Session("nType")) <> "5" And CStr(Session("nType")) <> "6" And CStr(Session("nType")) <> "7" And CStr(Session("nType")) <> "8")%></TD>
			<TD WIDTH="10%">&nbsp;</TD>
			<TD WIDTH="20%"><LABEL ID=100596><%= GetLocalResourceObject("tcnReten_minCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.NumericControl("tcnReten_min", 19, CStr(mclsContrproc.nReten_min),  , GetLocalResourceObject("tcnReten_minToolTip"), True, 6,  ,  ,  ,  , CStr(Session("nType")) <> "9" And CStr(Session("nType")) <> "10")%></TD>
		</TR>
		<TR>
			<TD WIDTH="20%">&nbsp;</TD>	
			<TD WIDTH="20%">&nbsp;</TD>
		    <TD WIDTH="10%">&nbsp;</TD>
            <TD WIDTH="20%"><LABEL ID=100597><%= GetLocalResourceObject("tcnMax_evenCaption") %></LABEL></TD> 
            <TD WIDTH="20%"><%=mobjValues.NumericControl("tcnMax_even", 19, CStr(mclsContrproc.nMax_even),  , GetLocalResourceObject("tcnMax_evenToolTip"), True, 6,  ,  ,  ,  , CStr(Session("nType")) <> "9" And CStr(Session("nType")) <> "10")%></TD>            
        </TR>
        <TR>                       
			<TD WIDTH="40%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=100590><A NAME="Cuota"><%= GetLocalResourceObject("AnchorCuota4Caption") %></A></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD WIDTH="40%" COLSPAN="4" CLASS="HighLighted"><LABEL ID=100591><A NAME="Excedentes"><%= GetLocalResourceObject("AnchorExcedentes3Caption") %></A></LABEL></TD>
        </TR>        
        <TR>
		    <TD COLSPAN="2"><HR></TD>		    
		    <TD WIDTH="10%">&nbsp;</TD>
		    <TD COLSPAN="4"><HR></TD>
        </TR>      								
		<TR>	
			<TD WIDTH="20%"><LABEL ID=100593><%= GetLocalResourceObject("cbeCumuloCaption") %></LABEL></TD>
			<TD WIDTH="20%"><%=mobjValues.PossiblesValues("cbeCumulo", "table79", eFunctions.Values.eValuesType.clngComboType, mclsContrproc.sCumultyp,  ,  ,  ,  ,  , "onChangeCumulo()", False,  , GetLocalResourceObject("cbeCumuloToolTip"))%></TD>
			<TD WIDTH="10%">&nbsp;</TD>
			<TD WIDTH="20%"><%=mobjValues.OptionControl(1, "OptCumulpol", GetLocalResourceObject("OptCumulpol_1Caption"), CStr(mclsContrproc.nOptCumulpol_1), "1",  ,  ,  , GetLocalResourceObject("OptCumulpol_1ToolTip"))%></TD>
            <TD WIDTH="20%">&nbsp;</TD>
		</TR>
		<TR>
			<TD WIDTH="20%"><LABEL ID=100593><%= GetLocalResourceObject("cbeMethodCaption") %></LABEL></TD>	
			<TD WIDTH="20%"><%=mobjValues.PossiblesValues("cbeMethod", "table90", eFunctions.Values.eValuesType.clngComboType, mclsContrproc.sCumreint,  ,  ,  ,  ,  ,  , CStr(Session("nType")) <> "1",  , GetLocalResourceObject("cbeMethodToolTip"))%></TD>
		    <TD WIDTH="10%">&nbsp;</TD>
            <TD WIDTH="20%"><%=mobjValues.OptionControl(1, "OptCumulpol", GetLocalResourceObject("OptCumulpol_2Caption"), CStr(mclsContrproc.nOptCumulpol_2), "2",  ,  ,  , GetLocalResourceObject("OptCumulpol_2ToolTip"))%></TD>
            <TD WIDTH="20%">&nbsp;</TD>            
        </TR>
        <TR>
			<TD WIDTH="10%">&nbsp;</TD>	
			<TD WIDTH="20%">&nbsp;</TD>
		    <TD WIDTH="10%">&nbsp;</TD>
            <TD WIDTH="15%"><%=mobjValues.OptionControl(1, "OptCumulpol", GetLocalResourceObject("OptCumulpol_3Caption"), CStr(mclsContrproc.nOptCumulpol_3), "3",  ,  ,  , GetLocalResourceObject("OptCumulpol_3ToolTip"))%></TD>
              
        </TR>
	</TABLE>
<%
mclsContrproc = Nothing
mclsContrMaster = Nothing%>
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 3 $|$$Date: 29/03/06 10:11 $"     
</SCRIPT>      
</FORM>
</BODY>
</HTML>
<%
If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	Response.Write("<SCRIPT>EnabledFields(" & Request.QueryString.Item("nMainAction") & "," & Session("nType") & ");</SCRIPT>")
End If
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 11.58.23
Call mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>
<%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then%>
<SCRIPT>
//+ Si el contrato de retencion es por covertura habilita el monto limite
    if (self.document.forms[0].chkRetCover.checked == true)
        //self.document.forms[0].tcnAmount.disabled=false;
</SCRIPT>
<%End If%>





