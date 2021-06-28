<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Variables para el manejo de las clase
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

'- Variables para el manejo de campos 
Dim mlngReceipt As Integer

Dim mlngBordereaux As Integer
Dim mblnNoCash As Boolean


'% insReaInitial: Se encarga de inicializar las variables de trabajo
'-----------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'-----------------------------------------------------------------------------------------
	mlngReceipt = eRemoteDB.Constants.intNull
	
End Sub

'% insValCashNum: valida que el usuario que hace el reverso posea caja para habilitar 
'%                o no la opción de eliminar el ingreso de caja.
'--------------------------------------------------------------------------------------
Private Sub insValCashNum()
	'--------------------------------------------------------------------------------------
	If Session("nCashNum") = 0 Then
		mblnNoCash = True
	Else
		mblnNoCash = False
	End If
End Sub

'% insOldValues: Se encarga de asignar los valores obtenidos en vbscript a javascript.
'-----------------------------------------------------------------------------------------
Private Sub insOldValues()
	'-----------------------------------------------------------------------------------------
	If mlngReceipt <> eRemoteDB.Constants.intNull And mlngBordereaux <> eRemoteDB.Constants.intNull Then
		With Response
			.Write("<SCRIPT>")
			.Write("var mlngReceipt = " & CStr(mlngReceipt) & ";")
			.Write("var mlngBordereaux = " & CStr(mlngBordereaux) & ";")
			.Write("</" & "Script>")
		End With
	Else
		With Response
			.Write("<SCRIPT>")
			.Write("var mlngReceipt = 0;")
			.Write("var mlngBordereaux = 0;")
			.Write("</" & "Script>")
		End With
	End If
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CO009_k")

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46 
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility 
	mobjValues.sCodisplPage = "CO009_k"
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46 
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility 
End With
%> 
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
	<SCRIPT>	
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 8 $|$$Date: 16/12/03 12:22 $|$$Author: Nvaplat40 $"
    </SCRIPT>       




	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT>
//%	ShowDefValues: Condiciona el recargo por el cambio en el patrón de busqueda
//-------------------------------------------------------------------------------------------
function ShowDefValues(Field, Type){
//-------------------------------------------------------------------------------------------
    with (document.forms[0]){
    	if ((tcnReceiptNum.value!=mlngReceipt) ||
			(tcnBordereaux.value!=mlngBordereaux)) {
//+ Si cambio el recibo se inicializan el contrato y la cuota.
			if (tcnReceiptNum.value!=mlngReceipt) {
				tcnContrat.value = '';
			}
			mlngReceipt = tcnReceiptNum.value
			mlngBordereaux = tcnBordereaux.value
            switch (Type){ 
//+ Tipo de documento: Recibo. 
			    case "Receipt":  
			        if (tcnReceiptNum.value != ''){
						insDefValues("ShowDataCO009", "nReceipt=" + tcnReceiptNum.value + "&nContrat=" + tcnContrat.value);
					}else{
	                    UpdateDiv('lblBranch','');
	                    UpdateDiv('lblProduct','');
						UpdateDiv('lblCurrency','');
						UpdateDiv('lblOffice','');
	                    UpdateDiv('lblWay_pay','');
	                    UpdateDiv('lblStatus','');
	                    UpdateDiv('lblAgreement','');
	                    UpdateDiv('lblPolicy','');
                        optTypOper[0].checked=false;
                        optTypOper[0].disabled=false;
                        optTypOper[1].checked=false;
                        optTypOper[1].disabled = false;
                        optTypOper[2].checked = false;
                        optTypOper[2].disabled = false;
	                    tcnBordereaux.value='';
	                    tcnBordereaux.disabled=false;
                        tcdDateIncrease.disabled=true;
                        tcdDateIncrease.value="";
                        btn_tcdDateIncrease.disabled=true;
                        chkRelAll.checked=false;
					}  
			        break;	
//+ Tipo de documento: Relación.
			    case "Bordereaux":
					if (tcnBordereaux.value>''){
        				insDefValues("ShowDataCO009", "nBordereaux=" + tcnBordereaux.value);
        			}else{
        			    UpdateDiv('lblBank','');
        			    UpdateDiv('lblAgreement','');
                        optTypOper[0].checked=false;
                        optTypOper[0].disabled=false;
                        optTypOper[1].checked=false;
                        optTypOper[1].disabled = false;
                        optTypOper[2].checked = false;
                        optTypOper[2].disabled = false;
                        chkRelAll.disabled=true;
                        tcnReceiptNum.disabled=false;
                        tcdDateIncrease.disabled=true;
                        btn_tcdDateIncrease.disabled=true;
                        tcdDateIncrease.value="";
        			}
					break;					
				default:
					break;                    
	        }
		}
	}
}

//% insChangeOper: Efectua el proceso de cancelación de la ventana.
//------------------------------------------------------------------------------------------
function insChangeOper(sValue){
//------------------------------------------------------------------------------------------
    if (sValue == "1") {
        self.document.forms[0].tcdDateIncrease.disabled = true;
        self.document.forms[0].btn_tcdDateIncrease.disabled = true;
        self.document.forms[0].tcdDateIncrease.value = "";
    }
    else if (sValue == "5") {
        self.document.forms[0].tcdDateIncrease.disabled = false;
        self.document.forms[0].btn_tcdDateIncrease.disabled = false;
        self.document.forms[0].tcdDateIncrease.value = '<%= mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate) %>';
        alert("Advertencia: Esta opción no generará cargos a cuentas.");
    }
    else {
        self.document.forms[0].tcdDateIncrease.disabled = false;
        self.document.forms[0].btn_tcdDateIncrease.disabled = false;
        self.document.forms[0].tcdDateIncrease.value = '<%= mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate) %>';
    }
}

//% insCancel: Efectua el proceso de cancelación de la ventana.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}
//% ShowPages: Llama a la ventana de Datos de verificación del recibo (SCO001)
//-------------------------------------------------------------------------------------------
function ShowPage(){
//-------------------------------------------------------------------------------------------
//+ Variable lstrLocation: Se usa para armar el QueryString que va a recibir la ventana
//+ SCO001 para poder realizar la búsqueda de los datos de verificación del recibo - ACM - 26/06/2001
	if(self.document.forms[0].elements["tcnReceiptNum"].value>0){
		var lstrLocation="";	
		lstrLocation = lstrLocation + "&nReceipt=" + self.document.forms[0].elements["tcnReceiptNum"].value;
		lstrLocation = lstrLocation + "&sCertype=2";
		lstrLocation = lstrLocation + "&nDigit=" + self.document.forms[0].elements["tcnDigit"].value;;
		lstrLocation = lstrLocation + "&nPayNumber=" + self.document.forms[0].elements["tcnPaynumbe"].value;;
		lstrLocation = lstrLocation + "&nGeneralNumerator=" + self.document.forms[0].elements["tcnGeneralNumerator"].value;
		lstrLocation = lstrLocation + "&nBranch=" + self.document.forms[0].elements["cbeBranch"].value;
		lstrLocation = lstrLocation + "&nProduct=" + self.document.forms[0].elements["valProduct"].value;

	//+ Se hace el llamado a la ventana SCO001
		ShowPopUp("/VTimeNet/Common/SCO001.aspx?sCodispl=SCO001"+lstrLocation,"",700,400,true,false,20,20)
	}
}   

</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("CO009", Request.QueryString.Item("sWindowDescript")))
	
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CO009_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	
End With

mobjMenu = Nothing
Call insValCashNum()
Call insReaInitial()
Call insOldValues()

%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCollectRef" ACTION="valCollectionTra.aspx?mode=1">
<BR><BR>
    <%Response.Write(mobjValues.ShowWindowsName("CO009", Request.QueryString.Item("sWindowDescript")))%>

    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="25%"><LABEL ID=10444><%= GetLocalResourceObject("tcdDateCaption") %></LABEL></TD>
<TD WIDTH="22%"><% %>
<%=mobjValues.DateControl("tcdDate", CStr(Today),  , GetLocalResourceObject("tcdDateToolTip"))%></TD>
            <TD WIDTH="5%">&nbsp</TD>
            <TD WIDTH="22%">&nbsp</TD>
            <TD WIDTH="25%">&nbsp</TD>
        </TR>        
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40509><A NAME="Recibo"><%= GetLocalResourceObject("AnchorReciboCaption") %></A></LABEL></TD>
        </TR>        
        <TR>
            <TD COLSPAN="5" CLASS="Horline"></TD>
        </TR>             
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnReceiptNumCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnReceiptNum", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnReceiptNumToolTip"),  ,  ,  ,  ,  , "ShowDefValues(this,'Receipt')")%>
                <%Response.Write(mobjValues.AnimatedButtonControl("btnShowSCO001", "/VTimeNet/Images/btn_ValuesOff.png", GetLocalResourceObject("btnShowSCO001ToolTip"),  , "ShowPage();", True))%></TD>			
            <TD>&nbsp</TD>                
			<TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>	
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL>
				<%=mobjValues.DIVControl("lblBranch", True)%></TD>
			<TD>&nbsp</TD>	
            <TD><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL>
				<%=mobjValues.DIVControl("lblProduct", True)%></TD>
        </TR> 
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("Anchor3Caption") %></LABEL>
                <%=mobjValues.DIVControl("lblPolicy", True)%></TD>
            <TD>&nbsp</TD>    
            <TD><LABEL><%= GetLocalResourceObject("Anchor4Caption") %></LABEL>
                <%=mobjValues.DIVControl("lblOffice", True)%></TD>
        </TR>
        <TR>
            <TD>
				<LABEL ID=10447><%= GetLocalResourceObject("Anchor5Caption") %></LABEL>
                <%=mobjValues.DIVControl("lblWay_pay", True)%>
            </TD>
            <TD>&nbsp</TD>
            <TD><LABEL ID=10447><%= GetLocalResourceObject("Anchor6Caption") %></LABEL>
				<%=mobjValues.DIVControl("lblStatus", True)%>
			</TD>
		</TR> 
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("Anchor7Caption") %></LABEL>
                <%=mobjValues.DIVControl("lblCurrency", True)%></TD>
            <TD>&nbsp</TD>    
            <TD colspan="3">&nbsp</TD>
		</TR> 
		
        <TR>
            <TD WIDTH="100%" COLSPAN="5">&nbsp;</TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=40509><A NAME="Caja"><%= GetLocalResourceObject("AnchorCajaCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="2" CLASS="Horline"></TD>
        </TR>        
        <TR>
            <TD COLSPAN="2"><%=mobjValues.HiddenControl("tcnBordereaux", "")%> </TD> 
    		</TD> 
        </TR> 
        <TR>
			<TD COLSPAN="2">
            <%=mobjValues.HiddenControl("chkRelAll", "2")%>
            <%=mobjValues.OptionControl(0, "optTypOper", GetLocalResourceObject("optTypOper_2Caption"), "2", "2", "insChangeOper(this.value)",,,GetLocalResourceObject("optTypOper_2ToolTip"))%></TD>
        </TR>
        <TR>
			<TD COLSPAN="2"><%= mobjValues.OptionControl(0, "optTypOper", GetLocalResourceObject("optTypOper_3Caption"), "2","3", "insChangeOper(this.value)", , , GetLocalResourceObject("optTypOper_3ToolTip"))%></TD>
        </TR>     
        <TR>
			<TD COLSPAN="2"><%= mobjValues.OptionControl(0, "optTypOper", GetLocalResourceObject("optTypOper_4Caption"), "2", "4", "insChangeOper(this.value)", , , GetLocalResourceObject("optTypOper_4ToolTip"))%></TD>
        </TR>
        <TR>
			<TD COLSPAN="2"><%= mobjValues.OptionControl(0, "optTypOper", GetLocalResourceObject("optTypOper_5Caption"), "2", "5", "insChangeOper(this.value)", , , GetLocalResourceObject("optTypOper_5Caption"))%></TD>
        </TR>         
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateIncreaseCaption") %></LABEL></TD>
            <TD ><%=mobjValues.DateControl("tcdDateIncrease",  ,  , GetLocalResourceObject("tcdDateIncreaseToolTip"),  ,  ,  ,  , True)%></TD>
        </TR> 
		<%With Response
	.Write(mobjValues.HiddenControl("tctPremiumExist", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnStatusPre", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnDigit", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnPaynumbe", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnType", CStr(0)))
	.Write(mobjValues.HiddenControl("tctDirdebit", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnPremium", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnBalance", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnCurrency", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnPolicy", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnGeneralNumerator", Session("sReceiptNum")))
	.Write(mobjValues.HiddenControl("tcnWayPay", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnContrat", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnDraft", CStr(0)))
	.Write(mobjValues.HiddenControl("hddClient", ""))
	.Write(mobjValues.HiddenControl("hddRel_amoun", CStr(0)))
	.Write(mobjValues.HiddenControl("hddOffice", ""))
	.Write(mobjValues.HiddenControl("hddOfficeAgen", ""))
	.Write(mobjValues.HiddenControl("hddAgency", ""))
	.Write(mobjValues.HiddenControl("cbeBranch", CStr(0)))
	.Write(mobjValues.HiddenControl("valProduct", CStr(0)))
	.Write(mobjValues.HiddenControl("hddnBranch", CStr(0)))
	.Write(mobjValues.HiddenControl("hddnProduct", CStr(0)))
	.Write(mobjValues.HiddenControl("hddnPolicy", CStr(0)))
	.Write(mobjValues.HiddenControl("hddnCertif", CStr(0)))

End With
%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("CO009_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>