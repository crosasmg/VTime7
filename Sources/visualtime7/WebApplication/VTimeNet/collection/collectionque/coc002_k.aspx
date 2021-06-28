<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("coc002_k")
With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "coc002_k"
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("COC002", "COC002_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing%>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
</SCRIPT>      
<SCRIPT>
//% InsDeleteField: se limpian los campos de despliegue.
//--------------------------------------------------------------------------------------------
function InsDeleteField(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
        cbeWayPay.value = ''; 
	    cbeOffice.value = '';
	    tcdInitDate.value = '';
	    tcdEndDate.value = '';
        dtcClient.value = '';
        UpdateDiv('lblClieName','');
	    tcnBalance.value = '';
        cbeCurrency.value = '';
	}
}

//% InsChangeField: se controla los parámetros del campo producto.
//--------------------------------------------------------------------------------------------
function InsChangeField(sField, sValue){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		switch (sField){
			case 'Query':
			    if(sValue == '1'){
			        tcnPolicy.disabled=false;
				    tcnProposal.value='';
				    tcnProposal.disabled=true;
				    InsDeleteField();
				}   
				else if(sValue == '2'){   
				    tcnProposal.disabled=false;
				    tcnPolicy.value='';
				    tcnPolicy.disabled=true;
				    InsDeleteField();
				}			    
			    break;	
			case 'Branch':
				valProduct.Parameters.Param1.sValue=sValue;
				valProduct.disabled = (sValue == '0');
				btnvalProduct.disabled = valProduct.disabled;
				valProduct.value = '';
		        UpdateDiv('valProductDesc','');
		        InsDeleteField();
				break;			
		}		
	}
}
//%	ShowDefValues: Condiciona el recargo por el cambio en el patrón de busqueda
//-------------------------------------------------------------------------------------------
function ShowDefValues(Field){
//-------------------------------------------------------------------------------------------
    with (document.forms[0]){
        if (Field.value != 0 && Field.value != ""){
            if(optQuery[0].checked==true){
                ShowPopUp("/VTimeNet/Collection/CollectionQue/ShowDefValues.aspx?Field=Policy" + "&nBranch=" + cbeBranch.value + "&nProduct="+ valProduct.value + "&nPolicy="+ tcnPolicy.value,"ShowDefValuesCurrency",1, 1,"no","no",2000,2000);
            }
            else{ 
                ShowPopUp("/VTimeNet/Collection/CollectionQue/ShowDefValues.aspx?Field=Proposal" + "&nBranch=" + cbeBranch.value + "&nProduct="+ valProduct.value + "&nProposal="+ tcnProposal.value,"ShowDefValuesCurrency",1, 1,"no","no",2000,2000);
            }
        }        
        else{
            ShowPopUp("/VTimeNet/Collection/CollectionQue/ShowDefValues.aspx?Field=Blank","ShowDefValuesCurrency",1, 1,"no","no",2000,2000);
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
    with(self.document.forms[0]){
		optQuery[0].disabled=false;
		optQuery[1].disabled=false;
		cbeBranch.disabled=false;
		valProduct.disabled=false;		
		if(optQuery[0].checked==true){
		    tcnPolicy.disabled=false;
		    tcnProposal.value='';
		    tcnProposal.disabled=true;
		}   
		else{   
		    tcnProposal.disabled=false;
		    tcnPolicy.value='';
		    tcnPolicy.disabled=true;
		}
	}    
    document.images["btnvalProduct"].disabled=false;    
    InsDeleteField();
}
//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	insReloadTop(false);
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCollecOper" ACTION="valCollectionQue.aspx?mode=1">
<BR><BR>
		<TABLE WIDTH="100%">
			<TR>
			    <TD>
					<%=mobjValues.FIELDSET(999, "Consulta a través de")%>
					<%=mobjValues.OptionControl(40633, "optQuery", GetLocalResourceObject("optQuery_CStr1Caption"), eFunctions.Values.vbChecked, CStr(1), "InsChangeField(""Query"",this.value)", True)%>
					<%=mobjValues.OptionControl(40633, "optQuery", GetLocalResourceObject("optQuery_CStr2Caption"), eFunctions.Values.vbUnChecked, CStr(2), "InsChangeField(""Query"",this.value)", True)%></TD>
					<%=mobjValues.closeFIELDSET()%>
			    </TD>
			    <TD>&nbsp;</TD>
			    <TD>
					<%=mobjValues.FIELDSET(999, "Datos de la consulta")%>
					<TABLE WIDTH=100%>
						<TR>
						    <TD><LABEL ID=10521><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
							<TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  , "InsChangeField(""Branch"",this.value)", True,  , GetLocalResourceObject("cbeBranchToolTip"))%> </TD>
							<TD>&nbsp;</TD>
							<TD><LABEL ID=13771><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>            
							<TD><%With mobjValues
	.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "ShowDefValues(this)", True, 5, GetLocalResourceObject("valProductToolTip")))
End With
%>
							</TD>
						</TR>
						<TR>    
						    <TD><LABEL ID=9908><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
						    <TD><%=mobjValues.NumericControl("tcnPolicy", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "ShowDefValues(this)", True)%></TD>
						    <TD>&nbsp;</TD>
						    <TD><LABEL ID=9908><%= GetLocalResourceObject("tcnProposalCaption") %></LABEL></TD>
						    <TD><%=mobjValues.NumericControl("tcnProposal", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnProposalToolTip"),  ,  ,  ,  ,  , "ShowDefValues(this)", True)%></TD>
						</TR>        
					</TABLE>
					<%=mobjValues.closeFIELDSET()%>
				</TD>
			</TR>
	    </TABLE>
    

    <%=mobjValues.FIELDSET(999, "Datos de la póliza")%>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="12%"><LABEL ID=9908><%= GetLocalResourceObject("cbeWayPayCaption") %></LABEL></TD>            
            <TD WIDTH="17%"><%=mobjValues.PossiblesValues("cbeWayPay", "Table5002", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeWayPayToolTip"))%></TD>
            <TD WIDTH="10%"><LABEL ID=9908><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
            <TD WIDTH="28%"><%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOfficeToolTip"))%></TD>            
            <TD WIDTH="12%"><LABEL ID=9908><%= GetLocalResourceObject("tcdInitDateCaption") %></LABEL></TD>            
            <TD WIDTH="21%"><%=mobjValues.DateControl("tcdInitDate",  ,  , GetLocalResourceObject("tcdInitDateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=9908><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>            
            <TD><%=mobjValues.DateControl("tcdEndDate",  ,  , GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=9908><%= GetLocalResourceObject("dtcClientCaption") %></LABEL></TD>            
            <TD COLSPAN="3"><%=mobjValues.ClientControl("dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientToolTip"),  , True, "lblClieName")%></TD>
        </TR>            
        <TR>
            <TD><LABEL ID=9908><%= GetLocalResourceObject("tcnBalanceCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnBalance", 20, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnBalanceToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
            <TD COLSPAN="3"><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
        </TR>        
    </TABLE>
   	<%=mobjValues.closeFIELDSET()%>

</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("coc002_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




