<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de los datos del cliente	
Dim mobjclient_evalrisk As eClient.Client_evalrisk

'-Variables para manejar el option de fumador
Dim loptNoInfo As Object '3
Dim loptSmoker As Object '1
Dim loptNoSmoker As Object '2


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjclient_evalrisk = New eClient.Client_evalrisk

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If

With mobjclient_evalrisk
	'If .InsPreBC001(Session("sClient")) Then
	'If .Find(Session("sClient"), Session("dEffecdate")) Then
	If .Find(Session("sClient"), Today) Then
	End If
End With
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




	<%=mobjValues.StyleSheet()%>
<SCRIPT>
//+ Variable para el control de versiones
		document.VssVersion="$$Revision: 4 $|$$Date: 20/01/04 11:44 $"
		
//% CancelErrors: se controla la acción Cancelar 
//---------------------------------------------------------------------------------------------------
	function CancelErrors(){
//---------------------------------------------------------------------------------------------------
	self.history.back
}
	
//% insEnabledFields: Habilita o deshabilita los campos de la ventana, dependiendo si están
//%					  llenos o no. ACM - 31/07/2001.	
//---------------------------------------------------------------------------------------------------
function insEnabledFields(){
//---------------------------------------------------------------------------------------------------
	with (self.document.forms[0]) {
//+ Fecha de Ingreso
		if(elements["tcdInpDate"].value=="")
			elements["tcdInpDate"].disabled=false
		else
			elements["tcdInpDate"].disabled=true;

//+ Apellido Paterno
		if(elements["tctLastName"].value=="")
			elements["tctLastName"].disabled=false
		else
			elements["tctLastName"].disabled=true;

//+ Apellido Materno
		if(elements["tctLastName2"].value=="")
			elements["tctLastName2"].disabled=false
		else
			elements["tctLastName2"].disabled=true;
		
//+ Nombres
		if(elements["tctFirstName"].value=="")
			elements["tctFirstName"].disabled=false
		else
			elements["tctFirstName"].disabled=true;
			
//+ Fecha de Nacimiento
		if(elements["tcdBirthDate"].value=="")
			elements["tcdBirthDate"].disabled=false
		else
			elements["tcdBirthDate"].disabled=true;		

//+ Estado Civil
		if(elements["cbeCivilsta"].value=="")
			elements["cbeCivilsta"].disabled=false
		else
			elements["cbeCivilsta"].disabled=true;
			
//+ Sexo
		if(elements["cbeSex"].value=="")
			elements["cbeSex"].disabled=false
		else
			elements["cbeSex"].disabled=true;

//+ Nacionalidad
		if(elements["cbeNationality"].value=="")
			elements["cbeNationality"].disabled=false
		else
			elements["cbeNationality"].disabled=true;
			
//+ Actividad Laboral
		if(elements["cbeOccupat"].value=="")
			elements["cbeOccupat"].disabled=false
		else
			elements["cbeOccupat"].disabled=true;
			
//+ Rubro Económico
		if(elements["cbeArea"].value=="")
			elements["cbeArea"].disabled=false
		else
			elements["cbeArea"].disabled=true;		

//+ Profesión
		if(elements["cbeTitle"].value=="")
			elements["cbeTitle"].disabled=false
		else
			elements["cbeTitle"].disabled=true;

//+ Fecha de Otorgamiento
		if(elements["tcdDriverDat"].value=="")
			elements["tcdDriverDat"].disabled=false
		else
			elements["tcdDriverDat"].disabled=true;
			
//+ Número de la Licencia
		if(elements["tctDriverNum"].value=="")
			elements["tctDriverNum"].disabled=false
		else
			elements["tctDriverNum"].disabled=true;
					
//+ Fecha de Término
		if(elements["tcdDrivExpDat"].value=="")
			elements["tcdDrivExpDat"].disabled=false
		else
			elements["tcdDrivExpDat"].disabled=true;
			
//+ Clase
		if(elements["cbeTypDriver"].value=="")
			elements["cbeTypDriver"].disabled=false
		else
			elements["cbeTypDriver"].disabled=true;
			
//+ Restricciones
		if(elements["cbeLimitDriv"].value=="")
			elements["cbeLimitDriv"].disabled=false
		else
			elements["cbeLimitDriv"].disabled=true;		
					
//+ Bloqueado
		if(elements["chkBlockade"].value==1)
			elements["chkBlockade"].disabled=false
		else
			elements["chkBlockade"].disabled=true;

//+ Dependiente
		if(elements["chkDependant"].value==1)
			elements["chkDependant"].disabled=false
		else
			elements["chkDependant"].disabled=true;

//+ Fecha de Defunción
		if(elements["tcdDeathdate"].value=="")
			elements["tcdDeathdate"].disabled=false
		else
			elements["tcdDeathdate"].disabled=true;

//+ Institución de salud
		if(elements["cbeHealth_Org"].value=="")
			elements["cbeHealth_Org"].disabled=false
		else
			elements["cbeHealth_Org"].disabled=true;

//+ AFP
		if(elements["cbeAfp"].value=="")
			elements["cbeAfp"].disabled=false
		else
			elements["cbeAfp"].disabled=true;

//+ Fecha de Matrimonio
		if(elements["tcdWedd"].value=="")
			elements["tcdWedd"].disabled=false
		else
			elements["tcdWedd"].disabled=true;
			
//+ Indicador de Factura
		if(elements["chkBill_Ind"].value=="")
			elements["chkBill_Ind"].disabled=false
		else
			elements["chkBill_Ind"].disabled=true;		
	}		
}
</SCRIPT>

    <%Response.Write(mobjMenu.setZone(2, "BC9000", "BC9000.aspx"))
mobjMenu = Nothing%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmBC9000" ACTION="valClientSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <A NAME="BeginPage"></A>
<!--    
    <P ALIGN="Center">
	    <LABEL><A HREF="#Nombre"><%= GetLocalResourceObject("AnchorNombreCaption") %></A></LABEL><LABEL> | </LABEL>
		<LABEL><A HREF="#Licencia"><%= GetLocalResourceObject("AnchorLicenciaCaption") %></A></LABEL><LABEL> | </LABEL>
		<LABEL><A HREF="#Control"><%= GetLocalResourceObject("AnchorControlCaption") %></A></LABEL>
    </P>
-->    
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
    <br />
	<TABLE WIDTH="100%">
	    <TR>
	        <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Nombre"><%= GetLocalResourceObject("AnchorNombre2Caption") %></A></LABEL></TD>
	    </TR>
	    <TR>	        
	        <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
	    </TR>

	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeCountryCaption") %></LABEL></TD>
            <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeCountry", "table66", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nCountry),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCountryToolTip"))%> </TD>
	    </TR>
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tcdOtherDateCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.DateControl("tcdOtherDate", mobjValues.TypeToString(mobjclient_evalrisk.dOtherdate, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdOtherDateToolTip"))%> </TD>
	    </TR>
	    <TR>
	        <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Nombre"><%= GetLocalResourceObject("AnchorNombre3Caption") %></A></LABEL></TD>
	    </TR>
	    <TR>	        
	        <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
	    </TR>
	    
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeSinceYearCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeSinceYear", "table9010", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nSinceYear),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSinceYearToolTip"))%> </TD
	    </TR>
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tcnNumEmployersCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.NumericControl("tcnNumEmployers", 4, CStr(mobjclient_evalrisk.nNumEmployers),  , GetLocalResourceObject("tcnNumEmployersToolTip"))%> </TD>
	    </TR>

	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeCntryRiskCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeCntryRisk", "table9011", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nCntryRisk),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCntryRiskToolTip"))%> </TD>
	    </TR>
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeBranchCiaCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeBranchCia", "table9031", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nBranchCia),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeTypeCiaCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeTypeCia", "table9012", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nTypeCia),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeTypeProductCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeTypeProduct", "table9013", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nTypeProduct),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>

	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeRiskCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeRisk", "table9014", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nRisk),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeActBusCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeActBus", "table9015", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nActBus),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	    

	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tctNote1Caption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.TextControl("tctNote1", 60, mobjclient_evalrisk.sNote1, True, "")%> </TD>
	    </TR>
	    <TR>
	        <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Nombre"><%= GetLocalResourceObject("AnchorNombre4Caption") %></A></LABEL></TD>
	    </TR>
	    <TR>	        
	        <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
	    </TR>
	    
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeRefBankCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeRefBank", "table9016", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nRefBank),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 

	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeRefBusCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeRefBus", "table9017", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nRefBus),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeRefLawCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeRefLaw", "table9018", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nRefLaw),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeNumPaysCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeNumPays", "table9019", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nNumPays),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeOldInsuranceCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeOldInsurance", "table9020", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nOldInsurance),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 	    
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeProPayCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeProPay", "table9021", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nProPay),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 	
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tcnCodDicomCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.NumericControl("tcnCodDicom", 4, CStr(mobjclient_evalrisk.nNumEmployers),  , GetLocalResourceObject("tcnCodDicomToolTip"))%> </TD>
	    </TR>	
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tctDesDicomCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.TextControl("tctDesDicom", 60, mobjclient_evalrisk.sDesDicom, True, "")%> </TD>
	    </TR>	            	    	    
	    
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tctNote2Caption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.TextControl("tctNote2", 60, mobjclient_evalrisk.sNote2, True, "")%> </TD>
	    </TR>	    
	    <TR>
	        <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Nombre"><%= GetLocalResourceObject("AnchorNombre5Caption") %></A></LABEL></TD>
	    </TR>	    
	    <TR>	        
	        <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
	    </TR>	    
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeCreditReasonCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeCreditReason", "table9022", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nCreditReason),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 	

	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeLiqCurrenCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeLiqCurren", "table9023", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nLiqcurrent),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 	

	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeLiqAcdCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeLiqAcd", "table9024", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nLiqAcd),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 	
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeRentabilityCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeRentability", "table9025", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nRentability),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 		    
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeGrowSalesCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeGrowSales", "table9026", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nGrowSales),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 		    
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeEconomicCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeEconomic", "table9027", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nEconomic),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 	
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeFinancialCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeFinancial", "table9028", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nFinancial),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 		    	    
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tctNote3Caption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.TextControl("tctNote3", 60, mobjclient_evalrisk.sNote3, True, "")%> </TD>
	    </TR>
	    <TR>
	        <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Nombre"><%= GetLocalResourceObject("AnchorNombre6Caption") %></A></LABEL></TD>
	    </TR>
	    <TR>	        
	        <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
	    </TR>	    
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeCodRatingCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeCodRating", "table9029", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nCodRating),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 		    	   	    
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeDesRatingCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeDesRating", "table9030", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nDesRating),  ,  ,  ,  ,  ,  ,  ,  , "")%> </TD>
	    </TR>	 		    	   	    
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tctNote4Caption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.TextControl("tctNote4", 60, mobjclient_evalrisk.sNote4, True, "")%> </TD>
	    </TR>	
	    <TR>
	        <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Nombre"><%= GetLocalResourceObject("AnchorNombre7Caption") %></A></LABEL></TD>
	    </TR>	    
	    <TR>	        
	        <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
	    </TR>	        
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_evalrisk.nCurrency),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip"))%> </TD>
	    </TR>	 		    	   	    
	    
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tcnLimitCreditCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.NumericControl("tcnLimitCredit", 18, CStr(mobjclient_evalrisk.nLimitCredit),  , GetLocalResourceObject("tcnLimitCreditToolTip"), True, 2)%> </TD>
	    </TR>	    
	</TABLE>
	<!--<P ALIGN="Center"><%=mobjValues.BeginPageButton%></P>-->
	
	
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjclient_evalrisk = Nothing
%>




