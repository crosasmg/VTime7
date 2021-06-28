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
Dim mlngReceipt As Object
Dim mintDraft As Object
Dim mlngBordereaux As Object
Dim mblnNoCash As Object


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CO788_k")

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46 
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility 
	mobjValues.sCodisplPage = "CO788_k"
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
	     document.VssVersion="$$Revision: 4 $|$$Date: 14/10/04 18:09 $|$$Author: Nvaplat40 $"
    </SCRIPT>       




	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT>
var nCashNum = '<%=Session("nCashNum")%>'
//% insShowField: Oculta o muestra los campos.
//------------------------------------------------------------------------------------------
function insShowField(sType,sTd,sShow){
//------------------------------------------------------------------------------------------
    if (sShow=='show')
        document.getElementById(sTd).style.display='';
    else
        document.getElementById(sTd).style.display='none';
}

//% insCancel: Efectua el proceso de cancelación de la ventana.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}
//% insBlankFields: Blanquea los campos de la página
//------------------------------------------------------------------------------------------
function insBlankFields(){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		cbeTypeDoc.value=0;
		dtcClient.value='';
        dtcClient_Digit.value = "";
		UpdateDiv("lblCliename", '');
		tcnNumDoc.value='';
		tcnDraft.value='';
		tcnBordereaux.value='';
        UpdateDiv('lblCliename','');
        UpdateDiv('lblBordereaux','');
        UpdateDiv('lblTypRel','');
        UpdateDiv('lblDateRel','');
        UpdateDiv('lblAgree','');
        UpdateDiv('lblQDoc','');
        UpdateDiv('lblAmountRel','');
        UpdateDiv('lblAmount','');
        insShowField('DIV','divDatRel','noshow');
        insShowField('TD','tdlbllblAmount','noshow');
        insShowField('TD','tdlblAmount','noshow');        
tcdDateIncrease.value = '<% %>
<%=mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)%>';
	}
}

//% insShowLabel: Oculta o muestra las descripciones de los campos
//------------------------------------------------------------------------------------------
function insShowLabel(sLabel){
//------------------------------------------------------------------------------------------
	insShowField('TD','tdlblNumDoc','noshow');
	insShowField('TD','tdlblNumCon','noshow');
	insShowField('TD','tdlblNumPol','noshow');
	insShowField('TD','tdlblNumProponum','noshow');
	insShowField('TD','tdlblNumBulletin','noshow');
	if (sLabel !='') insShowField('TD',sLabel,'show');
}


//% insShowInitial: Oculta los campos al entrar en la página
//------------------------------------------------------------------------------------------
function insShowInitial(){
//------------------------------------------------------------------------------------------
	insShowField('TD','tdlblNumDoc','noshow');
	insShowField('TD','tdlblNumCon','noshow');
	insShowField('TD','tdlblNumPol','noshow');
	insShowField('TD','tdlblNumProponum','noshow');
	insShowField('TD','tdlblNumBulletin','noshow');
	if (nCashNum!=0){
	    self.document.forms[0].optDocRev[2].checked = true;
	    self.document.forms[0].optDev[2].checked = true;
	    insShowField('TD','tdoptDocRev1','noshow');
	    insShowField('TD','tdoptDev1','noshow');
	    insShowField('TD','tdoptDocRev2','noshow');
	    insShowField('TD','tdoptDev2','noshow');
	    insShowField('TD','tdlblDatDev','noshow');
	    insShowField('TD','tdlblDatDev1','noshow');
	    insShowField('TD','tdlblDatDev2','noshow');
	    insShowField('TD','tdlblDatDev3','noshow');
	    insShowField('TD','tdlblDatDev4','noshow');
	    insShowField('TD','tdlblDatDev5','noshow');
	    insShowField('TD','tdlblDateIncrease','noshow');
	    insShowField('TD','tdtcdDateIncrease','noshow');
	    insShowField('TD','tdlblClient','noshow');
	    insShowField('TD','tddtcClient','noshow');
	    insChangeDocRev("3");
	    }
	else{
	    insShowField('TD','tdoptDocRev3','noshow');
	    insShowField('TD','tdoptDev3','noshow');
	    insChangeDocRev("1");
	    }
}

//% insChangeDocRev: Se ejecuta cuando cambia el documento a reversar.
//------------------------------------------------------------------------------------------
function insChangeDocRev(sValue){
//------------------------------------------------------------------------------------------
	insBlankFields();
	switch (sValue){
//Se reversa un documento	
		case "1":
			insShowField('TD','tdlbllblAmount','noshow');
			insShowField('TD','tdlblAmount','noshow');
			insShowField('TD','tdlbltcnBordereaux','noshow');
			insShowField('TD','tdtcnBordereaux','noshow');			
			insShowLabel('');
			insShowField('TD','tdtcnNumDoc','noshow');
			insShowField('TD','tdlblDraft','noshow');
			insShowField('TD','tdtcnDraft','noshow');		    
			insShowField('TD','tdlblTypeDoc','show');
			insShowField('TD','tdcbeTypeDoc','show');
			insShowField('TD','tdlblvalLoans','noshow');
			insShowField('TD','tdvalLoans','noshow');
            break;
//Se reversan todos los documentos
		case "2":  
		    insShowField('TD','tdlblTypeDoc','noshow');
		    insShowField('TD','tdcbeTypeDoc','noshow');
		    insShowField('TD','tdlblNumDoc','noshow');
		    insShowField('TD','tdtcnNumDoc','noshow');
		    insShowLabel('');
		    insShowField('TD','tdlblDraft','noshow');
		    insShowField('TD','tdtcnDraft','noshow');		    
			insShowField('TD','tdlblvalLoans','noshow');
			insShowField('TD','tdvalLoans','noshow');
 			insShowField('TD','tdlbltcnBordereaux','show');
			insShowField('TD','tdtcnBordereaux','show');
			break;
//Se reversa toda la relación
		case "3":  
		    insShowField('TD','tdlblTypeDoc','noshow');
		    insShowField('TD','tdcbeTypeDoc','noshow');
		    insShowLabel('');
		    insShowField('TD','tdtcnNumDoc','noshow');
		    insShowField('TD','tdlblDraft','noshow');
		    insShowField('TD','tdtcnDraft','noshow');		    
			insShowField('TD','tdlblvalLoans','noshow');
			insShowField('TD','tdvalLoans','noshow');
 			insShowField('TD','tdlbltcnBordereaux','show');
			insShowField('TD','tdtcnBordereaux','show');
			break;
	}
}
//% insChangeTypeDoc: Se ejecuta cuando cambia el tipo de documento a reversar.
//------------------------------------------------------------------------------------------
function insChangeTypeDoc(sValue){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
	    tcnNumDoc.value='';
	    tcnDraft.value='';
	}    
    UpdateDiv('lblAmount','');        	
    insShowField('DIV','divDatRel','noshow');
	switch (sValue){ 
//Se reversa una cuota de financiamiento
		case "2":  
			insShowField('TD','tdlbllblAmount','noshow');
			insShowField('TD','tdlblAmount','noshow');
 			insShowField('TD','tdlbltcnBordereaux','noshow');
			insShowField('TD','tdtcnBordereaux','noshow');
			insShowLabel('tdlblNumCon');
		    insShowField('TD','tdtcnNumDoc','show');
		    insShowField('TD','tdlblDraft','show');
		    insShowField('TD','tdtcnDraft','show');		    
			break;
//Se reversa un abono a préstamo        	
		case "6":  
			insShowField('TD','tdlbllblAmount','noshow');
			insShowField('TD','tdlblAmount','noshow');
 			insShowField('TD','tdlbltcnBordereaux','noshow');
			insShowField('TD','tdtcnBordereaux','noshow');
			insShowLabel('tdlblNumPol');
		    insShowField('TD','tdtcnNumDoc','show');
		    insShowField('TD','tdlblDraft','noshow');
		    insShowField('TD','tdtcnDraft','noshow');		    
			break;
//Para todos los demás documentos se pide solamente número del mismo
		default:  
		    if (sValue=="3"){ 
		        insShowLabel('tdlblNumBulletin');
		        }
		    else{
				if ((sValue=="7") || (sValue=="21") || (sValue=="22") || (sValue=="23")){
				    insShowLabel('tdlblNumProponum');
				}    
				else{
					if ((sValue=="24") || (sValue=="8") || (sValue=="9")){
						insShowLabel('tdlblNumPol');
					}
					else{					 
						insShowLabel('tdlblNumDoc');
					}	
				}
			}
			insShowField('TD','tdlbllblAmount','noshow');
			insShowField('TD','tdlblAmount','noshow');
			insShowField('TD','tdlbltcnBordereaux','noshow');
			insShowField('TD','tdtcnBordereaux','noshow');
			insShowField('TD','tdlblDraft','noshow');
			insShowField('TD','tdtcnDraft','noshow');
			insShowField('TD','tdtcnNumDoc','show');
            self.document.forms[0].dtcClient.value ='';
			self.document.forms[0].dtcClient_Digit.value ='';
			UpdateDiv('lblCliename','');
self.document.forms[0].tcdDateIncrease.value = '<% %>
<%=mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)%>';
			
	}
}

//% insChangeBordereaux: Se ejecuta cuando cambia el número de la relación.
//------------------------------------------------------------------------------------------
function insChangeBordereaux(){
//------------------------------------------------------------------------------------------
var ldate = '<% %>
<%=mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)%>'
    with (self.document.forms[0]){
        if (tcnBordereaux.value != 0)
            insDefValues("ShowDataCO788", "nBordereaux=" + tcnBordereaux.value);
        else{
            insShowField('DIV','divDatRel','noshow');
	        UpdateDiv('lblBordereaux','');
            UpdateDiv('lblTypRel','');
			UpdateDiv('lblDateRel','');
			UpdateDiv('lblAgree','');
			UpdateDiv('lblBank','');
			UpdateDiv('lblQDoc','');
			UpdateDiv('lblAmountRel','');
            dtcClient.value ='';
			dtcClient_Digit.value ='';
			UpdateDiv('lblCliename','');
			tcdDateIncrease.value = ldate;
		}
	}
}

//% insChangeDocument: Se ejecuta cuando cambia el número de documento.
//------------------------------------------------------------------------------------------
function insChangeDocument(sField){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]){        
        switch (sField){
            case "tcnNumDoc":
                switch (cbeTypeDoc.value){
                    case "6":                         
                        insDefValues("ShowPolicyCO788", "nCollecDocTyp=" + cbeTypeDoc.value + "&nDocument=" + tcnNumDoc.value);
                        break;
                    case "2":                        
                        break;
                    default:
                        insDefValues("ShowDataCO788", "nCollecDocTyp=" + cbeTypeDoc.value + "&nDocument=" + tcnNumDoc.value + "&nDraft=" + tcnDraft.value + "&nLoans=" + valLoans.value);     
                }
                break;
           case "tcnDraft":
                insDefValues("ShowDataCO788", "nCollecDocTyp=" + cbeTypeDoc.value + "&nDocument=" + tcnNumDoc.value + "&nDraft=" + tcnDraft.value);
                break;
           case "valLoans":				
                insDefValues("ShowDataCO788", "nCollecDocTyp=" + cbeTypeDoc.value + "&nDocument=" + tcnNumDoc.value + "&nDraft=" + tcnDraft.value + "&nLoans=" + valLoans.value);
                break;     
        }
	}
}

</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("CO788", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CO788_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With

mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCollectDev" ACTION="valCollectionTra.aspx?mode=1">
<BR><BR>
    <%Response.Write(mobjValues.ShowWindowsName("CO788", Request.QueryString.Item("sWindowDescript")))%>
    <BR>
    <TABLE WIDTH="30%" ALIGN=CENTER>
        <TR>
			<TD WIDTH="25%"><LABEL ID=10444><%= GetLocalResourceObject("tcdDateCaption") %></LABEL></TD>
<TD WIDTH="25%"><% %>
<%=mobjValues.DateControl("tcdDate", CStr(Today),  , GetLocalResourceObject("tcdDateToolTip"))%></TD>
        </TR>        
    </TABLE>    
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="20"></TD>
            <TD WIDTH="25%"></TD>
            <TD WIDTH="5%">&nbsp</TD>
            <TD WIDTH="20%"></TD>
            <TD WIDTH="30%"></TD>
       </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Reversa"><%= GetLocalResourceObject("AnchorReversaCaption") %></A></LABEL></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Opcion"><%= GetLocalResourceObject("AnchorOpcionCaption") %></A></LABEL></TD>
        </TR>        
        <TR>
            <TD COLSPAN="2" CLASS="Horline"></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="Horline"></TD> 
        </TR>             
        <TR>
            <TD id="tdoptDocRev1" COLSPAN="2"><%=mobjValues.OptionControl(0, "optDocRev", GetLocalResourceObject("optDocRev_1Caption"), "1", "1", "insChangeDocRev(this.value)")%> </TD>
            <TD></TD>
            <TD id="tdoptDev1" COLSPAN="2"><%=mobjValues.OptionControl(0, "optDev", GetLocalResourceObject("optDev_1Caption"), "1", "1")%> </TD>            
        </TR>             
        <TR>
            <TD id="tdoptDocRev2" COLSPAN="2"><%=mobjValues.OptionControl(0, "optDocRev", GetLocalResourceObject("optDocRev_2Caption"), "0", "2", "insChangeDocRev(this.value)")%> </TD>
            <TD></TD>
            <TD id="tdoptDev2" COLSPAN="2"><%=mobjValues.OptionControl(0, "optDev", GetLocalResourceObject("optDev_2Caption"), "0", "2")%> </TD>            
        </TR>             
        <TR>
            <TD id="tdoptDocRev3" COLSPAN="2"><%=mobjValues.OptionControl(0, "optDocRev", GetLocalResourceObject("optDocRev_3Caption"), "0", "3", "insChangeDocRev(this.value)")%> </TD>
            <TD></TD>
            <TD id="tdoptDev3" COLSPAN="2"><%=mobjValues.OptionControl(0, "optDev", GetLocalResourceObject("optDev_3Caption"), "0", "3")%> </TD>            
        </TR>             
        <TR>
            <TD id="tdlblDatDev" COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Datos"><%= GetLocalResourceObject("AnchorDatosCaption") %></A></LABEL></TD>
            <TD id="tdlblDatDev2"></TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Reversa"><%= GetLocalResourceObject("AnchorReversa2Caption") %></A></LABEL></TD>
        </TR>        
        <TR>
            <TD id="tdlblDatDev1" COLSPAN="2" CLASS="Horline"></TD>
            <TD id="tdlblDatDev3"></TD>
            <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>             
        <TR>
			<TD id="tdlblDateIncrease"><LABEL ID=0><%= GetLocalResourceObject("tcdDateIncreaseCaption") %></LABEL></TD>
<TD id="tdtcdDateIncrease"><% %>
<%=mobjValues.DateControl("tcdDateIncrease", CStr(Today),  , GetLocalResourceObject("tcdDateIncreaseToolTip"))%></TD>
            <TD id="tdlblDatDev4"></TD>                        
            <TD ID="tdlblTypeDoc"><LABEL ID=0><%= GetLocalResourceObject("cbeTypeDocCaption") %></LABEL></TD>             
            <%mobjValues.TypeList = 2
mobjValues.List = "17"%>
            <TD ID="tdcbeTypeDoc"><%=mobjValues.PossiblesValues("cbeTypeDoc", "table5587", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insChangeTypeDoc(this.value);",  ,  , GetLocalResourceObject("cbeTypeDocToolTip"))%></TD>
            <TD ID="tdlbltcnBordereaux"><LABEL ID=10443><%= GetLocalResourceObject("tcnBordereauxCaption") %></LABEL></TD> 
            <TD ID="tdtcnBordereaux"><%=mobjValues.NumericControl("tcnBordereaux", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnBordereauxToolTip"),  ,  ,  ,  ,  , "insChangeBordereaux();")%> </TD> 
        </TR>             
        <TR>
            <TD id="tdlblClient"><LABEL ID=10288><%= GetLocalResourceObject("dtcClientCaption") %></LABEL></TD>            
            <TD id="tddtcClient"><%=mobjValues.ClientControl("dtcClient", "",  , GetLocalResourceObject("dtcClientToolTip"),  ,  , "lblCliename", True)%></TD>
            <TD id="tdlblDatDev5"></TD>            
            <TD ID="tdlblNumDoc"><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>            
            <TD ID="tdlblNumBulletin"><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
            <TD ID="tdlblNumProponum"><LABEL><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
            <TD ID="tdlblNumCon"><LABEL><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
            <TD ID="tdlblNumPol"><LABEL><%= GetLocalResourceObject("tcnNumDocCaption") %></LABEL></TD>
            <TD ID="tdtcnNumDoc"><%=mobjValues.NumericControl("tcnNumDoc", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnNumDocToolTip"),  ,  ,  ,  ,  , "insChangeDocument('tcnNumDoc');")%>
        <TR>            
			<TD><%=mobjValues.DIVControl("lblCliename", True)%></TD>
			<TD></TD>			
			<TD ID="tdlblDraft"><LABEL><%= GetLocalResourceObject("tcnDraftCaption") %></LABEL></TD>	
			<TD ID="tdtcnDraft"><%=mobjValues.NumericControl("tcnDraft", 5, vbNullString,  , GetLocalResourceObject("tcnDraftToolTip"),  ,  ,  ,  ,  , "insChangeDocument('tcnDraft');")%></TD>
        </TR>
        <TR>
            <TD colspan="3"></TD>
			<TD ID="tdlblvalLoans"><LABEL ID=0><%= GetLocalResourceObject("valLoansCaption") %></LABEL></TD>
            <%
With mobjValues.Parameters
	.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nPolicy", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
%>
			<TD ID="tdvalLoans"><%=mobjValues.PossiblesValues("valLoans", "Tabtab_loans", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "setTimeout(""insChangeDocument('valLoans')"",50);",  , 10, GetLocalResourceObject("valLoansToolTip"))%></TD>
        </TR>        
        <TR>
            <TD colspan="3"></TD>
			<TD ID="tdlbllblAmount"><LABEL ID=0><%= GetLocalResourceObject("Anchor5Caption") %></LABEL></TD>
			<TD ID="tdlblAmount"><%=mobjValues.DIVControl("lblAmount")%></TD>
        </TR>        
    </TABLE>                  
	<DIV ID="divDatRel">    
		<TABLE WIDTH="60%" ALIGN="CENTER">
		    <TR>
				<TD WIDTH="40%">&nbsp</TD>
				<TD WIDTH="60%">&nbsp</TD>
		    </TR>
		    <TR>
		        <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="DatosRel"><%= GetLocalResourceObject("AnchorDatosRelCaption") %></A></LABEL></TD>
		    </TR>        
		    <TR>
		        <TD COLSPAN="2" CLASS="Horline"></TD>
		    </TR>
		    <TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("Anchor6Caption") %></LABEL></TD>
				<TD><%=mobjValues.DIVControl("lblBordereaux")%></TD>
		    </TR>
		    <TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("Anchor7Caption") %></LABEL></TD>
				<TD><%=mobjValues.DIVControl("lblTypRel")%></TD>
		    </TR>
		    <TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("Anchor8Caption") %></LABEL></TD>
				<TD><%=mobjValues.DIVControl("lblDateRel")%></TD>
		    </TR>
		    <TR>
				<TD ID="tdlbllblAgree"><LABEL ID=0><%= GetLocalResourceObject("Anchor9Caption") %></LABEL></TD>
				<TD ID="tdlblAgree"><%=mobjValues.DIVControl("lblAgree")%></TD>
		    </TR>
		    <TR>
				<TD ID="tdlbllblBank"><LABEL ID=0><%= GetLocalResourceObject("Anchor10Caption") %></LABEL></TD>
				<TD ID="tdlblBank"><%=mobjValues.DIVControl("lblBank")%></TD>
		    </TR>
		    <TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("Anchor11Caption") %></LABEL></TD>
				<TD><%=mobjValues.DIVControl("lblQDoc")%></TD>
		    </TR>
		    <TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("Anchor12Caption") %></LABEL></TD>
				<TD><%=mobjValues.DIVControl("lblAmountRel")%></TD>
		    </TR>
		</TABLE>
	</DIV>	
	<%=mobjValues.HiddenControl("hddSequence", "")%>
	<%=mobjValues.HiddenControl("hddDoc_Amount", "")%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
Response.Write("<SCRIPT>insShowInitial();</SCRIPT>")
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("CO788_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>






