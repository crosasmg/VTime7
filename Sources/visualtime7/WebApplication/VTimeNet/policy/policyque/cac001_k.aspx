<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mstrClient As String


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cac001_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cac001_k"

%>
<HTML>
<HEAD>
<SCRIPT>

//%insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
	
//%insStateZone: Ejecuta rutinas necesarias en el momento de las acciones del menu
//-------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------
	with(self.document.forms[0]){
		 optCertype[0].disabled=false;
		 optCertype[1].disabled=false;
		 optCertype[2].disabled=false;		 
		 optExecute[0].disabled=false;
		 optExecute[1].disabled=false;
	     tctClient.disabled=false;
	     btntctClient.disabled=false;
		 cbeBranch.disabled=false;
		 tcnPolicy.disabled=false;
		 tcdEffecdate.disabled=false;
		 btn_tcdEffecdate.disabled=false;
		 tctCreditnum.disabled=true;
		 tctAccnum.disabled=true;
	}
}

//% insChangeField: Se recargan los valores cuando cambia el campo
//-------------------------------------------------------------------------------------------
function insChangeField(Field){
//-------------------------------------------------------------------------------------------    

	with (self.document.forms[0]){
		switch(Field.name){
			case "optCertype":
				valCurrency.Parameters.Param1.sValue=Field.value;
				break;
			case "optExecute":
				break;
			case "cbeBranch":
				valProduct.Parameters.Param1.sValue=Field.value;
				valCurrency.Parameters.Param2.sValue=Field.value;
				break;
			case "valProduct":
				valCurrency.Parameters.Param3.sValue=Field.value;
				if (valProduct_sBrancht.value == "1"){
					tctCreditnum.disabled=false;
					tctAccnum.disabled=false;
				}
				else{
					tctCreditnum.disabled=true;
					tctAccnum.disabled=true;
				}	
				break;
			case "tcnPolicy":
				valCurrency.Parameters.Param4.sValue=Field.value;
				break;
			case "tcdEffecdate":
				valCurrency.Parameters.Param5.sValue=Field.value;
				break;
		}
	}
}

//% InsChangeClient: Despliega los datos del cliente
//-------------------------------------------------------------------------------------------
function InsChangeCurrency(){
//-------------------------------------------------------------------------------------------
	var lstrCertype

    with(self.document.forms[0]){
		if (optCertype[0].checked)
			lstrCertype = optCertype[0].value;
		else if	(optCertype[1].checked)
			lstrCertype = optCertype[1].value;
		else
			lstrCertype = optCertype[2].value;

		insDefValues('Curren_pol', "nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value + "&dEffecdate=" + tcdEffecdate.value + "&sCertype=" + lstrCertype)
	}        
}

</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>


<%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("CAC001", "CAC001_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmReahPolicy_K" ACTION="ValPolicyQue.aspx?x=1">
<BR></BR>
	<TABLE WIDTH=100%>
		<TR>
		    <TD WIDTH=25% COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Tipo de información"><%= GetLocalResourceObject("AnchorTipo de informaciónCaption") %></A></LABEL></TD>            
			<TD WIDTH=3%>&nbsp;</TD>
			<TD WIDTH=20%><LABEL><%= GetLocalResourceObject("tctClientCaption") %><LABEL></TD>
			<TD><%=mobjValues.ClientControl("tctClient", mstrClient, True, GetLocalResourceObject("tctClientToolTip"),  , True, "tctCliename",  ,  ,  ,  ,  , 5,  , Request.QueryString.Item("action") = "301")%></TD>
		</TR>
		<TR>
		    <TD COLSPAN="2" CLASS="Horline"></TD>	    
		    <TD COLSPAN="3"></TD>	    
		</TR>
		<TR>
   			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optCertype", GetLocalResourceObject("optCertype_2Caption"), "1", "2", "insChangeField(this);", True, 1)%></TD>
   			<TD>&nbsp;</TD>
			<TD><LABEL ID=13372><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></td>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), "", "valProduct",  ,  ,  , "insChangeField(this);", True, 6)%> </td>
		</TR>  		  
		<TR>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optCertype", GetLocalResourceObject("optCertype_3Caption"), "0", "3", "insChangeField(this);", True, 2)%></TD>
			<TD>&nbsp;</TD>
	        <TD><LABEL ID=13872><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
	        <TD><%
'+ Se crea parametro de salida para retornar el ramo tecnico (sBrancht)
With mobjValues.Parameters
	.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.ReturnValue("sBrancht", True, "Ramo técnico", True)
End With
Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "insChangeField(this);", True, 5, GetLocalResourceObject("valProductToolTip"),  , 7))
%>
			</TD>            
		</TR>
		<TR>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optCertype", GetLocalResourceObject("optCertype_1Caption"), "0", "1", "insChangeField(this);", True, 3)%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=13803><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 10, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insChangeField(this); InsChangeCurrency();", True, 8)%></TD>
		</TR>
		<TR>            
		    <TD COLSPAN="3">&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></td>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "insChangeField(this); InsChangeCurrency();", True, 9)%></TD>
		</TR>
		<TR>
		    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Estado de la información"><%= GetLocalResourceObject("AnchorEstado de la informaciónCaption") %></A></LABEL></TD>            
		    <TD>&nbsp;</TD>			
  		    <TD><LABEL ID=13097><%= GetLocalResourceObject("valCurrencyCaption") %></LABEL></TD>
			<TD><%
mobjValues.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nPolicy", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("dEffectdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("valCurrency", "tabCurren_polGroupP", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCurrencyToolTip"),  , 10))
%>
			</TD>
		</TR>  
		<TR>
			<TD COLSPAN="2" CLASS="Horline"></TD>
			<TD COLSPAN="3"></TD>	
		</TR>
		<TR>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_1Caption"), "1", "1", "insChangeField(this);", True, 4)%></TD>
		    <TD>&nbsp;</TD>			
			<TD><LABEL ID=0><%= GetLocalResourceObject("tctCreditnumCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctCreditnum", 20, "",  , GetLocalResourceObject("tctCreditnumToolTip"),  ,  ,  ,  , True, 11)%></TD>
    	</TR>
		<TR>
		    <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_2Caption"), "2", "2", "insChangeField(this);", True, 5)%></TD>
		    <TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tctAccnumCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctAccnum", 20, "",  , GetLocalResourceObject("tctAccnumToolTip"),  ,  ,  ,  , True, 12)%></TD>
		</TR>
    </TABLE>

</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("cac001_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




