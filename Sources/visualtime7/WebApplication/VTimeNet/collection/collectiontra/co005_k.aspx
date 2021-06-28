<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjError As Object


</script>

<%Response.Expires = -1441

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

%>
<SCRIPT LANGUAGE="JavaScript">
//% insShowField: Oculta o muestra los campos.
//------------------------------------------------------------------------------------------
function insShowField(sType,sTd,sShow){
//------------------------------------------------------------------------------------------
    if (sShow=='show')
        document.getElementById(sTd).style.display='';
    else
        document.getElementById(sTd).style.display='none';
}

//% insChangeReceipt: Se ejecuta cuando cambia el número del recibo.
//------------------------------------------------------------------------------------------
function insChangeReceipt(){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if (gmnReceipt.value != 0)
            insDefValues("ShowDataCO005", "nReceipt=" + gmnReceipt.value);
        else{
            insShowField('DIV','divDatRec','noshow');
			UpdateDiv('lblBranch','');
			UpdateDiv('lblProduct','');
			UpdateDiv('lblPolicy','');	    	    
			UpdateDiv('lblClient','');		
			UpdateDiv('lblCurrency','');
			UpdateDiv('lblOffice','');
			UpdateDiv('lblStatus_pre','');
	        UpdateDiv('lblTratypei','');			
			hddBranch.value='';
			hddProduct.value='';  
			cbeCause.value=0;			
		}
	}
}
//% insChangeOptAnull: Se ejecuta cuando cambia el número del recibo.
//------------------------------------------------------------------------------------------
function insChangeOptAnull(){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if (optAnul[0].checked==true)
            cbeCause.disabled = false;
        else
			cbeCause.disabled = true;
        insShowField('DIV','divDatRec','noshow');
	    UpdateDiv('lblBranch','');
	    UpdateDiv('lblProduct','');
		UpdateDiv('lblPolicy','');	    	    
        UpdateDiv('lblClient','');		
	    UpdateDiv('lblCurrency','');
	    UpdateDiv('lblOffice','');
	    UpdateDiv('lblStatus_pre','');
	    UpdateDiv('lblTratypei','');
	    hddBranch.value='';
	    hddProduct.value='';  
	    cbeCause.value=0;
	    gmnReceipt.value='';
	}
}
//% insCancel(): Cancela una acción.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 4 $|$$Date: 5/11/04 17:36 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CO005", "CO005_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAnulReinst" ACTION="ValCollectionTra.aspx?mode=1">
	<BR><BR>
	<%=mobjValues.ShowWindowsName("CO005", Request.QueryString.Item("sWindowDescript"))%>	
	<BR><BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="20%" COLSPAN="2">&nbsp;</TD>
            <TD WIDTH="20%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=40505><A NAME="Operación"><%= GetLocalResourceObject("AnchorOperaciónCaption") %></A></LABEL></TD>
            <TD WIDTH="20%" COLSPAN="2">&nbsp;</TD>
        </TR>
        <TR>
			<TD WIDTH="20%" COLSPAN="2"></TD>        
            <TD COLSPAN="2" CLASS="HorLine"></TD>
            <TD WIDTH="20%" COLSPAN="2"></TD>        
        </TR>
        <TR>
			<TD WIDTH="20%" COLSPAN="2">&nbsp;</TD>        
			<TD><%=mobjValues.OptionControl(100538, "optAnul", GetLocalResourceObject("optAnul_CStr1Caption"), "1", CStr(1), "insChangeOptAnull();",  ,  , GetLocalResourceObject("optAnul_CStr1ToolTip"))%></TD>
			<TD><%=mobjValues.OptionControl(100537, "optAnul", GetLocalResourceObject("optAnul_CStr2Caption"), "2", CStr(2), "insChangeOptAnull();",  ,  , GetLocalResourceObject("optAnul_CStr2ToolTip"))%></TD>
			<TD WIDTH="20%" COLSPAN="2">&nbsp;</TD>			
        </TR>
        <TR>
			<TD COLSPAN="6">&nbsp;</TD>        
        </TR>
        
        <TR>        
            <TD><LABEL ID=10380><%= GetLocalResourceObject("tcdDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdDate", CStr(Today),  , GetLocalResourceObject("tcdDateToolTip"))%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("gmnReceiptCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("gmnReceipt", 10,  ,  , GetLocalResourceObject("gmnReceiptToolTip"),  ,  ,  ,  ,  , "insChangeReceipt();")%></TD>
            <TD><LABEL ID=10378><%= GetLocalResourceObject("cbeCauseCaption") %></LABEL></TD>
            <%With Response
	mobjValues.TypeList = 2
	mobjValues.List = "0"
	.Write("<TD>" & mobjValues.PossiblesValues("cbeCause", "Table95", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCauseToolTip")) & "</TD>")
End With
%>
        </TR>        
    </TABLE>        
	<DIV ID="divDatRec">            
		<TABLE WIDTH="60%" ALIGN="CENTER">
        <TR>
            <TD COLSPAN="5">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40506><A NAME="Recibo"><%= GetLocalResourceObject("AnchorReciboCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
		    <TD><LABEL ID=10377><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		    <TD><%Response.Write(mobjValues.DIVControl("lblBranch"))
Response.Write(mobjValues.HiddenControl("hddBranch", ""))%> </TD>   		
        </TR>		    
        <TR>        
			<TD><LABEL ID=10381><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
  			<TD><%Response.Write(mobjValues.DIVControl("lblProduct"))
Response.Write(mobjValues.HiddenControl("hddProduct", ""))%></TD>
        </TR>
        <TR>        
			<TD><LABEL ID=10381><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
  			<TD><%=mobjValues.DIVControl("lblPolicy")%></TD>
        </TR>
        <TR>        
			<TD><LABEL ID=10381><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
  			<TD><%=mobjValues.DIVControl("lblClient")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=10383><%= GetLocalResourceObject("Anchor5Caption") %></LABEL></TD>
			<TD><%=mobjValues.DIVControl("lblOffice")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=10379><%= GetLocalResourceObject("Anchor6Caption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("lblCurrency")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=10379><%= GetLocalResourceObject("Anchor7Caption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("lblStatus_pre")%></TD>            
        </TR>
        <TR>
            <TD><LABEL ID=10379><%= GetLocalResourceObject("Anchor8Caption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("lblTratypei")%></TD>
        </TR>
    </TABLE>
	</DIV>                
</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>insShowField('DIV','divDatRec','noshow');</SCRIPT>")
mobjValues = Nothing
%>




