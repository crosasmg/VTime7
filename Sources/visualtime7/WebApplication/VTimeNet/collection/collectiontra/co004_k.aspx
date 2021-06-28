<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co004_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co004_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 25/10/04 5:51p $|$$Author: Nvapla10 $"

//% insStateZone: Habilita los campos de la forma según la acción a ejecutar
//-------------------------------------------------------------------------------------------
    function insStateZone(){
//-------------------------------------------------------------------------------------------    
}

//% insChangeOption: Actualiza los objetos de la forma, según el tipo de opción
//-------------------------------------------------------------------------------------------
function insChangeOption(lobjOption) {
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){		
		switch (lobjOption) {
//+         Póliza
			case "nPolicy":
			     tcnPolicy.disabled=false
			     tcnReceiptNum.disabled=true
			     tcnReceiptNum.value=''
			     tcnDraft.value=''
			     tcnContrat.value=''
			     tcnPolicy.value=''
			     cbeBranch.value=''
			     valProduct.value=''
			     dtcClient.value=''
			     dtcClient_Digit.value=''
			     UpdateDiv('sCliename',' ');
			     tcnDraft.value=''
    		     tcnContrat.value=''
				break;
//+         Recibo
			case "nReceipt":
			      tcnReceiptNum.disabled=false
			      tcnPolicy.disabled=true
			      tcnPolicy.value=''
			      cbeBranch.value=''
			      valProduct.value=''
			      dtcClient.value=''
			      dtcClient_Digit.value=''
			      UpdateDiv('sCliename',' ');
			      tcnDraft.value=''
			      tcnContrat.value=''
				break;
//+         Contrato
			case "nContrat":
			      tcnPolicy.disabled=true
			      tcnReceiptNum.disabled=true
			      tcnContrat.disabled=false
			      tcnReceiptNum.value=''
			      tcnContrat.value=''
			      tcnDraft.value=''
			      tcnPolicy.value=''
			      cbeBranch.value=''
			      valProduct.value=''
			      dtcClient.value=''
			      dtcClient_Digit.value=''
			      UpdateDiv('sCliename',' ');
				break;
//+         Cuota
			case "nDraft": 
			      tcnPolicy.disabled=true
			      tcnReceiptNum.disabled=true
			      tcnContrat.disabled=false
			      tcnDraft.disabled=false
			      tcnReceiptNum.value=''
			      tcnPolicy.value=''
			      cbeBranch.value=''
			      valProduct.value=''
			      dtcClient.value=''
			      dtcClient_Digit.value=''
			      UpdateDiv('sCliename',' ');
				break;
		}
    }
}

//% ShowChangeValues: Se cargan los valores de acuerdo al número de recibo introducido
//-------------------------------------------------------------------------------------------
function ShowChangeValues(lobjOption){
//-------------------------------------------------------------------------------------------
    with (document.forms[0]){
    
     switch (lobjOption) {
//+         Póliza
			case "nPolicy":
		          insDefValues("ShowDefValuesCO004","nPolicy=" + tcnPolicy.value);
		          break;
		    case "nReceipt":
			      insDefValues("Receipt_1","nReceipt=" + tcnReceiptNum.value);     
			      break;
			case "nContrat":
   			      insDefValues("Contrat_CO004","nContrat=" + tcnContrat.value);     
			      break;
	}
 }	
}	

//% ShowPages: Llama a la ventana de Datos de verificación del recibo (SCO001)
//-------------------------------------------------------------------------------------------
function ShowPage(){
//-------------------------------------------------------------------------------------------
//- Variable lstrLocation: Se usa para armar el QueryString que va a recibir la ventana
//- SCO001 para poder realizar la búsqueda de los datos de verificación del recibo
	var lstrLocation="";
	with (self.document.forms[0]){
		if (tcnReceiptNum.value != '' && tcnReceiptNum.value != '0'){
			lstrLocation = lstrLocation + "&nReceipt=" + tcnReceiptNum.value;
			lstrLocation = lstrLocation + "&sCertype=" + hddsCertype.value;
			lstrLocation = lstrLocation + "&nDigit=" + hddnDigit.value;
			lstrLocation = lstrLocation + "&nPayNumber=" + hddnPaynumbe.value;
			lstrLocation = lstrLocation + "&nGeneralNumerator=" + hddnGeneralNumerator.value;
			lstrLocation = lstrLocation + "&nBranch=" + hddnBranch.value;
			lstrLocation = lstrLocation + "&nProduct=" + hddnProduct.value;

//+ Se hace el llamado a la ventana SCO001
			ShowPopUp("/VTimeNet/Common/SCO001.aspx?sCodispl=SCO001" + lstrLocation,"",700,400,true,false,20,20)
		}
	}
}

//% insCancel: Acciones al cancelar la transacción.
//-------------------------------------------------------------------------------------------   
function insCancel(){
//-------------------------------------------------------------------------------------------   
	return true
}
</SCRIPT>

<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<%
Response.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Response.Write(mobjValues.StyleSheet() & vbCrLf)
Response.Write(mobjMenu.MakeMenu("CO004", "CO004_K.aspx", 1, vbNullString))
mobjMenu = Nothing
%> 
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmModCollect" ACTION="valCollectionTra.aspx?x=1">
	<BR><BR>    
<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))%>
	<BR>    
    <TABLE WIDTH="100%">
         <TR>
            <TD WIDTH="20%" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD COLSPAN="2">&nbsp;&nbsp;</TD>
            <TD COLSPAN="3">&nbsp;&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL ID=1><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="20%" CLASS="Horline"></TD>
            <TD COLSPAN="2"></TD>
            <TD COLSPAN="3"></TD>
           <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>
         <TR>  
           <TD>&nbsp;</TD>
           <TD>&nbsp;</TD>
        </TR>
        <TR>
           <TD WIDTH="20%"><%=mobjValues.OptionControl(0, "optGenera", GetLocalResourceObject("optGenera_1Caption"), "1", "1", "insChangeOption(""nPolicy"");")%> </TD>
           <TD COLSPAN="2">&nbsp;</TD>
           <TD COLSPAN="3">&nbsp;</TD>
           <TD><LABEL><%= GetLocalResourceObject("tcnPolicyCaption") %><LABEL> </TD>
           <TD><%=mobjValues.NumericControl("tcnPolicy", 10, "", False, GetLocalResourceObject("tcnPolicyToolTip"), False, False,  ,  ,  , "ShowChangeValues(""nPolicy"")", False)%></TD>
           <TD><LABEL ID=9388><%= GetLocalResourceObject("dtcClientCaption") %></LABEL></TD>
		   <TD><%=mobjValues.ClientControl("dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientToolTip"),  , True, "sCliename",  ,  ,  ,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>  
            <TD WIDTH="20%"><%=mobjValues.OptionControl(0, "optGenera", GetLocalResourceObject("optGenera_2Caption"),  , "2", "insChangeOption(""nReceipt"");")%> </TD>
            <TD COLSPAN="2">&nbsp;</TD>
            <TD COLSPAN="3">&nbsp;</TD>
            <TD><LABEL ID=10286><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=10294><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType, True)%></TD>

         </TR>
         <TR>  
          <TD WIDTH="20%"><%=mobjValues.OptionControl(0, "optGenera", GetLocalResourceObject("optGenera_3Caption"),  , "3", "insChangeOption(""nContrat"");")%> </TD>
          <TD COLSPAN="2">&nbsp;</TD>
          <TD COLSPAN="3">&nbsp;</TD>
          <TD><LABEL><%= GetLocalResourceObject("tcnReceiptNumCaption") %></LABEL></TD>
          <TD><%Response.Write(mobjValues.NumericControl("tcnReceiptNum", 10, "",  , GetLocalResourceObject("tcnReceiptNumToolTip"),  , 0,  ,  ,  , "ShowChangeValues(""nReceipt"")", True))
Response.Write(mobjValues.AnimatedButtonControl("btnShowSCO001", "/VTimeNet/Images/btn_ValuesOff.png", GetLocalResourceObject("btnShowSCO001ToolTip"),  , "ShowPage();", True))%>
         </TR>
         
         <TR> 
          <TD WIDTH="20%"><%=mobjValues.OptionControl(0, "optGenera", GetLocalResourceObject("optGenera_4Caption"),  , "4", "insChangeOption(""nDraft"");")%> </TD>
          <TD COLSPAN="2">&nbsp;</TD>
          <TD COLSPAN="3">&nbsp;</TD>
          <TD ><LABEL><%= GetLocalResourceObject("tcnContratCaption") %></LABEL></TD>
           <TD><%=mobjValues.NumericControl("tcnContrat", 10, "",  , GetLocalResourceObject("tcnContratToolTip"),  ,  ,  ,  ,  , "ShowChangeValues(""nContrat"")", True)%></TD>
          <TD ><LABEL><%= GetLocalResourceObject("tcnDraftCaption") %></LABEL></TD>
          <TD><%=mobjValues.NumericControl("tcnDraft", 5, "",  , GetLocalResourceObject("tcnDraftToolTip"),  ,  ,  ,  ,  , "ShowChangeValues(""nContrat"")", True)%></TD>
         </TR>  
    </TABLE>    
    <TABLE>    
<%

With Response
	.Write(mobjValues.HiddenControl("hddnBranch", CStr(0)))
	.Write(mobjValues.HiddenControl("hddnProduct", CStr(0)))
	.Write(mobjValues.HiddenControl("hddnPolicy", CStr(0)))
	.Write(mobjValues.HiddenControl("hddnCertif", CStr(0)))
	.Write(mobjValues.HiddenControl("hdddEffectDate", CStr(0)))
	.Write(mobjValues.HiddenControl("hddnDigit", CStr(0)))
	.Write(mobjValues.HiddenControl("hddnCod_Agree", CStr(0)))
	.Write(mobjValues.HiddenControl("hddnPaynumbe", CStr(0)))
	.Write(mobjValues.HiddenControl("hddnGeneralNumerator", Session("sReceiptnum")))
	.Write(mobjValues.HiddenControl("hddsCertype", ""))
	.Write(mobjValues.HiddenControl("hddnStatus_pre", ""))
End With
mobjValues = Nothing
%>
    </TABLE>    
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("co004_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




