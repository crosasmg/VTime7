<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agl774_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "agl774_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT LANGUAGE=JavaScript>

//%insChangeInter_typ: Se obtiene la fecha de ctrol_date dependiendo del tipo de intermediario seleccionado.
//------------------------------------------------------------------------------
function insChangeInter_typ(){
//------------------------------------------------------------------------------
	
	with(self.document.forms[0]){
	    insDefValues("LastProcess_date", "sValue=AGL774" + "&nInterTyp=" + cbeIntertyp.value, '/VTimeNet/Agent/AgentRep')
	}
}

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
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
    return true;
}

//%insChangeField: Control de cambio de parámetros
//--------------------------------------------------------------------------------------------
function insChangeField(oField){
//--------------------------------------------------------------------------------------------

    with(self.document.forms[0]){
        switch(oField.name){
    	case 'cbeInsur_Area':
    		valBranch.value='';
    		break;
    	case 'valBranch':
    		valProduct.Parameters.Param1.sValue=oField.value;
    		
    		valProduct.disabled = btnvalProduct.disabled = (oField.value=='0'||oField.value=='');
    		valProduct.value=''
    		UpdateDiv('valProductDesc', '');
    		break;

        case 'cbeIntertyp':
            if (oField.value=='1')
                hddType_proce.value = '21';
            
            else if (oField.value=='6')
                hddType_proce.value = '26';            
        }
    }
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		
<%Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("AGL774", "AGL774_K.aspx", 1, vbNullString))
	Response.Write(mobjMenu.setZone(1, "AGL774", "AGL774"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" NAME="AGL774_K" ACTION="ValAgentRep.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("AGL774", Request.QueryString.Item("sWindowDescript")))%>
    <TABLE WIDTH="100%">
        <TR>
	        <TD><LABEL ID=0><%= GetLocalResourceObject("cbeInsur_AreaCaption") %></LABEL></TD>
	    	<TD COLSPAN="4">
	    	<%mobjValues.BlankPosition = 0
Response.Write(mobjValues.PossiblesValues("cbeInsur_Area", "table5001", eFunctions.Values.eValuesType.clngComboType, "", False,  ,  ,  ,  , "insChangeField(this);",  ,  , GetLocalResourceObject("cbeInsur_AreaToolTip")))
%>
	    	</TD>
	    </TR>
        <TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("valBranchCaption") %></LABEL></TD>
	        <TD COLSPAN="4"><%=mobjValues.BranchControl("valBranch", GetLocalResourceObject("valBranchToolTip"), vbNullString, "valProduct")%></TD>		    </TD>
        </TR>		
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), vbNullString, eFunctions.Values.eValuesType.clngWindowType)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeIntertypCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%mobjValues.TypeList = 1
mobjValues.List = "1,6"
Response.Write(mobjValues.PossiblesValues("cbeIntertyp", "tabinter_typ_annu", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  , "insChangeField(this);insChangeInter_typ();", False,  , GetLocalResourceObject("cbeIntertypToolTip")))%> </TD>
        </TR>
		<TR>
	        <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
	        <TD><%=mobjValues.DateControl("tcdEffecdate", vbNullString, True, GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , False)%> </TD>
	        <TD>&nbsp;</TD>
	        <TD><LABEL ID=0><%= GetLocalResourceObject("tcdExpirdatCaption") %></LABEL></TD>
	        <TD><%=mobjValues.DateControl("tcdExpirdat", vbNullString, True, GetLocalResourceObject("tcdExpirdatToolTip"),  ,  ,  ,  , False)%> </TD>
        </TR>
        <%=mobjValues.HiddenControl("hddType_proce", "")%>        
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("agl774_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




