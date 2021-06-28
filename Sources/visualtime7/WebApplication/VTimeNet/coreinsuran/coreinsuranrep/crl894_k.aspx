<%@ Page explicit="true" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "CRL894"
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
Response.Write(mobjMenu.MakeMenu("CRL894", "CRL894_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>

<BODY Class="Header" VLink="white" LINK="white" alink="white">

<BR></BR>
<FORM METHOD="post" ID="FORM" NAME="frmPrintTechAcc" ACTION="ValCoReinsuranRep.aspx?X=1">
	<TABLE WIDTH="100%">
	  <TR>
	    <TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>   
	    <TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>   
      </TR>
	
	  <TR>
	    <TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("tcnMonth_iniCaption") %></LABEL>
	       <%=mobjValues.NumericControl("tcnMonth_ini", 4, "",  , "",  , 0,  ,  ,  , "ShowYear()", True)%>
	       <LABEL><%= GetLocalResourceObject("tcnYear_iniCaption") %></LABEL>
	       <%=mobjValues.NumericControl("tcnYear_ini", 4, "",  , "",  , 0,  ,  ,  , "ShowYear()", True)%>
	    </TD>   
	    
  	    <TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("tcnMonth_iniCaption") %></LABEL>
	       <%=mobjValues.NumericControl("tcnMonth_end", 4, "",  , "",  , 0,  ,  ,  , "ShowYear()", True)%>
	       <LABEL><%= GetLocalResourceObject("tcnYear_iniCaption") %></LABEL>
	       <%=mobjValues.NumericControl("tcnYear_end", 4, "",  , "",  , 0,  ,  ,  , "ShowYear()", True)%>
	    </TD>   
      </TR>
	</TABLE>
	<TABLE WIDTH="100%">   
	   <TR>              
	       <TD WIDTH="10%"><LABEL ID=101691><%= GetLocalResourceObject("tcnCompanyCaption") %></LABEL></TD>
	       <TD WIDTH="30%"><%=mobjValues.CompanyControl("tcnCompany", "",  , GetLocalResourceObject("tcnCompanyToolTip"),  , True, "tctCompanyName", False)%></TD>                   
	   </TR>
	   <TR>
	       <TD><LABEL ID=101692><%= GetLocalResourceObject("cbeBranchReiCaption") %></LABEL></TD>
	       <TD><%=mobjValues.PossiblesValues("cbeBranchRei", "table5000", 1,  ,  ,  ,  ,  ,  ,  , True,  , "")%></TD>
	   </TR>
	</TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>




