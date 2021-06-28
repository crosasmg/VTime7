<%@ Page explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "crl010_k"
%>
<HTML>
<HEAD>

<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/tMenu.js"></SCRIPT>
	<META http-equiv="Content-Language" content="es">



	<%=mobjValues.StyleSheet()%>

<SCRIPT>

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

// ShowYear: Muestra el año completo (4 digitos)
//-------------------------------------------------------------------------------------------
function ShowYear(){
//-------------------------------------------------------------------------------------------

	with (self.document.forms[0]) {	
	    tcnYear_contr.value = getCompleteYear(self.document.forms[0].tcnYear_contr.value)	    
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

//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------
	return true;
}   
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}
//--------------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------------
	var lintIndex;
    var error;
    try {
		for(lintIndex=0;lintIndex < self.document.forms[0].elements.length;lintIndex++){
			self.document.forms[0].elements[lintIndex].disabled=false;
			if(self.document.images.length>0)
			    if(typeof(self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name])!='undefined')
			       self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name].disabled = self.document.forms[0].elements[lintIndex].disabled 
		}
	} catch(error){}	

}	   
</SCRIPT>

    <%mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("CRL010", "CRL010_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>

<BODY Class="Header" VLink="white" LINK="white" alink="white">

<BR></BR>
<FORM METHOD="post" ID="FORM" NAME="frmPrintTechAcc" ACTION="ValCoReinsuranRep.aspx?X=1">
	<TABLE WIDTH="100%">

	   <TR>
	       <TD><LABEL ID=100568><%= GetLocalResourceObject("tcnYear_contrCaption") %></LABEL></TD>
	       <TD><%=mobjValues.NumericControl("tcnYear_contr", 4, "",  , "",  , 0,  ,  ,  , "ShowYear()", True)%></TD>               </TR>
	   <TR>
	       <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=101688><A NAME="Fecha"><%= GetLocalResourceObject("AnchorFechaCaption") %></A></LABEL></TD>
	   </TR>
	   <TR>
	       <TD COLSPAN="5"><HR></TD>
	   </TR>
	   <TR> </TR>
	   <TR>
	       <TD><LABEL ID=101689><%= GetLocalResourceObject("cbePerTypeCaption") %></LABEL></TD>
	       <TD><%=mobjValues.PossiblesValues("cbePerType", "table97", 1,  ,  ,  ,  ,  ,  , "PerType_Change(this)", True,  , "")%></TD>
	       <TD>&nbsp;&nbsp;</TD>
	       <TD><LABEL ID=100575><%= GetLocalResourceObject("tcnPerNumCaption") %></LABEL></TD>
	       <TD><%=mobjValues.NumericControl("tcnPerNum", 2, "",  , "",  , 0,  ,  ,  ,  , True)%></TD>            
	   </TR>
	   <TR>
	       <TD COLSPAN="5"><hr></TD>
	   </TR>
	</TABLE>
	<TABLE WIDTH="100%">   
	   <TR>
	       <TD><LABEL ID=101690><%= GetLocalResourceObject("cboContraTypeCaption") %></LABEL></TD>
	       <TD><%mobjValues.TypeList = 2
mobjValues.TypeOrder = 1
	               mobjValues.List = "4,684"
mobjValues.BlankPosition = True
Response.Write(mobjValues.PossiblesValues("cboContraType", "table173", eFunctions.Values.eValuesType.clngComboType, Session("nType"),  ,  ,  ,  ,  ,  , True,  , ""))%></TD>
	   </TR>
	   <TR>              
	       <TD WIDTH="10%"><LABEL ID=101691><%= GetLocalResourceObject("tcnCompanyCaption") %></LABEL></TD>
	       <TD WIDTH="30%"><%=mobjValues.CompanyControl("tcnCompany", "",  , GetLocalResourceObject("tcnCompanyToolTip"),  , True, "tctCompanyName", False)%></TD>                   
	   </TR>
	   <TR>
	       <TD><LABEL ID=101692><%= GetLocalResourceObject("cbeBranchReiCaption") %></LABEL></TD>
	       <TD><%= mobjValues.PossiblesValues("cbeBranchRei", "table5000", 1, , , , , , , , True, , "")%></TD>
	   </TR>
	   <TR>
	       <TD WIDTH="18%"><LABEL ID=101693><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
	       <TD><%=mobjValues.PossiblesValues("cbeCurrency", "table11", 1,  ,  ,  ,  ,  ,  ,  , True,  , "")%></TD>
	   </TR>    

	</TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>




