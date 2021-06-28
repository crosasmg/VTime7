<%@ Page explicit="true" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "crl895_k"
%>
<HTML>
<HEAD>

<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/tMenu.js"></SCRIPT>
	<meta http-equiv="Content-Language" content="es">



	<%=mobjValues.StyleSheet()%>

<SCRIPT>

// PerType_Change: Cambia el valor del campo nPerNum según el tipo de período
//-------------------------------------------------------------------------------------------
function PerType_Change(Field){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
	    switch(Field.value)
	    {
	    	case '1':
	    	case '5':
	    	{
	    		tcnPerNum.value='1';
	    		tcnPerNum.disabled=true;
	    		break;
	    	}	
	    	default:
	    	{
	    		tcnPerNum.value='';
	    		tcnPerNum.disabled=false;
	    		break;
	    	}	
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
//--------------------------------------------------------------------------------------------------
function EnabledField(Field)
//--------------------------------------------------------------------------------------------------
{   
 self.document.forms[0].elements["cbeBranchRei"].disabled = (Field==1 || Field==2 || Field==4)
 }

</SCRIPT>


    <%mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("CRL895", "CRL895_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<BR></BR>

    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>    
<BR></BR>
<FORM METHOD="POST" ID="FORM" NAME="frmPrintRCessClaim" ACTION="ValCoReinsuranRep.aspx?X=1">
<TABLE WIDTH="100%">
   <TR>
       <TD COLSPAN="3">&nbsp;</TD>
       <TD COLSPAN="2" ROWSPAN="3" VALIGN="TOP">
           <TABLE WIDTH="100%"> 
              <TR>
                <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=101666><A NAME="Periodo"><%= GetLocalResourceObject("AnchorPeriodoCaption") %></A></LABEL></TD>
              </TR>
              <TR>
                <TD COLSPAN="5" CLASS="HorLine"></TD>
              </TR>
              <TR>
                <TD><LABEL ID=101667><%= GetLocalResourceObject("tcdInitdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdInitdate", CStr(Today),  , GetLocalResourceObject("tcdInitdateToolTip"),  ,  ,  ,  ,  , 1)%></TD>
                <TD></TD>
                <TD><LABEL ID=101668><%= GetLocalResourceObject("tcdEnddateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEnddate", CStr(Today),  , GetLocalResourceObject("tcdEnddateToolTip"),  ,  ,  ,  ,  , 2)%></TD>
              </TR>  
           </TABLE>
       </TD>
   </TR>
   <TR>
       <TD WIDTH="10%"><LABEL ID=101669><%= GetLocalResourceObject("cbeCompanyCaption") %></LABEL></TD>
       <TD WIDTH="30%"><%=mobjValues.PossiblesValues("cbeCompany", "Company", 1,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCompanyToolTip"))%></TD>  
       <TD></TD>
   </TR>
   <TR>
       <TD><LABEL ID=101672><%= GetLocalResourceObject("cbeBranchReiCaption") %></LABEL></TD>
       <TD><%=mobjValues.PossiblesValues("cbeBranchRei", "table5000", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranchReiToolTip"))%></TD>
   </TR>
   </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>




