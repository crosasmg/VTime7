<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT> 
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 13.22 $"

//%insStateZone: Habilita los campos de la forma
//------------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------------
}

//%insPreZone: Ejecuta rutinas previas a la carga de la página
//------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//------------------------------------------------------------------------------------------------
}

//%insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------------
   return true
}

//%EnableFields: Habilita los RadioButtons y demás campos según la opción seleccionada
//------------------------------------------------------------------------------------------------
function EnableFields(nValue){
//------------------------------------------------------------------------------------------------
	switch(nValue)
	{
		case "1": //Intermediario
		{
			self.document.forms[0].elements["valIntermedia"].disabled=false;
			self.document.forms[0].elements["btnvalIntermedia"].disabled=false;
			self.document.forms[0].elements["tctClientCode"].disabled=true;
			self.document.forms[0].elements["btntctClientCode"].disabled=true;
			break;
		}
		case "2": //Clientes
		{
			self.document.forms[0].elements["valIntermedia"].disabled=true;
			self.document.forms[0].elements["btnvalIntermedia"].disabled=true;
			self.document.forms[0].elements["tctClientCode"].disabled=false;
			self.document.forms[0].elements["btntctClientCode"].disabled=false;
			self.document.forms[0].elements["valIntermedia"].value='';
			$(self.document.forms[0].elements["valIntermedia"]).change();
			break;
		}
		case "3": //Todos los intermediarios
		{
			self.document.forms[0].elements["valIntermedia"].disabled=true;
			self.document.forms[0].elements["btnvalIntermedia"].disabled=true;
			self.document.forms[0].elements["tctClientCode"].disabled=true;
			self.document.forms[0].elements["btntctClientCode"].disabled=true;
			self.document.forms[0].elements["valIntermedia"].value='';
			$(self.document.forms[0].elements["valIntermedia"]).change();
			break;
		}
		case "4": //Campo Póliza
		{		
		    if(self.document.forms[0].tcnPolicy.value!='')
		    {
		        self.document.forms[0].tcdStardate.value='';
		        self.document.forms[0].tcdEnddate.value='';
		        self.document.forms[0].tcdStardate.disabled=true;
		        self.document.forms[0].btn_tcdStardate.disabled=true;
		        self.document.forms[0].tcdEnddate.disabled=true;
		        self.document.forms[0].btn_tcdEnddate.disabled=true;
		    }
		    else
		    {
		        self.document.forms[0].tcdStardate.disabled=false;
		        self.document.forms[0].btn_tcdStardate.disabled=false;
		        self.document.forms[0].tcdEnddate.disabled=false;
		        self.document.forms[0].btn_tcdEnddate.disabled=false;
		    }		    
		}
	}
}

///%ChangeBranch: Asigna el valor del Ramo como parámetro para obtener el Producto
//--------------------------------------------------------------------------------------------
function ChangeBranch(Field){
//--------------------------------------------------------------------------------------------

	if(typeof(document.forms[0].valProduct)!='undefined'){
		self.document.forms[0].valProduct.Parameters.Param1.sValue=Field.value;
		self.document.forms[0].valProduct.disabled=false;
		self.document.forms[0].btnvalProduct.disabled=false;
		self.document.forms[0].tcnPolicy.disabled=false;
	}
}

//%ShowChangeValues: Se obtienen los datos de Sucursal/Oficina/Agencia asociados al intermediario
//-----------------------------------------------------------------------------------------------
function ShowChangeValues(){
//-----------------------------------------------------------------------------------------------
	insDefValues('AGL014','nIntermed=' + self.document.forms[0].elements["valIntermedia"].value,'/VTimeNet/Agent/AgentRep');		
}
</SCRIPT>

<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("AGL014", "AGL014_k.aspx", 1, ""))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="AGL014" ACTION="valAgentRep.aspx?sMode=1">
<BR><BR>
<%
Response.Write(mobjValues.ShowWindowsName("AGL014"))
%>
  <TABLE WIDTH="100%" COLS=5 BORDER=0>	
    <TR>	  
      <TD WIDTH="20%" ><%=mobjValues.OptionControl(0, "tcnTypeClient", GetLocalResourceObject("tcnTypeClient_CStr1Caption"), CStr(1), CStr(1), "EnableFields(this.value);")%></TD>
      <TD WIDTH="80%"><%=mobjValues.PossiblesValues("valIntermedia", "TabIntermedia", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  , "ShowChangeValues();",  ,  , GetLocalResourceObject("valIntermediaToolTip"))%> </TD>
    </TR>
    <TR>      
      <TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
      <TD><%=mobjValues.DIVControl("lblBran_Off")%></TD>
    </TR>
    <TR>      
      <TD><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
      <TD><%=mobjValues.DIVControl("lblOffice")%> </TD>
    </TR>
    <TR>      
      <TD><LABEL ID=0><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
      <TD><%=mobjValues.DIVControl("lblAgency")%> </TD>
    </TR>
    <TR>
      <TD><%=mobjValues.OptionControl(0, "tcnTypeClient", GetLocalResourceObject("tcnTypeClient_CStr2Caption"), CStr(2), CStr(2), "EnableFields(this.value);")%></TD>
      <TD><%=mobjValues.ClientControl("tctClientCode", "",  , GetLocalResourceObject("tctClientCodeToolTip"),  , True, "tctClientName", False)%></TD>
    </TR>
    <TR>         
      <TD COLSPAN=2><%=mobjValues.OptionControl(0, "tcnTypeClient", GetLocalResourceObject("tcnTypeClient_CStr3Caption"), CStr(3), CStr(3), "EnableFields(this.value);")%></TD>      
      <TD COLSPAN="1">&nbsp;</TD>            
      <TD><LABEL ID=0><%= GetLocalResourceObject("cbeInterTypeCaption") %></LABEL></TD>
	  <TD><%=mobjValues.PossiblesValues("cbeInterType", "TabInter_typ", 1,  ,  ,  ,  ,  ,  ,  ,  ,  , "")%></TD>
	</TR>
	<TR>
	  <TD COLSPAN="5">&nbsp;</TD>            
	</TR>
	<TR>
	  <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><A NAME="Prestamo"><%= GetLocalResourceObject("AnchorPrestamoCaption") %></A></LABEL></TD>
    </TR>
    
    <TR>
        <TD COLSPAN="5" CLASS="Horline"></TD>
    </TR>
    
    <TR>               
	  <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
	  <TD><%Response.Write(mobjValues.PossiblesValues("cbeBranch", "Table10", 1, "",  ,  ,  ,  ,  , "ChangeBranch(this);"))%></TD>    
	  <TD COLSPAN="1">&nbsp;</TD>            
	  <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
	  <%With mobjValues.Parameters
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With%>
	   <TD><%=mobjValues.PossiblesValues("valProduct", "tabProdmaster1", 2, "", True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valProductToolTip"))%></TD>
	 </TR>
	 <TR>	          
		<TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
		<TD><%=mobjValues.NumericControl("tcnPolicy", 8, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "EnableFields(""4"")")%></TD>
		<TD COLSPAN="1">&nbsp;</TD>            
    	<TD><LABEL ID=0><%= GetLocalResourceObject("cbeStatloanCaption") %></LABEL></TD>
		<TD><%=mobjValues.PossiblesValues("cbeStatloan", "table191", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatloanToolTip"))%></TD>
    </TR>
    <TR>
	<TR>
		<TD><LABEL ID=0><%= GetLocalResourceObject("tcdStardateCaption") %></LABEL></TD>
		<TD><%=mobjValues.DateControl("tcdStardate", "",  , GetLocalResourceObject("tcdStardateToolTip"))%></TD>
		<TD COLSPAN="1">&nbsp;</TD>
		<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEnddateCaption") %></LABEL></TD>
		<TD><%=mobjValues.DateControl("tcdEnddate", "",  , GetLocalResourceObject("tcdEnddateToolTip"))%></TD>
	</TR>
    <TR>
      <TD><LABEL ID=0><%= GetLocalResourceObject("cboLoanTypeCaption") %></LABEL></TD>
      <TD><%=mobjValues.PossiblesValues("cboLoanType", "Table245", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboLoanTypeToolTip"))%></TD>
      <TD>&nbsp;</TD>
      <TD><LABEL ID=0><%= GetLocalResourceObject("cboPayFormCaption") %></LABEL></TD>
      <TD><%=mobjValues.PossiblesValues("cboPayForm", "Table180", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboPayFormToolTip"))%></TD>
    </TR>
    <TR>        
		<TD><LABEL ID=0><%= GetLocalResourceObject("tcnLoanCaption") %></LABEL></TD>
       	<TD><%=mobjValues.NumericControl("tcnLoan", 5, "",  , GetLocalResourceObject("tcnLoanToolTip"))%></TD>
    </TR>    
    <TR>
    </TR>
  </TABLE>
<%
mobjValues = Nothing
%>  
</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>EnableFields(self.document.forms[0].elements['tcnTypeClient'][0].value)</script>")
%>






