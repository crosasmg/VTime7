<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mlngReceipt As Object
Dim mintBranch As Object
Dim mintProduct As Object
Dim mblnPremiumExist As Object

Dim mclsPremium As eCollection.Premium
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mclsPremium = New eCollection.Premium
End With
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0"> 
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT>
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 20/09/04 16:38 $"

//%	ShowDefValues: Condiciona el recargo por el cambio en el patrón de busqueda
//-------------------------------------------------------------------------------------------
function ShowDefValues(Field){
//-------------------------------------------------------------------------------------------
	var strParams; 
	with (document.forms[0]){
		if (Field.value != 0 && Field.value != ""){
    		strParams = "nReceipt=" + tcnReceiptNum.value + 
						"&nBranch=" + tcnBranch.value + 
						"&nProduct=" + tcnProduct.value
			insDefValues("Receipt_2",strParams,'/VTimeNet/Collection/CollectionTra'); 
		}
        else {
    		strParams = ""
			insDefValues("Blank2",strParams,'/VTimeNet/Collection/CollectionTra'); 
		}
	}
}
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
  
}

//% ShowPages: Llama a la ventana de Datos de verificación del recibo (SCO001) - ACM - 26/06/2001
//-------------------------------------------------------------------------------------------
function ShowPage(){
//-------------------------------------------------------------------------------------------
//- Variable lstrLocation: Se usa para armar el QueryString que va a recibir la ventana
//- SCO001 para poder realizar la búsqueda de los datos de verificación del recibo - ACM - 26/06/2001
	var lstrLocation="";
	
	lstrLocation = lstrLocation + "&nReceipt=" + self.document.forms[0].elements["tcnReceiptNum"].value;
	lstrLocation = lstrLocation + "&sCertype=2";
	lstrLocation = lstrLocation + "&nDigit=" + self.document.forms[0].elements["tcnDigit"].value;;
	lstrLocation = lstrLocation + "&nPayNumber=" + self.document.forms[0].elements["tcnPaynumbe"].value;;
	lstrLocation = lstrLocation + "&nGeneralNumerator=" + self.document.forms[0].elements["tcnGeneralNumerator"].value;
	lstrLocation = lstrLocation + "&nBranch=" + self.document.forms[0].elements["tcnBranch"].value;
	lstrLocation = lstrLocation + "&nProduct=" + self.document.forms[0].elements["tcnProduct"].value;
	//+ Se hace el llamado a la ventana SCO001
	ShowPopUp("/VTimeNet/Common/SCO001.aspx?sCodispl=SCO001"+lstrLocation,"",700,400,true,false,20,20)
}   
	</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("CO675"))
	.Write(mobjMenu.MakeMenu("CO675", "CO675_K.aspx", 1, ""))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "CO675_K.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmModCollect" ACTION="valCollectionTra.aspx?mode=1">
<BR><BR><BR>
<%Response.Write(mobjValues.ShowWindowsName("CO675"))%>    
    <TABLE WIDTH="100%">
    <BR><BR>
        <TR>
            <TD><LABEL ID=10528><%= GetLocalResourceObject("tcnReceiptNumCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnReceiptNum", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnReceiptNumToolTip"),  ,  ,  ,  ,  , "ShowDefValues(this);")%>
                <%Response.Write(mobjValues.AnimatedButtonControl("btnShowSCO001", "/VTimeNet/Images/btn_ValuesOff.png", GetLocalResourceObject("btnShowSCO001ToolTip"),  , "ShowPage();", True))%>
            </TD>
			<TD><LABEL ID=10528><%= GetLocalResourceObject("tctDescWayPayCaption") %></LABEL></TD>
			<TD><%Response.Write(mobjValues.TextControl("tctDescWayPay", 20, "",  , GetLocalResourceObject("tctDescWayPayToolTip"),  ,  ,  ,  , True))%></TD>			
		</TR>		
        <TR>
            <TD><LABEL ID=10528><%= GetLocalResourceObject("tcnContratCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnContrat", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnContratToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
			<TD><LABEL ID=10528><%= GetLocalResourceObject("valDraftCaption") %></LABEL></TD>
            <TD><%
With mobjValues
	.Parameters.Add("nContrat", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nStat_draft", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	
	Response.Write(.PossiblesValues("valDraft", "Tab_draft", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valDraftToolTip"),  ,  , False))
End With
%>
            </TD>			
		</TR>		
		<TR><TD COLSPAN="4">&nbsp;</TD></TR>
		<TR>
		    <TD><LABEL ID=10528><%= GetLocalResourceObject("tcdLimitDateCaption") %></LABEL></TD>
		    <TD><%=mobjValues.DateControl("tcdLimitDate",  ,  , GetLocalResourceObject("tcdLimitDateToolTip"),  ,  ,  ,  , True)%></TD>
		    <TD><LABEL ID=10290><%= GetLocalResourceObject("tcdNewLimitDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdNewLimitDate", CStr(Today),  , GetLocalResourceObject("tcdNewLimitDateToolTip"))%></TD>
		</TR>
		
<%
With Response
	.Write(mobjValues.HiddenControl("tctPremiumExist", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnDigit", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnWayPay", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnPaynumbe", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnGeneralNumerator", CStr(0)))
	.Write(mobjValues.HiddenControl("tctCertype", "2"))
	.Write(mobjValues.HiddenControl("tcnBranch", "0"))
	.Write(mobjValues.HiddenControl("tcnProduct", "0"))
	.Write(mobjValues.HiddenControl("tcnStatusPre", CStr(0)))
	.Write(mobjValues.HiddenControl("tcnBulletins", CStr(0)))
	.Write(mobjValues.HiddenControl("hddnType", CStr(0)))
End With
%>
    </TABLE>    
</FORM>
</BODY>
</HTML>





