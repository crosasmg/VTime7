<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As Object

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object
Dim mobjReinsuran As eCoReinsuran.t_reinsurutil


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjReinsuran = New eCoReinsuran.t_reinsurutil

mobjValues.sCodisplPage = "CRL893"

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//% CalculateTotalIngreso: Calcula el total una vez que se añaden las cantidades 
//-------------------------------------------------------------------------------------
function CalculateTotalIngreso(){
//-------------------------------------------------------------------------------------
	var ldblnAmount_pr=0;
	var ldblnnres_risklast=0;
	var ldbltcnres_cllast=0;
	var ldblTotal=0;

	if(self.document.forms[0].tcnAmount_pr.value!="") 
		ldblnAmount_pr=insConvertNumber(self.document.forms[0].tcnAmount_pr.value);
	else
		ldblnAmount_pr=0;
    
    if(self.document.forms[0].tcnres_risklast.value!="") 
		ldblnnres_risklast=insConvertNumber(self.document.forms[0].tcnres_risklast.value);
	else
		ldblnnres_risklast=0;
		
    if(self.document.forms[0].tcnres_cllast.value!="") 
		ldbltcnres_cllast=insConvertNumber(self.document.forms[0].tcnres_cllast.value);
	else
		ldbltcnres_cllast=0;
	
	ldblTotal = ldblnAmount_pr + ldblnnres_risklast + ldbltcnres_cllast
	self.document.forms[0].tcntotam_in.value = VTFormat(ldblTotal, '', '', '', 6, true);


	ldblTotalIn = insConvertNumber(self.document.forms[0].tcntotam_in.value);
	ldblTotalE = insConvertNumber(self.document.forms[0].tcntotam_out.value);
	ldblUtilTotal =  ldblTotalIn - ldblTotalE;
//	ldblPjeUti = insConvertNumber(self.document.forms[0].tcnPjeutil.value);

	self.document.forms[0].tcnAmountutil.value = VTFormat(ldblUtilTotal, '', '', '', 6, true);
	if (ldblUtilTotal > 0)
//	    ldblUtilTotal = ldblUtilTotal * ldblPjeUti;
//	    ldblUtilTotal = ldblUtilTotal * 2;
		self.document.forms[0].tcnAmpartutil.value = VTFormat(ldblUtilTotal, '', '', '', 6, true); 
	else 
		self.document.forms[0].tcnAmpartutil.value =0; 
	}
//-------------------------------------------------------------------------------------
function CalculateTotalEgreso(){
//-------------------------------------------------------------------------------------
	var ldblnAm_comm_pr=0;
	var ldblnreser_cl=0;
	var ldblnamadmin=0;
	var ldblnres_risk=0;
	var ldblnamlastneg=0;
	var ldblTotal=0;
	
	if(self.document.forms[0].tcnAm_comm.value!="") 
		ldblnAm_comm_pr=insConvertNumber(self.document.forms[0].tcnAm_comm.value);
	else
		ldblnAm_comm_pr=0;

	if(self.document.forms[0].tcnreser_cl.value!="") 
		ldblnreser_cl=insConvertNumber(self.document.forms[0].tcnreser_cl.value);
	else
		ldblnreser_cl=0;

	if(self.document.forms[0].tcnamadmin.value!="") 
		ldblnamadmin=insConvertNumber(self.document.forms[0].tcnamadmin.value);
	else
		ldblnamadmin=0;

	if(self.document.forms[0].tcnres_risk.value!="") 
		ldblnres_risk=insConvertNumber(self.document.forms[0].tcnres_risk.value);
	else
		ldblnres_risk=0;
		
	if(self.document.forms[0].tcnamlastneg.value!="") 
		ldblnamlastneg=insConvertNumber(self.document.forms[0].tcnamlastneg.value);
	else
		ldblnamlastneg=0;
		
	ldblTotal = ldblnAm_comm_pr + ldblnreser_cl + ldblnamadmin + ldblnres_risk + ldblnamlastneg;
	 
	self.document.forms[0].tcntotam_out.value = VTFormat(ldblTotal, '', '', '', 6, true);            


	ldblTotalIn = insConvertNumber(self.document.forms[0].tcntotam_in.value);
	ldblTotalE = insConvertNumber(self.document.forms[0].tcntotam_out.value);
	ldblUtilTotal =  ldblTotalIn - ldblTotalE;
//	ldblPjeUti = insConvertNumber(self.document.forms[0].tcnPjeutil.value);
	self.document.forms[0].tcnAmountutil.value = VTFormat(ldblUtilTotal, '', '', '', 6, true);
	
	if (ldblUtilTotal > 0)
//	    ldblUtilTotal = ldblUtilTotal * ldblPjeUti;
//	    ldblUtilTotal = ldblUtilTotal * 2;
		self.document.forms[0].tcnAmpartutil.value =VTFormat(ldblUtilTotal, '', '', '', 6, true); 
	else 
		self.document.forms[0].tcnAmpartutil.value =0; 
}	
//-------------------------------------------------------------------------------------
function CalculateUtil(){
//-------------------------------------------------------------------------------------
	var ldblUtilTotal=0;
	var ldblTotalIn=0;
	var ldblTotalE=0;
//	var ldblPjeUtil=1;
	
	ldblTotalIn = insConvertNumber(self.document.forms[0].tcntotam_in.value);
	ldblTotalE  = insConvertNumber(self.document.forms[0].tcntotam_out.value);
//	ldblPjeUtil = insConvertNumber(self.document.forms[0].tcnPjeutil.value);

	ldblUtilTotal =  ldblTotalIn - ldblTotalE;
	self.document.forms[0].tcnAmountutil.value =VTFormat(ldblUtilTotal, '', '', '', 6, true); 

//    if (ldblPjeUtil = 0)
		ldblPjeUtil = 1;
		
	if (ldblUtilTotal > 0)
//	    ldblUtilTotal = ldblUtilTotal * ldblPjeUtil;
		self.document.forms[0].tcnAmpartutil.value =VTFormat(ldblUtilTotal, '', '', '', 6, true); 
	else 
		self.document.forms[0].tcnAmpartutil.value =0; 
}

//-------------------------------------------------------------------------------------
function CalculatePjeUtil(){
//-------------------------------------------------------------------------------------
	var ldblUtilidad=0;
	var ldblUtilTotal=0;
	var ldblPjeUtil=0;
	
	ldblUtilidad = insConvertNumber(self.document.forms[0].tcnAmpartutil.value);
	ldblPjeUtil = insConvertNumber(self.document.forms[0].tcnPjeutil.value);
		
	if (ldblPjeUtil > 0)
	    ldblUtilTotal = ldblUtilidad * ldblPjeUtil /100;
		self.document.forms[0].tcnAmpartutil.value =VTFormat(ldblUtilTotal, '', '', '', 6, true); 
}


</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CRL893", "CRL893.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

Call mobjReinsuran.insPreCRL893(CInt(Request.QueryString.Item("nCompany")), CInt(Request.QueryString.Item("nNumber")), CDate(Request.QueryString.Item("dDate_ini")), CDate(Request.QueryString.Item("dDate_end")), Session("nTypeproc"))



%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CRL893" ACTION="valCoReinsuranrep.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("CRL893"))
Response.Write("<BR>")%>
<TABLE WIDTH="100%">
   <TR>                       
	<TD COLSPAN="2" CLASS="HighLighted"><LABEL><A><%= GetLocalResourceObject("AnchorCaption") %></A></LABEL></TD>
	<TD WIDTH="10%">&nbsp;</TD> 
	<TD COLSPAN="2" CLASS="HighLighted"><LABEL><A><%= GetLocalResourceObject("Anchor2Caption") %></A></LABEL></TD>
   </TR>        
   <TR>
   <TD COLSPAN="2"><HR></TD>		    
   <TD WIDTH="10%">&nbsp;</TD>
   <TD COLSPAN="2"><HR></TD>
   </TR>
   <TR>
     <TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcnAmount_prCaption") %></LABEL> 
         <%=mobjValues.NumericControl("tcnAmount_pr", 18, CStr(mobjReinsuran.namount_pr),  , GetLocalResourceObject("tcnAmount_prToolTip"),  , 4,  ,  ,  ,  , True)%></TD>
     <TD WIDTH="10%"></TD>
     <TD COLSPAN="2" ><LABEL><%= GetLocalResourceObject("tcnAm_commCaption") %></LABEL>
         <%=mobjValues.NumericControl("tcnAm_comm", 18, CStr(mobjReinsuran.nam_comm),  , GetLocalResourceObject("tcnAm_commToolTip"),  , 4,  ,  ,  ,  , True)%></TD>
     <TD>
   </TR>
   <TR>
      <TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcnres_risklastCaption") %></LABEL>
          <%=mobjValues.NumericControl("tcnres_risklast", 18, CStr(0),  , GetLocalResourceObject("tcnres_risklastToolTip"),  , 4,  ,  ,  , "CalculateTotalIngreso();", False)%></TD>
      <TD WIDTH="10%"></TD>
      <TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcnreser_clCaption") %></LABEL>
          <%=mobjValues.NumericControl("tcnreser_cl", 18, CStr(mobjReinsuran.nreser_cl),  , GetLocalResourceObject("tcnreser_clToolTip"),  , 4,  ,  ,  ,  , True)%></TD>
   </TR>
   
   <TR>
      <TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcnres_cllastCaption") %> </LABEL>
          <%=mobjValues.NumericControl("tcnres_cllast", 18, CStr(0),  , GetLocalResourceObject("tcnres_cllastToolTip"),  , 4,  ,  ,  , "CalculateTotalIngreso();", False)%></TD>
      <TD WIDTH="10%"></TD>
      <TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcnamadminCaption") %></LABEL>
          <%=mobjValues.NumericControl("tcnamadmin", 18, CStr(mobjReinsuran.namadmin),  , GetLocalResourceObject("tcnamadminToolTip"),  , 4,  ,  ,  ,  , True)%></TD>
   </TR>
    <TR>
      <TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcntotam_inCaption") %></LABEL>
          <%=mobjValues.NumericControl("tcntotam_in", 18,  ,  , GetLocalResourceObject("tcntotam_inToolTip"),  , 4,  ,  ,  , "", True)%></TD>
       <TD WIDTH="10%"></TD>
       <TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcnres_riskCaption") %></LABEL>
          <%=mobjValues.NumericControl("tcnres_risk", 18, CStr(0),  , GetLocalResourceObject("tcnres_riskToolTip"),  , 4,  ,  ,  , "CalculateTotalEgreso();", False)%></TD>
   </TR>
   <TR>
      <TD COLSPAN="2"></TD>
      <TD WIDTH="10%"></TD>
      <TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcnamlastnegCaption") %></LABEL>
         <%=mobjValues.NumericControl("tcnamlastneg", 18, CStr(0),  , GetLocalResourceObject("tcnamlastnegToolTip"),  , 4,  ,  ,  , "CalculateTotalEgreso();", False)%></TD>
    </TR>
    <TR>
      <TD COLSPAN="2"></TD>
      <TD WIDTH="10%"></TD>
      <TD COLSPAN="2" ><LABEL><%= GetLocalResourceObject("tcntotam_inCaption") %></LABEL>
         <%=mobjValues.NumericControl("tcntotam_out", 18,  ,  , GetLocalResourceObject("tcntotam_outToolTip"),  , 4,  ,  ,  ,  , True)%></TD>
   </TR>
   <TR>
   		<TD COLSPAN="2"  CLASS="HighLighted"><LABEL><A><%= GetLocalResourceObject("Anchor3Caption") %></A></LABEL></TD>
		<TD WIDTH="10%"></TD> 
		<TD COLSPAN="2">&nbsp;</TD>
   </TR>
      <TR>
	   <TD COLSPAN="2"><HR></TD>		    
	   <TD WIDTH="10%">&nbsp;</TD>
	   <TD COLSPAN="2">&nbsp;</TD>
   </TR>
   <TR>
      <TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcnAmountutilCaption") %></LABEL>
          <%=mobjValues.NumericControl("tcnAmountutil", 18, CStr(0),  , GetLocalResourceObject("tcnAmountutilToolTip"),  , 4,  ,  ,  ,  , True)%></TD>
      <TD WIDTH="10%">&nbsp;</TD>
      <TD COLSPAN="2">&nbsp;</TD>
   </TR>
   <TR>
      <TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcnPjeutilCaption") %></LABEL>
          <%=mobjValues.NumericControl("tcnPjeutil", 6, CStr(0),  , GetLocalResourceObject("tcnPjeutilToolTip"),  , 2,  ,  ,  , "CalculatePjeUtil();", False,  , False)%></TD>
      <TD WIDTH="10%">&nbsp;</TD>
      <TD COLSPAN="2">&nbsp;</TD>
   </TR>
   <TR>
      <TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcnAmpartutilCaption") %></LABEL>
          <%=mobjValues.NumericControl("tcnAmpartutil", 18, CStr(0),  , GetLocalResourceObject("tcnAmpartutilToolTip"),  , 4,  ,  ,  ,  , True)%></TD>
      <TD WIDTH="10%">&nbsp;</TD>
      <TD COLSPAN="2">&nbsp;</TD>
   </TR>
 <TR>
    <TD COLSPAN="2">&nbsp;</TD>
    <TD WIDTH="10%">&nbsp;</TD>
    <TD COLSPAN="2" CLASS="HighLighted"><%=mobjValues.CheckControl("chkPrint", GetLocalResourceObject("chkPrintCaption"), CStr(False), "1", "", False)%></TD>
</TR>
</TABLE>
<SCRIPT LANGUAGE="JavaScript">
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 1 $|$$Date: 20/04/06 18:28 $" 
</SCRIPT>
</FORM> 
</BODY>
</HTML>
<%Response.Write("<SCRIPT>CalculateTotalIngreso();</SCRIPT>")%>
<%Response.Write("<SCRIPT>CalculateTotalEgreso();</SCRIPT>")%>
<%Response.Write("<SCRIPT>CalculateUtil();</SCRIPT>")%>





