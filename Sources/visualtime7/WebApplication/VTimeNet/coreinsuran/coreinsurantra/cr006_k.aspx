<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo de las rutinas genéricas
    Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues

    mobjValues.sCodisplPage = "cr006_k"
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>

<SCRIPT>
//% insCancel: 
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}
//% insFinish: Ejecuta la acción de Finalizar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: Habilita los campos de la forma según la acción a ejecutar
//-------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------    
	self.document.forms[0].optReinsurance[0].disabled = false;
	self.document.forms[0].optReinsurance[1].disabled = false;
	self.document.forms[0].optReinsurance[2].disabled = false;

	if(self.document.forms[0].optReinsurance[0].checked!=true && self.document.forms[0].optReinsurance[2].checked!=true){
		self.document.forms[0].tcnNumber.disabled  = false;
	}
	self.document.forms[0].cbeBranchRei.disabled = false;
	if(self.document.forms[0].optReinsurance[0].checked!=true){
		self.document.forms[0].cbeContraType.disabled = false;
		self.document.forms[0].btncbeContraType.disabled = false;
	}
	if(self.document.forms[0].optReinsurance[2].checked!=true){
		self.document.forms[0].tcnYearSer.disabled = false;
	}else{
		self.document.forms[0].tcnYearSer.disabled = true;
	}

	self.document.forms[0].cbeCompany.disabled = false;
	self.document.forms[0].btncbeCompany.disabled = false;

	if(self.document.forms[0].optReinsurance[2].checked!=true){
		self.document.forms[0].cbeBussiType.disabled = false;
	}else{
		self.document.forms[0].cbeBussiType.disabled = true;
	}

	self.document.forms[0].cbeCurrency.disabled = false;
	
	if(self.document.forms[0].tcnYearSer.value>0){
		self.document.forms[0].cbePerType.disabled = false;
		if (self.document.forms[0].cbePerType.value > 1 || self.document.forms[0].cbePerType.value){
			self.document.forms[0].tcnPerNum.disabled=false;
		}
	}

	self.document.forms[0].tcnIdConsec.disabled = false;
	self.document.forms[0].cbePerType.disabled = false;
}

// ChangeYearSer: Muestra el año completo (4 digitos)
//-------------------------------------------------------------------------------------------
function ChangeYearSer(Field){
//-------------------------------------------------------------------------------------------
	if (Field!=''){
		if(self.document.forms[0].optReinsurance[0].disabled==false){
			self.document.forms[0].tcnYearSer.value=getCompleteYear(Field);
			self.document.forms[0].cbePerType.disabled=false;
		}
	}
	else{
		//self.document.forms[0].cbePerType.disabled=true;
		//self.document.forms[0].tcnPerNum.disabled=true;
		//self.document.forms[0].cbePerType.value=5;
		//self.document.forms[0].tcnPerNum.value=1;
	}
}
//% getCompleteYear: Esta rutina se encarga de devolver el año completo (4 digitos) cuando se introduce incompleto (2 dígitos).
//----------------------------------------------------------------------------------------------------------------------------
function getCompleteYear(lstrValue){
//------------------------------------------------------------------------------------------------------------------------------
    var ldtmYear = new Date()
    var lintPos  
    var lstrYear
    var llngValue = 0
    do {
       lstrValue = lstrValue.replace(".","")
    }
    while (lstrValue != lstrValue.replace(".",""))
    if (lstrValue == '') llngValue = 0
    else llngValue = parseFloat(lstrValue)
    if (llngValue<1000){
        if (llngValue<=50)
            llngValue += 2000
        else
            if (llngValue<100)
                llngValue += 1900
            else
                llngValue += 2000
    }
    return "" + llngValue    
 }   
//ChangeReinsurance: Habilita/Desabilita según sea el tipo de reaseguro.
//-------------------------------------------------------------------------------------------
function ChangeReinsurance(Field,Disabled){
//-------------------------------------------------------------------------------------------
 	switch(Field)
	{

//Desabilita el campo codigo si se selecciona facultativo.
		case "1":
		{			
			self.document.forms[0].tcnNumber.value='';
			self.document.forms[0].tcnNumber.disabled=true;
			self.document.forms[0].tcnYearSer.value='';
			self.document.forms[0].tcnYearSer.disabled=false;
			ChangeYearSer(self.document.forms[0].tcnYearSer.value);
			self.document.forms[0].cbeBussiType.disabled=false;
			self.document.forms[0].tcnYearSer.value = '';
			self.document.forms[0].cbePerType.disabled = false;
			self.document.forms[0].cbeContraType.disabled=true;
			self.document.forms[0].btncbeContraType.disabled=true;
			self.document.forms[0].cbeContraType.value=4;
			$(self.document.forms[0].cbeContraType).change();
			//PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - INICIO
			//self.document.forms[0].tcnIdConsec.value = '';
			//self.document.forms[0].tcnIdConsec.disabled = false;
			//PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - FIN
			break;
		}		
//Proporcionales: Muestra los valores 
		case "2":
		{			
			self.document.forms[0].cbeContraType.value='';
			self.document.forms[0].cbeContraType.disabled=false;
			self.document.forms[0].btncbeContraType.disabled=false;
			self.document.forms[0].tcnNumber.value='';
			self.document.forms[0].tcnNumber.disabled=false;			
			self.document.forms[0].tcnYearSer.value='';
			self.document.forms[0].tcnYearSer.disabled=false;
			ChangeYearSer(self.document.forms[0].tcnYearSer.value);
			self.document.forms[0].cbeBussiType.disabled = false;
			self.document.forms[0].cbePerType.disabled = false;
			$(self.document.forms[0].cbeContraType).change();
			self.document.forms[0].elements['cbeContraType'].List = '2,3,5,6,7,8,9,10';
			//PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - INICIO
			//self.document.forms[0].tcnIdConsec.value = '';
			//self.document.forms[0].tcnIdConsec.disabled = false;
			//PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - FIN
			break;
		}			
//Desabilita los campos Año-Serie y Tipo de negocio si se selecciona no proporcional.
		case "3":
		{
			self.document.forms[0].cbeContraType.value='';
			self.document.forms[0].tcnYearSer.value='';
			self.document.forms[0].tcnYearSer.disabled=true;
			ChangeYearSer(self.document.forms[0].tcnYearSer.value);
			self.document.forms[0].cbeBussiType.value='0';
			self.document.forms[0].cbeBussiType.disabled=true;
			self.document.forms[0].cbePerType.disabled = true;
			self.document.forms[0].tcnPerNum.disabled = true;
			self.document.forms[0].tcnNumber.disabled=false;
			self.document.forms[0].tcnNumber.value='';
			$(self.document.forms[0].cbeContraType).change();
			self.document.forms[0].elements['cbeContraType'].List = '683,685,686,687,688';
			//PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - INICIO
			//self.document.forms[0].tcnIdConsec.value = '';
			//self.document.forms[0].tcnIdConsec.disabled = false;
			//PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - FIN
			break;
		}	
		default:
		{
			self.document.forms[0].tcnNumber.disabled=false;
			self.document.forms[0].tcnYearSer.disabled=false;
			self.document.forms[0].cbeBussiType.disabled=false;
			break;
		}	
	}
}

// PerType_Change: Cambia el valor del campo nPerNum según el tipo de período
//-------------------------------------------------------------------------------------------
function PerType_Change(Field){
//-------------------------------------------------------------------------------------------
 	switch(Field.value)
	{
		case "1":
		{			
			self.document.forms[0].tcnPerNum.value='1';
			self.document.forms[0].tcnPerNum.disabled=true;
			break;
		}			
		case "5":
		{
			self.document.forms[0].tcnPerNum.value='1';
			self.document.forms[0].tcnPerNum.disabled=true;
			break;
		}	
		default:
		{
			self.document.forms[0].tcnPerNum.value='';
			self.document.forms[0].tcnPerNum.disabled=false;
			break;
		}	
	}		
}
// ClearDescCompany: Limpia el DIV del control.
//-------------------------------------------------------------------------------------------
function ClearDescCompany(){
//-------------------------------------------------------------------------------------------
	if (self.document.forms[0].cbeCompany.value==""){
		UpdateDiv ("tctCompanyName","");
	}
}
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 2 $|$$Date: 2/05/06 9:37 $"
</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <%With Response
            .Write(mobjValues.StyleSheet() & vbCrLf)
            .Write(mobjMenu.MakeMenu("CR006_K", "CR006.aspx", 1, ""))
        End With
        mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="post" ID="FORM" NAME="frmTechAcc" ACTION="valCoReinsuranTra.aspx?sMode=1">
    <TABLE WIDTH="100%">
		<TR>                       
			<TD COLSPAN="1" CLASS="HighLighted"><A NAME="Reaseguro"><LABEL><A><%= GetLocalResourceObject("AnchorCaption") %></A></LABEL></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="4" CLASS="HighLighted"><A NAME="Reaseguro"><LABEL><A><%= GetLocalResourceObject("Anchor2Caption") %></A></LABEL></TD>
        </TR>        
        <TR>
		    <TD COLSPAN="1"><HR></TD>		    
		    <TD>&nbsp;</TD>
		    <TD COLSPAN="4"><HR></TD>
        </TR>      								            	
        <TR>
			<TD WIDTH="45%"><%=mobjValues.OptionControl(0, "optReinsurance", GetLocalResourceObject("optReinsurance_CStr1Caption"), "0", CStr(1), "ChangeReinsurance(this.value);", True, 1, GetLocalResourceObject("optReinsurance_CStr1ToolTip"))%></TD>	
			<TD WIDTH="5%">&nbsp;</TD>
			<TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("tcnNumberCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnNumber", 5,  ,  , GetLocalResourceObject("tcnNumberToolTip"),  ,  ,  ,  ,  ,  , True, 4)%></TD>
            <%--PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - INICIO--%>
            <TD WIDTH="10%"><LABEL>Id Consecutivo</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnIdConsec", 10, "",  , GetLocalResourceObject("tcnIdConsec"),  ,  ,  ,  ,  ,  , True, 11)%></TD>            
            <%--PRY-REASEGUROS VT - LEVANTAMIENTO DE AJUSTE DE CUENTAS TECNICAS  - LAMC - FIN--%>
		</TR>
        <TR>
			<TD WIDTH="45%"><%=mobjValues.OptionControl(0, "optReinsurance", GetLocalResourceObject("optReinsurance_CStr2Caption"), "1", CStr(2), "ChangeReinsurance(this.value);", True, 2, GetLocalResourceObject("optReinsurance_CStr2ToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><label><%= GetLocalResourceObject("cbeBranchReiCaption") %></LABEL></TD>
            <%mobjValues.BlankPosition = False%>
            <TD><%=mobjValues.PossiblesValues("cbeBranchRei", "table5000", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBranchReiToolTip"),  , 5)%></TD>
		</TR>
        <TR>
			<TD WIDTH="45%"><%=mobjValues.OptionControl(0, "optReinsurance", GetLocalResourceObject("optReinsurance_CStr3Caption"), "0", CStr(3), "ChangeReinsurance(this.value);", True, 3, GetLocalResourceObject("optReinsurance_CStr3ToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><label><%= GetLocalResourceObject("cbeContraTypeCaption") %></LABEL></TD>
			<%
                mobjValues.TypeList = CShort("1")
                mobjValues.TypeOrder = CShort("1")
                mobjValues.List = "2,3,5,6,7,8,9,10"
                mobjValues.BlankPosition = False
%>
			<TD><%=mobjValues.PossiblesValues("cbeContraType", "table173", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeContraTypeToolTip"), eFunctions.Values.eTypeCode.eNumeric, 6)%></TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnYearSerCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnYearSer", 4, "",  , GetLocalResourceObject("tcnYearSerToolTip"),  ,  ,  ,  ,  , "ChangeYearSer(this.value);", True, 7)%></TD>
		</TR>
        <TR>    
			<TD WIDTH="45%">&nbsp;</TD>
			<TD WIDTH="5%">&nbsp;</TD>
            <TD><label><%= GetLocalResourceObject("cbeCompanyCaption") %></LABEL></TD>
            <TD><%=mobjValues.CompanyControl("cbeCompany", "",  , GetLocalResourceObject("cbeCompanyToolTip"), "ClearDescCompany();", True, "tctCompanyName", False,  ,  ,  , 8)%></TD>
		</TR>            
	</TABLE>
	<TABLE WIDTH="100%">
		<TR>                       
			<TD COLSPAN="2" CLASS="HighLighted"><A NAME="Período"><LABEL><A><%= GetLocalResourceObject("Anchor3Caption") %></A></LABEL></TD>
			<TD WIDTH="5%">&nbsp;</TD>
			<TD COLSPAN="2" CLASS="HighLighted"><A NAME="Período"><LABEL><A><%= GetLocalResourceObject("Anchor4Caption") %></A></LABEL></TD>
        </TR>        
        <TR>
		    <TD COLSPAN="2"><HR></TD>
		    <TD WIDTH="5%">&nbsp;</TD>		    		    
		    <TD COLSPAN="2"><HR></TD>	
        </TR>      								            	
        <TR>
            <TD WIDTH="15%"><LABEL><%= GetLocalResourceObject("cbePerTypeCaption") %></label></TD>
            <TD WIDTH="30%"><%=mobjValues.PossiblesValues("cbePerType", "table97", eFunctions.Values.eValuesType.clngComboType, "5",  ,  ,  ,  ,  , "PerType_Change(this)", True,  , GetLocalResourceObject("cbePerTypeToolTip"),  , 9)%></TD>
            <TD WIDTH="5%">&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("cbeBussiTypeCaption") %></LABEL></TD>
            <TD><%
                    mobjValues.BlankPosition = False
                    Response.Write(mobjValues.PossiblesValues("cbeBussiType", "table20", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBussiTypeToolTip"),  , 10))
%>
		    </TD>
        </TR>    
        <TR>
            <TD WIDTH="15%"><LABEL><%= GetLocalResourceObject("tcnPerNumCaption") %></LABEL></TD>
            <TD WIDTH="30%"><%=mobjValues.NumericControl("tcnPerNum", 2, "1",  , GetLocalResourceObject("tcnPerNumToolTip"),  ,  ,  ,  ,  ,  , True, 11)%></TD>
            <TD WIDTH="5%">&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 12)%></TD>
        </TR>
	</TABLE>
<SCRIPT>
	self.document.forms[0].btncbeCompany.disabled = true;
</SCRIPT>
</FORM>
</BODY>
</HTML>





