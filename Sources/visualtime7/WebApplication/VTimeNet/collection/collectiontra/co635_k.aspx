<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolPremiums As Object


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CO635_K")
'~End Header Block VisualTimer Utility

Response.CacheControl = "private"

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "CO635_K"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/constantes.js"></SCRIPT>
    <SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 2 $|$$Date: 4/09/03 11:42 $|$$Author: Nvaplat37 $"
    </SCRIPT>

<SCRIPT LANGUAGE=JavaScript>

//% ShowChangeValues: Se habilitan/deshabilitan los controles de acuerdo a lo definido para 
//%                      producto, póliza o certificado
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        lstrCertype = 2;
        
        switch(sField){
            case "nBranch":
                document.forms[0].tcnPolicy.value = '';
			case "Policy":
				if (document.forms[0].tcnPolicy.value != ''){
					insDefValues("insShowPolicy", "nPolicy=" + tcnPolicy.value,'/VTimeNet/collection/collectiontra');
                }
        }
    }
}
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(Action){
//--------------------------------------------------------------------------------------------
	var lblnQuery = (Action==401);

	with(top.frames['fraHeader'].document.forms[0]){
		cbeAgency.disabled=false;
		cbeBranch.disabled=false;
		tcnPolicy.disabled=false;
		valCollectorPre.disabled=false;
		btnvalCollectorPre.disabled=false;
		valCollectorPre.value='';
		cbeCollectortype.value='';
		cbeContype.value='';
		optColltype[0].checked=false;
		optColltype[1].checked=!lblnQuery;
		optColltype[2].checked=false;
		optColltype[0].disabled=lblnQuery;
		optColltype[1].disabled=lblnQuery;
		optColltype[2].disabled=lblnQuery;
		cbeWay_Pay.disabled=lblnQuery;
		cbeAgency.value='';
		cbeBranch.value='';
	}
	UpdateDiv('valCollectorPreDesc','','Normal');

}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return(true);
}
//% DisabledField: Habilita o deshabilita segun corresponda
//--------------------------------------------------------------------------------------------
function DisabledField(objField){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if (objField.name == 'cbeWay_Pay' && objField.value != '0'){
			tcdLimitdate.disabled=false;
			btn_tcdLimitdate.disabled=false;
		}
		else{
			tcdLimitdate.disabled=true;
			btn_tcdLimitdate.disabled=true;
			tcdLimitdate.value = '';
			if (optColltype[1].checked){
				cbeWay_Pay.disabled=false;
			}
			else{
				cbeWay_Pay.disabled=true;
				cbeWay_Pay.value = ''
			}
		}
	}
}

//% insChangeCollector: Llama a la función insDefValues para recuperar los datos de la cabecera
//--------------------------------------------------------------------------------------------
function insChangeCollector(value){
//--------------------------------------------------------------------------------------------
	if(value !='')
		insDefValues("CO635", "nCollector=" + value, '/VTimeNet/Collection/CollectionTra')
	else{
		with(self.document.forms[0]){
		    valCollectorPre.value='';
		    cbeCollectortype.value=0;
		    cbeContype.value=0;
            };
	    }
}

</SCRIPT>
	<%

Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("CO635", "CO635_k.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR>
<BR>
<FORM ACTION=valCollectionTra.aspx?sMode=2 METHOD=post NAME=CO635>
	<TABLE WIDTH="100%">
        <TR>
          <TD WIDTH="20%">&nbsp;</TD>
          <TD WIDTH="20%" ALIGN=RIGHT CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
          <TD WIDTH="20%">&nbsp</TD>
          <TD WIDTH="20%">&nbsp</TD>
          <TD WIDTH="20%">&nbsp</TD>
        </TR>
        <TR>
          <TD WIDTH="20%" WIDTH="20%" COLSPAN=2><HR></TD>
          <TD WIDTH="20%"></TD>
          <TD WIDTH="20%"></TD>
          <TD WIDTH="20%"></TD>
          <TD WIDTH="20%"></TD>
        </TR>
	</TABLE>

	<TABLE WIDTH="100%">
		<TR>
			<TD WIDTH="20%"><LABEL ID=0><%= GetLocalResourceObject("valCollectorPreCaption") %></LABEL></TD>
			<TD WIDTH="30%"><%=mobjValues.PossiblesValues("valCollectorPre", "tabCollector_Cliname", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , "insChangeCollector(this.value)", True, 10, GetLocalResourceObject("valCollectorPreToolTip"))%></TD>
			<TD WIDTH="5%">&nbsp</TD>
            <TD WIDTH="15%"><LABEL><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD WIDTH="30%"><%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
		    <TD WIDTH="20%"><LABEL ID=0><%= GetLocalResourceObject("cbeCollectortypeCaption") %></LABEL></TD>
		    <TD WIDTH="30%"><%=mobjValues.PossiblesValues("cbeCollectortype", "Table5551", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCollectortypeToolTip"))%> </TD>
			<TD WIDTH="5%">&nbsp</TD>
            <TD WIDTH="15%"><LABEL><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD WIDTH="30%"><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), "", "valProduct",  ,  ,  , "ShowChangeValues(""nBranch"")", True)%> </TD>
		</TR>
		<TR>
			<TD WIDTH="20%"><LABEL ID=0><%= GetLocalResourceObject("cbeContypeCaption") %></LABEL></TD>
			<TD WIDTH="30%"><%=mobjValues.PossiblesValues("cbeContype", "Table5557", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeContypeToolTip"))%> </TD>
			<TD WIDTH="5%">&nbsp</TD>
            <TD WIDTH="15%"><LABEL><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD WIDTH="30%"><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), "",  , True, "")%></TD>
		</TR>
		<TR>
			<TD WIDTH="20%">&nbsp</TD>
			<TD ALIGN=RIGHT WIDTH="30%" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
			<TD WIDTH="5%">&nbsp</TD>
			<TD WIDTH="15%"><LABEL><%= GetLocalResourceObject("tcnPolicyCaption") %><LABEL></TD>
			<TD WIDTH="30%"><%=mobjValues.NumericControl("tcnPolicy", 10, "", False, GetLocalResourceObject("tcnPolicyToolTip"), False, False,  ,  ,  , "ShowChangeValues(""Policy"")", True)%></TD>
		</TR>
		<TR>
			<TD COLSPAN=2><HR></TD>
			<TD WIDTH="5%">&nbsp</TD>
            <TD WIDTH="15%"><LABEL><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL></TD>
            <TD WIDTH="30%"><%=mobjValues.PossiblesValues("cbeAgency", "Table5555", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeAgencyToolTip"))%> </TD>
		</TR>
		<TR>
			<TD WIDTH="20%"><%=mobjValues.OptionControl(0, "optColltype", GetLocalResourceObject("optColltype_0Caption"),  , "0", "DisabledField(this)", True)%> </TD>
			<TD WIDTH="30%"><%=mobjValues.OptionControl(0, "optColltype", GetLocalResourceObject("optColltype_1Caption"),  , "1", "DisabledField(this)", True)%></TD>
			<TD WIDTH="5%">&nbsp</TD>
			<TD WIDTH="15%"><LABEL><%= GetLocalResourceObject("cbeWay_PayCaption") %></LABEL></TD>
			<TD WIDTH="30%"><%mobjValues.TypeList = 1
mobjValues.List = "1,2"
Response.Write(mobjValues.PossiblesValues("cbeWay_Pay", "TABLE5002", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , "DisabledField(this);", True,  , GetLocalResourceObject("cbeWay_PayToolTip")))%></TD>

		</TR>
		<TR>
			<TD WIDTH="20%"><%=mobjValues.OptionControl(0, "optColltype", GetLocalResourceObject("optColltype_2Caption"),  , "2", "DisabledField(this)", True)%></TD>
			<TD WIDTH="30%">&nbsp</TD>
			<TD WIDTH="5%">&nbsp</TD>
			<TD WIDTH="15%"><LABEL><%= GetLocalResourceObject("tcdLimitdateCaption") %></LABEL></TD>
			<TD WIDTH="30%"><%=mobjValues.DateControl("tcdLimitdate",  ,  , GetLocalResourceObject("tcdLimitdateToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("CO635_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





